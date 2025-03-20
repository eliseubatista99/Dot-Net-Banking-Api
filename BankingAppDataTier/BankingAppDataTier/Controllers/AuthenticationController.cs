using BankingAppDataTier.Contracts.Constants;
using BankingAppDataTier.Contracts.Database;
using BankingAppDataTier.Contracts.Dtos.Entitites;
using BankingAppDataTier.Contracts.Dtos.Inputs.Accounts;
using BankingAppDataTier.Contracts.Dtos.Inputs.Authentication;
using BankingAppDataTier.Contracts.Dtos.Outputs;
using BankingAppDataTier.Contracts.Dtos.Outputs.Accounts;
using BankingAppDataTier.Contracts.Dtos.Outputs.Authentication;
using BankingAppDataTier.Contracts.Enums;
using BankingAppDataTier.Contracts.Errors;
using BankingAppDataTier.Contracts.Providers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace BankingAppDataTier.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : Controller
    {
        private readonly ILogger<AuthenticationController> logger;
        private readonly IAuthenticationProvider authenticationProvider;
        private readonly IDatabaseClientsProvider databaseClientsProvider;
        private readonly IDatabaseTokenProvider databaseTokensProvider;

        public AuthenticationController(
            ILogger<AuthenticationController> _logger,
            IAuthenticationProvider _authProvider,
            IDatabaseClientsProvider _dbClientsProvider,
            IDatabaseTokenProvider _dbTokensProvider)
        {
            logger = _logger;
            authenticationProvider = _authProvider;
            databaseClientsProvider = _dbClientsProvider;
            databaseTokensProvider = _dbTokensProvider;
        }

        [HttpPost("Authenticate")]
        public ActionResult<AuthenticateOutput> Authenticate([FromBody] AuthenticateInput input)
        {
            var clientInDb = databaseClientsProvider.GetById(input.ClientId);

            if (clientInDb == null)
            {
                return BadRequest(new AuthenticateOutput()
                {
                    Token = string.Empty,
                    Error = AuthenticationErrors.InvalidClient,
                });
            }

            if (!ValidateCode(clientInDb, input.AuthenticationCode))
            {
                return Unauthorized(new AuthenticateOutput()
                {
                    Token = string.Empty,
                    Error = AuthenticationErrors.WrongCode,
                });
            }

            var deleteResult = databaseTokensProvider.DeleteTokensOfClient(input.ClientId);

            if (!deleteResult)
            {
                return new InternalServerError(new KeepAliveOutput()
                {
                    Error = AuthenticationErrors.FailedToGenerateToken,
                });
            }

            var (token, expirationDateTime) = authenticationProvider.GenerateToken(input.ClientId);

            var insertResult = databaseTokensProvider.Add(new TokenTableEntry
            {
                Token = token,
                ClientId = input.ClientId,
                ExpirationDate = expirationDateTime,
            });

            if (!insertResult)
            {
                return new InternalServerError(new AuthenticateOutput()
                {
                    Token = string.Empty,
                    Error = AuthenticationErrors.FailedToGenerateToken,
                });
            }

            return Ok(new AuthenticateOutput()
            {
                ExpirationDateTime = expirationDateTime,
                Token = token,
            });
        }

        [HttpPost("KeepAlive")]
        public ActionResult<KeepAliveOutput> KeepAlive([FromBody] KeepAliveInput input)
        {
            var tokenInDb = databaseTokensProvider.GetToken(input.Token);

            if (tokenInDb == null)
            {
                return BadRequest(new KeepAliveOutput()
                {
                    Error = AuthenticationErrors.InvalidToken,
                });
            }

            var tokenValidationResult = authenticationProvider.IsValidToken(input.Token);

            if (!tokenValidationResult.isValid)
            {
                return Unauthorized(new KeepAliveOutput()
                {
                    Error = AuthenticationErrors.InvalidToken,
                });
            }

            var today = DateTime.Now;

            if (tokenValidationResult.expirationTime.Ticks <= today.Ticks)
            {
                return Unauthorized(new KeepAliveOutput()
                {
                    Error = AuthenticationErrors.TokenExpired,
                });
            }

            var lifeTime = authenticationProvider.GetTokenLifeTime();

            var newExpirationTime = today.AddMinutes(lifeTime);

            var result = databaseTokensProvider.SetExpirationDateTime(tokenInDb.Token, newExpirationTime);

            if (!result)
            {
                return new InternalServerError(new KeepAliveOutput()
                {
                    Error = AuthenticationErrors.FailedToKeepAlive,
                });
            }

            return Ok(new KeepAliveOutput()
            {
                ExpirationDateTime = newExpirationTime,
            });
        }

        [HttpPost("IsValidToken")]
        public ActionResult<IsValidTokenOutput> IsValidToken([FromBody] IsValidTokenInput input)
        {
            var tokenInDb = databaseTokensProvider.GetToken(input.Token);

            if (tokenInDb == null)
            {
                return Ok(new IsValidTokenOutput()
                {
                    IsValid = false,
                });
            }

            var tokenValidationResult = authenticationProvider.IsValidToken(input.Token);

            var today = DateTime.Now;

            if (tokenValidationResult.expirationTime.Ticks <= today.Ticks)
            {
                return Ok(new IsValidTokenOutput()
                {
                    IsValid = false,
                });
            }

            return Ok(new IsValidTokenOutput()
            {
                ExpirationDateTime = tokenInDb.ExpirationDate,
                IsValid = tokenValidationResult.isValid,
            });
        }

        [HttpPost("GetAuthenticationPositions")]
        public ActionResult<GetAuthenticationPositionsOutput> GetAuthenticationPositions([FromBody] GetAuthenticationPositionsInput input)
        {
            var clientInDb = databaseClientsProvider.GetById(input.ClientId);

            if (clientInDb == null)
            {
                return BadRequest(new AuthenticateOutput()
                {
                    Token = string.Empty,
                    Error = AuthenticationErrors.InvalidClient,
                });
            }

            Random rnd = new Random();

            var numberOfPositions = input.NumberOfPositions ?? BankingAppDataTierConstants.DEDFAULT_NUMBER_OF_POSITIONS;
            var positions = Enumerable.Range(0, clientInDb.Password.Length).ToList();
            var result = new List<int>();

            for (int i = 0; i < numberOfPositions; i++)
            {
                var randomPos = rnd.Next(0, positions.Count - 1);
                var newPos = positions[randomPos];

                result.Add(newPos);
                positions = positions.Where(p => p != newPos).ToList();
            }

            return Ok(new GetAuthenticationPositionsOutput()
            {
                Positions = result,
            });
        }



        private bool ValidateCode(ClientsTableEntry client, AuthenticationCodeDto code)
        {
            var passwordByChar = client.Password.ToCharArray();

            foreach (var codeItem in code.Code)
            {
                // If the code position is greater than the number of password chars, something is wrong
                if (codeItem.Position >= passwordByChar.Length)
                {
                    return false;
                }

                // If any position is differente, code is wrong
                if (passwordByChar[codeItem.Position] != codeItem.Value)
                {
                    return false;
                }
            }

            return true;
        }

    }
}

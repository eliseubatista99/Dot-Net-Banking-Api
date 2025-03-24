using BankingAppDataTier.Contracts.Constants;
using BankingAppDataTier.Contracts.Database;
using BankingAppDataTier.Contracts.Dtos.Entitites;
using BankingAppDataTier.Contracts.Dtos.Inputs.Authentication;
using BankingAppDataTier.Contracts.Dtos.Outputs.Authentication;
using BankingAppDataTier.Contracts.Errors;
using BankingAppDataTier.Contracts.Providers;
using ElideusDotNetFramework.Providers.Contracts;
using Microsoft.AspNetCore.Mvc;


namespace BankingAppDataTier.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : Controller
    {
        private readonly ILogger logger;
        private readonly IAuthenticationProvider authenticationProvider;
        private readonly IDatabaseClientsProvider databaseClientsProvider;
        private readonly IDatabaseTokenProvider databaseTokensProvider;

        public AuthenticationController(IApplicationContext _executionContext)
        {
            logger = _executionContext.GetDependency<ILogger>()!;
            authenticationProvider = _executionContext.GetDependency<IAuthenticationProvider>()!;
            databaseClientsProvider = _executionContext.GetDependency<IDatabaseClientsProvider>()!;
            databaseTokensProvider = _executionContext.GetDependency<IDatabaseTokenProvider>()!;
        }


        [HttpPost("GetAuthenticationPositions")]
        public ActionResult<GetAuthenticationPositionsOutput> GetAuthenticationPositions([FromBody] GetAuthenticationPositionsInput input)
        {
            var clientInDb = databaseClientsProvider.GetById(input.ClientId);

            if (clientInDb == null)
            {
                return BadRequest(new GetAuthenticationPositionsOutput()
                {
                    Positions = new List<int>(),
                    Error = AuthenticationErrors.InvalidClient,
                });
            }

            if(clientInDb.Password.Length == 0)
            {
                return Ok(new GetAuthenticationPositionsOutput()
                {
                    Positions = new List<int>(),
                });
            }

            Random rnd = new Random();

            var numberOfPositions = input.NumberOfPositions ?? BankingAppDataTierConstants.DEDFAULT_NUMBER_OF_POSITIONS;

            var positions = Enumerable.Range(0, clientInDb.Password.Length).Select(x => (int) x).ToList();
            var result = new List<int>();

            for (int i = 0; i < numberOfPositions; i++)
            {
                //If there is no more to take, end the loop
                if(positions.Count == 0)
                {
                    break;
                }

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



        private bool ValidateCode(ClientsTableEntry client, List<AuthenticationCodeItemDto> code)
        {
            var passwordByChar = client.Password.ToCharArray();

            foreach (var codeItem in code)
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

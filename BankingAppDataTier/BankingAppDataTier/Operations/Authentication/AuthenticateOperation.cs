using BankingAppDataTier.Contracts.Database;
using BankingAppDataTier.Contracts.Dtos;
using BankingAppDataTier.Contracts.Errors;
using BankingAppDataTier.Contracts.Operations.Inputs.Authentication;
using BankingAppDataTier.Contracts.Operations.Outputs.Authentication;
using BankingAppDataTier.Contracts.Providers;
using ElideusDotNetFramework.Core;
using System.Net;

namespace BankingAppDataTier.Operations.Authentication
{
    public class AuthenticateOperation(IApplicationContext context, string endpoint)
        : BankingAppDataTierOperation<AuthenticateInput, AuthenticateOutput>(context, endpoint)
    {
        protected override bool UseAuthentication { get; set; } = false;

        private IDatabaseClientsProvider databaseClientsProvider;

        protected override async Task InitAsync()
        {
            await base.InitAsync();

            databaseClientsProvider = executionContext.GetDependency<IDatabaseClientsProvider>()!;
        }

        protected override async Task<AuthenticateOutput> ExecuteAsync(AuthenticateInput input)
        {
             var clientInDb = databaseClientsProvider.GetById(input.ClientId);

            if (clientInDb == null)
            {
                return new AuthenticateOutput
                {
                    Token = string.Empty,
                    Error = AuthenticationErrors.InvalidClient,
                    StatusCode = HttpStatusCode.NotFound,
                };
            }

            if (!ValidateCode(clientInDb, input.AuthenticationCode))
            {
                return new AuthenticateOutput
                {
                    Token = string.Empty,
                    Error = AuthenticationErrors.WrongCode,
                    StatusCode = HttpStatusCode.Unauthorized,
                };
            }

            var deleteResult = databaseTokensProvider.DeleteTokensOfClient(input.ClientId);

            if (!deleteResult)
            {
                return new AuthenticateOutput
                {
                    Token = string.Empty,
                    Error = AuthenticationErrors.FailedToGenerateToken,
                    StatusCode = HttpStatusCode.InternalServerError,
                };
            }

            var (token, expirationDateTime) = authProvider.GenerateToken(input.ClientId);

            var insertResult = databaseTokensProvider.Add(new TokenTableEntry
            {
                Token = token,
                ClientId = input.ClientId,
                ExpirationDate = expirationDateTime,
            });

            if (!insertResult)
            {
                return new AuthenticateOutput
                {
                    Token = string.Empty,
                    Error = AuthenticationErrors.FailedToGenerateToken,
                    StatusCode = HttpStatusCode.InternalServerError,
                };
            }

            return new AuthenticateOutput
            {
                ExpirationDateTime = expirationDateTime,
                Token = token,
            };
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

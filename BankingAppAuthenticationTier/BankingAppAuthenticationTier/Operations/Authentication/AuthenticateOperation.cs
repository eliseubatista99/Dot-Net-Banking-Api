using BankingAppAuthenticationTier.Contracts.Dtos;
using BankingAppAuthenticationTier.Contracts.Operations;
using BankingAppAuthenticationTier.Library.Database;
using BankingAppAuthenticationTier.Library.Errors;
using BankingAppAuthenticationTier.Library.Providers;
using ElideusDotNetFramework.Core;
using System.Net;

namespace BankingAppAuthenticationTier.Operations
{
    public class AuthenticateOperation(IApplicationContext context, string endpoint)
        : BankingAppAuthenticationTierOperation<AuthenticateInput, AuthenticateOutput>(context, endpoint)
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
                    Error = AuthenticationErrors.InvalidClient,
                    StatusCode = HttpStatusCode.NotFound,
                };
            }

            if (!ValidateCode(clientInDb, input.AuthenticationCode))
            {
                return new AuthenticateOutput
                {
                    Error = AuthenticationErrors.WrongCode,
                    StatusCode = HttpStatusCode.Unauthorized,
                };
            }
           
            var (token, refreshToken) = authProvider.GenerateTokens(input.ClientId);

            if (token == null || refreshToken == null)
            {
                return new AuthenticateOutput
                {
                    Error = AuthenticationErrors.FailedToGenerateToken,
                    StatusCode = HttpStatusCode.InternalServerError,
                };
            }

            return new AuthenticateOutput
            {
                Token = token,
                RefreshToken = refreshToken,
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

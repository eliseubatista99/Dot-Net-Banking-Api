using BankingAppDataTier.Contracts.Errors;
using BankingAppDataTier.Contracts.Providers;
using ElideusDotNetFramework.Errors.Contracts;
using ElideusDotNetFramework.Operations;
using ElideusDotNetFramework.Operations.Contracts;
using ElideusDotNetFramework.Providers.Contracts;
using System.Net;

namespace BankingAppDataTier.Operations
{
    public class BankingAppDataTierOperation<TIn, TOut>(IApplicationContext context, string endpoint) :
        BaseOperation<TIn, TOut>(context, endpoint) where TIn : OperationInput where TOut : OperationOutput
    {
        protected virtual bool UseAuthentication { get; set; } = true;

        protected IMapperProvider mapperProvider;
        protected IAuthenticationProvider authProvider;
        protected IDatabaseTokenProvider databaseTokensProvider;

        protected override async Task InitAsync()
        {
            await base.InitAsync();

            mapperProvider = executionContext.GetDependency<IMapperProvider>()!;
            authProvider = executionContext.GetDependency<IAuthenticationProvider>()!;
            databaseTokensProvider = executionContext.GetDependency<IDatabaseTokenProvider>()!;
        }

        protected virtual Error? ValidateToken(string token)
        {
            var isValidResult = authProvider.IsValidToken(token);

            if (!isValidResult.isValid)
            {
                return AuthenticationErrors.InvalidToken;
            }

            var tokenInDb = databaseTokensProvider.GetToken(token);

            if (tokenInDb == null)
            {
                return AuthenticationErrors.InvalidToken;
            }

            var today = DateTime.Now;

            if (tokenInDb.ExpirationDate.Ticks <= today.Ticks)
            {
                return AuthenticationErrors.TokenExpired;
            }

            return null;
        } 


        protected override async Task<(HttpStatusCode? StatusCode, Error? Error)> ValidateInput(TIn input)
        {
            if (UseAuthentication)
            {
                if (input.Metadata?.Token == null || input.Metadata.Token == String.Empty)
                {
                    var invalidInputError = GenericErrors.InvalidInputError(nameof(input.Metadata.Token));
                    return (HttpStatusCode.BadRequest, invalidInputError);
                }

                var validationError = ValidateToken(input.Metadata.Token);

                if (validationError != null)
                {
                    return (HttpStatusCode.Unauthorized, validationError);
                }
            }

            return await base.ValidateInput(input);
        }
    }
}

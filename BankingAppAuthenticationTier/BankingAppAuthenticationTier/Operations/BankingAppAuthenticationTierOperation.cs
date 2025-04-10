using ElideusDotNetFramework.Core.Errors;
using ElideusDotNetFramework.Core.Operations;
using ElideusDotNetFramework.Core;
using System.Net;
using BankingAppAuthenticationTier.Library.Errors;

namespace BankingAppAuthenticationTier.Operations
{
    public class BankingAppAuthenticationTierOperation<TIn, TOut>(IApplicationContext context, string endpoint) :
        BaseOperation<TIn, TOut>(context, endpoint) where TIn : OperationInput where TOut : OperationOutput
    {
        protected virtual bool UseAuthentication { get; set; } = true;

        protected IMapperProvider mapperProvider;
        protected IAuthenticationProvider authProvider;

        protected override async Task InitAsync()
        {
            await base.InitAsync();

            mapperProvider = executionContext.GetDependency<IMapperProvider>()!;
            authProvider = executionContext.GetDependency<IAuthenticationProvider>()!;
        }

        protected virtual Error? ValidateToken(string token)
        {
            if (token == null)
            {
                return AuthenticationErrors.InvalidToken;
            }

            var isValidResult = authProvider.IsValidToken(token);

            if (!isValidResult.isValid)
            {
                return AuthenticationErrors.InvalidToken;
            }

            return null;
        } 


        protected override async Task<(HttpStatusCode? StatusCode, Error? Error)> ValidateInput(HttpRequest request, TIn input)
        {
            if (UseAuthentication)
            {
                var token = request.Headers.Authorization.FirstOrDefault();

                if (token == null)
                {
                    return (HttpStatusCode.Unauthorized, AuthenticationErrors.InvalidToken);
                }

                var validationError = ValidateToken(token);

                if (validationError != null)
                {
                    return (HttpStatusCode.Unauthorized, validationError);
                }
            }

            return await base.ValidateInput(request, input);
        }
    }
}

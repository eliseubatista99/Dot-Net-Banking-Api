using BankingAppDataTier.Library.Database;
using BankingAppDataTier.Library.Errors;
using BankingAppDataTier.Library.Providers;
using ElideusDotNetFramework.Core.Errors;
using ElideusDotNetFramework.Core.Operations;
using ElideusDotNetFramework.Core;
using System.Net;

namespace BankingAppDataTier.Operations
{
    public class BankingAppDataTierOperation<TIn, TOut>(IApplicationContext context, string endpoint) :
        BaseOperation<TIn, TOut>(context, endpoint) where TIn : OperationInput where TOut : OperationOutput
    {
        protected virtual bool UseAuthentication { get; set; } = true;

        protected IMapperProvider mapperProvider;
        protected IAuthenticationTierProvider authTierProvider;

        protected override async Task InitAsync()
        {
            await base.InitAsync();

            mapperProvider = executionContext.GetDependency<IMapperProvider>()!;
            authTierProvider = executionContext.GetDependency<IAuthenticationTierProvider>()!;
        }

        protected virtual async Task<Error?> ValidateToken(string token)
        {
            var isValidResult = await authTierProvider.IsValidToken(new BankingAppAuthenticationTier.Contracts.Operations.IsValidTokenInput
            {
                Token = token!,
            });

            if (!isValidResult.IsValid)
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

                //if (token == null)
                //{
                //    var invalidInputError = InputErrors.InvalidInputField(nameof(token));
                //    return (HttpStatusCode.Unauthorized, invalidInputError);
                //}

                var validationError = await ValidateToken(token);

                if (validationError != null)
                {
                    return (HttpStatusCode.Unauthorized, validationError);
                }
            }

            return await base.ValidateInput(request, input);
        }
    }
}

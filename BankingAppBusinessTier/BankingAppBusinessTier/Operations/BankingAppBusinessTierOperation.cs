using BankingAppBusinessTier.Library.Errors;
using BankingAppBusinessTier.Library.Providers;
using ElideusDotNetFramework.Core.Errors;
using ElideusDotNetFramework.Core.Operations;
using ElideusDotNetFramework.Core;
using System.Net;

namespace BankingAppBusinessTier.Operations
{
    public class BankingAppBusinessTierOperation<TIn, TOut>(IApplicationContext context, string endpoint) :
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

        protected virtual async Task<Error?> ValidateToken(TIn input)
        {
            var isValidResult = await authTierProvider.IsValidToken(new BankingAppAuthenticationTier.Contracts.Operations.IsValidTokenInput
            {
                Token = input.Metadata!.Token!,
                Metadata = input.Metadata,
            });

            if (!isValidResult.IsValid)
            {
                return AuthenticationErrors.InvalidToken;
            }

            return null;
        }


        protected override async Task<(HttpStatusCode? StatusCode, Error? Error)> ValidateInput(TIn input)
        {
            if (UseAuthentication)
            {
                if (input.Metadata?.Token == null || input.Metadata.Token == String.Empty)
                {
                    var invalidInputError = InputErrors.InvalidInputField(nameof(input.Metadata.Token));
                    return (HttpStatusCode.BadRequest, invalidInputError);
                }

                var validationError = await ValidateToken(input);

                if (validationError != null)
                {
                    return (HttpStatusCode.Unauthorized, validationError);
                }
            }

            return await base.ValidateInput(input);
        }
    }
}

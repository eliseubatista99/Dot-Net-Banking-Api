using BankingAppAuthenticationTier.Contracts.Operations;
using ElideusDotNetFramework.Core;

namespace BankingAppAuthenticationTier.Operations
{
    public class IsValidTokenOperation(IApplicationContext context, string endpoint)
        : BankingAppAuthenticationTierOperation<IsValidTokenInput, IsValidTokenOutput>(context, endpoint)
    {
        protected override bool UseAuthentication { get; set; } = false;

        protected override async Task<IsValidTokenOutput> ExecuteAsync(IsValidTokenInput input)
        {
            var (isValid, _, _) = authProvider.IsValidToken(input.Token);

            return new IsValidTokenOutput()
            {
                IsValid = isValid,
            };
        }
    }
}

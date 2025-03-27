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
            var (token, tokenValidationError) = ValidateToken(input.Token);

            if(tokenValidationError != null)
            {
                return new IsValidTokenOutput()
                {
                    ExpirationDateTime = token?.ExpirationDate,
                    IsValid = false,
                    Reason = tokenValidationError.Code,
                };
            }

            return new IsValidTokenOutput()
            {
                ExpirationDateTime = token?.ExpirationDate,
                IsValid = true,
            };
        }
    }
}

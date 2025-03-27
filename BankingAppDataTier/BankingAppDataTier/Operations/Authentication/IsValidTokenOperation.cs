using BankingAppDataTier.Contracts.Operations.Inputs.Authentication;
using BankingAppDataTier.Contracts.Operations.Outputs.Authentication;
using ElideusDotNetFramework.Core;

namespace BankingAppDataTier.Operations.Authentication
{
    public class IsValidTokenOperation(IApplicationContext context, string endpoint)
        : BankingAppDataTierOperation<IsValidTokenInput, IsValidTokenOutput>(context, endpoint)
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

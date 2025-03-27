using ElideusDotNetFramework.Operations;

namespace BankingAppDataTier.Contracts.Dtos.Inputs.Authentication
{
    public class IsValidTokenInput : OperationInput
    {
        public required string Token { get; set; }

    }
}

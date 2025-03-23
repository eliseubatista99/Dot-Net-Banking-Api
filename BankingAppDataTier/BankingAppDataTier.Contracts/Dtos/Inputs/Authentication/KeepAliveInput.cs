using ElideusDotNetFramework.Operations.Contracts;

namespace BankingAppDataTier.Contracts.Dtos.Inputs.Authentication
{
    public class KeepAliveInput : OperationInput
    {
        public required string Token { get; set; }

    }
}

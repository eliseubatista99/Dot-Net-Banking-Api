using ElideusDotNetFramework.Operations;

namespace BankingAppDataTier.Contracts.Dtos.Outputs.Authentication
{
    public class AuthenticateOutput : OperationOutput
    {
        public required string Token { get; set; }

        public DateTime? ExpirationDateTime { get; set; }
    }
}

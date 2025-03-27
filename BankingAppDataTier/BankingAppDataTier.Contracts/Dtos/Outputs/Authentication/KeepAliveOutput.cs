using ElideusDotNetFramework.Core.Operations;

namespace BankingAppDataTier.Contracts.Dtos.Outputs.Authentication
{
    public class KeepAliveOutput : OperationOutput
    {
        public DateTime? ExpirationDateTime { get; set; }
    }
}

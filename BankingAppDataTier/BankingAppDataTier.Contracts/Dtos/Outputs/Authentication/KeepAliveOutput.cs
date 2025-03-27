using ElideusDotNetFramework.Core.Operations;
using System.Diagnostics.CodeAnalysis;

namespace BankingAppDataTier.Contracts.Dtos.Outputs.Authentication
{
    [ExcludeFromCodeCoverage]

    public class KeepAliveOutput : OperationOutput
    {
        public DateTime? ExpirationDateTime { get; set; }
    }
}

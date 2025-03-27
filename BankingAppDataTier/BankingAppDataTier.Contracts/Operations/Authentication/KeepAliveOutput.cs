using ElideusDotNetFramework.Core.Operations;
using System.Diagnostics.CodeAnalysis;

namespace BankingAppDataTier.Contracts.Operations.Authentication
{
    [ExcludeFromCodeCoverage]

    public class KeepAliveOutput : OperationOutput
    {
        public DateTime? ExpirationDateTime { get; set; }
    }
}

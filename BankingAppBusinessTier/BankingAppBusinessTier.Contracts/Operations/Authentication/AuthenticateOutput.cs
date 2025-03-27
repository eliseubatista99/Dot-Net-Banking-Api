using ElideusDotNetFramework.Core.Operations;
using System.Diagnostics.CodeAnalysis;

namespace BankingAppBusinessTier.Contracts.Operations.Authentication
{
    [ExcludeFromCodeCoverage]

    public class AuthenticateOutput : OperationOutput
    {
        public required string Token { get; set; }

        public DateTime? ExpirationDateTime { get; set; }
    }
}

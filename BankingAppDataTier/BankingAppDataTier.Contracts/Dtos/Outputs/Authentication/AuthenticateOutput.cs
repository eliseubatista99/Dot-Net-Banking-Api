using ElideusDotNetFramework.Core.Operations;
using System.Diagnostics.CodeAnalysis;

namespace BankingAppDataTier.Contracts.Dtos.Outputs.Authentication
{
    [ExcludeFromCodeCoverage]

    public class AuthenticateOutput : OperationOutput
    {
        public required string Token { get; set; }

        public DateTime? ExpirationDateTime { get; set; }
    }
}

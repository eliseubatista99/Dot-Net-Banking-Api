using ElideusDotNetFramework.Core.Operations;
using System.Diagnostics.CodeAnalysis;

namespace BankingAppAuthenticationTier.Contracts.Operations
{
    [ExcludeFromCodeCoverage]

    public class RefreshTokenInput : OperationInput
    {
        public required string RefreshToken { get; set; }

    }
}

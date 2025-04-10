using ElideusDotNetFramework.Core.Operations;
using System.Diagnostics.CodeAnalysis;

namespace BankingAppAuthenticationTier.Contracts.Operations
{
    [ExcludeFromCodeCoverage]

    public class IsValidTokenInput : OperationInput
    {
        public required string Token { get; set; }

    }
}

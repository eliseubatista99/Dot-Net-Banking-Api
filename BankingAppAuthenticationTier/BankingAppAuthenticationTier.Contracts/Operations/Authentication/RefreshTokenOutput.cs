using ElideusDotNetFramework.Core;
using ElideusDotNetFramework.Core.Operations;
using System.Diagnostics.CodeAnalysis;

namespace BankingAppAuthenticationTier.Contracts.Operations
{
    [ExcludeFromCodeCoverage]

    public class RefreshTokenOutput : OperationOutput
    {
        public TokenData? Token { get; set; }

        public TokenData? RefreshToken { get; set; }

    }
}

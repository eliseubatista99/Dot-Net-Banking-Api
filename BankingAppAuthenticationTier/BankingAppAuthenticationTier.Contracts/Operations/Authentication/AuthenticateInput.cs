using BankingAppAuthenticationTier.Contracts.Dtos;
using ElideusDotNetFramework.Core.Operations;
using System.Diagnostics.CodeAnalysis;

namespace BankingAppAuthenticationTier.Contracts.Operations
{
    [ExcludeFromCodeCoverage]

    public class AuthenticateInput : OperationInput
    {
        public required string ClientId { get; set; }

        public required List<AuthenticationCodeItemDto> AuthenticationCode { get; set; }

    }
}

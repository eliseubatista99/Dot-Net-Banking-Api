using BankingAppDataTier.Contracts.Dtos.Entitites;
using ElideusDotNetFramework.Core.Operations;
using System.Diagnostics.CodeAnalysis;

namespace BankingAppDataTier.Contracts.Dtos.Inputs.Authentication
{
    [ExcludeFromCodeCoverage]

    public class AuthenticateInput : OperationInput
    {
        public required string ClientId { get; set; }

        public required List<AuthenticationCodeItemDto> AuthenticationCode { get; set; }

    }
}

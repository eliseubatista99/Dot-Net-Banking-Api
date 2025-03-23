using BankingAppDataTier.Contracts.Dtos.Entitites;
using ElideusDotNetFramework.Operations.Contracts;

namespace BankingAppDataTier.Contracts.Dtos.Inputs.Authentication
{
    public class AuthenticateInput : OperationInput
    {
        public required string ClientId { get; set; }

        public required List<AuthenticationCodeItemDto> AuthenticationCode { get; set; }

    }
}

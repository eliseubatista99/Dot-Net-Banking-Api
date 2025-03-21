using BankingAppDataTier.Contracts.Dtos.Entitites;

namespace BankingAppDataTier.Contracts.Dtos.Inputs.Authentication
{
    public class AuthenticateInput
    {
        public required string ClientId { get; set; }

        public required List<AuthenticationCodeItemDto> AuthenticationCode { get; set; }

    }
}

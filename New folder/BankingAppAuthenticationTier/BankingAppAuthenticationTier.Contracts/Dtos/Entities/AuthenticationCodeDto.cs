namespace BankingAppAuthenticationTier.Contracts.Dtos.Entities
{
    public class AuthenticationCodeDto
    {
        public required List<AuthenticationCodeItemDto> Code { get; set; }
    }
}

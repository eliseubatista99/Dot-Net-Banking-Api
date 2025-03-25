namespace BankingAppDataTier.Contracts.Dtos.Entitites
{
    public class AuthenticationCodeDto
    {
        public required List<AuthenticationCodeItemDto> Code { get; set; }
    }
}

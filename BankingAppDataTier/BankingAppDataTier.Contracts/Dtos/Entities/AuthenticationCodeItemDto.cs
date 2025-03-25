namespace BankingAppDataTier.Contracts.Dtos.Entitites
{
    public class AuthenticationCodeItemDto
    {
        public required int Position { get; set; }

        public required char Value { get; set; }
    }
}

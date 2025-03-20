namespace BankingAppAuthenticationTier.Contracts.Dtos.Entities
{
    public class BankingAppAuthenticationTierError
    {
        public required string Code { get; set; }

        public required string Message { get; set; }
    }
}

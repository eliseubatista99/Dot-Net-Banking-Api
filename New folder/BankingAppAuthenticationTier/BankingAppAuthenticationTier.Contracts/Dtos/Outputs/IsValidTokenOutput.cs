namespace BankingAppAuthenticationTier.Contracts.Dtos.Outputs
{
    public class IsValidTokenOutput : BaseOutput
    {
        public required bool IsValid { get; set; }
        public DateTime? ExpirationDateTime { get; set; }
    }
}

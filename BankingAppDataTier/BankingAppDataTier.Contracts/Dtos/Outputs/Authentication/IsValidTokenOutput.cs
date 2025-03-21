namespace BankingAppDataTier.Contracts.Dtos.Outputs.Authentication
{
    public class IsValidTokenOutput : BaseOutput
    {
        public required bool IsValid { get; set; }

        public string? Reason { get; set; }

        public DateTime? ExpirationDateTime { get; set; }
    }
}

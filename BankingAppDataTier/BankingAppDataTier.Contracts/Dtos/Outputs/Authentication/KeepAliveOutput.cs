namespace BankingAppDataTier.Contracts.Dtos.Outputs.Authentication
{
    public class KeepAliveOutput : BaseOutput
    {
        public DateTime? ExpirationDateTime { get; set; }
    }
}

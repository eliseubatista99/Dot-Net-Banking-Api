namespace BankingAppAuthenticationTier.Contracts.Dtos.Outputs
{
    public class KeepAliveOutput : BaseOutput
    {
        public DateTime? ExpirationDateTime { get; set; }
    }
}

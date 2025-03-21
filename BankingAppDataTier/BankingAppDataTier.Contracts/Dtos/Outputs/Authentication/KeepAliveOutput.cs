namespace BankingAppDataTier.Contracts.Dtos.Outputs.Authentication
{
    public class KeepAliveOutput : _BaseOutput
    {
        public DateTime? ExpirationDateTime { get; set; }
    }
}

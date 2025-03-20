namespace BankingAppDataTier.Contracts.Dtos.Outputs.Authentication
{
    public class AuthenticateOutput : BaseOutput
    {
        public required string Token { get; set; }

        public DateTime? ExpirationDateTime { get; set; }
    }
}

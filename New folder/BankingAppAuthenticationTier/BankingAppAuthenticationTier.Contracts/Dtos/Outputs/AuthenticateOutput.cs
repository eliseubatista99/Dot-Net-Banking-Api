namespace BankingAppAuthenticationTier.Contracts.Dtos.Outputs
{
    public class AuthenticateOutput : BaseOutput
    {
        public required string Token { get; set; }

        public DateTime? ExpirationDateTime { get; set; }
    }
}

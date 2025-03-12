namespace BankingAppDataTier.Contracts.Dtos.Outputs.Authentication
{
    public class GenerateTokenOutput : BaseOutput
    {
        public required string Token { get; set; }
    }
}

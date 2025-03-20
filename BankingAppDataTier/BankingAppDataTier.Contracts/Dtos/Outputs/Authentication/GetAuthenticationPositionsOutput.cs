namespace BankingAppDataTier.Contracts.Dtos.Outputs.Authentication
{
    public class GetAuthenticationPositionsOutput : BaseOutput
    {
        public required List<int> Positions { get; set; }
    }
}

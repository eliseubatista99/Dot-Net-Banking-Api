namespace BankingAppDataTier.Contracts.Dtos.Outputs.Authentication
{
    public class GetAuthenticationPositionsOutput : _BaseOutput
    {
        public required List<int> Positions { get; set; }
    }
}

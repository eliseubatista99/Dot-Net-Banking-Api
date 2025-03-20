namespace BankingAppAuthenticationTier.Contracts.Dtos.Outputs
{
    public class GetAuthenticationPositionsOutput : BaseOutput
    {
        public required List<int> Positions { get; set; }
    }
}

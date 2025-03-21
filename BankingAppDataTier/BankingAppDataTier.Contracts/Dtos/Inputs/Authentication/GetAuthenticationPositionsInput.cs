namespace BankingAppDataTier.Contracts.Dtos.Inputs.Authentication
{
    public class GetAuthenticationPositionsInput
    {
        public required string ClientId { get; set; }

        public int? NumberOfPositions { get; set; }

    }
}

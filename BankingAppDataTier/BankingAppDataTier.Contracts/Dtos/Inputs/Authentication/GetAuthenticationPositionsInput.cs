namespace BankingAppDataTier.Contracts.Dtos.Inputs.Authentication
{
    public class GetAuthenticationPositionsInput : _BaseInput
    {
        public required string ClientId { get; set; }

        public int? NumberOfPositions { get; set; }

    }
}

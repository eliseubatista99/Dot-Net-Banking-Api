namespace BankingAppDataTier.Contracts.Dtos.Inputs.Authentication
{
    public class IsValidTokenInput : _BaseInput
    {
        public required string Token { get; set; }

    }
}

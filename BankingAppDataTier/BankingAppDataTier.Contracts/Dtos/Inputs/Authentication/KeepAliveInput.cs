namespace BankingAppDataTier.Contracts.Dtos.Inputs.Authentication
{
    public class KeepAliveInput : _BaseInput
    {
        public required string Token { get; set; }

    }
}

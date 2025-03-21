namespace BankingAppDataTier.Contracts.Dtos.Inputs.Accounts
{
    public class GetClientAccountsInput : _BaseInput
    {
        /// <summary>
        /// Gets or sets the client id.
        /// </summary>
        public required string ClientId { get; set; }
    }
}

namespace BankingAppDataTier.Contracts.Dtos.Inputs.Accounts
{
    public class GetAccountByIdInput : _BaseInput
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public required string Id { get; set; }
    }
}

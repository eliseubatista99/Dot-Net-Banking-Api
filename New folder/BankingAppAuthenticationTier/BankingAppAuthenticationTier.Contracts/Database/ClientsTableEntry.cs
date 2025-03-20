namespace BankingAppAuthenticationTier.Contracts.Database
{
    public class ClientsTableEntry
    {
        /// <summary>
        /// Gets or sets the client id.
        /// </summary>
        public required string Id { get; set; }

        /// <summary>
        /// Gets or sets the client password.
        /// </summary>
        public required string Password { get; set; }
    }
}

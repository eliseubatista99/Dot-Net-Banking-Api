using System.Diagnostics.CodeAnalysis;

namespace BankingAppAuthenticationTier.Contracts.Database
{
    [ExcludeFromCodeCoverage]

    public class TokenTableEntry
    {
        /// <summary>
        /// Gets or sets the token.
        /// </summary>
        public required string Token { get; set; }

        /// <summary>
        /// Gets or sets the client id.
        /// </summary>
        public required string ClientId { get; set; }

        /// <summary>
        /// Gets or sets the expiration date.
        /// </summary>
        public required DateTime ExpirationDate { get; set; }
    }
}

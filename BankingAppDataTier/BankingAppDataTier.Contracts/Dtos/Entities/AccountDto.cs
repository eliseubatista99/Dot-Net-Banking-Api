using BankingAppDataTier.Contracts.Enums;

namespace BankingAppDataTier.Contracts.Dtos.Entitites
{
    public class AccountDto
    {
        /// <summary>
        /// Gets or sets the account id.
        /// </summary>
        public required string Id { get; set; }

        /// <summary>
        /// Gets or sets the account id.
        /// </summary>
        public required string OwnerCliendId { get; set; }

        /// <summary>
        /// Gets or sets the account type.
        /// </summary>
        public required AccountType AccountType { get; set; }

        /// <summary>
        /// Gets or sets the balance.
        /// </summary>
        public required decimal Balance { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// Gets or sets the image in base64.
        /// </summary>
        public string? Image { get; set; }

        /// <summary>
        /// Gets or sets the source account id.
        /// </summary>
        public string? SourceAccountId { get; set; }

        /// <summary>
        /// Gets or sets the investments duration in months.
        /// </summary>
        public int? Duration { get; set; }

        /// <summary>
        /// Gets or sets the investments interest.
        /// </summary>
        public decimal? Interest { get; set; }
    }
}


using BankingAppDataTier.Contracts.Enums;

namespace BankingAppDataTier.Contracts.Dtos.Entitites
{
    public class TransactionDto
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public required string Id { get; set; }

        /// <summary>
        /// Gets or sets the Transaction Date.
        /// </summary>
        public required DateTime TransactionDate { get; set; }

        /// <summary>
        /// Gets or sets the Transaction Role.
        /// </summary>
        public required TransactionRole Role { get; set; }

        /// <summary>
        /// Gets or sets the Description.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the Amount.
        /// </summary>
        public required decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets the Amount.
        /// </summary>
        public decimal? Fees { get; set; }

        /// <summary>
        /// Gets or sets the Amount.
        /// </summary>
        public required bool Urgent { get; set; }

        /// <summary>
        /// Gets or sets the Source Account.
        /// </summary>
        public string? SourceAccount { get; set; }

        /// <summary>
        /// Gets or sets the Destination Account.
        /// </summary>
        public string? DestinationAccount { get; set; }

        /// <summary>
        /// Gets or sets the Destination Name.
        /// </summary>
        public required string DestinationName { get; set; }

        /// <summary>
        /// Gets or sets the Source Card.
        /// </summary>
        public string? SourceCard { get; set; }
    }
}

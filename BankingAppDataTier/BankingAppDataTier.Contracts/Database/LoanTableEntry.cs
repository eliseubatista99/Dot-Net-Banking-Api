namespace BankingAppDataTier.Contracts.Database
{
    public class LoanTableEntry
    {
        /// <summary>
        /// Gets or sets the loan id.
        /// </summary>
        public required string Id { get; set; }

        /// <summary>
        /// Gets or sets the loan name.
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        public required DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the related account.
        /// </summary>
        public required string RelatedAccount { get; set; }

        /// <summary>
        /// Gets or sets the related offer.
        /// </summary>
        public required string RelatedOffer { get; set; }

        /// <summary>
        /// Gets or sets the duration.
        /// </summary>
        public required int Duration { get; set; }

        /// <summary>
        /// Gets or sets the contracted amount.
        /// </summary>
        public required decimal ContractedAmount { get; set; }

        /// <summary>
        /// Gets or sets the paid amount.
        /// </summary>
        public required decimal PaidAmount { get; set; }
    }
}

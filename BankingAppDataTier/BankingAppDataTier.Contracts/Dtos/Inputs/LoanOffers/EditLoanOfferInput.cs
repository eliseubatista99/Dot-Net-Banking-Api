namespace BankingAppDataTier.Contracts.Dtos.Inputs.LoanOffer
{
    public class EditLoanOfferInput
    {
        /// <summary>
        /// Gets or sets the loan offer id.
        /// </summary>
        public required string Id { get; set; }

        /// <summary>
        /// Gets or sets the max effort.
        /// </summary>
        public int? MaxEffort { get; set; }

        /// <summary>
        /// Gets or sets the Interest.
        /// </summary>
        public decimal? Interest { get; set; }
    }
}

namespace BankingAppDataTier.Contracts.Dtos.Inputs.LoanOffer
{
    public class EditLoanOfferInput : _BaseInput
    {
        /// <summary>
        /// Gets or sets the loan offer id.
        /// </summary>
        public required string Id { get; set; }

        /// <summary>
        /// Gets or sets the loan offer name.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the loan offer description.
        /// </summary>
        public string? Description { get; set; }

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

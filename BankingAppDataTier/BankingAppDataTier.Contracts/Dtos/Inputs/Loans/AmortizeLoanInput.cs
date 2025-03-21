namespace BankingAppDataTier.Contracts.Dtos.Inputs.Loans
{
    public class AmortizeLoanInput : _BaseInput
    {
        /// <summary>
        /// Gets or sets the loan id.
        /// </summary>
        public required string Id { get; set; }

        /// <summary>
        /// Gets or sets the amount.
        /// </summary>
        public required decimal Amount { get; set; }
    }
}

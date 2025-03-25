using ElideusDotNetFramework.Operations.Contracts;

namespace BankingAppDataTier.Contracts.Dtos.Inputs.LoanOffer
{
    public class DeleteLoanOfferInput : OperationInput
    {
        /// <summary>
        /// Gets or sets the loan offer id.
        /// </summary>
        public required string Id { get; set; }
    }
}

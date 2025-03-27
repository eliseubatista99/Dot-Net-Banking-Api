using ElideusDotNetFramework.Core.Operations;

namespace BankingAppDataTier.Contracts.Dtos.Inputs.LoanOffer
{
    public class GetLoanOfferByIdInput : OperationInput
    {
        /// <summary>
        /// Gets or sets the loan offer id.
        /// </summary>
        public required string Id { get; set; }
    }
}

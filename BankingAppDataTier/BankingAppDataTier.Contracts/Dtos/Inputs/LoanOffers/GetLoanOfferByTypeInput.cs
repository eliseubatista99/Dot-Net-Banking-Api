using BankingAppDataTier.Contracts.Enums;
using ElideusDotNetFramework.Core.Operations;

namespace BankingAppDataTier.Contracts.Dtos.Inputs.LoanOffer
{
    public class GetLoanOfferByTypeInput : OperationInput
    {
        /// <summary>
        /// Gets or sets the loan offer id.
        /// </summary>
        public required LoanType OfferType { get; set; }

        /// <summary>
        /// Gets or sets the value stating wheter or not to include inactive offers.
        /// </summary>
        public bool? IncludeInactive { get; set; }
    }
}

using ElideusDotNetFramework.Core.Operations;

namespace BankingAppDataTier.Contracts.Dtos.Inputs.LoanOffer
{
    public class ActivateOrDeactivateLoanOfferInput : OperationInput
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public required string Id { get; set; }

        /// <summary>
        /// Gets or sets the active state.
        /// </summary>
        public required bool Active { get; set; }
    }
}

using ElideusDotNetFramework.Core.Operations;
using System.Diagnostics.CodeAnalysis;

namespace BankingAppDataTier.Contracts.Dtos.Inputs.LoanOffer
{
    [ExcludeFromCodeCoverage]

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

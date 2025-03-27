using ElideusDotNetFramework.Core.Operations;
using System.Diagnostics.CodeAnalysis;

namespace BankingAppDataTier.Contracts.Dtos.Inputs.LoanOffer
{
    [ExcludeFromCodeCoverage]

    public class GetLoanOfferByIdInput : OperationInput
    {
        /// <summary>
        /// Gets or sets the loan offer id.
        /// </summary>
        public required string Id { get; set; }
    }
}

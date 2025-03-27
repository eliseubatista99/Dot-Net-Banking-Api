using ElideusDotNetFramework.Core.Operations;
using System.Diagnostics.CodeAnalysis;

namespace BankingAppDataTier.Contracts.Operations.Inputs.Loans
{
    [ExcludeFromCodeCoverage]

    public class EditLoanInput : OperationInput
    {
        /// <summary>
        /// Gets or sets the loan id.
        /// </summary>
        public required string Id { get; set; }

        /// <summary>
        /// Gets or sets the loan name.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the related account.
        /// </summary>
        public string? RelatedAccount { get; set; }

        /// <summary>
        /// Gets or sets the related offer.
        /// </summary>
        public string? RelatedOffer { get; set; }

        /// <summary>
        /// Gets or sets the duration.
        /// </summary>
        public int? Duration { get; set; }

        /// <summary>
        /// Gets or sets the paid amount.
        /// </summary>
        public decimal? PaidAmount { get; set; }
    }
}

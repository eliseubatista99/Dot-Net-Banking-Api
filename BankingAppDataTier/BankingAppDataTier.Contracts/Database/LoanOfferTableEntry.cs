using System.Diagnostics.CodeAnalysis;

namespace BankingAppDataTier.Contracts.Database
{
    [ExcludeFromCodeCoverage]

    public class LoanOfferTableEntry
    {
        /// <summary>
        /// Gets or sets the loan offer id.
        /// </summary>
        public required string Id { get; set; }

        /// <summary>
        /// Gets or sets the loan offer name.
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// Gets or sets the loan offer description.
        /// </summary>
        public required string Description { get; set; }

        /// <summary>
        /// Gets or sets the loan type.
        /// </summary>
        public required string LoanType { get; set; }

        /// <summary>
        /// Gets or sets the max effort.
        /// </summary>
        public required int MaxEffort { get; set; }

        /// <summary>
        /// Gets or sets the Interest.
        /// </summary>
        public required decimal Interest { get; set; }

        /// <summary>
        /// Gets or sets the is active flag.
        /// </summary>
        public required bool IsActive { get; set; }
    }
}

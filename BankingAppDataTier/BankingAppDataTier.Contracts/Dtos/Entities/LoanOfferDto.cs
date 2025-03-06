using BankingAppDataTier.Contracts.Enums;

namespace BankingAppDataTier.Contracts.Dtos.Entitites
{
    public class LoanOfferDto
    {
        /// <summary>
        /// Gets or sets the loan offer id.
        /// </summary>
        public required string Id { get; set; }

        /// <summary>
        /// Gets or sets the loan type.
        /// </summary>
        public required LoanType LoanType { get; set; }

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
        public bool? IsActive { get; set; }
    }
}

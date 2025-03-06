using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingAppDataTier.Contracts.Database
{
    public class LoanOfferTableEntry
    {
        /// <summary>
        /// Gets or sets the loan offer id.
        /// </summary>
        public required string Id { get; set; }

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

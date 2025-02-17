using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingAppDataTier.Contracts.Database
{
    public class InvestmentsAccountBridgeTableEntry
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public required string Id { get; set; }

        /// <summary>
        /// Gets or sets the source account id.
        /// </summary>
        public required string SourceAccountId { get; set; }

        /// <summary>
        /// Gets or sets the investments account id.
        /// </summary>
        public required string InvestmentsAccountId { get; set; }

        /// <summary>
        /// Gets or sets the investments duration in months.
        /// </summary>
        public required int Duration { get; set; }

        /// <summary>
        /// Gets or sets the investments interest.
        /// </summary>
        public required decimal Interest { get; set; }
    }
}

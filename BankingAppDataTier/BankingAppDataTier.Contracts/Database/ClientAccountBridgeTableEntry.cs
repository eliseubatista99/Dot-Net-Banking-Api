using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingAppDataTier.Contracts.Database
{
    public class ClientAccountBridgeTableEntry
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>The id.</value>
        public required string Id { get; set; }

        /// <summary>
        /// Gets or sets the client id.
        /// </summary>
        /// <value>The client id.</value>
        public required string ClientId { get; set; }

        /// <summary>
        /// Gets or sets the client password.
        /// </summary>
        /// <value>The client password.</value>
        public required string AccountId { get; set; }
    }
}

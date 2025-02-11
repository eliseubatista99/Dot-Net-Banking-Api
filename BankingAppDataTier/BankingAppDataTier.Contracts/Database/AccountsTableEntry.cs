using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingAppDataTier.Contracts.Database
{
    public class AccountsTableEntry
    {
        /// <summary>
        /// Gets or sets the account id.
        /// </summary>
        public required string AccountId { get; set; }

        /// <summary>
        /// Gets or sets the account type.
        /// </summary>
        public required string AccountType { get; set; }

        /// <summary>
        /// Gets or sets the account balance.
        /// </summary>
        public required decimal Balance { get; set; }

        /// <summary>
        /// Gets or sets the account name.
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// Gets or sets the account image.
        /// </summary>
        public string? Image { get; set; }
    }
}

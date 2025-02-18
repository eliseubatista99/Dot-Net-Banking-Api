using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingAppDataTier.Contracts.Enums
{
    public enum AccountType
    {
        /// <summary>
        /// Gets or sets the None.
        /// </summary>
        None,

        /// <summary>
        /// Gets or sets the Current.
        /// </summary>
        Current,

        /// <summary>
        /// Gets or sets the Savings.
        /// </summary>
        Savings,

        /// <summary>
        /// Gets or sets the Investments.
        /// </summary>
        Investments,
    }
}

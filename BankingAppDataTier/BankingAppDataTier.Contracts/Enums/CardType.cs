using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingAppDataTier.Contracts.Enums
{
    public enum CardType
    {
        /// <summary>
        /// Gets or sets the None.
        /// </summary>
        None,

        /// <summary>
        /// Gets or sets the Debit.
        /// </summary>
        Debit,

        /// <summary>
        /// Gets or sets the Credit.
        /// </summary>
        Credit,

        /// <summary>
        /// Gets or sets the PrePaid.
        /// </summary>
        PrePaid,

        /// <summary>
        /// Gets or sets the Meal.
        /// </summary>
        Meal,
    }
}

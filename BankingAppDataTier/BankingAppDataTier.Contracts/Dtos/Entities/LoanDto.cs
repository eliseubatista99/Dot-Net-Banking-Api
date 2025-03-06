using BankingAppDataTier.Contracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingAppDataTier.Contracts.Dtos.Entitites
{
    public class LoanDto
    {
        /// <summary>
        /// Gets or sets the loan id.
        /// </summary>
        public required string Id { get; set; }

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        public required DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the related account.
        /// </summary>
        public required string RelatedAccount { get; set; }

        /// <summary>
        /// Gets or sets the related offer.
        /// </summary>
        public required string RelatedOffer { get; set; }

        /// <summary>
        /// Gets or sets the duration.
        /// </summary>
        public required int Duration { get; set; }

        /// <summary>
        /// Gets or sets the amount.
        /// </summary>
        public required decimal Amount { get; set; }
    }
}

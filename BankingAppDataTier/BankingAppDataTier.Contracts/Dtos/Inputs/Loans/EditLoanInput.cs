﻿using BankingAppDataTier.Contracts.Dtos.Entitites;
using BankingAppDataTier.Contracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingAppDataTier.Contracts.Dtos.Inputs.Plastics
{
    public class EditLoanInput
    {
        /// <summary>
        /// Gets or sets the loan id.
        /// </summary>
        public required string Id { get; set; }

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        public DateTime? StartDate { get; set; }

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
        /// Gets or sets the amount.
        /// </summary>
        public decimal? Amount { get; set; }
    }
}

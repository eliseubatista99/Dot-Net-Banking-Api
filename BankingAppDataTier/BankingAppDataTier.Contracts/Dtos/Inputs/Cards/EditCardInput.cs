using BankingAppDataTier.Contracts.Dtos.Entitites;
using BankingAppDataTier.Contracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingAppDataTier.Contracts.Dtos.Inputs.Cards
{
    public class EditCardInput
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public required string Id { get; set; }

        /// <summary>
        /// Gets or sets the account balance.
        /// </summary>
        public decimal? Balance { get; set; }

        /// <summary>
        /// Gets or sets the payment day.
        /// </summary>
        public int? PaymentDay { get; set; }

        /// <summary>
        /// Gets or sets the request date.
        /// </summary>
        public DateTime? RequestDate { get; set; }

        /// <summary>
        /// Gets or sets the activation date.
        /// </summary>
        public DateTime? ActivationDate { get; set; }

        /// <summary>
        /// Gets or sets the expiration date.
        /// </summary>
        public DateTime? ExpirationDate { get; set; }
    }
}

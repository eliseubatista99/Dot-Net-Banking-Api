using BankingAppDataTier.Contracts.Dtos.Entitites;
using BankingAppDataTier.Contracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingAppDataTier.Contracts.Dtos.Inputs
{
    public class EditAccountInput
    {
        /// <summary>
        /// Gets or sets the account id.
        /// </summary>
        public required string AccountId { get; set; }

        /// <summary>
        /// Gets or sets the account type.
        /// </summary>
        public AccountType? AccountType { get; set; }

        /// <summary>
        /// Gets or sets the balance.
        /// </summary>
        public decimal? Balance { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the image in base64.
        /// </summary>
        public string? Image { get; set; }

        /// <summary>
        /// Gets or sets the source account id.
        /// </summary>
        public string? SourceAccountId { get; set; }

        /// <summary>
        /// Gets or sets the investments duration in months.
        /// </summary>
        public int? Duration { get; set; }

        /// <summary>
        /// Gets or sets the investments interest.
        /// </summary>
        public decimal? Interest { get; set; }
    }
}

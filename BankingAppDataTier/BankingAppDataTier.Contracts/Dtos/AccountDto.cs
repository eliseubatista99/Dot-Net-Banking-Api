using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingAppDataTier.Contracts.Dtos
{
    public class AccountDto
    {
        /// <summary>
        /// Gets or sets the account id.
        /// </summary>
        public required string Id { get; set; }

        /// <summary>
        /// Gets or sets the account type.
        /// </summary>
        public required AccountType AccountType { get; set; }

        /// <summary>
        /// Gets or sets the balance.
        /// </summary>
        public required decimal Balance { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// Gets or sets the image in base64.
        /// </summary>
        public string? Image { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingAppDataTier.Contracts.Database
{
    public class ClientsTableEntry
    {
        /// <summary>
        /// Gets or sets the client id.
        /// </summary>
        /// <value>The client id.</value>
        public required string Id { get; set; }

        /// <summary>
        /// Gets or sets the client password.
        /// </summary>
        /// <value>The client password.</value>
        public required string Password { get; set; }

        /// <summary>
        /// Gets or sets the client name.
        /// </summary>
        /// <value>The client name.</value>
        public required string Name { get; set; }

        /// <summary>
        /// Gets or sets the client surname.
        /// </summary>
        /// <value>The client surname.</value>
        public required string Surname { get; set; }

        /// <summary>
        /// Gets or sets the client birth date.
        /// </summary>
        /// <value>The client birth date.</value>
        public required DateTime BirthDate { get; set; }

        /// <summary>
        /// Gets or sets the client vat number.
        /// </summary>
        /// <value>The client vat number.</value>
        public required string VATNumber { get; set; }

        /// <summary>
        /// Gets or sets the client phone number.
        /// </summary>
        /// <value>The client phone number.</value>
        public required string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the client email.
        /// </summary>
        /// <value>The client email.</value>
        public required string Email { get; set; }
    }
}

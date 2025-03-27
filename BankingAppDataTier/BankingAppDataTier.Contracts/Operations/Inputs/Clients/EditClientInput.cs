using ElideusDotNetFramework.Core.Operations;
using System.Diagnostics.CodeAnalysis;

namespace BankingAppDataTier.Contracts.Operations.Inputs.Clients
{
    [ExcludeFromCodeCoverage]

    public class EditClientInput : OperationInput
    {
        /// <summary>
        /// Gets or sets the client id.
        /// </summary>
        /// <value>The client id.</value>
        public required string Id { get; set; }

        /// <summary>
        /// Gets or sets the client name.
        /// </summary>
        /// <value>The client name.</value>
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets the client surname.
        /// </summary>
        /// <value>The client surname.</value>
        public string? Surname { get; set; }

        /// <summary>
        /// Gets or sets the client birth date.
        /// </summary>
        /// <value>The client birth date.</value>
        public DateTime? BirthDate { get; set; }

        /// <summary>
        /// Gets or sets the client vat number.
        /// </summary>
        /// <value>The client vat number.</value>
        public string? VATNumber { get; set; }

        /// <summary>
        /// Gets or sets the client phone number.
        /// </summary>
        /// <value>The client phone number.</value>
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the client email.
        /// </summary>
        /// <value>The client email.</value>
        public string? Email { get; set; }
    }
}

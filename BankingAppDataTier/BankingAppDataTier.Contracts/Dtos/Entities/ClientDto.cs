namespace BankingAppDataTier.Contracts.Dtos.Entitites
{
    public class ClientDto
    {
        /// <summary>
        /// Gets or sets the client id.
        /// </summary>
        public required string Id { get; set; }

        /// <summary>
        /// Gets or sets the client name.
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// Gets or sets the client surname.
        /// </summary>
        public required string Surname { get; set; }

        /// <summary>
        /// Gets or sets the client birth date.
        /// </summary>
        public required DateTime BirthDate { get; set; }

        /// <summary>
        /// Gets or sets the client vat number.
        /// </summary>
        public required string VATNumber { get; set; }

        /// <summary>
        /// Gets or sets the client phone number.
        /// </summary>
        public required string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the client email.
        /// </summary>
        public required string Email { get; set; }  

    }
}

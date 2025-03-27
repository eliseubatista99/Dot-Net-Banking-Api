using BankingAppDataTier.Contracts.Enums;
using System.Diagnostics.CodeAnalysis;

namespace BankingAppDataTier.Contracts.Dtos.Entitites
{
    [ExcludeFromCodeCoverage]

    public class CardDto
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public required string Id { get; set; }

        /// <summary>
        /// Gets or sets the card type.
        /// </summary>
        public required CardType CardType { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// Gets or sets the image in base64.
        /// </summary>
        public required string Image { get; set; }

        /// <summary>
        /// Gets or sets the related account id.
        /// </summary>
        public required string RelatedAccountID { get; set; }
        /// <summary>
        /// Gets or sets the plastic id.
        /// </summary>
        public required string PlasticId { get; set; }

        /// <summary>
        /// Gets or sets the request date.
        /// </summary>
        public required DateTime RequestDate { get; set; }


        /// <summary>
        /// Gets or sets the expiration date.
        /// </summary>
        public required DateTime ExpirationDate { get; set; }

        /// <summary>
        /// Gets or sets the activation date.
        /// </summary>
        public DateTime? ActivationDate { get; set; }

        /// <summary>
        /// Gets or sets the account balance.
        /// </summary>
        public decimal? Balance { get; set; }

        /// <summary>
        /// Gets or sets the payment day.
        /// </summary>
        public int? PaymentDay { get; set; }
    }
}

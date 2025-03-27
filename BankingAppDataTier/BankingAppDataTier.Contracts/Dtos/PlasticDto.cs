using BankingAppDataTier.Contracts.Enums;
using System.Diagnostics.CodeAnalysis;

namespace BankingAppDataTier.Contracts.Dtos
{
    [ExcludeFromCodeCoverage]

    public class PlasticDto
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
        /// Gets or sets the cashback.
        /// </summary>
        public decimal? Cashback { get; set; }

        /// <summary>
        /// Gets or sets the commission.
        /// </summary>
        public decimal? Commission { get; set; }

        /// <summary>
        /// Gets or sets the image in base64.
        /// </summary>
        public required string Image { get; set; }

        /// <summary>
        /// Gets or sets the is active flag.
        /// </summary>
        public bool? IsActive { get; set; }
    }
}

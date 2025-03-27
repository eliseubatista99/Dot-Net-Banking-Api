using ElideusDotNetFramework.Core.Operations;

namespace BankingAppDataTier.Contracts.Dtos.Inputs.Plastics
{
    public class EditPlasticInput : OperationInput
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public required string Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string? Name { get; set; }

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
        public string? Image { get; set; }
    }
}

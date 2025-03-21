namespace BankingAppDataTier.Contracts.Dtos.Inputs.Plastics
{
    public class ActivateOrDeactivatePlasticInput : _BaseInput
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public required string Id { get; set; }

        /// <summary>
        /// Gets or sets the active state.
        /// </summary>
        public required bool Active { get; set; }
    }
}

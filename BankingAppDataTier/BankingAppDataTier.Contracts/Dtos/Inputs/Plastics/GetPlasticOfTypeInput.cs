using BankingAppDataTier.Contracts.Enums;
using ElideusDotNetFramework.Core.Operations;

namespace BankingAppDataTier.Contracts.Dtos.Inputs.Plastics
{
    public class GetPlasticOfTypeInput : OperationInput
    {
        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        public required CardType PlasticType { get; set; }

        /// <summary>
        /// Gets or sets the value stating wheter or not to include inactive offers.
        /// </summary>
        public bool? IncludeInactive { get; set; }
    }
}

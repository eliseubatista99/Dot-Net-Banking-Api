using BankingAppDataTier.Contracts.Dtos.Entitites;

namespace BankingAppDataTier.Contracts.Dtos.Inputs.Plastics
{
    public class AddPlasticInput : _BaseInput
    {
        public required PlasticDto Plastic { get; set; }
    }
}

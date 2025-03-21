using BankingAppDataTier.Contracts.Dtos.Entitites;

namespace BankingAppDataTier.Contracts.Dtos.Outputs.Plastics
{
    public class GetPlasticByIdOutput : _BaseOutput
    {
        public PlasticDto? Plastic { get; set; }
    }
}

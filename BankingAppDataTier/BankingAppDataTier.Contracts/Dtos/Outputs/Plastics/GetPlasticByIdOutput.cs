using BankingAppDataTier.Contracts.Dtos.Entitites;

namespace BankingAppDataTier.Contracts.Dtos.Outputs.Plastics
{
    public class GetPlasticByIdOutput : BaseOutput
    {
        public PlasticDto? Plastic { get; set; }
    }
}

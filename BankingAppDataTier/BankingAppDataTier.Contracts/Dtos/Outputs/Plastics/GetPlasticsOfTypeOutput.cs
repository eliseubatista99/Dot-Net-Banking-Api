using BankingAppDataTier.Contracts.Dtos.Entitites;

namespace BankingAppDataTier.Contracts.Dtos.Outputs.Plastics
{
    public class GetPlasticsOfTypeOutput : _BaseOutput
    {
        public required List<PlasticDto> Plastics { get; set; }
    }
}

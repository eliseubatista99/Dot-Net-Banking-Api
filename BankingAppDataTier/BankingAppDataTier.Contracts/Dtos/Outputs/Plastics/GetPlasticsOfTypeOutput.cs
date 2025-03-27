using BankingAppDataTier.Contracts.Dtos.Entitites;
using ElideusDotNetFramework.Core.Operations;

namespace BankingAppDataTier.Contracts.Dtos.Outputs.Plastics
{
    public class GetPlasticsOfTypeOutput : OperationOutput
    {
        public required List<PlasticDto> Plastics { get; set; }
    }
}

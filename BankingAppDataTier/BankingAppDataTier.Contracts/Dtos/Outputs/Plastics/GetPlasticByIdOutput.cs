using BankingAppDataTier.Contracts.Dtos.Entitites;
using ElideusDotNetFramework.Operations;

namespace BankingAppDataTier.Contracts.Dtos.Outputs.Plastics
{
    public class GetPlasticByIdOutput : OperationOutput
    {
        public PlasticDto? Plastic { get; set; }
    }
}

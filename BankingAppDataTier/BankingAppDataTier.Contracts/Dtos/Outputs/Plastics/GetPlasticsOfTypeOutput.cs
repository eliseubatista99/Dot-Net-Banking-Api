using BankingAppDataTier.Contracts.Dtos.Entitites;
using ElideusDotNetFramework.Core.Operations;
using System.Diagnostics.CodeAnalysis;

namespace BankingAppDataTier.Contracts.Dtos.Outputs.Plastics
{
    [ExcludeFromCodeCoverage]

    public class GetPlasticsOfTypeOutput : OperationOutput
    {
        public required List<PlasticDto> Plastics { get; set; }
    }
}

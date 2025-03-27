using BankingAppDataTier.Contracts.Dtos;
using ElideusDotNetFramework.Core.Operations;
using System.Diagnostics.CodeAnalysis;

namespace BankingAppDataTier.Contracts.Operations.Plastics
{
    [ExcludeFromCodeCoverage]

    public class GetPlasticByIdOutput : OperationOutput
    {
        public PlasticDto? Plastic { get; set; }
    }
}

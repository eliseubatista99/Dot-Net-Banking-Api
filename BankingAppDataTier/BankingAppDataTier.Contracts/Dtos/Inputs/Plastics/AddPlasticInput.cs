using BankingAppDataTier.Contracts.Dtos.Entitites;
using ElideusDotNetFramework.Core.Operations;
using System.Diagnostics.CodeAnalysis;

namespace BankingAppDataTier.Contracts.Dtos.Inputs.Plastics
{
    [ExcludeFromCodeCoverage]

    public class AddPlasticInput : OperationInput
    {
        public required PlasticDto Plastic { get; set; }
    }
}

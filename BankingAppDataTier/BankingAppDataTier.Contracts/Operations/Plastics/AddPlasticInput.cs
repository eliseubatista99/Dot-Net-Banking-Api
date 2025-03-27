using BankingAppDataTier.Contracts.Dtos;
using ElideusDotNetFramework.Core.Operations;
using System.Diagnostics.CodeAnalysis;

namespace BankingAppDataTier.Contracts.Operations.Plastics
{
    [ExcludeFromCodeCoverage]

    public class AddPlasticInput : OperationInput
    {
        public required PlasticDto Plastic { get; set; }
    }
}

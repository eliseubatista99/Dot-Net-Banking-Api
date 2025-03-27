using BankingAppBusinessTier.Contracts.Dtos;
using ElideusDotNetFramework.Core.Operations;
using System.Diagnostics.CodeAnalysis;

namespace BankingAppBusinessTier.Contracts.Operations.Cards
{
    [ExcludeFromCodeCoverage]

    public class GetEligiblePlasticsOutput : OperationOutput
    {
        public required List<PlasticDto> Plastics { get; set; }
    }
}

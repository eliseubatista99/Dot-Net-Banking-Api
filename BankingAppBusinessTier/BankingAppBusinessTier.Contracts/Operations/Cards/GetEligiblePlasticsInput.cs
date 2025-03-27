using BankingAppBusinessTier.Contracts.Enums;
using ElideusDotNetFramework.Core.Operations;
using System.Diagnostics.CodeAnalysis;

namespace BankingAppBusinessTier.Contracts.Operations.Cards
{
    [ExcludeFromCodeCoverage]

    public class GetEligiblePlasticsInput : OperationInput
    {
        public required string ClientId { get; set; }

        public required CardType PlasticType { get; set; }

    }
}

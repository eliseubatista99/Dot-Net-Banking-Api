using BankingAppDataTier.Contracts.Dtos.Entitites;
using ElideusDotNetFramework.Core.Operations;
using System.Diagnostics.CodeAnalysis;

namespace BankingAppDataTier.Contracts.Dtos.Outputs.Cards
{
    [ExcludeFromCodeCoverage]

    public class GetCardByIdOutput : OperationOutput
    {
        public CardDto? Card { get; set; }
    }
}

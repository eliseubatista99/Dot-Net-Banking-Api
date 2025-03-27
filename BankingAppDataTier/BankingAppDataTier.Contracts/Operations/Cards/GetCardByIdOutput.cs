using BankingAppDataTier.Contracts.Dtos;
using ElideusDotNetFramework.Core.Operations;
using System.Diagnostics.CodeAnalysis;

namespace BankingAppDataTier.Contracts.Operations.Cards
{
    [ExcludeFromCodeCoverage]

    public class GetCardByIdOutput : OperationOutput
    {
        public CardDto? Card { get; set; }
    }
}

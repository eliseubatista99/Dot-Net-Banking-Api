using BankingAppDataTier.Contracts.Dtos.Entitites;
using ElideusDotNetFramework.Core.Operations;
using System.Diagnostics.CodeAnalysis;

namespace BankingAppDataTier.Contracts.Dtos.Outputs.Cards
{
    [ExcludeFromCodeCoverage]

    public class GetCardsOfAccountOutput : OperationOutput
    {
        public List<CardDto> Cards { get; set; }
    }
}

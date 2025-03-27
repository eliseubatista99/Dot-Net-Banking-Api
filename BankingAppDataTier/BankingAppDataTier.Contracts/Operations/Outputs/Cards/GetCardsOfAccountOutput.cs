using BankingAppDataTier.Contracts.Dtos;
using ElideusDotNetFramework.Core.Operations;
using System.Diagnostics.CodeAnalysis;

namespace BankingAppDataTier.Contracts.Operations.Outputs.Cards
{
    [ExcludeFromCodeCoverage]

    public class GetCardsOfAccountOutput : OperationOutput
    {
        public List<CardDto> Cards { get; set; }
    }
}

using BankingAppDataTier.Contracts.Dtos.Entitites;
using ElideusDotNetFramework.Core.Operations;

namespace BankingAppDataTier.Contracts.Dtos.Outputs.Cards
{
    public class GetCardsOfAccountOutput : OperationOutput
    {
        public List<CardDto> Cards { get; set; }
    }
}

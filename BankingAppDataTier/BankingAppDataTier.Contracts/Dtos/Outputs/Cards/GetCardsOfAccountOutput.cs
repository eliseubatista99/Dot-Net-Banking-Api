using BankingAppDataTier.Contracts.Dtos.Entitites;
using ElideusDotNetFramework.Operations.Contracts;

namespace BankingAppDataTier.Contracts.Dtos.Outputs.Cards
{
    public class GetCardsOfAccountOutput : OperationOutput
    {
        public List<CardDto> Cards { get; set; }
    }
}

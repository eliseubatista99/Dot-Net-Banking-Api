using BankingAppDataTier.Contracts.Dtos.Entitites;

namespace BankingAppDataTier.Contracts.Dtos.Outputs.Cards
{
    public class GetCardsOfAccountOutput : _BaseOutput
    {
        public List<CardDto> Cards { get; set; }
    }
}

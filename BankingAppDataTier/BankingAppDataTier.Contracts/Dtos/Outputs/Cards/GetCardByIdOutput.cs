using BankingAppDataTier.Contracts.Dtos.Entitites;

namespace BankingAppDataTier.Contracts.Dtos.Outputs.Cards
{
    public class GetCardByIdOutput : _BaseOutput
    {
        public CardDto? Card { get; set; }
    }
}

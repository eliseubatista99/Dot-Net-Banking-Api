using BankingAppDataTier.Contracts.Dtos.Entitites;

namespace BankingAppDataTier.Contracts.Dtos.Inputs.Cards
{
    public class AddCardInput
    {
        public required CardDto Card { get; set; }
    }
}

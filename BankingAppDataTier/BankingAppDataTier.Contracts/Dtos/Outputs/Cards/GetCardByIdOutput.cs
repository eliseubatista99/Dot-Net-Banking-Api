using BankingAppDataTier.Contracts.Dtos.Entitites;
using ElideusDotNetFramework.Operations;

namespace BankingAppDataTier.Contracts.Dtos.Outputs.Cards
{
    public class GetCardByIdOutput : OperationOutput
    {
        public CardDto? Card { get; set; }
    }
}

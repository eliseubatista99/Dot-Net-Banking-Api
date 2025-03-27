using BankingAppDataTier.Contracts.Dtos.Entitites;
using ElideusDotNetFramework.Operations;

namespace BankingAppDataTier.Contracts.Dtos.Inputs.Cards
{
    public class AddCardInput : OperationInput
    {
        public required CardDto Card { get; set; }
    }
}

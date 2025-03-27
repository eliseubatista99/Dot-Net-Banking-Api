using BankingAppDataTier.Contracts.Dtos.Entitites;
using ElideusDotNetFramework.Core.Operations;
using System.Diagnostics.CodeAnalysis;

namespace BankingAppDataTier.Contracts.Dtos.Inputs.Cards
{
    [ExcludeFromCodeCoverage]

    public class AddCardInput : OperationInput
    {
        public required CardDto Card { get; set; }
    }
}

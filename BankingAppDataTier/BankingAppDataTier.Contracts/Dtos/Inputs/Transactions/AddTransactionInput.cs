using BankingAppDataTier.Contracts.Dtos.Entitites;
using ElideusDotNetFramework.Core.Operations;
using System.Diagnostics.CodeAnalysis;

namespace BankingAppDataTier.Contracts.Dtos.Inputs.Transactions
{
    [ExcludeFromCodeCoverage]

    public class AddTransactionInput : OperationInput
    {
        public required TransactionDto Transaction { get; set; }
    }
}

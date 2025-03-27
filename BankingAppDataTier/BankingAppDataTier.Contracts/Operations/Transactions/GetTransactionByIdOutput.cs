using BankingAppDataTier.Contracts.Dtos;
using ElideusDotNetFramework.Core.Operations;
using System.Diagnostics.CodeAnalysis;

namespace BankingAppDataTier.Contracts.Operations.Transactions
{
    [ExcludeFromCodeCoverage]

    public class GetTransactionByIdOutput : OperationOutput
    {
        public TransactionDto? Transaction { get; set; }
    }
}

using BankingAppDataTier.Contracts.Dtos;
using ElideusDotNetFramework.Core.Operations;
using System.Diagnostics.CodeAnalysis;

namespace BankingAppDataTier.Contracts.Operations
{
    [ExcludeFromCodeCoverage]

    public class GetTransactionsOfClientOutput : OperationOutput
    {
        public required List<TransactionDto> Transactions { get; set; }
    }
}

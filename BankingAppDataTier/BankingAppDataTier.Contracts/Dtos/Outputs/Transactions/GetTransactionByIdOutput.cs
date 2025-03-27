using BankingAppDataTier.Contracts.Dtos.Entitites;
using ElideusDotNetFramework.Core.Operations;
using System.Diagnostics.CodeAnalysis;

namespace BankingAppDataTier.Contracts.Dtos.Outputs.Transactions
{
    [ExcludeFromCodeCoverage]

    public class GetTransactionByIdOutput : OperationOutput
    {
        public TransactionDto? Transaction { get; set; }
    }
}

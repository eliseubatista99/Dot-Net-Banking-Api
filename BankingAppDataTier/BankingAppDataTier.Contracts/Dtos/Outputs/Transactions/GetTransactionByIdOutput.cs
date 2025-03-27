using BankingAppDataTier.Contracts.Dtos.Entitites;
using ElideusDotNetFramework.Operations;

namespace BankingAppDataTier.Contracts.Dtos.Outputs.Transactions
{
    public class GetTransactionByIdOutput : OperationOutput
    {
        public TransactionDto? Transaction { get; set; }
    }
}

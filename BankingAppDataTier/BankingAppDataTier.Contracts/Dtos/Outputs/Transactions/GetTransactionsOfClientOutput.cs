using BankingAppDataTier.Contracts.Dtos.Entitites;
using ElideusDotNetFramework.Operations.Contracts;

namespace BankingAppDataTier.Contracts.Dtos.Outputs.Transactions
{
    public class GetTransactionsOfClientOutput : OperationOutput
    {
        public required List<TransactionDto> Transactions { get; set; }
    }
}

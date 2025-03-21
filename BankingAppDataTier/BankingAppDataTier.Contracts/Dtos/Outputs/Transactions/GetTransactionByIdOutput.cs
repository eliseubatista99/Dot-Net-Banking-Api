using BankingAppDataTier.Contracts.Dtos.Entitites;

namespace BankingAppDataTier.Contracts.Dtos.Outputs.Transactions
{
    public class GetTransactionByIdOutput : _BaseOutput
    {
        public TransactionDto? Transaction { get; set; }
    }
}

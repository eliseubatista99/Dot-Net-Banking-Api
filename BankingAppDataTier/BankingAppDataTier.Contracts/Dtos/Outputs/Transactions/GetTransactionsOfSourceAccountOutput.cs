using BankingAppDataTier.Contracts.Dtos.Entitites;

namespace BankingAppDataTier.Contracts.Dtos.Outputs.Transactions
{
    public class GetTransactionsOfSourceAccountOutput : BaseOutput
    {
        public required List<TransactionDto> Transactions { get; set; }
    }
}

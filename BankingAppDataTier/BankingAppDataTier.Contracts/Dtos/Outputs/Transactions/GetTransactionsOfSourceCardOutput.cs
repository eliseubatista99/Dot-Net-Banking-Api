using BankingAppDataTier.Contracts.Dtos.Entitites;

namespace BankingAppDataTier.Contracts.Dtos.Outputs.Transactions
{
    public class GetTransactionsOfSourceCardOutput : BaseOutput
    {
        public required List<TransactionDto> Transactions { get; set; }
    }
}

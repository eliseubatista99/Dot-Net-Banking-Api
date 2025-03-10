using BankingAppDataTier.Contracts.Dtos.Entitites;

namespace BankingAppDataTier.Contracts.Dtos.Inputs.Transactions
{
    public class AddTransactionInput
    {
        public required TransactionDto Transaction { get; set; }
    }
}

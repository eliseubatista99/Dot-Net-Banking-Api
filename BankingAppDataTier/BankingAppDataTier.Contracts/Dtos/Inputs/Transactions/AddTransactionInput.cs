using BankingAppDataTier.Contracts.Dtos.Entitites;

namespace BankingAppDataTier.Contracts.Dtos.Inputs.Transactions
{
    public class AddTransactionInput : _BaseInput
    {
        public required TransactionDto Transaction { get; set; }
    }
}

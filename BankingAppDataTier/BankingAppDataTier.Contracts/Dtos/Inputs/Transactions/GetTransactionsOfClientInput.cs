using BankingAppDataTier.Contracts.Enums;

namespace BankingAppDataTier.Contracts.Dtos.Inputs.Transactions
{
    public class GetTransactionsOfClientInput: _BaseInput
    {
        public required string Client { get; set; }

        public List<string>? Accounts { get; set; }

        public List<string>? Cards { get; set; }

        public TransactionRole? Role { get; set; }
    }
}

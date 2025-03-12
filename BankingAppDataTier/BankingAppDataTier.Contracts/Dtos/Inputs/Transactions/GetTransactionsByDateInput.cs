using BankingAppDataTier.Contracts.Dtos.Entitites;
using BankingAppDataTier.Contracts.Enums;

namespace BankingAppDataTier.Contracts.Dtos.Inputs.Transactions
{
    public class GetTransactionsByDateInput
    {
        public required DateTime StartDate { get; set; }
        public required DateTime EndDate { get; set; }

        public required string Client { get; set; }

        public List<string>? Accounts { get; set; }

        public TransactionRole? Role { get; set; }
    }
}

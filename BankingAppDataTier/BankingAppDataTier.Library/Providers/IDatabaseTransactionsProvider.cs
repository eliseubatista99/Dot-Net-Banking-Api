using BankingAppDataTier.Library.Database;
using ElideusDotNetFramework.Database;

namespace BankingAppDataTier.Library.Providers
{
    public interface IDatabaseTransactionsProvider : IDatabaseProvider<TransactionTableEntry>
    {
        public List<TransactionTableEntry> GetBySourceAccount(string account);

        public List<TransactionTableEntry> GetByDestinationAccount(string account);

        public List<TransactionTableEntry> GetBySourceCard(string card);
    }
}

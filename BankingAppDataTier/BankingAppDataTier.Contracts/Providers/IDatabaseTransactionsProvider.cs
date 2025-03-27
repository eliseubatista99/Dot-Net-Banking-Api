using BankingAppDataTier.Contracts.Database;
using ElideusDotNetFramework.PostgreSql;

namespace BankingAppDataTier.Contracts.Providers
{
    public interface IDatabaseTransactionsProvider : INpgsqlDatabaseProvider<TransactionTableEntry>
    {
        public List<TransactionTableEntry> GetBySourceAccount(string account);

        public List<TransactionTableEntry> GetByDestinationAccount(string account);

        public List<TransactionTableEntry> GetBySourceCard(string card);
    }
}

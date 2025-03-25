using BankingAppDataTier.Contracts.Database;

namespace BankingAppDataTier.Contracts.Providers
{
    public interface IDatabaseTransactionsProvider
    {
        public bool CreateTableIfNotExists();

        public List<TransactionTableEntry> GetAll();

        public TransactionTableEntry? GetById(string id);

        public List<TransactionTableEntry> GetBySourceAccount(string account);

        public List<TransactionTableEntry> GetByDestinationAccount(string account);

        public List<TransactionTableEntry> GetBySourceCard(string card);

        public bool Add(TransactionTableEntry entry);

        public bool Edit(TransactionTableEntry entry);

        public bool Delete(string id);

        public bool DeleteAll();
    }
}

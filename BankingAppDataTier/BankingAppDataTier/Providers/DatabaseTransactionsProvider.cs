using BankingAppDataTier.Contracts.Database;
using BankingAppDataTier.Contracts.Providers;

namespace BankingAppDataTier.Providers
{
    public class DatabaseTransactionsProvider : IDatabaseTransactionsProvider
    {
        public bool Add(TransactionTableEntry entry)
        {
            throw new NotImplementedException();
        }

        public bool CreateTableIfNotExists()
        {
            throw new NotImplementedException();
        }

        public bool Delete(string id)
        {
            throw new NotImplementedException();
        }

        public bool Edit(TransactionTableEntry entry)
        {
            throw new NotImplementedException();
        }

        public List<TransactionTableEntry> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<TransactionTableEntry> GetByDestinationAccount(string account)
        {
            throw new NotImplementedException();
        }

        public TransactionTableEntry? GetById(string id)
        {
            throw new NotImplementedException();
        }

        public List<TransactionTableEntry> GetBySourceAccount(string account)
        {
            throw new NotImplementedException();
        }

        public List<TransactionTableEntry> GetBySourceCard(string card)
        {
            throw new NotImplementedException();
        }
    }
}
using BankingAppDataTier.Contracts.Database;

namespace BankingAppDataTier.Contracts.Providers
{
    public interface IDatabaseClientsProvider
    {
        public bool CreateTableIfNotExists();

        public List<ClientsTableEntry> GetAll();

        public ClientsTableEntry? GetById(string id);

        public bool Add(ClientsTableEntry entry);

        public bool Edit(ClientsTableEntry entry);

        public bool ChangePassword(string id, string password);

        public bool Delete(string id);

        public bool DeleteAll();
    }
}

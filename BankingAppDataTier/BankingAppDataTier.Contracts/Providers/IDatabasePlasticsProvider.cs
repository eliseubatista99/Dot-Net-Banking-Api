using BankingAppDataTier.Contracts.Database;

namespace BankingAppDataTier.Contracts.Providers
{
    public interface IDatabasePlasticsProvider
    {
        public bool CreateTableIfNotExists();

        public List<PlasticTableEntry> GetAll();

        public PlasticTableEntry? GetById(string id);

        public List<PlasticTableEntry> GetPlasticsOfCardType(string cardType, bool onlyActive = false);

        public bool Add(PlasticTableEntry entry);

        public bool Edit(PlasticTableEntry entry);

        public bool Delete(string id);

        public bool DeleteAll();
    }
}

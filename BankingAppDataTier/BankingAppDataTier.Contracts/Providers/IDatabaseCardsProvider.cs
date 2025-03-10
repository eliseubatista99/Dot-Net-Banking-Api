using BankingAppDataTier.Contracts.Database;

namespace BankingAppDataTier.Contracts.Providers
{
    public interface IDatabaseCardsProvider
    {
        public bool CreateTableIfNotExists();

        public List<CardsTableEntry> GetAll();

        public List<CardsTableEntry> GetCardsOfAccount(string accountId);

        public List<CardsTableEntry> GetCardsWithPlastic(string plasticId);

        public CardsTableEntry? GetById(string id);

        public bool Add(CardsTableEntry entry);

        public bool Edit(CardsTableEntry entry);

        public bool Delete(string id);

        public bool DeleteAll();
    }
}

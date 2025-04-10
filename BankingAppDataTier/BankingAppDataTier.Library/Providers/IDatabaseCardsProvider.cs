using BankingAppDataTier.Library.Database;
using ElideusDotNetFramework.Database;

namespace BankingAppDataTier.Library.Providers
{
    public interface IDatabaseCardsProvider : IDatabaseProvider<CardsTableEntry>
    {
        public List<CardsTableEntry> GetCardsOfAccount(string accountId);

        public List<CardsTableEntry> GetCardsWithPlastic(string plasticId);
    }
}

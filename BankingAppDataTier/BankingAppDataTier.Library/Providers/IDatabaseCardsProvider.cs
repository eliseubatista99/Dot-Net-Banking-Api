using BankingAppDataTier.Library.Database;
using ElideusDotNetFramework.PostgreSql;

namespace BankingAppDataTier.Library.Providers
{
    public interface IDatabaseCardsProvider : INpgsqlDatabaseProvider<CardsTableEntry>
    {
        public List<CardsTableEntry> GetCardsOfAccount(string accountId);

        public List<CardsTableEntry> GetCardsWithPlastic(string plasticId);
    }
}

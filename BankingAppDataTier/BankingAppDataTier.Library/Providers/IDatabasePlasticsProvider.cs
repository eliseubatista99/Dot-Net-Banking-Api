using BankingAppDataTier.Library.Database;
using ElideusDotNetFramework.PostgreSql;

namespace BankingAppDataTier.Library.Providers
{
    public interface IDatabasePlasticsProvider : INpgsqlDatabaseProvider<PlasticTableEntry>
    {
        public List<PlasticTableEntry> GetPlasticsOfCardType(string cardType, bool onlyActive = false);
    }
}

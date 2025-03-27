using BankingAppDataTier.Contracts.Database;
using ElideusDotNetFramework.Database;

namespace BankingAppDataTier.Contracts.Providers
{
    public interface IDatabasePlasticsProvider : INpgsqlDatabaseProvider<PlasticTableEntry>
    {
        public List<PlasticTableEntry> GetPlasticsOfCardType(string cardType, bool onlyActive = false);
    }
}

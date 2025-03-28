using BankingAppDataTier.Library.Database;
using ElideusDotNetFramework.Database;

namespace BankingAppDataTier.Library.Providers
{
    public interface IDatabasePlasticsProvider : IDatabaseProvider<PlasticTableEntry>
    {
        public List<PlasticTableEntry> GetPlasticsOfCardType(string cardType, bool onlyActive = false);
    }
}

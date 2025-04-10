using BankingAppDataTier.Library.Database;
using ElideusDotNetFramework.Database;

namespace BankingAppDataTier.Library.Providers
{
    public interface IDatabaseClientsProvider : IDatabaseProvider<ClientsTableEntry>
    {
        public bool ChangePassword(string id, string password);
    }
}

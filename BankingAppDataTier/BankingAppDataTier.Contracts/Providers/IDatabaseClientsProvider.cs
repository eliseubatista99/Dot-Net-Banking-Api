using BankingAppDataTier.Contracts.Database;
using ElideusDotNetFramework.Database;

namespace BankingAppDataTier.Contracts.Providers
{
    public interface IDatabaseClientsProvider : INpgsqlDatabaseProvider<ClientsTableEntry>
    {
        public bool ChangePassword(string id, string password);
    }
}

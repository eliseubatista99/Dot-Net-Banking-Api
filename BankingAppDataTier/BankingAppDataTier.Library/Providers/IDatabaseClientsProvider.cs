using BankingAppDataTier.Library.Database;
using ElideusDotNetFramework.PostgreSql;

namespace BankingAppDataTier.Library.Providers
{
    public interface IDatabaseClientsProvider : INpgsqlDatabaseProvider<ClientsTableEntry>
    {
        public bool ChangePassword(string id, string password);
    }
}

using BankingAppAuthenticationTier.Library.Database;
using ElideusDotNetFramework.PostgreSql;

namespace BankingAppAuthenticationTier.Library.Providers
{
    public interface IDatabaseClientsProvider : INpgsqlDatabaseProvider<ClientsTableEntry>
    {
    }
}

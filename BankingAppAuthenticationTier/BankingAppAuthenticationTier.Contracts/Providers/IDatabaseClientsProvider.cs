using BankingAppAuthenticationTier.Contracts.Database;
using ElideusDotNetFramework.PostgreSql;

namespace BankingAppAuthenticationTier.Contracts.Providers
{
    public interface IDatabaseClientsProvider : INpgsqlDatabaseProvider<ClientsTableEntry>
    {
    }
}

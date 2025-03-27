using BankingAppDataTier.Library.Database;
using ElideusDotNetFramework.PostgreSql;

namespace BankingAppDataTier.Library.Providers
{
    public interface IDatabaseAccountsProvider : INpgsqlDatabaseProvider<AccountsTableEntry>
    {
        public List<AccountsTableEntry> GetAccountsOfClient(string clientId);
    }
}

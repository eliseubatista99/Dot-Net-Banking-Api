using BankingAppDataTier.Contracts.Database;
using ElideusDotNetFramework.Database;

namespace BankingAppDataTier.Contracts.Providers
{
    public interface IDatabaseAccountsProvider : INpgsqlDatabaseProvider<AccountsTableEntry>
    {
        public List<AccountsTableEntry> GetAccountsOfClient(string clientId);
    }
}

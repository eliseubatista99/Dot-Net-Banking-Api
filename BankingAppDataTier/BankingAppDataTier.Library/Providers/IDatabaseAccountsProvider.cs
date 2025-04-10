using BankingAppDataTier.Library.Database;
using ElideusDotNetFramework.Database;

namespace BankingAppDataTier.Library.Providers
{
    public interface IDatabaseAccountsProvider : IDatabaseProvider<AccountsTableEntry>
    {
        public List<AccountsTableEntry> GetAccountsOfClient(string clientId);
    }
}

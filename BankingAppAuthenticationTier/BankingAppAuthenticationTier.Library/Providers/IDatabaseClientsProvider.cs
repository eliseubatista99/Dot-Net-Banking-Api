using BankingAppAuthenticationTier.Library.Database;
using ElideusDotNetFramework.Database;

namespace BankingAppAuthenticationTier.Library.Providers
{
    public interface IDatabaseClientsProvider : IDatabaseProvider<ClientsTableEntry>
    {
    }
}

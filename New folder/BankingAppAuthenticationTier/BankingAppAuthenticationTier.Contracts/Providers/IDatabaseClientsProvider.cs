using BankingAppAuthenticationTier.Contracts.Database;

namespace BankingAppAuthenticationTier.Contracts.Providers
{
    public interface IDatabaseClientsProvider
    {
        public ClientsTableEntry? GetById(string id);
    }
}

using BankingAppAuthenticationTier.Library.Database;
using ElideusDotNetFramework.Database;

namespace BankingAppAuthenticationTier.Library.Providers
{
    public interface IDatabaseTokenProvider : IDatabaseProvider<TokenTableEntry>
    {
        public bool DeleteAllExpired();

        public bool DeleteTokensOfClient(string clientId);
    }
}

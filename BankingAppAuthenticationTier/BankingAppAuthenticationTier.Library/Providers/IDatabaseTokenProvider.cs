using BankingAppAuthenticationTier.Library.Database;
using ElideusDotNetFramework.PostgreSql;

namespace BankingAppAuthenticationTier.Library.Providers
{
    public interface IDatabaseTokenProvider : INpgsqlDatabaseProvider<TokenTableEntry>
    {
        public bool DeleteAllExpired();

        public bool DeleteTokensOfClient(string clientId);
    }
}

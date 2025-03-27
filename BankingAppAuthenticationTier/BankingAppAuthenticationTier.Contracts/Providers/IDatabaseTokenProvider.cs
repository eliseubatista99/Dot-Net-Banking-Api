using BankingAppAuthenticationTier.Contracts.Database;
using ElideusDotNetFramework.PostgreSql;

namespace BankingAppAuthenticationTier.Contracts.Providers
{
    public interface IDatabaseTokenProvider : INpgsqlDatabaseProvider<TokenTableEntry>
    {
        public bool DeleteAllExpired();

        public bool DeleteTokensOfClient(string clientId);
    }
}

using BankingAppDataTier.Contracts.Database;
using ElideusDotNetFramework.PostgreSql;

namespace BankingAppDataTier.Contracts.Providers
{
    public interface IDatabaseTokenProvider : INpgsqlDatabaseProvider<TokenTableEntry>
    {
        public bool DeleteAllExpired();

        public bool DeleteTokensOfClient(string clientId);
    }
}

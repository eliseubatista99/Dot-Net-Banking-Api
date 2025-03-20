using BankingAppAuthenticationTier.Contracts.Database;
using BankingAppAuthenticationTier.Contracts.Providers;

namespace BankingAppAuthenticationTier.DatabaseInitializers
{
    public static class TokenDatabaseInitializer
    {
        public static void InitializeDatabase(IDatabaseTokenProvider dbProvider)
        {
            dbProvider.CreateTableIfNotExists();

            dbProvider.DeleteAllExpired();
        }

        public static void CustomMock(IDatabaseTokenProvider dbProvider, List<TokenTableEntry> mock)
        {
            dbProvider.CreateTableIfNotExists();

            foreach (var entry in mock)
            {
                dbProvider.Add(entry);
            }
        }

    }
}

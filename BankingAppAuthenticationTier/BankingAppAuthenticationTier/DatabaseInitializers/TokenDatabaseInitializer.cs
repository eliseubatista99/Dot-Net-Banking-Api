using BankingAppAuthenticationTier.Library.Database;
using BankingAppAuthenticationTier.Library.Providers;
using System.Diagnostics.CodeAnalysis;

namespace BankingAppAuthenticationTier.DatabaseInitializers
{
    [ExcludeFromCodeCoverage]
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

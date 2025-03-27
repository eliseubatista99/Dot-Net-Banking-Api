using BankingAppAuthenticationTier.Library.Providers;
using System.Diagnostics.CodeAnalysis;

namespace BankingAppAuthenticationTier.DatabaseInitializers
{
    [ExcludeFromCodeCoverage]
    public static class ClientsDatabaseInitializer
    {
        public static void DefaultMock(IDatabaseClientsProvider dbProvider)
        {
            dbProvider.CreateTableIfNotExists();        
        }
    }
}

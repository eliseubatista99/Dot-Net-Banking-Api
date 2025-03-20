using BankingAppDataTier.Contracts.Constants;
using BankingAppDataTier.Contracts.Database;
using BankingAppDataTier.Contracts.Providers;

namespace BankingAppDataTier.Tests.Mocks.Database
{
    public static class TokensEntriesMock
    {
        private static List<TokenTableEntry> Entries =
        [
            new TokenTableEntry
            {
                Token = "AA",
                ClientId = "Permanent_Client_01",
                ExpirationDate = DateTime.Now,
            }
        ];

        public static void Mock(IDatabaseTokenProvider dbProvider)
        {
            dbProvider.CreateTableIfNotExists();

            foreach (var entry in Entries)
            {
                dbProvider.Add(entry);
            }
        }
    }
}

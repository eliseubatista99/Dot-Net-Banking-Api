using BankingAppDataTier.Contracts.Database;
using BankingAppDataTier.Contracts.Providers;
using BankingAppDataTier.Tests.Constants;

namespace BankingAppDataTier.Tests.Mocks.Database
{
    public static class TokensEntriesMock
    {
        private static List<TokenTableEntry> Entries =
        [
            new TokenTableEntry
            {
                Token = TestsConstants.PermanentToken,
                ClientId = "Permanent_Client_02",
                ExpirationDate = DateTime.Now.AddMinutes(15),
            },
            new TokenTableEntry
            {
                Token = TestsConstants.ExpiredToken,
                ClientId = "Permanent_Client_03",
                ExpirationDate = DateTime.Now.AddMinutes(-15),
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

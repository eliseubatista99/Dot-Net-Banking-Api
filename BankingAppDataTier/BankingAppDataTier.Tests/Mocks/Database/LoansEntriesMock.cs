using BankingAppDataTier.Contracts.Database;
using BankingAppDataTier.Contracts.Providers;

namespace BankingAppDataTier.Tests.Mocks.Database
{
    public static class LoansEntriesMock
    {
        private static List<LoanTableEntry> Entries =
        [
            new LoanTableEntry
            {
                Id = "L01",
                RelatedAccount = "Permanent_Current_01",
                StartDate = new DateTime(2025, 02, 03),
                RelatedOffer = "LO01",
                Duration = 24,
                Amount = 10000.0M,
            },
            new LoanTableEntry
            {
                Id = "L02",
                RelatedAccount = "Permanent_Current_02",
                StartDate = new DateTime(2025, 02, 03),
                RelatedOffer = "LO01",
                Duration = 24,
                Amount = 10000.0M,
            }
        ];
        public static void Mock(IDatabaseLoansProvider dbProvider)
        {
            dbProvider.CreateTableIfNotExists();

            var elementsInDb = dbProvider.GetAll();

            //If the database already has entries, don't add anything
            if (elementsInDb.Count > 0)
            {
                return;
            }

            foreach (var entry in Entries)
            {
                dbProvider.Add(entry);
            }
        }
    }
}

using BankingAppDataTier.Contracts.Database;
using BankingAppDataTier.Contracts.Providers;

namespace BankingAppDataTier.Tests.Mocks.Database
{
    public static class LoanOffersEntriesMock
    {
        private static List<LoanOfferTableEntry> Entries =
        [
            new LoanOfferTableEntry
            {
                Id = "LO01",
                LoanType = "AU",
                MaxEffort = 30,
                Interest = 7.0M,
                IsActive = true,
            },
            new LoanOfferTableEntry
            {
                Id = "LO02",
                LoanType = "MO",
                MaxEffort = 40,
                Interest = 3.0M,
                IsActive = true,
            },
            new LoanOfferTableEntry
            {
                Id = "LO03",
                LoanType = "PE",
                MaxEffort = 25,
                Interest = 10.0M,
                IsActive = true,
            },
            new LoanOfferTableEntry
            {
                Id = "LO04",
                LoanType = "AU",
                MaxEffort = 20,
                Interest = 3.0M,
                IsActive = false,
            }
        ];

        public static void Mock(IDatabaseLoanOfferProvider dbProvider)
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

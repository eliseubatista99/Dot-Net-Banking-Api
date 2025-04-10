using BankingAppDataTier.Library.Database;
using BankingAppDataTier.Library.Providers;

namespace BankingAppDataTier.Tests.Mocks.Database
{
    public static class LoanOffersEntriesMock
    {
        private static List<LoanOfferTableEntry> Entries =
        [
            new LoanOfferTableEntry
            {
                Id = "Permanent_AU_01",
                Name = "Auto Loan",
                Description = "Take the wheel",
                LoanType = "AU",
                MaxEffort = 30,
                Interest = 7.0M,
                IsActive = true,
            },
            new LoanOfferTableEntry
            {
                Id = "Permanent_MO_01",
                Name = "Mortgage Loan",
                Description = "The house of your dreams",
                LoanType = "MO",
                MaxEffort = 40,
                Interest = 3.0M,
                IsActive = true,
            },
            new LoanOfferTableEntry
            {
                Id = "Permanent_PE_01",
                Name = "Personal Loan",
                Description = "Embrace your dreams",
                LoanType = "PE",
                MaxEffort = 25,
                Interest = 10.0M,
                IsActive = true,
            },
            new LoanOfferTableEntry
            {
                Id = "To_Edit_AU_01",
                Name = "Auto Loan",
                Description = "Take the wheel",
                LoanType = "AU",
                MaxEffort = 25,
                Interest = 10.0M,
                IsActive = true,
            },
            new LoanOfferTableEntry
            {
                Id = "To_Edit_AU_02",
                Name = "Auto Loan",
                Description = "Take the wheel",
                LoanType = "AU",
                MaxEffort = 25,
                Interest = 10.0M,
                IsActive = true,
            },
            new LoanOfferTableEntry
            {
                Id = "To_Delete_AU_01",
                Name = "Auto Loan",
                Description = "Take the wheel",
                LoanType = "AU",
                MaxEffort = 25,
                Interest = 10.0M,
                IsActive = true,
            },
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

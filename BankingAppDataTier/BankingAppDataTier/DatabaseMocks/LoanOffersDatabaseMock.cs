using BankingAppDataTier.Contracts.Constants;
using BankingAppDataTier.Contracts.Database;
using BankingAppDataTier.Contracts.Providers;

namespace BankingAppDataTier.Database
{
    public static class LoanOffersDatabaseMock
    {
        public static void DefaultMock(IDatabaseLoanOfferProvider dbProvider, bool clearDatabase = false)
        {
            if (clearDatabase == true)
            {
                dbProvider.DeleteAll();
            }

            dbProvider!.CreateTableIfNotExists();

            var elementsInDb = dbProvider.GetAll();


            //If the database already has entries, don't add anything
            if (elementsInDb.Count > 0)
            {
                return;
            }

            dbProvider.Add(
                new LoanOfferTableEntry
                {
                    Id = "LO01",
                    LoanType = "AU",
                    MaxEffort = 30,
                    Interest = 7.0M,
                    IsActive = true,
                }
            );

            dbProvider.Add(
               new LoanOfferTableEntry
               {
                   Id = "LO02",
                   LoanType = "MO",
                   MaxEffort = 40,
                   Interest = 3.0M,
                   IsActive = true,
               }
            );

            dbProvider.Add(
               new LoanOfferTableEntry
               {
                   Id = "LO03",
                   LoanType = "PE",
                   MaxEffort = 25,
                   Interest = 10.0M,
                   IsActive = true,
               }
            );

            dbProvider.Add(
                new LoanOfferTableEntry
                {
                    Id = "LO04",
                    LoanType = "AU",
                    MaxEffort = 20,
                    Interest = 3.0M,
                    IsActive = false,
                }
            );
        }

        public static void CustomMock(IDatabaseLoanOfferProvider dbProvider, List<LoanOfferTableEntry> mock)
        {
            dbProvider.DeleteAll();

            dbProvider!.CreateTableIfNotExists();

            foreach (var entry in mock)
            {
                dbProvider.Add(entry);
            }
        }
    }
}

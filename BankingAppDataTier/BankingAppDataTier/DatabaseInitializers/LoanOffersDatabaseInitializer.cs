using BankingAppDataTier.Contracts.Database;
using BankingAppDataTier.Contracts.Providers;

namespace BankingAppDataTier.DatabaseInitializers
{
    public static class LoanOffersDatabaseInitializer
    {
        public static void DefaultMock(IDatabaseLoanOfferProvider dbProvider)
        {
            dbProvider.CreateTableIfNotExists();

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
                    Name = "Auto Loan",
                    Description = "Take the wheel",
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
                   Name = "Mortgage Loan",
                   Description = "The house of your dreams",
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
                   Name = "Personal Loan",
                   Description = "Embrace your dreams",
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
                    Name = "Auto Loan",
                    Description = "Take the wheel",
                    LoanType = "AU",
                    MaxEffort = 20,
                    Interest = 3.0M,
                    IsActive = false,
                }
            );
        }

        public static void CustomMock(IDatabaseLoanOfferProvider dbProvider, List<LoanOfferTableEntry> mock)
        {
            dbProvider.CreateTableIfNotExists();

            var elementsInDb = dbProvider.GetAll();

            //If the database already has entries, don't add anything
            if (elementsInDb.Count > 0)
            {
                return;
            }

            foreach (var entry in mock)
            {
                dbProvider.Add(entry);
            }
        }
    }
}

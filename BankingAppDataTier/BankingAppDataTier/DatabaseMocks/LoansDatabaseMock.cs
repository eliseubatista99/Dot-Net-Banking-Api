using BankingAppDataTier.Contracts.Constants;
using BankingAppDataTier.Contracts.Database;
using BankingAppDataTier.Contracts.Providers;

namespace BankingAppDataTier.Database
{
    public static class LoansDatabaseMock
    {
        public static void DefaultMock(IDatabaseLoansProvider dbProvider, bool clearDatabase = false)
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
                new LoanTableEntry
                {
                    Id = "L01",
                    RelatedAccount = "ACJW000000",
                    StartDate = new DateTime(2025, 02, 03),
                    RelatedOffer = "LO01",
                    Duration = 24,
                    Amount = 10000.0M,
                }
            );
        }

        public static void CustomMock(IDatabaseLoansProvider dbProvider, List<LoanTableEntry> mock)
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

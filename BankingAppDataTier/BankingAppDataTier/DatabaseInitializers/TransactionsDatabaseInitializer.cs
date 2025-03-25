using BankingAppDataTier.Contracts.Database;
using BankingAppDataTier.Contracts.Providers;

namespace BankingAppDataTier.DatabaseInitializers
{
    public static class TransactionsDatabaseInitializer
    {
        public static void DefaultMock(IDatabaseTransactionsProvider dbProvider)
        {
            dbProvider.CreateTableIfNotExists();

            var elementsInDb = dbProvider.GetAll();

            //If the database already has entries, don't add anything
            if (elementsInDb.Count > 0)
            {
                return;
            }

            dbProvider.Add(
                new TransactionTableEntry
                {
                    Id = "T001",
                    TransactionDate = new DateTime(2025, 02, 10),
                    Description = "Test transfer",
                    Amount = 100.35M,
                    Fees = 1.25M,
                    Urgent = true,
                    SourceAccount = "ACJW000000",
                    DestinationName = "Eletricity Company",
                }
            );

            dbProvider.Add(
                new TransactionTableEntry
                {
                    Id = "T002",
                    TransactionDate = new DateTime(2025, 02, 10),
                    Description = "Test transfer from card",
                    Amount = 100.35M,
                    Fees = 1.25M,
                    Urgent = true,
                    SourceCard = "ACJW000000_DB01",
                    DestinationName = "Water Company",
                }
            );
        }

        public static void CustomMock(IDatabaseTransactionsProvider dbProvider, List<TransactionTableEntry> mock)
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

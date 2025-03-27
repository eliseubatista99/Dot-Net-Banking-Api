using BankingAppDataTier.Contracts.Database;
using BankingAppDataTier.Contracts.Providers;
using System.Diagnostics.CodeAnalysis;

namespace BankingAppDataTier.DatabaseInitializers
{
    [ExcludeFromCodeCoverage]
    public static class LoansDatabaseInitializer
    {
        public static void DefaultMock(IDatabaseLoansProvider dbProvider)
        {
            dbProvider.CreateTableIfNotExists();

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
                    Name = "Auto Loan",
                    RelatedAccount = "ACJW000000",
                    StartDate = new DateTime(2025, 02, 03),
                    RelatedOffer = "LO01",
                    Duration = 24,
                    ContractedAmount = 10000.0M,
                    PaidAmount = 0,
                }
            );     
        }

        public static void CustomMock(IDatabaseLoansProvider dbProvider, List<LoanTableEntry> mock)
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

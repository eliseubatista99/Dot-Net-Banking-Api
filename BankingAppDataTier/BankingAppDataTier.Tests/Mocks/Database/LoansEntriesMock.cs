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
                Id = "Permanent_AU_01",
                RelatedAccount = "Permanent_Current_01",
                Name = "Auto Loan",
                StartDate = new DateTime(2025, 02, 03),
                RelatedOffer = "Permanent_AU_01",
                Duration = 24,
                ContractedAmount = 10000.0M,
                PaidAmount = 0,
            },
            new LoanTableEntry
            {
                Id = "Permanent_AU_02",
                RelatedAccount = "Permanent_Current_02",
                Name = "Auto Loan",
                StartDate = new DateTime(2025, 02, 03),
                RelatedOffer = "Permanent_AU_01",
                Duration = 24,
                ContractedAmount = 10000.0M,
                PaidAmount = 0,
            },
            new LoanTableEntry
            {
                Id = "Permanent_MO_01",
                RelatedAccount = "Permanent_Current_01",
                Name = "Mortgage Loan",
                StartDate = new DateTime(2025, 02, 03),
                RelatedOffer = "Permanent_MO_01",
                Duration = 24,
                ContractedAmount = 10000.0M,
                PaidAmount = 0,
            },
            new LoanTableEntry
            {
                Id = "Permanent_PE_01",
                RelatedAccount = "Permanent_Current_01",
                Name = "Personal Loan",
                StartDate = new DateTime(2025, 02, 03),
                RelatedOffer = "Permanent_PE_01",
                Duration = 24,
                ContractedAmount = 10000.0M,
                PaidAmount = 0,
            },
            new LoanTableEntry
            {
                Id = "To_Edit_AU_01",
                RelatedAccount = "Permanent_Current_01",
                Name = "Auto Loan",
                StartDate = new DateTime(2025, 02, 03),
                RelatedOffer = "Permanent_AU_01",
                Duration = 24,
                ContractedAmount = 10000.0M,
                PaidAmount = 0,
            },
            new LoanTableEntry
            {
                Id = "To_Delete_AU_01",
                RelatedAccount = "Permanent_Current_01",
                Name = "Auto Loan",
                StartDate = new DateTime(2025, 02, 03),
                RelatedOffer = "Permanent_AU_01",
                Duration = 24,
                ContractedAmount = 10000.0M,
                PaidAmount = 0,
            },
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

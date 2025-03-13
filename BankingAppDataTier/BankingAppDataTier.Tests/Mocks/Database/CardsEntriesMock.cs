using BankingAppDataTier.Contracts.Database;
using BankingAppDataTier.Contracts.Providers;

namespace BankingAppDataTier.Tests.Mocks.Database
{
    public static class CardsEntriesMock
    {
        private static List<CardsTableEntry> Entries =
        [
            new CardsTableEntry
            {
                Id = "Permanent_Debit_01",
                Name = "DB_Basic",
                RelatedAccountID = "Permanent_Current_01",
                PlasticId = "Permanent_Debit_01",
                RequestDate = new DateTime(2025, 01, 01),
                ActivationDate = new DateTime(2025, 01, 15),
                ExpirationDate = new DateTime(2028, 01, 15),
            },
            new CardsTableEntry
            {
                Id = "Permanent_Debit_02",
                Name = "DB_Basic",
                RelatedAccountID = "Permanent_Current_01",
                PlasticId = "Permanent_Debit_01",
                RequestDate = new DateTime(2025, 01, 01),
                ActivationDate = new DateTime(2025, 01, 15),
                ExpirationDate = new DateTime(2028, 01, 15),
            },
            new CardsTableEntry
            {
                Id = "Permanent_PrePaid_01",
                Name = "PP_Agile",
                RelatedAccountID = "Permanent_Current_01",
                PlasticId = "Permanent_PrePaid_01",
                Balance = 100,
                RequestDate = new DateTime(2025, 01, 01),
                ActivationDate = new DateTime(2025, 01, 15),
                ExpirationDate = new DateTime(2028, 01, 15),
            },
            new CardsTableEntry
            {
                Id = "Permanent_Credit_01",
                Name = "CR_Prestige",
                RelatedAccountID = "Permanent_Current_01",
                PlasticId = "Permanent_Credit_01",
                Balance = 100,
                PaymentDay = 11,
                RequestDate = new DateTime(2025, 01, 01),
                ActivationDate = new DateTime(2025, 01, 15),
                ExpirationDate = new DateTime(2028, 01, 15),
            },
            new CardsTableEntry
            {
                Id = "Permanent_Meal_01",
                Name = "ME_TableSlide",
                RelatedAccountID = "Permanent_Current_01",
                PlasticId = "Permanent_Meal_01",
                Balance = 100,
                RequestDate = new DateTime(2025, 01, 01),
                ActivationDate = new DateTime(2025, 01, 15),
                ExpirationDate = new DateTime(2028, 01, 15),
            },
            new CardsTableEntry
            {
                Id = "To_Edit_Debit_01",
                Name = "DB_Basic",
                RelatedAccountID = "Permanent_Current_01",
                PlasticId = "Permanent_Debit_01",
                RequestDate = new DateTime(2025, 01, 01),
                ActivationDate = new DateTime(2025, 01, 15),
                ExpirationDate = new DateTime(2028, 01, 15),
            },
            new CardsTableEntry
            {
                Id = "To_Edit_PrePaid_01",
                Name = "PP_Agile",
                RelatedAccountID = "Permanent_Current_01",
                PlasticId = "Permanent_PrePaid_01",
                Balance = 100,
                RequestDate = new DateTime(2025, 01, 01),
                ActivationDate = new DateTime(2025, 01, 15),
                ExpirationDate = new DateTime(2028, 01, 15),
            },
            new CardsTableEntry
            {
                Id = "To_Edit_Credit_01",
                Name = "CR_Prestige",
                RelatedAccountID = "Permanent_Current_01",
                PlasticId = "Permanent_Credit_01",
                Balance = 100,
                PaymentDay = 11,
                RequestDate = new DateTime(2025, 01, 01),
                ActivationDate = new DateTime(2025, 01, 15),
                ExpirationDate = new DateTime(2028, 01, 15),
            },
            new CardsTableEntry
            {
                Id = "To_Delete_Debit_01",
                Name = "DB_Basic",
                RelatedAccountID = "Permanent_Current_01",
                PlasticId = "Permanent_Debit_01",
                RequestDate = new DateTime(2025, 01, 01),
                ActivationDate = new DateTime(2025, 01, 15),
                ExpirationDate = new DateTime(2028, 01, 15),
            }
        ];

        public static void Mock(IDatabaseCardsProvider dbProvider)
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

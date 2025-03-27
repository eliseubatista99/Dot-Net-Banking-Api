using BankingAppDataTier.Library.Constants;
using BankingAppDataTier.Library.Database;
using BankingAppDataTier.Library.Providers;

namespace BankingAppDataTier.Tests.Mocks.Database
{
    public static class PlasticsEntriesMock
    {
        private static List<PlasticTableEntry> Entries =
        [
            new PlasticTableEntry
            {
                Id = "Permanent_Debit_01",
                CardType = BankingAppDataTierConstants.CARD_TYPE_DEBIT,
                Name = "DotNet Basic",
                IsActive = true,
                Image = string.Empty,
            }, 
            new PlasticTableEntry
            {
                Id = "Permanent_Debit_02",
                CardType = BankingAppDataTierConstants.CARD_TYPE_DEBIT,
                Name = "DotNet Founder",
                Cashback = 10,
                Commission = 5,
                IsActive = false,
                Image = string.Empty,
            },
            new PlasticTableEntry
            {
                Id = "Permanent_Debit_03",
                CardType = BankingAppDataTierConstants.CARD_TYPE_DEBIT,
                Name = "DotNet Gold",
                Cashback = 3,
                Commission = 10,
                IsActive = true,
                Image = string.Empty,
            },
            new PlasticTableEntry
            {
                Id = "Permanent_Credit_01",
                CardType = BankingAppDataTierConstants.CARD_TYPE_CREDIT,
                Name = "DotNet Classic",
                Commission = 4,
                IsActive = true,
                Image = string.Empty,
            },
            new PlasticTableEntry
            {
                Id = "Permanent_Credit_02",
                CardType = BankingAppDataTierConstants.CARD_TYPE_CREDIT,
                Name = "DotNet Prestige",
                Cashback = 3,
                Commission = 12,
                IsActive = true,
                Image = string.Empty,
            },
            new PlasticTableEntry
            {
                Id = "Permanent_PrePaid_01",
                CardType = BankingAppDataTierConstants.CARD_TYPE_PRE_PAID,
                Name = "DotNet Agile",
                Commission = 1,
                IsActive = true,
                Image = string.Empty,
            },
            new PlasticTableEntry
            {
                Id = "Permanent_Meal_01",
                CardType = BankingAppDataTierConstants.CARD_TYPE_MEAL,
                Name = "DotNet TableSlide",
                IsActive = true,
                Image = string.Empty,
            },
            new PlasticTableEntry
            {
                Id = "To_Edit_Debit_01",
                CardType = BankingAppDataTierConstants.CARD_TYPE_DEBIT,
                Name = "DotNet Basic",
                IsActive = true,
                Image = string.Empty,
            },
            new PlasticTableEntry
            {
                Id = "To_Delete_Debit_01",
                CardType = BankingAppDataTierConstants.CARD_TYPE_DEBIT,
                Name = "DotNet Basic",
                IsActive = true,
                Image = string.Empty,
            }
        ];

        public static void Mock(IDatabasePlasticsProvider dbProvider)
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


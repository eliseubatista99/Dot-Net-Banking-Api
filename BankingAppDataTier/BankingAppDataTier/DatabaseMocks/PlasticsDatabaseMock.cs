using BankingAppDataTier.Contracts.Constants;
using BankingAppDataTier.Contracts.Database;
using BankingAppDataTier.Contracts.Providers;

namespace BankingAppDataTier.Database
{
    public static class PlasticsDatabaseMock
    {
        public static void DefaultMock(IDatabasePlasticsProvider dbProvider, bool clearDatabase = false)
        {
            dbProvider.CreateTableIfNotExists();

            if (clearDatabase == true)
            {
                dbProvider.DeleteAll();
            }

            var elementsInDb = dbProvider.GetAll();


            //If the database already has entries, don't add anything
            if (elementsInDb.Count > 0)
            {
                return;
            }

            dbProvider.Add(
                           new PlasticTableEntry
                           {
                               Id = "DB_Basic",
                               CardType = BankingAppDataTierConstants.CARD_TYPE_DEBIT,
                               Name = "DotNet Basic",
                               IsActive = true,
                               Image = string.Empty,
                           }
                       );

            dbProvider.Add(
                new PlasticTableEntry
                {
                    Id = "DB_Founder",
                    CardType = BankingAppDataTierConstants.CARD_TYPE_DEBIT,
                    Name = "DotNet Founder",
                    Cashback = 10,
                    Commission = 5,
                    IsActive = false,
                    Image = string.Empty,
                }
            );

            dbProvider.Add(
                new PlasticTableEntry
                {
                    Id = "DB_Gold",
                    CardType = BankingAppDataTierConstants.CARD_TYPE_DEBIT,
                    Name = "DotNet Gold",
                    Cashback = 3,
                    Commission = 10,
                    IsActive = true,
                    Image = string.Empty,
                }
            );

            dbProvider.Add(
                new PlasticTableEntry
                {
                    Id = "CR_Classic",
                    CardType = BankingAppDataTierConstants.CARD_TYPE_CREDIT,
                    Name = "DotNet Classic",
                    Commission = 4,
                    IsActive = true,
                    Image = string.Empty,
                }
            );

            dbProvider.Add(
                new PlasticTableEntry
                {
                    Id = "CR_Prestige",
                    CardType = BankingAppDataTierConstants.CARD_TYPE_CREDIT,
                    Name = "DotNet Prestige",
                    Cashback = 3,
                    Commission = 12,
                    IsActive = true,
                    Image = string.Empty,
                }
            );

            dbProvider.Add(
                new PlasticTableEntry
                {
                    Id = "PP_Agile",
                    CardType = BankingAppDataTierConstants.CARD_TYPE_PRE_PAID,
                    Name = "DotNet Agile",
                    Commission = 1,
                    IsActive = true,
                    Image = string.Empty,
                }
            );

            dbProvider.Add(
                new PlasticTableEntry
                {
                    Id = "ME_TableSlide",
                    CardType = BankingAppDataTierConstants.CARD_TYPE_MEAL,
                    Name = "DotNet TableSlide",
                    IsActive = true,
                    Image = string.Empty,
                }
            );
        }

        public static void CustomMock(IDatabasePlasticsProvider dbProvider, List<PlasticTableEntry> mock)
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

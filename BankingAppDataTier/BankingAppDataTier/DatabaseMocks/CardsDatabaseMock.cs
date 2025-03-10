using BankingAppDataTier.Contracts.Constants;
using BankingAppDataTier.Contracts.Database;
using BankingAppDataTier.Contracts.Providers;

namespace BankingAppDataTier.Database
{
    public static class CardsDatabaseMock
    {
        public static void DefaultMock(IDatabaseCardsProvider dbProvider, bool clearDatabase = false)
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
               new CardsTableEntry
               {
                   Id = "ACJW000000_DB01",
                   Name = "DB_Basic",
                   RelatedAccountID = "ACJW000000",
                   PlasticId = "DB_Basic",
                   RequestDate = new DateTime(2025, 01, 01),
                   ActivationDate = new DateTime(2025, 01, 15),
                   ExpirationDate = new DateTime(2028, 01, 15),
               }
           );

            dbProvider.Add(
                new CardsTableEntry
                {
                    Id = "ACJW000000_PP01",
                    Name = "PP_Agile",
                    RelatedAccountID = "ACJW000000",
                    PlasticId = "PP_Agile",
                    Balance = 100,
                    RequestDate = new DateTime(2025, 01, 01),
                    ActivationDate = new DateTime(2025, 01, 15),
                    ExpirationDate = new DateTime(2028, 01, 15),
                }
            );

            dbProvider.Add(
                new CardsTableEntry
                {
                    Id = "ACJW000000_CR01",
                    Name = "CR_Prestige",
                    RelatedAccountID = "ACJW000000",
                    PlasticId = "CR_Prestige",
                    Balance = 100,
                    PaymentDay = 11,
                    RequestDate = new DateTime(2025, 01, 01),
                    ActivationDate = new DateTime(2025, 01, 15),
                    ExpirationDate = new DateTime(2028, 01, 15),
                }
            );

            dbProvider.Add(
                new CardsTableEntry
                {
                    Id = "ACJW000000_ME01",
                    Name = "ME_TableSlide",
                    RelatedAccountID = "ACJW000000",
                    PlasticId = "ME_TableSlide",
                    Balance = 100,
                    RequestDate = new DateTime(2025, 01, 01),
                    ActivationDate = new DateTime(2025, 01, 15),
                    ExpirationDate = new DateTime(2028, 01, 15),
                }
            );
        }

        public static void CustomMock(IDatabaseCardsProvider dbProvider, List<CardsTableEntry> mock)
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

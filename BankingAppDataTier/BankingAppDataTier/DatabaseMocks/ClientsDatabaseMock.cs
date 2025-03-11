using BankingAppDataTier.Contracts.Database;
using BankingAppDataTier.Contracts.Providers;

namespace BankingAppDataTier.Database
{
    public static class ClientsDatabaseMock
    {
        public static void DefaultMock(IDatabaseClientsProvider dbProvider, bool includeTestEntries = false)
        {
            dbProvider.CreateTableIfNotExists();

            var elementsInDb = dbProvider.GetAll();

            //If the database already has entries, don't add anything
            if (elementsInDb.Count > 0)
            {
                return;
            }

            dbProvider.Add(
                new ClientsTableEntry
                {
                    Id = "JW0000000",
                    Password = "password",
                    Name = "John",
                    Surname = "Wick",
                    BirthDate = new DateTime(1990, 02, 13),
                    VATNumber = "123123123",
                    PhoneNumber = "911111111",
                    Email = "jonh.wick@dotnetbanking.com"
                }
            );

            dbProvider.Add(
                new ClientsTableEntry
                {
                    Id = "JS0000000",
                    Password = "password",
                    Name = "Jack",
                    Surname = "Sparrow",
                    BirthDate = new DateTime(1995, 06, 21),
                    VATNumber = "111222333",
                    PhoneNumber = "911111112",
                    Email = "jacl.sparrow@dotnetbanking.com"
                }
            );

            if (includeTestEntries)
            {
                dbProvider.Add(
                    new ClientsTableEntry
                    {
                        Id = "DE0000000",
                        Password = "password",
                        Name = "Derp",
                        Surname = "Derpington",
                        BirthDate = new DateTime(1995, 06, 21),
                        VATNumber = "111222333",
                        PhoneNumber = "911111112",
                        Email = "derp@dotnetbanking.com"
                    }
                );
            }
        }

        public static void CustomMock(IDatabaseClientsProvider dbProvider, List<ClientsTableEntry> mock)
        {
            dbProvider.DeleteAll();

            dbProvider!.CreateTableIfNotExists();

            foreach(var entry in mock)
            {
                dbProvider.Add(entry);
            }
        }

    }
}

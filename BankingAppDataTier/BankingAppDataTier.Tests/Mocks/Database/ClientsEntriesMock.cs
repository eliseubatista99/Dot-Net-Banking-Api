using BankingAppDataTier.Contracts.Database;
using BankingAppDataTier.Contracts.Providers;

namespace BankingAppDataTier.Tests.Mocks.Database
{
    public static class ClientsEntriesMock
    {
        private static List<ClientsTableEntry> Entries =
        [
                new ClientsTableEntry
                {
                    Id = "Permanent_Client_01",
                    Password = "password",
                    Name = "John",
                    Surname = "Wick",
                    BirthDate = new DateTime(1990, 02, 13),
                    VATNumber = "123123123",
                    PhoneNumber = "911111111",
                    Email = "jonh.wick@dotnetbanking.com"
                },
                new ClientsTableEntry
                {
                    Id = "Permanent_Client_02",
                    Password = "xe",
                    Name = "Jack",
                    Surname = "Sparrow",
                    BirthDate = new DateTime(1995, 06, 21),
                    VATNumber = "111222333",
                    PhoneNumber = "911111112",
                    Email = "jacl.sparrow@dotnetbanking.com"
                },
                new ClientsTableEntry
                {
                    Id = "Permanent_Client_03",
                    Password = "password",
                    Name = "Jack",
                    Surname = "Sparrow",
                    BirthDate = new DateTime(1995, 06, 21),
                    VATNumber = "111222333",
                    PhoneNumber = "911111112",
                    Email = "jacl.sparrow@dotnetbanking.com"
                },
                new ClientsTableEntry
                {
                    Id = "To_Edit_Client_01",
                    Password = "password",
                    Name = "Derp",
                    Surname = "Derpington",
                    BirthDate = new DateTime(1995, 06, 21),
                    VATNumber = "111222333",
                    PhoneNumber = "911111112",
                    Email = "derp@dotnetbanking.com"
                },
                new ClientsTableEntry
                {
                    Id = "To_Delete_Client_01",
                    Password = "password",
                    Name = "Derp",
                    Surname = "Derpington",
                    BirthDate = new DateTime(1995, 06, 21),
                    VATNumber = "111222333",
                    PhoneNumber = "911111112",
                    Email = "derp@dotnetbanking.com"
                }
        ];
        public static void Mock(IDatabaseClientsProvider dbProvider)
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

using BankingAppDataTier.Contracts.Configs;
using BankingAppDataTier.Contracts.Constants;
using BankingAppDataTier.Contracts.Database;

namespace BankingAppDataTier.Database
{
    public class DatabaseInitializer
    {
        private IDatabaseProvider databaseProvider;

        public DatabaseInitializer(IDatabaseProvider dbProvider) 
        {
            this.databaseProvider = dbProvider;

            InitializeClients();
        }

        private void InitializeClients()
        {
            this.databaseProvider.CreateClientsTableIfNotExists();

            var clients = this.databaseProvider.GetAllClients();

            //If the database already has entries, don't add anything
            if(clients.Count > 0)
            {
                return;
            }

            this.databaseProvider.AddClient(
                new ClientsTableEntry {
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

            this.databaseProvider.AddClient(
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
        }
    }
}

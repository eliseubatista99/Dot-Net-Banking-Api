using BankingAppDataTier.Contracts.Constants;
using BankingAppDataTier.Contracts.Database;
using BankingAppDataTier.Contracts.Providers;

namespace BankingAppDataTier.Database
{
    public class DatabaseInitializer
    {
        private IServiceCollection serviceCollection;

        public DatabaseInitializer(IServiceCollection service) 
        {
            this.serviceCollection = service;

            InitializeClients();
            InitializeAccounts();
        }

        private void InitializeClients()
        {
            var dbProvider = this.serviceCollection.BuildServiceProvider().GetService<IDatabaseClientsProvider>();
            dbProvider!.CreateTableIfNotExists();

            var elementsInDb = dbProvider.GetAll();

            //If the database already has entries, don't add anything
            if(elementsInDb.Count > 0)
            {
                return;
            }

            dbProvider.Add(
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
        }

        private void InitializeAccounts()
        {
            var dbProvider = this.serviceCollection.BuildServiceProvider().GetService<IDatabaseAccountsProvider>();
            dbProvider!.CreateTableIfNotExists();

            var elementsInDb = dbProvider.GetAll();

            //If the database already has entries, don't add anything
            if (elementsInDb.Count > 0)
            {
                return;
            }

            dbProvider.Add(
                new AccountsTableEntry
                {
                    AccountId = "ACJW000000",
                    AccountType = AccountsTable.ACCOUNT_TYPE_CURRENT,
                    Balance = 1000,
                    Name = "Jonh Wick Current Account",
                    //Image = DefaultBase64Img.Content,
                },
                "JW0000000"
            );

            dbProvider.Add(
                new AccountsTableEntry
                {
                    AccountId = "ACJW000001",
                    AccountType = AccountsTable.ACCOUNT_TYPE_SAVINGS,
                    Balance = 2000,
                    Name = "Jonh Wick Savings Account",
                },
                "JW0000000"
            );

            dbProvider.Add(
                 new AccountsTableEntry
                 {
                     AccountId = "ACJW000002",
                     AccountType = AccountsTable.ACCOUNT_TYPE_INVESTMENTS,
                     Balance = 3000,
                     Name = "Jonh Wick Investments Account",
                     SourceAccountId = "ACJW000000",
                     Duration = 6,
                     Interest = 3.5M,
                 },
                "JW0000000"
            );        
        }
    }
}

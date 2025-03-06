using BankingAppDataTier.Contracts.Constants;
using BankingAppDataTier.Contracts.Constants.Database;
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
            InitializePlastics();
            InitializeCards();
            InitializeTransactions();
            InitializeLoanOffers();
            InitializeLoans();
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
                    OwnerCliendId = "JW0000000",
                    AccountType = BankingAppDataTierConstants.ACCOUNT_TYPE_CURRENT,
                    Balance = 1000,
                    Name = "Jonh Wick Current Account",
                    //Image = DefaultBase64Img.Content,
                }
            );

            dbProvider.Add(
                new AccountsTableEntry
                {
                    AccountId = "ACJW000001",
                    OwnerCliendId = "JW0000000",
                    AccountType = BankingAppDataTierConstants.ACCOUNT_TYPE_SAVINGS,
                    Balance = 2000,
                    Name = "Jonh Wick Savings Account",
                }
            );

            dbProvider.Add(
                 new AccountsTableEntry
                 {
                     AccountId = "ACJW000002",
                     OwnerCliendId = "JW0000000",
                     AccountType = BankingAppDataTierConstants.ACCOUNT_TYPE_INVESTMENTS,
                     Balance = 3000,
                     Name = "Jonh Wick Investments Account",
                     SourceAccountId = "ACJW000000",
                     Duration = 6,
                     Interest = 3.5M,
                 }
            );        
        }

        private void InitializePlastics()
        {
            var dbProvider = this.serviceCollection.BuildServiceProvider().GetService<IDatabasePlasticsProvider>();
            dbProvider!.CreateTableIfNotExists();

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
                }
            );

            dbProvider.Add(
                new PlasticTableEntry
                {
                    Id = "ME_TableSlide",
                    CardType = BankingAppDataTierConstants.CARD_TYPE_MEAL,
                    Name = "DotNet TableSlide",
                    IsActive = true,
                }
            );
        }

        private void InitializeCards()
        {
            var dbProvider = this.serviceCollection.BuildServiceProvider().GetService<IDatabaseCardsProvider>();
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
                    CardType = BankingAppDataTierConstants.CARD_TYPE_DEBIT,
                    RelatedAccountID = "ACJW000000",
                    PlasticId = "DB_Basic",
                    RequestDate = new DateTime(2025, 01,01),
                    ActivationDate = new DateTime(2025, 01,15),
                    ExpirationDate = new DateTime(2028, 01,15),
                },
                "ACJW000000"
            );

            dbProvider.Add(
                new CardsTableEntry
                {
                    Id = "ACJW000000_PP01",
                    CardType = BankingAppDataTierConstants.CARD_TYPE_PRE_PAID,
                    RelatedAccountID = "ACJW000000",
                    PlasticId = "PP_Agile",
                    Balance = 100,
                    RequestDate = new DateTime(2025, 01, 01),
                    ActivationDate = new DateTime(2025, 01, 15),
                    ExpirationDate = new DateTime(2028, 01, 15),
                },
                "ACJW000000"
            );

            dbProvider.Add(
                new CardsTableEntry
                {
                    Id = "ACJW000000_CR01",
                    CardType = BankingAppDataTierConstants.CARD_TYPE_CREDIT,
                    RelatedAccountID = "ACJW000000",
                    PlasticId = "CR_Prestige",
                    Balance = 100,
                    PaymentDay = 11,
                    RequestDate = new DateTime(2025, 01, 01),
                    ActivationDate = new DateTime(2025, 01, 15),
                    ExpirationDate = new DateTime(2028, 01, 15),
                },
                "ACJW000000"
            );

            dbProvider.Add(
                new CardsTableEntry
                {
                    Id = "ACJW000000_ME01",
                    CardType = BankingAppDataTierConstants.CARD_TYPE_MEAL,
                    RelatedAccountID = "ACJW000000",
                    PlasticId = "ME_TableSlide",
                    Balance = 100,
                    RequestDate = new DateTime(2025, 01, 01),
                    ActivationDate = new DateTime(2025, 01, 15),
                    ExpirationDate = new DateTime(2028, 01, 15),
                },
                "ACJW000000"
            );

        }

        private void InitializeTransactions()
        {
            var dbProvider = this.serviceCollection.BuildServiceProvider().GetService<IDatabaseTransactionsProvider>();
            dbProvider!.CreateTableIfNotExists();

            var elementsInDb = dbProvider.GetAll();

            //If the database already has entries, don't add anything
            if (elementsInDb.Count > 0)
            {
                return;
            }

            dbProvider.Add(
                new TransactionTableEntry
                {
                    Id = "T001",
                    TransactionDate = new DateTime(2025, 02, 10),
                    Description = "Test transfer",
                    Amount = 100.35M,
                    Fees = 1.25M,
                    Urgent = true,
                    SourceAccount = "ACJW000000",
                    DestinationName = "Eletricity Company",
                }
            );

            dbProvider.Add(
                new TransactionTableEntry
                {
                    Id = "T002",
                    TransactionDate = new DateTime(2025, 02, 10),
                    Description = "Test transfer from card",
                    Amount = 100.35M,
                    Fees = 1.25M,
                    Urgent = true,
                    SourceCard = "ACJW000000_DB01",
                    DestinationName = "Water Company",
                }
            );

        }

        private void InitializeLoanOffers()
        {
            var dbProvider = this.serviceCollection.BuildServiceProvider().GetService<IDatabaseLoanOfferProvider>();
            dbProvider!.CreateTableIfNotExists();

            var elementsInDb = dbProvider.GetAll();

            //If the database already has entries, don't add anything
            if (elementsInDb.Count > 0)
            {
                return;
            }

            dbProvider.Add(
                new LoanOfferTableEntry
                {
                    Id = "LO01",
                    LoanType = "AU",
                    MaxEffort = 30,
                    Interest = 7.0M,
                    IsActive = true,
                }
            );

            dbProvider.Add(
               new LoanOfferTableEntry
               {
                   Id = "LO02",
                   LoanType = "MO",
                   MaxEffort = 40,
                   Interest = 3.0M,
                   IsActive = true,
               }
            );

            dbProvider.Add(
               new LoanOfferTableEntry
               {
                   Id = "LO03",
                   LoanType = "PE",
                   MaxEffort = 25,
                   Interest = 10.0M,
                   IsActive = true,
               }
            );

            dbProvider.Add(
                new LoanOfferTableEntry
                {
                    Id = "LO04",
                    LoanType = "AU",
                    MaxEffort = 20,
                    Interest = 3.0M,
                    IsActive = false,
                }
            );


        }

        private void InitializeLoans()
        {
            var dbProvider = this.serviceCollection.BuildServiceProvider().GetService<IDatabaseLoansProvider>();
            dbProvider!.CreateTableIfNotExists();

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
                    StartDate = new DateTime(2025, 02, 03),
                    RelatedOffer = "LO01",
                    Duration = 24,
                    Amount = 10000.0M,
                }
            );
        }

    }
}

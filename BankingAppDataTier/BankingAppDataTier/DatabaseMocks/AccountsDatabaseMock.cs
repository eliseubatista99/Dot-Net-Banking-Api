using BankingAppDataTier.Contracts.Constants;
using BankingAppDataTier.Contracts.Database;
using BankingAppDataTier.Contracts.Providers;

namespace BankingAppDataTier.Database
{
    public static class AccountsDatabaseMock
    {
        public static void DefaultMock(IDatabaseAccountsProvider dbProvider, bool clearDatabase = false)
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

        public static void CustomMock(IDatabaseAccountsProvider dbProvider, List<AccountsTableEntry> mock)
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

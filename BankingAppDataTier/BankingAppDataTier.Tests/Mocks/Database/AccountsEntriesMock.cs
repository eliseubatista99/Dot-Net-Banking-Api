using BankingAppDataTier.Contracts.Constants;
using BankingAppDataTier.Contracts.Database;
using BankingAppDataTier.Contracts.Providers;

namespace BankingAppDataTier.Tests.Mocks.Database
{
    public static class AccountsEntriesMock
    {
        private static List<AccountsTableEntry> Entries =
        [
            new AccountsTableEntry
            {
                AccountId = "Permanent_Current_01",
                OwnerCliendId = "Permanent_Client_01",
                AccountType = BankingAppDataTierConstants.ACCOUNT_TYPE_CURRENT,
                Balance = 1000,
                Name = "Jonh Wick Current Account",
                //Image = DefaultBase64Img.Content,
            },
            new AccountsTableEntry
            {
                AccountId = "Permanent_Current_02",
                OwnerCliendId = "Permanent_Client_01",
                AccountType = BankingAppDataTierConstants.ACCOUNT_TYPE_CURRENT,
                Balance = 1000,
                Name = "Jonh Wick Current Account",
                //Image = DefaultBase64Img.Content,
            },
            new AccountsTableEntry
            {
                AccountId = "Permanent_Savings_01",
                OwnerCliendId = "Permanent_Client_01",
                AccountType = BankingAppDataTierConstants.ACCOUNT_TYPE_SAVINGS,
                Balance = 2000,
                Name = "Jonh Wick Savings Account",
            },
            new AccountsTableEntry
            {
                AccountId = "Permanent_Investements_01",
                OwnerCliendId = "Permanent_Client_01",
                AccountType = BankingAppDataTierConstants.ACCOUNT_TYPE_INVESTMENTS,
                Balance = 3000,
                Name = "Jonh Wick Investments Account",
                SourceAccountId = "Permanent_Current_01",
                Duration = 6,
                Interest = 3.5M,
            },
            new AccountsTableEntry
            {
                AccountId = "To_Edit_Current_01",
                OwnerCliendId = "Permanent_Client_01",
                AccountType = BankingAppDataTierConstants.ACCOUNT_TYPE_CURRENT,
                Balance = 2000,
                Name = "Jonh Wick Current Account For Tests",
                //Image = DefaultBase64Img.Content,
            },

            new AccountsTableEntry
            {
                AccountId = "To_Edit_Savings_01",
                OwnerCliendId = "Permanent_Client_01",
                AccountType = BankingAppDataTierConstants.ACCOUNT_TYPE_SAVINGS,
                Balance = 2000,
                Name = "Jonh Wick Savings Account For Tests",
            },
            new AccountsTableEntry
            {
                AccountId = "To_Edit_Investements_01",
                OwnerCliendId = "Permanent_Client_01",
                AccountType = BankingAppDataTierConstants.ACCOUNT_TYPE_INVESTMENTS,
                Balance = 3000,
                Name = "Jonh Wick Investments Account For Tests",
                SourceAccountId = "ACJW000000",
                Duration = 6,
                Interest = 3.5M,
            },
            new AccountsTableEntry
            {
                AccountId = "To_Delete_Current_01",
                OwnerCliendId = "Permanent_Client_01",
                AccountType = BankingAppDataTierConstants.ACCOUNT_TYPE_CURRENT,
                Balance = 2000,
                Name = "Jonh Wick Current Account For Tests",
                //Image = DefaultBase64Img.Content,
            }
        ];

        public static void Mock(IDatabaseAccountsProvider dbProvider)
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

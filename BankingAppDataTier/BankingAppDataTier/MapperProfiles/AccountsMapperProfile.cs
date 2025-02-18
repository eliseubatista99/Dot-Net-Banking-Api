using BankingAppDataTier.Contracts.Constants;
using BankingAppDataTier.Contracts.Database;
using BankingAppDataTier.Contracts.Dtos.Entitites;
using BankingAppDataTier.Contracts.Enums;
using Microsoft.Data.SqlClient;

namespace BankingAppDataTier.MapperProfiles
{
    public static class AccountsMapperProfile
    {
        public static AccountsTableEntry MapSqlDataToAccountsTableEntry(SqlDataReader sqlReader)
        {
            var accountImage = (sqlReader[AccountsTable.COLUMN_IMAGE]).ToString();

            var sourceAccountId = sqlReader[InvestmentsAccountBridgeTable.COLUMN_SOURCE_ACCOUNT_ID];
            var duration = sqlReader[InvestmentsAccountBridgeTable.COLUMN_DURATION];
            var interest = sqlReader[InvestmentsAccountBridgeTable.COLUMN_INTEREST];

            return new AccountsTableEntry
            {
                AccountId = sqlReader[AccountsTable.COLUMN_ID].ToString()!,
                AccountType = sqlReader[AccountsTable.COLUMN_TYPE].ToString()!,
                Balance = Convert.ToDecimal(sqlReader[AccountsTable.COLUMN_BALANCE].ToString())!,
                Name = sqlReader[AccountsTable.COLUMN_NAME].ToString()!,
                Image = accountImage,
                SourceAccountId = sourceAccountId is System.DBNull? null : sourceAccountId.ToString(),
                Duration = duration is System.DBNull ? null : Convert.ToInt16(duration),
                Interest = interest is System.DBNull ? null :Convert.ToDecimal(interest),
            };
        }

        public static AccountType MapStringAccountTypeToAccountTypeEnum(string value)
        {
            return value switch
            {
                AccountsTable.ACCOUNT_TYPE_CURRENT => AccountType.Current,
                AccountsTable.ACCOUNT_TYPE_SAVINGS => AccountType.Savings,
                AccountsTable.ACCOUNT_TYPE_INVESTMENTS => AccountType.Investments,
                _ => AccountType.None,
            };
        }

        public static string MaAccountTypeEnumToStringAccountType(AccountType value)
        {
            return value switch
            {
                AccountType.Current => AccountsTable.ACCOUNT_TYPE_CURRENT,
                AccountType.Savings => AccountsTable.ACCOUNT_TYPE_SAVINGS,
                AccountType.Investments => AccountsTable.ACCOUNT_TYPE_INVESTMENTS,
                _ => ""
            };
        }

        public static AccountDto MapAccountsTableEntryToAccountDto(AccountsTableEntry tableEntry)
        {
            return new AccountDto
            {
                Id = tableEntry.AccountId,
                AccountType = MapStringAccountTypeToAccountTypeEnum(tableEntry.AccountType),
                Balance = tableEntry.Balance,
                Name = tableEntry.Name,
                Image = tableEntry.Image,
                SourceAccountId = tableEntry.SourceAccountId,
                Duration = tableEntry.Duration,
                Interest = tableEntry.Interest
            };
        }

        public static AccountsTableEntry MapAccountDtoToAccountsTableEntry(AccountDto dto)
        {
            return new AccountsTableEntry
            {
                AccountId = dto.Id,
                AccountType = MaAccountTypeEnumToStringAccountType(dto.AccountType),
                Balance = dto.Balance,
                Name = dto.Name,
                Image = dto.Image,
                SourceAccountId = dto.SourceAccountId,
                Duration = dto.Duration,
                Interest = dto.Interest
            };
        }
    }
}

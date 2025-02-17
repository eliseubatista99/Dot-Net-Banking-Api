using BankingAppDataTier.Contracts.Constants;
using BankingAppDataTier.Contracts.Database;
using BankingAppDataTier.Contracts.Dtos;
using Microsoft.Data.SqlClient;

namespace BankingAppDataTier.MapperProfiles
{
    public static class AccountsMapperProfile
    {
        public static AccountsTableEntry MapSqlDataToAccountsTableEntry(SqlDataReader sqlReader)
        {
            var accountImage = (sqlReader[AccountsTable.COLUMN_IMAGE]).ToString();

            return new AccountsTableEntry
            {
                AccountId = sqlReader[AccountsTable.COLUMN_ID].ToString()!,
                AccountType = sqlReader[AccountsTable.COLUMN_TYPE].ToString()!,
                Balance = Convert.ToDecimal(sqlReader[AccountsTable.COLUMN_BALANCE].ToString())!,
                Name = sqlReader[AccountsTable.COLUMN_NAME].ToString()!,
                Image = accountImage,
            };
        }

        public static AccountType MapStringAccountTypeToAccountTypeEnum(string value)
        {
            return value switch
            {
                "CU" => AccountType.Current,
                "SA" => AccountType.Savings,
                "IN" => AccountType.Investments,
                _ => AccountType.None,
            };
        }

        public static string MaAccountTypeEnumToStringAccountType(AccountType value)
        {
            return value switch
            {
                AccountType.Current => "CU",
                AccountType.Savings => "SA",
                AccountType.Investments => "IN",
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
                Image = tableEntry.Image
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
            };
        }
    }
}

using BankingAppDataTier.Contracts.Constants.Database;
using BankingAppDataTier.Contracts.Database;
using BankingAppDataTier.Contracts.Dtos.Entitites;
using Npgsql;

namespace BankingAppDataTier.MapperProfiles
{
    public static class AccountsMapperProfile
    {
        public static AccountsTableEntry MapSqlDataToTableEntry(NpgsqlDataReader sqlReader)
        {
            var accountImage = (sqlReader[AccountsTable.COLUMN_IMAGE]).ToString();

            var sourceAccountId = sqlReader[AccountsTable.COLUMN_SOURCE_ACCOUNT_ID];
            var duration = sqlReader[AccountsTable.COLUMN_DURATION];
            var interest = sqlReader[AccountsTable.COLUMN_INTEREST];

            return new AccountsTableEntry
            {
                AccountId = sqlReader[AccountsTable.COLUMN_ID].ToString()!,
                OwnerCliendId = sqlReader[AccountsTable.COLUMN_OWNER_CLIENT_ID].ToString()!,
                AccountType = sqlReader[AccountsTable.COLUMN_TYPE].ToString()!,
                Balance = Convert.ToDecimal(sqlReader[AccountsTable.COLUMN_BALANCE].ToString())!,
                Name = sqlReader[AccountsTable.COLUMN_NAME].ToString()!,
                Image = accountImage,
                SourceAccountId = sourceAccountId is System.DBNull? null : sourceAccountId.ToString(),
                Duration = duration is System.DBNull ? null : Convert.ToInt16(duration),
                Interest = interest is System.DBNull ? null :Convert.ToDecimal(interest),
            };
        }

        public static AccountDto MapTableEntryToDto(AccountsTableEntry tableEntry)
        {
            return new AccountDto
            {
                Id = tableEntry.AccountId,
                OwnerCliendId = tableEntry.OwnerCliendId,
                AccountType = EnumsMapperProfile.MapAccountTypeFromString(tableEntry.AccountType),
                Balance = tableEntry.Balance,
                Name = tableEntry.Name,
                Image = tableEntry.Image,
                SourceAccountId = tableEntry.SourceAccountId,
                Duration = tableEntry.Duration,
                Interest = tableEntry.Interest
            };
        }

        public static AccountsTableEntry MapDtoToTableEntry(AccountDto dto)
        {
            return new AccountsTableEntry
            {
                AccountId = dto.Id,
                OwnerCliendId= dto.OwnerCliendId,
                AccountType = EnumsMapperProfile.MapAccountTypeToString(dto.AccountType),
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

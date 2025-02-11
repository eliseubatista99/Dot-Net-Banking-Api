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
            var accountImage = (sqlReader[ClientsTable.COLUMN_BIRTH_DATE]).ToString();

            return new AccountsTableEntry
            {
                AccountId = sqlReader[AccountsTable.COLUMN_ID].ToString()!,
                AccountType = sqlReader[AccountsTable.COLUMN_TYPE].ToString()!,
                Balance = Convert.ToDecimal(sqlReader[AccountsTable.COLUMN_BALANCE].ToString())!,
                Name = sqlReader[AccountsTable.COLUMN_NAME].ToString()!,
                Image = accountImage,
            };
        }

        public static ClientDto MapClientTableEntryToClientDto(ClientsTableEntry tableEntry)
        {
            return new ClientDto
            {
                Id = tableEntry.Id,
                Name = tableEntry.Name,
                Surname = tableEntry.Surname,
                BirthDate = tableEntry.BirthDate,
                VATNumber = tableEntry.VATNumber,
                PhoneNumber = tableEntry.PhoneNumber,
                Email = tableEntry.Email,
            };
        }

        public static ClientsTableEntry MapClientDtoToClientTableEntry(ClientDto dto)
        {
            return new ClientsTableEntry
            {
                Id = dto.Id,
                Password = string.Empty,
                Name = dto.Name,
                Surname = dto.Surname,
                BirthDate = dto.BirthDate,
                VATNumber = dto.VATNumber,
                PhoneNumber = dto.PhoneNumber,
                Email = dto.Email,
            };
        }
    }
}

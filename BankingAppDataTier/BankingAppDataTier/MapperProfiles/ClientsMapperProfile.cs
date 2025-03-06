using BankingAppDataTier.Contracts.Constants.Database;
using BankingAppDataTier.Contracts.Database;
using BankingAppDataTier.Contracts.Dtos.Entitites;
using Npgsql;

namespace BankingAppDataTier.MapperProfiles
{
    public static class ClientsMapperProfile
    {
        public static ClientsTableEntry MapSqlDataToTableEntry(NpgsqlDataReader sqlReader)
        {
            return new ClientsTableEntry
            {
                Id = sqlReader[ClientsTable.COLUMN_ID].ToString()!,
                Password = sqlReader[ClientsTable.COLUMN_PASSWORD].ToString()!,
                Name = sqlReader[ClientsTable.COLUMN_NAME].ToString()!,
                Surname = sqlReader[ClientsTable.COLUMN_SURNAME].ToString()!,
                BirthDate = Convert.ToDateTime(sqlReader[ClientsTable.COLUMN_BIRTH_DATE]),
                VATNumber = sqlReader[ClientsTable.COLUMN_VAT_NUMBER].ToString()!,
                PhoneNumber = sqlReader[ClientsTable.COLUMN_PHONE_NUMBER].ToString()!,
                Email = sqlReader[ClientsTable.COLUMN_EMAIL].ToString()!,
            };
        }

        public static ClientDto MapTableEntryToDto(ClientsTableEntry tableEntry)
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

        public static ClientsTableEntry MapDtoToTableEntry(ClientDto dto)
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

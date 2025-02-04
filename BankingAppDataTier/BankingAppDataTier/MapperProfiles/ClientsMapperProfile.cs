using BankingAppDataTier.Contracts.Constants;
using BankingAppDataTier.Contracts.Database;
using BankingAppDataTier.Contracts.Dtos;
using Microsoft.Data.SqlClient;

namespace BankingAppDataTier.MapperProfiles
{
    public static class ClientsMapperProfile
    {
        public static ClientsTableEntry MapSqlDataToClientTableEntry(SqlDataReader sqlReader)
        {
            return new ClientsTableEntry
            {
                Id = sqlReader[ClientsTable.COLUMN_ID].ToString()!,
                Password = sqlReader[ClientsTable.COLUMN_PASSWORD].ToString()!,
                Name = sqlReader[ClientsTable.COLUMN_ID].ToString()!,
                Surname = sqlReader[ClientsTable.COLUMN_ID].ToString()!,
                BirthDate = Convert.ToDateTime(sqlReader[ClientsTable.COLUMN_ID]),
                VATNumber = sqlReader[ClientsTable.COLUMN_ID].ToString()!,
                PhoneNumber = sqlReader[ClientsTable.COLUMN_ID].ToString()!,
                Email = sqlReader[ClientsTable.COLUMN_ID].ToString()!,
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

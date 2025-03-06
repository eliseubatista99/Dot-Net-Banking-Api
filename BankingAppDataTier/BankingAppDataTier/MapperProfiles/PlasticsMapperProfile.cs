using BankingAppDataTier.Contracts.Constants;
using BankingAppDataTier.Contracts.Constants.Database;
using BankingAppDataTier.Contracts.Database;
using BankingAppDataTier.Contracts.Dtos.Entitites;
using BankingAppDataTier.Contracts.Enums;
using Microsoft.Data.SqlClient;
using Npgsql;

namespace BankingAppDataTier.MapperProfiles
{
    public static class PlasticsMapperProfile
    {
        public static PlasticTableEntry MapSqlDataToTableEntry(NpgsqlDataReader sqlReader)
        {
            var image = (sqlReader[PlasticsTable.COLUMN_IMAGE]).ToString();
            var cashback = sqlReader[PlasticsTable.COLUMN_CASHBACK];
            var commission = sqlReader[PlasticsTable.COLUMN_COMISSION];

            return new PlasticTableEntry
            {
                Id = sqlReader[PlasticsTable.COLUMN_ID].ToString()!,
                CardType = sqlReader[PlasticsTable.COLUMN_TYPE].ToString()!,
                Name = sqlReader[PlasticsTable.COLUMN_NAME].ToString()!,
                Image = image,
                Cashback = cashback is System.DBNull ? null : Convert.ToDecimal(cashback),
                Commission = commission is System.DBNull ? null : Convert.ToDecimal(commission),
            };
        }

        public static PlasticDto MapTableEntryToDto(PlasticTableEntry tableEntry)
        {
            return new PlasticDto
            {
                Id = tableEntry.Id,
                CardType = EnumsMapperProfile.MapCardTypeFromString(tableEntry.CardType),
                Name = tableEntry.Name,
                Cashback = tableEntry.Cashback,
                Commission = tableEntry.Commission,
                Image = tableEntry.Image
            };
        }

        public static PlasticTableEntry MapDtoToTableEntry(PlasticDto dto)
        {
            return new PlasticTableEntry
            {
                Id = dto.Id,
                CardType = EnumsMapperProfile.MapCardTypeEnumToStringCardType(dto.CardType),
                Name = dto.Name,
                Cashback = dto.Cashback,
                Commission = dto.Commission,
                Image = dto.Image
            };
        }
    }
}

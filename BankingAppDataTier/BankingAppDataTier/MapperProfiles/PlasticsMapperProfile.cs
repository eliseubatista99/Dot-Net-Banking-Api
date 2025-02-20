using BankingAppDataTier.Contracts.Constants;
using BankingAppDataTier.Contracts.Constants.Database;
using BankingAppDataTier.Contracts.Database;
using BankingAppDataTier.Contracts.Dtos.Entitites;
using BankingAppDataTier.Contracts.Enums;
using Microsoft.Data.SqlClient;

namespace BankingAppDataTier.MapperProfiles
{
    public static class PlasticsMapperProfile
    {
        public static PlasticTableEntry MapSqlDataToTableEntry(SqlDataReader sqlReader)
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
                CardType = MapStringCardTypeToCardTypeEnum(tableEntry.CardType),
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
                CardType = MapCardTypeEnumToStringCardType(dto.CardType),
                Name = dto.Name,
                Cashback = dto.Cashback,
                Commission = dto.Commission,
                Image = dto.Image
            };
        }

        public static string MapCardTypeEnumToStringCardType(CardType value)
        {
            return value switch
            {
                CardType.Debit => BankingAppDataTierConstants.CARD_TYPE_DEBIT,
                CardType.Credit => BankingAppDataTierConstants.CARD_TYPE_CREDIT,
                CardType.PrePaid => BankingAppDataTierConstants.CARD_TYPE_PRE_PAID,
                CardType.Meal => BankingAppDataTierConstants.CARD_TYPE_MEAL,
                _ => ""
            };
        }

        public static CardType MapStringCardTypeToCardTypeEnum(string value)
        {
            return value switch
            {
                BankingAppDataTierConstants.CARD_TYPE_DEBIT => CardType.Debit,
                BankingAppDataTierConstants.CARD_TYPE_CREDIT => CardType.Credit,
                BankingAppDataTierConstants.CARD_TYPE_PRE_PAID => CardType.PrePaid,
                BankingAppDataTierConstants.CARD_TYPE_MEAL => CardType.Meal,
                _ => CardType.None,
            };
        }
    }
}

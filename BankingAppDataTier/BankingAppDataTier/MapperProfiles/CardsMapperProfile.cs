﻿using BankingAppDataTier.Contracts.Constants;
using BankingAppDataTier.Contracts.Constants.Database;
using BankingAppDataTier.Contracts.Database;
using BankingAppDataTier.Contracts.Dtos.Entitites;
using BankingAppDataTier.Contracts.Enums;
using Microsoft.Data.SqlClient;
using Npgsql;

namespace BankingAppDataTier.MapperProfiles
{
    public static class CardsMapperProfile
    {
        public static CardsTableEntry MapSqlDataToTableEntry(NpgsqlDataReader sqlReader)
        {
            var balance = sqlReader[CardsTable.COLUMN_BALANCE];
            var paymentDate = sqlReader[CardsTable.COLUMN_PAYMENT_DAY];
            var activationDate = sqlReader[CardsTable.COLUMN_ACTIVATION_DATE];

            return new CardsTableEntry
            {
                Id = sqlReader[CardsTable.COLUMN_ID].ToString()!,
                CardType = sqlReader[CardsTable.COLUMN_TYPE].ToString()!,
                RelatedAccountID = sqlReader[CardsTable.COLUMN_RELATED_ACCOUNT_ID].ToString()!,
                PlasticId = sqlReader[CardsTable.COLUMN_PLASTIC_ID].ToString()!,
                RequestDate = Convert.ToDateTime(sqlReader[CardsTable.COLUMN_REQUEST_DATE].ToString())!,
                ActivationDate = activationDate is System.DBNull ? null : Convert.ToDateTime(activationDate),
                ExpirationDate = Convert.ToDateTime(sqlReader[CardsTable.COLUMN_EXPIRATION_DATE].ToString())!,
                Balance = balance is System.DBNull ? null : Convert.ToDecimal(balance),
                PaymentDay = paymentDate is System.DBNull ? null : Convert.ToInt16(paymentDate),
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

        public static CardDto MapTableEntryToDto(CardsTableEntry tableEntry)
        {
            return new CardDto
            {
                Id = tableEntry.Id,
                CardType = MapStringCardTypeToCardTypeEnum(tableEntry.CardType),
                RelatedAccountID = tableEntry.RelatedAccountID,
                PlasticId = tableEntry.PlasticId,
                Balance = tableEntry.Balance,
                RequestDate = tableEntry.RequestDate,
                ActivationDate = tableEntry.ActivationDate,
                ExpirationDate = tableEntry.ExpirationDate,
                PaymentDay = tableEntry.PaymentDay,
            };
        }

        public static CardsTableEntry MapDtoToTableEntry(CardDto dto)
        {
            return new CardsTableEntry
            {
                Id = dto.Id,
                CardType = MapCardTypeEnumToStringCardType(dto.CardType),
                RelatedAccountID = dto.RelatedAccountID,
                PlasticId = dto.PlasticId,
                Balance = dto.Balance,
                RequestDate = dto.RequestDate,
                ActivationDate = dto.ActivationDate,
                ExpirationDate = dto.ExpirationDate,
                PaymentDay = dto.PaymentDay,
            };
        }
    }
}

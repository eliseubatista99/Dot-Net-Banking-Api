using BankingAppDataTier.Contracts.Constants.Database;
using BankingAppDataTier.Contracts.Database;
using BankingAppDataTier.Contracts.Dtos.Entitites;
using Npgsql;
using System.Data;

namespace BankingAppDataTier.MapperProfiles
{
    public static class TransactionsMapperProfile
    {
        public static TransactionTableEntry MapSqlDataToTableEntry(NpgsqlDataReader sqlReader)
        {
            var description = sqlReader[TransactionsTable.COLUMN_DESCRIPTION];
            var fees = sqlReader[TransactionsTable.COLUMN_FEES];
            var sourceAccount = sqlReader[TransactionsTable.COLUMN_SOURCE_ACCOUNT];
            var destinationAccount = sqlReader[TransactionsTable.COLUMN_DESTINATION_ACCOUNT];
            var sourceCard = sqlReader[TransactionsTable.COLUMN_SOURCE_CARD];


            return new TransactionTableEntry
            {
                Id = sqlReader[TransactionsTable.COLUMN_ID].ToString()!,
                TransactionDate = Convert.ToDateTime(sqlReader[TransactionsTable.COLUMN_TRANSACTION_DATE].ToString())!,
                Amount = Convert.ToDecimal(sqlReader[TransactionsTable.COLUMN_AMOUNT].ToString())!,
                Urgent = Convert.ToBoolean(sqlReader[TransactionsTable.COLUMN_URGENT].ToString())!,
                DestinationName = sqlReader[TransactionsTable.COLUMN_DESTINATION_NAME].ToString()!,
                Description = description is System.DBNull ? null : description.ToString(),
                Fees = fees is System.DBNull ? null : Convert.ToDecimal(fees),
                SourceAccount = sourceAccount is System.DBNull ? null : sourceAccount.ToString(),
                DestinationAccount = fees is System.DBNull ? null : destinationAccount.ToString(),
                SourceCard = fees is System.DBNull ? null : sourceCard.ToString(),
            };
        }

        public static TransactionDto MapTableEntryToDto(TransactionTableEntry tableEntry)
        {
            return new TransactionDto
            {
                Id = tableEntry.Id,
                Role = Contracts.Enums.TransactionRole.None,
                TransactionDate = tableEntry.TransactionDate,
                Amount = tableEntry.Amount,
                Urgent = tableEntry.Urgent,
                DestinationName = tableEntry.DestinationName,
                Description = tableEntry.Description,
                Fees = tableEntry.Fees,
                SourceAccount = tableEntry.SourceAccount,
                DestinationAccount = tableEntry.DestinationAccount,
                SourceCard = tableEntry.SourceCard,
            };
        }

        public static TransactionTableEntry MapDtoToTableEntry(TransactionDto dto)
        {
            return new TransactionTableEntry
            {
                Id = dto.Id,
                TransactionDate = dto.TransactionDate,
                Amount = dto.Amount,
                Urgent = dto.Urgent,
                DestinationName = dto.DestinationName,
                Description = dto.Description,
                Fees = dto.Fees,
                SourceAccount = dto.SourceAccount,
                DestinationAccount = dto.DestinationAccount,
                SourceCard = dto.SourceCard,
            };
        }   
    }
}

using BankingAppDataTier.Contracts.Constants;
using BankingAppDataTier.Contracts.Constants.Database;
using BankingAppDataTier.Contracts.Database;
using BankingAppDataTier.Contracts.Dtos.Entitites;
using BankingAppDataTier.Contracts.Enums;
using Microsoft.Data.SqlClient;
using Npgsql;

namespace BankingAppDataTier.MapperProfiles
{
    public static class LoansMapperProfile
    {
        public static LoanTableEntry MapSqlDataToTableEntry(NpgsqlDataReader sqlReader)
        {
            return new LoanTableEntry
            {
                Id = sqlReader[LoansTable.COLUMN_ID].ToString()!,
                StartDate = Convert.ToDateTime(sqlReader[LoansTable.COLUMN_START_DATE].ToString())!,
                RelatedOffer = sqlReader[LoansTable.COLUMN_RELATED_OFFER].ToString()!,
                Duration = Convert.ToInt16(sqlReader[LoansTable.COLUMN_DURATION].ToString())!,
                Amount = Convert.ToDecimal(sqlReader[LoansTable.COLUMN_AMOUNT].ToString())!,
            };
        }

        public static LoanDto MapTableEntryToDto(LoanTableEntry tableEntry)
        {
            return new LoanDto
            {
                Id = tableEntry.Id,
                StartDate = tableEntry.StartDate,
                RelatedOffer = tableEntry.RelatedOffer,
                Duration = tableEntry.Duration,
                Amount = tableEntry.Amount,
            };
        }

        public static LoanTableEntry MapDtoToTableEntry(LoanDto dto)
        {
            return new LoanTableEntry
            {
                Id = dto.Id,
                StartDate = dto.StartDate,
                RelatedOffer = dto.RelatedOffer,
                Duration = dto.Duration,
                Amount = dto.Amount,
            };
        }
    }
}

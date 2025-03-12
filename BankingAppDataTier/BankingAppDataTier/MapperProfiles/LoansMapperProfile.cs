using BankingAppDataTier.Contracts.Constants.Database;
using BankingAppDataTier.Contracts.Database;
using BankingAppDataTier.Contracts.Dtos.Entitites;
using BankingAppDataTier.Contracts.Enums;
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
                Name = sqlReader[LoansTable.TABLE_NAME].ToString()!,
                StartDate = Convert.ToDateTime(sqlReader[LoansTable.COLUMN_START_DATE].ToString())!,
                RelatedAccount = sqlReader[LoansTable.COLUMN_RELATED_ACCOUNT].ToString()!,
                RelatedOffer = sqlReader[LoansTable.COLUMN_RELATED_OFFER].ToString()!,
                Duration = Convert.ToInt16(sqlReader[LoansTable.COLUMN_DURATION].ToString())!,
                ContractedAmount = Convert.ToDecimal(sqlReader[LoansTable.COLUMN_CONTRACTED_AMOUNT].ToString())!,
                PaidAmount = Convert.ToDecimal(sqlReader[LoansTable.COLUMN_PAID_AMOUNT].ToString())!,
            };
        }

        public static LoanDto MapTableEntryToDto(LoanTableEntry tableEntry)
        {
            return new LoanDto
            {
                Id = tableEntry.Id,
                Name = tableEntry.Name,
                StartDate = tableEntry.StartDate,
                RelatedAccount = tableEntry.RelatedAccount,
                RelatedOffer = tableEntry.RelatedOffer,
                Duration = tableEntry.Duration,
                ContractedAmount = tableEntry.ContractedAmount,
                PaidAmount = tableEntry.PaidAmount,
                LoanType = LoanType.None,
                Interest = 0,
            };
        }

        public static LoanTableEntry MapDtoToTableEntry(LoanDto dto)
        {
            return new LoanTableEntry
            {
                Id = dto.Id,
                Name = dto.Name,
                StartDate = dto.StartDate,
                RelatedAccount = dto.RelatedAccount,
                RelatedOffer = dto.RelatedOffer,
                Duration = dto.Duration,
                ContractedAmount = dto.ContractedAmount,
                PaidAmount = dto.PaidAmount,
            };
        }
    }
}

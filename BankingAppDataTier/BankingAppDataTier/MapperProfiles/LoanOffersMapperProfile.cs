using BankingAppDataTier.Contracts.Constants.Database;
using BankingAppDataTier.Contracts.Database;
using BankingAppDataTier.Contracts.Dtos.Entitites;
using Npgsql;

namespace BankingAppDataTier.MapperProfiles
{
    public static class LoanOffersMapperProfile
    {
        public static LoanOfferTableEntry MapSqlDataToTableEntry(NpgsqlDataReader sqlReader)
        {
            return new LoanOfferTableEntry
            {
                Id = sqlReader[LoanOffersTable.COLUMN_ID].ToString()!,
                LoanType = sqlReader[LoanOffersTable.COLUMN_TYPE].ToString()!,
                MaxEffort = Convert.ToInt16(sqlReader[LoanOffersTable.COLUMN_MAX_EFFORT].ToString())!,
                Interest = Convert.ToDecimal(sqlReader[LoanOffersTable.COLUMN_INTEREST].ToString())!,
                IsActive = Convert.ToBoolean(sqlReader[LoanOffersTable.COLUMN_IS_ACTIVE].ToString())!,
            };
        }

        public static LoanOfferDto MapTableEntryToDto(LoanOfferTableEntry tableEntry)
        {
            return new LoanOfferDto
            {
                Id = tableEntry.Id,
                LoanType = EnumsMapperProfile.MapLoanTypeFromString(tableEntry.LoanType),
                MaxEffort = tableEntry.MaxEffort,
                Interest = tableEntry.Interest,
                IsActive = tableEntry.IsActive,
            };
        }

        public static LoanOfferTableEntry MapDtoToTableEntry(LoanOfferDto dto)
        {
            return new LoanOfferTableEntry
            {
                Id = dto.Id,
                LoanType = EnumsMapperProfile.MapLoanTypeToString(dto.LoanType),
                MaxEffort = dto.MaxEffort,
                Interest = dto.Interest,
                IsActive = dto.IsActive.GetValueOrDefault(),
            };
        }
    }
}

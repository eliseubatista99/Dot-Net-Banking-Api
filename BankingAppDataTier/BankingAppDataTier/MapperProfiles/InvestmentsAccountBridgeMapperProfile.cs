using BankingAppDataTier.Contracts.Constants;
using BankingAppDataTier.Contracts.Database;
using BankingAppDataTier.Contracts.Dtos;
using Microsoft.Data.SqlClient;

namespace BankingAppDataTier.MapperProfiles
{
    public static class InvestmentsAccountBridgeMapperProfile
    {
        public static InvestmentsAccountBridgeTableEntry MapSqlDataToInvestmentsAccountBridgeTableEntry(SqlDataReader sqlReader)
        {
            return new InvestmentsAccountBridgeTableEntry
            {
                Id = sqlReader[InvestmentsAccountBridgeTable.COLUMN_ID].ToString()!,
                SourceAccountId = sqlReader[InvestmentsAccountBridgeTable.COLUMN_SOURCE_ACCOUNT_ID].ToString()!,
                InvestmentsAccountId = sqlReader[InvestmentsAccountBridgeTable.COLUMN_INVESTMENTS_ACCOUNT_ID].ToString()!,
                Duration = Convert.ToInt16(sqlReader[InvestmentsAccountBridgeTable.COLUMN_DURATION])!,
                Interest = Convert.ToDecimal(sqlReader[InvestmentsAccountBridgeTable.COLUMN_INTEREST])!,
            };
        }
    }
}

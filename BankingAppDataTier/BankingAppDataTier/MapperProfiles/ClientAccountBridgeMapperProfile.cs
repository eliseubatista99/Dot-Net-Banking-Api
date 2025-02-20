using BankingAppDataTier.Contracts.Constants.Database;
using BankingAppDataTier.Contracts.Database;
using Microsoft.Data.SqlClient;

namespace BankingAppDataTier.MapperProfiles
{
    public static class ClientAccountBridgeMapperProfile
    {
        public static ClientAccountBridgeTableEntry MapSqlDataToTableEntry(SqlDataReader sqlReader)
        {
            return new ClientAccountBridgeTableEntry
            {
                Id = sqlReader[ClientAccountBridgeTable.COLUMN_ID].ToString()!,
                AccountId = sqlReader[ClientAccountBridgeTable.COLUMN_ACCOUNT_ID].ToString()!,
                ClientId = sqlReader[ClientAccountBridgeTable.COLUMN_CLIENT_ID].ToString()!,
            };
        }
    }
}

using BankingAppDataTier.Contracts.Configs;
using BankingAppDataTier.Contracts.Constants;
using BankingAppDataTier.Contracts.Database;
using BankingAppDataTier.Contracts.Dtos;
using BankingAppDataTier.Contracts.Providers;
using BankingAppDataTier.MapperProfiles;
using Microsoft.Data.SqlClient;
using System.Xml;

namespace BankingAppDataTier.Providers
{
    public class DatabaseInvestmentsAccountBridgeProvider : IDatabaseInvestmentsAccountBridgeProvider
    {

        private IConfiguration Configuration;

        private SqlConnection SqlConnection;
        private SqlCommand SqlCommnand;

        public DatabaseInvestmentsAccountBridgeProvider(IConfiguration configuration)
        {
            this.Configuration = configuration;

            var connectionString = Configuration.GetSection(DatabaseConfigs.DatabaseSection).GetValue<string>(DatabaseConfigs.DatabaseConnection);
            this.SqlConnection = new SqlConnection(connectionString);

            this.SqlCommnand = new SqlCommand();

            this.SqlCommnand.Connection = SqlConnection;
            this.SqlCommnand.CommandType = System.Data.CommandType.Text;
            this.SqlCommnand.Parameters.Clear();
        }

        public bool CreateTableIfNotExists()
        {
            try
            {
                this.SqlCommnand.Parameters.Clear();
                this.SqlCommnand.CommandText = $"IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[{InvestmentsAccountBridgeTable.TABLE_NAME}]')  AND type in (N'U')) " +
                    $"BEGIN " +
                    $"CREATE TABLE {InvestmentsAccountBridgeTable.TABLE_NAME} " +
                    $"(" +
                    $"{InvestmentsAccountBridgeTable.COLUMN_ID} VARCHAR(64) NOT NULL," +
                    $"{InvestmentsAccountBridgeTable.COLUMN_SOURCE_ACCOUNT_ID} VARCHAR(64) NOT NULL," +
                    $"{InvestmentsAccountBridgeTable.COLUMN_INVESTMENTS_ACCOUNT_ID} VARCHAR(64) NOT NULL," +
                    $"{InvestmentsAccountBridgeTable.COLUMN_DURATION} INTEGER NOT NULL," +
                    $"{InvestmentsAccountBridgeTable.COLUMN_INTEREST} DECIMAL(3,2) NOT NULL," +
                    $"PRIMARY KEY ({InvestmentsAccountBridgeTable.COLUMN_ID} )," +
                    $"FOREIGN KEY ({InvestmentsAccountBridgeTable.COLUMN_SOURCE_ACCOUNT_ID}) REFERENCES {AccountsTable.TABLE_NAME}({AccountsTable.COLUMN_ID})," +
                    $"FOREIGN KEY ({InvestmentsAccountBridgeTable.COLUMN_INVESTMENTS_ACCOUNT_ID}) REFERENCES {AccountsTable.TABLE_NAME}({AccountsTable.COLUMN_ID})" +
                    $") " +
                    $"END";

                SqlConnection.Open();

                var affectedRows = this.SqlCommnand.ExecuteNonQuery();

                SqlConnection.Close();

                return affectedRows != -1;
            }
            catch (Exception ex)
            {
                SqlConnection.Close();
                return false;
            }
        }

        public List<InvestmentsAccountBridgeTableEntry> GetAll()
        {
            try
            {
                List<InvestmentsAccountBridgeTableEntry> result = new List<InvestmentsAccountBridgeTableEntry>();

                this.SqlCommnand.Parameters.Clear();
                this.SqlCommnand.CommandText = $"SELECT * FROM {InvestmentsAccountBridgeTable.TABLE_NAME}";

                SqlConnection.Open();

                var sqlReader = this.SqlCommnand.ExecuteReader();

                while (sqlReader!.Read())
                {
                    var dataEntry = InvestmentsAccountBridgeMapperProfile.MapSqlDataToInvestmentsAccountBridgeTableEntry(sqlReader);

                    result.Add(dataEntry);
                }

                SqlConnection.Close();

                return result;
            }
            catch (Exception ex)
            {
                SqlConnection.Close();
                return new List<InvestmentsAccountBridgeTableEntry>();
            }
        }

        public InvestmentsAccountBridgeTableEntry? GetById(string id)
        {
            try
            {
                InvestmentsAccountBridgeTableEntry? result = null;

                this.SqlCommnand.Parameters.Clear();
                this.SqlCommnand.CommandText = $"SELECT * FROM {InvestmentsAccountBridgeTable.TABLE_NAME} WHERE {InvestmentsAccountBridgeTable.COLUMN_ID} = '{id}'";

                SqlConnection.Open();

                var sqlReader = this.SqlCommnand.ExecuteReader();

                result = InvestmentsAccountBridgeMapperProfile.MapSqlDataToInvestmentsAccountBridgeTableEntry(sqlReader);


                SqlConnection.Close();

                return result;
            }
            catch (Exception ex)
            {
                SqlConnection.Close();
                return null;
            }
        }

        public InvestmentsAccountBridgeTableEntry? GetByInvestmentsAccountId(string investmentsAccountId)
        {
            try
            {
                InvestmentsAccountBridgeTableEntry? result = null;

                this.SqlCommnand.Parameters.Clear();
                this.SqlCommnand.CommandText = $"SELECT * FROM {InvestmentsAccountBridgeTable.TABLE_NAME} WHERE {InvestmentsAccountBridgeTable.COLUMN_INVESTMENTS_ACCOUNT_ID} = '{investmentsAccountId}'";

                SqlConnection.Open();

                var sqlReader = this.SqlCommnand.ExecuteReader();

                sqlReader!.Read();

                result = InvestmentsAccountBridgeMapperProfile.MapSqlDataToInvestmentsAccountBridgeTableEntry(sqlReader);

                SqlConnection.Close();

                return result;
            }
            catch (Exception ex)
            {
                SqlConnection.Close();
                return null;
            }
        }


        public List<InvestmentsAccountBridgeTableEntry> GetInvestmentsAccountsOfAccount(string accountId)
        {
            try
            {
                List<InvestmentsAccountBridgeTableEntry> result = new List<InvestmentsAccountBridgeTableEntry>();

                this.SqlCommnand.Parameters.Clear();
                this.SqlCommnand.CommandText = $"SELECT * FROM {InvestmentsAccountBridgeTable.TABLE_NAME} WHERE {InvestmentsAccountBridgeTable.COLUMN_SOURCE_ACCOUNT_ID} = '{accountId}'";

                SqlConnection.Open();

                var sqlReader = this.SqlCommnand.ExecuteReader();

                while (sqlReader!.Read())
                {
                    var dataEntry = InvestmentsAccountBridgeMapperProfile.MapSqlDataToInvestmentsAccountBridgeTableEntry(sqlReader);

                    result.Add(dataEntry);
                }

                SqlConnection.Close();

                return result;
            }
            catch (Exception ex)
            {
                SqlConnection.Close();
                return new List<InvestmentsAccountBridgeTableEntry>();
            }
        }
        public bool Add(InvestmentsAccountBridgeTableEntry entry)
        {
            try
            {
                this.SqlCommnand.Parameters.Clear();
                this.SqlCommnand.CommandText = $"INSERT INTO {InvestmentsAccountBridgeTable.TABLE_NAME} " +
                    $"({InvestmentsAccountBridgeTable.COLUMN_ID}, {InvestmentsAccountBridgeTable.COLUMN_SOURCE_ACCOUNT_ID}, " +
                    $"{InvestmentsAccountBridgeTable.COLUMN_INVESTMENTS_ACCOUNT_ID}, {InvestmentsAccountBridgeTable.COLUMN_DURATION}, " +
                    $"{InvestmentsAccountBridgeTable.COLUMN_INTEREST}) " +
                    $"VALUES " +
                    $"('{entry.Id}', '{entry.SourceAccountId}', '{entry.InvestmentsAccountId}', '{entry.Duration}', '{entry.Interest}');";


                SqlConnection.Open();

                var affectedRows = this.SqlCommnand.ExecuteNonQuery();

                SqlConnection.Close();

                return affectedRows != -1;
            }
            catch (Exception ex)
            {
                SqlConnection.Close();
                return false;
            }
        }

        public bool Edit(InvestmentsAccountBridgeTableEntry entry)
        {
            try
            {
                this.SqlCommnand.Parameters.Clear();
                this.SqlCommnand.CommandText = $"UPDATE {InvestmentsAccountBridgeTable.TABLE_NAME} " +
                    $"SET {InvestmentsAccountBridgeTable.COLUMN_ID} = '{entry.Id}', " +
                    $"{InvestmentsAccountBridgeTable.COLUMN_SOURCE_ACCOUNT_ID} = '{entry.SourceAccountId}', " +
                    $"{InvestmentsAccountBridgeTable.COLUMN_INVESTMENTS_ACCOUNT_ID} = '{entry.InvestmentsAccountId}'" +
                    $"{InvestmentsAccountBridgeTable.COLUMN_DURATION} = '{entry.Duration}'" +
                    $"{InvestmentsAccountBridgeTable.COLUMN_INTEREST} = '{entry.Interest}'" +
                    $"WHERE {InvestmentsAccountBridgeTable.COLUMN_ID} = '{entry.Id}';";


                SqlConnection.Open();

                var affectedRows = this.SqlCommnand.ExecuteNonQuery();

                SqlConnection.Close();

                return affectedRows != -1;
            }
            catch (Exception ex)
            {
                SqlConnection.Close();
                return false;
            }
        }

        public bool Delete(string id)
        {
            try
            {
                this.SqlCommnand.Parameters.Clear();
                this.SqlCommnand.CommandText = $"DELETE FROM {InvestmentsAccountBridgeTable.TABLE_NAME} WHERE {InvestmentsAccountBridgeTable.COLUMN_ID} = '{id}'";

                SqlConnection.Open();

                var affectedRows = this.SqlCommnand.ExecuteNonQuery();

                SqlConnection.Close();

                return affectedRows != -1;
            }
            catch (Exception ex)
            {
                SqlConnection.Close();
                return false;
            }
        }

    }
}

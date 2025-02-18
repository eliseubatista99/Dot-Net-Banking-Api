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
    public class DatabaseClientAccountBridgeProvider : IDatabaseClientAccountBridgeProvider
    {

        private IConfiguration Configuration;

        private SqlConnection SqlConnection;
        private SqlCommand SqlCommnand;

        public DatabaseClientAccountBridgeProvider(IConfiguration configuration)
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
                this.SqlCommnand.CommandText = $"IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[{ClientAccountBridgeTable.TABLE_NAME}]')  AND type in (N'U')) " +
                    $"BEGIN " +
                    $"CREATE TABLE {ClientAccountBridgeTable.TABLE_NAME} " +
                    $"(" +
                    $"{ClientAccountBridgeTable.COLUMN_ID} VARCHAR(64) NOT NULL," +
                    $"{ClientAccountBridgeTable.COLUMN_ACCOUNT_ID} VARCHAR(64) NOT NULL," +
                    $"{ClientAccountBridgeTable.COLUMN_CLIENT_ID} VARCHAR(64) NOT NULL," +
                    $"PRIMARY KEY ({ClientAccountBridgeTable.COLUMN_ID} )," +
                    $"FOREIGN KEY ({ClientAccountBridgeTable.COLUMN_ACCOUNT_ID}) REFERENCES {AccountsTable.TABLE_NAME}({AccountsTable.COLUMN_ID})," +
                    $"FOREIGN KEY ({ClientAccountBridgeTable.COLUMN_CLIENT_ID}) REFERENCES {ClientsTable.TABLE_NAME}({ClientsTable.COLUMN_ID})" +
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

        public List<ClientAccountBridgeTableEntry> GetAll()
        {
            try
            {
                List<ClientAccountBridgeTableEntry> result = new List<ClientAccountBridgeTableEntry>();

                this.SqlCommnand.Parameters.Clear();
                this.SqlCommnand.CommandText = $"SELECT * FROM {ClientAccountBridgeTable.TABLE_NAME}";

                SqlConnection.Open();

                var sqlReader = this.SqlCommnand.ExecuteReader();

                while (sqlReader!.Read())
                {
                    var dataEntry = ClientAccountBridgeMapperProfile.MapSqlDataToClientAccountBridgeTableEntry(sqlReader);

                    result.Add(dataEntry);
                }

                SqlConnection.Close();

                return result;
            }
            catch (Exception ex)
            {
                SqlConnection.Close();
                return new List<ClientAccountBridgeTableEntry>();
            }
        }

        public ClientAccountBridgeTableEntry? GetByAccountId(string accountId)
        {
            try
            {
                ClientAccountBridgeTableEntry? result = null;

                this.SqlCommnand.Parameters.Clear();
                this.SqlCommnand.CommandText = $"SELECT * FROM {ClientAccountBridgeTable.TABLE_NAME} WHERE" +
                    $" {ClientAccountBridgeTable.COLUMN_ACCOUNT_ID} = '{accountId}'";

                SqlConnection.Open();

                var sqlReader = this.SqlCommnand.ExecuteReader();

                if (sqlReader.HasRows)
                {
                    sqlReader.Read();
                    result = ClientAccountBridgeMapperProfile.MapSqlDataToClientAccountBridgeTableEntry(sqlReader);
                }

                SqlConnection.Close();

                return result;
            }
            catch (Exception ex)
            {
                SqlConnection.Close();
                return null;
            }
        }

        public List<ClientAccountBridgeTableEntry> GetAccountsOfClient(string clientID)
        {
            try
            {
                List<ClientAccountBridgeTableEntry> result = new List<ClientAccountBridgeTableEntry>();

                this.SqlCommnand.Parameters.Clear();
                this.SqlCommnand.CommandText = $"SELECT * FROM {ClientAccountBridgeTable.TABLE_NAME} WHERE {ClientAccountBridgeTable.COLUMN_CLIENT_ID} = '{clientID}'";

                SqlConnection.Open();

                var sqlReader = this.SqlCommnand.ExecuteReader();

                while (sqlReader!.Read())
                {
                    var dataEntry = ClientAccountBridgeMapperProfile.MapSqlDataToClientAccountBridgeTableEntry(sqlReader);

                    result.Add(dataEntry);
                }

                SqlConnection.Close();

                return result;
            }
            catch (Exception ex)
            {
                SqlConnection.Close();
                return new List<ClientAccountBridgeTableEntry>();
            }
        }

        public string? GetClientOfAccount(string accountId)
        {
            try
            {
                string? result = null;

                this.SqlCommnand.Parameters.Clear();
                this.SqlCommnand.CommandText = $"SELECT * FROM {ClientAccountBridgeTable.TABLE_NAME} WHERE {ClientAccountBridgeTable.COLUMN_ACCOUNT_ID} = '{accountId}'";

                SqlConnection.Open();

                var sqlReader = this.SqlCommnand.ExecuteReader();

                if (sqlReader.HasRows)
                {
                    sqlReader.Read();
                    var dataEntry = ClientAccountBridgeMapperProfile.MapSqlDataToClientAccountBridgeTableEntry(sqlReader);

                    result = dataEntry.ClientId;
                }

                SqlConnection.Close();

                return result;
            }
            catch (Exception ex)
            {
                SqlConnection.Close();
                return null;
            }
        }

        public bool Add(ClientAccountBridgeTableEntry entry)
        {
            try
            {
                this.SqlCommnand.Parameters.Clear();
                this.SqlCommnand.CommandText = $"INSERT INTO {ClientAccountBridgeTable.TABLE_NAME} " +
                    $"({ClientAccountBridgeTable.COLUMN_ID}, {ClientAccountBridgeTable.COLUMN_ACCOUNT_ID}, {ClientAccountBridgeTable.COLUMN_CLIENT_ID}) " +
                    $"VALUES " +
                    $"('{entry.Id}', '{entry.AccountId}', '{entry.ClientId}');";


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

        public bool Edit(ClientAccountBridgeTableEntry entry)
        {
            try
            {
                this.SqlCommnand.Parameters.Clear();
                this.SqlCommnand.CommandText = $"UPDATE {ClientAccountBridgeTable.TABLE_NAME} " +
                    $"SET {ClientAccountBridgeTable.COLUMN_ID} = '{entry.Id}', " +
                    $"{ClientAccountBridgeTable.COLUMN_ACCOUNT_ID} = '{entry.AccountId}', " +
                    $"{ClientAccountBridgeTable.COLUMN_CLIENT_ID} = '{entry.ClientId}'" +
                    $"WHERE {ClientAccountBridgeTable.COLUMN_ID} = '{entry.Id}';";


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
                this.SqlCommnand.CommandText = $"DELETE FROM {ClientAccountBridgeTable.TABLE_NAME} WHERE {ClientAccountBridgeTable.COLUMN_ID} = '{id}'";

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

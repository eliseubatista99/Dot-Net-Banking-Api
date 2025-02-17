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
    public class DatabaseAccountsProvider : IDatabaseAccountsProvider
    {

        private IConfiguration Configuration;

        private SqlConnection SqlConnection;
        private SqlCommand SqlCommnand;

        public DatabaseAccountsProvider(IConfiguration configuration)
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
                this.SqlCommnand.CommandText = $"IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[{AccountsTable.TABLE_NAME}]')  AND type in (N'U')) " +
                    $"BEGIN " +
                    $"CREATE TABLE {AccountsTable.TABLE_NAME} " +
                    $"(" +
                    $"{AccountsTable.COLUMN_ID} VARCHAR(64) NOT NULL," +
                    $"{AccountsTable.COLUMN_TYPE} CHAR(2) NOT NULL," +
                    $"{AccountsTable.COLUMN_BALANCE} DECIMAL(20,2) NOT NULL," +
                    $"{AccountsTable.COLUMN_NAME} VARCHAR(64) NOT NULL," +
                    $"{AccountsTable.COLUMN_IMAGE} VARCHAR(MAX)," +
                    $"PRIMARY KEY ({AccountsTable.COLUMN_ID} )" +
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

        public List<AccountsTableEntry> GetAll()
        {
            try
            {
                List<AccountsTableEntry> result = new List<AccountsTableEntry>();

                this.SqlCommnand.Parameters.Clear();
                this.SqlCommnand.CommandText = $"SELECT * FROM {AccountsTable.TABLE_NAME}";

                SqlConnection.Open();

                var sqlReader = this.SqlCommnand.ExecuteReader();

                while (sqlReader!.Read())
                {
                    var dataEntry = AccountsMapperProfile.MapSqlDataToAccountsTableEntry(sqlReader);

                    result.Add(dataEntry);
                }

                SqlConnection.Close();

                return result;
            }
            catch (Exception ex)
            {
                SqlConnection.Close();
                return new List<AccountsTableEntry>();
            }
        }

        public AccountsTableEntry? GetById(string id)
        {
            try
            {
                this.SqlCommnand.Parameters.Clear();
                this.SqlCommnand.CommandText = $"SELECT * FROM {AccountsTable.TABLE_NAME} WHERE {AccountsTable.COLUMN_ID} = '{id}'";

                SqlConnection.Open();

                var sqlReader = this.SqlCommnand.ExecuteReader();

                //Read one time
                sqlReader.Read();

                //If no entry was found, return nothing
                if (sqlReader == null || !sqlReader.HasRows)
                {
                    SqlConnection.Close();
                    return null;
                }

                var dataEntry = AccountsMapperProfile.MapSqlDataToAccountsTableEntry(sqlReader);

                SqlConnection.Close();

                return dataEntry;
            }
            catch (Exception ex)
            {
                SqlConnection.Close();
                return null;
            }
        }

        public AccountsTableEntry? GetByClientId(string id)
        {
            try
            {
                this.SqlCommnand.Parameters.Clear();
                this.SqlCommnand.CommandText = $"SELECT * FROM {AccountsTable.TABLE_NAME} WHERE {AccountsTable.COLUMN_ID} = '{id}'";

                SqlConnection.Open();

                var sqlReader = this.SqlCommnand.ExecuteReader();

                //Read one time
                sqlReader.Read();

                //If no entry was found, return nothing
                if (sqlReader == null || !sqlReader.HasRows)
                {
                    SqlConnection.Close();
                    return null;
                }

                var dataEntry = AccountsMapperProfile.MapSqlDataToAccountsTableEntry(sqlReader);

                SqlConnection.Close();

                return dataEntry;
            }
            catch (Exception ex)
            {
                SqlConnection.Close();
                return null;
            }
        }


        public bool Add(AccountsTableEntry entry)
        {
            try
            {
                this.SqlCommnand.Parameters.Clear();
                this.SqlCommnand.CommandText = $"INSERT INTO {AccountsTable.TABLE_NAME} " +
                    $"({AccountsTable.COLUMN_ID}, {AccountsTable.COLUMN_TYPE}, {AccountsTable.COLUMN_BALANCE}, {AccountsTable.COLUMN_NAME}, {AccountsTable.COLUMN_IMAGE}) " +
                    $"VALUES " +
                    $"('{entry.AccountId}', '{entry.AccountType}', '{entry.Balance}', '{entry.Name}', '{entry.Image}');";


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

        public bool Edit(AccountsTableEntry entry)
        {
            try
            {
                this.SqlCommnand.Parameters.Clear();
                this.SqlCommnand.CommandText = $"UPDATE {AccountsTable.TABLE_NAME} " +
                    $"SET {AccountsTable.COLUMN_ID} = '{entry.AccountId}', " +
                    $"{AccountsTable.COLUMN_TYPE} = '{entry.AccountType}', " +
                    $"{AccountsTable.COLUMN_BALANCE} = '{entry.Balance}', " +
                    $"{AccountsTable.COLUMN_NAME} = '{entry.Name}', " +
                    $"{AccountsTable.COLUMN_IMAGE} = '{entry.Image}'" +
                    $"WHERE {AccountsTable.COLUMN_ID} = '{entry.AccountId}';";


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
                this.SqlCommnand.CommandText = $"DELETE FROM {AccountsTable.TABLE_NAME} WHERE {AccountsTable.COLUMN_ID} = '{id}'";

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

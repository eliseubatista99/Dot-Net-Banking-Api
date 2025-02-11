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
    public class DatabaseProvider : IDatabaseProvider
    {

        private IConfiguration Configuration;

        private SqlConnection SqlConnection;
        private SqlCommand SqlCommnand;

        public DatabaseProvider(IConfiguration configuration)
        {
            this.Configuration = configuration;

            var connectionString = Configuration.GetSection(DatabaseConfigs.DatabaseSection).GetValue<string>(DatabaseConfigs.DatabaseConnection);
            this.SqlConnection = new SqlConnection(connectionString);

            this.SqlCommnand = new SqlCommand();

            this.SqlCommnand.Connection = SqlConnection;
            this.SqlCommnand.CommandType = System.Data.CommandType.Text;
            this.SqlCommnand.Parameters.Clear();
        }

        public bool CreateClientsTableIfNotExists()
        {
            try
            {
                this.SqlCommnand.Parameters.Clear();
                this.SqlCommnand.CommandText = $"IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[{ClientsTable.TABLE_NAME}]')  AND type in (N'U')) " +
                    $"BEGIN " +
                    $"CREATE TABLE {ClientsTable.TABLE_NAME} " +
                    $"(" +
                    $"{ClientsTable.COLUMN_ID} VARCHAR(64) NOT NULL," +
                    $"{ClientsTable.COLUMN_PASSWORD} VARCHAR(64) NOT NULL," +
                    $"{ClientsTable.COLUMN_NAME} VARCHAR(64) NOT NULL," +
                    $"{ClientsTable.COLUMN_SURNAME} VARCHAR(64) NOT NULL," +
                    $"{ClientsTable.COLUMN_BIRTH_DATE} DATE NOT NULL," +
                    $"{ClientsTable.COLUMN_VAT_NUMBER} VARCHAR(30) NOT NULL," +
                    $"{ClientsTable.COLUMN_PHONE_NUMBER} VARCHAR(20) NOT NULL," +
                    $"{ClientsTable.COLUMN_EMAIL} VARCHAR(60) NOT NULL," +
                    $"PRIMARY KEY ({ClientsTable.COLUMN_ID} )" +
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

        public List<ClientsTableEntry> GetAllClients()
        {
            try
            {
                List<ClientsTableEntry> result = new List<ClientsTableEntry>();

                this.SqlCommnand.Parameters.Clear();
                this.SqlCommnand.CommandText = $"SELECT * FROM {ClientsTable.TABLE_NAME}";

                SqlConnection.Open();

                var sqlReader = this.SqlCommnand.ExecuteReader();

                while (sqlReader!.Read())
                {
                    var dataEntry = ClientsMapperProfile.MapSqlDataToClientTableEntry(sqlReader);

                    result.Add(dataEntry);
                }

                SqlConnection.Close();

                return result;
            }
            catch (Exception ex)
            {
                SqlConnection.Close();
                return new List<ClientsTableEntry>();
            } 
        }

        public ClientsTableEntry? GetClientById(string id)
        {
            try
            {
                this.SqlCommnand.Parameters.Clear();
                this.SqlCommnand.CommandText = $"SELECT * FROM {ClientsTable.TABLE_NAME} WHERE {ClientsTable.COLUMN_ID} = '{id}'";

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

                var dataEntry = ClientsMapperProfile.MapSqlDataToClientTableEntry(sqlReader);

                SqlConnection.Close();

                return dataEntry;
            }
            catch (Exception ex)
            {
                SqlConnection.Close();
                return null;
            }
        }

        public bool AddClient(ClientsTableEntry client)
        {
            try
            {
                this.SqlCommnand.Parameters.Clear();
                this.SqlCommnand.CommandText = $"INSERT INTO {ClientsTable.TABLE_NAME} " +
                    $"({ClientsTable.COLUMN_ID}, {ClientsTable.COLUMN_PASSWORD}, {ClientsTable.COLUMN_NAME}, {ClientsTable.COLUMN_SURNAME}, {ClientsTable.COLUMN_BIRTH_DATE}, " +
                    $"{ClientsTable.COLUMN_VAT_NUMBER}, {ClientsTable.COLUMN_PHONE_NUMBER}, {ClientsTable.COLUMN_EMAIL}) " +
                    $"VALUES " +
                    $"('{client.Id}', '{client.Password}', '{client.Name}', '{client.Surname}', '{client.BirthDate.ToString("yyyy-MM-dd")}', '{client.VATNumber}', '{client.PhoneNumber}', '{client.Email}');";


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

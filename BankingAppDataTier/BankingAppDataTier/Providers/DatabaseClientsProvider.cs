using BankingAppDataTier.Contracts.Configs;
using BankingAppDataTier.Contracts.Constants.Database;
using BankingAppDataTier.Contracts.Database;
using BankingAppDataTier.Contracts.Providers;
using BankingAppDataTier.Database;
using Npgsql;

namespace BankingAppDataTier.Providers
{
    public class DatabaseClientsProvider : IDatabaseClientsProvider
    {

        private IConfiguration Configuration;
        private IMapperProvider mapperProvider;

        private string connectionString;

        public DatabaseClientsProvider(IConfiguration configuration, IMapperProvider _mapperProvider)
        {
            this.Configuration = configuration;
            this.mapperProvider = _mapperProvider;

            connectionString = Configuration.GetSection(DatabaseConfigs.DatabaseSection).GetValue<string>(DatabaseConfigs.DatabaseConnection);
        }

        public bool CreateTableIfNotExists()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                var (transaction, command) = SqlDatabaseHelper.InitialzieSqlTransaction(connection);

                try
                {
                    command.CommandText = $"CREATE TABLE IF NOT EXISTS {ClientsTable.TABLE_NAME}" +
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
                        $")";

                    command.ExecuteNonQuery();

                    // Attempt to commit the transaction.
                    transaction.Commit();

                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public List<ClientsTableEntry> GetAll()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                List<ClientsTableEntry> result = new List<ClientsTableEntry>();

                connection.Open();

                var (transaction, command) = SqlDatabaseHelper.InitialzieSqlTransaction(connection);

                try
                {
                    command.CommandText = $"SELECT * FROM {ClientsTable.TABLE_NAME}";

                    var sqlReader = command.ExecuteReader();

                    while (sqlReader!.Read())
                    {
                        var dataEntry = mapperProvider.Map<NpgsqlDataReader, ClientsTableEntry>(sqlReader);

                        result.Add(dataEntry);
                    }

                    return result;
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public ClientsTableEntry? GetById(string id)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                ClientsTableEntry? result = null;

                connection.Open();

                var (transaction, command) = SqlDatabaseHelper.InitialzieSqlTransaction(connection);

                try
                {
                    command.CommandText = $"SELECT * FROM {ClientsTable.TABLE_NAME} WHERE {ClientsTable.COLUMN_ID} = '{id}'";

                    var sqlReader = command.ExecuteReader();

                    if (sqlReader.HasRows)
                    {
                        sqlReader.Read();
                        result = mapperProvider.Map<NpgsqlDataReader, ClientsTableEntry>(sqlReader);

                    }

                    return result;
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public bool Add(ClientsTableEntry entry)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                var (transaction, command) = SqlDatabaseHelper.InitialzieSqlTransaction(connection);

                try
                {
                    command.CommandText = $"INSERT INTO {ClientsTable.TABLE_NAME} " +
                        $"({ClientsTable.COLUMN_ID}, {ClientsTable.COLUMN_PASSWORD}, {ClientsTable.COLUMN_NAME}, {ClientsTable.COLUMN_SURNAME}, {ClientsTable.COLUMN_BIRTH_DATE}, " +
                        $"{ClientsTable.COLUMN_VAT_NUMBER}, {ClientsTable.COLUMN_PHONE_NUMBER}, {ClientsTable.COLUMN_EMAIL}) " +
                        $"VALUES " +
                        $"('{entry.Id}', '{entry.Password}', '{entry.Name}', '{entry.Surname}', '{entry.BirthDate.ToString("yyyy-MM-dd")}', '{entry.VATNumber}', '{entry.PhoneNumber}', '{entry.Email}');";

                    command.ExecuteNonQuery();

                    // Attempt to commit the transaction.
                    transaction.Commit();

                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public bool Edit(ClientsTableEntry entry)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                var (transaction, command) = SqlDatabaseHelper.InitialzieSqlTransaction(connection);

                try
                {
                    command.CommandText = $"UPDATE {ClientsTable.TABLE_NAME} " +
                    $"SET {ClientsTable.COLUMN_ID} = '{entry.Id}', " +
                    $"{ClientsTable.COLUMN_NAME} = '{entry.Name}', " +
                    $"{ClientsTable.COLUMN_SURNAME} = '{entry.Surname}', " +
                    $"{ClientsTable.COLUMN_BIRTH_DATE} = '{entry.BirthDate.ToString("yyyy-MM-dd")}', " +
                    $"{ClientsTable.COLUMN_VAT_NUMBER} = '{entry.VATNumber}', " +
                    $"{ClientsTable.COLUMN_PHONE_NUMBER} = '{entry.PhoneNumber}', " +
                    $"{ClientsTable.COLUMN_EMAIL} = '{entry.Email}' " +
                    $"WHERE {ClientsTable.COLUMN_ID} = '{entry.Id}';";

                    command.ExecuteNonQuery();

                    // Attempt to commit the transaction.
                    transaction.Commit();

                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public bool ChangePassword(string id, string password)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                var (transaction, command) = SqlDatabaseHelper.InitialzieSqlTransaction(connection);

                try
                {
                    command.CommandText = $"UPDATE {ClientsTable.TABLE_NAME} " +
                    $"SET {ClientsTable.COLUMN_PASSWORD} = '{password}' " +
                    $"WHERE {ClientsTable.COLUMN_ID} = '{id}';";

                    command.ExecuteNonQuery();

                    // Attempt to commit the transaction.
                    transaction.Commit();

                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public bool Delete(string id)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                var (transaction, command) = SqlDatabaseHelper.InitialzieSqlTransaction(connection);

                try
                {
                    command.CommandText = $"DELETE FROM {ClientsTable.TABLE_NAME} WHERE {ClientsTable.COLUMN_ID} = '{id}'";

                    command.ExecuteNonQuery();

                    // Attempt to commit the transaction.
                    transaction.Commit();

                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public bool DeleteAll()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                var (transaction, command) = SqlDatabaseHelper.InitialzieSqlTransaction(connection);

                try
                {
                    command.CommandText = $"DELETE FROM {ClientsTable.TABLE_NAME} WHERE 1=1";

                    command.ExecuteNonQuery();

                    // Attempt to commit the transaction.
                    transaction.Commit();

                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}

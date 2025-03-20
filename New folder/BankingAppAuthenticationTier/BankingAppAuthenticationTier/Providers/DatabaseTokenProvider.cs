using BankingAppAuthenticationTier.Contracts.Configs;
using BankingAppAuthenticationTier.Contracts.Constants.Database;
using BankingAppAuthenticationTier.Contracts.Database;
using BankingAppAuthenticationTier.Contracts.Providers;
using BankingAppAuthenticationTier.Database;
using Npgsql;

namespace BankingAppAuthenticationTier.Providers
{
    public class DatabaseTokenProvider : IDatabaseTokenProvider
    {

        private IConfiguration Configuration;
        private IMapperProvider mapperProvider;

        private string connectionString;

        public DatabaseTokenProvider(IConfiguration configuration, IMapperProvider _mapperProvider)
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
                    command.CommandText = $"CREATE TABLE IF NOT EXISTS {TokensTable.TABLE_NAME}" +
                        $"(" +
                        $"{TokensTable.COLUMN_TOKEN} VARCHAR NOT NULL," +
                        $"{TokensTable.COLUMN_CLIENT_ID} VARCHAR(64) NOT NULL," +
                        $"{TokensTable.COLUMN_EXPIRATION_DATE} VARCHAR(20) NOT NULL," +
                        $"PRIMARY KEY ({TokensTable.COLUMN_TOKEN} )," +
                        $"FOREIGN KEY ({TokensTable.COLUMN_CLIENT_ID}) REFERENCES {ClientsTable.TABLE_NAME}({ClientsTable.COLUMN_ID})" +
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

        public bool Add(TokenTableEntry entry)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                var (transaction, command) = SqlDatabaseHelper.InitialzieSqlTransaction(connection);

                try
                {
                    command.CommandText = $"INSERT INTO {TokensTable.TABLE_NAME} " +
                                            $"({TokensTable.COLUMN_TOKEN}, {TokensTable.COLUMN_CLIENT_ID}, {TokensTable.COLUMN_EXPIRATION_DATE}) " +
                                            $"VALUES " +
                                            $"('{entry.Token}', '{entry.ClientId}', '{SqlDatabaseHelper.FormatDateWithTime(entry.ExpirationDate)}');";

                    command.ExecuteNonQuery();

                    // Attempt to commit the transaction.
                    transaction.Commit();

                    return true;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
        
        public bool Delete(string token)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                var (transaction, command) = SqlDatabaseHelper.InitialzieSqlTransaction(connection);

                try
                {
                    command.CommandText = $"DELETE FROM {TokensTable.TABLE_NAME} WHERE {TokensTable.COLUMN_TOKEN} = '{token}'";

                    command.ExecuteNonQuery();

                    // Attempt to commit the transaction.
                    transaction.Commit();

                    return true;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public bool SetExpirationDateTime(string token, DateTime expiration)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                var (transaction, command) = SqlDatabaseHelper.InitialzieSqlTransaction(connection);

                try
                {
                    command.CommandText = $"UPDATE {TokensTable.TABLE_NAME} " +
                    $"SET {TokensTable.COLUMN_EXPIRATION_DATE} = '{SqlDatabaseHelper.FormatDateWithTime(expiration)}' " +
                    $"WHERE {TokensTable.COLUMN_TOKEN} = '{token}';";

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

        public TokenTableEntry? GetToken(string token)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                TokenTableEntry? result = null;

                connection.Open();

                var (transaction, command) = SqlDatabaseHelper.InitialzieSqlTransaction(connection);

                try
                {
                    command.CommandText = $"SELECT * FROM {TokensTable.TABLE_NAME} WHERE {TokensTable.COLUMN_TOKEN} = '{token}'";

                    var sqlReader = command.ExecuteReader();

                    if (sqlReader.HasRows)
                    {
                        sqlReader.Read();
                        result = mapperProvider.Map<NpgsqlDataReader, TokenTableEntry>(sqlReader);
                    }

                    return result;
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public bool DeleteAllExpired()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                DateTime today = DateTime.Now;
                connection.Open();

                var (transaction, command) = SqlDatabaseHelper.InitialzieSqlTransaction(connection);

                try
                {
                    command.CommandText = $"DELETE FROM {TokensTable.TABLE_NAME} " +
                        $"WHERE {TokensTable.COLUMN_EXPIRATION_DATE}<'{today.ToString("yyyy-MM-dd")}'";

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

        public bool DeleteTokensOfClient(string clientId)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                DateTime today = DateTime.Now;
                connection.Open();

                var (transaction, command) = SqlDatabaseHelper.InitialzieSqlTransaction(connection);

                try
                {
                    command.CommandText = $"DELETE FROM {TokensTable.TABLE_NAME} " +
                        $"WHERE {TokensTable.COLUMN_CLIENT_ID}='{clientId}'";

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
                    command.CommandText = $"DELETE FROM {TokensTable.TABLE_NAME} WHERE 1=1";

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

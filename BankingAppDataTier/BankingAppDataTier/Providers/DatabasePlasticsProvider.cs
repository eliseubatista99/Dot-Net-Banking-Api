using BankingAppDataTier.Contracts.Configs;
using BankingAppDataTier.Contracts.Constants.Database;
using BankingAppDataTier.Contracts.Database;
using BankingAppDataTier.Contracts.Enums;
using BankingAppDataTier.Contracts.Providers;
using BankingAppDataTier.Database;
using BankingAppDataTier.MapperProfiles;
using Microsoft.Data.SqlClient;
using Npgsql;

namespace BankingAppDataTier.Providers
{
    public class DatabasePlasticsProvider : IDatabasePlasticsProvider
    {
        private IConfiguration Configuration;

        private string connectionString;

        public DatabasePlasticsProvider(IConfiguration configuration)
        {
            this.Configuration = configuration;

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
                    command.CommandText = $"CREATE TABLE IF NOT EXISTS {PlasticsTable.TABLE_NAME}" +
                        $"(" +
                        $"{PlasticsTable.COLUMN_ID} VARCHAR(64) NOT NULL," +
                        $"{PlasticsTable.COLUMN_TYPE} VARCHAR(64) NOT NULL," +
                        $"{PlasticsTable.COLUMN_NAME} VARCHAR(64) NOT NULL," +
                        $"{PlasticsTable.COLUMN_CASHBACK} DECIMAL(5,2)," +
                        $"{PlasticsTable.COLUMN_COMISSION} DECIMAL(5,2)," +
                        $"{PlasticsTable.COLUMN_IMAGE} VARCHAR NOT NULL," +
                        $"{PlasticsTable.COLUMN_IS_ACTIVE} BOOL NOT NULL," +
                        $"PRIMARY KEY ({PlasticsTable.COLUMN_ID} )" +
                        $") ";

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

        public bool Add(PlasticTableEntry entry)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                var (transaction, command) = SqlDatabaseHelper.InitialzieSqlTransaction(connection);

                try
                {
                    command.CommandText = $"INSERT INTO {PlasticsTable.TABLE_NAME} " +
                        $"({PlasticsTable.COLUMN_ID}, {PlasticsTable.COLUMN_TYPE}, {PlasticsTable.COLUMN_NAME}, {PlasticsTable.COLUMN_CASHBACK}, " +
                        $"{PlasticsTable.COLUMN_COMISSION}, {PlasticsTable.COLUMN_IMAGE}, {PlasticsTable.COLUMN_IS_ACTIVE}) " +
                        $"VALUES " +
                        $"('{entry.Id}', '{entry.CardType}', '{entry.Name}', '{entry.Cashback.GetValueOrDefault()}', " +
                        $"'{entry.Commission.GetValueOrDefault()}', '{entry.Image}', '{entry.IsActive}');";

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
                    command.CommandText = $"DELETE FROM {PlasticsTable.TABLE_NAME} WHERE {PlasticsTable.COLUMN_ID} = '{id}'";

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

        public bool Edit(PlasticTableEntry entry)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                var (transaction, command) = SqlDatabaseHelper.InitialzieSqlTransaction(connection);

                try
                {
                    command.CommandText = $"UPDATE {PlasticsTable.TABLE_NAME} " +
                    $"SET {PlasticsTable.COLUMN_ID} = '{entry.Id}', " +
                    $"{PlasticsTable.COLUMN_TYPE} = '{entry.CardType}', " +
                    $"{PlasticsTable.COLUMN_NAME} = '{entry.Name}', " +
                    $"{PlasticsTable.COLUMN_CASHBACK} = '{entry.Cashback}', " +
                    $"{PlasticsTable.COLUMN_COMISSION} = '{entry.Commission}', " +
                    $"{PlasticsTable.COLUMN_IMAGE} = '{entry.Image}', " +
                    $"{PlasticsTable.COLUMN_IS_ACTIVE} = '{entry.IsActive}'" +
                    $"WHERE {PlasticsTable.COLUMN_ID} = '{entry.Id}';";

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

        public List<PlasticTableEntry> GetAll()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                List<PlasticTableEntry> result = new List<PlasticTableEntry>();

                connection.Open();

                var (transaction, command) = SqlDatabaseHelper.InitialzieSqlTransaction(connection);

                try
                {
                    command.CommandText = $"SELECT * FROM {PlasticsTable.TABLE_NAME}";

                    var sqlReader = command.ExecuteReader();

                    while (sqlReader!.Read())
                    {
                        var dataEntry = PlasticsMapperProfile.MapSqlDataToTableEntry(sqlReader);

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

        public PlasticTableEntry? GetById(string id)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                PlasticTableEntry? result = null;

                connection.Open();

                var (transaction, command) = SqlDatabaseHelper.InitialzieSqlTransaction(connection);

                try
                {
                    command.CommandText = $"SELECT * FROM {PlasticsTable.TABLE_NAME} WHERE {PlasticsTable.COLUMN_ID} = '{id}'";

                    var sqlReader = command.ExecuteReader();

                    if (sqlReader.HasRows)
                    {
                        sqlReader.Read();
                        result = PlasticsMapperProfile.MapSqlDataToTableEntry(sqlReader);
                    }

                    return result;
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public List<PlasticTableEntry> GetPlasticsOfCardType(string cardType, bool onlyActive = false)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                List<PlasticTableEntry> result = new List<PlasticTableEntry>();

                connection.Open();

                var (transaction, command) = SqlDatabaseHelper.InitialzieSqlTransaction(connection);

                try
                {
                    command.CommandText = $"SELECT * FROM {PlasticsTable.TABLE_NAME} WHERE {PlasticsTable.COLUMN_TYPE} = '{cardType}'";

                    if (onlyActive == true)
                    {
                        command.CommandText += $"AND {PlasticsTable.COLUMN_IS_ACTIVE} = 'TRUE'";
                    }

                    var sqlReader = command.ExecuteReader();


                    while (sqlReader!.Read())
                    {
                        var dataEntry = PlasticsMapperProfile.MapSqlDataToTableEntry(sqlReader);

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
    }
}

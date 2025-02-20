using BankingAppDataTier.Contracts.Configs;
using BankingAppDataTier.Contracts.Constants.Database;
using BankingAppDataTier.Contracts.Database;
using BankingAppDataTier.Contracts.Enums;
using BankingAppDataTier.Contracts.Providers;
using BankingAppDataTier.Database;
using BankingAppDataTier.MapperProfiles;
using Microsoft.Data.SqlClient;

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
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var (transaction, command) = SqlDatabaseHelper.InitialzieSqlTransaction(connection);

                try
                {
                    command.CommandText = $"IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[{PlasticsTable.TABLE_NAME}]')  AND type in (N'U')) " +
                        $"BEGIN " +
                        $"CREATE TABLE {PlasticsTable.TABLE_NAME} " +
                        $"(" +
                        $"{PlasticsTable.COLUMN_ID} VARCHAR(64) NOT NULL," +
                        $"{PlasticsTable.COLUMN_TYPE} VARCHAR(64) NOT NULL," +
                        $"{PlasticsTable.COLUMN_NAME} VARCHAR(64) NOT NULL," +
                        $"{PlasticsTable.COLUMN_CASHBACK} DECIMAL(5,2)," +
                        $"{PlasticsTable.COLUMN_COMISSION} DECIMAL(5,2)," +
                        $"{PlasticsTable.COLUMN_IMAGE} VARCHAR(MAX)," +
                        $"PRIMARY KEY ({PlasticsTable.COLUMN_ID} )" +
                        $") " +
                        $"END";

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
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var (transaction, command) = SqlDatabaseHelper.InitialzieSqlTransaction(connection);

                try
                {
                    command.CommandText = $"INSERT INTO {PlasticsTable.TABLE_NAME} " +
                        $"({PlasticsTable.COLUMN_ID}, {PlasticsTable.COLUMN_TYPE}, {PlasticsTable.COLUMN_NAME}, {PlasticsTable.COLUMN_CASHBACK}, " +
                        $"{PlasticsTable.COLUMN_COMISSION}, {PlasticsTable.COLUMN_IMAGE}) " +
                        $"VALUES " +
                        $"('{entry.Id}', '{entry.CardType}', '{entry.Name}', '{entry.Cashback.GetValueOrDefault()}', " +
                        $"'{entry.Commission.GetValueOrDefault()}', '{entry.Image}');";

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
            using (SqlConnection connection = new SqlConnection(connectionString))
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
            using (SqlConnection connection = new SqlConnection(connectionString))
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
                    $"{PlasticsTable.COLUMN_IMAGE} = '{entry.Image}'" +
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
            using (SqlConnection connection = new SqlConnection(connectionString))
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
            using (SqlConnection connection = new SqlConnection(connectionString))
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

        public List<PlasticTableEntry> GetPlasticsOfCardType(CardType type)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                List<PlasticTableEntry> result = null;

                connection.Open();

                var (transaction, command) = SqlDatabaseHelper.InitialzieSqlTransaction(connection);

                try
                {
                    var typeAsString = PlasticsMapperProfile.MapCardTypeEnumToStringCardType(type);

                    command.CommandText = $"SELECT * FROM {PlasticsTable.TABLE_NAME} WHERE {PlasticsTable.COLUMN_TYPE} = '{typeAsString}'";

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

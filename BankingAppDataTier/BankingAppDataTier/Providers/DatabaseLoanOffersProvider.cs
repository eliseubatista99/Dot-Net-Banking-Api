using BankingAppDataTier.Contracts.Configs;
using BankingAppDataTier.Contracts.Constants.Database;
using BankingAppDataTier.Contracts.Database;
using BankingAppDataTier.Contracts.Providers;
using BankingAppDataTier.Database;
using BankingAppDataTier.MapperProfiles;
using Npgsql;

namespace BankingAppDataTier.Providers
{
    public class DatabaseLoanOffersProvider : IDatabaseLoanOfferProvider
    {

        private IConfiguration Configuration;

        private string connectionString;

        public DatabaseLoanOffersProvider(IConfiguration configuration)
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
                    command.CommandText = $"CREATE TABLE IF NOT EXISTS {LoanOffersTable.TABLE_NAME}" +
                        $"(" +
                        $"{LoanOffersTable.COLUMN_ID} VARCHAR(64) NOT NULL," +
                        $"{LoanOffersTable.COLUMN_NAME} VARCHAR(64) NOT NULL," +
                        $"{LoanOffersTable.COLUMN_DESCRIPTION} VARCHAR(64) NOT NULL," +
                        $"{LoanOffersTable.COLUMN_TYPE} CHAR(2) NOT NULL," +
                        $"{LoanOffersTable.COLUMN_MAX_EFFORT} INTEGER NOT NULL," +
                        $"{LoanOffersTable.COLUMN_INTEREST} DECIMAL(5,2) NOT NULL," +
                        $"{LoanOffersTable.COLUMN_IS_ACTIVE} BOOL NOT NULL," +
                        $"PRIMARY KEY ({LoanOffersTable.COLUMN_ID} )" +
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

        public bool Add(LoanOfferTableEntry entry)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                var (transaction, command) = SqlDatabaseHelper.InitialzieSqlTransaction(connection);

                try
                {
                    command.CommandText = $"INSERT INTO {LoanOffersTable.TABLE_NAME} " +
                        $"({LoanOffersTable.COLUMN_ID}, {LoanOffersTable.COLUMN_NAME}, {LoanOffersTable.COLUMN_DESCRIPTION}, {LoanOffersTable.COLUMN_TYPE}," +
                        $" {LoanOffersTable.COLUMN_MAX_EFFORT}, {LoanOffersTable.COLUMN_INTEREST}, {LoanOffersTable.COLUMN_IS_ACTIVE}) " +
                        $"VALUES " +
                        $"('{entry.Id}', '{entry.Name}', '{entry.Description}','{entry.LoanType}', '{entry.MaxEffort}', '{entry.Interest}', '{entry.IsActive}');";

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
                    command.CommandText = $"DELETE FROM {LoanOffersTable.TABLE_NAME} WHERE {LoanOffersTable.COLUMN_ID} = '{id}'";

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

        public bool Edit(LoanOfferTableEntry entry)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                var (transaction, command) = SqlDatabaseHelper.InitialzieSqlTransaction(connection);

                try
                {
                    command.CommandText = $"UPDATE {LoanOffersTable.TABLE_NAME} " +
                    $"SET {LoanOffersTable.COLUMN_TYPE} = '{entry.LoanType}', " +
                    $"{LoanOffersTable.COLUMN_NAME} = '{entry.Name}', " +
                    $"{LoanOffersTable.COLUMN_DESCRIPTION} = '{entry.Description}', " +
                    $"{LoanOffersTable.COLUMN_MAX_EFFORT} = '{entry.MaxEffort}', " +
                    $"{LoanOffersTable.COLUMN_INTEREST} = '{entry.Interest}', " +
                    $"{LoanOffersTable.COLUMN_IS_ACTIVE} = '{entry.IsActive}' " +
                    $"WHERE {LoanOffersTable.COLUMN_ID} = '{entry.Id}';";

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

        public List<LoanOfferTableEntry> GetAll()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                List<LoanOfferTableEntry> result = new List<LoanOfferTableEntry>();

                connection.Open();

                var (transaction, command) = SqlDatabaseHelper.InitialzieSqlTransaction(connection);

                try
                {
                    command.CommandText = $"SELECT * FROM {LoanOffersTable.TABLE_NAME}";

                    var sqlReader = command.ExecuteReader();

                    while (sqlReader!.Read())
                    {
                        var dataEntry = LoanOffersMapperProfile.MapSqlDataToTableEntry(sqlReader);

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

        public LoanOfferTableEntry? GetById(string id)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                LoanOfferTableEntry? result = null;

                connection.Open();

                var (transaction, command) = SqlDatabaseHelper.InitialzieSqlTransaction(connection);

                try
                {
                    command.CommandText = $"SELECT * FROM {LoanOffersTable.TABLE_NAME} WHERE {LoanOffersTable.COLUMN_ID} = '{id}'";

                    var sqlReader = command.ExecuteReader();

                    if (sqlReader.HasRows)
                    {
                        sqlReader.Read();
                        result = LoanOffersMapperProfile.MapSqlDataToTableEntry(sqlReader);
                    }

                    return result;
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public List<LoanOfferTableEntry> GetByType(string loanType, bool onlyActive = false)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                List<LoanOfferTableEntry> result = new List<LoanOfferTableEntry>();

                connection.Open();

                var (transaction, command) = SqlDatabaseHelper.InitialzieSqlTransaction(connection);

                try
                {
                    var accountIdsOfClient = new List<string>();

                    command.CommandText = $"SELECT * FROM {LoanOffersTable.TABLE_NAME} WHERE {LoanOffersTable.COLUMN_TYPE} = '{loanType}'";

                    if(onlyActive == true)
                    {
                        command.CommandText += $"AND {LoanOffersTable.COLUMN_IS_ACTIVE} = 'TRUE'";
                    }

                    using (var sqlReader = command.ExecuteReader())
                    {
                        while (sqlReader!.Read())
                        {
                            var dataEntry = LoanOffersMapperProfile.MapSqlDataToTableEntry(sqlReader);

                            result.Add(dataEntry);
                        }
                    }

                    return result;
                }
                catch (Exception ex)
                {
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
                    command.CommandText = $"DELETE FROM {LoanOffersTable.TABLE_NAME} WHERE 1=1";

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

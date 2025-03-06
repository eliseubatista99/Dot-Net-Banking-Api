using BankingAppDataTier.Contracts.Configs;
using BankingAppDataTier.Contracts.Constants.Database;
using BankingAppDataTier.Contracts.Database;
using BankingAppDataTier.Contracts.Providers;
using BankingAppDataTier.Database;
using BankingAppDataTier.MapperProfiles;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using Npgsql;

namespace BankingAppDataTier.Providers
{
    public class DatabaseLoanProvider : IDatabaseLoansProvider
    {

        private IConfiguration Configuration;

        private string connectionString;

        public DatabaseLoanProvider(IConfiguration configuration)
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
                    command.CommandText = $"CREATE TABLE IF NOT EXISTS {LoansTable.TABLE_NAME}" +
                        $"(" +
                        $"{LoansTable.COLUMN_ID} VARCHAR(64) NOT NULL," +
                        $"{LoansTable.COLUMN_RELATED_OFFER} VARCHAR(64) NOT NULL," +
                        $"{LoansTable.COLUMN_DURATION} INTEGER NOT NULL," +
                        $"{LoansTable.COLUMN_AMOUNT} DECIMAL(20,2) NOT NULL," +
                        $"PRIMARY KEY ({LoansTable.COLUMN_ID} )," +
                        $"FOREIGN KEY ({LoansTable.COLUMN_RELATED_OFFER}) REFERENCES {LoanOffersTable.TABLE_NAME}({LoanOffersTable.COLUMN_ID})" +
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

        public bool Add(LoanTableEntry entry)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                var (transaction, command) = SqlDatabaseHelper.InitialzieSqlTransaction(connection);

                try
                {
                    command.CommandText = $"INSERT INTO {LoansTable.TABLE_NAME} " +
                        $"({LoansTable.COLUMN_ID}, {LoansTable.COLUMN_START_DATE}, {LoansTable.COLUMN_RELATED_OFFER}, {LoansTable.COLUMN_DURATION}, {LoansTable.COLUMN_AMOUNT}) " +
                        $"VALUES " +
                        $"('{entry.Id}', {entry.StartDate}', '{entry.RelatedOffer}', '{entry.Duration}', '{entry.Amount}');";

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
                    command.CommandText = $"DELETE FROM {LoansTable.TABLE_NAME} WHERE {LoansTable.COLUMN_ID} = '{id}'";

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

        public bool Edit(LoanTableEntry entry)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                var (transaction, command) = SqlDatabaseHelper.InitialzieSqlTransaction(connection);

                try
                {
                    command.CommandText = $"UPDATE {LoansTable.TABLE_NAME} " +
                    $"SET {LoansTable.COLUMN_ID} = '{entry.Id}', " +
                    $"SET {LoansTable.COLUMN_START_DATE} = '{entry.StartDate}', " +
                    $"{LoansTable.COLUMN_RELATED_OFFER} = '{entry.RelatedOffer}', " +
                    $"{LoansTable.COLUMN_DURATION} = '{entry.Duration}', " +
                    $"{LoansTable.COLUMN_AMOUNT} = '{entry.Amount}' " +
                    $"WHERE {LoansTable.COLUMN_ID} = '{entry.Id}';";

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

        public List<LoanTableEntry> GetAll()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                List<LoanTableEntry> result = new List<LoanTableEntry>();

                connection.Open();

                var (transaction, command) = SqlDatabaseHelper.InitialzieSqlTransaction(connection);

                try
                {
                    command.CommandText = $"SELECT * FROM {LoansTable.TABLE_NAME}";

                    var sqlReader = command.ExecuteReader();

                    while (sqlReader!.Read())
                    {
                        var dataEntry = LoansMapperProfile.MapSqlDataToTableEntry(sqlReader);

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

        public LoanTableEntry? GetById(string id)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                LoanTableEntry? result = null;

                connection.Open();

                var (transaction, command) = SqlDatabaseHelper.InitialzieSqlTransaction(connection);

                try
                {
                    command.CommandText = $"SELECT * FROM {LoansTable.TABLE_NAME} WHERE {LoansTable.COLUMN_ID} = '{id}'";

                    var sqlReader = command.ExecuteReader();

                    if (sqlReader.HasRows)
                    {
                        sqlReader.Read();
                        result = LoansMapperProfile.MapSqlDataToTableEntry(sqlReader);
                    }

                    return result;
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public List<LoanTableEntry> GetByOffer(string relatedOffer)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                List<LoanTableEntry> result = new List<LoanTableEntry>();

                connection.Open();

                var (transaction, command) = SqlDatabaseHelper.InitialzieSqlTransaction(connection);

                try
                {
                    var cardIdsOfAccount = new List<string>();

                    command.CommandText = $"SELECT * FROM {LoansTable.TABLE_NAME} WHERE {LoansTable.COLUMN_RELATED_OFFER} = '{relatedOffer}'";

                    using (var sqlReader = command.ExecuteReader())
                    {
                        while (sqlReader!.Read())
                        {
                            var dataEntry = LoansMapperProfile.MapSqlDataToTableEntry(sqlReader);

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
    }
}
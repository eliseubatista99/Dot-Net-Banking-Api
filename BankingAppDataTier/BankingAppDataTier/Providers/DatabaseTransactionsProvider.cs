using BankingAppDataTier.Contracts.Configs;
using BankingAppDataTier.Contracts.Constants.Database;
using BankingAppDataTier.Contracts.Database;
using BankingAppDataTier.Contracts.Providers;
using BankingAppDataTier.Database;
using BankingAppDataTier.MapperProfiles;
using Npgsql;

namespace BankingAppDataTier.Providers
{
    public class DatabaseTransactionsProvider : IDatabaseTransactionsProvider
    {
        private IConfiguration Configuration;

        private string connectionString;

        public DatabaseTransactionsProvider(IConfiguration configuration)
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
                    command.CommandText = $"CREATE TABLE IF NOT EXISTS {TransactionsTable.TABLE_NAME}" +
                        $"(" +
                        $"{TransactionsTable.COLUMN_ID} VARCHAR(64) NOT NULL," +
                        $"{TransactionsTable.COLUMN_TRANSACTION_DATE} DATE NOT NULL," +
                        $"{TransactionsTable.COLUMN_DESCRIPTION} VARCHAR(64)," +
                        $"{TransactionsTable.COLUMN_SOURCE_ACCOUNT} VARCHAR(64)," +
                        $"{TransactionsTable.COLUMN_DESTINATION_NAME} VARCHAR(64) NOT NULL," +
                        $"{TransactionsTable.COLUMN_DESTINATION_ACCOUNT} VARCHAR(64)," +
                        $"{TransactionsTable.COLUMN_SOURCE_CARD} VARCHAR(64)," +
                        $"{TransactionsTable.COLUMN_AMOUNT} DECIMAL(20,2) NOT NULL," +
                        $"{TransactionsTable.COLUMN_FEES} DECIMAL(10,2)," +
                        $"{TransactionsTable.COLUMN_URGENT} BOOL NOT NULL," +
                        $"PRIMARY KEY ({TransactionsTable.COLUMN_ID} )" +
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

        public bool Add(TransactionTableEntry entry)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                var (transaction, command) = SqlDatabaseHelper.InitialzieSqlTransaction(connection);

                try
                {
                    command.CommandText = this.BuildAddCommand(entry);

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
                    command.CommandText = $"DELETE FROM {TransactionsTable.TABLE_NAME} WHERE {TransactionsTable.COLUMN_ID} = '{id}'";

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

        public bool Edit(TransactionTableEntry entry)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                var (transaction, command) = SqlDatabaseHelper.InitialzieSqlTransaction(connection);

                try
                {
                    command.CommandText = this.BuildEditCommand(entry);

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

        public List<TransactionTableEntry> GetAll()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                List<TransactionTableEntry> result = new List<TransactionTableEntry>();

                connection.Open();

                var (transaction, command) = SqlDatabaseHelper.InitialzieSqlTransaction(connection);

                try
                {
                    command.CommandText = $"SELECT * FROM {TransactionsTable.TABLE_NAME}";

                    var sqlReader = command.ExecuteReader();

                    while (sqlReader!.Read())
                    {
                        var dataEntry = TransactionsMapperProfile.MapSqlDataToTableEntry(sqlReader);

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

        public List<TransactionTableEntry> GetByDestinationAccount(string account)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                List<TransactionTableEntry> result = new List<TransactionTableEntry>();

                connection.Open();

                var (transaction, command) = SqlDatabaseHelper.InitialzieSqlTransaction(connection);

                try
                {
                    command.CommandText = $"SELECT * FROM {TransactionsTable.TABLE_NAME} WHERE {TransactionsTable.COLUMN_DESTINATION_ACCOUNT} = '{account}'";

                    using (var sqlReader = command.ExecuteReader())
                    {
                        while (sqlReader!.Read())
                        {
                            var dataEntry = TransactionsMapperProfile.MapSqlDataToTableEntry(sqlReader);

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

        public TransactionTableEntry? GetById(string id)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                TransactionTableEntry? result = null;

                connection.Open();

                var (transaction, command) = SqlDatabaseHelper.InitialzieSqlTransaction(connection);

                try
                {
                    command.CommandText = $"SELECT * FROM {AccountsTable.TABLE_NAME} WHERE {AccountsTable.COLUMN_ID} = '{id}'";

                    var sqlReader = command.ExecuteReader();

                    if (sqlReader.HasRows)
                    {
                        sqlReader.Read();
                        result = TransactionsMapperProfile.MapSqlDataToTableEntry(sqlReader);
                    }

                    return result;
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public List<TransactionTableEntry> GetBySourceAccount(string account)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                List<TransactionTableEntry> result = new List<TransactionTableEntry>();

                connection.Open();

                var (transaction, command) = SqlDatabaseHelper.InitialzieSqlTransaction(connection);

                try
                {
                    command.CommandText = $"SELECT * FROM {TransactionsTable.TABLE_NAME} WHERE {TransactionsTable.COLUMN_SOURCE_ACCOUNT} = '{account}'";

                    using (var sqlReader = command.ExecuteReader())
                    {
                        while (sqlReader!.Read())
                        {
                            var dataEntry = TransactionsMapperProfile.MapSqlDataToTableEntry(sqlReader);

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

        public List<TransactionTableEntry> GetBySourceCard(string card)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                List<TransactionTableEntry> result = new List<TransactionTableEntry>();

                connection.Open();

                var (transaction, command) = SqlDatabaseHelper.InitialzieSqlTransaction(connection);

                try
                {
                    command.CommandText = $"SELECT * FROM {TransactionsTable.TABLE_NAME} WHERE {TransactionsTable.COLUMN_SOURCE_CARD} = '{card}'";

                    using (var sqlReader = command.ExecuteReader())
                    {
                        while (sqlReader!.Read())
                        {
                            var dataEntry = TransactionsMapperProfile.MapSqlDataToTableEntry(sqlReader);

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

        public List<TransactionTableEntry> GetByDate(DateTime date)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                List<TransactionTableEntry> result = new List<TransactionTableEntry>();

                connection.Open();

                var (transaction, command) = SqlDatabaseHelper.InitialzieSqlTransaction(connection);

                try
                {
                    command.CommandText = $"SELECT * FROM {TransactionsTable.TABLE_NAME} WHERE {TransactionsTable.COLUMN_TRANSACTION_DATE} = '{date}'";

                    using (var sqlReader = command.ExecuteReader())
                    {
                        while (sqlReader!.Read())
                        {
                            var dataEntry = TransactionsMapperProfile.MapSqlDataToTableEntry(sqlReader);

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
                    command.CommandText = $"DELETE FROM {TransactionsTable.TABLE_NAME} WHERE 1=1";

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


        private string BuildAddCommand(TransactionTableEntry entry)
        {
            var result = $"INSERT INTO {TransactionsTable.TABLE_NAME} " +
                $"({TransactionsTable.COLUMN_ID}, {TransactionsTable.COLUMN_TRANSACTION_DATE}, {TransactionsTable.COLUMN_DESCRIPTION}, {TransactionsTable.COLUMN_SOURCE_ACCOUNT}," +
                $" {TransactionsTable.COLUMN_DESTINATION_NAME}, {TransactionsTable.COLUMN_DESTINATION_ACCOUNT}, {TransactionsTable.COLUMN_SOURCE_CARD}," +
                $" {TransactionsTable.COLUMN_AMOUNT}, {TransactionsTable.COLUMN_FEES}, {TransactionsTable.COLUMN_URGENT}";

            result += $") VALUES " +
                $"('{entry.Id}', '{entry.TransactionDate}','{entry.Description}', '{entry.SourceAccount}', '{entry.DestinationName}', '{entry.DestinationAccount}'," +
                $" '{entry.SourceCard}', '{entry.Amount}', '{entry.Fees}', '{entry.Urgent}'";

            result += ");";

            return result;
        }

        private string BuildEditCommand(TransactionTableEntry entry)
        {

            var commandText = $"CREATE TABLE IF NOT EXISTS {TransactionsTable.TABLE_NAME}" +
                        $"(" +
                        $"{TransactionsTable.COLUMN_ID} VARCHAR(64) NOT NULL," +
                        $"{TransactionsTable.COLUMN_TRANSACTION_DATE} DATE NOT NULL," +
                        $"{TransactionsTable.COLUMN_DESCRIPTION} VARCHAR(64)" +
                        $"{TransactionsTable.COLUMN_SOURCE_ACCOUNT} VARCHAR(64)," +
                        $"{TransactionsTable.COLUMN_DESTINATION_NAME} VARCHAR(64) NOT NULL," +
                        $"{TransactionsTable.COLUMN_DESTINATION_ACCOUNT} VARCHAR(64)," +
                        $"{TransactionsTable.COLUMN_SOURCE_CARD} VARCHAR(64)," +
                        $"{TransactionsTable.COLUMN_AMOUNT} DECIMAL(20,2) NOT NULL," +
                        $"{TransactionsTable.COLUMN_FEES} DECIMAL(5,2)," +
                        $"{TransactionsTable.COLUMN_URGENT} BOOL NOT NULL," +
                        $"PRIMARY KEY ({TransactionsTable.COLUMN_ID} )" +
                        $") ";

            var result = $"UPDATE {TransactionsTable.TABLE_NAME} " +
                    $"SET {TransactionsTable.COLUMN_TRANSACTION_DATE} = '{entry.TransactionDate}', " +
                    $"{TransactionsTable.COLUMN_DESTINATION_NAME} = '{entry.DestinationName}', " +
                    $"{TransactionsTable.COLUMN_AMOUNT} = '{entry.Amount}', " +
                    $"{TransactionsTable.COLUMN_URGENT} = '{entry.Urgent}'";


            if (entry.Description != null)
            {
                result += $", {TransactionsTable.COLUMN_DESCRIPTION} = '{entry.Description}'";
            }

            if (entry.SourceAccount != null)
            {
                result += $", {TransactionsTable.COLUMN_SOURCE_ACCOUNT} = '{entry.SourceAccount}'";
            }

            if (entry.DestinationAccount != null)
            {
                result += $", {TransactionsTable.COLUMN_DESTINATION_ACCOUNT} = '{entry.DestinationAccount}'";
            }

            if (entry.SourceCard != null)
            {
                result += $", {TransactionsTable.COLUMN_SOURCE_CARD} = '{entry.SourceCard}'";
            }

            if (entry.Fees != null)
            {
                result += $", {TransactionsTable.COLUMN_FEES} = '{entry.Fees}'";
            }

            result += $"WHERE {TransactionsTable.COLUMN_ID} = '{entry.Id}';";

            return result;
        }
    }
}
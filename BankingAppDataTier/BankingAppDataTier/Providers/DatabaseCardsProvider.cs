using BankingAppDataTier.Contracts.Configs;
using BankingAppDataTier.Contracts.Constants.Database;
using BankingAppDataTier.Contracts.Database;
using BankingAppDataTier.Contracts.Providers;
using BankingAppDataTier.Database;
using BankingAppDataTier.MapperProfiles;
using Npgsql;

namespace BankingAppDataTier.Providers
{
    public class DatabaseCardsProvider : IDatabaseCardsProvider
    {

        private IConfiguration Configuration;

        private string connectionString;

        public DatabaseCardsProvider(IConfiguration configuration)
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
                    command.CommandText = $"CREATE TABLE IF NOT EXISTS {CardsTable.TABLE_NAME}" +
                        $"(" +
                        $"{CardsTable.COLUMN_ID} VARCHAR(64) NOT NULL," +
                        $"{CardsTable.COLUMN_NAME} VARCHAR(64) NOT NULL," +
                        $"{CardsTable.COLUMN_RELATED_ACCOUNT_ID} VARCHAR(64) NOT NULL," +
                        $"{CardsTable.COLUMN_PLASTIC_ID} VARCHAR(64) NOT NULL," +
                        $"{CardsTable.COLUMN_BALANCE} DECIMAL(20,2)," +
                        $"{CardsTable.COLUMN_PAYMENT_DAY} INT," +
                        $"{CardsTable.COLUMN_REQUEST_DATE} DATE NOT NULL," +
                        $"{CardsTable.COLUMN_ACTIVATION_DATE} DATE," +
                        $"{CardsTable.COLUMN_EXPIRATION_DATE} DATE NOT NULL," +
                        $"PRIMARY KEY ({CardsTable.COLUMN_ID} )," +
                        $"FOREIGN KEY ({CardsTable.COLUMN_RELATED_ACCOUNT_ID}) REFERENCES {AccountsTable.TABLE_NAME}({AccountsTable.COLUMN_ID})," +
                        $"FOREIGN KEY ({CardsTable.COLUMN_PLASTIC_ID}) REFERENCES {PlasticsTable.TABLE_NAME}({PlasticsTable.COLUMN_ID})" +
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


        public bool Add(CardsTableEntry entry)
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
                catch (Exception)
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
                    command.CommandText = $"DELETE FROM {CardsTable.TABLE_NAME} WHERE {CardsTable.COLUMN_ID} = '{id}'";

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

        public bool Edit(CardsTableEntry entry)
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

        public List<CardsTableEntry> GetAll()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                List<CardsTableEntry> result = new List<CardsTableEntry>();

                connection.Open();

                var (transaction, command) = SqlDatabaseHelper.InitialzieSqlTransaction(connection);

                try
                {
                    command.CommandText = $"SELECT * FROM {CardsTable.TABLE_NAME}";

                    var sqlReader = command.ExecuteReader();

                    while (sqlReader!.Read())
                    {
                        var dataEntry = CardsMapperProfile.MapSqlDataToTableEntry(sqlReader);

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

        public CardsTableEntry? GetById(string id)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                CardsTableEntry? result = null;

                connection.Open();

                var (transaction, command) = SqlDatabaseHelper.InitialzieSqlTransaction(connection);

                try
                {
                    command.CommandText = $"SELECT * FROM {CardsTable.TABLE_NAME} WHERE {CardsTable.COLUMN_ID} = '{id}'";

                    var sqlReader = command.ExecuteReader();

                    if (sqlReader.HasRows)
                    {
                        sqlReader.Read();
                        result = CardsMapperProfile.MapSqlDataToTableEntry(sqlReader);
                    }

                    return result;
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public List<CardsTableEntry> GetCardsOfAccount(string accountId)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                List<CardsTableEntry> result = new List<CardsTableEntry>();

                connection.Open();

                var (transaction, command) = SqlDatabaseHelper.InitialzieSqlTransaction(connection);

                try
                {
                    var cardIdsOfAccount = new List<string>();

                    command.CommandText = $"SELECT * FROM {CardsTable.TABLE_NAME} WHERE {CardsTable.COLUMN_RELATED_ACCOUNT_ID} = '{accountId}'";

                    using (var sqlReader = command.ExecuteReader())
                    {
                        while (sqlReader!.Read())
                        {
                            var dataEntry = CardsMapperProfile.MapSqlDataToTableEntry(sqlReader);

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

        public List<CardsTableEntry> GetCardsWithPlastic(string plasticId)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                List<CardsTableEntry> result = new List<CardsTableEntry>();

                connection.Open();

                var (transaction, command) = SqlDatabaseHelper.InitialzieSqlTransaction(connection);

                try
                {
                    var cardIdsOfAccount = new List<string>();

                    command.CommandText = $"SELECT * FROM {CardsTable.TABLE_NAME} WHERE {CardsTable.COLUMN_PLASTIC_ID} = '{plasticId}'";

                    using (var sqlReader = command.ExecuteReader())
                    {
                        while (sqlReader!.Read())
                        {
                            var dataEntry = CardsMapperProfile.MapSqlDataToTableEntry(sqlReader);

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
                    command.CommandText = $"DROP TABLE [ IF EXISTS ] {CardsTable.TABLE_NAME}";

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

        private string BuildAddCommand(CardsTableEntry entry)
        {
            var result = $"INSERT INTO {CardsTable.TABLE_NAME} " +
                $"({CardsTable.COLUMN_ID}, {CardsTable.COLUMN_NAME}, {CardsTable.COLUMN_PLASTIC_ID}, {CardsTable.COLUMN_RELATED_ACCOUNT_ID}, " +
                $"{CardsTable.COLUMN_REQUEST_DATE}, {CardsTable.COLUMN_EXPIRATION_DATE}";

            if (entry.ActivationDate != null)
            {
                result += $", {CardsTable.COLUMN_ACTIVATION_DATE}";
            }

            if (entry.PaymentDay != null)
            {
                result += $", {CardsTable.COLUMN_PAYMENT_DAY}";
            }

            if (entry.Balance != null)
            {
                result += $", {CardsTable.COLUMN_BALANCE}";
            }


            result += $") VALUES " +
                $"('{entry.Id}', '{entry.Name}', '{entry.PlasticId}', '{entry.RelatedAccountID}', " +
                $"'{entry.RequestDate.ToString("yyyy-MM-dd")}', '{entry.ExpirationDate.ToString("yyyy-MM-dd")}'";

            if (entry.ActivationDate != null)
            {
                result += $", '{entry.ActivationDate.GetValueOrDefault().ToString("yyyy-MM-dd")}'";
            }

            if (entry.PaymentDay != null)
            {
                result += $", '{entry.PaymentDay.GetValueOrDefault()}'";
            }

            if (entry.Balance != null)
            {
                result += $", '{entry.Balance}'";
            }

            result += ");";

            return result;
        }

        private string BuildEditCommand(CardsTableEntry entry)
        {
            var result = $"UPDATE {CardsTable.TABLE_NAME} " +
                    $"SET {CardsTable.COLUMN_RELATED_ACCOUNT_ID} = '{entry.RelatedAccountID}', " +
                    $"{CardsTable.COLUMN_NAME} = '{entry.Name}', " +
                    $"{CardsTable.COLUMN_PLASTIC_ID} = '{entry.PlasticId}', " +
                    $"{CardsTable.COLUMN_REQUEST_DATE} = '{entry.RequestDate.ToString("yyyy-MM-dd")}', " +
                    $"{CardsTable.COLUMN_EXPIRATION_DATE} = '{entry.ExpirationDate.ToString("yyyy-MM-dd")}'";


            if (entry.ActivationDate != null)
            {
                result += $", {CardsTable.COLUMN_ACTIVATION_DATE} = '{entry.ActivationDate.GetValueOrDefault().ToString("yyyy-MM-dd")}'";
            }

            if (entry.Balance != null)
            {
                result += $", {CardsTable.COLUMN_BALANCE} = '{entry.Balance}'";
            }

            if (entry.PaymentDay != null)
            {
                result += $", {CardsTable.COLUMN_PAYMENT_DAY} = '{entry.PaymentDay.GetValueOrDefault()}'";
            }

            result += $"WHERE {CardsTable.COLUMN_ID} = '{entry.Id}';";

            return result;
        }

        
    }
}

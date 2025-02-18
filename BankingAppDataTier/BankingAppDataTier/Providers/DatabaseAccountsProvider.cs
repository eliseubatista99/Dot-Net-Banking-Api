using BankingAppDataTier.Contracts.Configs;
using BankingAppDataTier.Contracts.Constants;
using BankingAppDataTier.Contracts.Database;
using BankingAppDataTier.Contracts.Providers;
using BankingAppDataTier.Database;
using BankingAppDataTier.MapperProfiles;
using Microsoft.Data.SqlClient;
using System.Xml;

namespace BankingAppDataTier.Providers
{
    public class DatabaseAccountsProvider : IDatabaseAccountsProvider
    {

        private IConfiguration Configuration;

        private string connectionString;

        public DatabaseAccountsProvider(IConfiguration configuration)
        {
            this.Configuration = configuration;

            connectionString = Configuration.GetSection(DatabaseConfigs.DatabaseSection).GetValue<string>(DatabaseConfigs.DatabaseConnection);
        }

        public bool CreateTableIfNotExists()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var(transaction, command) = SqlDatabaseHelper.InitialzieSqlTransaction(connection);
           
                try
                {
                    command.CommandText = $"IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[{AccountsTable.TABLE_NAME}]')  AND type in (N'U')) " +
                        $"BEGIN " +
                        $"CREATE TABLE {AccountsTable.TABLE_NAME} " +
                        $"(" +
                        $"{AccountsTable.COLUMN_ID} VARCHAR(64) NOT NULL," +
                        $"{AccountsTable.COLUMN_TYPE} CHAR(2) NOT NULL," +
                        $"{AccountsTable.COLUMN_BALANCE} DECIMAL(20,2) NOT NULL," +
                        $"{AccountsTable.COLUMN_NAME} VARCHAR(64) NOT NULL," +
                        $"{AccountsTable.COLUMN_IMAGE} VARCHAR(MAX)," +
                        $"{AccountsTable.COLUMN_SOURCE_ACCOUNT_ID} VARCHAR(64)," +
                        $"{AccountsTable.COLUMN_DURATION} INTEGER," +
                        $"{AccountsTable.COLUMN_INTEREST} DECIMAL(3,2)," +
                        $"PRIMARY KEY ({AccountsTable.COLUMN_ID} )" +
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

        public List<AccountsTableEntry> GetAccountsOfClient(string clientId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                List<AccountsTableEntry> result = new List<AccountsTableEntry>();

                connection.Open();

                var (transaction, command) = SqlDatabaseHelper.InitialzieSqlTransaction(connection);

                try
                {
                    command.CommandText = $"SELECT * FROM {ClientAccountBridgeTable.TABLE_NAME} WHERE {ClientAccountBridgeTable.COLUMN_CLIENT_ID} = '{clientId}'";

                    var sqlReaderClientAccounts = command.ExecuteReader();

                    while (sqlReaderClientAccounts!.Read())
                    {
                        var clientAccountEntry = ClientAccountBridgeMapperProfile.MapSqlDataToClientAccountBridgeTableEntry(sqlReaderClientAccounts);

                        command.CommandText = $"SELECT * FROM {AccountsTable.TABLE_NAME} WHERE {AccountsTable.COLUMN_ID} = '{clientAccountEntry.AccountId}'";

                        var sqlReaderAccount = command.ExecuteReader();
                        sqlReaderAccount.Read();

                        var dataEntry = AccountsMapperProfile.MapSqlDataToAccountsTableEntry(sqlReaderAccount);

                        result.Add(dataEntry);
                    }

                    return result;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public List<AccountsTableEntry> GetAll()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                List<AccountsTableEntry> result = new List<AccountsTableEntry>();

                connection.Open();

                var (transaction, command) = SqlDatabaseHelper.InitialzieSqlTransaction(connection);

                try
                {
                    command.CommandText = $"SELECT * FROM {AccountsTable.TABLE_NAME}";

                    var sqlReader = command.ExecuteReader();

                    while (sqlReader!.Read())
                    {
                        var dataEntry = AccountsMapperProfile.MapSqlDataToAccountsTableEntry(sqlReader);

                        result.Add(dataEntry);
                    }

                    return result;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public AccountsTableEntry? GetAccountOfId(string id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                AccountsTableEntry? result = null;

                connection.Open();

                var (transaction, command) = SqlDatabaseHelper.InitialzieSqlTransaction(connection);

                try
                {
                    command.CommandText = $"SELECT * FROM {AccountsTable.TABLE_NAME} WHERE {AccountsTable.COLUMN_ID} = '{id}'";

                    var sqlReader = command.ExecuteReader();

                    if (sqlReader.HasRows)
                    {
                        sqlReader.Read();
                        result = AccountsMapperProfile.MapSqlDataToAccountsTableEntry(sqlReader);
                    }

                    return result;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public bool Add(AccountsTableEntry entry, string clientId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var (transaction, command) = SqlDatabaseHelper.InitialzieSqlTransaction(connection);

                try
                {
                    command.CommandText =  this.BuildAddCommand(entry);

                    command.ExecuteNonQuery();

                    var clientAccountBridgeEntry = new ClientAccountBridgeTableEntry() { Id = $"{clientId}_{entry.AccountId}", AccountId = entry.AccountId, ClientId = clientId };

                    command.CommandText = $"INSERT INTO {ClientAccountBridgeTable.TABLE_NAME} " +
                        $"({ClientAccountBridgeTable.COLUMN_ID}, {ClientAccountBridgeTable.COLUMN_ACCOUNT_ID}, {ClientAccountBridgeTable.COLUMN_CLIENT_ID}) " +
                        $"VALUES " +
                        $"('{clientAccountBridgeEntry.Id}', '{clientAccountBridgeEntry.AccountId}', '{clientAccountBridgeEntry.ClientId}');";

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

        public bool Edit(AccountsTableEntry entry)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
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

        public bool Delete(string id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var (transaction, command) = SqlDatabaseHelper.InitialzieSqlTransaction(connection);

                try
                {
                    command.CommandText = $"DELETE FROM {ClientAccountBridgeTable.TABLE_NAME} WHERE {ClientAccountBridgeTable.COLUMN_ID} = '{id}'";

                    command.ExecuteNonQuery();

                    command.CommandText = $"DELETE FROM {AccountsTable.TABLE_NAME} WHERE {AccountsTable.COLUMN_ID} = '{id}'";

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


        private string BuildAddCommand(AccountsTableEntry entry)
        {
            var result = $"INSERT INTO {AccountsTable.TABLE_NAME} " +
                $"({AccountsTable.COLUMN_ID}, {AccountsTable.COLUMN_TYPE}, {AccountsTable.COLUMN_BALANCE}, {AccountsTable.COLUMN_NAME}, " +
                $"{AccountsTable.COLUMN_IMAGE}";

            if (entry.AccountType == AccountsTable.ACCOUNT_TYPE_INVESTMENTS)
            {
                result += $", {AccountsTable.COLUMN_SOURCE_ACCOUNT_ID}, {AccountsTable.COLUMN_DURATION}, {AccountsTable.COLUMN_INTEREST}";
            }

            result += $") VALUES " +
                $"('{entry.AccountId}', '{entry.AccountType}', '{entry.Balance}', '{entry.Name}', '{entry.Image}'";

            if (entry.AccountType == AccountsTable.ACCOUNT_TYPE_INVESTMENTS)
            {
                result += $", '{entry.SourceAccountId}', '{entry.Duration}', '{entry.Interest}'";
            }

            result += ");";

            return result;
        }

        private string BuildEditCommand(AccountsTableEntry entry)
        {
            var result = $"UPDATE {AccountsTable.TABLE_NAME} " +
                    $"SET {AccountsTable.COLUMN_TYPE} = '{entry.AccountType}', " +
                    $"{AccountsTable.COLUMN_BALANCE} = '{entry.Balance}', " +
                    $"{AccountsTable.COLUMN_NAME} = '{entry.Name}', " +
                    $"{AccountsTable.COLUMN_IMAGE} = '{entry.Image}'";
                   

            if (entry.AccountType == AccountsTable.ACCOUNT_TYPE_INVESTMENTS)
            {
                result += $", {AccountsTable.COLUMN_SOURCE_ACCOUNT_ID} = '{entry.SourceAccountId}', " +
                    $"{AccountsTable.COLUMN_DURATION} = '{entry.Duration}', " +
                    $"{AccountsTable.COLUMN_INTEREST} = '{entry.Interest}'";
            }

            result += $"WHERE {AccountsTable.COLUMN_ID} = '{entry.AccountId}';";

            return result;
        }

    }
}

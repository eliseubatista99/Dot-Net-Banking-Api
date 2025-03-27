using BankingAppDataTier.Contracts.Configs;
using BankingAppDataTier.Contracts.Constants;
using BankingAppDataTier.Contracts.Constants.Database;
using BankingAppDataTier.Contracts.Database;
using BankingAppDataTier.Contracts.Providers;
using ElideusDotNetFramework.Core;
using ElideusDotNetFramework.PostgreSql;

namespace BankingAppDataTier.Providers
{
    public class DatabaseAccountsProvider : NpgsqlDatabaseProvider<AccountsTableEntry>, IDatabaseAccountsProvider
    {
        public DatabaseAccountsProvider(IApplicationContext applicationContext): base (applicationContext)
        {
            var configuration = applicationContext.GetDependency<IConfiguration>()!;
            connectionString = configuration.GetSection(DatabaseConfigs.DatabaseSection).GetValue<string>(DatabaseConfigs.DatabaseConnection)!;
        }

        public override bool CreateTableIfNotExists()
        {
            var command = $"CREATE TABLE IF NOT EXISTS {AccountsTable.TABLE_NAME}" +
                        $"(" +
                        $"{AccountsTable.COLUMN_ID} VARCHAR(64) NOT NULL," +
                        $"{AccountsTable.COLUMN_OWNER_CLIENT_ID} VARCHAR(64) NOT NULL," +
                        $"{AccountsTable.COLUMN_TYPE} CHAR(2) NOT NULL," +
                        $"{AccountsTable.COLUMN_BALANCE} DECIMAL(20,2) NOT NULL," +
                        $"{AccountsTable.COLUMN_NAME} VARCHAR(64) NOT NULL," +
                        $"{AccountsTable.COLUMN_IMAGE} VARCHAR," +
                        $"{AccountsTable.COLUMN_SOURCE_ACCOUNT_ID} VARCHAR(64)," +
                        $"{AccountsTable.COLUMN_DURATION} INTEGER," +
                        $"{AccountsTable.COLUMN_INTEREST} DECIMAL(5,2)," +
                        $"PRIMARY KEY ({AccountsTable.COLUMN_ID} )," +
                        $"FOREIGN KEY ({AccountsTable.COLUMN_OWNER_CLIENT_ID}) REFERENCES {ClientsTable.TABLE_NAME}({ClientsTable.COLUMN_ID})" +
                        $")";

            return ExecuteWrite(connectionString, command);
        }

        public override List<AccountsTableEntry> GetAll()
        {
            var command = $"SELECT * FROM {AccountsTable.TABLE_NAME}";

            return ExecuteReadMultiple(connectionString, command);
        }

        public override AccountsTableEntry? GetById(string id)
        {
            var command = $"SELECT * FROM {AccountsTable.TABLE_NAME} WHERE {AccountsTable.COLUMN_ID} = '{id}'";

            return ExecuteRead(connectionString, command);
        }

        public override bool Add(AccountsTableEntry entry)
        {
            var command = this.BuildAddCommand(entry);

            return ExecuteWrite(connectionString, command);
        }

        public override bool Edit(AccountsTableEntry entry)
        {
            var command = this.BuildEditCommand(entry);

            return ExecuteWrite(connectionString, command);
        }

        public override bool Delete(string id)
        {
            var command = $"DELETE FROM {AccountsTable.TABLE_NAME} WHERE {AccountsTable.COLUMN_ID} = '{id}'";

            return ExecuteWrite(connectionString, command);
        }

        public override bool DeleteAll()
        {
            var command = $"DELETE FROM {AccountsTable.TABLE_NAME} WHERE 1=1";

            return ExecuteWrite(connectionString, command);
        }

        public List<AccountsTableEntry> GetAccountsOfClient(string clientId)
        {
            var command = $"SELECT * FROM {AccountsTable.TABLE_NAME} WHERE {AccountsTable.COLUMN_OWNER_CLIENT_ID} = '{clientId}'";

            return ExecuteReadMultiple(connectionString, command);
        }


        private string BuildAddCommand(AccountsTableEntry entry)
        {
            var result = $"INSERT INTO {AccountsTable.TABLE_NAME} " +
                $"({AccountsTable.COLUMN_ID}, {AccountsTable.COLUMN_OWNER_CLIENT_ID}, {AccountsTable.COLUMN_TYPE}, {AccountsTable.COLUMN_BALANCE}, {AccountsTable.COLUMN_NAME}, " +
                $"{AccountsTable.COLUMN_IMAGE}";

            if (entry.AccountType == BankingAppDataTierConstants.ACCOUNT_TYPE_INVESTMENTS)
            {
                result += $", {AccountsTable.COLUMN_SOURCE_ACCOUNT_ID}, {AccountsTable.COLUMN_DURATION}, {AccountsTable.COLUMN_INTEREST}";
            }

            result += $") VALUES " +
                $"('{entry.AccountId}', '{entry.OwnerCliendId}','{entry.AccountType}', '{entry.Balance}', '{entry.Name}', '{entry.Image}'";

            if (entry.AccountType == BankingAppDataTierConstants.ACCOUNT_TYPE_INVESTMENTS)
            {
                result += $", '{entry.SourceAccountId}', '{entry.Duration}', '{entry.Interest}'";
            }

            result += ");";

            return result;
        }

        private string BuildEditCommand(AccountsTableEntry entry)
        {
            var result = $"UPDATE {AccountsTable.TABLE_NAME} " +
                    $"SET {AccountsTable.COLUMN_OWNER_CLIENT_ID} = '{entry.OwnerCliendId}', " +
                    $"{AccountsTable.COLUMN_TYPE} = '{entry.AccountType}', " +
                    $"{AccountsTable.COLUMN_BALANCE} = '{entry.Balance}', " +
                    $"{AccountsTable.COLUMN_NAME} = '{entry.Name}', " +
                    $"{AccountsTable.COLUMN_IMAGE} = '{entry.Image}'";
                   

            if (entry.AccountType == BankingAppDataTierConstants.ACCOUNT_TYPE_INVESTMENTS)
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

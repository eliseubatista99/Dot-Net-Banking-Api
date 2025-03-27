using BankingAppDataTier.Contracts.Configs;
using BankingAppDataTier.Contracts.Constants.Database;
using BankingAppDataTier.Contracts.Database;
using BankingAppDataTier.Contracts.Providers;
using ElideusDotNetFramework.Core;
using ElideusDotNetFramework.PostgreSql;

namespace BankingAppDataTier.Providers
{
    public class DatabaseTransactionsProvider : NpgsqlDatabaseProvider<TransactionTableEntry>, IDatabaseTransactionsProvider
    {
        public DatabaseTransactionsProvider(IApplicationContext applicationContext) : base(applicationContext)
        {
            var configuration = applicationContext.GetDependency<IConfiguration>()!;
            connectionString = configuration.GetSection(DatabaseConfigs.DatabaseSection).GetValue<string>(DatabaseConfigs.DatabaseConnection)!;
        }

        public override bool CreateTableIfNotExists()
        {
            var command = $"CREATE TABLE IF NOT EXISTS {TransactionsTable.TABLE_NAME}" +
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

            return ExecuteWrite(connectionString, command);
        }

        public override List<TransactionTableEntry> GetAll()
        {
            var command = $"SELECT * FROM {TransactionsTable.TABLE_NAME}";

            return ExecuteReadMultiple(connectionString, command);
        }

        public override TransactionTableEntry? GetById(string id)
        {
            var command = $"SELECT * FROM {TransactionsTable.TABLE_NAME} WHERE {TransactionsTable.COLUMN_ID} = '{id}'";

            return ExecuteRead(connectionString, command);
        }

        public override bool Add(TransactionTableEntry entry)
        {
            var command = this.BuildAddCommand(entry);

            return ExecuteWrite(connectionString, command);
        }

        public override bool Edit(TransactionTableEntry entry)
        {
            var command = this.BuildEditCommand(entry);

            return ExecuteWrite(connectionString, command);
        }

        public override bool Delete(string id)
        {
            var command = $"DELETE FROM {TransactionsTable.TABLE_NAME} WHERE {TransactionsTable.COLUMN_ID} = '{id}'";

            return ExecuteWrite(connectionString, command);
        }

        public override bool DeleteAll()
        {
            var command = $"DELETE FROM {TransactionsTable.TABLE_NAME} WHERE 1=1";

            return ExecuteWrite(connectionString, command);
        }

        public List<TransactionTableEntry> GetByDestinationAccount(string account)
        {
            var command = $"SELECT * FROM {TransactionsTable.TABLE_NAME} WHERE {TransactionsTable.COLUMN_DESTINATION_ACCOUNT} = '{account}'";

            return ExecuteReadMultiple(connectionString, command);
        }

        public List<TransactionTableEntry> GetBySourceAccount(string account)
        {
            var command = $"SELECT * FROM {TransactionsTable.TABLE_NAME} WHERE {TransactionsTable.COLUMN_SOURCE_ACCOUNT} = '{account}'";

            return ExecuteReadMultiple(connectionString, command);
        }

        public List<TransactionTableEntry> GetBySourceCard(string card)
        {
            var command = $"SELECT * FROM {TransactionsTable.TABLE_NAME} WHERE {TransactionsTable.COLUMN_SOURCE_CARD} = '{card}'";

            return ExecuteReadMultiple(connectionString, command);
        }

        private string BuildAddCommand(TransactionTableEntry entry)
        {
            var result = $"INSERT INTO {TransactionsTable.TABLE_NAME} " +
                $"({TransactionsTable.COLUMN_ID}, {TransactionsTable.COLUMN_TRANSACTION_DATE}, " +
                $"{TransactionsTable.COLUMN_DESCRIPTION}, {TransactionsTable.COLUMN_SOURCE_ACCOUNT}," +
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
using BankingAppDataTier.Contracts.Configs;
using BankingAppDataTier.Contracts.Constants.Database;
using BankingAppDataTier.Contracts.Database;
using BankingAppDataTier.Contracts.Providers;
using ElideusDotNetFramework.Core;
using ElideusDotNetFramework.PostgreSql;

namespace BankingAppDataTier.Providers
{
    public class DatabaseCardsProvider : NpgsqlDatabaseProvider<CardsTableEntry>, IDatabaseCardsProvider
    {
        public DatabaseCardsProvider(IApplicationContext applicationContext) : base(applicationContext)
        {
            var configuration = applicationContext.GetDependency<IConfiguration>()!;
            connectionString = configuration.GetSection(DatabaseConfigs.DatabaseSection).GetValue<string>(DatabaseConfigs.DatabaseConnection)!;
        }

        public override bool CreateTableIfNotExists()
        {
            var command = $"CREATE TABLE IF NOT EXISTS {CardsTable.TABLE_NAME}" +
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

            return ExecuteWrite(connectionString, command);
        }

        public override List<CardsTableEntry> GetAll()
        {
            var command = $"SELECT * FROM {CardsTable.TABLE_NAME}";

            return ExecuteReadMultiple(connectionString, command);
        }

        public override CardsTableEntry? GetById(string id)
        {
            var command = $"SELECT * FROM {CardsTable.TABLE_NAME} WHERE {CardsTable.COLUMN_ID} = '{id}'";

            return ExecuteRead(connectionString, command);
        }

        public override bool Add(CardsTableEntry entry)
        {
            var command = this.BuildAddCommand(entry);

            return ExecuteWrite(connectionString, command);
        }

        public override bool Edit(CardsTableEntry entry)
        {
            var command = this.BuildEditCommand(entry);

            return ExecuteWrite(connectionString, command);
        }

        public override bool Delete(string id)
        {
            var command = $"DELETE FROM {CardsTable.TABLE_NAME} WHERE {CardsTable.COLUMN_ID} = '{id}'";

            return ExecuteWrite(connectionString, command);
        }

        public override bool DeleteAll()
        {
            var command = $"DELETE FROM {CardsTable.TABLE_NAME} WHERE 1=1";

            return ExecuteWrite(connectionString, command);
        }

        public List<CardsTableEntry> GetCardsOfAccount(string accountId)
        {
            var command = $"SELECT * FROM {CardsTable.TABLE_NAME} WHERE {CardsTable.COLUMN_RELATED_ACCOUNT_ID} = '{accountId}'";

            return ExecuteReadMultiple(connectionString, command);
        }

        public List<CardsTableEntry> GetCardsWithPlastic(string plasticId)
        {
            var command = $"SELECT * FROM {CardsTable.TABLE_NAME} WHERE {CardsTable.COLUMN_PLASTIC_ID} = '{plasticId}'";

            return ExecuteReadMultiple(connectionString, command);
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

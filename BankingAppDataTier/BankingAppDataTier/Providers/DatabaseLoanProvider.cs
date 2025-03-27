using BankingAppDataTier.Contracts.Configs;
using BankingAppDataTier.Contracts.Constants.Database;
using BankingAppDataTier.Contracts.Database;
using BankingAppDataTier.Contracts.Providers;
using ElideusDotNetFramework.Application;
using ElideusDotNetFramework.Database;

namespace BankingAppDataTier.Providers
{
    public class DatabaseLoanProvider : NpgsqlDatabaseProvider<LoanTableEntry>, IDatabaseLoansProvider
    {
        public DatabaseLoanProvider(IApplicationContext applicationContext) : base(applicationContext)
        {
            var configuration = applicationContext.GetDependency<IConfiguration>()!;
            connectionString = configuration.GetSection(DatabaseConfigs.DatabaseSection).GetValue<string>(DatabaseConfigs.DatabaseConnection)!;
        }

        public override bool CreateTableIfNotExists()
        {
            var command = $"CREATE TABLE IF NOT EXISTS {LoansTable.TABLE_NAME}" +
                        $"(" +
                        $"{LoansTable.COLUMN_ID} VARCHAR(64) NOT NULL," +
                        $"{LoansTable.COLUMN_NAME} VARCHAR(64) NOT NULL," +
                        $"{LoansTable.COLUMN_RELATED_ACCOUNT} VARCHAR(64) NOT NULL," +
                        $"{LoansTable.COLUMN_START_DATE} DATE NOT NULL," +
                        $"{LoansTable.COLUMN_RELATED_OFFER} VARCHAR(64) NOT NULL," +
                        $"{LoansTable.COLUMN_DURATION} INTEGER NOT NULL," +
                        $"{LoansTable.COLUMN_CONTRACTED_AMOUNT} DECIMAL(20,2) NOT NULL," +
                        $"{LoansTable.COLUMN_PAID_AMOUNT} DECIMAL(20,2) NOT NULL," +
                        $"PRIMARY KEY ({LoansTable.COLUMN_ID} )," +
                        $"FOREIGN KEY ({LoansTable.COLUMN_RELATED_ACCOUNT}) REFERENCES {AccountsTable.TABLE_NAME}({AccountsTable.COLUMN_ID}), " +
                        $"FOREIGN KEY ({LoansTable.COLUMN_RELATED_OFFER}) REFERENCES {LoansTable.TABLE_NAME}({LoanOffersTable.COLUMN_ID})" +
                        $")";

            return ExecuteWrite(connectionString, command);
        }

        public override List<LoanTableEntry> GetAll()
        {
            var command = $"SELECT * FROM {LoansTable.TABLE_NAME}";

            return ExecuteReadMultiple(connectionString, command);
        }

        public override LoanTableEntry? GetById(string id)
        {
            var command = $"SELECT * FROM {LoansTable.TABLE_NAME} WHERE {LoansTable.COLUMN_ID} = '{id}'";

            return ExecuteRead(connectionString, command);
        }

        public override bool Add(LoanTableEntry entry)
        {
            var command = $"INSERT INTO {LoansTable.TABLE_NAME} " +
                        $"({LoansTable.COLUMN_ID}, {LoansTable.COLUMN_NAME}, {LoansTable.COLUMN_RELATED_ACCOUNT}, {LoansTable.COLUMN_START_DATE}," +
                        $" {LoansTable.COLUMN_RELATED_OFFER}, {LoansTable.COLUMN_DURATION}, {LoansTable.COLUMN_CONTRACTED_AMOUNT}, {LoansTable.COLUMN_PAID_AMOUNT}) " +
                        $"VALUES " +
                        $"('{entry.Id}', '{entry.Name}', '{entry.RelatedAccount}', '{entry.StartDate.ToString("yyyy-MM-dd")}', '{entry.RelatedOffer}', " +
                        $"'{entry.Duration}', '{entry.ContractedAmount}', '{entry.PaidAmount}');";

            return ExecuteWrite(connectionString, command);
        }

        public override bool Edit(LoanTableEntry entry)
        {
            var command = $"UPDATE {LoansTable.TABLE_NAME} " +
                    $"SET {LoansTable.COLUMN_START_DATE} = '{entry.StartDate}', " +
                    $"{LoansTable.COLUMN_NAME} = '{entry.Name}', " +
                    $"{LoansTable.COLUMN_RELATED_ACCOUNT} = '{entry.RelatedAccount}', " +
                    $"{LoansTable.COLUMN_RELATED_OFFER} = '{entry.RelatedOffer}', " +
                    $"{LoansTable.COLUMN_DURATION} = '{entry.Duration}', " +
                    $"{LoansTable.COLUMN_CONTRACTED_AMOUNT} = '{entry.ContractedAmount}', " +
                    $"{LoansTable.COLUMN_PAID_AMOUNT} = '{entry.PaidAmount}' " +
                    $"WHERE {LoansTable.COLUMN_ID} = '{entry.Id}';";

            return ExecuteWrite(connectionString, command);
        }

        public override bool Delete(string id)
        {
            var command = $"DELETE FROM {LoansTable.TABLE_NAME} WHERE {LoansTable.COLUMN_ID} = '{id}'";

            return ExecuteWrite(connectionString, command);
        }

        public override bool DeleteAll()
        {
            var command = $"DELETE FROM {LoansTable.TABLE_NAME} WHERE 1=1";

            return ExecuteWrite(connectionString, command);
        }

        public List<LoanTableEntry> GetByAccount(string relatedAccount)
        {
            var command = $"SELECT * FROM {LoansTable.TABLE_NAME} WHERE {LoansTable.COLUMN_RELATED_ACCOUNT} = '{relatedAccount}'";

            return ExecuteReadMultiple(connectionString, command);
        }

        public List<LoanTableEntry> GetByOffer(string loanOffer)
        {
            var command = $"SELECT * FROM {LoansTable.TABLE_NAME} WHERE {LoansTable.COLUMN_RELATED_OFFER} = '{loanOffer}'";

            return ExecuteReadMultiple(connectionString, command);
        }
    }
}
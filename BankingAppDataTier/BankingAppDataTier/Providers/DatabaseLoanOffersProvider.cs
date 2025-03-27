using BankingAppDataTier.Contracts.Configs;
using BankingAppDataTier.Contracts.Constants.Database;
using BankingAppDataTier.Contracts.Database;
using BankingAppDataTier.Contracts.Providers;
using ElideusDotNetFramework.Application;
using ElideusDotNetFramework.Database;

namespace BankingAppDataTier.Providers
{
    public class DatabaseLoanOffersProvider : NpgsqlDatabaseProvider<LoanOfferTableEntry>, IDatabaseLoanOfferProvider
    {
        public DatabaseLoanOffersProvider(IApplicationContext applicationContext) : base(applicationContext)
        {
            var configuration = applicationContext.GetDependency<IConfiguration>()!;
            connectionString = configuration.GetSection(DatabaseConfigs.DatabaseSection).GetValue<string>(DatabaseConfigs.DatabaseConnection)!;
        }

        public override bool CreateTableIfNotExists()
        {
            var command = $"CREATE TABLE IF NOT EXISTS {LoanOffersTable.TABLE_NAME}" +
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

            return ExecuteWrite(connectionString, command);
        }

        public override List<LoanOfferTableEntry> GetAll()
        {
            var command = $"SELECT * FROM {LoanOffersTable.TABLE_NAME}";

            return ExecuteReadMultiple(connectionString, command);
        }

        public override LoanOfferTableEntry? GetById(string id)
        {
            var command = $"SELECT * FROM {LoanOffersTable.TABLE_NAME} WHERE {LoanOffersTable.COLUMN_ID} = '{id}'";

            return ExecuteRead(connectionString, command);
        }

        public override bool Add(LoanOfferTableEntry entry)
        {
            var command = $"INSERT INTO {LoanOffersTable.TABLE_NAME} " +
                        $"({LoanOffersTable.COLUMN_ID}, {LoanOffersTable.COLUMN_NAME}, {LoanOffersTable.COLUMN_DESCRIPTION}, {LoanOffersTable.COLUMN_TYPE}," +
                        $" {LoanOffersTable.COLUMN_MAX_EFFORT}, {LoanOffersTable.COLUMN_INTEREST}, {LoanOffersTable.COLUMN_IS_ACTIVE}) " +
                        $"VALUES " +
                        $"('{entry.Id}', '{entry.Name}', '{entry.Description}','{entry.LoanType}', '{entry.MaxEffort}', '{entry.Interest}', '{entry.IsActive}');";

            return ExecuteWrite(connectionString, command);
        }

        public override bool Edit(LoanOfferTableEntry entry)
        {
            var command = $"UPDATE {LoanOffersTable.TABLE_NAME} " +
                    $"SET {LoanOffersTable.COLUMN_TYPE} = '{entry.LoanType}', " +
                    $"{LoanOffersTable.COLUMN_NAME} = '{entry.Name}', " +
                    $"{LoanOffersTable.COLUMN_DESCRIPTION} = '{entry.Description}', " +
                    $"{LoanOffersTable.COLUMN_MAX_EFFORT} = '{entry.MaxEffort}', " +
                    $"{LoanOffersTable.COLUMN_INTEREST} = '{entry.Interest}', " +
                    $"{LoanOffersTable.COLUMN_IS_ACTIVE} = '{entry.IsActive}' " +
                    $"WHERE {LoanOffersTable.COLUMN_ID} = '{entry.Id}';";

            return ExecuteWrite(connectionString, command);
        }

        public override bool Delete(string id)
        {
            var command = $"DELETE FROM {LoanOffersTable.TABLE_NAME} WHERE {LoanOffersTable.COLUMN_ID} = '{id}'";

            return ExecuteWrite(connectionString, command);
        }

        public override bool DeleteAll()
        {
            var command = $"DELETE FROM {LoanOffersTable.TABLE_NAME} WHERE 1=1";

            return ExecuteWrite(connectionString, command);
        }


        public List<LoanOfferTableEntry> GetByType(string loanType, bool onlyActive = false)
        {
            var command = $"SELECT * FROM {LoanOffersTable.TABLE_NAME} WHERE {LoanOffersTable.COLUMN_TYPE} = '{loanType}'";

            if (onlyActive == true)
            {
                command += $"AND {LoanOffersTable.COLUMN_IS_ACTIVE} = 'TRUE'";
            }

            return ExecuteReadMultiple(connectionString, command);
        }
    }
}

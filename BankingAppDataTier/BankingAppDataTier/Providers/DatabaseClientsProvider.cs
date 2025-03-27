using BankingAppDataTier.Contracts.Configs;
using BankingAppDataTier.Contracts.Constants.Database;
using BankingAppDataTier.Contracts.Database;
using BankingAppDataTier.Contracts.Providers;
using ElideusDotNetFramework.Application;
using ElideusDotNetFramework.Database;

namespace BankingAppDataTier.Providers
{
    public class DatabaseClientsProvider : NpgsqlDatabaseProvider<ClientsTableEntry>, IDatabaseClientsProvider
    {
        public DatabaseClientsProvider(IApplicationContext applicationContext) : base(applicationContext)
        {
            var configuration = applicationContext.GetDependency<IConfiguration>()!;
            connectionString = configuration.GetSection(DatabaseConfigs.DatabaseSection).GetValue<string>(DatabaseConfigs.DatabaseConnection)!;
        }

        public override bool CreateTableIfNotExists()
        {
            var command = $"CREATE TABLE IF NOT EXISTS {ClientsTable.TABLE_NAME}" +
                        $"(" +
                        $"{ClientsTable.COLUMN_ID} VARCHAR(64) NOT NULL," +
                        $"{ClientsTable.COLUMN_PASSWORD} VARCHAR(64) NOT NULL," +
                        $"{ClientsTable.COLUMN_NAME} VARCHAR(64) NOT NULL," +
                        $"{ClientsTable.COLUMN_SURNAME} VARCHAR(64) NOT NULL," +
                        $"{ClientsTable.COLUMN_BIRTH_DATE} DATE NOT NULL," +
                        $"{ClientsTable.COLUMN_VAT_NUMBER} VARCHAR(30) NOT NULL," +
                        $"{ClientsTable.COLUMN_PHONE_NUMBER} VARCHAR(20) NOT NULL," +
                        $"{ClientsTable.COLUMN_EMAIL} VARCHAR(60) NOT NULL," +
                        $"PRIMARY KEY ({ClientsTable.COLUMN_ID} )" +
                        $")";

            return ExecuteWrite(connectionString, command);
        }

        public override List<ClientsTableEntry> GetAll()
        {
            var command = $"SELECT * FROM {ClientsTable.TABLE_NAME}";

            return ExecuteReadMultiple(connectionString, command);
        }

        public override ClientsTableEntry? GetById(string id)
        {
            var command = $"SELECT * FROM {ClientsTable.TABLE_NAME} WHERE {ClientsTable.COLUMN_ID} = '{id}'";

            return ExecuteRead(connectionString, command);
        }

        public override bool Add(ClientsTableEntry entry)
        {
            var command = $"INSERT INTO {ClientsTable.TABLE_NAME} " +
                        $"({ClientsTable.COLUMN_ID}, {ClientsTable.COLUMN_PASSWORD}, {ClientsTable.COLUMN_NAME}, {ClientsTable.COLUMN_SURNAME}, {ClientsTable.COLUMN_BIRTH_DATE}, " +
                        $"{ClientsTable.COLUMN_VAT_NUMBER}, {ClientsTable.COLUMN_PHONE_NUMBER}, {ClientsTable.COLUMN_EMAIL}) " +
                        $"VALUES " +
                        $"('{entry.Id}', '{entry.Password}', '{entry.Name}', '{entry.Surname}', '{entry.BirthDate.ToString("yyyy-MM-dd")}', '{entry.VATNumber}', '{entry.PhoneNumber}', '{entry.Email}');";

            return ExecuteWrite(connectionString, command);
        }

        public override bool Edit(ClientsTableEntry entry)
        {
            var command = $"UPDATE {ClientsTable.TABLE_NAME} " +
                    $"SET {ClientsTable.COLUMN_ID} = '{entry.Id}', " +
                    $"{ClientsTable.COLUMN_NAME} = '{entry.Name}', " +
                    $"{ClientsTable.COLUMN_SURNAME} = '{entry.Surname}', " +
                    $"{ClientsTable.COLUMN_BIRTH_DATE} = '{entry.BirthDate.ToString("yyyy-MM-dd")}', " +
                    $"{ClientsTable.COLUMN_VAT_NUMBER} = '{entry.VATNumber}', " +
                    $"{ClientsTable.COLUMN_PHONE_NUMBER} = '{entry.PhoneNumber}', " +
                    $"{ClientsTable.COLUMN_EMAIL} = '{entry.Email}' " +
                    $"WHERE {ClientsTable.COLUMN_ID} = '{entry.Id}';";

            return ExecuteWrite(connectionString, command);
        }

        public override bool Delete(string id)
        {
            var command = $"DELETE FROM {ClientsTable.TABLE_NAME} WHERE {ClientsTable.COLUMN_ID} = '{id}'";

            return ExecuteWrite(connectionString, command);
        }

        public override bool DeleteAll()
        {
            var command = $"DELETE FROM {ClientsTable.TABLE_NAME} WHERE 1=1";

            return ExecuteWrite(connectionString, command);
        }

        public bool ChangePassword(string id, string password)
        {
            var command = $"UPDATE {ClientsTable.TABLE_NAME} " +
                    $"SET {ClientsTable.COLUMN_PASSWORD} = '{password}' " +
                    $"WHERE {ClientsTable.COLUMN_ID} = '{id}';";

            return ExecuteWrite(connectionString, command);
        }
    }
}

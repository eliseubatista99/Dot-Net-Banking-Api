using BankingAppDataTier.Library.Configs;
using BankingAppDataTier.Library.Constants.Database;
using BankingAppDataTier.Library.Database;
using BankingAppDataTier.Library.Providers;
using ElideusDotNetFramework.Core;
using ElideusDotNetFramework.PostgreSql;
using System.Diagnostics.CodeAnalysis;

namespace BankingAppDataTier.Providers
{
    [ExcludeFromCodeCoverage]
    public class DatabasePlasticsProvider : NpgsqlDatabaseProvider<PlasticTableEntry>, IDatabasePlasticsProvider
    {
        public DatabasePlasticsProvider(IApplicationContext applicationContext) : base(applicationContext)
        {
            var configuration = applicationContext.GetDependency<IConfiguration>()!;
            connectionString = configuration.GetSection(DatabaseConfigs.DatabaseSection).GetValue<string>(DatabaseConfigs.DatabaseConnection)!;
        }

        public override bool CreateTableIfNotExists()
        {
            var command = $"CREATE TABLE IF NOT EXISTS {PlasticsTable.TABLE_NAME}" +
                        $"(" +
                        $"{PlasticsTable.COLUMN_ID} VARCHAR(64) NOT NULL," +
                        $"{PlasticsTable.COLUMN_TYPE} VARCHAR(64) NOT NULL," +
                        $"{PlasticsTable.COLUMN_NAME} VARCHAR(64) NOT NULL," +
                        $"{PlasticsTable.COLUMN_CASHBACK} DECIMAL(5,2)," +
                        $"{PlasticsTable.COLUMN_COMISSION} DECIMAL(5,2)," +
                        $"{PlasticsTable.COLUMN_IMAGE} VARCHAR NOT NULL," +
                        $"{PlasticsTable.COLUMN_IS_ACTIVE} BOOL NOT NULL," +
                        $"PRIMARY KEY ({PlasticsTable.COLUMN_ID} )" +
                        $") ";

            return ExecuteWrite(connectionString, command);
        }

        public override List<PlasticTableEntry> GetAll()
        {
            var command = $"SELECT * FROM {PlasticsTable.TABLE_NAME}";

            return ExecuteReadMultiple(connectionString, command);
        }

        public override PlasticTableEntry? GetById(string id)
        {
            var command = $"SELECT * FROM {PlasticsTable.TABLE_NAME} WHERE {PlasticsTable.COLUMN_ID} = '{id}'";

            return ExecuteRead(connectionString, command);
        }

        public override bool Add(PlasticTableEntry entry)
        {
            var command = $"INSERT INTO {PlasticsTable.TABLE_NAME} " +
                        $"({PlasticsTable.COLUMN_ID}, {PlasticsTable.COLUMN_TYPE}, {PlasticsTable.COLUMN_NAME}, {PlasticsTable.COLUMN_CASHBACK}, " +
                        $"{PlasticsTable.COLUMN_COMISSION}, {PlasticsTable.COLUMN_IMAGE}, {PlasticsTable.COLUMN_IS_ACTIVE}) " +
                        $"VALUES " +
                        $"('{entry.Id}', '{entry.CardType}', '{entry.Name}', '{entry.Cashback.GetValueOrDefault()}', " +
                        $"'{entry.Commission.GetValueOrDefault()}', '{entry.Image}', '{entry.IsActive}');";

            return ExecuteWrite(connectionString, command);
        }

        public override bool Edit(PlasticTableEntry entry)
        {
            var command = $"UPDATE {PlasticsTable.TABLE_NAME} " +
                    $"SET {PlasticsTable.COLUMN_ID} = '{entry.Id}', " +
                    $"{PlasticsTable.COLUMN_TYPE} = '{entry.CardType}', " +
                    $"{PlasticsTable.COLUMN_NAME} = '{entry.Name}', " +
                    $"{PlasticsTable.COLUMN_CASHBACK} = '{entry.Cashback}', " +
                    $"{PlasticsTable.COLUMN_COMISSION} = '{entry.Commission}', " +
                    $"{PlasticsTable.COLUMN_IMAGE} = '{entry.Image}', " +
                    $"{PlasticsTable.COLUMN_IS_ACTIVE} = '{entry.IsActive}'" +
                    $"WHERE {PlasticsTable.COLUMN_ID} = '{entry.Id}';";

            return ExecuteWrite(connectionString, command);
        }

        public override bool Delete(string id)
        {
            var command = $"DELETE FROM {PlasticsTable.TABLE_NAME} WHERE {PlasticsTable.COLUMN_ID} = '{id}'";

            return ExecuteWrite(connectionString, command);
        }

        public override bool DeleteAll()
        {
            var command = $"DELETE FROM {PlasticsTable.TABLE_NAME} WHERE 1=1";

            return ExecuteWrite(connectionString, command);
        }


        public List<PlasticTableEntry> GetPlasticsOfCardType(string cardType, bool onlyActive = false)
        {
            var command = $"SELECT * FROM {PlasticsTable.TABLE_NAME} WHERE {PlasticsTable.COLUMN_TYPE} = '{cardType}'";

            if (onlyActive == true)
            {
                command += $"AND {PlasticsTable.COLUMN_IS_ACTIVE} = 'TRUE'";
            }

            return ExecuteReadMultiple(connectionString, command);
        }
    }
}

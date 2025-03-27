using BankingAppAuthenticationTier.Library.Configs;
using BankingAppAuthenticationTier.Library.Constants.Database;
using BankingAppAuthenticationTier.Library.Database;
using BankingAppAuthenticationTier.Library.Providers;
using ElideusDotNetFramework.Core;
using ElideusDotNetFramework.PostgreSql;
using System.Diagnostics.CodeAnalysis;

namespace BankingAppAuthenticationTier.Providers
{
    [ExcludeFromCodeCoverage]
    public class DatabaseTokenProvider : NpgsqlDatabaseProvider<TokenTableEntry>, IDatabaseTokenProvider
    {
        public DatabaseTokenProvider(IApplicationContext applicationContext) : base(applicationContext)
        {
            var configuration = applicationContext.GetDependency<IConfiguration>()!;
            connectionString = configuration.GetSection(DatabaseConfigs.DatabaseSection).GetValue<string>(DatabaseConfigs.DatabaseConnection)!;
        }

        public override bool CreateTableIfNotExists()
        {
            var command = $"CREATE TABLE IF NOT EXISTS {TokensTable.TABLE_NAME}" +
                        $"(" +
                        $"{TokensTable.COLUMN_TOKEN} VARCHAR NOT NULL," +
                        $"{TokensTable.COLUMN_CLIENT_ID} VARCHAR(64) NOT NULL," +
                        $"{TokensTable.COLUMN_EXPIRATION_DATE} VARCHAR(20) NOT NULL," +
                        $"PRIMARY KEY ({TokensTable.COLUMN_TOKEN} )" +
                        $")";

            return ExecuteWrite(connectionString, command);
        }

        public override List<TokenTableEntry> GetAll()
        {
            var command = $"SELECT * FROM {TokensTable.TABLE_NAME}";

            return ExecuteReadMultiple(connectionString, command);
        }

        public override TokenTableEntry? GetById(string id)
        {
            var command = $"SELECT * FROM {TokensTable.TABLE_NAME} WHERE {TokensTable.COLUMN_TOKEN} = '{id}'";

            return ExecuteRead(connectionString, command);
        }

        public override bool Add(TokenTableEntry entry)
        {
            var command = $"INSERT INTO {TokensTable.TABLE_NAME} " +
                                            $"({TokensTable.COLUMN_TOKEN}, {TokensTable.COLUMN_CLIENT_ID}, {TokensTable.COLUMN_EXPIRATION_DATE}) " +
                                            $"VALUES " +
                                            $"('{entry.Token}', '{entry.ClientId}', '{NpgsqlDatabaseHelper.FormatDateWithTime(entry.ExpirationDate)}');";

            return ExecuteWrite(connectionString, command);
        }

        public override bool Edit(TokenTableEntry entry)
        {
            var command = $"UPDATE {TokensTable.TABLE_NAME} " +
                $"SET {TokensTable.COLUMN_EXPIRATION_DATE} = '{NpgsqlDatabaseHelper.FormatDateWithTime(entry.ExpirationDate)}' " +
                $"WHERE {TokensTable.COLUMN_TOKEN} = '{entry.Token}';";

            return ExecuteWrite(connectionString, command);
        }

        public override bool Delete(string id)
        {
            var command = $"DELETE FROM {TokensTable.TABLE_NAME} WHERE {TokensTable.COLUMN_TOKEN} = '{id}'";

            return ExecuteWrite(connectionString, command);
        }

        public override bool DeleteAll()
        {
            var command = $"DELETE FROM {TokensTable.TABLE_NAME} WHERE 1=1";

            return ExecuteWrite(connectionString, command);
        }


        public bool DeleteAllExpired()
        {
            DateTime today = DateTime.Now;

            var command = $"DELETE FROM {TokensTable.TABLE_NAME} " +
                        $"WHERE {TokensTable.COLUMN_EXPIRATION_DATE}<'{today.ToString("yyyy-MM-dd")}'";

            return ExecuteWrite(connectionString, command);
        }

        public bool DeleteTokensOfClient(string clientId)
        {
            var command = $"DELETE FROM {TokensTable.TABLE_NAME} " +
                        $"WHERE {TokensTable.COLUMN_CLIENT_ID}='{clientId}'";

            return ExecuteWrite(connectionString, command);
        }
    }
}

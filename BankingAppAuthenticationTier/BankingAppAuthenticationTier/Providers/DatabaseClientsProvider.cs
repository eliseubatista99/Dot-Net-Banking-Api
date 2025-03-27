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
            return new List<ClientsTableEntry>();
        }

        public override ClientsTableEntry? GetById(string id)
        {
            var command = $"SELECT * FROM {ClientsTable.TABLE_NAME} WHERE {ClientsTable.COLUMN_ID} = '{id}'";

            return ExecuteRead(connectionString, command);
        }

        public override bool Add(ClientsTableEntry entry)
        {
            return true;
        }

        public override bool Edit(ClientsTableEntry entry)
        {
            return true;
        }

        public override bool Delete(string id)
        {
            return true;
        }

        public override bool DeleteAll()
        {
            return true;
        }
    }
}

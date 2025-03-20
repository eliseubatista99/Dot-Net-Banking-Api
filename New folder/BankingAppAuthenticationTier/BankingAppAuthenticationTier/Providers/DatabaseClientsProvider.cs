using BankingAppAuthenticationTier.Contracts.Configs;
using BankingAppAuthenticationTier.Contracts.Constants.Database;
using BankingAppAuthenticationTier.Contracts.Database;
using BankingAppAuthenticationTier.Contracts.Providers;
using BankingAppAuthenticationTier.Database;
using Npgsql;

namespace BankingAppAuthenticationTier.Providers
{
    public class DatabaseClientsProvider : IDatabaseClientsProvider
    {

        private IConfiguration Configuration;
        private IMapperProvider mapperProvider;
        private string connectionString;

        public DatabaseClientsProvider(IConfiguration configuration, IMapperProvider _mapperProvider)
        {
            this.Configuration = configuration;
            this.mapperProvider = _mapperProvider;

            connectionString = Configuration.GetSection(DatabaseConfigs.DatabaseSection).GetValue<string>(DatabaseConfigs.DatabaseConnection);
        }

        public ClientsTableEntry? GetById(string id)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                ClientsTableEntry? result = null;

                connection.Open();

                var (transaction, command) = SqlDatabaseHelper.InitialzieSqlTransaction(connection);

                try
                {
                    command.CommandText = $"SELECT * FROM {ClientsTable.TABLE_NAME} WHERE {ClientsTable.COLUMN_ID} = '{id}'";

                    var sqlReader = command.ExecuteReader();

                    if (sqlReader.HasRows)
                    {
                        sqlReader.Read();
                        result = mapperProvider.Map<NpgsqlDataReader, ClientsTableEntry>(sqlReader);

                    }

                    return result;
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }
    }
}

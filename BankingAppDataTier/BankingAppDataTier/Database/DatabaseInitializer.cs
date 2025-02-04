using BankingAppDataTier.Contracts.Configs;
using BankingAppDataTier.Contracts.Constants;
using BankingAppDataTier.Contracts.Database;

namespace BankingAppDataTier.Database
{
    public class DatabaseInitializer
    {
        private IBankingAppSqlProvider BankingAppSqlProvider;

        public DatabaseInitializer(IBankingAppSqlProvider sqlProvider) 
        {
            this.BankingAppSqlProvider = sqlProvider;

            InitializeClients();
        }

        private void InitializeClients()
        {
            this.BankingAppSqlProvider.ExecuteWriteQuery(ClientsTable.BuildCreateTableQuery());
        }
    }
}

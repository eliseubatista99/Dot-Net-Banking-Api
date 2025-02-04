using BankingAppDataTier.Contracts.Configs;
using BankingAppDataTier.Contracts.Constants;
using BankingAppDataTier.Contracts.Database;
using BankingAppDataTier.Contracts.Dtos;
using Microsoft.Data.SqlClient;

namespace BankingAppDataTier.Database
{
    public class BankingAppSqlProvider : IBankingAppSqlProvider
    {

        private IConfiguration Configuration;

        private SqlConnection SqlConnection;
        private SqlCommand SqlCommnand;

        public BankingAppSqlProvider(IConfiguration configuration)
        {
            this.Configuration = configuration;

            var connectionString = Configuration.GetSection(DatabaseConfigs.DatabaseSection).GetValue<string>(DatabaseConfigs.DatabaseConnection);
            this.SqlConnection = new SqlConnection(connectionString);

            this.SqlCommnand = new SqlCommand();

            this.SqlCommnand.Connection = SqlConnection;
            this.SqlCommnand.CommandType = System.Data.CommandType.Text;
            this.SqlCommnand.Parameters.Clear();

            Console.WriteLine($"[ZAU]: {connectionString}");
        }

        public SqlDataReader ExecuteReadQuery(string query)
        {
            this.SqlCommnand.Parameters.Clear();
            this.SqlCommnand.CommandText = query;

            SqlConnection.Open();

            var sqlReader = this.SqlCommnand.ExecuteReader();

            SqlConnection.Close();

            return sqlReader;
        }

        public bool ExecuteWriteQuery(string query)
        {
            this.SqlCommnand.Parameters.Clear();
            this.SqlCommnand.CommandText = query;

            SqlConnection.Open();

            var affectedRows = this.SqlCommnand.ExecuteNonQuery();
            
            SqlConnection.Close();

            return affectedRows != -1;
        }
    }
}

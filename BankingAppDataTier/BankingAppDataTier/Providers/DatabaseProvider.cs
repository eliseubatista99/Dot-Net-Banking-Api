using BankingAppDataTier.Contracts.Configs;
using BankingAppDataTier.Contracts.Providers;
using Microsoft.Data.SqlClient;

namespace BankingAppDataTier.Providers
{
    public class DatabaseProvider : IDatabaseProvider
    {

        private IConfiguration Configuration;

        private string connectionString;

        public DatabaseProvider(IConfiguration configuration)
        {
            this.Configuration = configuration;

            connectionString = Configuration.GetSection(DatabaseConfigs.DatabaseSection).GetValue<string>(DatabaseConfigs.DatabaseConnection);
        }
        public SqlDataReader? ExecuteQuery(string query)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                SqlTransaction transaction;

                // Start a local transaction.
                transaction = connection.BeginTransaction();

                // Must assign both transaction object and connection
                // to Command object for a pending local transaction
                command.Connection = connection;
                command.Transaction = transaction;

                try
                {
                    command.CommandText = query;

                    return command.ExecuteReader();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Read Exception Type: {0}\n: Message: {1}", ex.GetType(), ex.Message);

                    return null;
                }
            }
        }



        public bool ExecuteNonQuery(string query)
        {
            return this.ExecuteNonQueries(new List<string> { query });
        }

        public bool ExecuteNonQueries(List<string> queries)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                SqlTransaction transaction;

                // Start a local transaction.
                transaction = connection.BeginTransaction();

                // Must assign both transaction object and connection
                // to Command object for a pending local transaction
                command.Connection = connection;
                command.Transaction = transaction;

                try
                {

                    foreach (var query in queries)
                    {
                        command.CommandText = query;
                        command.ExecuteNonQuery();
                    }

                    // Attempt to commit the transaction.
                    transaction.Commit();

                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Commit Exception Type: {0}\n: Message: {1}", ex.GetType(), ex.Message);

                    transaction.Rollback();

                    // Attempt to roll back the transaction.
                    try
                    {
                        transaction.Rollback();
                    }
                    catch (Exception ex2)
                    {
                        // This catch block will handle any errors that may have occurred
                        // on the server that would cause the rollback to fail, such as
                        // a closed connection.
                        Console.WriteLine("Rollback Exception Type: {0}\n: Message: {1}", ex2.GetType(), ex2.Message);
                    }

                    return false;
                }
            }
        }


    }
}

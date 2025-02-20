using Microsoft.Data.SqlClient;

namespace BankingAppDataTier.Database
{
    public static class SqlDatabaseHelper
    {
        public static (SqlTransaction transaction, SqlCommand command) InitialzieSqlTransaction(SqlConnection connection)
        {
            SqlCommand command = connection.CreateCommand();
            SqlTransaction transaction;

            // Start a local transaction.
            transaction = connection.BeginTransaction();

            // Must assign both transaction object and connection
            // to Command object for a pending local transaction
            command.Parameters.Clear();
            command.Connection = connection;
            command.Transaction = transaction;

            return (transaction, command);
        }
    }
}

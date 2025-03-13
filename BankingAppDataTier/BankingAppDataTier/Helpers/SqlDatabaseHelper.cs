﻿using Npgsql;

namespace BankingAppDataTier.Database
{
    public static class SqlDatabaseHelper
    {
        public static (NpgsqlTransaction transaction, NpgsqlCommand command) InitialzieSqlTransaction(NpgsqlConnection connection)
        {
            NpgsqlCommand command = connection.CreateCommand();
            NpgsqlTransaction transaction;

            // Start a local transaction.
            transaction = connection.BeginTransaction();

            // Must assign both transaction object and connection
            // to Command object for a pending local transaction
            command.Parameters.Clear();
            command.Connection = connection;
            command.Transaction = transaction;

            return (transaction, command);
        }

        public static string? ReadColumnValue(NpgsqlDataReader sqlReader, string columnName)
        {
            var value = (sqlReader[columnName]);

            if(value is System.DBNull)
            {
                return null;
            }

            return value.ToString();
        }

        public static Nullable<T> ReadColumnValue<T>(NpgsqlDataReader sqlReader, string columnName) where T : struct
        {
            var value = (sqlReader[columnName]);

            if (value is System.DBNull)
            {
                return null;
            }

            return (T)value;
        }
    }
}

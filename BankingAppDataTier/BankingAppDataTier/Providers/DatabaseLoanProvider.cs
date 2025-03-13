﻿using BankingAppDataTier.Contracts.Configs;
using BankingAppDataTier.Contracts.Constants.Database;
using BankingAppDataTier.Contracts.Database;
using BankingAppDataTier.Contracts.Providers;
using BankingAppDataTier.Database;
using Npgsql;

namespace BankingAppDataTier.Providers
{
    public class DatabaseLoanProvider : IDatabaseLoansProvider
    {

        private IConfiguration Configuration;
        private IMapperProvider mapperProvider;

        private string connectionString;

        public DatabaseLoanProvider(IConfiguration configuration, IMapperProvider _mapperProvider)
        {
            this.Configuration = configuration;
            this.mapperProvider = _mapperProvider;

            connectionString = Configuration.GetSection(DatabaseConfigs.DatabaseSection).GetValue<string>(DatabaseConfigs.DatabaseConnection);
        }

        public bool CreateTableIfNotExists()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                var (transaction, command) = SqlDatabaseHelper.InitialzieSqlTransaction(connection);

                try
                {
                    command.CommandText = $"CREATE TABLE IF NOT EXISTS {LoansTable.TABLE_NAME}" +
                        $"(" +
                        $"{LoansTable.COLUMN_ID} VARCHAR(64) NOT NULL," +
                        $"{LoansTable.COLUMN_NAME} VARCHAR(64) NOT NULL," +
                        $"{LoansTable.COLUMN_RELATED_ACCOUNT} VARCHAR(64) NOT NULL," +
                        $"{LoansTable.COLUMN_START_DATE} DATE NOT NULL," +
                        $"{LoansTable.COLUMN_RELATED_OFFER} VARCHAR(64) NOT NULL," +
                        $"{LoansTable.COLUMN_DURATION} INTEGER NOT NULL," +
                        $"{LoansTable.COLUMN_CONTRACTED_AMOUNT} DECIMAL(20,2) NOT NULL," +
                        $"{LoansTable.COLUMN_PAID_AMOUNT} DECIMAL(20,2) NOT NULL," +
                        $"PRIMARY KEY ({LoansTable.COLUMN_ID} )," +
                        $"FOREIGN KEY ({LoansTable.COLUMN_RELATED_ACCOUNT}) REFERENCES {AccountsTable.TABLE_NAME}({AccountsTable.COLUMN_ID}), " +
                        $"FOREIGN KEY ({LoansTable.COLUMN_RELATED_OFFER}) REFERENCES {LoanOffersTable.TABLE_NAME}({LoanOffersTable.COLUMN_ID})" +
                        $")";

                    command.ExecuteNonQuery();

                    // Attempt to commit the transaction.
                    transaction.Commit();

                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public bool Add(LoanTableEntry entry)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                var (transaction, command) = SqlDatabaseHelper.InitialzieSqlTransaction(connection);

                try
                {
                    command.CommandText = $"INSERT INTO {LoansTable.TABLE_NAME} " +
                        $"({LoansTable.COLUMN_ID}, {LoansTable.COLUMN_NAME}, {LoansTable.COLUMN_RELATED_ACCOUNT}, {LoansTable.COLUMN_START_DATE}," +
                        $" {LoansTable.COLUMN_RELATED_OFFER}, {LoansTable.COLUMN_DURATION}, {LoansTable.COLUMN_CONTRACTED_AMOUNT}, {LoansTable.COLUMN_PAID_AMOUNT}) " +
                        $"VALUES " +
                        $"('{entry.Id}', '{entry.Name}', '{entry.RelatedAccount}', '{entry.StartDate.ToString("yyyy-MM-dd")}', '{entry.RelatedOffer}', " +
                        $"'{entry.Duration}', '{entry.ContractedAmount}', '{entry.PaidAmount}');";

                    command.ExecuteNonQuery();

                    // Attempt to commit the transaction.
                    transaction.Commit();

                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public bool Delete(string id)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                var (transaction, command) = SqlDatabaseHelper.InitialzieSqlTransaction(connection);

                try
                {
                    command.CommandText = $"DELETE FROM {LoansTable.TABLE_NAME} WHERE {LoansTable.COLUMN_ID} = '{id}'";

                    command.ExecuteNonQuery();

                    // Attempt to commit the transaction.
                    transaction.Commit();

                    return true;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public bool Edit(LoanTableEntry entry)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                var (transaction, command) = SqlDatabaseHelper.InitialzieSqlTransaction(connection);

                try
                {
                    command.CommandText = $"UPDATE {LoansTable.TABLE_NAME} " +
                    $"SET {LoansTable.COLUMN_START_DATE} = '{entry.StartDate}', " +
                    $"{LoansTable.COLUMN_NAME} = '{entry.Name}', " +
                    $"{LoansTable.COLUMN_RELATED_ACCOUNT} = '{entry.RelatedAccount}', " +
                    $"{LoansTable.COLUMN_RELATED_OFFER} = '{entry.RelatedOffer}', " +
                    $"{LoansTable.COLUMN_DURATION} = '{entry.Duration}', " +
                    $"{LoansTable.COLUMN_CONTRACTED_AMOUNT} = '{entry.ContractedAmount}', " +
                    $"{LoansTable.COLUMN_PAID_AMOUNT} = '{entry.PaidAmount}' " +
                    $"WHERE {LoansTable.COLUMN_ID} = '{entry.Id}';";

                    command.ExecuteNonQuery();

                    // Attempt to commit the transaction.
                    transaction.Commit();

                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public List<LoanTableEntry> GetAll()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                List<LoanTableEntry> result = new List<LoanTableEntry>();

                connection.Open();

                var (transaction, command) = SqlDatabaseHelper.InitialzieSqlTransaction(connection);

                try
                {
                    command.CommandText = $"SELECT * FROM {LoansTable.TABLE_NAME}";

                    var sqlReader = command.ExecuteReader();

                    while (sqlReader!.Read())
                    {
                        var dataEntry = mapperProvider.Map<NpgsqlDataReader, LoanTableEntry>(sqlReader);

                        result.Add(dataEntry);
                    }

                    return result;
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public LoanTableEntry? GetById(string id)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                LoanTableEntry? result = null;

                connection.Open();

                var (transaction, command) = SqlDatabaseHelper.InitialzieSqlTransaction(connection);

                try
                {
                    command.CommandText = $"SELECT * FROM {LoansTable.TABLE_NAME} WHERE {LoansTable.COLUMN_ID} = '{id}'";

                    var sqlReader = command.ExecuteReader();

                    if (sqlReader.HasRows)
                    {
                        sqlReader.Read();
                        result = mapperProvider.Map<NpgsqlDataReader, LoanTableEntry>(sqlReader);
                    }

                    return result;
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public List<LoanTableEntry> GetByAccount(string relatedAccount)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                List<LoanTableEntry> result = new List<LoanTableEntry>();

                connection.Open();

                var (transaction, command) = SqlDatabaseHelper.InitialzieSqlTransaction(connection);

                try
                {
                    var cardIdsOfAccount = new List<string>();

                    command.CommandText = $"SELECT * FROM {LoansTable.TABLE_NAME} WHERE {LoansTable.COLUMN_RELATED_ACCOUNT} = '{relatedAccount}'";

                    using (var sqlReader = command.ExecuteReader())
                    {
                        while (sqlReader!.Read())
                        {
                            var dataEntry = mapperProvider.Map<NpgsqlDataReader, LoanTableEntry>(sqlReader);

                            result.Add(dataEntry);
                        }
                    }

                    return result;
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public List<LoanTableEntry> GetByOffer(string loanOffer)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                List<LoanTableEntry> result = new List<LoanTableEntry>();

                connection.Open();

                var (transaction, command) = SqlDatabaseHelper.InitialzieSqlTransaction(connection);

                try
                {
                    var cardIdsOfAccount = new List<string>();

                    command.CommandText = $"SELECT * FROM {LoansTable.TABLE_NAME} WHERE {LoansTable.COLUMN_RELATED_OFFER} = '{loanOffer}'";

                    using (var sqlReader = command.ExecuteReader())
                    {
                        while (sqlReader!.Read())
                        {
                            var dataEntry = mapperProvider.Map<NpgsqlDataReader, LoanTableEntry>(sqlReader);

                            result.Add(dataEntry);
                        }
                    }

                    return result;
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public bool DeleteAll()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                var (transaction, command) = SqlDatabaseHelper.InitialzieSqlTransaction(connection);

                try
                {
                    command.CommandText = $"DELETE FROM {LoansTable.TABLE_NAME} WHERE 1=1";

                    command.ExecuteNonQuery();

                    // Attempt to commit the transaction.
                    transaction.Commit();

                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
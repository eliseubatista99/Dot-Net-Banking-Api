using BankingAppDataTier.Contracts.Constants;
using BankingAppDataTier.Contracts.Database;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingAppDataTier.Contracts.Providers
{
    public interface IDatabaseProvider
    {
        public SqlDataReader? ExecuteQuery(string query);

        public bool ExecuteNonQuery(string query);

        public bool ExecuteNonQueries(List<string> queries);

    }
}

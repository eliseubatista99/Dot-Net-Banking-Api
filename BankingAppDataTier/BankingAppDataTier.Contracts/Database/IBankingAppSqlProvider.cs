using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingAppDataTier.Contracts.Database
{
    public interface IBankingAppSqlProvider
    {
        public SqlDataReader ExecuteReadQuery(string query);

        public bool ExecuteWriteQuery(string query);
    }
}

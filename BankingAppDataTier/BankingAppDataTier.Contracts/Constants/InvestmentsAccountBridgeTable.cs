using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingAppDataTier.Contracts.Constants
{
    public static class InvestmentsAccountBridgeTable
    {
        public static string TABLE_NAME = "INVESTMENTS_ACCOUNT_BRIDGE";

        public static string COLUMN_ID = "ID";

        public static string COLUMN_SOURCE_ACCOUNT_ID = "SOURCE_ACCOUNT_ID";

        public static string COLUMN_INVESTMENTS_ACCOUNT_ID = "INVESTMENTS_ACCOUNT_ID";

        public static string COLUMN_DURATION = "DURATION";

        public static string COLUMN_INTEREST = "INTEREST";
    }
}

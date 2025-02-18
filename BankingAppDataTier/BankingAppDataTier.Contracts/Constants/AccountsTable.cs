using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingAppDataTier.Contracts.Constants
{
    public static class AccountsTable
    {
        public static string TABLE_NAME = "ACCOUNTS";

        public static string COLUMN_ID = "ID";

        public static string COLUMN_TYPE = "ACCOUNT_TYPE";

        public static string COLUMN_BALANCE = "BALANCE";

        public static string COLUMN_NAME = "ACCOUNT_NAME";

        public static string COLUMN_IMAGE = "IMAGE";

        public static string COLUMN_SOURCE_ACCOUNT_ID = "SOURCE_ACCOUNT_ID";

        public static string COLUMN_DURATION = "DURATION";

        public static string COLUMN_INTEREST = "INTEREST";

        public const string ACCOUNT_TYPE_CURRENT = "CU";

        public const string ACCOUNT_TYPE_SAVINGS = "SA";

        public const string ACCOUNT_TYPE_INVESTMENTS = "IN";
    }
}

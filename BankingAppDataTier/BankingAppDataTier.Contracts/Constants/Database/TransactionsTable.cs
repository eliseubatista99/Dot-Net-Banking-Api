using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingAppDataTier.Contracts.Constants.Database
{
    public static class TransactionsTable
    {
        public static string TABLE_NAME = "´TRANSACTIONS";

        public static string COLUMN_ID = "ID";

        public static string COLUMN_TRANSACTION_DATE = "TRANSACTION_DATE";

        public static string COLUMN_DESCRIPTION = "DESCRIPTION";

        public static string COLUMN_SOURCE_ACCOUNT = "SOURCE_ACCOUNT";

        public static string COLUMN_DESTINATION_NAME = "DESTINATION_NAME";

        public static string COLUMN_DESTINATION_ACCOUNT = "DESTINATION_ACCOUNT";

        public static string COLUMN_SOURCE_CARD = "SOURCE_CARD";

        public static string COLUMN_AMOUNT = "AMOUNT";

        public static string COLUMN_FEES = "FEES";

        public static string COLUMN_URGENT = "URGENT";
    }
}

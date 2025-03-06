using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingAppDataTier.Contracts.Constants.Database
{
    public static class LoansTable
    {
        public static string TABLE_NAME = "LOANS";

        public static string COLUMN_ID = "ID";

        public static string COLUMN_RELATED_OFFER = "RELATED_OFFER";

        public static string COLUMN_START_DATE = "START_DATE";

        public static string COLUMN_DURATION = "DURATION";

        public static string COLUMN_AMOUNT = "AMOUNT";
    }
}

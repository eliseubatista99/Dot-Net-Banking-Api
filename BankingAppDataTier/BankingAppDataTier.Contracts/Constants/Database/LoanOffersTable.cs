using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingAppDataTier.Contracts.Constants.Database
{
    public static class LoanOffersTable
    {
        public static string TABLE_NAME = "LOAN_OFFERS";

        public static string COLUMN_ID = "ID";

        public static string COLUMN_TYPE = "LOAN_TYPE";

        public static string COLUMN_MAX_EFFORT = "MAX_EFFORT";

        public static string COLUMN_INTEREST = "INTEREST";

        public static string COLUMN_IS_ACTIVE = "IS_ACTIVE";

    }
}

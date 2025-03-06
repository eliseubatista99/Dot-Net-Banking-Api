using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingAppDataTier.Contracts.Constants.Database
{
    public static class CardsTable
    {
        public static string TABLE_NAME = "CARDS";

        public static string COLUMN_ID = "ID";

        public static string COLUMN_NAME = "NAME";

        public static string COLUMN_RELATED_ACCOUNT_ID = "RELATED_ACCOUNT_ID";

        public static string COLUMN_PLASTIC_ID = "PLASTIC_ID";

        public static string COLUMN_BALANCE = "BALANCE";

        public static string COLUMN_PAYMENT_DAY = "PAYMENT_DAY";

        public static string COLUMN_REQUEST_DATE = "REQUEST_DATE";

        public static string COLUMN_ACTIVATION_DATE = "ACTIVATION_DATE";

        public static string COLUMN_EXPIRATION_DATE = "EXPIRATION_DATE";
    }
}

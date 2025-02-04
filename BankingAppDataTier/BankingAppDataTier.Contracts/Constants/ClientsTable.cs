using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingAppDataTier.Contracts.Constants
{
    public static class ClientsTable
    {
        public static string TABLE_NAME = "CLIENTS";

        public static string COLUMN_ID = "ID";

        public static string COLUMN_PASSWORD = "PASSWORD";

        public static string COLUMN_NAME = "NAME";

        public static string COLUMN_SURNAME = "SURNAME";

        public static string COLUMN_BIRTH_DATE = "BIRTHDATE";

        public static string COLUMN_VAT_NUMBER = "VAT";

        public static string COLUMN_PHONE_NUMBER = "PHONE";

        public static string COLUMN_EMAIL = "EMAIL";

        public static string BuildCreateTableQuery()
        {
            return $"IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[{ClientsTable.TABLE_NAME}]')  AND type in (N'U')) " +
                $"BEGIN " +
                $"CREATE TABLE {ClientsTable.TABLE_NAME} " +
                $"(" +
                $"{ClientsTable.COLUMN_ID} VARCHAR(64) NOT NULL," +
                $"{ClientsTable.COLUMN_NAME} VARCHAR(64) NOT NULL," +
                $"{ClientsTable.COLUMN_SURNAME} VARCHAR(64) NOT NULL," +
                $"{ClientsTable.COLUMN_BIRTH_DATE} DATE NOT NULL," +
                $"{ClientsTable.COLUMN_VAT_NUMBER} VARCHAR(30) NOT NULL," +
                $"{ClientsTable.COLUMN_PHONE_NUMBER} VARCHAR(20) NOT NULL," +
                $"{ClientsTable.COLUMN_EMAIL} VARCHAR(60) NOT NULL," +
                $"PRIMARY KEY ({ClientsTable.COLUMN_ID} )" +
                $") " +
                $"END";
        }
    }
}

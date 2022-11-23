using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.Utility
{
    public static class GlobalConstant
    {
        public static string ConnectionString;
        public static string xlsConnectionString;
        public static string EncryptionKey;
        public static string DateFormat;

        public static DateTime MinSQLDateTime; //= DateTime.Parse("01/01/1753", provider, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
        public static DateTime MaxSQLDateTime;
        public static string MaxdateToday;
        public static DateTime Maxdate;
        public static DateTime MaxdateAddOneYear;

        static GlobalConstant()
        {
            //Connection for SQL Server Database
            ConnectionString = ConfigurationManager.ConnectionStrings["SqlConnectionString11"].ConnectionString;
            //EncryptionKey = ConfigurationManager.AppSettings["EncryptionKey"];
            DateFormat = ConfigurationManager.AppSettings["DateFormat"];
            IFormatProvider provider = new System.Globalization.CultureInfo("en-GB", true);
            MinSQLDateTime = DateTime.Parse("01/01/1753", provider, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            MaxSQLDateTime = DateTime.Parse("31/12/2050", provider, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            MaxdateAddOneYear = System.DateTime.Now.AddYears(1);
        }
    }
}

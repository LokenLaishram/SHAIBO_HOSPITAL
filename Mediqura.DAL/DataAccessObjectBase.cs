using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.DAL
{
    public class DataAccessObjectBase
    {
        static IFormatProvider provider = new System.Globalization.CultureInfo("en-GB", true);
        public DateTime SQL_DateTimeMininum = DateTime.Parse("01/01/1753", provider, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
        public DateTime SQL_DateTimeMaximum = DateTime.Parse("31/12/9999", provider, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
    }
}

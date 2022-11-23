using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.DAL
{
    public class DatabaseConstants
    {
        internal static class CommonConstants
        {
            internal static class Parameters
            {
                public const string CurrentIndex = "@CurrentIndex";
                public const string PageSize = "@PageSize";

                public const string FinancialYearID = "@FinancialYearID";
                public const string CompanyID = "@HospitalID";

                public const string IsActive = "@IsActive";
                public const string AddedBy = "@AddedBy ";
                public const string AddedDTM = "@AddedDTM";
                public const string LastModBy = "@LastModBy";
                public const string LastModDTM = "@LastModDTM";
                public const string ActionType = "@ActionType";
                public const string IsExist = "@IsExist";
            }
        }
        #region Lookups Constants of database
        internal static class LookupConstants
        {
            internal static class StoreProcedures
            {
                public const string GetMasterLookup = "usp_GetMasterLookup";
            }
            internal static class Parameters
            {
                public const string LookupsName = "@LookupsName";
                public const string ItemId = "@ItemId";

            }
        }
        #endregion
    }
}

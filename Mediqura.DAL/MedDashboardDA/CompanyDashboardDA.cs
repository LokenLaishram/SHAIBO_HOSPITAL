using Mediqura.CommonData.MedDashboardData;
using Mediqura.Utility;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using Mediqura.Utility.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.DAL.MedDashboardDA
{
   public class CompanyDashboardDA
    {
       public List<CompanyDashboardData> LoadDashboard()
        {
            List<CompanyDashboardData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[0];
        
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "dbo.MDQ_CompanyDashboard", arParms);
                    List<CompanyDashboardData> lstDash = ORHelper<CompanyDashboardData>.FromDataReaderToList(sqlReader);
                    result = lstDash;
                }
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.DataAccessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
                throw new DataAccessException("5000001", ex);
            }
            return result;
        }
    }

}

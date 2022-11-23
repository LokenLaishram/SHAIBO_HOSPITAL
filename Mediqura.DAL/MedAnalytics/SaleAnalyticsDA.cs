using Mediqura.CommonData.MedAnalytics;
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

namespace Mediqura.DAL.MedAnalytics
{
   public class SaleAnalyticsDA
    {
       public List<SaleAnalyticsGetData> GetSalesAnalytics(SalesAnalyticsData objD)
        {
            List<SaleAnalyticsGetData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[3];
                    arParms[0] = new SqlParameter("@Month", SqlDbType.Int);
                    arParms[0].Value = objD.Month;

                    arParms[1] = new SqlParameter("@DateTo", SqlDbType.DateTime);
                    arParms[1].Value = objD.DateTo;

                    arParms[2] = new SqlParameter("@DateFrom", SqlDbType.DateTime);
                    arParms[2].Value = objD.DateFrom;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetSalesAnalytics", arParms);

                    List<SaleAnalyticsGetData> lstresult = ORHelper<SaleAnalyticsGetData>.FromDataReaderToList(sqlReader);
                    result = lstresult;
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

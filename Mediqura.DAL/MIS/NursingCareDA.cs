using Mediqura.CommonData.MIS;
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

namespace Mediqura.DAL.MIS
{
    public class NursingCareDA
    {
        public List<NursingCareReportData> GetNursingCareList(NursingCareReportData objData)
        {
            List<NursingCareReportData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[3];

                    arParms[0] = new SqlParameter("@dateFrom", SqlDbType.DateTime);
                    arParms[0].Value = objData.DateFrom;

                    arParms[1] = new SqlParameter("@dateto", SqlDbType.DateTime);
                    arParms[1].Value = objData.DateTo;

                    arParms[2] = new SqlParameter("@serviceID", SqlDbType.Int);
                    arParms[2].Value = objData.ServiceID;


                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "MDQ_get_NursingCareDetails", arParms);
                    List<NursingCareReportData> ListData = ORHelper<NursingCareReportData>.FromDataReaderToList(sqlReader);
                    result = ListData;
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

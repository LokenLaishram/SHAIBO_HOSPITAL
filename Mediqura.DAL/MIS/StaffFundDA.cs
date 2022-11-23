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
  public class StaffFundDA
    {
        public List<StaffFundReportData> GetTransactionList(StaffFundReportData objData)
        {
            List<StaffFundReportData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[4];

                    arParms[0] = new SqlParameter("@Service", SqlDbType.Int);
                    arParms[0].Value = objData.ServiceID;

                    arParms[1] = new SqlParameter("@dateFrom", SqlDbType.DateTime);
                    arParms[1].Value = objData.DateFrom;

                    arParms[2] = new SqlParameter("@dateto", SqlDbType.DateTime);
                    arParms[2].Value = objData.DateTo;

                    arParms[3] = new SqlParameter("@LoginName", SqlDbType.VarChar);
                    arParms[3].Value = objData.EmpName;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_StaffFund_report", arParms);
                    List<StaffFundReportData> ListData = ORHelper<StaffFundReportData>.FromDataReaderToList(sqlReader);
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
        public List<StaffFundReportData> GetStaffFundList(StaffFundReportData objData)
        {
            List<StaffFundReportData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@dateFrom", SqlDbType.DateTime);
                    arParms[0].Value = objData.DateFrom;

                    arParms[1] = new SqlParameter("@dateto", SqlDbType.DateTime);
                    arParms[1].Value = objData.DateTo;

                   
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "MDQ_get_StaffFund_details", arParms);
                    List<StaffFundReportData> ListData = ORHelper<StaffFundReportData>.FromDataReaderToList(sqlReader);
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

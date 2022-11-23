using Mediqura.CommonData.AdmissionData;
using Mediqura.CommonData.PatientData;
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
using Mediqura.CommonData.OTData;
using Mediqura.CommonData.MedNurseData;
namespace Mediqura.DAL.MedNurseDA
{
    public class InvestigationChartDA
    {
        public List<InvestigationChartData> GetPatientName(InvestigationChartData objD)
        {
            List<InvestigationChartData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@PatientName", SqlDbType.VarChar);
                    arParms[0].Value = objD.PatientName;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetPatientName", arParms);
                    List<InvestigationChartData> lstresult = ORHelper<InvestigationChartData>.FromDataReaderToList(sqlReader);
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
        public List<InvestigationChartData> GetInvestPatientDetail(InvestigationChartData objpat)
        {
            List<InvestigationChartData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                    arParms[0].Value = objpat.IPNo;

                    //arParms[0] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    //arParms[0].Value = objpat.UHID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetInvestPatientDetail", arParms);
                    List<InvestigationChartData> listdetails = ORHelper<InvestigationChartData>.FromDataReaderToList(sqlReader);
                    result = listdetails;
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
        public List<InvestigationChartData> SearchInvestPatientDetail(InvestigationChartData objpat)
        {
            List<InvestigationChartData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[4];

                    arParms[0] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[0].Value = objpat.UHID;

                    arParms[1] = new SqlParameter("@Year", SqlDbType.Int);
                    arParms[1].Value = objpat.Year;

                    arParms[2] = new SqlParameter("@Month", SqlDbType.Int);
                    arParms[2].Value = objpat.MonthID;

                    arParms[3] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                    arParms[3].Value = objpat.IPNo;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_SearchInvestPatientDetail", arParms);
                    List<InvestigationChartData> listdetails = ORHelper<InvestigationChartData>.FromDataReaderToList(sqlReader);
                    result = listdetails;
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

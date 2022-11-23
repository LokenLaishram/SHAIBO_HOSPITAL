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
    public class IntakeOutputChartDA
    {
        public List<IntakeOutputChartData> GetPatientName(IntakeOutputChartData objD)
        {
            List<IntakeOutputChartData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@PatientName", SqlDbType.VarChar);
                    arParms[0].Value = objD.PatientName;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetPatientNamelist", arParms);
                    List<IntakeOutputChartData> lstresult = ORHelper<IntakeOutputChartData>.FromDataReaderToList(sqlReader);
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
        public List<IntakeOutputChartData> GetIntakeOutputPatientDetail(IntakeOutputChartData objpat)
        {
            List<IntakeOutputChartData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[4];

                    arParms[0] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                    arParms[0].Value = objpat.IPNo;

                    arParms[1] = new SqlParameter("@searchby", SqlDbType.Int);
                    arParms[1].Value = objpat.searchby;

                    arParms[2] = new SqlParameter("@DateFrom", SqlDbType.DateTime);
                    arParms[2].Value = objpat.DateFrom;

                    arParms[3] = new SqlParameter("@DateTo", SqlDbType.DateTime);
                    arParms[3].Value = objpat.DateTo;



                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetIntakeOutputPatientDetail", arParms);
                    List<IntakeOutputChartData> listdetails = ORHelper<IntakeOutputChartData>.FromDataReaderToList(sqlReader);
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
        public int InsertIntakeOutputdetails(IntakeOutputChartData objpat)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[21];

                    arParms[0] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                    arParms[0].Value = objpat.IPNo;

                    arParms[1] = new SqlParameter("@PatientName", SqlDbType.VarChar);
                    arParms[1].Value = objpat.PatientName;

                    arParms[2] = new SqlParameter("@IntakeOutputDate", SqlDbType.DateTime);
                    arParms[2].Value = objpat.IntakeOutputDate;

                    arParms[3] = new SqlParameter("@fluidsstart", SqlDbType.DateTime);
                    arParms[3].Value = objpat.fluidsstart;

                    arParms[4] = new SqlParameter("@fluidsend", SqlDbType.DateTime);
                    arParms[4].Value = objpat.fluidsend;

                    arParms[5] = new SqlParameter("@fluids", SqlDbType.Float);
                    arParms[5].Value = objpat.fluids;

                    arParms[6] = new SqlParameter("@oralstart", SqlDbType.DateTime);
                    arParms[6].Value = objpat.oralstart;

                    arParms[7] = new SqlParameter("@oralend", SqlDbType.DateTime);
                    arParms[7].Value = objpat.oralend;

                    arParms[8] = new SqlParameter("@oral", SqlDbType.Float);
                    arParms[8].Value = objpat.oral;

                    arParms[9] = new SqlParameter("@urinestart", SqlDbType.DateTime);
                    arParms[9].Value = objpat.urinestart;

                    arParms[10] = new SqlParameter("@urineend", SqlDbType.DateTime);
                    arParms[10].Value = objpat.urineend;

                    arParms[11] = new SqlParameter("@urine", SqlDbType.Float);
                    arParms[11].Value = objpat.urine;

                    arParms[12] = new SqlParameter("@others", SqlDbType.Float);
                    arParms[12].Value = objpat.others;

                    arParms[13] = new SqlParameter("@remarks", SqlDbType.VarChar);
                    arParms[13].Value = objpat.remarks;

                    arParms[14] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[14].Direction = ParameterDirection.Output;

                    arParms[15] = new SqlParameter("@AddedBy", SqlDbType.BigInt);
                    arParms[15].Value = objpat.EmployeeID;                                      

                    arParms[16] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[16].Value = objpat.UHID;

                    arParms[17] = new SqlParameter("@ID", SqlDbType.BigInt);
                    arParms[17].Value = objpat.ID;

                    arParms[18] = new SqlParameter("@totalintakechart", SqlDbType.Float);
                    arParms[18].Value = objpat.totalintakechart;

                    arParms[19] = new SqlParameter("@totaloutputchart", SqlDbType.Float);
                    arParms[19].Value = objpat.totaloutputchart;

                    arParms[20] = new SqlParameter("@totalbalancechart", SqlDbType.Float);
                    arParms[20].Value = objpat.totalbalancechart;

                    

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_UpdateIntakeOutputDetails", arParms);
                    if (result_ > 0 || result_ == -1)
                    {
                        result = Convert.ToInt32(arParms[14].Value);
                    }
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
        public int CancelIntakeOutputChartDataLists(IntakeOutputChartData objpat)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[3];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.BigInt);
                    arParms[0].Value = objpat.ID;

                    arParms[1] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[1].Direction = ParameterDirection.Output;

                    arParms[2] = new SqlParameter("@UserId", SqlDbType.BigInt);
                    arParms[2].Value = objpat.EmployeeID;
                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_CancelIntakeoutputDataLists", arParms);
                    if (result_ > 0 || result_ == -1)
                        result = Convert.ToInt32(arParms[1].Value);
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

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
    public class PatientSugarChartDA
    {
        public int UpdatePatientSugar(PatientSugarChartData objData)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[9];

                    arParms[0] = new SqlParameter("@Ipno", SqlDbType.VarChar);
                    arParms[0].Value = objData.Ipno;

                    arParms[1] = new SqlParameter("@AddedDate", SqlDbType.DateTime);
                    arParms[1].Value = objData.AddedDate;

                    arParms[2] = new SqlParameter("@RBSmgDl", SqlDbType.VarChar);
                    arParms[2].Value = objData.RBSmgDl;

                    arParms[3] = new SqlParameter("@remarks", SqlDbType.VarChar);
                    arParms[3].Value = objData.remarks;

                    arParms[4] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[4].Direction = ParameterDirection.Output;

                    arParms[5] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[5].Value = objData.EmployeeID;

                    arParms[6] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[6].Value = objData.HospitalID;

                    arParms[7] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[7].Value = objData.FinancialYearID;

                    arParms[8] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                    arParms[8].Value = objData.IPaddress;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_UpdatePatientSugarDetails", arParms);
                    if (result_ > 0 || result_ == -1)
                        result = Convert.ToInt32(arParms[4].Value);
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
        public List<PatientSugarChartData> GetSugarDetailByIPNo(PatientSugarChartData objData)
        {
            List<PatientSugarChartData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@Ipno", SqlDbType.NVarChar);
                    arParms[0].Value = objData.Ipno;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetPatientSugarDetailList", arParms);
                    List<PatientSugarChartData> lstOTRolesDetails = ORHelper<PatientSugarChartData>.FromDataReaderToList(sqlReader);
                    result = lstOTRolesDetails;
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
        //public List<PatientSugarChartData> GetOTRoleDetailsByID(PatientSugarChartData objData)
        //{
        //    List<PatientSugarChartData> result = null;
        //    try
        //    {
        //        {
        //            SqlParameter[] arParms = new SqlParameter[1];

        //            arParms[0] = new SqlParameter("@StaionID", SqlDbType.Int);
        //            arParms[0].Value = objData.StaionID;

        //            SqlDataReader sqlReader = null;
        //            sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_GetStationDetailsByID", arParms);
        //            List<PatientSugarChartData> lstLabUnitDetails = ORHelper<PatientSugarChartData>.FromDataReaderToList(sqlReader);
        //            result = lstLabUnitDetails;
        //        }
        //    }
        //    catch (Exception ex) //Exception of the business layer(itself)//unhandle
        //    {
        //        PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.DataAccessExceptionPolicy, ex, "330001");
        //        LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
        //        throw new DataAccessException("5000001", ex);
        //    }
        //    return result;
        //}
        public int CancelSugarDetails(PatientSugarChartData objpat)
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
                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_CancelSugarDetails", arParms);
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

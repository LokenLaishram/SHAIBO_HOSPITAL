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
    public class NursingStationDA
    {
        public List<NursingStationData> GetNursingStationList(NursingStationData objSchedule)
        {
            List<NursingStationData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[4];

                    arParms[0] = new SqlParameter("@nurseID", SqlDbType.Int);
                    arParms[0].Value = objSchedule.NurseID;

                    arParms[1] = new SqlParameter("@WardID", SqlDbType.Int);
                    arParms[1].Value = objSchedule.WardID;

                    arParms[2] = new SqlParameter("@StationTypeID", SqlDbType.Int);
                    arParms[2].Value = objSchedule.StationTypeID;

                    arParms[3] = new SqlParameter("@GenStockID", SqlDbType.Int);
                    arParms[3].Value = objSchedule.GenStockID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_NUR_Get_GetNursingStationList", arParms);
                    List<NursingStationData> lstLabGroupTypeDetails = ORHelper<NursingStationData>.FromDataReaderToList(sqlReader);
                    result = lstLabGroupTypeDetails;
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
        public int UpdateNursingStationAssignDetails(NursingStationData objbill)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[9];

                    arParms[0] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[0].Value = objbill.FinancialYearID;

                    arParms[1] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[1].Value = objbill.EmployeeID;

                    arParms[2] = new SqlParameter("@Output", SqlDbType.Int);
                    arParms[2].Direction = ParameterDirection.Output;

                    arParms[3] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[3].Value = objbill.HospitalID;

                    arParms[4] = new SqlParameter("@StationID", SqlDbType.Int);
                    arParms[4].Value = objbill.StationTypeID;

                    arParms[5] = new SqlParameter("@WardID", SqlDbType.Int);
                    arParms[5].Value = objbill.WardID;

                    arParms[6] = new SqlParameter("@NurseID", SqlDbType.Int);
                    arParms[6].Value = objbill.NurseID;

                    arParms[7] = new SqlParameter("@GenStockID", SqlDbType.Int);
                    arParms[7].Value = objbill.GenStockID;

                    arParms[8] = new SqlParameter("@ActionType", SqlDbType.Int);
                    arParms[8].Value = objbill.ActionType;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_UpdateNursingStationAssignDetails", arParms);
                    if (result_ > 0 || result_ == -1)
                    {
                        result = Convert.ToInt32(arParms[2].Value);
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
        public int DeleteNursesByID(NursingStationData objbill)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[4];
                               
                    arParms[0] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[0].Value = objbill.EmployeeID;

                    arParms[1] = new SqlParameter("@Output", SqlDbType.Int);
                    arParms[1].Direction = ParameterDirection.Output;

                    arParms[2] = new SqlParameter("@NurseID", SqlDbType.Int);
                    arParms[2].Value = objbill.NurseID;

                    arParms[3] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    arParms[3].Value = objbill.Remarks;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_DeleteNursesbyID", arParms);
                    if (result_ > 0 || result_ == -1)
                    {
                        result = Convert.ToInt32(arParms[1].Value);
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
        public List<NursingStationData> GetNursingList(NursingStationData objSchedule)
        {
            List<NursingStationData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[0];

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_NUR_Get_GetNurseList", arParms);
                    List<NursingStationData> lstLabGroupTypeDetails = ORHelper<NursingStationData>.FromDataReaderToList(sqlReader);
                    result = lstLabGroupTypeDetails;
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

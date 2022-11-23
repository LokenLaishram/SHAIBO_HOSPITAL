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

namespace Mediqura.DAL.OTDA
{
    public class OTSchedulingDA
    {
        public List<OTSchedulingData> GetPatientList(OTSchedulingData objpat)
        {
            List<OTSchedulingData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[6];

                    arParms[0] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[0].Value = objpat.UHID;

                    arParms[1] = new SqlParameter("@DoctorID", SqlDbType.BigInt);
                    arParms[1].Value = objpat.DoctorID;

                    arParms[2] = new SqlParameter("@TheatreID", SqlDbType.Int);
                    arParms[2].Value = objpat.TheatreID;

                    arParms[3] = new SqlParameter("@DateFrom", SqlDbType.Date);
                    arParms[3].Value = objpat.DateFrom;

                    arParms[4] = new SqlParameter("@DateTo", SqlDbType.Date);
                    arParms[4].Value = objpat.DateTo;

                    arParms[5] = new SqlParameter("@OTStatusID", SqlDbType.Int);
                    arParms[5].Value = objpat.OTStatusID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_OTSchedulingDetails", arParms);
                    List<OTSchedulingData> listOTdetails = ORHelper<OTSchedulingData>.FromDataReaderToList(sqlReader);
                    result = listOTdetails;
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
        public int UpdateOTSchedulingDetails(OTSchedulingData objbill)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[5];

                    arParms[0] = new SqlParameter("@XMLData", SqlDbType.Xml);
                    arParms[0].Value = objbill.XMLData;

                    arParms[1] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[1].Value = objbill.FinancialYearID;

                    arParms[2] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[2].Value = objbill.EmployeeID;

                    arParms[3] = new SqlParameter("@Output", SqlDbType.Int);
                    arParms[3].Direction = ParameterDirection.Output;
                
                    arParms[4] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                    arParms[4].Value = objbill.IPaddress;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Update_OTSchedulingList", arParms);
                    if (result_ > 0 || result_ == -1)
                        result = Convert.ToInt32(arParms[3].Value);
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
        public List<OTSchedulingData> GetOTTodayList(OTSchedulingData objpat)
        {
            List<OTSchedulingData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@DateFrom", SqlDbType.DateTime);
                    arParms[0].Value = objpat.DateFrom;

                    arParms[1] = new SqlParameter("@TheatreID", SqlDbType.Int);
                    arParms[1].Value = objpat.TheatreID;   

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Today_OT_ListDetails", arParms);
                    List<OTSchedulingData> listTodayOTdetails = ORHelper<OTSchedulingData>.FromDataReaderToList(sqlReader);
                    result = listTodayOTdetails;
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
        public int CancelOTScheduling(OTSchedulingData objpat)
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

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_CancelOTScheduling", arParms);
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
        public List<OTSchedulingData> GetOTPatientList(OTSchedulingData objpat)
        {
            List<OTSchedulingData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[6];

                    arParms[0] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[0].Value = objpat.UHID;

                    arParms[1] = new SqlParameter("@DoctorID", SqlDbType.BigInt);
                    arParms[1].Value = objpat.DoctorID;

                    arParms[2] = new SqlParameter("@TheatreID", SqlDbType.Int);
                    arParms[2].Value = objpat.TheatreID;

                    arParms[3] = new SqlParameter("@DateFrom", SqlDbType.Date);
                    arParms[3].Value = objpat.DateFrom;

                    arParms[4] = new SqlParameter("@DateTo", SqlDbType.Date);
                    arParms[4].Value = objpat.DateTo;

                    arParms[5] = new SqlParameter("@OTStatusID", SqlDbType.Int);
                    arParms[5].Value = objpat.OTStatusID;

                   

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_OTSchedulingListDetails", arParms);
                    List<OTSchedulingData> listOTdetails = ORHelper<OTSchedulingData>.FromDataReaderToList(sqlReader);
                    result = listOTdetails;
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
     
        //****************NEW OT SCHEDULER**********************//

        public List<OTSchedulingData> GetPatientDetailsByUHID(OTSchedulingData objD)
        {
            List<OTSchedulingData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[0].Value = objD.UHID;
                 
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetPatientNameByUHID", arParms);
                    List<OTSchedulingData> lstresult = ORHelper<OTSchedulingData>.FromDataReaderToList(sqlReader);
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
        public int UpdateOTScheduler(OTSchedulingData objot)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[25];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                    arParms[0].Value = objot.ID;

                    arParms[1] = new SqlParameter("@UHID", SqlDbType.VarChar);
                    arParms[1].Value = objot.UHID;

                    arParms[2] = new SqlParameter("@PatientName", SqlDbType.VarChar);
                    arParms[2].Value = objot.PatientName;

                    arParms[3] = new SqlParameter("@WardBedName", SqlDbType.VarChar);
                    arParms[3].Value = objot.WardBedName;

                    arParms[4] = new SqlParameter("@OTDate", SqlDbType.DateTime);
                    arParms[4].Value = objot.Date;

                    arParms[5] = new SqlParameter("@StartHour", SqlDbType.Int);
                    arParms[5].Value = objot.StartHour;

                    arParms[6] = new SqlParameter("@StartMinute", SqlDbType.Int);
                    arParms[6].Value = objot.StartMinute;

                    arParms[7] = new SqlParameter("@StartMeridiem", SqlDbType.Int);
                    arParms[7].Value = objot.StartMeridiem;

                    arParms[8] = new SqlParameter("@EndHour", SqlDbType.Int);
                    arParms[8].Value = objot.EndHour;

                    arParms[9] = new SqlParameter("@EndMinute", SqlDbType.Int);
                    arParms[9].Value = objot.EndMinute;

                    arParms[10] = new SqlParameter("@EndMeridiem", SqlDbType.VarChar);
                    arParms[10].Value = objot.EndMeridiem;

                    arParms[11] = new SqlParameter("@DoctorID", SqlDbType.Int);
                    arParms[11].Value = objot.DoctorID;

                    arParms[12] = new SqlParameter("@DoctorName", SqlDbType.VarChar);
                    arParms[12].Value = objot.Surgeon;

                    arParms[13] = new SqlParameter("@AnaesthetistID", SqlDbType.Int);
                    arParms[13].Value = objot.AnaesthetistID;

                    arParms[14] = new SqlParameter("@PACID", SqlDbType.Int);
                    arParms[14].Value = objot.PACID;

                    arParms[15] = new SqlParameter("@OTStatusID", SqlDbType.Int);
                    arParms[15].Value = objot.OTStatusID;

                    arParms[16] = new SqlParameter("@OperationName", SqlDbType.VarChar);
                    arParms[16].Value = objot.OperationName;

                    arParms[17] = new SqlParameter("@TheatreID", SqlDbType.Int);
                    arParms[17].Value = objot.TheatreID;

                    arParms[18] = new SqlParameter("@Remark", SqlDbType.VarChar);
                    arParms[18].Value = objot.Remark;

                    arParms[19] = new SqlParameter("@AddedBy", SqlDbType.BigInt);
                    arParms[19].Value = objot.EmployeeID;

                    arParms[20] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[20].Value = objot.HospitalID;

                    arParms[21] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[21].Value = objot.FinancialYearID;

                    arParms[22] = new SqlParameter("@ActionType", SqlDbType.SmallInt);
                    arParms[22].Value = objot.ActionType;

                    arParms[23] = new SqlParameter("@IPaddress", SqlDbType.VarChar);
                    arParms[23].Value = objot.IPaddress;

                    arParms[24] = new SqlParameter("@Output", SqlDbType.BigInt);
                    arParms[24].Direction = ParameterDirection.Output;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Update_OTScheduleDetails", arParms);
                    if (result_ > 0 || result_ == -1)
                    {
                        result = Convert.ToInt32(arParms[24].Value);
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
        public List<OTSchedulingData> GetOTScheduleByID(OTSchedulingData objpat)
        {
            List<OTSchedulingData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.BigInt);
                    arParms[0].Value = objpat.ID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetOTScheduleByID", arParms);
                    List<OTSchedulingData> listotdetails = ORHelper<OTSchedulingData>.FromDataReaderToList(sqlReader);
                    result = listotdetails;
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
        public int DeleteOTScheduleByID(OTSchedulingData objotschedule)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[5];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.BigInt);
                    arParms[0].Value = objotschedule.ID;

                    arParms[1] = new SqlParameter("@OTNo", SqlDbType.VarChar);
                    arParms[1].Value = objotschedule.OTNo;

                    arParms[2] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    arParms[2].Value = objotschedule.Remark;

                    arParms[3] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[3].Value = objotschedule.EmployeeID;

                    arParms[4] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[4].Direction = ParameterDirection.Output;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GEN_STR_DeleteOTScgeduleByOTNo", arParms);
                    if (result_ > 0 || result_ == -1)
                    {
                        result = Convert.ToInt32(arParms[4].Value);
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
    }
}

using Mediqura.CommonData.AdmissionData;
using Mediqura.CommonData.MedUtilityData;
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

namespace Mediqura.DAL.MedUtilityDA
{
    public class AppoinmentScheduleDA
    {
        public List<AppoinmentScheduleData> GetSchedule(AppoinmentScheduleData objSchedule)
        {
            List<AppoinmentScheduleData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[8];

                    arParms[0] = new SqlParameter("@DoctorType", SqlDbType.Int);
                    arParms[0].Value = objSchedule.DoctorType;

                    arParms[1] = new SqlParameter("@DepartmentID", SqlDbType.Int);
                    arParms[1].Value = objSchedule.DepartmentID;

                    arParms[2] = new SqlParameter("@DoctorID", SqlDbType.Int);
                    arParms[2].Value = objSchedule.DoctorID;

                    arParms[3] = new SqlParameter("@MonthID", SqlDbType.Int);
                    arParms[3].Value = objSchedule.MonthID;

                    arParms[4] = new SqlParameter("@Year", SqlDbType.Int);
                    arParms[4].Value = objSchedule.Year;

                    arParms[5] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[5].Value = objSchedule.EmployeeID;

                    arParms[6] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[6].Value = objSchedule.HospitalID;

                    arParms[7] = new SqlParameter("@DateID", SqlDbType.BigInt);
                    arParms[7].Value = objSchedule.DateID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_CMS_Get_Appointment_Schedules", arParms);
                    List<AppoinmentScheduleData> lstLabGroupTypeDetails = ORHelper<AppoinmentScheduleData>.FromDataReaderToList(sqlReader);
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
        public List<AppoinmentScheduleData> GetYear(AppoinmentScheduleData objD)
        {
            List<AppoinmentScheduleData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];
                    arParms[0] = new SqlParameter("@YearID", SqlDbType.Int);
                    arParms[0].Value = objD.YearID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_YearByID", arParms);
                    List<AppoinmentScheduleData> lstresult = ORHelper<AppoinmentScheduleData>.FromDataReaderToList(sqlReader);
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
        public int UpdateAppoinmentDetails(AppoinmentScheduleData objAppmt)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[8];

                    arParms[0] = new SqlParameter("@DoctorType", SqlDbType.Int);
                    arParms[0].Value = objAppmt.DoctorType;

                    arParms[1] = new SqlParameter("@DepartmentID", SqlDbType.Int);
                    arParms[1].Value = objAppmt.DepartmentID;

                    arParms[2] = new SqlParameter("@DoctorID", SqlDbType.BigInt);
                    arParms[2].Value = objAppmt.DoctorID;

                    arParms[3] = new SqlParameter("@MonthID", SqlDbType.Int);
                    arParms[3].Value = objAppmt.MonthID;

                    arParms[4] = new SqlParameter("@Year", SqlDbType.Int);
                    arParms[4].Value = objAppmt.Year;

                    arParms[5] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[5].Direction = ParameterDirection.Output;

                    arParms[6] = new SqlParameter("@XMLData", SqlDbType.Xml);
                    arParms[6].Value = objAppmt.XMLData;

                    arParms[7] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[7].Value = objAppmt.EmployeeID;


                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_UpdateAppoinmentSchd", arParms);
                    if (result_ > 0 || result_ == -1)
                        result = Convert.ToInt32(arParms[5].Value);
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

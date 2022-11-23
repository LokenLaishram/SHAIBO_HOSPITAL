using Mediqura.CommonData.AdmissionData;
using Mediqura.CommonData.MedUtilityData;
using Mediqura.CommonData.MedOPDData;
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

namespace Mediqura.DAL.MedOPDDA
{
    public class DocAppoinmentDA
    {
        public List<DocAppoinmentData> GetSchedule(DocAppoinmentData objSchedule)
        {
            List<DocAppoinmentData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[7];

                    arParms[0] = new SqlParameter("@DoctorType", SqlDbType.Int);
                    arParms[0].Value = objSchedule.DoctorType;

                    arParms[1] = new SqlParameter("@DepartmentID", SqlDbType.Int);
                    arParms[1].Value = objSchedule.DepartmentID;

                    arParms[2] = new SqlParameter("@DoctorID", SqlDbType.Int);
                    arParms[2].Value = objSchedule.DoctorID;

                    arParms[3] = new SqlParameter("@DateFrom", SqlDbType.DateTime);
                    arParms[3].Value = objSchedule.DateFrom;

                    arParms[4] = new SqlParameter("@DateTo", SqlDbType.DateTime);
                    arParms[4].Value = objSchedule.DateTo;

                    arParms[5] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[5].Value = objSchedule.EmployeeID;

                    arParms[6] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[6].Value = objSchedule.HospitalID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_CMS_Get_AppointmentList1", arParms);
                    List<DocAppoinmentData> lstDocAppointmentDetails = ORHelper<DocAppoinmentData>.FromDataReaderToList(sqlReader);
                    result = lstDocAppointmentDetails;
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
        public List<DocAppoinmentData> GetSchedulePageload(DocAppoinmentData objSchedule)
        {
            List<DocAppoinmentData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[4];

                    //arParms[0] = new SqlParameter("@DoctorType", SqlDbType.Int);
                    //arParms[0].Value = objSchedule.DoctorType;

                    //arParms[1] = new SqlParameter("@DepartmentID", SqlDbType.Int);
                    //arParms[1].Value = objSchedule.DepartmentID;

                    //arParms[2] = new SqlParameter("@DoctorID", SqlDbType.Int);
                    //arParms[2].Value = objSchedule.DoctorID;

                    //arParms[3] = new SqlParameter("@MonthID", SqlDbType.Int);
                    //arParms[3].Value = objSchedule.MonthID;

                    arParms[0] = new SqlParameter("@DateFrom", SqlDbType.DateTime);
                    arParms[0].Value = objSchedule.DateFrom;

                    arParms[1] = new SqlParameter("@DateTo", SqlDbType.DateTime);
                    arParms[1].Value = objSchedule.DateTo;


                    //arParms[6] = new SqlParameter("@TodayDate", SqlDbType.DateTime);
                    //arParms[6].Value = objSchedule.TodayDate;

                    //arParms[7] = new SqlParameter("@nextday", SqlDbType.DateTime);
                    //arParms[7].Value = objSchedule.nextday;



                    arParms[2] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[2].Value = objSchedule.EmployeeID;

                    arParms[3] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[3].Value = objSchedule.HospitalID;



                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_CMS_Get_AppointmentListDefault", arParms);
                    List<DocAppoinmentData> lstDocAppointmentDetails = ORHelper<DocAppoinmentData>.FromDataReaderToList(sqlReader);
                    result = lstDocAppointmentDetails;
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

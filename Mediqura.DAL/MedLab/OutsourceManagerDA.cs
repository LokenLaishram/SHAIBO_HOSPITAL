using Mediqura.CommonData.AdmissionData;
using Mediqura.CommonData.MedUtilityData;
using Mediqura.CommonData.MedLab;
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

namespace Mediqura.DAL.MedLab
{
    public class OutsourceManagerDA
    {
        public List<LabOutsourceManagerData> GetLabList(LabOutsourceManagerData objOutSourceData)
        {
            List<LabOutsourceManagerData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[5];

                    arParms[0] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[0].Value = objOutSourceData.UHID;

                    arParms[1] = new SqlParameter("@TestID", SqlDbType.Int);
                    arParms[1].Value = objOutSourceData.TestID;

                    arParms[2] = new SqlParameter("@PatientName", SqlDbType.VarChar);
                    arParms[2].Value = objOutSourceData.PatientName;

                    arParms[3] = new SqlParameter("@DateFrom", SqlDbType.DateTime);
                    arParms[3].Value = objOutSourceData.DateFrom;

                    arParms[4] = new SqlParameter("@DateTo", SqlDbType.DateTime);
                    arParms[4].Value = objOutSourceData.DateTo;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_GetLabOutsourceDetails", arParms);
                    List<LabOutsourceManagerData> lstPatientTypeDetails = ORHelper<LabOutsourceManagerData>.FromDataReaderToList(sqlReader);
                    result = lstPatientTypeDetails;
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
        public List<LabOutsourceManagerData> GetOutsourcePageload(LabOutsourceManagerData objSchedule)
        {
            List<LabOutsourceManagerData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[4];

                    arParms[0] = new SqlParameter("@DateFrom", SqlDbType.DateTime);
                    arParms[0].Value = objSchedule.DateFrom;

                    arParms[1] = new SqlParameter("@DateTo", SqlDbType.DateTime);
                    arParms[1].Value = objSchedule.DateTo;

                    arParms[2] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[2].Value = objSchedule.EmployeeID;

                    arParms[3] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[3].Value = objSchedule.HospitalID;



                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_GetLabOutsourceLoad", arParms);
                    List<LabOutsourceManagerData> lstDocAppointmentDetails = ORHelper<LabOutsourceManagerData>.FromDataReaderToList(sqlReader);
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
        public int UpdateLabOutsource(LabOutsourceManagerData objbill)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@XMLData", SqlDbType.Xml);
                    arParms[0].Value = objbill.XMLData;

                    arParms[1] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[1].Direction = ParameterDirection.Output;

                    //arParms[0] = new SqlParameter("@IsSampleCollcteded", SqlDbType.Int);
                    //arParms[0].Value = objbill.IsSampleCollcteded;

                    //arParms[1] = new SqlParameter("@IsOutsourcedSampleSend", SqlDbType.Int);
                    //arParms[1].Value = objbill.IsOutsourcedSampleSend;

                    //arParms[2] = new SqlParameter("@ISReportDelivered", SqlDbType.Int);
                    //arParms[2].Value = objbill.ISReportDelivered;

                    //arParms[3] = new SqlParameter("@IsOutsourcedTest", SqlDbType.Int);
                    //arParms[3].Value = objbill.IsOutsourcedTest;

                   
                    //arParms[4] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    //arParms[4].Value = objbill.HospitalID;

                    //arParms[5] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    //arParms[5].Value = objbill.FinancialYearID;

                    //arParms[6] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    //arParms[6].Value = objbill.EmployeeID;

                    //arParms[7] = new SqlParameter("@Output", SqlDbType.Int);
                    //arParms[7].Direction = ParameterDirection.Output;

                    //arParms[8] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                    //arParms[8].Value = objbill.IPaddress;



                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Save_LabOutsource", arParms);
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

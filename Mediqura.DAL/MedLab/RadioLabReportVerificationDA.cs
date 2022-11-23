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
   public class RadioLabReportVerificationDA
    {
       public List<RadioLabReportVerificationData> GetPatientList(RadioLabReportVerificationData objpatient)
       {
           List<RadioLabReportVerificationData> result = null;
           try
           {
               {
                   SqlParameter[] arParms = new SqlParameter[11];

                   arParms[0] = new SqlParameter("@patienttype", SqlDbType.Int);
                   arParms[0].Value = objpatient.PatientType;

                   arParms[1] = new SqlParameter("@UHID", SqlDbType.BigInt);
                   arParms[1].Value = objpatient.UHID;

                   arParms[2] = new SqlParameter("@LabSubGroupType", SqlDbType.Int);
                   arParms[2].Value = objpatient.LabSubGrpID;

                   arParms[3] = new SqlParameter("@patientName", SqlDbType.VarChar);
                   arParms[3].Value = objpatient.PatientName;

                   arParms[4] = new SqlParameter("@TestStatus", SqlDbType.Int);
                   arParms[4].Value = objpatient.TestStatus;

                   arParms[5] = new SqlParameter("@TestID", SqlDbType.VarChar);
                   arParms[5].Value = objpatient.TestID;

                   arParms[6] = new SqlParameter("@LabTestID", SqlDbType.BigInt);
                   arParms[6].Value = objpatient.LabTestID;

                   arParms[7] = new SqlParameter("@LabGroupType", SqlDbType.Int);
                   arParms[7].Value = objpatient.LabGrpID;

                   arParms[8] = new SqlParameter("@pageno", SqlDbType.Int);
                   arParms[8].Value = objpatient.CurrentIndex;

                   arParms[9] = new SqlParameter("@DateFrom", SqlDbType.DateTime);
                   arParms[9].Value = objpatient.DateFrom;

                   arParms[10] = new SqlParameter("@DateTo", SqlDbType.DateTime);
                   arParms[10].Value = objpatient.DateTo;


                    SqlDataReader sqlReader = null;
                   sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_CMS_GetRadioTestPatient_list", arParms);
                   List<RadioLabReportVerificationData> lstpatientData = ORHelper<RadioLabReportVerificationData>.FromDataReaderToList(sqlReader);
                   result = lstpatientData;
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
       public List<RadioLabReportVerificationData> GetRadioReportTemplateByTestId(RadioLabReportVerificationData objRadioReportMaster)
       {
           List<RadioLabReportVerificationData> result = null;
           try
           {
               {
                   SqlParameter[] arParms = new SqlParameter[1];

                   arParms[0] = new SqlParameter("@ID", SqlDbType.BigInt);
                   arParms[0].Value = objRadioReportMaster.LabID;

                   SqlDataReader sqlReader = null;
                   sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GET_RadioReport_for_patient_BY_ID", arParms);
                   List<RadioLabReportVerificationData> ListTemplateData = ORHelper<RadioLabReportVerificationData>.FromDataReaderToList(sqlReader);
                   result = ListTemplateData;
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
       public int UpdateReportTemplateVerification(RadioLabReportVerificationData objRadioReportMaster)
       {
           int result = 0;
           try
           {
               {
                   SqlParameter[] arParms = new SqlParameter[5];

                   arParms[0] = new SqlParameter("@ID", SqlDbType.BigInt);
                   arParms[0].Value = objRadioReportMaster.ID;

                   arParms[1] = new SqlParameter("@verifionType", SqlDbType.Int);
                   arParms[1].Value = 0;

                   arParms[2] = new SqlParameter("@Output", SqlDbType.SmallInt);
                   arParms[2].Direction = ParameterDirection.Output;

                   arParms[3] = new SqlParameter("@BillID", SqlDbType.BigInt);
                   arParms[3].Value = objRadioReportMaster.billID;

                   arParms[4] = new SqlParameter("@VerifyBy", SqlDbType.BigInt);
                   arParms[4].Value = objRadioReportMaster.VerifyBy;
                   
                   int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_UPDATE_RadioReport_for_patient", arParms);
                   if (result_ > 0 || result_ == -1)
                       result = Convert.ToInt32(arParms[2].Value);

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

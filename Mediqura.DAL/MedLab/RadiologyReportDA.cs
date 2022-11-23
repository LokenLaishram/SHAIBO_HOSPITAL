using Mediqura.CommonData;
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
   public class RadiologyReportDA
    {
       public List<RadiologyReportData> GetPatientList(RadiologyReportData objpatient)
       {
           List<RadiologyReportData> result = null;
           try
           {
               {
                   SqlParameter[] arParms = new SqlParameter[8];

                   arParms[0] = new SqlParameter("@DateTo", SqlDbType.DateTime);
                   arParms[0].Value = objpatient.ToDate;

                   arParms[1] = new SqlParameter("@patienttype", SqlDbType.Int);
                   arParms[1].Value = objpatient.PatientType;

                   arParms[2] = new SqlParameter("@UHID", SqlDbType.BigInt);
                   arParms[2].Value = objpatient.UHID;

                   arParms[3] = new SqlParameter("@LabSubGroupType", SqlDbType.Int);
                   arParms[3].Value = objpatient.LabSubGrpID;

                   arParms[4] = new SqlParameter("@patientName", SqlDbType.VarChar);
                   arParms[4].Value = objpatient.PatientName;

                   arParms[5] = new SqlParameter("@TestStatus", SqlDbType.Int);
                   arParms[5].Value = objpatient.TestStatus;

                   arParms[6] = new SqlParameter("@DateFrom", SqlDbType.DateTime);
                   arParms[6].Value = objpatient.FromDate;

                   arParms[7] = new SqlParameter("@LabGroupType", SqlDbType.Int);
                   arParms[7].Value = objpatient.LabGrpID;
                 
                    SqlDataReader sqlReader = null;
                   sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_CMS_GetPatientTest", arParms);
                   List<RadiologyReportData> lstpatientData = ORHelper<RadiologyReportData>.FromDataReaderToList(sqlReader);
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

       public int UpdateReportTemplate(RadiologyReportData objRadioReportMaster)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[12];

                    arParms[0] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[0].Value = objRadioReportMaster.HospitalID;

                    arParms[1] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[1].Value = objRadioReportMaster.UHID;

                    arParms[2] = new SqlParameter("@LabSubGroupID", SqlDbType.Int);
                    arParms[2].Value = objRadioReportMaster.LabSubGrpID;

                    arParms[3] = new SqlParameter("@TestID", SqlDbType.BigInt);
                    arParms[3].Value = objRadioReportMaster.TestID;

                    arParms[4] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[4].Direction = ParameterDirection.Output;

                    arParms[5] = new SqlParameter("@LabTestID", SqlDbType.BigInt);
                    arParms[5].Value = objRadioReportMaster.LabTestID;

                    arParms[6] = new SqlParameter("@FinancialID", SqlDbType.Int);
                    arParms[6].Value = objRadioReportMaster.FinancialYearID;

                    arParms[7] = new SqlParameter("@Template", SqlDbType.VarChar);
                    arParms[7].Value = objRadioReportMaster.Template;

                    arParms[8] = new SqlParameter("@ActionType", SqlDbType.Int);
                    arParms[8].Value = objRadioReportMaster.ActionType;

                    arParms[9] = new SqlParameter("@AddedBy", SqlDbType.Int);
                    arParms[9].Value = objRadioReportMaster.EmployeeID;

                    arParms[10] = new SqlParameter("@PatientType", SqlDbType.Int);
                    arParms[10].Value = objRadioReportMaster.PatientType;

                    arParms[11] = new SqlParameter("@HeaderType", SqlDbType.Int);
                    arParms[11].Value = objRadioReportMaster.HeaderID;

       

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Update_patient_radio_report", arParms);
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
       public List<RadiologyReportData> GetRadioReportTemplateByTestId(RadiologyReportData objRadioReportMaster)
       {
           List<RadiologyReportData> result = null;
           try
           {
               {
                   SqlParameter[] arParms = new SqlParameter[9];

                   arParms[0] = new SqlParameter("@LabSubGroupID", SqlDbType.Int);
                   arParms[0].Value = objRadioReportMaster.LabSubGrpID;

                   arParms[1] = new SqlParameter("@TestID", SqlDbType.Int);
                   arParms[1].Value = objRadioReportMaster.TestID;

                   arParms[2] = new SqlParameter("@ActionType", SqlDbType.Int);
                   arParms[2].Value = objRadioReportMaster.ActionType;

                   arParms[3] = new SqlParameter("@UHID", SqlDbType.BigInt);
                   arParms[3].Value = objRadioReportMaster.UHID;

                   arParms[4] = new SqlParameter("@PatientType", SqlDbType.Int);
                   arParms[4].Value = objRadioReportMaster.PatientType;

                   arParms[5] = new SqlParameter("@ID", SqlDbType.BigInt);
                   arParms[5].Value = objRadioReportMaster.ID;

                   arParms[6] = new SqlParameter("@LabTestID", SqlDbType.BigInt);
                   arParms[6].Value = objRadioReportMaster.LabTestID;

                   arParms[7] = new SqlParameter("@TestStatus", SqlDbType.Int);
                   arParms[7].Value = objRadioReportMaster.TestStatus;

                   arParms[8] = new SqlParameter("@LabGroupType", SqlDbType.Int);
                   arParms[8].Value = objRadioReportMaster.LabGrpID;
                    SqlDataReader sqlReader = null;
                   sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GET_RadioReport_for_patient", arParms);
                   List<RadiologyReportData> ListTemplateData = ORHelper<RadiologyReportData>.FromDataReaderToList(sqlReader);
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
       public List<RadiologyReportData> GetHeaderTemplateByID(RadiologyReportData objRadioReportMaster)
       {
           List<RadiologyReportData> result = null;
           try
           {
               {
                   SqlParameter[] arParms = new SqlParameter[2];
    
                   arParms[0] = new SqlParameter("@LabGroupType", SqlDbType.Int);
                   arParms[0].Value = objRadioReportMaster.LabGrpID;

                   arParms[1] = new SqlParameter("@HeaderType", SqlDbType.Int);
                   arParms[1].Value = objRadioReportMaster.HeaderID;
                   SqlDataReader sqlReader = null;
                   sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GET_RadioHeaderTemplateDetailsByID", arParms);
                   List<RadiologyReportData> ListTemplateData = ORHelper<RadiologyReportData>.FromDataReaderToList(sqlReader);
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
   
    }
}

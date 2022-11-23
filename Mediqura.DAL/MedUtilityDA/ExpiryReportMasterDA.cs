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
     public class ExpiryReportMasterDA
    {
         public int UpdateExpiryReport(ExpiryReportMasterData objRadioReportMaster)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[4];


                    arParms[0] = new SqlParameter("@Template", SqlDbType.VarChar);
                    arParms[0].Value = objRadioReportMaster.Template;

                    arParms[1] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[1].Value = objRadioReportMaster.EmployeeID;

                    arParms[2] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[2].Direction = ParameterDirection.Output;

                    arParms[3] = new SqlParameter("@ActionType", SqlDbType.Int);
                    arParms[3].Value = objRadioReportMaster.ID;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Update_ExpiryReport", arParms);
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
         public int DeleteExpiryReport(ExpiryReportMasterData objbill)
         {
             int result = 0;
             try
             {
                 {
                     SqlParameter[] arParms = new SqlParameter[6];

                     arParms[0] = new SqlParameter("@ID", SqlDbType.BigInt);
                     arParms[0].Value = objbill.ID;

                     arParms[1] = new SqlParameter("@Output", SqlDbType.SmallInt);
                     arParms[1].Direction = ParameterDirection.Output;

                     arParms[2] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                     arParms[2].Value = objbill.Remarks;

                     arParms[3] = new SqlParameter("@HospitalID", SqlDbType.Int);
                     arParms[3].Value = objbill.HospitalID;
              
                     arParms[4] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                     arParms[4].Value = objbill.IPaddress;

                     arParms[5] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                     arParms[5].Value = objbill.EmployeeID;

                     int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_DeleteExpiryReport", arParms);
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
   
         public int UpdateExpiryReportMaker(ExpiryReportMasterData objRadioReportMaster)
         {
             int result = 0;
             try
             {
                 {
                     SqlParameter[] arParms = new SqlParameter[9];


                     arParms[0] = new SqlParameter("@Template", SqlDbType.VarChar);
                     arParms[0].Value = objRadioReportMaster.Template;

                     arParms[1] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                     arParms[1].Value = objRadioReportMaster.EmployeeID;

                     arParms[2] = new SqlParameter("@Output", SqlDbType.SmallInt);
                     arParms[2].Direction = ParameterDirection.Output;

                     arParms[3] = new SqlParameter("@ActionType", SqlDbType.Int);
                     arParms[3].Value = objRadioReportMaster.ActionType;

                     arParms[4] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                     arParms[4].Value = objRadioReportMaster.IPNo;

                     arParms[5] = new SqlParameter("@MannerID", SqlDbType.Int);
                     arParms[5].Value = objRadioReportMaster.MannerID;

                     arParms[6] = new SqlParameter("@EmergencyNo", SqlDbType.VarChar);
                     arParms[6].Value = objRadioReportMaster.EmergencyNo;

                     arParms[7] = new SqlParameter("@PatientType", SqlDbType.Int);
                     arParms[7].Value = objRadioReportMaster.PatientType;

                     arParms[8] = new SqlParameter("@ID", SqlDbType.BigInt);
                     arParms[8].Value = objRadioReportMaster.ID;

                   

                     int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Update_ExpiryReportMaker", arParms);
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
   
         public List<ExpiryReportMasterData> GetPatientDetailsByID(ExpiryReportMasterData objRadioReportMaster)
         {
             List<ExpiryReportMasterData> result = null;
             try
             {
                 {
                     SqlParameter[] arParms = new SqlParameter[3];
                     
                     arParms[0] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                     arParms[0].Value = objRadioReportMaster.IPNo;

                     arParms[1] = new SqlParameter("@PatientType", SqlDbType.Int);
                     arParms[1].Value = objRadioReportMaster.PatientType;

                     arParms[2] = new SqlParameter("@EmerNo", SqlDbType.VarChar);
                     arParms[2].Value = objRadioReportMaster.EmergencyNo;



                     SqlDataReader sqlReader = null;
                     sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GET_ExpiryReport_for_patient", arParms);
                     List<ExpiryReportMasterData> ListTemplateData = ORHelper<ExpiryReportMasterData>.FromDataReaderToList(sqlReader);
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
  
         public List<ExpiryReportMasterData> GetExpiryTemplateByID(ExpiryReportMasterData objRadioReportMaster)
        {
            List<ExpiryReportMasterData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[0];

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GET_ExpiryReport", arParms);
                    List<ExpiryReportMasterData> ListTemplateData = ORHelper<ExpiryReportMasterData>.FromDataReaderToList(sqlReader);
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

         public List<ExpiryReportMasterData> GetExpiryReportList(ExpiryReportMasterData objRadioReportMaster)
         {
             List<ExpiryReportMasterData> result = null;
             try
             {
                 {
                     SqlParameter[] arParms = new SqlParameter[5];

                     arParms[0] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                     arParms[0].Value = objRadioReportMaster.IPNo;

                     arParms[1] = new SqlParameter("@PatientType", SqlDbType.Int);
                     arParms[1].Value = objRadioReportMaster.PatientType;

                     arParms[2] = new SqlParameter("@EmerNo", SqlDbType.VarChar);
                     arParms[2].Value = objRadioReportMaster.EmergencyNo;


                     arParms[3] = new SqlParameter("@DateFrom", SqlDbType.DateTime);
                     arParms[3].Value = objRadioReportMaster.DateFrom;

                     arParms[4] = new SqlParameter("@DateTo", SqlDbType.DateTime);
                     arParms[4].Value = objRadioReportMaster.DateTo;

                     SqlDataReader sqlReader = null;
                     sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GET_ExpiryReportList", arParms);
                     List<ExpiryReportMasterData> ListTemplateData = ORHelper<ExpiryReportMasterData>.FromDataReaderToList(sqlReader);
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

         public List<ExpiryReportMasterData> GetExpiryReportDetails(ExpiryReportMasterData objRadioReportMaster)
         {
             List<ExpiryReportMasterData> result = null;
             try
             {
                 {
                     SqlParameter[] arParms = new SqlParameter[1];

                     arParms[0] = new SqlParameter("@ID", SqlDbType.BigInt);
                     arParms[0].Value = objRadioReportMaster.ID;

                     SqlDataReader sqlReader = null;
                     sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GET_ExpiryReportPreview", arParms);
                     List<ExpiryReportMasterData> ListTemplateData = ORHelper<ExpiryReportMasterData>.FromDataReaderToList(sqlReader);
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

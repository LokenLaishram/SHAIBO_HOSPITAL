using Mediqura.CommonData.OTData;
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
namespace Mediqura.DAL.OTDA
{
    public class PatientConsentTypeDA
    {
        public int UpdateConsentTypeDetails(PatientConsentTypeData objOTRolesMaster)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[9];

                    arParms[0] = new SqlParameter("@TypeID", SqlDbType.Int);
                    arParms[0].Value = objOTRolesMaster.TypeID;

                    arParms[1] = new SqlParameter("@TypeCode", SqlDbType.VarChar);
                    arParms[1].Value = objOTRolesMaster.TypeCode;

                    arParms[2] = new SqlParameter("@Typedescp", SqlDbType.VarChar);
                    arParms[2].Value = objOTRolesMaster.Typedescp;

                    arParms[3] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[3].Value = objOTRolesMaster.EmployeeID;

                    arParms[4] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[4].Value = objOTRolesMaster.HospitalID;

                    arParms[5] = new SqlParameter("@ActionType", SqlDbType.Int);
                    arParms[5].Value = objOTRolesMaster.ActionType;

                    arParms[6] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[6].Direction = ParameterDirection.Output;

                    arParms[7] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[7].Value = objOTRolesMaster.IsActive;

                    arParms[8] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                    arParms[8].Value = objOTRolesMaster.IPaddress;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_UpdateConsentTypeMST", arParms);
                    if (result_ > 0 || result_ == -1)
                        result = Convert.ToInt32(arParms[6].Value);
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
        public List<PatientConsentTypeData> SearchConsentType(PatientConsentTypeData objOTRolesMaster)
        {
            List<PatientConsentTypeData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[3];

                    arParms[0] = new SqlParameter("@Code", SqlDbType.VarChar);
                    arParms[0].Value = objOTRolesMaster.TypeCode;

                    arParms[1] = new SqlParameter("@Descriptions", SqlDbType.VarChar);
                    arParms[1].Value = objOTRolesMaster.Typedescp;

                    arParms[2] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[2].Value = objOTRolesMaster.IsActive;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_ConsentType", arParms);
                    List<PatientConsentTypeData> lstOTRolesDetails = ORHelper<PatientConsentTypeData>.FromDataReaderToList(sqlReader);
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
        public List<PatientConsentTypeData> GetEmployeeTypeDetailsByID(PatientConsentTypeData objOTRolesMaster)
        {
            List<PatientConsentTypeData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@TypeID", SqlDbType.Int);
                    arParms[0].Value = objOTRolesMaster.TypeID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_GetConsentTypeByID", arParms);
                    List<PatientConsentTypeData> lstLabUnitDetails = ORHelper<PatientConsentTypeData>.FromDataReaderToList(sqlReader);
                    result = lstLabUnitDetails;
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
        public int DeleteConsentTypeDetailsByID(PatientConsentTypeData objOTRolesMaster)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[5];

                    arParms[0] = new SqlParameter("@TypeID", SqlDbType.Int);
                    arParms[0].Value = objOTRolesMaster.TypeID;

                    arParms[1] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[1].Value = objOTRolesMaster.EmployeeID;

                    arParms[2] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[2].Direction = ParameterDirection.Output;


                    arParms[3] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    arParms[3].Value = objOTRolesMaster.Remarks;

                    arParms[4] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                    arParms[4].Value = objOTRolesMaster.IPaddress;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_DeleteConsentTypeByID", arParms);
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
        public List<PatientConsentData> GetConsentTemplateByID(PatientConsentData objRadioReportMaster)
        {
            List<PatientConsentData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@TypeID", SqlDbType.Int);
                    arParms[0].Value = objRadioReportMaster.TypeID;

                    //arParms[1] = new SqlParameter("@TypeFeatureID", SqlDbType.Int);
                    //arParms[1].Value = objRadioReportMaster.TypeFeatureID;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GET_ConsentReportMST", arParms);
                    List<PatientConsentData> ListTemplateData = ORHelper<PatientConsentData>.FromDataReaderToList(sqlReader);
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
        public int UpdateConsentReport(PatientConsentData objRadioReportMaster)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[5];


                    arParms[0] = new SqlParameter("@TypeID", SqlDbType.Int);
                    arParms[0].Value = objRadioReportMaster.TypeID;

                    arParms[1] = new SqlParameter("@Template", SqlDbType.VarChar);
                    arParms[1].Value = objRadioReportMaster.Template;

                    arParms[2] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[2].Value = objRadioReportMaster.EmployeeID;

                    arParms[3] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[3].Direction = ParameterDirection.Output;

                    arParms[4] = new SqlParameter("@ActionType", SqlDbType.Int);
                    arParms[4].Value = objRadioReportMaster.ID;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Update_ConsentReportMST", arParms);
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
        public List<PatientConsentData> SearchConsentTemplateByIPNO(PatientConsentData objD)
        {
            List<PatientConsentData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                    arParms[0].Value = objD.IPNo;

                    //arParms[1] = new SqlParameter("@TypeID", SqlDbType.Int);
                    //arParms[1].Value = objD.TypeID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Search_ConsentByIpNo", arParms);
                    List<PatientConsentData> ListTemplateData = ORHelper<PatientConsentData>.FromDataReaderToList(sqlReader);
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
        public int UpdatePatientConsent(PatientConsentData objRadioReportMaster)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[10];

                    arParms[0] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                    arParms[0].Value = objRadioReportMaster.IPNo;

                    arParms[1] = new SqlParameter("@TypeID", SqlDbType.Int);
                    arParms[1].Value = objRadioReportMaster.TypeID;

                    arParms[2] = new SqlParameter("@RelativeName", SqlDbType.VarChar);
                    arParms[2].Value = objRadioReportMaster.RelativeName;

                    arParms[3] = new SqlParameter("@RelationID", SqlDbType.Int);
                    arParms[3].Value = objRadioReportMaster.RelationID;

                    arParms[4] = new SqlParameter("@Template", SqlDbType.VarChar);
                    arParms[4].Value = objRadioReportMaster.Template;

                    arParms[5] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[5].Value = objRadioReportMaster.HospitalID;

                    arParms[6] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[6].Direction = ParameterDirection.Output;

                    arParms[7] = new SqlParameter("@FinancialID", SqlDbType.Int);
                    arParms[7].Value = objRadioReportMaster.FinancialYearID;


                    arParms[8] = new SqlParameter("@ActionType", SqlDbType.Int);
                    arParms[8].Value = objRadioReportMaster.ActionType;

                    arParms[9] = new SqlParameter("@EmployeeID", SqlDbType.Int);
                    arParms[9].Value = objRadioReportMaster.EmployeeID;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Update_PatientConsent", arParms);
                    if (result_ > 0 || result_ == -1)
                        result = Convert.ToInt32(arParms[6].Value);

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
        public List<PatientConsentData> GetConsentTemplateByIPNO(PatientConsentData objD)
        {
            List<PatientConsentData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                    arParms[0].Value = objD.IPNo;

                    arParms[1] = new SqlParameter("@TypeID", SqlDbType.Int);
                    arParms[1].Value = objD.TypeID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GET_ConsentByIpNo", arParms);
                    List<PatientConsentData> ListTemplateData = ORHelper<PatientConsentData>.FromDataReaderToList(sqlReader);
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

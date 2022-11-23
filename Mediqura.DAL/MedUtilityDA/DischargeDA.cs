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
    public class DischargeDA
    {
        public int UpdateDischargeDetails(DischargeData objDischargeMaster)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[9];

                    arParms[0] = new SqlParameter("@DischargeTypeID", SqlDbType.Int);
                    arParms[0].Value = objDischargeMaster.DischargeTypeID;

                    arParms[1] = new SqlParameter("@DischargeTypeCode", SqlDbType.VarChar);
                    arParms[1].Value = objDischargeMaster.DischargeTypeCode;

                    arParms[2] = new SqlParameter("@DischargeTypedescp", SqlDbType.VarChar);
                    arParms[2].Value = objDischargeMaster.DischargeTypedescp;

                    arParms[3] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[3].Value = objDischargeMaster.EmployeeID;

                    arParms[4] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[4].Value = objDischargeMaster.HospitalID;

                    arParms[5] = new SqlParameter("@ActionType", SqlDbType.Int);
                    arParms[5].Value = objDischargeMaster.ActionType;

                    arParms[6] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[6].Direction = ParameterDirection.Output;

                    arParms[7] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[7].Value = objDischargeMaster.IsActive;

                    arParms[8] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                    arParms[8].Value = objDischargeMaster.IPaddress;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_UpdateDiscgargetypeMST", arParms);
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
        public List<DischargeData> SearchDischargeDetails(DischargeData objDischargeMaster)
        {
            List<DischargeData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[3];

                    arParms[0] = new SqlParameter("@Code", SqlDbType.VarChar);
                    arParms[0].Value = objDischargeMaster.DischargeTypeCode;

                    arParms[1] = new SqlParameter("@Descriptions", SqlDbType.VarChar);
                    arParms[1].Value = objDischargeMaster.DischargeTypedescp;

                    arParms[2] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[2].Value = objDischargeMaster.IsActive;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_SearchDischarge", arParms);
                    List<DischargeData> lstDischargeDetails = ORHelper<DischargeData>.FromDataReaderToList(sqlReader);
                    result = lstDischargeDetails;
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
        public List<DischargeData> GetDischargeDetailsByID(DischargeData objDischargeMaster)
        {
            List<DischargeData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@DischargeTypeID", SqlDbType.Int);
                    arParms[0].Value = objDischargeMaster.DischargeTypeID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_GetDischargeatypeDetailsByID", arParms);
                    List<DischargeData> lstLabUnitDetails = ORHelper<DischargeData>.FromDataReaderToList(sqlReader);
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
        public int DeleteDischargeDetailsByID(DischargeData objDischargeMaster)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[5];

                    arParms[0] = new SqlParameter("@DischargeTypeID", SqlDbType.Int);
                    arParms[0].Value = objDischargeMaster.DischargeTypeID;

                    arParms[1] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[1].Value = objDischargeMaster.EmployeeID;

                    arParms[2] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[2].Direction = ParameterDirection.Output;


                    arParms[3] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    arParms[3].Value = objDischargeMaster.Remarks;

                    arParms[4] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                    arParms[4].Value = objDischargeMaster.IPaddress;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_DeleteDistypeDetailsByID", arParms);
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
        public int UpdateDishaegeReport(DischargeData objRadioReportMaster)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[6];


                    arParms[0] = new SqlParameter("@DischargeTypeID", SqlDbType.Int);
                    arParms[0].Value = objRadioReportMaster.DischargeTypeID;

                    arParms[1] = new SqlParameter("@TypeFeatureID", SqlDbType.Int);
                    arParms[1].Value = objRadioReportMaster.TypeFeatureID;

                    arParms[2] = new SqlParameter("@Template", SqlDbType.VarChar);
                    arParms[2].Value = objRadioReportMaster.Template;

                    arParms[3] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[3].Value = objRadioReportMaster.EmployeeID;

                    arParms[4] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[4].Direction = ParameterDirection.Output;

                    arParms[5] = new SqlParameter("@ActionType", SqlDbType.Int);
                    arParms[5].Value = objRadioReportMaster.ID;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Update_DischargeTypeReport", arParms);
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
        public List<DischargeData> GetRadioTemplateByID(DischargeData objRadioReportMaster)
        {
            List<DischargeData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@DischargeTypeID", SqlDbType.Int);
                    arParms[0].Value = objRadioReportMaster.DischargeTypeID;

                    arParms[1] = new SqlParameter("@TypeFeatureID", SqlDbType.Int);
                    arParms[1].Value = objRadioReportMaster.TypeFeatureID;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GET_DischargeReport", arParms);
                    List<DischargeData> ListTemplateData = ORHelper<DischargeData>.FromDataReaderToList(sqlReader);
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
        public List<DischargeData> GetDischargeTemplate(DischargeData objData)
        {
            List<DischargeData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@Ipno", SqlDbType.VarChar);
                    arParms[0].Value = objData.IPNo;

                    //arParms[1] = new SqlParameter("@TypeFeatureID", SqlDbType.Int);
                    //arParms[1].Value = objRadioReportMaster.TypeFeatureID;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GET_DischargeView", arParms);
                    List<DischargeData> ListTemplateData = ORHelper<DischargeData>.FromDataReaderToList(sqlReader);
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
        //public List<DischargeData> GetPatientList(DischargeData objpatient)
        //{
        //    List<DischargeData> result = null;
        //    try
        //    {
        //        {
        //            SqlParameter[] arParms = new SqlParameter[6];

        //            arParms[0] = new SqlParameter("@DischargeTypeID", SqlDbType.Int);
        //            arParms[0].Value = objpatient.DischargeTypeID;

        //            arParms[1] = new SqlParameter("@IPNo", SqlDbType.VarChar);
        //            arParms[1].Value = objpatient.IPNo;

        //            SqlDataReader sqlReader = null;
        //            sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GET_DischargeSummary", arParms);
        //            List<DischargeData> lstpatientData = ORHelper<DischargeData>.FromDataReaderToList(sqlReader);
        //            result = lstpatientData;
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
        public List<DischargeData> GetFnalBillList(DischargeData objpatient)
        {
            List<DischargeData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[3];

                    arParms[0] = new SqlParameter("@DateFrom", SqlDbType.DateTime);
                    arParms[0].Value = objpatient.DateFrom;

                    arParms[1] = new SqlParameter("@DateTo", SqlDbType.DateTime);
                    arParms[1].Value = objpatient.DateTo;

                    arParms[2] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                    arParms[2].Value = objpatient.IPNo;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GET_DischargeReady", arParms);
                    List<DischargeData> lstpatientData = ORHelper<DischargeData>.FromDataReaderToList(sqlReader);
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
        public List<DischargeData> GetDischargeTemplateByID(DischargeData objRadioReportMaster)
        {
            List<DischargeData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@DischargeTypeID", SqlDbType.Int);
                    arParms[0].Value = objRadioReportMaster.DischargeTypeID;

                    arParms[1] = new SqlParameter("@TypeFeatureID", SqlDbType.Int);
                    arParms[1].Value = objRadioReportMaster.TypeFeatureID;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GET_DischargeReport", arParms);
                    List<DischargeData> ListTemplateData = ORHelper<DischargeData>.FromDataReaderToList(sqlReader);
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
        public int UpdateSummaryReport(DischargeData objRadioReportMaster)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[8];

                    arParms[0] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                    arParms[0].Value = objRadioReportMaster.IPNo;

                    arParms[1] = new SqlParameter("@DischargeTypeID", SqlDbType.Int);
                    arParms[1].Value = objRadioReportMaster.DischargeTypeID;

                    arParms[2] = new SqlParameter("@Template", SqlDbType.VarChar);
                    arParms[2].Value = objRadioReportMaster.Template;

                    arParms[3] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[3].Value = objRadioReportMaster.HospitalID;

                    arParms[4] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[4].Direction = ParameterDirection.Output;

                    arParms[5] = new SqlParameter("@FinancialID", SqlDbType.Int);
                    arParms[5].Value = objRadioReportMaster.FinancialYearID;


                    arParms[6] = new SqlParameter("@ActionType", SqlDbType.Int);
                    arParms[6].Value = objRadioReportMaster.ActionType;

                    arParms[7] = new SqlParameter("@EmployeeID", SqlDbType.Int);
                    arParms[7].Value = objRadioReportMaster.EmployeeID;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Update_DischargeSummary", arParms);
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
        public List<DischargeData> GetDischargeTemplateByIPNO(DischargeData objD)
        {
            List<DischargeData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                    arParms[0].Value = objD.IPNo;

                    arParms[1] = new SqlParameter("@DischargeTypeID", SqlDbType.Int);
                    arParms[1].Value = objD.DischargeTypeID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GET_DischargeReportByIpNo", arParms);
                    List<DischargeData> ListTemplateData = ORHelper<DischargeData>.FromDataReaderToList(sqlReader);
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
        public List<DischargeData> GetSummaryList(DischargeData objpatient)
        {
            List<DischargeData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[4];

                    arParms[0] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                    arParms[0].Value = objpatient.IPNo;

                    arParms[1] = new SqlParameter("@DischargeTypeID", SqlDbType.Int);
                    arParms[1].Value = objpatient.DischargeTypeID;


                    arParms[2] = new SqlParameter("@DateFrom", SqlDbType.DateTime);
                    arParms[2].Value = objpatient.DateFrom;

                    arParms[3] = new SqlParameter("@DateTo", SqlDbType.DateTime);
                    arParms[3].Value = objpatient.DateTo;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GET_DischargeSummaryList", arParms);
                    List<DischargeData> lstpatientData = ORHelper<DischargeData>.FromDataReaderToList(sqlReader);
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
        public List<DischargeData> getIPNoDishList(DischargeData objD)
        {
            List<DischargeData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                    arParms[0].Value = objD.IPNo;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_IPNoforDisReport", arParms);
                    List<DischargeData> lstresult = ORHelper<DischargeData>.FromDataReaderToList(sqlReader);
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
        public List<DischargeData> getIPNoDish(DischargeData objD)
        {
            List<DischargeData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                    arParms[0].Value = objD.IPNo;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_IPNoIntimation", arParms);
                    List<DischargeData> lstresult = ORHelper<DischargeData>.FromDataReaderToList(sqlReader);
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
        public List<DischargeData> GetDischargeList(DischargeData objpatient)
        {
            List<DischargeData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[6];

                    arParms[0] = new SqlParameter("@WardID", SqlDbType.Int);
                    arParms[0].Value = objpatient.WardID;

                    arParms[1] = new SqlParameter("@DischargeTypeID", SqlDbType.Int);
                    arParms[1].Value = objpatient.DischargeTypeID;
                    
                    arParms[2] = new SqlParameter("@DateFrom", SqlDbType.DateTime);
                    arParms[2].Value = objpatient.DateFrom;

                    arParms[3] = new SqlParameter("@DateTo", SqlDbType.DateTime);
                    arParms[3].Value = objpatient.DateTo;

                    arParms[4] = new SqlParameter("@DischargeDocID", SqlDbType.BigInt);
                    arParms[4].Value = objpatient.DischargeDocID;

                    arParms[5] = new SqlParameter("@PatientCategory", SqlDbType.Int);
                    arParms[5].Value = objpatient.PatientCategory;

                   
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GET_DischargeList", arParms);
                    List<DischargeData> lstpatientData = ORHelper<DischargeData>.FromDataReaderToList(sqlReader);
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
        public List<DischargeData> GetDischargeReadyList(DischargeData objpatient)
        {
            List<DischargeData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[4];

                    arParms[0] = new SqlParameter("@WardID", SqlDbType.Int);
                    arParms[0].Value = objpatient.WardID;

                    arParms[1] = new SqlParameter("@DateFrom", SqlDbType.DateTime);
                    arParms[1].Value = objpatient.DateFrom;

                    arParms[2] = new SqlParameter("@DateTo", SqlDbType.DateTime);
                    arParms[2].Value = objpatient.DateTo;

                    arParms[3] = new SqlParameter("@DischargeDocID", SqlDbType.BigInt);
                    arParms[3].Value = objpatient.DischargeDocID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GET_DischrgReadyList", arParms);
                    List<DischargeData> lstpatientData = ORHelper<DischargeData>.FromDataReaderToList(sqlReader);
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


    }
}

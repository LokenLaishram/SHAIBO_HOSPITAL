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
    public class InvDashboardDA
    {
        public List<InvDashboardMasterData> GetInvestigationDetails(InvDashboardMasterData objpatient)
        {
            List<InvDashboardMasterData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[8];

                    arParms[0] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[0].Value = objpatient.UHID;

                    arParms[1] = new SqlParameter("@PatientName", SqlDbType.VarChar);
                    arParms[1].Value = objpatient.PatientName;

                    arParms[2] = new SqlParameter("@TestID", SqlDbType.BigInt);
                    arParms[2].Value = objpatient.TestID;

                    arParms[3] = new SqlParameter("@DateFrom", SqlDbType.DateTime);
                    arParms[3].Value = objpatient.DateFrom;

                    arParms[4] = new SqlParameter("@DateTo", SqlDbType.DateTime);
                    arParms[4].Value = objpatient.DateTo;

                    arParms[5] = new SqlParameter("@InvNo", SqlDbType.VarChar);
                    arParms[5].Value = objpatient.InvNo;

                    arParms[6] = new SqlParameter("@PatientType", SqlDbType.Int);
                    arParms[6].Value = objpatient.PatientType;
                    
                    arParms[7] = new SqlParameter("@TestCenter", SqlDbType.Int);
                    arParms[7].Value = objpatient.TestCenterID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_CMS_Get_LabInvestigationDetails", arParms);
                    List<InvDashboardMasterData> lstpatientData = ORHelper<InvDashboardMasterData>.FromDataReaderToList(sqlReader);
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
        public List<InvDashboardMasterData> GetInvestigationReport(InvDashboardMasterData objDesignationTypeMaster)
        {
            List<InvDashboardMasterData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                    arParms[0].Value = objDesignationTypeMaster.ID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetInvestigationReportDetails", arParms);
                    List<InvDashboardMasterData> lstDesignationTypeDetails = ORHelper<InvDashboardMasterData>.FromDataReaderToList(sqlReader);
                    result = lstDesignationTypeDetails;
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
        public List<InvDashboardMasterData> GetInvNoWithContext(InvDashboardMasterData objD)
        {
            List<InvDashboardMasterData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@InvNo", SqlDbType.VarChar);
                    arParms[0].Value = objD.InvNo;

                    arParms[1] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[1].Value = objD.UHID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_InvWithContext", arParms);
                    List<InvDashboardMasterData> lstresult = ORHelper<InvDashboardMasterData>.FromDataReaderToList(sqlReader);
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
        public List<InvDashboardMasterData> GetInvNo(InvDashboardMasterData objD)
        {
            List<InvDashboardMasterData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@InvNo", SqlDbType.VarChar);
                    arParms[0].Value = objD.InvNo;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetLabInvNo", arParms);
                    List<InvDashboardMasterData> lstresult = ORHelper<InvDashboardMasterData>.FromDataReaderToList(sqlReader);
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
        public List<InvDashboardMasterData> getIPNoNemrgNo(InvDashboardMasterData objD)
        {
            List<InvDashboardMasterData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                    arParms[0].Value = objD.IPNo;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_IPNoEmrgNoInv", arParms);
                    List<InvDashboardMasterData> lstresult = ORHelper<InvDashboardMasterData>.FromDataReaderToList(sqlReader);
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

        public List<InvTatData> GetInvestigationTat(InvTatData objData)
        {
            List<InvTatData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[6];

                    arParms[0] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[0].Value = objData.UHID;

                    arParms[1] = new SqlParameter("@PatientName", SqlDbType.VarChar);
                    arParms[1].Value = objData.PatientName;

                    arParms[2] = new SqlParameter("@TestID", SqlDbType.BigInt);
                    arParms[2].Value = objData.TestID;

                    arParms[3] = new SqlParameter("@DateFrom", SqlDbType.DateTime);
                    arParms[3].Value = objData.DateFrom;

                    arParms[4] = new SqlParameter("@DateTo", SqlDbType.DateTime);
                    arParms[4].Value = objData.DateTo;

                    arParms[5] = new SqlParameter("@InvNo", SqlDbType.VarChar);
                    arParms[5].Value = objData.InvNumber;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_CMS_Get_LabInvTatCalculation", arParms);
                    List<InvTatData> lstpatientData = ORHelper<InvTatData>.FromDataReaderToList(sqlReader);
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
        public List<InvDashboardMasterData> GetRadioDetails(InvDashboardMasterData objpatient)
        {
            List<InvDashboardMasterData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[8];

                    arParms[0] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[0].Value = objpatient.UHID;

                    arParms[1] = new SqlParameter("@PatientName", SqlDbType.VarChar);
                    arParms[1].Value = objpatient.PatientName;

                    arParms[2] = new SqlParameter("@TestID", SqlDbType.BigInt);
                    arParms[2].Value = objpatient.TestID;

                    arParms[3] = new SqlParameter("@DateFrom", SqlDbType.DateTime);
                    arParms[3].Value = objpatient.DateFrom;

                    arParms[4] = new SqlParameter("@DateTo", SqlDbType.DateTime);
                    arParms[4].Value = objpatient.DateTo;

                    arParms[5] = new SqlParameter("@InvNo", SqlDbType.VarChar);
                    arParms[5].Value = objpatient.InvNo;

                    arParms[6] = new SqlParameter("@PatientType", SqlDbType.Int);
                    arParms[6].Value = objpatient.PatientType;

                    arParms[7] = new SqlParameter("@TestCenter", SqlDbType.Int);
                    arParms[7].Value = objpatient.TestCenterID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_CMS_Get_RadioInvestigationDetails", arParms);
                    List<InvDashboardMasterData> lstpatientData = ORHelper<InvDashboardMasterData>.FromDataReaderToList(sqlReader);
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
        public List<InvDashboardMasterData> GetEndosDetails(InvDashboardMasterData objpatient)
        {
            List<InvDashboardMasterData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[8];

                    arParms[0] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[0].Value = objpatient.UHID;

                    arParms[1] = new SqlParameter("@PatientName", SqlDbType.VarChar);
                    arParms[1].Value = objpatient.PatientName;

                    arParms[2] = new SqlParameter("@TestID", SqlDbType.BigInt);
                    arParms[2].Value = objpatient.TestID;

                    arParms[3] = new SqlParameter("@DateFrom", SqlDbType.DateTime);
                    arParms[3].Value = objpatient.DateFrom;

                    arParms[4] = new SqlParameter("@DateTo", SqlDbType.DateTime);
                    arParms[4].Value = objpatient.DateTo;

                    arParms[5] = new SqlParameter("@InvNo", SqlDbType.VarChar);
                    arParms[5].Value = objpatient.InvNo;

                    arParms[6] = new SqlParameter("@PatientType", SqlDbType.Int);
                    arParms[6].Value = objpatient.PatientType;

                    arParms[7] = new SqlParameter("@TestCenter", SqlDbType.Int);
                    arParms[7].Value = objpatient.TestCenterID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_CMS_Get_EndosInvestigationDetails", arParms);
                    List<InvDashboardMasterData> lstpatientData = ORHelper<InvDashboardMasterData>.FromDataReaderToList(sqlReader);
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

using Mediqura.CommonData.MedEmergencyData;
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
namespace Mediqura.DAL.MedEmergencyDA
{
    public class EmrgAdmissionDA
    {
        public List<EmrgAdmissionData> UpdateEmrgAdmissionDetails(EmrgAdmissionData objadm)
        {
            List<EmrgAdmissionData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[21];

                    arParms[0] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[0].Value = objadm.UHID;

                    arParms[1] = new SqlParameter("@DocID", SqlDbType.BigInt);
                    arParms[1].Value = objadm.DocID;

                    arParms[2] = new SqlParameter("@AdmissionCharge", SqlDbType.Money);
                    arParms[2].Value = objadm.TotalAdmissionCharge;

                    arParms[3] = new SqlParameter("@Case", SqlDbType.VarChar);
                    arParms[3].Value = objadm.Cases;

                    arParms[4] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[4].Value = objadm.HospitalID;

                    arParms[5] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[5].Value = objadm.EmployeeID;

                    arParms[6] = new SqlParameter("@ActionType", SqlDbType.SmallInt);
                    arParms[6].Value = objadm.ActionType;

                    arParms[7] = new SqlParameter("@PaymentMode", SqlDbType.Int);
                    arParms[7].Value = objadm.PaymentMode;

                    arParms[8] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[8].Value = objadm.FinancialYearID;

                    arParms[9] = new SqlParameter("@BlockID", SqlDbType.Int);
                    arParms[9].Value = objadm.BlockID;

                    arParms[10] = new SqlParameter("@FloorID", SqlDbType.Int);
                    arParms[10].Value = objadm.FloorID;

                    arParms[11] = new SqlParameter("@WardID", SqlDbType.Int);
                    arParms[11].Value = objadm.WardID;

                    arParms[12] = new SqlParameter("@XMLData", SqlDbType.Xml);
                    arParms[12].Value = objadm.XMLData;

                    arParms[13] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                    arParms[13].Value = objadm.IPaddress;

                    arParms[14] = new SqlParameter("@ID", SqlDbType.Int);
                    arParms[14].Value = objadm.ID;

                    arParms[15] = new SqlParameter("@OPnumber", SqlDbType.VarChar);
                    arParms[15].Value = objadm.OPnumber;

                    arParms[16] = new SqlParameter("@DeptID", SqlDbType.Int);
                    arParms[16].Value = objadm.DeptID;

                    arParms[17] = new SqlParameter("@Informeward", SqlDbType.Int);
                    arParms[17].Value = objadm.Informeward;

                    arParms[18] = new SqlParameter("@Informehk", SqlDbType.Int);
                    arParms[18].Value = objadm.Informehk;

                    arParms[19] = new SqlParameter("@ReferalID", SqlDbType.BigInt);
                    arParms[19].Value = objadm.ReferalID;

                    arParms[20] = new SqlParameter("@SourceID", SqlDbType.Int);
                    arParms[20].Value = objadm.SourceID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Update_EmergencyAdmissionDetails", arParms);
                    List<EmrgAdmissionData> listpatientdetails = ORHelper<EmrgAdmissionData>.FromDataReaderToList(sqlReader);
                    result = listpatientdetails;

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
        public List<EmrgAdmissionData> GetEMRGNo(EmrgAdmissionData objD)
        {
            List<EmrgAdmissionData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[0].Value = objD.UHID;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetEMRGNo", arParms);
                    List<EmrgAdmissionData> lstresult = ORHelper<EmrgAdmissionData>.FromDataReaderToList(sqlReader);
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
        public List<EmrgAdmissionData> GetEmrgList(EmrgAdmissionData objbill)
        {
            List<EmrgAdmissionData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[7];

                    arParms[0] = new SqlParameter("@EmrgNo", SqlDbType.NVarChar);
                    arParms[0].Value = objbill.EmrgNo;

                    arParms[1] = new SqlParameter("@PatientName", SqlDbType.NVarChar);
                    arParms[1].Value = objbill.PatientName;

                    arParms[2] = new SqlParameter("@DateFrom", SqlDbType.DateTime);
                    arParms[2].Value = objbill.DateFrom;

                    arParms[3] = new SqlParameter("@DateTo", SqlDbType.DateTime);
                    arParms[3].Value = objbill.DateTo;

                    arParms[4] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[4].Value = objbill.IsActive;

                    arParms[5] = new SqlParameter("@DocID", SqlDbType.Int);
                    arParms[5].Value = objbill.DocID;

                    arParms[6] = new SqlParameter("@DischargeStatus", SqlDbType.Int);
                    arParms[6].Value = objbill.DischargeStatus;

                    //arParms[7] = new SqlParameter("@userID", SqlDbType.Int);
                    //arParms[7].Value = objbill.userID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetEmergencyList", arParms);
                    List<EmrgAdmissionData> listpatientdetails = ORHelper<EmrgAdmissionData>.FromDataReaderToList(sqlReader);
                    result = listpatientdetails;
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
        public List<EmrgAdmissionData> GetEmrgNo(EmrgAdmissionData objD)
        {
            List<EmrgAdmissionData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@EmrgNo", SqlDbType.NVarChar);
                    arParms[0].Value = objD.EmrgNo;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_EmergNO", arParms);
                    List<EmrgAdmissionData> lstresult = ORHelper<EmrgAdmissionData>.FromDataReaderToList(sqlReader);
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
        public List<EmrgAdmissionData> GetPhrEmrgNo(EmrgAdmissionData objD)
        {
            List<EmrgAdmissionData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@EmrgNo", SqlDbType.NVarChar);
                    arParms[0].Value = objD.EmrgNo;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_PhrEmergNO", arParms);
                    List<EmrgAdmissionData> lstresult = ORHelper<EmrgAdmissionData>.FromDataReaderToList(sqlReader);
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
        public List<EmrgAdmissionData> GetFEmrgNo(EmrgAdmissionData objD)
        {
            List<EmrgAdmissionData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@EmrgNo", SqlDbType.VarChar);
                    arParms[0].Value = objD.EmrgNo;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_FEmergNO", arParms);
                    List<EmrgAdmissionData> lstresult = ORHelper<EmrgAdmissionData>.FromDataReaderToList(sqlReader);
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
        public List<EmrgAdmissionData> Getemergnecyservicenumber(EmrgAdmissionData objD)
        {
            List<EmrgAdmissionData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@ServiceNumber", SqlDbType.VarChar);
                    arParms[0].Value = objD.ServiceNumber;

                    arParms[1] = new SqlParameter("@EmergencyNo", SqlDbType.VarChar);
                    arParms[1].Value = objD.EmergencyNo;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Servicenumber", arParms);
                    List<EmrgAdmissionData> lstresult = ORHelper<EmrgAdmissionData>.FromDataReaderToList(sqlReader);
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
        public List<EmrgAdmissionData> EmergencyInvnumber(EmrgAdmissionData objD)
        {
            List<EmrgAdmissionData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@InvNumber", SqlDbType.VarChar);
                    arParms[0].Value = objD.InvNumber;

                    arParms[1] = new SqlParameter("@EmergencyNo", SqlDbType.VarChar);
                    arParms[1].Value = objD.EmergencyNo;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_EmergInvnumber", arParms);
                    List<EmrgAdmissionData> lstresult = ORHelper<EmrgAdmissionData>.FromDataReaderToList(sqlReader);
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
        public List<EmrgAdmissionData> GetEmrgDetailsByID(EmrgAdmissionData objEmrgData)
        {
            List<EmrgAdmissionData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                    arParms[0].Value = objEmrgData.ID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_GetEmergencyDetailsByID", arParms);
                    List<EmrgAdmissionData> lstPatientTypeDetails = ORHelper<EmrgAdmissionData>.FromDataReaderToList(sqlReader);
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
        public List<EmrgAdmissionData> DeleteEmrgDetailsByID(EmrgAdmissionData objEmrgData)
        {
            List<EmrgAdmissionData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[9];

                    arParms[0] = new SqlParameter("@EmrgNo", SqlDbType.NVarChar);
                    arParms[0].Value = objEmrgData.EmrgNo;

                    arParms[1] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[1].Direction = ParameterDirection.Output;

                    arParms[2] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    arParms[2].Value = objEmrgData.Remarks;

                    arParms[3] = new SqlParameter("@UserId", SqlDbType.BigInt);
                    arParms[3].Value = objEmrgData.EmployeeID;

                    arParms[4] = new SqlParameter("@Charges", SqlDbType.Money);
                    arParms[4].Value = objEmrgData.AdmissionCharge;

                    arParms[5] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[5].Value = objEmrgData.UHID;

                    arParms[6] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[6].Value = objEmrgData.HospitalID;

                    arParms[7] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                    arParms[7].Value = objEmrgData.IPaddress;

                    arParms[8] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[8].Value = objEmrgData.EmployeeID;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_DeleteEmrgDetailsbyID", arParms);
                    List<EmrgAdmissionData> lstPatientTypeDetails = ORHelper<EmrgAdmissionData>.FromDataReaderToList(sqlReader);
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
        public List<EmrgAdmissionData> GetPatientsDetailsByEmrgNo(EmrgAdmissionData objD)
        {
            List<EmrgAdmissionData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@EmrgNo", SqlDbType.NVarChar);
                    arParms[0].Value = objD.EmrgNo;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetPatientDetailsByEmrgNo", arParms);
                    List<EmrgAdmissionData> lstresult = ORHelper<EmrgAdmissionData>.FromDataReaderToList(sqlReader);
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
        public List<EmrgAdmissionData> GetemrgfinalbillDetail(EmrgAdmissionData objD)
        {
            List<EmrgAdmissionData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[5];

                    arParms[0] = new SqlParameter("@EmrgNo", SqlDbType.VarChar);
                    arParms[0].Value = objD.EmrgNo;

                    arParms[1] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[1].Value = objD.FinancialYearID;

                    arParms[2] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[2].Value = objD.HospitalID;

                    arParms[3] = new SqlParameter("@EmrgID", SqlDbType.BigInt);
                    arParms[3].Value = objD.EmrgID;

                    arParms[4] = new SqlParameter("@AmountEnable", SqlDbType.Int);
                    arParms[4].Value = objD.AmountEnable;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Get_emergency_finalbill_Details", arParms);
                    List<EmrgAdmissionData> lstresult = ORHelper<EmrgAdmissionData>.FromDataReaderToList(sqlReader);
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
        public List<PHREmrgFinalData> Get_PHR_emrgfinalBilldetails(PHREmrgFinalData objD)
        {
            List<PHREmrgFinalData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[3];

                    arParms[0] = new SqlParameter("@EmrgNo", SqlDbType.VarChar);
                    arParms[0].Value = objD.EmrgNo;

                    arParms[1] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[1].Value = objD.FinancialYearID;

                    arParms[2] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[2].Value = objD.HospitalID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Get_PHR_emergency_finalbill_Details", arParms);
                    List<PHREmrgFinalData> lstresult = ORHelper<PHREmrgFinalData>.FromDataReaderToList(sqlReader);
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
        public List<PHRIPFinalData> Get_PHR_IPfinalBilldetails(PHRIPFinalData objD)
        {
            List<PHRIPFinalData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[3];

                    arParms[0] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                    arParms[0].Value = objD.IPNo;

                    arParms[1] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[1].Value = objD.FinancialYearID;

                    arParms[2] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[2].Value = objD.HospitalID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Get_PHR_IP_finalbill_Details", arParms);
                    List<PHRIPFinalData> lstresult = ORHelper<PHRIPFinalData>.FromDataReaderToList(sqlReader);
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
        public List<EmrgAdmissionData> GetEmrgServices(EmrgAdmissionData objservice)
        {
            List<EmrgAdmissionData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[3];

                    arParms[0] = new SqlParameter("@ServiceName", SqlDbType.VarChar);
                    arParms[0].Value = objservice.ServiceName;

                    arParms[1] = new SqlParameter("@ServiceTypeID", SqlDbType.Int);
                    arParms[1].Value = objservice.ServiceTypeID;

                    arParms[2] = new SqlParameter("@DoctorID", SqlDbType.BigInt);
                    arParms[2].Value = objservice.DocID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Get_EMRG_Services", arParms);
                    List<EmrgAdmissionData> lstServiceDetails = ORHelper<EmrgAdmissionData>.FromDataReaderToList(sqlReader);
                    result = lstServiceDetails;
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
        public List<EmrgAdmissionData> GetEmrgPHRServices(EmrgAdmissionData objservice)
        {
            List<EmrgAdmissionData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@ItemName", SqlDbType.VarChar);
                    arParms[0].Value = objservice.ServiceName;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetEMRGPhrServices", arParms);
                    List<EmrgAdmissionData> lstServiceDetails = ORHelper<EmrgAdmissionData>.FromDataReaderToList(sqlReader);
                    result = lstServiceDetails;
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
        public List<EmrgAdmissionData> GetServiceChargeByID(EmrgAdmissionData objbill)
        {
            List<EmrgAdmissionData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[4];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.BigInt);
                    arParms[0].Value = objbill.ID;

                    arParms[1] = new SqlParameter("@ServiceCategoryID", SqlDbType.Int);
                    arParms[1].Value = objbill.ServiceCategoryID;

                    arParms[2] = new SqlParameter("@DoctorID", SqlDbType.BigInt);
                    arParms[2].Value = objbill.DocID;

                    arParms[3] = new SqlParameter("@EmergencyNo", SqlDbType.VarChar);
                    arParms[3].Value = objbill.EmergencyNo;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Get_Emerg_Service_ChargeList", arParms);
                    List<EmrgAdmissionData> chargelist = ORHelper<EmrgAdmissionData>.FromDataReaderToList(sqlReader);
                    result = chargelist;
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
        public List<EmrgAdmissionData> UpdateEMRGServiceRecord(EmrgAdmissionData objbill)
        {
            List<EmrgAdmissionData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[8];

                    arParms[0] = new SqlParameter("@XMLData", SqlDbType.Xml);
                    arParms[0].Value = objbill.XMLData;

                    arParms[1] = new SqlParameter("@EmrgNo", SqlDbType.VarChar);
                    arParms[1].Value = objbill.EmrgNo;

                    arParms[2] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[2].Value = objbill.HospitalID;

                    arParms[3] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[3].Value = objbill.FinancialYearID;

                    arParms[4] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[4].Value = objbill.EmployeeID;

                    arParms[5] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                    arParms[5].Value = objbill.IPaddress;

                    arParms[6] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[6].Value = objbill.UHID;

                    arParms[7] = new SqlParameter("@ServiceCategory", SqlDbType.Int);
                    arParms[7].Value = objbill.ServiceCategoryID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Update_EMRGServiceRecordList", arParms);
                    List<EmrgAdmissionData> chargelist = ORHelper<EmrgAdmissionData>.FromDataReaderToList(sqlReader);
                    result = chargelist;
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
        public int UpdateEMRGServiceRecordPHR(EmrgAdmissionData objbill)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[8];

                    arParms[0] = new SqlParameter("@XMLData", SqlDbType.Xml);
                    arParms[0].Value = objbill.XMLData;

                    arParms[1] = new SqlParameter("@EmrgNo", SqlDbType.NVarChar);
                    arParms[1].Value = objbill.EmrgNo;

                    arParms[2] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[2].Value = objbill.HospitalID;

                    arParms[3] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[3].Value = objbill.FinancialYearID;

                    arParms[4] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[4].Value = objbill.EmployeeID;

                    arParms[5] = new SqlParameter("@Output", SqlDbType.Int);
                    arParms[5].Direction = ParameterDirection.Output;

                    //arParms[6] = new SqlParameter("@RefferalDoctorID", SqlDbType.BigInt);
                    //arParms[6].Value = objbill.RefferaDocID;

                    arParms[6] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                    arParms[6].Value = objbill.IPaddress;

                    arParms[7] = new SqlParameter("@ServiceTypeID", SqlDbType.Int);
                    arParms[7].Value = objbill.ServiceTypeID;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Update_EMRGServiceRecordListPHR", arParms);
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
        public List<EmrgAdmissionData> GetEMRGServiceList(EmrgAdmissionData objbill)
        {
            List<EmrgAdmissionData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[9];

                    arParms[0] = new SqlParameter("@EmrgNo", SqlDbType.VarChar);
                    arParms[0].Value = objbill.EmrgNo;

                    arParms[1] = new SqlParameter("@PatientName", SqlDbType.VarChar);
                    arParms[1].Value = objbill.PatientName;

                    arParms[2] = new SqlParameter("@DateFrom", SqlDbType.DateTime);
                    arParms[2].Value = objbill.DateFrom;

                    arParms[3] = new SqlParameter("@DateTo", SqlDbType.DateTime);
                    arParms[3].Value = objbill.DateTo;

                    arParms[4] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[4].Value = objbill.IsActive;

                    arParms[5] = new SqlParameter("@ServiceCategoryID", SqlDbType.Int);
                    arParms[5].Value = objbill.ServiceCategoryID;

                    arParms[6] = new SqlParameter("@ServiceNumber", SqlDbType.VarChar);
                    arParms[6].Value = objbill.ServiceNumber;

                    arParms[7] = new SqlParameter("@InvNumber", SqlDbType.VarChar);
                    arParms[7].Value = objbill.InvNumber;

                    arParms[8] = new SqlParameter("@AmountEnable", SqlDbType.Int);
                    arParms[8].Value = objbill.AmountEnable;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetEMRGServiceRecordList", arParms);
                    List<EmrgAdmissionData> listpatientdetails = ORHelper<EmrgAdmissionData>.FromDataReaderToList(sqlReader);
                    result = listpatientdetails;
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
        public List<EmrgAdmissionData> GetEMRGDrugList(EmrgAdmissionData objbill)
        {
            List<EmrgAdmissionData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[5];

                    arParms[0] = new SqlParameter("@EmrgNo", SqlDbType.NVarChar);
                    arParms[0].Value = objbill.EmrgNo;

                    arParms[1] = new SqlParameter("@PatientName", SqlDbType.VarChar);
                    arParms[1].Value = objbill.PatientName;

                    arParms[2] = new SqlParameter("@DateFrom", SqlDbType.DateTime);
                    arParms[2].Value = objbill.DateFrom;

                    arParms[3] = new SqlParameter("@DateTo", SqlDbType.DateTime);
                    arParms[3].Value = objbill.DateTo;

                    arParms[4] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[4].Value = objbill.IsActive;

                    //arParms[5] = new SqlParameter("@ServiceTypeID", SqlDbType.Int);
                    //arParms[5].Value = objbill.ServiceTypeID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetEMRGDrugServiceList", arParms);
                    List<EmrgAdmissionData> listpatientdetails = ORHelper<EmrgAdmissionData>.FromDataReaderToList(sqlReader);
                    result = listpatientdetails;
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
        public int DeleteEMGServiceRecordByEMRGNo(EmrgAdmissionData objbill)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[12];

                    arParms[0] = new SqlParameter("@EmrgNo", SqlDbType.NVarChar);
                    arParms[0].Value = objbill.EmrgNo;

                    arParms[1] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[1].Direction = ParameterDirection.Output;

                    arParms[2] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    arParms[2].Value = objbill.Remarks;

                    arParms[3] = new SqlParameter("@UserId", SqlDbType.BigInt);
                    arParms[3].Value = objbill.EmployeeID;

                    arParms[4] = new SqlParameter("@Charges", SqlDbType.Money);
                    arParms[4].Value = objbill.ServiceCharge;

                    arParms[5] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[5].Value = objbill.UHID;

                    arParms[6] = new SqlParameter("@HospitalID", SqlDbType.BigInt);
                    arParms[6].Value = objbill.HospitalID;

                    arParms[7] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                    arParms[7].Value = objbill.IPaddress;

                    arParms[8] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[8].Value = objbill.EmployeeID;

                    arParms[9] = new SqlParameter("@ID", SqlDbType.BigInt);
                    arParms[9].Value = objbill.ID;

                    arParms[10] = new SqlParameter("@InvNumber", SqlDbType.VarChar);
                    arParms[10].Value = objbill.InvNumber;

                    arParms[11] = new SqlParameter("@ServiceNumber", SqlDbType.VarChar);
                    arParms[11].Value = objbill.ServiceNumber;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_DeleteEMGServiceRecordbyEMRGNo", arParms);
                    if (result_ > 0 || result_ == -1)
                    {
                        result = Convert.ToInt32(arParms[1].Value);
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
        public int Deleteemrgfinalbill(EmrgAdmissionData objbill)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[8];

                    arParms[0] = new SqlParameter("@EmrgNo", SqlDbType.VarChar);
                    arParms[0].Value = objbill.EmrgNo;

                    arParms[1] = new SqlParameter("@BillNo", SqlDbType.VarChar);
                    arParms[1].Value = objbill.BillNo;

                    arParms[2] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    arParms[2].Value = objbill.Remarks;

                    arParms[3] = new SqlParameter("@HospitalID", SqlDbType.BigInt);
                    arParms[3].Value = objbill.HospitalID;

                    arParms[4] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                    arParms[4].Value = objbill.IPaddress;

                    arParms[5] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[5].Value = objbill.EmployeeID;

                    arParms[6] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[6].Value = objbill.UHID;

                    arParms[7] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[7].Direction = ParameterDirection.Output;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Delete_Emrg_FinalBill", arParms);
                    if (result_ > 0 || result_ == -1)
                    {
                        result = Convert.ToInt32(arParms[7].Value);
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
        public int DeletePHRemrgfinalbill(PHREmrgFinalData objbill)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[8];

                    arParms[0] = new SqlParameter("@EmrgNo", SqlDbType.VarChar);
                    arParms[0].Value = objbill.EmrgNo;

                    arParms[1] = new SqlParameter("@BillNo", SqlDbType.VarChar);
                    arParms[1].Value = objbill.BillNo;

                    arParms[2] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    arParms[2].Value = objbill.Remarks;

                    arParms[3] = new SqlParameter("@HospitalID", SqlDbType.BigInt);
                    arParms[3].Value = objbill.HospitalID;

                    arParms[4] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                    arParms[4].Value = objbill.IPaddress;

                    arParms[5] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[5].Value = objbill.EmployeeID;

                    arParms[6] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[6].Value = objbill.UHID;

                    arParms[7] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[7].Direction = ParameterDirection.Output;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Delete_Phr_Emrg_FinalBill", arParms);
                    if (result_ > 0 || result_ == -1)
                    {
                        result = Convert.ToInt32(arParms[7].Value);
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
        public int DeletePHRipfinalbill(PHRIPFinalData objbill)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[8];

                    arParms[0] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                    arParms[0].Value = objbill.IPNo;

                    arParms[1] = new SqlParameter("@BillNo", SqlDbType.VarChar);
                    arParms[1].Value = objbill.BillNo;

                    arParms[2] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    arParms[2].Value = objbill.Remarks;

                    arParms[3] = new SqlParameter("@HospitalID", SqlDbType.BigInt);
                    arParms[3].Value = objbill.HospitalID;

                    arParms[4] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                    arParms[4].Value = objbill.IPaddress;

                    arParms[5] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[5].Value = objbill.EmployeeID;

                    arParms[6] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[6].Value = objbill.UHID;

                    arParms[7] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[7].Direction = ParameterDirection.Output;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Delete_Phr_IP_FinalBill", arParms);
                    if (result_ > 0 || result_ == -1)
                    {
                        result = Convert.ToInt32(arParms[7].Value);
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
        public int DeleteEMGDrugRecordByEMRGNo(EmrgAdmissionData objbill)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[10];

                    arParms[0] = new SqlParameter("@EmrgNo", SqlDbType.NVarChar);
                    arParms[0].Value = objbill.EmrgNo;

                    arParms[1] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[1].Direction = ParameterDirection.Output;

                    arParms[2] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    arParms[2].Value = objbill.Remarks;

                    arParms[3] = new SqlParameter("@UserId", SqlDbType.BigInt);
                    arParms[3].Value = objbill.EmployeeID;

                    arParms[4] = new SqlParameter("@Charges", SqlDbType.Money);
                    arParms[4].Value = objbill.ServiceCharge;

                    arParms[5] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[5].Value = objbill.UHID;

                    arParms[6] = new SqlParameter("@HospitalID", SqlDbType.BigInt);
                    arParms[6].Value = objbill.HospitalID;

                    arParms[7] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                    arParms[7].Value = objbill.IPaddress;

                    arParms[8] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[8].Value = objbill.EmployeeID;

                    arParms[9] = new SqlParameter("@ID", SqlDbType.BigInt);
                    arParms[9].Value = objbill.ID;


                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_DeleteEMGDrugRecordbyEMRGNo", arParms);
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
        public int DeleteEmrgRegnDetailsByID(EmrgAdmissionData objbill)
        {
            int result = 0;
            try
            {
                {

                    SqlParameter[] arParms = new SqlParameter[10];

                    arParms[0] = new SqlParameter("@EmrgNo", SqlDbType.NVarChar);
                    arParms[0].Value = objbill.EmrgNo;

                    arParms[1] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[1].Direction = ParameterDirection.Output;

                    arParms[2] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    arParms[2].Value = objbill.Remarks;

                    arParms[3] = new SqlParameter("@UserId", SqlDbType.BigInt);
                    arParms[3].Value = objbill.EmployeeID;

                    arParms[4] = new SqlParameter("@Charges", SqlDbType.Money);
                    arParms[4].Value = objbill.AdmissionCharge;

                    arParms[5] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[5].Value = objbill.UHID;

                    arParms[6] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[6].Value = objbill.HospitalID;

                    arParms[7] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                    arParms[7].Value = objbill.IPaddress;

                    arParms[8] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[8].Value = objbill.EmployeeID;

                    arParms[9] = new SqlParameter("@BillNo", SqlDbType.VarChar);
                    arParms[9].Value = objbill.BillNo;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_DeleteEMRGAdmissionbyID", arParms);
                    if (result_ > 0 || result_ == -1)
                    {
                        result = Convert.ToInt32(arParms[1].Value);
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
        public List<EmrgAdmissionData> SearchEMRGSampleCollectionDetails(EmrgAdmissionData objSampleCollectionData)
        {
            List<EmrgAdmissionData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];


                    arParms[0] = new SqlParameter("@EmrgNo", SqlDbType.NVarChar);
                    arParms[0].Value = objSampleCollectionData.EmrgNo;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_SearchEMRGLabSampleCollectionGrid", arParms);
                    List<EmrgAdmissionData> lstDetails = ORHelper<EmrgAdmissionData>.FromDataReaderToList(sqlReader);
                    result = lstDetails;
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
        public List<EmrgAdmissionData> GetSampleDescription(EmrgAdmissionData objD)
        {
            List<EmrgAdmissionData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@Samplecode", SqlDbType.VarChar);
                    arParms[0].Value = objD.Samplecode;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_CMS_GetSampleTypeDesc", arParms);
                    List<EmrgAdmissionData> lstresult = ORHelper<EmrgAdmissionData>.FromDataReaderToList(sqlReader);
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
        public int UpdateEmrgSampleCollectionDetails(EmrgAdmissionData objSampleCollectionDataData)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[10];

                    arParms[0] = new SqlParameter("@SampleID", SqlDbType.BigInt);
                    arParms[0].Value = objSampleCollectionDataData.ID;

                    arParms[1] = new SqlParameter("@XMLData", SqlDbType.Xml);
                    arParms[1].Value = objSampleCollectionDataData.XMLData;

                    arParms[2] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[2].Value = objSampleCollectionDataData.EmployeeID;

                    arParms[3] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[3].Value = objSampleCollectionDataData.HospitalID;

                    arParms[4] = new SqlParameter("@ActionType", SqlDbType.Int);
                    arParms[4].Value = objSampleCollectionDataData.ActionType;

                    arParms[5] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[5].Direction = ParameterDirection.Output;

                    arParms[6] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                    arParms[6].Value = objSampleCollectionDataData.IPaddress;

                    arParms[7] = new SqlParameter("@TakenBy", SqlDbType.VarChar);
                    arParms[7].Value = objSampleCollectionDataData.TakenBy;

                    arParms[8] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[8].Value = objSampleCollectionDataData.FinancialYearID;

                    arParms[9] = new SqlParameter("@EmrgNo", SqlDbType.NVarChar);
                    arParms[9].Value = objSampleCollectionDataData.EmrgNo;


                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_UpdateEmrgLabSampleCollection", arParms);
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
        public List<EmrgAdmissionData> GetEMRGPhrDrugList(EmrgAdmissionData objbill)
        {
            List<EmrgAdmissionData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@EmrgNo", SqlDbType.NVarChar);
                    arParms[0].Value = objbill.EmrgNo;

                    //arParms[1] = new SqlParameter("@PatientName", SqlDbType.VarChar);
                    //arParms[1].Value = objbill.PatientName;

                    //arParms[2] = new SqlParameter("@DateFrom", SqlDbType.DateTime);
                    //arParms[2].Value = objbill.DateFrom;

                    //arParms[3] = new SqlParameter("@DateTo", SqlDbType.DateTime);
                    //arParms[3].Value = objbill.DateTo;

                    //arParms[4] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    //arParms[4].Value = objbill.IsActive;

                    //arParms[5] = new SqlParameter("@ServiceTypeID", SqlDbType.Int);
                    //arParms[5].Value = objbill.ServiceTypeID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetEMRGDrugRecordList", arParms);
                    List<EmrgAdmissionData> listpatientdetails = ORHelper<EmrgAdmissionData>.FromDataReaderToList(sqlReader);
                    result = listpatientdetails;
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
        public List<EmrgAdmissionData> GetEMRGServices(EmrgAdmissionData objbill)
        {
            List<EmrgAdmissionData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@EmrgNo", SqlDbType.NVarChar);
                    arParms[0].Value = objbill.EmrgNo;

                    //arParms[1] = new SqlParameter("@PatientName", SqlDbType.VarChar);
                    //arParms[1].Value = objbill.PatientName;

                    //arParms[2] = new SqlParameter("@DateFrom", SqlDbType.DateTime);
                    //arParms[2].Value = objbill.DateFrom;

                    //arParms[3] = new SqlParameter("@DateTo", SqlDbType.DateTime);
                    //arParms[3].Value = objbill.DateTo;

                    //arParms[4] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    //arParms[4].Value = objbill.IsActive;

                    //arParms[5] = new SqlParameter("@ServiceTypeID", SqlDbType.Int);
                    //arParms[5].Value = objbill.ServiceTypeID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Get_EMRG_Bill_Details", arParms);
                    List<EmrgAdmissionData> listpatientdetails = ORHelper<EmrgAdmissionData>.FromDataReaderToList(sqlReader);
                    result = listpatientdetails;
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
        public List<EmrgAdmissionData> Get_EMRG_BilldetailsbyID(EmrgAdmissionData objD)
        {
            List<EmrgAdmissionData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];
                    arParms[0] = new SqlParameter("@EmrgNo", SqlDbType.NVarChar);
                    arParms[0].Value = objD.EmrgNo;

                    arParms[1] = new SqlParameter("@ServiceTypeID", SqlDbType.Int);
                    arParms[1].Value = objD.ServiceTypeID;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Get_EMRG_Bill_DetailsByservicetypeID", arParms);
                    List<EmrgAdmissionData> lstresult = ORHelper<EmrgAdmissionData>.FromDataReaderToList(sqlReader);
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
        public List<EmrgAdmissionData> Update_EMRGFinal_BillDetails(EmrgAdmissionData objbill)
        {
            List<EmrgAdmissionData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[20];

                    arParms[0] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[0].Value = objbill.UHID;

                    arParms[1] = new SqlParameter("@EmrgNo", SqlDbType.VarChar);
                    arParms[1].Value = objbill.EmrgNo;

                    arParms[2] = new SqlParameter("@TotalBillAmount", SqlDbType.Money);
                    arParms[2].Value = objbill.TotalBillAmount;

                    arParms[3] = new SqlParameter("@TotalPaidAmount", SqlDbType.Money);
                    arParms[3].Value = objbill.TotalPaidAmount;

                    arParms[4] = new SqlParameter("@TotalDiscount", SqlDbType.Money);
                    arParms[4].Value = objbill.TotalDiscount;

                    arParms[5] = new SqlParameter("@PaymentMode", SqlDbType.Int);
                    arParms[5].Value = objbill.PaymentMode;

                    arParms[6] = new SqlParameter("@BankName", SqlDbType.VarChar);
                    arParms[6].Value = objbill.BankName;

                    arParms[7] = new SqlParameter("@Chequenumber", SqlDbType.VarChar);
                    arParms[7].Value = objbill.Chequenumber;

                    arParms[8] = new SqlParameter("@Invoicenumber", SqlDbType.VarChar);
                    arParms[8].Value = objbill.Invoicenumber;

                    arParms[9] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[9].Value = objbill.HospitalID;

                    arParms[10] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[10].Value = objbill.FinancialYearID;

                    arParms[11] = new SqlParameter("@AddedBy", SqlDbType.BigInt);
                    arParms[11].Value = objbill.EmployeeID;

                    arParms[12] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    arParms[12].Value = objbill.Remarks;

                    arParms[13] = new SqlParameter("@BankID", SqlDbType.Int);
                    arParms[13].Value = objbill.BankID;

                    arParms[14] = new SqlParameter("@TotalDuemanount", SqlDbType.Money);
                    arParms[14].Value = objbill.TotalDuemanount;

                    arParms[15] = new SqlParameter("@XMLData", SqlDbType.Xml);
                    arParms[15].Value = objbill.XMLData;

                    arParms[16] = new SqlParameter("@AdjustedAmount", SqlDbType.Money);
                    arParms[16].Value = objbill.AdjustedAmount;

                    arParms[17] = new SqlParameter("@isExtraDiscount", SqlDbType.Int);
                    arParms[17].Value = objbill.isExtradiscount;

                    arParms[18] = new SqlParameter("@ExtraDiscountXML", SqlDbType.VarChar);
                    arParms[18].Value = objbill.extraDiscountXML;

                    arParms[19] = new SqlParameter("@TotalPayableAmount", SqlDbType.Money);
                    arParms[19].Value = objbill.TotalPayableAmount;


                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Save_EMRG_Final_Bills", arParms);
                    List<EmrgAdmissionData> lstresult = ORHelper<EmrgAdmissionData>.FromDataReaderToList(sqlReader);
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
        public List<PHREmrgFinalData> Update_PHR_EMRGFinal_BillDetails(PHREmrgFinalData objbill)
        {
            List<PHREmrgFinalData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[21];

                    arParms[0] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[0].Value = objbill.UHID;

                    arParms[1] = new SqlParameter("@EmrgNo", SqlDbType.VarChar);
                    arParms[1].Value = objbill.EmrgNo;

                    arParms[2] = new SqlParameter("@TotalBillAmount", SqlDbType.Money);
                    arParms[2].Value = objbill.TotalBillAmount;

                    arParms[3] = new SqlParameter("@TotalPaidAmount", SqlDbType.Money);
                    arParms[3].Value = objbill.TotalPaidAmount;

                    arParms[4] = new SqlParameter("@TotalDiscount", SqlDbType.Money);
                    arParms[4].Value = objbill.TotalDiscount;

                    arParms[5] = new SqlParameter("@PaymentMode", SqlDbType.Int);
                    arParms[5].Value = objbill.Paymode;

                    arParms[6] = new SqlParameter("@BankName", SqlDbType.VarChar);
                    arParms[6].Value = objbill.BankName;

                    arParms[7] = new SqlParameter("@Chequenumber", SqlDbType.VarChar);
                    arParms[7].Value = objbill.Chequenumber;

                    arParms[8] = new SqlParameter("@Invoicenumber", SqlDbType.VarChar);
                    arParms[8].Value = objbill.Invoicenumber;

                    arParms[9] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[9].Value = objbill.HospitalID;

                    arParms[10] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[10].Value = objbill.FinancialYearID;

                    arParms[11] = new SqlParameter("@AddedBy", SqlDbType.BigInt);
                    arParms[11].Value = objbill.EmployeeID;

                    arParms[12] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    arParms[12].Value = objbill.Remarks;

                    arParms[13] = new SqlParameter("@BankID", SqlDbType.Int);
                    arParms[13].Value = objbill.BankID;

                    arParms[14] = new SqlParameter("@TotalDuemanount", SqlDbType.Money);
                    arParms[14].Value = objbill.TotalDuemanount;

                    arParms[15] = new SqlParameter("@AdjustedAmount", SqlDbType.Money);
                    arParms[15].Value = objbill.AdjustedAmount;

                    arParms[16] = new SqlParameter("@TotalPayableAmount", SqlDbType.Money);
                    arParms[16].Value = objbill.TotalPayableAmount;

                    arParms[17] = new SqlParameter("@SubmitType", SqlDbType.Int);
                    arParms[17].Value = objbill.SubmitType;

                    arParms[18] = new SqlParameter("@PatientName", SqlDbType.VarChar);
                    arParms[18].Value = objbill.PatientName;

                    arParms[19] = new SqlParameter("@ReqNo", SqlDbType.VarChar);
                    arParms[19].Value = objbill.ReqNo;

                    arParms[20] = new SqlParameter("@ResponsiblePerson", SqlDbType.BigInt);
                    arParms[20].Value = objbill.ResponsiblePerson;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Save_PHR_EMRG_Final_Bill", arParms);
                    List<PHREmrgFinalData> lstresult = ORHelper<PHREmrgFinalData>.FromDataReaderToList(sqlReader);
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
        public List<PHRIPFinalData> Update_PHR_IPFinal_BillDetails(PHRIPFinalData objbill)
        {
            List<PHRIPFinalData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[21];

                    arParms[0] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[0].Value = objbill.UHID;

                    arParms[1] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                    arParms[1].Value = objbill.IPNo;

                    arParms[2] = new SqlParameter("@TotalBillAmount", SqlDbType.Money);
                    arParms[2].Value = objbill.TotalBillAmount;

                    arParms[3] = new SqlParameter("@TotalPaidAmount", SqlDbType.Money);
                    arParms[3].Value = objbill.TotalPaidAmount;

                    arParms[4] = new SqlParameter("@TotalDiscount", SqlDbType.Money);
                    arParms[4].Value = objbill.TotalDiscount;

                    arParms[5] = new SqlParameter("@PaymentMode", SqlDbType.Int);
                    arParms[5].Value = objbill.Paymode;

                    arParms[6] = new SqlParameter("@BankName", SqlDbType.VarChar);
                    arParms[6].Value = objbill.BankName;

                    arParms[7] = new SqlParameter("@Chequenumber", SqlDbType.VarChar);
                    arParms[7].Value = objbill.Chequenumber;

                    arParms[8] = new SqlParameter("@Invoicenumber", SqlDbType.VarChar);
                    arParms[8].Value = objbill.Invoicenumber;

                    arParms[9] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[9].Value = objbill.HospitalID;

                    arParms[10] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[10].Value = objbill.FinancialYearID;

                    arParms[11] = new SqlParameter("@AddedBy", SqlDbType.BigInt);
                    arParms[11].Value = objbill.EmployeeID;

                    arParms[12] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    arParms[12].Value = objbill.Remarks;

                    arParms[13] = new SqlParameter("@BankID", SqlDbType.Int);
                    arParms[13].Value = objbill.BankID;

                    arParms[14] = new SqlParameter("@TotalDuemanount", SqlDbType.Money);
                    arParms[14].Value = objbill.TotalDuemanount;

                    arParms[15] = new SqlParameter("@AdjustedAmount", SqlDbType.Money);
                    arParms[15].Value = objbill.AdjustedAmount;

                    arParms[16] = new SqlParameter("@TotalPayableAmount", SqlDbType.Money);
                    arParms[16].Value = objbill.TotalPayableAmount;

                    arParms[17] = new SqlParameter("@SubmitType", SqlDbType.Int);
                    arParms[17].Value = objbill.SubmitType;

                    arParms[18] = new SqlParameter("@PatientName", SqlDbType.VarChar);
                    arParms[18].Value = objbill.PatientName;

                    arParms[19] = new SqlParameter("@ReqNo", SqlDbType.VarChar);
                    arParms[19].Value = objbill.ReqNo;

                    arParms[20] = new SqlParameter("@ResponsiblePerson", SqlDbType.BigInt);
                    arParms[20].Value = objbill.ResponsiblePerson;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Save_PHR_IP_Final_Bill", arParms);
                    List<PHRIPFinalData> lstresult = ORHelper<PHRIPFinalData>.FromDataReaderToList(sqlReader);
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
        public List<EmrgAdmissionData> GetEMRGbillList(EmrgAdmissionData objD)
        {
            List<EmrgAdmissionData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[9];

                    arParms[0] = new SqlParameter("@EmrgNo", SqlDbType.NVarChar);
                    arParms[0].Value = objD.EmrgNo;

                    arParms[1] = new SqlParameter("@PatientName", SqlDbType.VarChar);
                    arParms[1].Value = objD.PatientName;

                    arParms[2] = new SqlParameter("@Paymode", SqlDbType.Int);
                    arParms[2].Value = objD.Paymode;

                    arParms[3] = new SqlParameter("@DateFrom", SqlDbType.DateTime);
                    arParms[3].Value = objD.DateFrom;

                    arParms[4] = new SqlParameter("@DateTo", SqlDbType.DateTime);
                    arParms[4].Value = objD.DateTo;

                    arParms[5] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[5].Value = objD.IsActive;

                    arParms[6] = new SqlParameter("@CollectedByID", SqlDbType.BigInt);
                    arParms[6].Value = objD.CollectedByID;

                    arParms[7] = new SqlParameter("@BillNo", SqlDbType.VarChar);
                    arParms[7].Value = objD.BillNo;

                    arParms[8] = new SqlParameter("@AmountEnable", SqlDbType.Int);
                    arParms[8].Value = objD.AmountEnable;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetEMRGFinalBillList", arParms);
                    List<EmrgAdmissionData> lstresult = ORHelper<EmrgAdmissionData>.FromDataReaderToList(sqlReader);
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
        public List<PHREmrgFinalData> GetPharamacyFinalBillList(PHREmrgFinalData objD)
        {
            List<PHREmrgFinalData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[9];

                    arParms[0] = new SqlParameter("@EmrgNo", SqlDbType.NVarChar);
                    arParms[0].Value = objD.EmrgNo;

                    arParms[1] = new SqlParameter("@PatientName", SqlDbType.VarChar);
                    arParms[1].Value = objD.PatientName;

                    arParms[2] = new SqlParameter("@Paymode", SqlDbType.Int);
                    arParms[2].Value = objD.Paymode;

                    arParms[3] = new SqlParameter("@DateFrom", SqlDbType.DateTime);
                    arParms[3].Value = objD.DateFrom;

                    arParms[4] = new SqlParameter("@DateTo", SqlDbType.DateTime);
                    arParms[4].Value = objD.DateTo;

                    arParms[5] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[5].Value = objD.IsActive;

                    arParms[6] = new SqlParameter("@CollectedByID", SqlDbType.BigInt);
                    arParms[6].Value = objD.CollectedByID;

                    arParms[7] = new SqlParameter("@BillNo", SqlDbType.VarChar);
                    arParms[7].Value = objD.BillNo;

                    arParms[8] = new SqlParameter("@AmountEnable", SqlDbType.Int);
                    arParms[8].Value = objD.AmountEnable;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetPharamacyFinalBillList", arParms);
                    List<PHREmrgFinalData> lstresult = ORHelper<PHREmrgFinalData>.FromDataReaderToList(sqlReader);
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
        public List<PHRIPFinalData> GetPharamacyIPFinalBillList(PHRIPFinalData objD)
        {
            List<PHRIPFinalData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[9];

                    arParms[0] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                    arParms[0].Value = objD.IPNo;

                    arParms[1] = new SqlParameter("@PatientName", SqlDbType.VarChar);
                    arParms[1].Value = objD.PatientName;

                    arParms[2] = new SqlParameter("@Paymode", SqlDbType.Int);
                    arParms[2].Value = objD.Paymode;

                    arParms[3] = new SqlParameter("@DateFrom", SqlDbType.DateTime);
                    arParms[3].Value = objD.DateFrom;

                    arParms[4] = new SqlParameter("@DateTo", SqlDbType.DateTime);
                    arParms[4].Value = objD.DateTo;

                    arParms[5] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[5].Value = objD.IsActive;

                    arParms[6] = new SqlParameter("@CollectedByID", SqlDbType.BigInt);
                    arParms[6].Value = objD.CollectedByID;

                    arParms[7] = new SqlParameter("@BillNo", SqlDbType.VarChar);
                    arParms[7].Value = objD.BillNo;

                    arParms[8] = new SqlParameter("@AmountEnable", SqlDbType.Int);
                    arParms[8].Value = objD.AmountEnable;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Get_IP_PharamacyFinalBillList", arParms);
                    List<PHRIPFinalData> lstresult = ORHelper<PHRIPFinalData>.FromDataReaderToList(sqlReader);
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
        public int Update_EMRGPhr_BillDetails(EmrgAdmissionData objbill)
        {
            int result = 0;
            try
            {
                {

                    SqlParameter[] arParms = new SqlParameter[14];

                    arParms[0] = new SqlParameter("@XMLData", SqlDbType.Xml);
                    arParms[0].Value = objbill.XMLData;

                    arParms[1] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[1].Value = objbill.UHID;

                    arParms[2] = new SqlParameter("@EmrgNo", SqlDbType.NVarChar);
                    arParms[2].Value = objbill.EmrgNo;

                    arParms[3] = new SqlParameter("@TotalBillAmount", SqlDbType.Money);
                    arParms[3].Value = objbill.TotalBillAmount;

                    arParms[4] = new SqlParameter("@TotalDiscount", SqlDbType.Money);
                    arParms[4].Value = objbill.TotalDiscount;

                    arParms[5] = new SqlParameter("@PaymentMode", SqlDbType.Int);
                    arParms[5].Value = objbill.PaymentMode;

                    arParms[6] = new SqlParameter("@BankName", SqlDbType.VarChar);
                    arParms[6].Value = objbill.BankName;

                    arParms[7] = new SqlParameter("@Chequenumber", SqlDbType.VarChar);
                    arParms[7].Value = objbill.Chequenumber;

                    arParms[8] = new SqlParameter("@CardNo", SqlDbType.VarChar);
                    arParms[8].Value = objbill.Invoicenumber;

                    arParms[9] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[9].Value = objbill.HospitalID;

                    arParms[10] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[10].Value = objbill.FinancialYearID;

                    arParms[11] = new SqlParameter("@AddedBy", SqlDbType.BigInt);
                    arParms[11].Value = objbill.EmployeeID;

                    arParms[12] = new SqlParameter("@Output", SqlDbType.BigInt);
                    arParms[12].Direction = ParameterDirection.Output;

                    arParms[13] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    arParms[13].Value = objbill.Remarks;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Save_EMRG_PHR_Bills", arParms);
                    if (result_ > 0 || result_ == -1)
                    {
                        result = Convert.ToInt32(arParms[12].Value);
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
        public List<EmrgAdmissionData> GetEMRGPhrbillList(EmrgAdmissionData objD)
        {
            List<EmrgAdmissionData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[8];

                    arParms[0] = new SqlParameter("@EmrgNo", SqlDbType.NVarChar);
                    arParms[0].Value = objD.EmrgNo;

                    arParms[1] = new SqlParameter("@PatientName", SqlDbType.VarChar);
                    arParms[1].Value = objD.PatientName;

                    arParms[2] = new SqlParameter("@Paymode", SqlDbType.Int);
                    arParms[2].Value = objD.Paymode;

                    arParms[3] = new SqlParameter("@DateFrom", SqlDbType.DateTime);
                    arParms[3].Value = objD.DateFrom;

                    arParms[4] = new SqlParameter("@DateTo", SqlDbType.DateTime);
                    arParms[4].Value = objD.DateTo;

                    arParms[5] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[5].Value = objD.IsActive;

                    arParms[6] = new SqlParameter("@CollectedByID", SqlDbType.BigInt);
                    arParms[6].Value = objD.CollectedByID;

                    arParms[7] = new SqlParameter("@BillNo", SqlDbType.VarChar);
                    arParms[7].Value = objD.BillNo;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetEMRGPhrBillList", arParms);
                    List<EmrgAdmissionData> lstresult = ORHelper<EmrgAdmissionData>.FromDataReaderToList(sqlReader);
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

        public List<EMRDiscountListData> GetDiscountListForEmergency(EmrgAdmissionData objD)
        {
            List<EMRDiscountListData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@EmrgNo", SqlDbType.NVarChar);
                    arParms[0].Value = objD.EmrgNo;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetEmergencyDiscountList", arParms);
                    List<EMRDiscountListData> lstresult = ORHelper<EMRDiscountListData>.FromDataReaderToList(sqlReader);
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
        public List<EmrgAdmissionData> UpdateEmrgCodes(EmrgAdmissionData objadm)
        {
            List<EmrgAdmissionData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[3];

                    arParms[0] = new SqlParameter("@EMRGNo", SqlDbType.VarChar);
                    arParms[0].Value = objadm.EmrgNo;

                    arParms[1] = new SqlParameter("@QR", SqlDbType.Image);
                    arParms[1].Value = objadm.QRImage;

                    arParms[2] = new SqlParameter("@Barcode", SqlDbType.Image);
                    arParms[2].Value = objadm.BarcodeImage;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Update_EmergencyCodes", arParms);
                    List<EmrgAdmissionData> listpatientdetails = ORHelper<EmrgAdmissionData>.FromDataReaderToList(sqlReader);
                    result = listpatientdetails;

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
        public List<EmrgAdmissionData> GetEmrgListReport(EmrgAdmissionData objEmrgdata)
        {
            List<EmrgAdmissionData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[7];

                    arParms[0] = new SqlParameter("@DateFrom", SqlDbType.DateTime);
                    arParms[0].Value = objEmrgdata.DateFrom;

                    arParms[1] = new SqlParameter("@DateTo", SqlDbType.DateTime);
                    arParms[1].Value = objEmrgdata.DateTo;

                    //arParms[2] = new SqlParameter("@DeptID", SqlDbType.Int);
                    //arParms[2].Value = objEmrgdata.DeptID;

                    arParms[2] = new SqlParameter("@DocID", SqlDbType.BigInt);
                    arParms[2].Value = objEmrgdata.DocID;

                    arParms[3] = new SqlParameter("@DischargeStatus", SqlDbType.Int);
                    arParms[3].Value = objEmrgdata.DischargeStatus;

                    arParms[4] = new SqlParameter("@GenID", SqlDbType.Int);
                    arParms[4].Value = objEmrgdata.GenID;

                    arParms[5] = new SqlParameter("@Agefrom", SqlDbType.Int);
                    arParms[5].Value = objEmrgdata.Agefrom;

                    arParms[6] = new SqlParameter("@Ageto", SqlDbType.Int);
                    arParms[6].Value = objEmrgdata.Ageto;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetEmergencyListReport", arParms);
                    List<EmrgAdmissionData> listpatientdetails = ORHelper<EmrgAdmissionData>.FromDataReaderToList(sqlReader);
                    result = listpatientdetails;
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

using Mediqura.CommonData.AdmissionData;
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

namespace Mediqura.DAL.MedBillDA
{
    public class FinalBillDA
    {
        public List<IPData> getIPNo(IPData objD)
        {
            List<IPData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                    arParms[0].Value = objD.IPNo;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_IPNo", arParms);
                    List<IPData> lstresult = ORHelper<IPData>.FromDataReaderToList(sqlReader);
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
        public List<IPData> GetIntimatedIPNo(IPData objD)
        {
            List<IPData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                    arParms[0].Value = objD.IPNo;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_IPNoIntimation", arParms);
                    List<IPData> lstresult = ORHelper<IPData>.FromDataReaderToList(sqlReader);
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
        public List<IPData> GetIntimatedIPNos(IPData objD)
        {
            List<IPData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                    arParms[0].Value = objD.IPNo;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_IPNoIntimations", arParms);
                    List<IPData> lstresult = ORHelper<IPData>.FromDataReaderToList(sqlReader);
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
        public List<IPData> GetPHRIntimatedIPNos(IPData objD)
        {
            List<IPData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                    arParms[0].Value = objD.IPNo;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_PHR_IPNoIntimations", arParms);
                    List<IPData> lstresult = ORHelper<IPData>.FromDataReaderToList(sqlReader);
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
        public List<IPData> GetFinalIPNo(IPData objD)
        {
            List<IPData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                    arParms[0].Value = objD.IPNo;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Get_FinalIpnumbers", arParms);
                    List<IPData> lstresult = ORHelper<IPData>.FromDataReaderToList(sqlReader);
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
        public List<FinalBillData> Get_IP_Summarised_Bills(FinalBillData objD)
        {
            List<FinalBillData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];
                    arParms[0] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                    arParms[0].Value = objD.IPNo;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Get_IP_Summarised_Bill", arParms);
                    List<FinalBillData> lstresult = ORHelper<FinalBillData>.FromDataReaderToList(sqlReader);
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
        public int Update_Final_BillDetails(FinalBillData objbill)
        {
            int result = 0;
            try
            {
                {

                    SqlParameter[] arParms = new SqlParameter[17];

                    arParms[0] = new SqlParameter("@XMLData", SqlDbType.Xml);
                    arParms[0].Value = objbill.XMLData;

                    arParms[1] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[1].Value = objbill.UHID;

                    arParms[2] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                    arParms[2].Value = objbill.IPNo;

                    arParms[3] = new SqlParameter("@TotalBillAmount", SqlDbType.Money);
                    arParms[3].Value = objbill.TotalBillAmount;

                    arParms[4] = new SqlParameter("@TotalAdjusted", SqlDbType.Money);
                    arParms[4].Value = objbill.TotalAdjusted;

                    arParms[5] = new SqlParameter("@TotalDiscount", SqlDbType.Money);
                    arParms[5].Value = objbill.TotalDiscount;

                    arParms[6] = new SqlParameter("@PaidAmount", SqlDbType.Money);
                    arParms[6].Value = objbill.PaidAmount;

                    arParms[7] = new SqlParameter("@TotalDue", SqlDbType.Money);
                    arParms[7].Value = objbill.TotalDue;

                    arParms[8] = new SqlParameter("@PaymentMode", SqlDbType.Int);
                    arParms[8].Value = objbill.PaymentMode;

                    arParms[9] = new SqlParameter("@BankName", SqlDbType.VarChar);
                    arParms[9].Value = objbill.BankName;

                    arParms[10] = new SqlParameter("@AccountNo", SqlDbType.VarChar);
                    arParms[10].Value = objbill.AccountNo;

                    arParms[11] = new SqlParameter("@CardNo", SqlDbType.VarChar);
                    arParms[11].Value = objbill.CardNo;

                    arParms[12] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[12].Value = objbill.HospitalID;

                    arParms[13] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[13].Value = objbill.FinancialYearID;

                    arParms[14] = new SqlParameter("@AddedBy", SqlDbType.BigInt);
                    arParms[14].Value = objbill.EmployeeID;

                    arParms[15] = new SqlParameter("@Output", SqlDbType.BigInt);
                    arParms[15].Direction = ParameterDirection.Output;

                    arParms[16] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    arParms[16].Value = objbill.Remarks;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Save_IP_Final_Bills", arParms);
                    if (result_ > 0 || result_ == -1)
                    {
                        result = Convert.ToInt32(arParms[15].Value);
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
        public List<FinalBillData> Get_IP_BilldetailsbyID(FinalBillData objD)
        {
            List<FinalBillData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];
                    arParms[0] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                    arParms[0].Value = objD.IPNo;
                    arParms[1] = new SqlParameter("@ServiceTypeID", SqlDbType.Int);
                    arParms[1].Value = objD.ServiceTypeID;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Get_IP_Bill_DetailsByservicetypeID", arParms);
                    List<FinalBillData> lstresult = ORHelper<FinalBillData>.FromDataReaderToList(sqlReader);
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
        public List<FinalBillData> Get_IPD_Collection_List(FinalBillData objD)
        {
            List<FinalBillData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[8];

                    arParms[0] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[0].Value = objD.UHID;

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
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetIPDFinalBillList", arParms);
                    List<FinalBillData> lstresult = ORHelper<FinalBillData>.FromDataReaderToList(sqlReader);
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
        public List<InterimBillData> Get_IPD_Iterimbill_List(InterimBillData objD)
        {
            List<InterimBillData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[7];

                    arParms[0] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[0].Value = objD.UHID;

                    arParms[1] = new SqlParameter("@PatientName", SqlDbType.VarChar);
                    arParms[1].Value = objD.PatientName;

                    arParms[2] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                    arParms[2].Value = objD.IPNo;

                    arParms[3] = new SqlParameter("@DateFrom", SqlDbType.DateTime);
                    arParms[3].Value = objD.DateFrom;

                    arParms[4] = new SqlParameter("@DateTo", SqlDbType.DateTime);
                    arParms[4].Value = objD.DateTo;

                    arParms[5] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[5].Value = objD.HospitalID;

                    arParms[6] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[6].Value = objD.FinancialYearID;


                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Get_IP_Intermediate_Bill", arParms);
                    List<InterimBillData> lstresult = ORHelper<InterimBillData>.FromDataReaderToList(sqlReader);
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
        public int Update_interimbilldetails(InterimBillData objbill)
        {
            int result = 0;
            try
            {
                {

                    SqlParameter[] arParms = new SqlParameter[4];

                    arParms[0] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                    arParms[0].Value = objbill.IPNo;

                    arParms[1] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[1].Value = objbill.HospitalID;

                    arParms[2] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[2].Value = objbill.FinancialYearID;

                    arParms[3] = new SqlParameter("@Output", SqlDbType.Int);
                    arParms[3].Direction = ParameterDirection.Output;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Update_Interim_BillStatus", arParms);
                    if (result_ > 0 || result_ == -1)
                    {
                        result = Convert.ToInt32(arParms[3].Value);
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
        public List<FinalBillData> GetPatientAdmissionDetailsByIPNo(FinalBillData objD)
        {
            List<FinalBillData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];
                    arParms[0] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                    arParms[0].Value = objD.IPNo;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetPatientAdmissionDetailsByIPNo", arParms);

                    List<FinalBillData> lstresult = ORHelper<FinalBillData>.FromDataReaderToList(sqlReader);
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
        public List<IPfinalbillData> GetIPfinalbillDetails(IPfinalbillData objD)
        {
            List<IPfinalbillData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[4];

                    arParms[0] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                    arParms[0].Value = objD.IPNo;

                    arParms[1] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[1].Value = objD.FinancialYearID;

                    arParms[2] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[2].Value = objD.HospitalID;

                    arParms[3] = new SqlParameter("@AmountEnable", SqlDbType.Int);
                    arParms[3].Value = objD.AmountEnable;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Get_IP_finalbilldetails", arParms);
                    List<IPfinalbillData> lstresult = ORHelper<IPfinalbillData>.FromDataReaderToList(sqlReader);
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
        public List<BillAdjustmentData> GetBilldetails(BillAdjustmentData objD)
        {
            List<BillAdjustmentData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[4];

                    arParms[0] = new SqlParameter("@BillCategory", SqlDbType.Int);
                    arParms[0].Value = objD.BillCategory;

                    arParms[1] = new SqlParameter("@PatientNumber", SqlDbType.VarChar);
                    arParms[1].Value = objD.PatientNumber;

                    arParms[2] = new SqlParameter("@ServiceCategory", SqlDbType.Int);
                    arParms[2].Value = objD.ServiceCategoryID;

                    arParms[3] = new SqlParameter("@PatientCategory", SqlDbType.Int);
                    arParms[3].Value = objD.PatientCategory;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Get_ServicerecordDetailByBillCategory", arParms);
                    List<BillAdjustmentData> lstresult = ORHelper<BillAdjustmentData>.FromDataReaderToList(sqlReader);
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
        public int DeleteServiceRecordbyID(BillAdjustmentData objbill)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[10];

                    arParms[0] = new SqlParameter("@PatientNumber", SqlDbType.VarChar);
                    arParms[0].Value = objbill.PatientNumber;

                    arParms[1] = new SqlParameter("@ID", SqlDbType.BigInt);
                    arParms[1].Value = objbill.ID;

                    arParms[2] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    arParms[2].Value = objbill.Remarks;

                    arParms[3] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[3].Value = objbill.HospitalID;

                    arParms[4] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[4].Value = objbill.EmployeeID;

                    arParms[5] = new SqlParameter("@InvNumber", SqlDbType.VarChar);
                    arParms[5].Value = objbill.InvNumber;

                    arParms[6] = new SqlParameter("@ServiceNumber", SqlDbType.VarChar);
                    arParms[6].Value = objbill.ServiceNumber;

                    arParms[7] = new SqlParameter("@BillCategory", SqlDbType.Int);
                    arParms[7].Value = objbill.BillCategory;

                    arParms[8] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[8].Direction = ParameterDirection.Output;

                    arParms[9] = new SqlParameter("@ServiceID", SqlDbType.Int);
                    arParms[9].Value = objbill.ServiceID;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_DeleteServiceRecordByID", arParms);
                    if (result_ > 0 || result_ == -1)
                    {
                        result = Convert.ToInt32(arParms[8].Value);
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
        public int UpdateServiceRecord(BillAdjustmentData objD)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[5];

                    arParms[0] = new SqlParameter("@XMLData", SqlDbType.Xml);
                    arParms[0].Value = objD.XMLData;

                    arParms[1] = new SqlParameter("@PatientNumber", SqlDbType.VarChar);
                    arParms[1].Value = objD.PatientNumber;

                    arParms[2] = new SqlParameter("@BillCategory", SqlDbType.Int);
                    arParms[2].Value = objD.BillCategory;

                    arParms[3] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[3].Value = objD.EmployeeID;

                    arParms[4] = new SqlParameter("@Output", SqlDbType.Int);
                    arParms[4].Direction = ParameterDirection.Output;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_UpdateServiceRecords", arParms);
                    if (result_ > 0 || result_ == -1)
                    {
                        result = Convert.ToInt32(arParms[4].Value);
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
        public List<IPDiscountListData> GetDiscountListForIP(IPfinalbillData objD)
        {
            List<IPDiscountListData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@IPNo", SqlDbType.NVarChar);
                    arParms[0].Value = objD.IPNo;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetIPDiscountList", arParms);
                    List<IPDiscountListData> lstresult = ORHelper<IPDiscountListData>.FromDataReaderToList(sqlReader);
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
        public int DeleteIPservices(IPfinalbillData objbill)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[10];

                    arParms[0] = new SqlParameter("@IPNo", SqlDbType.NVarChar);
                    arParms[0].Value = objbill.IPNo;

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

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_DeleteIPservices", arParms);
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
        public int DeleteIPFinalbill(IPfinalbillData objbill)
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

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Delete_IP_FinalBill", arParms);
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
        public List<FinalBillData> GetIntimationList(FinalBillData objbill)
        {
            List<FinalBillData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[6];

                    arParms[0] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                    arParms[0].Value = objbill.AdmissionNo;

                    arParms[1] = new SqlParameter("@PatientName", SqlDbType.VarChar);
                    arParms[1].Value = objbill.PatientName;


                    arParms[3] = new SqlParameter("@DateFrom", SqlDbType.DateTime);
                    arParms[3].Value = objbill.DateFrom;

                    arParms[4] = new SqlParameter("@DateTo", SqlDbType.DateTime);
                    arParms[4].Value = objbill.DateTo;

                    arParms[5] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[5].Value = objbill.IsActive;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_GetIntimationList", arParms);
                    List<FinalBillData> listpatientdetails = ORHelper<FinalBillData>.FromDataReaderToList(sqlReader);
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
        public List<FinalBillData> GetCurrentIntimationList(FinalBillData objbill)
        {
            List<FinalBillData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetIntimatedpatientList", arParms);
                    List<FinalBillData> listpatientdetails = ORHelper<FinalBillData>.FromDataReaderToList(sqlReader);
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
        public List<FinalBillData> GetNestedList(FinalBillData objbill)
        {
            List<FinalBillData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                    arParms[0].Value = objbill.AdmissionNo;

                    arParms[1] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[1].Value = objbill.IsActive;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_GetIntimationList2", arParms);
                    List<FinalBillData> listpatientdetails = ORHelper<FinalBillData>.FromDataReaderToList(sqlReader);
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
        public List<FinalBillData> bindNestedService(FinalBillData objbill)
        {
            List<FinalBillData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                    arParms[0].Value = objbill.AdmissionNo;

                    arParms[1] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[1].Value = objbill.IsActive;

                    SqlDataReader sqlReader = null;
                    //sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_GetIntimationList2", arParms);
                    List<FinalBillData> listpatientdetails = ORHelper<FinalBillData>.FromDataReaderToList(sqlReader);
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
        public List<IPfinalbillData> UpdatFinalBill(IPfinalbillData objbill)
        {
            List<IPfinalbillData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[20];

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
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Save_IPD_Final_Bills", arParms);
                    List<IPfinalbillData> lstresult = ORHelper<IPfinalbillData>.FromDataReaderToList(sqlReader);
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
        public List<IPfinalbillData> GetIPDfinalbillList(IPfinalbillData objD)
        {
            List<IPfinalbillData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[10];

                    arParms[0] = new SqlParameter("@IPNo", SqlDbType.NVarChar);
                    arParms[0].Value = objD.IPNo;

                    arParms[1] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[1].Value = objD.UHID;

                    arParms[2] = new SqlParameter("@PatientName", SqlDbType.VarChar);
                    arParms[2].Value = objD.PatientName;

                    arParms[3] = new SqlParameter("@Paymode", SqlDbType.Int);
                    arParms[3].Value = objD.Paymode;

                    arParms[4] = new SqlParameter("@DateFrom", SqlDbType.DateTime);
                    arParms[4].Value = objD.DateFrom;

                    arParms[5] = new SqlParameter("@DateTo", SqlDbType.DateTime);
                    arParms[5].Value = objD.DateTo;

                    arParms[6] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[6].Value = objD.IsActive;

                    arParms[7] = new SqlParameter("@CollectedByID", SqlDbType.BigInt);
                    arParms[7].Value = objD.CollectedByID;

                    arParms[8] = new SqlParameter("@BillNo", SqlDbType.VarChar);
                    arParms[8].Value = objD.BillNo;

                    arParms[9] = new SqlParameter("@AmountEnable", SqlDbType.Int);
                    arParms[9].Value = objD.AmountEnable;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetIPD_FinalbilList", arParms);
                    List<IPfinalbillData> lstresult = ORHelper<IPfinalbillData>.FromDataReaderToList(sqlReader);
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
    }
}

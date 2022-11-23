using Mediqura.CommonData.AdmissionData;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.MedBillData;
using Mediqura.Utility;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using Mediqura.Utility.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Mediqura.DAL.MedBillDA
{
    public class OPDbillingDA
    {
        public List<OPDbillingData> GetServiceChargeByID(OPDbillingData objbill)
        {
            List<OPDbillingData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[3];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.BigInt);
                    arParms[0].Value = objbill.ID;

                    arParms[1] = new SqlParameter("@ServiceTypeID", SqlDbType.Int);
                    arParms[1].Value = objbill.ServiceTypeID;

                    arParms[2] = new SqlParameter("@DoctorID", SqlDbType.BigInt);
                    arParms[2].Value = objbill.DocID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Get_OPD_Service_ChargeList", arParms);
                    List<OPDbillingData> chargelist = ORHelper<OPDbillingData>.FromDataReaderToList(sqlReader);
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
        public List<BankDetail> Getbanklist(BankDetail objbill)
        {
            List<BankDetail> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@PaymodeID", SqlDbType.Int);
                    arParms[0].Value = objbill.PaymodeID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Get_BankList", arParms);
                    List<BankDetail> chargelist = ORHelper<BankDetail>.FromDataReaderToList(sqlReader);
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
        public List<OPDbillingData> UpdateOPDBill(OPDbillingData objbill)
        {
            List<OPDbillingData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[29];

                    arParms[0] = new SqlParameter("@XMLData", SqlDbType.Xml);
                    arParms[0].Value = objbill.XMLData;

                    arParms[1] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[1].Value = objbill.UHID;

                    arParms[2] = new SqlParameter("@TotalBillAmount", SqlDbType.Money);
                    arParms[2].Value = objbill.TotalBillAmount;

                    arParms[3] = new SqlParameter("@AdjustedAmount", SqlDbType.Money);
                    arParms[3].Value = objbill.AdjustedAmount;

                    arParms[4] = new SqlParameter("@DiscountedAmount", SqlDbType.Money);
                    arParms[4].Value = objbill.DiscountedAmount;

                    arParms[5] = new SqlParameter("@PaidAmount", SqlDbType.Money);
                    arParms[5].Value = objbill.PaidAmount;

                    arParms[6] = new SqlParameter("@PaymentMode", SqlDbType.Int);
                    arParms[6].Value = objbill.PaymentMode;

                    arParms[7] = new SqlParameter("@BankName", SqlDbType.VarChar);
                    arParms[7].Value = objbill.BankName;

                    arParms[8] = new SqlParameter("@InvoiceNumber", SqlDbType.VarChar);
                    arParms[8].Value = objbill.InvoiceNumber;

                    arParms[9] = new SqlParameter("@ChequeUTRnumber", SqlDbType.VarChar);
                    arParms[9].Value = objbill.ChequeUTRnumber;

                    arParms[10] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[10].Value = objbill.HospitalID;

                    arParms[11] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[11].Value = objbill.FinancialYearID;

                    arParms[12] = new SqlParameter("@AddedBy", SqlDbType.BigInt);
                    arParms[12].Value = objbill.EmployeeID;

                    arParms[13] = new SqlParameter("@DeptID", SqlDbType.Int);
                    arParms[13].Value = objbill.DeptID;

                    arParms[14] = new SqlParameter("@DocID", SqlDbType.BigInt);
                    arParms[14].Value = objbill.DocID;

                    arParms[15] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                    arParms[15].Value = objbill.IPaddress;

                    arParms[16] = new SqlParameter("@DiscountByID", SqlDbType.BigInt);
                    arParms[16].Value = objbill.DiscountByID;

                    arParms[17] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    arParms[17].Value = objbill.Remarks;

                    arParms[18] = new SqlParameter("@IsDis", SqlDbType.Int);
                    arParms[18].Value = objbill.isDis;

                    arParms[19] = new SqlParameter("@Barcode", SqlDbType.Image);
                    arParms[19].Value = objbill.BarcodeImage;

                    arParms[20] = new SqlParameter("@ID", SqlDbType.BigInt);
                    arParms[20].Value = objbill.ID;

                    arParms[21] = new SqlParameter("@patientType", SqlDbType.Int);
                    arParms[21].Value = objbill.patientType;

                    arParms[22] = new SqlParameter("@isExtraDiscount", SqlDbType.Int);
                    arParms[22].Value = objbill.isExtraDiscount;

                    arParms[23] = new SqlParameter("@ExtraDiscountXML", SqlDbType.Xml);
                    arParms[23].Value = objbill.extraDiscountXML;

                    arParms[24] = new SqlParameter("@QR", SqlDbType.Image);
                    arParms[24].Value = objbill.QRImage;

                    arParms[25] = new SqlParameter("@IsVerified", SqlDbType.Int);
                    arParms[25].Value = objbill.IsVerified;

                    arParms[26] = new SqlParameter("@SourceID", SqlDbType.Int);
                    arParms[26].Value = objbill.SourceID;

                    arParms[27] = new SqlParameter("@ReferalID", SqlDbType.BigInt);
                    arParms[27].Value = objbill.ReferalID;

                    arParms[28] = new SqlParameter("@ReferalName", SqlDbType.VarChar);
                    arParms[28].Value = objbill.ReferalName;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Update_OPDBillDetails", arParms);
                    List<OPDbillingData> chargelist = ORHelper<OPDbillingData>.FromDataReaderToList(sqlReader);
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
        public List<LabBillingData> GetLabServiceAmountByIDTestCenterID(LabBillingData objbill)
        {
            List<LabBillingData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.BigInt);
                    arParms[0].Value = objbill.ID;

                    arParms[1] = new SqlParameter("@TestCenterID", SqlDbType.Int);
                    arParms[1].Value = objbill.TestCenterID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetLabServiceAmountByIDTestCenterID", arParms);
                    List<LabBillingData> chargelist = ORHelper<LabBillingData>.FromDataReaderToList(sqlReader);
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
        public int UpdatePaymentVerification(OPDbillingData obj)
        {
            int result = 0;
            try
            {
                SqlParameter[] arParms = new SqlParameter[3];

                arParms[0] = new SqlParameter("@BillNo", SqlDbType.VarChar);
                arParms[0].Value = obj.BillNo;

                arParms[1] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                arParms[1].Value = obj.EmployeeID;

                arParms[2] = new SqlParameter("@Output", SqlDbType.SmallInt);
                arParms[2].Direction = ParameterDirection.Output;



                int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_UpdatePaymentVerification", arParms);
                if (result_ > 0 || result_ == -1)
                    result = Convert.ToInt32(arParms[2].Value);

            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.DataAccessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
                throw new DataAccessException("5000001", ex);
            }
            return result;

        }
        public int UpdateLabPaymentVerification(LabBillingData obj)
        {
            int result = 0;
            try
            {
                SqlParameter[] arParms = new SqlParameter[3];

                arParms[0] = new SqlParameter("@BillNo", SqlDbType.VarChar);
                arParms[0].Value = obj.BillNo;

                arParms[1] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                arParms[1].Value = obj.EmployeeID;

                arParms[2] = new SqlParameter("@Output", SqlDbType.SmallInt);
                arParms[2].Direction = ParameterDirection.Output;



                int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_UpdateLabPaymentVerification", arParms);
                if (result_ > 0 || result_ == -1)
                    result = Convert.ToInt32(arParms[2].Value);

            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.DataAccessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
                throw new DataAccessException("5000001", ex);
            }
            return result;

        }
        public List<OPDbillingData> GetOPBillList(OPDbillingData objbill)
        {
            List<OPDbillingData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[9];

                    arParms[0] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[0].Value = objbill.UHID;

                    arParms[1] = new SqlParameter("@PatientName", SqlDbType.VarChar);
                    arParms[1].Value = objbill.PatientName;

                    arParms[2] = new SqlParameter("@Paymode", SqlDbType.Int);
                    arParms[2].Value = objbill.Paymode;

                    arParms[3] = new SqlParameter("@DateFrom", SqlDbType.DateTime);
                    arParms[3].Value = objbill.DateFrom;

                    arParms[4] = new SqlParameter("@DateTo", SqlDbType.DateTime);
                    arParms[4].Value = objbill.DateTo;

                    arParms[5] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[5].Value = objbill.IsActive;

                    arParms[6] = new SqlParameter("@CollectedByID", SqlDbType.BigInt);
                    arParms[6].Value = objbill.CollectedByID;

                    arParms[7] = new SqlParameter("@pageno", SqlDbType.Int);
                    arParms[7].Value = objbill.CurrentIndex;

                    arParms[8] = new SqlParameter("@AmountEnable", SqlDbType.Int);
                    arParms[8].Value = objbill.AmountEnable;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetOPDBillList", arParms);
                    List<OPDbillingData> listpatientdetails = ORHelper<OPDbillingData>.FromDataReaderToList(sqlReader);
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
        public List<OPDbillingData> GetOPPaymentList(OPDbillingData objbill)
        {
            List<OPDbillingData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[7];

                    arParms[0] = new SqlParameter("@BillNo", SqlDbType.VarChar);
                    arParms[0].Value = objbill.BillNo;

                    arParms[1] = new SqlParameter("@PatientName", SqlDbType.VarChar);
                    arParms[1].Value = objbill.PatientName;

                    arParms[2] = new SqlParameter("@Paymode", SqlDbType.Int);
                    arParms[2].Value = objbill.Paymode;

                    arParms[3] = new SqlParameter("@DateFrom", SqlDbType.DateTime);
                    arParms[3].Value = objbill.DateFrom;

                    arParms[4] = new SqlParameter("@DateTo", SqlDbType.DateTime);
                    arParms[4].Value = objbill.DateTo;

                    arParms[5] = new SqlParameter("@CollectedByID", SqlDbType.BigInt);
                    arParms[5].Value = objbill.CollectedByID;

                    arParms[6] = new SqlParameter("@AmountEnable", SqlDbType.Int);
                    arParms[6].Value = objbill.AmountEnable;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetOPDPaymentList", arParms);
                    List<OPDbillingData> listpatientdetails = ORHelper<OPDbillingData>.FromDataReaderToList(sqlReader);
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
        public List<OPDbillingData> GetOPBillListReport(OPDbillingData objbill)
        {
            List<OPDbillingData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[10];

                    arParms[0] = new SqlParameter("@Paymode", SqlDbType.Int);
                    arParms[0].Value = objbill.Paymode;

                    arParms[1] = new SqlParameter("@DateFrom", SqlDbType.DateTime);
                    arParms[1].Value = objbill.DateFrom;

                    arParms[2] = new SqlParameter("@DateTo", SqlDbType.DateTime);
                    arParms[2].Value = objbill.DateTo;

                    arParms[3] = new SqlParameter("@AmountEnable", SqlDbType.Int);
                    arParms[3].Value = objbill.AmountEnable;

                    arParms[4] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[4].Value = objbill.UHID;

                    arParms[5] = new SqlParameter("@ServiceType", SqlDbType.Int);
                    arParms[5].Value = objbill.ServiceTypeID;

                    arParms[6] = new SqlParameter("@ServiceID", SqlDbType.Int);
                    arParms[6].Value = objbill.ServiceID;

                    arParms[7] = new SqlParameter("@DepartmentID", SqlDbType.Int);
                    arParms[7].Value = objbill.DeptID;

                    arParms[8] = new SqlParameter("@DoctorID", SqlDbType.BigInt);
                    arParms[8].Value = objbill.DocID;

                    arParms[9] = new SqlParameter("@PatientType", SqlDbType.Int);
                    arParms[9].Value = objbill.patientType;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetOPDBillListReport", arParms);
                    List<OPDbillingData> listpatientdetails = ORHelper<OPDbillingData>.FromDataReaderToList(sqlReader);
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
        public List<DoctorPayoutData> GetDoctorsPayableservices(DoctorPayoutData objbill)
        {
            List<DoctorPayoutData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[6];

                    arParms[0] = new SqlParameter("@DoctorID", SqlDbType.BigInt);
                    arParms[0].Value = objbill.DoctorID;

                    arParms[1] = new SqlParameter("@PaymentStatus", SqlDbType.Int);
                    arParms[1].Value = objbill.PaymentStatus;

                    arParms[2] = new SqlParameter("@DateFrom", SqlDbType.DateTime);
                    arParms[2].Value = objbill.DateFrom;

                    arParms[3] = new SqlParameter("@DateTo", SqlDbType.DateTime);
                    arParms[3].Value = objbill.DateTo;

                    arParms[4] = new SqlParameter("@ServiceCategory", SqlDbType.Int);
                    arParms[4].Value = objbill.ServiceCategory;

                    arParms[5] = new SqlParameter("@PayableCategory", SqlDbType.Int);
                    arParms[5].Value = objbill.PayableCategory;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_DoctorPayableServices", arParms);
                    List<DoctorPayoutData> servicelist = ORHelper<DoctorPayoutData>.FromDataReaderToList(sqlReader);
                    result = servicelist;
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
        public List<DoctorPayoutData> GetDoctorsEarnings(DoctorPayoutData objbill)
        {
            List<DoctorPayoutData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[5];

                    arParms[0] = new SqlParameter("@DoctorID", SqlDbType.BigInt);
                    arParms[0].Value = objbill.DoctorID;

                    arParms[1] = new SqlParameter("@PaymentStatus", SqlDbType.Int);
                    arParms[1].Value = objbill.PaymentStatus;

                    arParms[2] = new SqlParameter("@DateFrom", SqlDbType.DateTime);
                    arParms[2].Value = objbill.DateFrom;

                    arParms[3] = new SqlParameter("@DateTo", SqlDbType.DateTime);
                    arParms[3].Value = objbill.DateTo;

                    arParms[4] = new SqlParameter("@ServiceCategory", SqlDbType.Int);
                    arParms[4].Value = objbill.ServiceCategory;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_DoctorEarnings", arParms);
                    List<DoctorPayoutData> servicelist = ORHelper<DoctorPayoutData>.FromDataReaderToList(sqlReader);
                    result = servicelist;
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
        public List<DoctorPayoutData> GetPaymentlist(DoctorPayoutData objbill)
        {
            List<DoctorPayoutData> result = null;
            try
            {
                SqlParameter[] arParms = new SqlParameter[6];

                arParms[0] = new SqlParameter("@DoctorID", SqlDbType.BigInt);
                arParms[0].Value = objbill.DoctorID;

                arParms[1] = new SqlParameter("@PaymentNumber", SqlDbType.VarChar);
                arParms[1].Value = objbill.PaymentNumber;

                arParms[2] = new SqlParameter("@DateFrom", SqlDbType.DateTime);
                arParms[2].Value = objbill.DateFrom;

                arParms[3] = new SqlParameter("@DateTo", SqlDbType.DateTime);
                arParms[3].Value = objbill.DateTo;

                arParms[4] = new SqlParameter("@ServiceCategory", SqlDbType.Int);
                arParms[4].Value = objbill.ServiceCategory;

                arParms[5] = new SqlParameter("@PayableCategory", SqlDbType.Int);
                arParms[5].Value = objbill.PayableCategory;

                SqlDataReader sqlReader = null;
                sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Get_Doctor_Paymenthistorylist", arParms);
                List<DoctorPayoutData> servicelist = ORHelper<DoctorPayoutData>.FromDataReaderToList(sqlReader);
                result = servicelist;

            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.DataAccessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
                throw new DataAccessException("5000001", ex);
            }
            return result;
        }
        public List<DoctorPayoutData> GetdueDates(DoctorPayoutData objbill)
        {
            List<DoctorPayoutData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[4];

                    arParms[0] = new SqlParameter("@DoctorID", SqlDbType.BigInt);
                    arParms[0].Value = objbill.DoctorID;

                    arParms[1] = new SqlParameter("@PaymentStatus", SqlDbType.Int);
                    arParms[1].Value = objbill.PaymentStatus;

                    arParms[2] = new SqlParameter("@ServiceCategory", SqlDbType.Int);
                    arParms[2].Value = objbill.ServiceCategory;

                    arParms[3] = new SqlParameter("@PayableCategory", SqlDbType.Int);
                    arParms[3].Value = objbill.PayableCategory;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_DoctorDueDates", arParms);
                    List<DoctorPayoutData> servicelist = ORHelper<DoctorPayoutData>.FromDataReaderToList(sqlReader);
                    result = servicelist;
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
        public List<DoctorPayoutData> PaidDoctorsServices(DoctorPayoutData objbill)
        {
            List<DoctorPayoutData> result = null;
            try
            {
                SqlParameter[] arParms = new SqlParameter[15];

                arParms[0] = new SqlParameter("@XMLPaidData", SqlDbType.Xml);
                arParms[0].Value = objbill.XMLPaidData;

                arParms[1] = new SqlParameter("@ReferralID", SqlDbType.BigInt);
                arParms[1].Value = objbill.ReferralID;

                arParms[2] = new SqlParameter("@ServiceCategory", SqlDbType.Int);
                arParms[2].Value = objbill.ServiceCategory;

                arParms[3] = new SqlParameter("@GdTotalBillAmount", SqlDbType.Money);
                arParms[3].Value = objbill.GdTotalBillAmount;

                arParms[4] = new SqlParameter("@GdTotalDiscount", SqlDbType.Money);
                arParms[4].Value = objbill.GdTotalDiscount;

                arParms[5] = new SqlParameter("@GdNetAmount", SqlDbType.Money);
                arParms[5].Value = objbill.GdNetAmount;

                arParms[6] = new SqlParameter("@GdReferralPayable", SqlDbType.Money);
                arParms[6].Value = objbill.GdReferralPayable;

                arParms[7] = new SqlParameter("@GdRefDiscount", SqlDbType.Money);
                arParms[7].Value = objbill.GdRefDiscount;

                arParms[8] = new SqlParameter("@GdPaid", SqlDbType.Money);
                arParms[8].Value = objbill.GdPaid;

                arParms[9] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                arParms[9].Value = objbill.Remarks;

                arParms[10] = new SqlParameter("@DateFrom", SqlDbType.DateTime);
                arParms[10].Value = objbill.DateFrom;

                arParms[11] = new SqlParameter("@DateTo", SqlDbType.DateTime);
                arParms[11].Value = objbill.DateTo;

                arParms[12] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                arParms[12].Value = objbill.EmployeeID;

                arParms[13] = new SqlParameter("@HospitalID", SqlDbType.Int);
                arParms[13].Value = objbill.HospitalID;

                arParms[14] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                arParms[14].Value = objbill.FinancialYearID;



                SqlDataReader sqlReader = null;
                sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Lab_SaveReferralPercentPayment", arParms);
                List<DoctorPayoutData> servicelist = ORHelper<DoctorPayoutData>.FromDataReaderToList(sqlReader);
                result = servicelist;
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.DataAccessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
                throw new DataAccessException("5000001", ex);
            }
            return result;
        }
        public List<OPDbillingData> GetOPPatientBillList(OPDbillingData objbill)
        {
            List<OPDbillingData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[8];

                    arParms[0] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[0].Value = objbill.UHID;

                    arParms[1] = new SqlParameter("@PatientName", SqlDbType.VarChar);
                    arParms[1].Value = objbill.PatientName;

                    arParms[2] = new SqlParameter("@Paymode", SqlDbType.Int);
                    arParms[2].Value = objbill.Paymode;

                    arParms[3] = new SqlParameter("@DateFrom", SqlDbType.DateTime);
                    arParms[3].Value = objbill.DateFrom;

                    arParms[4] = new SqlParameter("@DateTo", SqlDbType.DateTime);
                    arParms[4].Value = objbill.DateTo;

                    arParms[5] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[5].Value = objbill.IsActive;

                    arParms[6] = new SqlParameter("@CollectedByID", SqlDbType.BigInt);
                    arParms[6].Value = objbill.CollectedByID;

                    arParms[7] = new SqlParameter("@AmountEnable", SqlDbType.Int);
                    arParms[7].Value = objbill.AmountEnable;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetOPDPatientBillList", arParms);
                    List<OPDbillingData> listpatientdetails = ORHelper<OPDbillingData>.FromDataReaderToList(sqlReader);
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
        public List<OPDbillingData> GetautoOPbills(OPDbillingData objbill)
        {
            List<OPDbillingData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@BillNo", SqlDbType.VarChar);
                    arParms[0].Value = objbill.BillNo;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetauoOPbills", arParms);
                    List<OPDbillingData> listpatientdetails = ORHelper<OPDbillingData>.FromDataReaderToList(sqlReader);
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
        public List<OPDbillingData> GetPatientDetailbybillNo(OPDbillingData objbill)
        {
            List<OPDbillingData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@BillNo", SqlDbType.VarChar);
                    arParms[0].Value = objbill.BillNo;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetpatientdetailsbyOPbills", arParms);
                    List<OPDbillingData> listpatientdetails = ORHelper<OPDbillingData>.FromDataReaderToList(sqlReader);
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
        public List<LabBillingData> GetPatientDetailbyLabbillNo(LabBillingData objbill)
        {
            List<LabBillingData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@BillNo", SqlDbType.VarChar);
                    arParms[0].Value = objbill.BillNo;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetpatientdetailsbyLabbills", arParms);
                    List<LabBillingData> listpatientdetails = ORHelper<LabBillingData>.FromDataReaderToList(sqlReader);
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
        public List<LabBillingData> GetLabServiceChargeByID(LabBillingData objbill)
        {
            List<LabBillingData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.BigInt);
                    arParms[0].Value = objbill.ID;

                    arParms[1] = new SqlParameter("@ServiceCategoryID", SqlDbType.Int);
                    arParms[1].Value = objbill.ServiceCategoryID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Get_OPD_LabService_ChargeList", arParms);
                    List<LabBillingData> chargelist = ORHelper<LabBillingData>.FromDataReaderToList(sqlReader);
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
        public List<LabBillingData> GetBillNo(LabBillingData objbill)
        {
            List<LabBillingData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@BillNo", SqlDbType.VarChar);
                    arParms[0].Value = objbill.BillNo;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "GetBillNo", arParms);
                    List<LabBillingData> chargelist = ORHelper<LabBillingData>.FromDataReaderToList(sqlReader);
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
        public List<LabBillingData> GetAdjustmentNo(LabBillingData objbill)
        {
            List<LabBillingData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@AdjustmentNo", SqlDbType.VarChar);
                    arParms[0].Value = objbill.AdjustmentNo;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "GetAdjustmentNo", arParms);
                    List<LabBillingData> chargelist = ORHelper<LabBillingData>.FromDataReaderToList(sqlReader);
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
        public List<LabBillingData> GetTestStatusByBillNO(LabBillingData objbill)
        {
            List<LabBillingData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@BillNo", SqlDbType.VarChar);
                    arParms[0].Value = objbill.BillNo;

                    arParms[1] = new SqlParameter("@LabServiceID", SqlDbType.Int);
                    arParms[1].Value = objbill.LabServiceID;


                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "GetTestStatusByBillNO", arParms);
                    List<LabBillingData> chargelist = ORHelper<LabBillingData>.FromDataReaderToList(sqlReader);
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
        public List<LabBillingData> GetIPServiceChargeByID(LabBillingData objbill)
        {
            List<LabBillingData> result = null;
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

                    arParms[3] = new SqlParameter("@IPnumber", SqlDbType.VarChar);
                    arParms[3].Value = objbill.IPnumber;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Get_IPD_Service_ChargeList", arParms);
                    List<LabBillingData> chargelist = ORHelper<LabBillingData>.FromDataReaderToList(sqlReader);
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
        public List<LabBillingData> GetOTServiceChargeByID(LabBillingData objbill)
        {
            List<LabBillingData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.BigInt);
                    arParms[0].Value = objbill.ID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Get_OTService_ChargeList", arParms);
                    List<LabBillingData> chargelist = ORHelper<LabBillingData>.FromDataReaderToList(sqlReader);
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
        public List<LabBillingData> UpdateOPDLabBill(LabBillingData objbill)
        {
            List<LabBillingData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[40];

                    arParms[0] = new SqlParameter("@XMLData", SqlDbType.Xml);
                    arParms[0].Value = objbill.XMLData;

                    arParms[1] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[1].Value = objbill.UHID;

                    arParms[2] = new SqlParameter("@ID", SqlDbType.BigInt);
                    arParms[2].Value = objbill.ID;

                    arParms[3] = new SqlParameter("@TotalBillAmount", SqlDbType.Money);
                    arParms[3].Value = objbill.TotalBillAmount;

                    arParms[4] = new SqlParameter("@AdjustedAmount", SqlDbType.Money);
                    arParms[4].Value = objbill.AdjustedAmount;

                    arParms[5] = new SqlParameter("@DiscountedAmount", SqlDbType.Money);
                    arParms[5].Value = objbill.DiscountedAmount;

                    arParms[6] = new SqlParameter("@PaidAmount", SqlDbType.BigInt);
                    arParms[6].Value = objbill.PaidAmount;

                    arParms[7] = new SqlParameter("@PaymentMode", SqlDbType.Int);
                    arParms[7].Value = objbill.PaymentMode;

                    arParms[8] = new SqlParameter("@BankName", SqlDbType.VarChar);
                    arParms[8].Value = objbill.BankName;

                    arParms[9] = new SqlParameter("@InvoiceNumber", SqlDbType.VarChar);
                    arParms[9].Value = objbill.INvoiceNo;

                    arParms[10] = new SqlParameter("@ChequeUTRnumber", SqlDbType.VarChar);
                    arParms[10].Value = objbill.CheaqueNo;

                    arParms[11] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[11].Value = objbill.HospitalID;

                    arParms[12] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[12].Value = objbill.FinancialYearID;

                    arParms[13] = new SqlParameter("@AddedBy", SqlDbType.BigInt);
                    arParms[13].Value = objbill.EmployeeID;

                    arParms[14] = new SqlParameter("@DeptID", SqlDbType.BigInt);
                    arParms[14].Value = objbill.DeptID;

                    arParms[15] = new SqlParameter("@DocID", SqlDbType.BigInt);
                    arParms[15].Value = objbill.DocID;

                    arParms[16] = new SqlParameter("@PatientType", SqlDbType.BigInt);
                    arParms[16].Value = objbill.PatientType;

                    arParms[17] = new SqlParameter("@ReferredDoctorID", SqlDbType.Int);
                    arParms[17].Value = objbill.ReferredDoctorID;

                    arParms[18] = new SqlParameter("@PatientName", SqlDbType.VarChar);
                    arParms[18].Value = objbill.PatientName;

                    arParms[19] = new SqlParameter("@PatientAddress", SqlDbType.VarChar);
                    arParms[19].Value = objbill.PatientAddress;

                    arParms[20] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                    arParms[20].Value = objbill.IPaddress;

                    arParms[21] = new SqlParameter("@DiscountByID", SqlDbType.BigInt);
                    arParms[21].Value = objbill.DiscountByID;

                    arParms[22] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    arParms[22].Value = objbill.Remarks;

                    arParms[23] = new SqlParameter("@Age", SqlDbType.VarChar);
                    arParms[23].Value = objbill.AgeCount;

                    arParms[24] = new SqlParameter("@GenderID", SqlDbType.VarChar);
                    arParms[24].Value = objbill.GenderID;

                    arParms[25] = new SqlParameter("@IsDis", SqlDbType.Int);
                    arParms[25].Value = objbill.isDis;

                    arParms[26] = new SqlParameter("@Barcode", SqlDbType.Image);
                    arParms[26].Value = objbill.BarcodeImage;

                    arParms[27] = new SqlParameter("@OPnumber", SqlDbType.VarChar);
                    arParms[27].Value = objbill.OPnumber;

                    arParms[28] = new SqlParameter("@TPAcompanyID", SqlDbType.Int);
                    arParms[28].Value = objbill.CompanyID;

                    arParms[29] = new SqlParameter("@PackageID", SqlDbType.Int);
                    arParms[29].Value = objbill.PackageID;

                    arParms[30] = new SqlParameter("@isExtraDiscount", SqlDbType.Int);
                    arParms[30].Value = objbill.isExtraDiscount;

                    arParms[31] = new SqlParameter("@ExtraDiscountXML", SqlDbType.Xml);
                    arParms[31].Value = objbill.extraDiscountXML;

                    arParms[32] = new SqlParameter("@PatientCat", SqlDbType.Int);
                    arParms[32].Value = objbill.PatientCat;

                    arParms[33] = new SqlParameter("@IsVerified", SqlDbType.Int);
                    arParms[33].Value = objbill.IsVerified;

                    arParms[34] = new SqlParameter("@ReferalID", SqlDbType.BigInt);
                    arParms[34].Value = objbill.ReferalID;

                    arParms[35] = new SqlParameter("@SourceID", SqlDbType.Int);
                    arParms[35].Value = objbill.SourceID;

                    arParms[36] = new SqlParameter("@ReferalName", SqlDbType.VarChar);
                    arParms[36].Value = objbill.ReferalName;

                    arParms[37] = new SqlParameter("@DueAmount", SqlDbType.Money);
                    arParms[37].Value = objbill.DueAmount;

                    arParms[38] = new SqlParameter("@RunnerID", SqlDbType.Int);
                    arParms[38].Value = objbill.RunnerID;

                    arParms[39] = new SqlParameter("@RefNo", SqlDbType.VarChar);
                    arParms[39].Value = objbill.RefNo;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Update_OPDLabBillDetails", arParms);
                    List<LabBillingData> chargelist = ORHelper<LabBillingData>.FromDataReaderToList(sqlReader);
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
        public List<LabBillingData> GetLabBillList(LabBillingData objbill)
        {
            List<LabBillingData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[11];

                    arParms[0] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[0].Value = objbill.UHID;

                    arParms[1] = new SqlParameter("@PatientName", SqlDbType.VarChar);
                    arParms[1].Value = objbill.PatientName;

                    arParms[2] = new SqlParameter("@Paymode", SqlDbType.Int);
                    arParms[2].Value = objbill.Paymode;

                    arParms[3] = new SqlParameter("@DateFrom", SqlDbType.DateTime);
                    arParms[3].Value = objbill.DateFrom;

                    arParms[4] = new SqlParameter("@DateTo", SqlDbType.DateTime);
                    arParms[4].Value = objbill.DateTo;

                    arParms[5] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[5].Value = objbill.IsActive;

                    arParms[6] = new SqlParameter("@CollectedByID", SqlDbType.BigInt);
                    arParms[6].Value = objbill.CollectedByID;

                    arParms[7] = new SqlParameter("@InvestigationNo", SqlDbType.VarChar);
                    arParms[7].Value = objbill.InvestigationNo;

                    arParms[8] = new SqlParameter("@pageno", SqlDbType.Int);
                    arParms[8].Value = objbill.CurrentIndex;

                    arParms[9] = new SqlParameter("@AmountEnable", SqlDbType.Int);
                    arParms[9].Value = objbill.AmountEnable;

                    arParms[10] = new SqlParameter("@RunnerID", SqlDbType.Int);
                    arParms[10].Value = objbill.RunnerID;


                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetLabBillList", arParms);
                    List<LabBillingData> listpatientdetails = ORHelper<LabBillingData>.FromDataReaderToList(sqlReader);
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
        public List<LabBillingData> GetLabPaymentList(LabBillingData objbill)
        {
            List<LabBillingData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[7];

                    arParms[0] = new SqlParameter("@BillNo", SqlDbType.VarChar);
                    arParms[0].Value = objbill.BillNo;

                    arParms[1] = new SqlParameter("@PatientName", SqlDbType.VarChar);
                    arParms[1].Value = objbill.PatientName;

                    arParms[2] = new SqlParameter("@Paymode", SqlDbType.Int);
                    arParms[2].Value = objbill.Paymode;

                    arParms[3] = new SqlParameter("@DateFrom", SqlDbType.DateTime);
                    arParms[3].Value = objbill.DateFrom;

                    arParms[4] = new SqlParameter("@DateTo", SqlDbType.DateTime);
                    arParms[4].Value = objbill.DateTo;

                    arParms[5] = new SqlParameter("@CollectedByID", SqlDbType.BigInt);
                    arParms[5].Value = objbill.CollectedByID;


                    arParms[6] = new SqlParameter("@AmountEnable", SqlDbType.Int);
                    arParms[6].Value = objbill.AmountEnable;


                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetLabPaymentList", arParms);
                    List<LabBillingData> listpatientdetails = ORHelper<LabBillingData>.FromDataReaderToList(sqlReader);
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
        public List<LabBillingData> GetLabBillListReport(LabBillingData objbill)
        {
            List<LabBillingData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[4];

                    arParms[0] = new SqlParameter("@Paymode", SqlDbType.Int);
                    arParms[0].Value = objbill.Paymode;

                    arParms[1] = new SqlParameter("@DateFrom", SqlDbType.DateTime);
                    arParms[1].Value = objbill.DateFrom;

                    arParms[2] = new SqlParameter("@DateTo", SqlDbType.DateTime);
                    arParms[2].Value = objbill.DateTo;


                    arParms[3] = new SqlParameter("@AmountEnable", SqlDbType.Int);
                    arParms[3].Value = objbill.AmountEnable;


                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetLabBillListReport", arParms);
                    List<LabBillingData> listpatientdetails = ORHelper<LabBillingData>.FromDataReaderToList(sqlReader);
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
        public List<LabBillingData> GetPatientLabBillList(LabBillingData objbill)
        {
            List<LabBillingData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[9];

                    arParms[0] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[0].Value = objbill.UHID;

                    arParms[1] = new SqlParameter("@PatientName", SqlDbType.VarChar);
                    arParms[1].Value = objbill.PatientName;

                    arParms[2] = new SqlParameter("@Paymode", SqlDbType.Int);
                    arParms[2].Value = objbill.Paymode;

                    arParms[3] = new SqlParameter("@DateFrom", SqlDbType.DateTime);
                    arParms[3].Value = objbill.DateFrom;

                    arParms[4] = new SqlParameter("@DateTo", SqlDbType.DateTime);
                    arParms[4].Value = objbill.DateTo;

                    arParms[5] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[5].Value = objbill.IsActive;

                    arParms[6] = new SqlParameter("@CollectedByID", SqlDbType.BigInt);
                    arParms[6].Value = objbill.CollectedByID;

                    arParms[7] = new SqlParameter("@InvestigationNo", SqlDbType.VarChar);
                    arParms[7].Value = objbill.InvestigationNo;

                    arParms[8] = new SqlParameter("@RunnerID", SqlDbType.Int);
                    arParms[8].Value = objbill.RunnerID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetPatientLabBillList", arParms);
                    List<LabBillingData> listpatientdetails = ORHelper<LabBillingData>.FromDataReaderToList(sqlReader);
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
        public int DeleteOPDBillByID(OPDbillingData objbill)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[10];

                    arParms[0] = new SqlParameter("@BillNo", SqlDbType.VarChar);
                    arParms[0].Value = objbill.BillNo;

                    arParms[1] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[1].Direction = ParameterDirection.Output;

                    arParms[2] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    arParms[2].Value = objbill.Remarks;

                    arParms[3] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[3].Value = objbill.EmployeeID;

                    arParms[4] = new SqlParameter("@Amount", SqlDbType.Money);
                    arParms[4].Value = objbill.Amount;

                    arParms[5] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[5].Value = objbill.UHID;

                    arParms[6] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[6].Value = objbill.HospitalID;

                    arParms[7] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                    arParms[7].Value = objbill.IPaddress;

                    arParms[8] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[8].Value = objbill.FinancialYearID;

                    arParms[9] = new SqlParameter("@AdjustedAmount", SqlDbType.Money);
                    arParms[9].Value = objbill.AdjustedAmount;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_DeleteOPDBillbyID", arParms);
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
        public int ChecDoctorAvailability(OPdoctoravailable objdoc)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[3];

                    arParms[0] = new SqlParameter("@DoctorID", SqlDbType.BigInt);
                    arParms[0].Value = objdoc.DoctorID;

                    arParms[1] = new SqlParameter("@DepartmentID", SqlDbType.Int);
                    arParms[1].Value = objdoc.DepartmentID;

                    arParms[2] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[2].Direction = ParameterDirection.Output;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_CMS_GetOPD_DoctorAvailibility", arParms);
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
        public int DeleteIPDBillByID(FinalBillData objbill)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[10];

                    arParms[0] = new SqlParameter("@BillNo", SqlDbType.VarChar);
                    arParms[0].Value = objbill.BillNo;

                    arParms[1] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[1].Direction = ParameterDirection.Output;

                    arParms[2] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    arParms[2].Value = objbill.Remarks;

                    arParms[3] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[3].Value = objbill.EmployeeID;

                    arParms[4] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[4].Value = objbill.UHID;

                    arParms[5] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[5].Value = objbill.HospitalID;

                    arParms[6] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                    arParms[6].Value = objbill.IPaddress;

                    arParms[7] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[7].Value = objbill.FinancialYearID;

                    arParms[8] = new SqlParameter("@AdjustedAmount", SqlDbType.Money);
                    arParms[8].Value = objbill.AdjustedAmount;

                    arParms[9] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                    arParms[9].Value = objbill.IPNo;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_DeleteIPDBillbyID", arParms);
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
        public int DeleteOPDLabBillByID(LabBillingData objbill)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[10];

                    arParms[0] = new SqlParameter("@BillNo", SqlDbType.VarChar);
                    arParms[0].Value = objbill.BillNo;

                    arParms[1] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[1].Direction = ParameterDirection.Output;

                    arParms[2] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    arParms[2].Value = objbill.Remarks;

                    arParms[3] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[3].Value = objbill.EmployeeID;

                    arParms[4] = new SqlParameter("@Amount", SqlDbType.Money);
                    arParms[4].Value = objbill.Amount;

                    arParms[5] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[5].Value = objbill.UHID;

                    arParms[6] = new SqlParameter("@HospitalID", SqlDbType.BigInt);
                    arParms[6].Value = objbill.HospitalID;

                    arParms[7] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                    arParms[7].Value = objbill.IPaddress;

                    arParms[8] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[8].Value = objbill.FinancialYearID;

                    arParms[9] = new SqlParameter("@AdjustedAmount", SqlDbType.Money);
                    arParms[9].Value = objbill.AdjustedAmount;


                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_DeleteOPDLabBillbyID", arParms);
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
        public List<PHRbillingData> GetOPServiceChargeByID(PHRbillingData objbill)
        {
            List<PHRbillingData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.BigInt);
                    arParms[0].Value = objbill.StockID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Get_OPD_Item_PriceList", arParms);
                    List<PHRbillingData> chargelist = ORHelper<PHRbillingData>.FromDataReaderToList(sqlReader);
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
        public int UpdateOPDPhrBill(PHRbillingData objbill)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[27];

                    arParms[0] = new SqlParameter("@XMLData", SqlDbType.Xml);
                    arParms[0].Value = objbill.XMLData;

                    arParms[1] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[1].Value = objbill.UHID;

                    arParms[2] = new SqlParameter("@ID", SqlDbType.BigInt);
                    arParms[2].Value = objbill.ID;

                    arParms[3] = new SqlParameter("@TotalBillAmount", SqlDbType.Money);
                    arParms[3].Value = objbill.TotalBillAmount;

                    arParms[4] = new SqlParameter("@AdjustedAmount", SqlDbType.Money);
                    arParms[4].Value = objbill.AdjustedAmount;

                    arParms[5] = new SqlParameter("@DiscountedAmount", SqlDbType.Money);
                    arParms[5].Value = objbill.DiscountedAmount;

                    arParms[6] = new SqlParameter("@PaidAmount", SqlDbType.Money);
                    arParms[6].Value = objbill.PaidAmount;

                    arParms[7] = new SqlParameter("@PaymentMode", SqlDbType.Int);
                    arParms[7].Value = objbill.PaymentMode;

                    arParms[8] = new SqlParameter("@BankName", SqlDbType.VarChar);
                    arParms[8].Value = objbill.BankName;

                    arParms[9] = new SqlParameter("@AccountNo", SqlDbType.VarChar);
                    arParms[9].Value = objbill.AccountNo;

                    arParms[10] = new SqlParameter("@CardNo", SqlDbType.VarChar);
                    arParms[10].Value = objbill.CardNo;

                    arParms[11] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[11].Value = objbill.HospitalID;

                    arParms[12] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[12].Value = objbill.FinancialYearID;

                    arParms[13] = new SqlParameter("@AddedBy", SqlDbType.BigInt);
                    arParms[13].Value = objbill.EmployeeID;

                    arParms[14] = new SqlParameter("@Output", SqlDbType.BigInt);
                    arParms[14].Direction = ParameterDirection.Output;

                    arParms[15] = new SqlParameter("@DeptID", SqlDbType.BigInt);
                    arParms[15].Value = objbill.DeptID;

                    arParms[16] = new SqlParameter("@DocID", SqlDbType.BigInt);
                    arParms[16].Value = objbill.DocID;

                    arParms[17] = new SqlParameter("@PatientType", SqlDbType.BigInt);
                    arParms[17].Value = objbill.PatientType;

                    arParms[18] = new SqlParameter("@ReferredDoctorID", SqlDbType.Int);
                    arParms[18].Value = objbill.ReferredDoctorID;

                    arParms[19] = new SqlParameter("@PatientName", SqlDbType.VarChar);
                    arParms[19].Value = objbill.PatientName;

                    arParms[20] = new SqlParameter("@PatientAddress", SqlDbType.VarChar);
                    arParms[20].Value = objbill.PatientAddress;

                    arParms[21] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                    arParms[21].Value = objbill.IPaddress;

                    arParms[22] = new SqlParameter("@ContactNo", SqlDbType.VarChar);
                    arParms[22].Value = objbill.ContactNo;

                    arParms[23] = new SqlParameter("@DiscountByID", SqlDbType.BigInt);
                    arParms[23].Value = objbill.DiscountByID;

                    arParms[24] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    arParms[24].Value = objbill.Remarks;

                    arParms[25] = new SqlParameter("@TotalBillTax", SqlDbType.Money);
                    arParms[25].Value = objbill.TotalBillTax;

                    arParms[26] = new SqlParameter("@custommertype", SqlDbType.Int);
                    arParms[26].Value = objbill.custommertype;


                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Update_OPDPhrBillDetails", arParms);
                    if (result_ > 0 || result_ == -1)
                        result = Convert.ToInt32(arParms[14].Value);
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
        public List<PHRbillingData> GetOPDPhrBillList(PHRbillingData objbill)
        {
            List<PHRbillingData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[8];

                    arParms[0] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[0].Value = objbill.UHID;

                    arParms[1] = new SqlParameter("@PatientName", SqlDbType.VarChar);
                    arParms[1].Value = objbill.PatientName;

                    arParms[2] = new SqlParameter("@Paymode", SqlDbType.Int);
                    arParms[2].Value = objbill.Paymode;

                    arParms[3] = new SqlParameter("@DateFrom", SqlDbType.DateTime);
                    arParms[3].Value = objbill.DateFrom;

                    arParms[4] = new SqlParameter("@DateTo", SqlDbType.DateTime);
                    arParms[4].Value = objbill.DateTo;

                    arParms[5] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[5].Value = objbill.IsActive;

                    arParms[6] = new SqlParameter("@CollectedBy", SqlDbType.BigInt);
                    arParms[6].Value = objbill.CollectedByID;

                    arParms[7] = new SqlParameter("@Customertype", SqlDbType.Int);
                    arParms[7].Value = objbill.custommertype;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetOPDPhrBillList", arParms);
                    List<PHRbillingData> listpatientdetails = ORHelper<PHRbillingData>.FromDataReaderToList(sqlReader);
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
        public int DeleteOPDPhrBillByID(PHRbillingData objbill)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[9];

                    arParms[0] = new SqlParameter("@BillNo", SqlDbType.VarChar);
                    arParms[0].Value = objbill.BillNo;

                    arParms[1] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[1].Direction = ParameterDirection.Output;

                    arParms[2] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    arParms[2].Value = objbill.Remarks;

                    arParms[3] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[3].Value = objbill.EmployeeID;

                    arParms[4] = new SqlParameter("@AdjustedAmount", SqlDbType.Money);
                    arParms[4].Value = objbill.Amount;

                    arParms[5] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[5].Value = objbill.UHID;

                    arParms[6] = new SqlParameter("@HospitalID", SqlDbType.BigInt);
                    arParms[6].Value = objbill.HospitalID;

                    arParms[7] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                    arParms[7].Value = objbill.IPaddress;

                    arParms[8] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[8].Value = objbill.FinancialYearID;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_DeleteOPDPhrBillbyID", arParms);
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
        public List<LabBillingData> GetPharmacyItemChargeByID(LabBillingData objbill)
        {
            List<LabBillingData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.BigInt);
                    arParms[0].Value = objbill.ID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Get_IPD_Item_PriceList", arParms);
                    List<LabBillingData> chargelist = ORHelper<LabBillingData>.FromDataReaderToList(sqlReader);
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
        public List<LabBillingData> GetLabServiceList(LabBillingData objbill)
        {
            List<LabBillingData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@BillNo", SqlDbType.VarChar);
                    arParms[0].Value = objbill.BillNo;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetLabServiceList", arParms);
                    List<LabBillingData> listpatientdetails = ORHelper<LabBillingData>.FromDataReaderToList(sqlReader);
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
        public List<OPDbillingData> GetOpservicesbypatientvisitcount(OPDbillingData objbill)
        {
            List<OPDbillingData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[6];


                    arParms[0] = new SqlParameter("@DoctorID", SqlDbType.BigInt);
                    arParms[0].Value = objbill.DocID;

                    arParms[1] = new SqlParameter("@DepartmentID", SqlDbType.Int);
                    arParms[1].Value = objbill.DeptID;

                    arParms[2] = new SqlParameter("@NoDays", SqlDbType.Int);
                    arParms[2].Value = objbill.NoDays;

                    arParms[3] = new SqlParameter("@PrintregdCard", SqlDbType.Int);
                    arParms[3].Value = objbill.PrintregdCard;

                    arParms[4] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[4].Value = objbill.UHID;

                    arParms[5] = new SqlParameter("@ServiceTypeID", SqlDbType.Int);
                    arParms[5].Value = objbill.ServiceTypeID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_OP_servicesbylastvisitdetails", arParms);
                    List<OPDbillingData> listpatientdetails = ORHelper<OPDbillingData>.FromDataReaderToList(sqlReader);
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
        public List<LabBillingData> GetPackageservices(LabBillingData objbill)
        {
            List<LabBillingData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@PackageID", SqlDbType.Int);
                    arParms[0].Value = objbill.ID;

                    arParms[1] = new SqlParameter("@CompanyID", SqlDbType.Int);
                    arParms[1].Value = objbill.CompanyID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Get_LabPackageservices", arParms);
                    List<LabBillingData> listpatientdetails = ORHelper<LabBillingData>.FromDataReaderToList(sqlReader);
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
        public List<LabBillingData> UpdateLabServiceCancelRecord(LabBillingData objbill)
        {
            List<LabBillingData> result;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[8];

                    arParms[0] = new SqlParameter("@XMLData", SqlDbType.Xml);
                    arParms[0].Value = objbill.XMLData;

                    arParms[1] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[1].Value = objbill.UHID;

                    arParms[2] = new SqlParameter("@TotalRefundable", SqlDbType.Money);
                    arParms[2].Value = objbill.TotalRefundable;

                    arParms[3] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[3].Value = objbill.HospitalID;

                    arParms[4] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[4].Value = objbill.FinancialYearID;

                    arParms[5] = new SqlParameter("@AddedBy", SqlDbType.BigInt);
                    arParms[5].Value = objbill.EmployeeID;

                    arParms[6] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    arParms[6].Value = objbill.Remarks;

                    arParms[7] = new SqlParameter("@BillNo", SqlDbType.VarChar);
                    arParms[7].Value = objbill.BillNo;

                    SqlDataReader sqlReader = null;

                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Update_LabServiceAdjustmentDetails", arParms);
                    List<LabBillingData> AdjustmentList = ORHelper<LabBillingData>.FromDataReaderToList(sqlReader);
                    result = AdjustmentList;
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
        public List<LabBillingData> GetAdjustmentList(LabBillingData objbill)
        {
            List<LabBillingData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[6];

                    arParms[0] = new SqlParameter("@AdjustmentNo", SqlDbType.VarChar);
                    arParms[0].Value = objbill.AdjustmentNo;

                    arParms[1] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[1].Value = objbill.UHID;

                    arParms[2] = new SqlParameter("@PatientName", SqlDbType.VarChar);
                    arParms[2].Value = objbill.PatientName;

                    arParms[3] = new SqlParameter("@DateFrom", SqlDbType.DateTime);
                    arParms[3].Value = objbill.DateFrom;

                    arParms[4] = new SqlParameter("@DateTo", SqlDbType.DateTime);
                    arParms[4].Value = objbill.DateTo;

                    arParms[5] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[5].Value = objbill.IsActive;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetAdjustmentList", arParms);
                    List<LabBillingData> listpatientdetails = ORHelper<LabBillingData>.FromDataReaderToList(sqlReader);
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
        public int DeleteAdjustmentByID(LabBillingData objbill)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[10];

                    arParms[0] = new SqlParameter("@AdjustmentNo", SqlDbType.VarChar);
                    arParms[0].Value = objbill.BillNo;

                    arParms[1] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[1].Direction = ParameterDirection.Output;

                    arParms[2] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    arParms[2].Value = objbill.Remarks;

                    arParms[3] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[3].Value = objbill.EmployeeID;

                    arParms[4] = new SqlParameter("@Amount", SqlDbType.Money);
                    arParms[4].Value = objbill.NetLabServiceCharge;

                    arParms[5] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[5].Value = objbill.UHID;

                    arParms[6] = new SqlParameter("@ID", SqlDbType.BigInt);
                    arParms[6].Value = objbill.ID;

                    arParms[7] = new SqlParameter("@HospitalID", SqlDbType.BigInt);
                    arParms[7].Value = objbill.HospitalID;

                    arParms[8] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
                    arParms[8].Value = objbill.IPaddress;

                    arParms[9] = new SqlParameter("@LabServiceID", SqlDbType.Int);
                    arParms[9].Value = objbill.LabServiceID;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_DeleteDeleteAdjustmentByID", arParms);
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
        public List<LabBillingData> GetDiscountServiceByID(Int64 ID)
        {
            List<LabBillingData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@BillID", SqlDbType.BigInt);
                    arParms[0].Value = ID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Get_OP_INV_discount_service", arParms);
                    List<LabBillingData> chargelist = ORHelper<LabBillingData>.FromDataReaderToList(sqlReader);
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
        public List<OPDbillingData> GetDiscountOpServiceByID(Int64 ID)
        {
            List<OPDbillingData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@BillID", SqlDbType.BigInt);
                    arParms[0].Value = ID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Get_OP_discount_service", arParms);
                    List<OPDbillingData> chargelist = ORHelper<OPDbillingData>.FromDataReaderToList(sqlReader);
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
        public List<PatientQRdata> GetPatientQRData(Int64 UHID)
        {
            List<PatientQRdata> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[0].Value = UHID;


                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Get_PatientDetails_QR", arParms);
                    List<PatientQRdata> patintdetails = ORHelper<PatientQRdata>.FromDataReaderToList(sqlReader);
                    result = patintdetails;
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
        public int checkMsbDoctor(Int64 doctorID)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@DoctorID", SqlDbType.BigInt);
                    arParms[0].Value = doctorID;

                    arParms[1] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[1].Direction = ParameterDirection.Output;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_Check_msb_doctor", arParms);
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
        public List<LabBillingData> GetBillDetailsByBillNo(LabBillingData objbill)
        {
            List<LabBillingData> result = null;
            try
            {
                SqlParameter[] arParms = new SqlParameter[2];

                arParms[0] = new SqlParameter("@PatientType", SqlDbType.Int);
                arParms[0].Value = objbill.PatientType;

                arParms[1] = new SqlParameter("@BillNo", SqlDbType.VarChar);
                arParms[1].Value = objbill.BillNo;

                SqlDataReader sqlReader = null;
                sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Get_BillDetails", arParms);
                List<LabBillingData> chargelist = ORHelper<LabBillingData>.FromDataReaderToList(sqlReader);
                result = chargelist;

            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.DataAccessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
                throw new DataAccessException("5000001", ex);
            }
            return result;
        }
        public List<LabBillingData> UpdateReferalByBillNo(LabBillingData objbill)
        {
            List<LabBillingData> result = null;
            try
            {
                SqlParameter[] arParms = new SqlParameter[4];

                arParms[0] = new SqlParameter("@PatientType", SqlDbType.Int);
                arParms[0].Value = objbill.PatientType;

                arParms[1] = new SqlParameter("@BillNo", SqlDbType.VarChar);
                arParms[1].Value = objbill.BillNo;

                arParms[2] = new SqlParameter("@SourceID", SqlDbType.Int);
                arParms[2].Value = objbill.SourceID;

                arParms[3] = new SqlParameter("@ReferalID", SqlDbType.Int);
                arParms[3].Value = objbill.ReferalID;

                SqlDataReader sqlReader = null;
                sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Get_UpdateReferalByBillNo", arParms);
                List<LabBillingData> chargelist = ORHelper<LabBillingData>.FromDataReaderToList(sqlReader);
                result = chargelist;

            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.DataAccessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
                throw new DataAccessException("5000001", ex);
            }
            return result;
        }
        public int DeleteReferralPamentByVoucherNo(DoctorPayoutData objbill)
        {
            int result = 0;
            try
            {
                SqlParameter[] arParms = new SqlParameter[4];

                arParms[0] = new SqlParameter("@PaymentNumber", SqlDbType.VarChar);
                arParms[0].Value = objbill.PaymentNumber;

                arParms[1] = new SqlParameter("@DoctorID", SqlDbType.Int);
                arParms[1].Value = objbill.DoctorID;

                arParms[2] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                arParms[2].Value = objbill.EmployeeID;

                arParms[3] = new SqlParameter("@Output", SqlDbType.SmallInt);
                arParms[3].Direction = ParameterDirection.Output;

                int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_DeleteReferralPaymentByPNo", arParms);
                if (result_ > 0 || result_ == -1)
                    result = Convert.ToInt32(arParms[3].Value);

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

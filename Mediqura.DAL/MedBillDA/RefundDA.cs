using Mediqura.CommonData.MedBillData;
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
    public class RefundDA
    {
        public List<RefundData> UpdateRefundDetails(RefundData objbill)
        {
            List<RefundData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[11];

                    arParms[0] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[0].Value = objbill.UHID;

                    arParms[1] = new SqlParameter("@Paymode", SqlDbType.Int);
                    arParms[1].Value = objbill.Paymode;

                    arParms[2] = new SqlParameter("@BankName", SqlDbType.VarChar);
                    arParms[2].Value = objbill.BankName;

                    arParms[3] = new SqlParameter("@RefundAmount", SqlDbType.Money);
                    arParms[3].Value = objbill.RefundAmount;

                    arParms[4] = new SqlParameter("@DueAmount", SqlDbType.Money);
                    arParms[4].Value = objbill.DueAmount;

                    arParms[5] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[5].Value = objbill.HospitalID;

                    arParms[6] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[6].Value = objbill.FinancialYearID;

                    arParms[7] = new SqlParameter("@AddedBy", SqlDbType.BigInt);
                    arParms[7].Value = objbill.EmployeeID;

                    arParms[8] = new SqlParameter("@Cheque", SqlDbType.VarChar);
                    arParms[8].Value = objbill.Cheque;

                    arParms[9] = new SqlParameter("@LastRefundedAmount", SqlDbType.Money);
                    arParms[9].Value = objbill.LastRefundedAmount;

                    arParms[10] = new SqlParameter("@BillNo", SqlDbType.VarChar);
                    arParms[10].Value = objbill.BillNo;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Update_PatientRefundDetails", arParms);
                    List<RefundData> listpatientdetails = ORHelper<RefundData>.FromDataReaderToList(sqlReader);
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
        public List<RefundData> UpdatePhrRefundDetails(RefundData objbill)
        {
            List<RefundData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[11];

                    arParms[0] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[0].Value = objbill.UHID;

                    arParms[1] = new SqlParameter("@Paymode", SqlDbType.Int);
                    arParms[1].Value = objbill.Paymode;

                    arParms[2] = new SqlParameter("@BankName", SqlDbType.VarChar);
                    arParms[2].Value = objbill.BankName;

                    arParms[3] = new SqlParameter("@RefundAmount", SqlDbType.Money);
                    arParms[3].Value = objbill.RefundAmount;

                    arParms[4] = new SqlParameter("@DueAmount", SqlDbType.Money);
                    arParms[4].Value = objbill.DueAmount;

                    arParms[5] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[5].Value = objbill.HospitalID;

                    arParms[6] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[6].Value = objbill.FinancialYearID;

                    arParms[7] = new SqlParameter("@AddedBy", SqlDbType.BigInt);
                    arParms[7].Value = objbill.EmployeeID;

                    arParms[8] = new SqlParameter("@Cheque", SqlDbType.VarChar);
                    arParms[8].Value = objbill.Cheque;

                    arParms[9] = new SqlParameter("@LastRefundedAmount", SqlDbType.Money);
                    arParms[9].Value = objbill.LastRefundedAmount;

                    arParms[10] = new SqlParameter("@BillNo", SqlDbType.VarChar);
                    arParms[10].Value = objbill.BillNo;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_PHR_Update_PatientRefundDetails", arParms);
                    List<RefundData> listpatientdetails = ORHelper<RefundData>.FromDataReaderToList(sqlReader);
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
        public List<RefundData> GetPhrRefundList(RefundData objbill)
        {
            List<RefundData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[5];

                    arParms[0] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[0].Value = objbill.UHID;

                    arParms[1] = new SqlParameter("@Paymode", SqlDbType.Int);
                    arParms[1].Value = objbill.Paymode;

                    arParms[2] = new SqlParameter("@DateFrom", SqlDbType.DateTime);
                    arParms[2].Value = objbill.DateFrom;

                    arParms[3] = new SqlParameter("@DateTo", SqlDbType.DateTime);
                    arParms[3].Value = objbill.DateTo;

                    arParms[4] = new SqlParameter("@IsActive", SqlDbType.Bit);
                    arParms[4].Value = objbill.IsActive;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_PHR_GetRefundList", arParms);
                    List<RefundData> listpatientdetails = ORHelper<RefundData>.FromDataReaderToList(sqlReader);
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

        public List<RefundData> GetRefundList(RefundData objbill)
        {
            List<RefundData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[6];

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

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetRefundList", arParms);
                    List<RefundData> listpatientdetails = ORHelper<RefundData>.FromDataReaderToList(sqlReader);
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
        public int DeleteRefundByID(RefundData objbill)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[7];

                    arParms[0] = new SqlParameter("@RefundNo", SqlDbType.VarChar);
                    arParms[0].Value = objbill.RefundNo;

                    arParms[1] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[1].Direction = ParameterDirection.Output;

                    arParms[2] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    arParms[2].Value = objbill.Remarks;

                    arParms[3] = new SqlParameter("@UserId", SqlDbType.BigInt);
                    arParms[3].Value = objbill.EmployeeID;

                    arParms[4] = new SqlParameter("@RefundAmount", SqlDbType.Money);
                    arParms[4].Value = objbill.RefundAmount;

                    arParms[5] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[5].Value = objbill.UHID;

                    arParms[6] = new SqlParameter("@LastRefundedAmount", SqlDbType.Money);
                    arParms[6].Value = objbill.LastRefundedAmount;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_DeletePatientRefudnbyID", arParms);
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

        public int DeletePHRRefundByID(RefundData objbill)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[7];

                    arParms[0] = new SqlParameter("@RefundNo", SqlDbType.VarChar);
                    arParms[0].Value = objbill.RefundNo;

                    arParms[1] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[1].Direction = ParameterDirection.Output;

                    arParms[2] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                    arParms[2].Value = objbill.Remarks;

                    arParms[3] = new SqlParameter("@UserId", SqlDbType.BigInt);
                    arParms[3].Value = objbill.EmployeeID;

                    arParms[4] = new SqlParameter("@RefundAmount", SqlDbType.Money);
                    arParms[4].Value = objbill.RefundAmount;

                    arParms[5] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[5].Value = objbill.UHID;

                    arParms[6] = new SqlParameter("@LastRefundedAmount", SqlDbType.Money);
                    arParms[6].Value = objbill.LastRefundedAmount;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_PHR_DeletePatientRefudnbyID", arParms);
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
        public List<DiscountRefundData> GetDiscountRefundList(DiscountRefundData objbill)
        {
            List<DiscountRefundData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[6];

                    arParms[0] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[0].Value = objbill.UHID;

                    arParms[1] = new SqlParameter("@PatientName", SqlDbType.VarChar);
                    arParms[1].Value = objbill.PatientName;

                    arParms[2] = new SqlParameter("@BillNo", SqlDbType.VarChar);
                    arParms[2].Value = objbill.BillNo;

                    arParms[3] = new SqlParameter("@DateFrom", SqlDbType.DateTime);
                    arParms[3].Value = objbill.DateFrom;

                    arParms[4] = new SqlParameter("@DateTo", SqlDbType.DateTime);
                    arParms[4].Value = objbill.DateTo;

                    arParms[5] = new SqlParameter("@PatientType", SqlDbType.Int);
                    arParms[5].Value = objbill.PatientType;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetDiscountRefundList", arParms);
                    List<DiscountRefundData> listpatientdetails = ORHelper<DiscountRefundData>.FromDataReaderToList(sqlReader);
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
        public List<DiscountRefundData> UpdateDiscountRefundDetails(DiscountRefundData objData)
        {
            List<DiscountRefundData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[9];

                    arParms[0] = new SqlParameter("@UHID", SqlDbType.BigInt);
                    arParms[0].Value = objData.UHID;

                    arParms[1] = new SqlParameter("@PatientName", SqlDbType.VarChar);
                    arParms[1].Value = objData.PatientName;

                    arParms[2] = new SqlParameter("@BillNo", SqlDbType.VarChar);
                    arParms[2].Value = objData.BillNo;

                    arParms[3] = new SqlParameter("@PatientType", SqlDbType.Int);
                    arParms[3].Value = objData.PatientType;

                    arParms[4] = new SqlParameter("@RefundAmount", SqlDbType.Money);
                    arParms[4].Value = objData.RefundAmount;

                    arParms[5] = new SqlParameter("@TotalAmount", SqlDbType.Money);
                    arParms[5].Value = objData.TotalAmount;

                    arParms[6] = new SqlParameter("@AddedBy", SqlDbType.BigInt);
                    arParms[6].Value = objData.EmployeeID;

                    arParms[7] = new SqlParameter("@FinancialID", SqlDbType.Int);
                    arParms[7].Value = objData.FinancialYearID;

                    arParms[8] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[8].Value = objData.HospitalID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Update_DiscountRefund", arParms);
                    List<DiscountRefundData> listpatientdetails = ORHelper<DiscountRefundData>.FromDataReaderToList(sqlReader);
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

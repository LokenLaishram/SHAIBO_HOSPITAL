using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mediqura.CommonData.MedPharData;
using System.Data.SqlClient;
using System.Data;
using Mediqura.Utility;
using Mediqura.Utility.Util;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using Mediqura.CommonData.MedBillData;

namespace Mediqura.DAL.MedPharDA
{
	public class VendorPaymentDA
	{
		public List<VendorPaymentData> GetVendorPurchaseDetails(VendorPaymentData objdata)
		{
			List<VendorPaymentData> result = null;
			try
			{
				{
					SqlParameter[] arParms = new SqlParameter[3];

					arParms[0] = new SqlParameter("@DateFrom", SqlDbType.DateTime);
					arParms[0].Value = objdata.DateFrom;

					arParms[1] = new SqlParameter("@DateTo", SqlDbType.DateTime);
					arParms[1].Value = objdata.DateTo;

					arParms[2] = new SqlParameter("@SupplierID", SqlDbType.Int);
					arParms[2].Value = objdata.SupplierID;

					SqlDataReader sqlReader = null;
					sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_PHR_GetVendorPurchaseDetails", arParms);
					List<VendorPaymentData> VendorPaymentDetails = ORHelper<VendorPaymentData>.FromDataReaderToList(sqlReader);
					result = VendorPaymentDetails;
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
		
		public List<VendorPaymentData> GetVendorPaymentDetails(VendorPaymentData objdata)
		{
			List<VendorPaymentData> result = null;
			try
			{
				{
					SqlParameter[] arParms = new SqlParameter[4];

					arParms[0] = new SqlParameter("@SupplierID", SqlDbType.Int);
					arParms[0].Value = objdata.SupplierID;

					arParms[1] = new SqlParameter("@PaymentStatus", SqlDbType.Int);
					arParms[1].Value = objdata.PaymentStatus;
					
					arParms[2] = new SqlParameter("@DateFrom", SqlDbType.DateTime);
					arParms[2].Value = objdata.DateFrom;

					arParms[3] = new SqlParameter("@DateTo", SqlDbType.DateTime);
					arParms[3].Value = objdata.DateTo;

					SqlDataReader sqlReader = null;
					sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_PHR_GetVendorPaymentDetails", arParms);
					List<VendorPaymentData> VendorPaymentDetails = ORHelper<VendorPaymentData>.FromDataReaderToList(sqlReader);
					result = VendorPaymentDetails;
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

		public List<VendorPaymentData> GetVendorPaymentbySupplierID(VendorPaymentData objdata)
		{
			List<VendorPaymentData> result = null;
			try
			{
				{
					SqlParameter[] arParms = new SqlParameter[4];

					arParms[0] = new SqlParameter("@SupplierID", SqlDbType.Int);
					arParms[0].Value = objdata.SupplierID;

					arParms[1] = new SqlParameter("@PaymentStatus", SqlDbType.Int);
					arParms[1].Value = objdata.PaymentStatus;

					arParms[2] = new SqlParameter("@DateFrom", SqlDbType.DateTime);
					arParms[2].Value = objdata.DateFrom;

					arParms[3] = new SqlParameter("@DateTo", SqlDbType.DateTime);
					arParms[3].Value = objdata.DateTo;

					SqlDataReader sqlReader = null;
					sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_PHR_GetVendorPaymentbySupplierID", arParms);
					List<VendorPaymentData> PurchaseRequisitionList = ORHelper<VendorPaymentData>.FromDataReaderToList(sqlReader);
					result = PurchaseRequisitionList;
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

		public string UpdateVendorPaymentDetails(VendorPaymentData objdata)
		{
			string result = "";
			try
			{
				{
					SqlParameter[] arParms = new SqlParameter[16];

					arParms[0] = new SqlParameter("@XMLData", SqlDbType.Xml);
					arParms[0].Value = objdata.XMLData;

					arParms[1] = new SqlParameter("@PaidBy", SqlDbType.BigInt);
					arParms[1].Value = objdata.EmployeeID;

					arParms[2] = new SqlParameter("@Output", SqlDbType.VarChar, 50);
					arParms[2].Direction = ParameterDirection.Output;

					arParms[3] = new SqlParameter("@ActionType", SqlDbType.Int);
					arParms[3].Value = objdata.ActionType;

					arParms[4] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
					arParms[4].Value = objdata.FinancialYearID;

					arParms[5] = new SqlParameter("@PayableAmount", SqlDbType.Decimal);
					arParms[5].Value = objdata.PayableAmount;

					arParms[6] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
					arParms[6].Value = objdata.IPaddress;

					arParms[7] = new SqlParameter("@TotalPaidAmount", SqlDbType.Decimal);
					arParms[7].Value = objdata.TotalPaidAmount;

					arParms[8] = new SqlParameter("@Remark", SqlDbType.VarChar);
					arParms[8].Value = objdata.Remark;

					arParms[9] = new SqlParameter("@TotalDueAmount", SqlDbType.Decimal);
					arParms[9].Value = objdata.TotalDueAmount;

					arParms[10] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
					arParms[10].Value = objdata.EmployeeID;

					arParms[11] = new SqlParameter("@Paymode", SqlDbType.Int);
					arParms[11].Value = objdata.Paymode;

					arParms[12] = new SqlParameter("@BankName", SqlDbType.VarChar);
					arParms[12].Value = objdata.BankName;

					arParms[13] = new SqlParameter("@ChequeUTRnumber", SqlDbType.VarChar);
					arParms[13].Value = objdata.ChequeUTRnumber;

					arParms[14] = new SqlParameter("@InvoiceNumber", SqlDbType.VarChar);
					arParms[14].Value = objdata.InvoiceNumber;

					arParms[15] = new SqlParameter("@SupplierID", SqlDbType.Int);
					arParms[15].Value = objdata.SupplierID;

					int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_PHR_UpdateVendorPaymentDetails", arParms);
					if (result_ > 0 || result_ == -1)
						result = Convert.ToString(arParms[2].Value);
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

		public List<VendorPaymentData> GetVendorPaymentList(VendorPaymentData objdata)
		{
			List<VendorPaymentData> result = null;
			try
			{
				{
					SqlParameter[] arParms = new SqlParameter[6];

					arParms[0] = new SqlParameter("@IsActive", SqlDbType.Bit);
					arParms[0].Value = objdata.IsActive;

					arParms[1] = new SqlParameter("@PaymentNo", SqlDbType.VarChar);
					arParms[1].Value = objdata.PaymentNo;

					arParms[2] = new SqlParameter("@DateFrom", SqlDbType.DateTime);
					arParms[2].Value = objdata.DateFrom;

					arParms[3] = new SqlParameter("@DateTo", SqlDbType.DateTime);
					arParms[3].Value = objdata.DateTo;

					arParms[4] = new SqlParameter("@Paymode", SqlDbType.Int);
					arParms[4].Value = objdata.Paymode;

					arParms[5] = new SqlParameter("@SupplierID", SqlDbType.Int);
					arParms[5].Value = objdata.SupplierID;

					SqlDataReader sqlReader = null;
					sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_PHR_GetVendorPaymentList", arParms);
					List<VendorPaymentData> PurchaseRequisitionList = ORHelper<VendorPaymentData>.FromDataReaderToList(sqlReader);
					result = PurchaseRequisitionList;
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

		public List<VendorPaymentData> SearchChildvendorpaymentDetails(VendorPaymentData objdata)
		{
			List<VendorPaymentData> result = null;
			try
			{
				{
					SqlParameter[] arParms = new SqlParameter[2];

					arParms[0] = new SqlParameter("@SupplierID", SqlDbType.Int);
					arParms[0].Value = objdata.SupplierID;

					arParms[1] = new SqlParameter("@ReceiptNo", SqlDbType.VarChar);
					arParms[1].Value = objdata.ReceiptNo;

					SqlDataReader sqlReader = null;
					sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_PHR_SearchChildvendorpaymentDetails", arParms);
					List<VendorPaymentData> Childvendorpayment = ORHelper<VendorPaymentData>.FromDataReaderToList(sqlReader);
					result = Childvendorpayment;
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

		public List<VendorPaymentData> SearchChildvendorpaymentByPaymentNo(VendorPaymentData objdata)
		{
			List<VendorPaymentData> result = null;
			try
			{
				{
					SqlParameter[] arParms = new SqlParameter[2];

					arParms[0] = new SqlParameter("@ID", SqlDbType.BigInt);
					arParms[0].Value = objdata.ID;

					arParms[1] = new SqlParameter("@PaymentNo", SqlDbType.VarChar);
					arParms[1].Value = objdata.PaymentNo;

					SqlDataReader sqlReader = null;
					sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_PHR_SearchChildvendorpaymentByPaymentNo", arParms);
					List<VendorPaymentData> Childvendorpayment = ORHelper<VendorPaymentData>.FromDataReaderToList(sqlReader);
					result = Childvendorpayment;
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

		public List<BankDetail> Getbanklist(BankDetail objbankdetail)
		{
			List<BankDetail> result = null;
			try
			{
				{
					SqlParameter[] arParms = new SqlParameter[1];

					arParms[0] = new SqlParameter("@PaymodeID", SqlDbType.Int);
					arParms[0].Value = objbankdetail.PaymodeID;

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

		public List<VendorPaymentData> GetVendorpaymentno(VendorPaymentData objdata)
		{
			List<VendorPaymentData> result = null;
			try
			{
				{
					SqlParameter[] arParms = new SqlParameter[1];

					arParms[0] = new SqlParameter("@PaymentNo", SqlDbType.VarChar);
					arParms[0].Value = objdata.PaymentNo;

					SqlDataReader sqlReader = null;
					sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetVendorpaymentno", arParms);
					List<VendorPaymentData> chargelist = ORHelper<VendorPaymentData>.FromDataReaderToList(sqlReader);
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

		public int DeleteVendorPaymentByPaymentNo(VendorPaymentData objdata)
		{
			int result = 0;
			try
			{
				{
					SqlParameter[] arParms = new SqlParameter[4];

					arParms[0] = new SqlParameter("@PaymentNo", SqlDbType.VarChar);
					arParms[0].Value = objdata.PaymentNo;

					arParms[1] = new SqlParameter("@Remarks", SqlDbType.VarChar);
					arParms[1].Value = objdata.Remark;

					arParms[2] = new SqlParameter("@Output", SqlDbType.SmallInt);
					arParms[2].Direction = ParameterDirection.Output;

					arParms[3] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
					arParms[3].Value = objdata.EmployeeID;



					int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_PHR_DeleteVendorPaymentByPaymentNo", arParms);
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

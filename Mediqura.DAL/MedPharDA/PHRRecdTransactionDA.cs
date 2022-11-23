using Mediqura.CommonData.MedPharData;
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

namespace Mediqura.DAL.MedPharDA
{
	public class PHRRecdTransactionDA
	{
		public List<PHRRecdTransactionData> GetTransactionList(PHRRecdTransactionData objbill)
		{
			List<PHRRecdTransactionData> result = null;
			try
			{
				{
					SqlParameter[] arParms = new SqlParameter[5];

					arParms[0] = new SqlParameter("@AccountID", SqlDbType.Int);
					arParms[0].Value = objbill.AccountID;

					arParms[1] = new SqlParameter("@DateFrom", SqlDbType.DateTime);
					arParms[1].Value = objbill.DateFrom;

					arParms[2] = new SqlParameter("@DateTo", SqlDbType.DateTime);
					arParms[2].Value = objbill.DateTo;

					arParms[3] = new SqlParameter("@Ttype", SqlDbType.Int);
					arParms[3].Value = objbill.TransactionTypeID;

					arParms[4] = new SqlParameter("@AccountState", SqlDbType.Int);
					arParms[4].Value = objbill.AccountState;
					SqlDataReader sqlReader = null;
					sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_PHR_GetTransactionList", arParms);
					List<PHRRecdTransactionData> listpatientdetails = ORHelper<PHRRecdTransactionData>.FromDataReaderToList(sqlReader);
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
		public List<PHRRecdTransactionData> GetManualTransactionList(PHRRecdTransactionData objbill)
		{
			List<PHRRecdTransactionData> result = null;
			try
			{
				{
					SqlParameter[] arParms = new SqlParameter[5];

					arParms[0] = new SqlParameter("@AccountID", SqlDbType.Int);
					arParms[0].Value = objbill.AccountID;

					arParms[1] = new SqlParameter("@DateFrom", SqlDbType.DateTime);
					arParms[1].Value = objbill.DateFrom;

					arParms[2] = new SqlParameter("@DateTo", SqlDbType.DateTime);
					arParms[2].Value = objbill.DateTo;

					arParms[3] = new SqlParameter("@Ttype", SqlDbType.Int);
					arParms[3].Value = objbill.TransactionTypeID;

					arParms[4] = new SqlParameter("@AccountState", SqlDbType.Int);
					arParms[4].Value = objbill.AccountState;
					SqlDataReader sqlReader = null;
					sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_PHR_GetManualTransactionList", arParms);
					List<PHRRecdTransactionData> listpatientdetails = ORHelper<PHRRecdTransactionData>.FromDataReaderToList(sqlReader);
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
		public List<PHRRecdTransactionData> Get_TransactionDetailsByVoucherNo(PHRRecdTransactionData objbill)
		{
			List<PHRRecdTransactionData> result = null;
			try
			{
				{
					SqlParameter[] arParms = new SqlParameter[1];

					arParms[0] = new SqlParameter("@VoucherNo", SqlDbType.VarChar);
					arParms[0].Value = objbill.VoucherNo;

					SqlDataReader sqlReader = null;
					sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_PHR_Get_TransactionDetailsByVoucherNo", arParms);
					List<PHRRecdTransactionData> listpatientdetails = ORHelper<PHRRecdTransactionData>.FromDataReaderToList(sqlReader);
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

		public List<PHRRecdTransactionData> GetIncomeTransactionList(PHRRecdTransactionData objbill)
		{
			List<PHRRecdTransactionData> result = null;
			try
			{
				{
					SqlParameter[] arParms = new SqlParameter[4];

					arParms[0] = new SqlParameter("@AccountID", SqlDbType.Int);
					arParms[0].Value = 0;

					arParms[1] = new SqlParameter("@DateFrom", SqlDbType.DateTime);
					arParms[1].Value = objbill.DateFrom;

					arParms[2] = new SqlParameter("@DateTo", SqlDbType.DateTime);
					arParms[2].Value = objbill.DateTo;

					arParms[3] = new SqlParameter("@Voucher", SqlDbType.VarChar);
					arParms[3].Value = objbill.VoucherNo;

					SqlDataReader sqlReader = null;
					sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_PHR_GetIncomeTransactionListByAccountID", arParms);
					List<PHRRecdTransactionData> listpatientdetails = ORHelper<PHRRecdTransactionData>.FromDataReaderToList(sqlReader);
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
	
		public int DeleteIncomeTranByID(PHRRecdTransactionData objbill)
		{
			int result = 0;
			try
			{
				{
					SqlParameter[] arParms = new SqlParameter[6];

					arParms[0] = new SqlParameter("@VoucherNo", SqlDbType.VarChar);
					arParms[0].Value = objbill.VoucherNo;

					arParms[1] = new SqlParameter("@Output", SqlDbType.SmallInt);
					arParms[1].Direction = ParameterDirection.Output;

					arParms[2] = new SqlParameter("@Remarks", SqlDbType.VarChar);
					arParms[2].Value = objbill.Remarks;

					arParms[3] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
					arParms[3].Value = objbill.EmployeeID;

					arParms[4] = new SqlParameter("@HospitalID", SqlDbType.Int);
					arParms[4].Value = objbill.HospitalID;

					arParms[5] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
					arParms[5].Value = objbill.FinancialYearID;


					int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_PHR_DeleteIncomebyID", arParms);
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

		public int DeleteAccountTransactionDetailsByID(PHRRecdTransactionData objbill)
		{
			int result = 0;
			try
			{
				{
					SqlParameter[] arParms = new SqlParameter[4];

					arParms[0] = new SqlParameter("@VoucherNo", SqlDbType.VarChar);
					arParms[0].Value = objbill.VoucherNo;

					arParms[1] = new SqlParameter("@EmployeeID", SqlDbType.Int);
					arParms[1].Value = objbill.EmployeeID;

					arParms[2] = new SqlParameter("@Output", SqlDbType.SmallInt);
					arParms[2].Direction = ParameterDirection.Output;


					arParms[3] = new SqlParameter("@Remarks", SqlDbType.VarChar);
					arParms[3].Value = objbill.Remarks;

					int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_PHR_DeleteAccountByVoucherNo", arParms);
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

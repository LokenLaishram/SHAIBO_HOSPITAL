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
	public class PHRAccountDA
	{
		public int UpdateAccountGroup(PHRAcountData objData)
		{
			int result = 0;
			try
			{
				{
					SqlParameter[] arParms = new SqlParameter[8];

					arParms[0] = new SqlParameter("@GroupName", SqlDbType.VarChar);
					arParms[0].Value = objData.GroupName;

					arParms[1] = new SqlParameter("@GroupUnderID", SqlDbType.Int);
					arParms[1].Value = objData.GroupUnderID;

					arParms[2] = new SqlParameter("@GroupNatureID", SqlDbType.Int);
					arParms[2].Value = objData.NatureID;

					arParms[3] = new SqlParameter("@Output", SqlDbType.SmallInt);
					arParms[3].Direction = ParameterDirection.Output;

					arParms[4] = new SqlParameter("@AddedBy", SqlDbType.BigInt);
					arParms[4].Value = objData.EmployeeID;

					arParms[5] = new SqlParameter("@HospitalID", SqlDbType.Int);
					arParms[5].Value = objData.HospitalID;

					arParms[6] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
					arParms[6].Value = objData.FinancialYearID;

					arParms[7] = new SqlParameter("@ID", SqlDbType.Int);
					arParms[7].Value = objData.ID;


					int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_PHR_Update_AccountGroup", arParms);
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
		public List<PHRAccountGroupMasterData> GetAccntGrpList(PHRAcountData objData)
		{
			List<PHRAccountGroupMasterData> result = null;
			try
			{
				{
					SqlParameter[] arParms = new SqlParameter[3];

					arParms[0] = new SqlParameter("@GroupName", SqlDbType.VarChar);
					arParms[0].Value = objData.GroupName;

					arParms[1] = new SqlParameter("@GroupUnderID", SqlDbType.Int);
					arParms[1].Value = objData.GroupUnderID;

					arParms[2] = new SqlParameter("@GroupNatureID", SqlDbType.Int);
					arParms[2].Value = objData.NatureID;

					SqlDataReader sqlReader = null;
					sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_PHR_GetAccountgroupList", arParms);
					List<PHRAccountGroupMasterData> listpatientdetails = ORHelper<PHRAccountGroupMasterData>.FromDataReaderToList(sqlReader);
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
		public int DeleteAccountGroupByID(PHRAcountData objpat)
		{
			int result = 0;
			try
			{
				{
					SqlParameter[] arParms = new SqlParameter[4];

					arParms[0] = new SqlParameter("@ID", SqlDbType.BigInt);
					arParms[0].Value = objpat.ID;
					arParms[1] = new SqlParameter("@Output", SqlDbType.SmallInt);
					arParms[1].Direction = ParameterDirection.Output;
					arParms[2] = new SqlParameter("@Remarks", SqlDbType.VarChar);
					arParms[2].Value = objpat.Remarks;
					arParms[3] = new SqlParameter("@UserId", SqlDbType.BigInt);
					arParms[3].Value = objpat.EmployeeID;
					int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_PHR_DeleteAccntGrpbyID", arParms);
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
		public int UpdateAccountLedger(PHRAcountLedgerData objData)
		{
			int result = 0;
			try
			{
				{
					SqlParameter[] arParms = new SqlParameter[11];

					arParms[0] = new SqlParameter("@AccountName", SqlDbType.VarChar);
					arParms[0].Value = objData.AccountName;

					arParms[1] = new SqlParameter("@GroupUnderID", SqlDbType.Int);
					arParms[1].Value = objData.GroupUnderID;

					arParms[2] = new SqlParameter("@GroupNatureID", SqlDbType.Int);
					arParms[2].Value = objData.NatureID;

					arParms[3] = new SqlParameter("@Output", SqlDbType.SmallInt);
					arParms[3].Direction = ParameterDirection.Output;

					arParms[4] = new SqlParameter("@AddedBy", SqlDbType.BigInt);
					arParms[4].Value = objData.EmployeeID;

					arParms[5] = new SqlParameter("@HospitalID", SqlDbType.Int);
					arParms[5].Value = objData.HospitalID;

					arParms[6] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
					arParms[6].Value = objData.FinancialYearID;

					arParms[7] = new SqlParameter("@ID", SqlDbType.Int);
					arParms[7].Value = objData.ID;

					arParms[8] = new SqlParameter("@Site", SqlDbType.VarChar);
					arParms[8].Value = objData.Site;

					arParms[9] = new SqlParameter("@OpnBalance", SqlDbType.Money);
					arParms[9].Value = objData.Opnbal;

					arParms[10] = new SqlParameter("@OpnDate", SqlDbType.DateTime);
					arParms[10].Value = objData.OpnDate;


					int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_PHR_Update_AccountLedger", arParms);
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
		public List<PHRAccountLedgerMasterData> GetAccntLedgerList(PHRAcountLedgerData objData)
		{
			List<PHRAccountLedgerMasterData> result = null;
			try
			{
				{
					SqlParameter[] arParms = new SqlParameter[5];

					arParms[0] = new SqlParameter("@AccountName", SqlDbType.VarChar);
					arParms[0].Value = objData.AccountName;

					arParms[1] = new SqlParameter("@GroupUnderID", SqlDbType.Int);
					arParms[1].Value = objData.GroupUnderID;

					arParms[2] = new SqlParameter("@GroupNatureID", SqlDbType.Int);
					arParms[2].Value = objData.NatureID;

					arParms[3] = new SqlParameter("@Site", SqlDbType.VarChar);
					arParms[3].Value = objData.Site;

					arParms[4] = new SqlParameter("@OpnBalance", SqlDbType.Money);
					arParms[4].Value = objData.Opnbal;

					SqlDataReader sqlReader = null;
					sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_PHR_GetAccountLedgerList", arParms);
					List<PHRAccountLedgerMasterData> listpatientdetails = ORHelper<PHRAccountLedgerMasterData>.FromDataReaderToList(sqlReader);
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
		public int DeleteAccountLedgerByID(PHRAcountLedgerData objpat)
		{
			int result = 0;
			try
			{
				{
					SqlParameter[] arParms = new SqlParameter[4];

					arParms[0] = new SqlParameter("@ID", SqlDbType.BigInt);
					arParms[0].Value = objpat.ID;
					arParms[1] = new SqlParameter("@Output", SqlDbType.SmallInt);
					arParms[1].Direction = ParameterDirection.Output;
					arParms[2] = new SqlParameter("@Remarks", SqlDbType.VarChar);
					arParms[2].Value = objpat.Remarks;
					arParms[3] = new SqlParameter("@UserId", SqlDbType.BigInt);
					arParms[3].Value = objpat.EmployeeID;
					int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_PHR_DeleteAccntLedgerbyID", arParms);
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

		public List<PHRAccountMappingMasterData> GetAcntServiceMappingList(PHRAccountMappingMasterData objData)
		{
			List<PHRAccountMappingMasterData> result = null;
			try
			{
				{
					SqlParameter[] arParms = new SqlParameter[4];

					arParms[0] = new SqlParameter("@ServiceType", SqlDbType.Int);
					arParms[0].Value = objData.ServiceType;

					arParms[1] = new SqlParameter("@subServicetype", SqlDbType.Int);
					arParms[1].Value = objData.SubServiceType;

					arParms[2] = new SqlParameter("@MappingType", SqlDbType.Int);
					arParms[2].Value = objData.MappingType;

					arParms[3] = new SqlParameter("@GroupType", SqlDbType.Int);
					arParms[3].Value = objData.GroupType;

					SqlDataReader sqlReader = null;
					sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_PHR_GetServiceMappingAccount", arParms);
					List<PHRAccountMappingMasterData> listpatientdetails = ORHelper<PHRAccountMappingMasterData>.FromDataReaderToList(sqlReader);
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
		public int UpdateAccntMappingMaster(PHRAccountMappingMasterData objData)
		{
			int result = 0;
			try
			{
				{
					SqlParameter[] arParms = new SqlParameter[5];

					arParms[0] = new SqlParameter("@XmlData", SqlDbType.Xml);
					arParms[0].Value = objData.XMLData;

					arParms[1] = new SqlParameter("@Output", SqlDbType.SmallInt);
					arParms[1].Direction = ParameterDirection.Output;

					arParms[2] = new SqlParameter("@AddedBy", SqlDbType.BigInt);
					arParms[2].Value = objData.EmployeeID;

					arParms[3] = new SqlParameter("@HospitalID", SqlDbType.Int);
					arParms[3].Value = objData.HospitalID;

					arParms[4] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
					arParms[4].Value = objData.FinancialYearID;



					int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_PHR_Update_AccountMappingMaster", arParms);
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
		public List<PHRAcountLedgerData> SearchLedgerByName(PHRAcountLedgerData objLedger)
		{
			List<PHRAcountLedgerData> result = null;
			try
			{
				{
					SqlParameter[] arParms = new SqlParameter[2];

					arParms[0] = new SqlParameter("@LedgerName", SqlDbType.VarChar);
					arParms[0].Value = objLedger.AccountName;

					arParms[1] = new SqlParameter("@PaymentType", SqlDbType.Int);
					arParms[1].Value = objLedger.Payment_type;

					SqlDataReader sqlReader = null;
					sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_PHR_SearchLedgerByName", arParms);
					List<PHRAcountLedgerData> lstServiceDetails = ORHelper<PHRAcountLedgerData>.FromDataReaderToList(sqlReader);
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
		public PHRAccountTransactionOutput UpdateAccountTransaction(PHRAccountTransactionData objData)
		{

			PHRAccountTransactionOutput outputdata = new PHRAccountTransactionOutput();

			try
			{
				{
					SqlParameter[] arParms = new SqlParameter[15];

					arParms[0] = new SqlParameter("@Amount", SqlDbType.Money);
					arParms[0].Value = objData.Amount;
					
					arParms[1] = new SqlParameter("@AddedBy", SqlDbType.BigInt);
					arParms[1].Value = objData.EmployeeID;

					arParms[2] = new SqlParameter("@HospitalID", SqlDbType.Int);
					arParms[2].Value = objData.HospitalID;

					arParms[3] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
					arParms[3].Value = objData.FinancialYearID;

					arParms[4] = new SqlParameter("@Date", SqlDbType.DateTime);
					arParms[4].Value = objData.Date;

					arParms[5] = new SqlParameter("@Naration", SqlDbType.VarChar);
					arParms[5].Value = objData.Naration;

					arParms[6] = new SqlParameter("@PaymentType", SqlDbType.Int);
					arParms[6].Value = objData.PaymentType;

					arParms[7] = new SqlParameter("@PaymentMode", SqlDbType.Int);
					arParms[7].Value = objData.PaymentMode;

					arParms[8] = new SqlParameter("@TransactionType", SqlDbType.Int);
					arParms[8].Value = objData.TransactionType;

					arParms[9] = new SqlParameter("@InstrumentName", SqlDbType.VarChar);
					arParms[9].Value = objData.InstrumentName;

					arParms[10] = new SqlParameter("@InstrumentDate", SqlDbType.DateTime);
					arParms[10].Value = objData.InstrumentDate;

					arParms[11] = new SqlParameter("@BankPayeeName", SqlDbType.VarChar);
					arParms[11].Value = objData.BankPayeeName;

					arParms[12] = new SqlParameter("@BankBranchName", SqlDbType.VarChar);
					arParms[12].Value = objData.BankBranchName;

					arParms[13] = new SqlParameter("@CrossInt", SqlDbType.VarChar);
					arParms[13].Value = objData.CrossInt;

					arParms[14] = new SqlParameter("@AccountID", SqlDbType.BigInt);
					arParms[14].Value = objData.AccountID;
					

					SqlDataReader sqlReader = null;
					sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_PHR_Update_Account_transaction", arParms);
					List<PHRAccountTransactionOutput> output = ORHelper<PHRAccountTransactionOutput>.FromDataReaderToList(sqlReader);

					outputdata.outputdata = Convert.ToInt32(output[0].outputdata);
					outputdata.voucher = output[0].voucher;

				}
			}
			catch (Exception ex) //Exception of the business layer(itself)//unhandle
			{
				PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.DataAccessExceptionPolicy, ex, "330001");
				LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
				throw new DataAccessException("5000001", ex);
			}
			return outputdata;
		}
		public PHRAccountTransactionOutput UpdateIncomeTransaction(PHRAccountTransactionData objData)
		{

			PHRAccountTransactionOutput outputdata = new PHRAccountTransactionOutput();

			try
			{
				{
					SqlParameter[] arParms = new SqlParameter[10];

					arParms[0] = new SqlParameter("@DebitID", SqlDbType.Int);
					arParms[0].Value = objData.DebitID;

					arParms[1] = new SqlParameter("@AddedBy", SqlDbType.BigInt);
					arParms[1].Value = objData.EmployeeID;

					arParms[2] = new SqlParameter("@HospitalID", SqlDbType.Int);
					arParms[2].Value = objData.HospitalID;

					arParms[3] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
					arParms[3].Value = objData.FinancialYearID;

					arParms[4] = new SqlParameter("@TotalAmount", SqlDbType.Money);
					arParms[4].Value = objData.TotalDebit;

					arParms[5] = new SqlParameter("@Naration", SqlDbType.VarChar);
					arParms[5].Value = objData.Naration;

					arParms[6] = new SqlParameter("@PaymentMode", SqlDbType.Int);
					arParms[6].Value = objData.PaymentMode;

					arParms[7] = new SqlParameter("@BankName", SqlDbType.VarChar);
					arParms[7].Value = objData.BankName;

					arParms[8] = new SqlParameter("@Cheque", SqlDbType.VarChar);
					arParms[8].Value = objData.Cheque;

					arParms[9] = new SqlParameter("@Invoicenumber", SqlDbType.VarChar);
					arParms[9].Value = objData.Invoicenumber;

					SqlDataReader sqlReader = null;
					sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_PHR_Update_Income_transaction", arParms);
					List<PHRAccountTransactionOutput> output = ORHelper<PHRAccountTransactionOutput>.FromDataReaderToList(sqlReader);

					outputdata.outputdata = Convert.ToInt32(output[0].outputdata);
					outputdata.voucher = output[0].voucher;

				}
			}
			catch (Exception ex) //Exception of the business layer(itself)//unhandle
			{
				PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.DataAccessExceptionPolicy, ex, "330001");
				LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
				throw new DataAccessException("5000001", ex);
			}
			return outputdata;
		}

		public List<PHRAccountTransactionData> GetExpensesTransactionList(PHRAccountTransactionData ObjData)
		{
			List<PHRAccountTransactionData> result = null;
			try
			{
				{
					SqlParameter[] arParms = new SqlParameter[6];

					arParms[0] = new SqlParameter("@TransactionID", SqlDbType.Int);
					arParms[0].Value = ObjData.TransactionID;

					arParms[1] = new SqlParameter("@dateFrom", SqlDbType.DateTime);
					arParms[1].Value = ObjData.DateFrom;

					arParms[2] = new SqlParameter("@dateto", SqlDbType.DateTime);
					arParms[2].Value = ObjData.DateTo;

					arParms[3] = new SqlParameter("@AccountID", SqlDbType.BigInt);
					arParms[3].Value = ObjData.AccountID;

					arParms[4] = new SqlParameter("@AccountState", SqlDbType.Int);
					arParms[4].Value = ObjData.AccountState;

					arParms[5] = new SqlParameter("@LoginName", SqlDbType.VarChar);
					arParms[5].Value = ObjData.EmpName;

					SqlDataReader sqlReader = null;
					sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_PHR_GetManualExpensesTransactionList", arParms);
					List<PHRAccountTransactionData> listexpensestran = ORHelper<PHRAccountTransactionData>.FromDataReaderToList(sqlReader);
					result = listexpensestran;
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

		public List<PHRAccountTransactionData> GetIncomeTransactionList(PHRAccountTransactionData ObjData)
		{
			List<PHRAccountTransactionData> result = null;
			try
			{
				{
					SqlParameter[] arParms = new SqlParameter[6];

					arParms[0] = new SqlParameter("@TransactionID", SqlDbType.Int);
					arParms[0].Value = ObjData.TransactionID;

					arParms[1] = new SqlParameter("@dateFrom", SqlDbType.DateTime);
					arParms[1].Value = ObjData.DateFrom;

					arParms[2] = new SqlParameter("@dateto", SqlDbType.DateTime);
					arParms[2].Value = ObjData.DateTo;

					arParms[3] = new SqlParameter("@AccountID", SqlDbType.BigInt);
					arParms[3].Value = ObjData.AccountID;

					arParms[4] = new SqlParameter("@AccountState", SqlDbType.Int);
					arParms[4].Value = ObjData.AccountState;

					arParms[5] = new SqlParameter("@LoginName", SqlDbType.VarChar);
					arParms[5].Value = ObjData.EmpName;


					SqlDataReader sqlReader = null;
					sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_PHR_GetManualIncomeTransactionList", arParms);
					List<PHRAccountTransactionData> listexpensestran = ORHelper<PHRAccountTransactionData>.FromDataReaderToList(sqlReader);
					result = listexpensestran;
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

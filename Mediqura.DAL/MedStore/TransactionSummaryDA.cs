using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mediqura.CommonData.MedStore;
using System.Data.SqlClient;
using System.Data;
using Mediqura.Utility;
using Mediqura.Utility.Util;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;

namespace Mediqura.DAL.MedStore
{
	public class TransactionSummaryDA
	{
		public List<TransactionSummaryData> GetIncomeTransactionList(TransactionSummaryData ObjData)
		{
			List<TransactionSummaryData> result = null;
			try
			{
				{
					SqlParameter[] arParms = new SqlParameter[8];

					arParms[0] = new SqlParameter("@TransactionID", SqlDbType.Int);
					arParms[0].Value = ObjData.TransactionID;

					arParms[1] = new SqlParameter("@dateFrom", SqlDbType.DateTime);
					arParms[1].Value = ObjData.DateFrom;

					arParms[2] = new SqlParameter("@dateto", SqlDbType.DateTime);
					arParms[2].Value = ObjData.DateTo;

					arParms[3] = new SqlParameter("@CollectedByID", SqlDbType.BigInt);
					arParms[3].Value = ObjData.CollectedByID;

					arParms[4] = new SqlParameter("@AccountState", SqlDbType.Int);
					arParms[4].Value = ObjData.AccountState;
					
					arParms[5] = new SqlParameter("@LoginName", SqlDbType.VarChar);
					arParms[5].Value = ObjData.EmpName;

					arParms[6] = new SqlParameter("@Paymode", SqlDbType.Int);
					arParms[6].Value = ObjData.Paymode;

					arParms[7] = new SqlParameter("@AccountID", SqlDbType.Int);
					arParms[7].Value = ObjData.AccountID;

					SqlDataReader sqlReader = null;
					sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_PHR_GetIncomeTransactionList", arParms);
					List<TransactionSummaryData> listincometran = ORHelper<TransactionSummaryData>.FromDataReaderToList(sqlReader);
					result = listincometran;
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

		public List<TransactionSummaryData> GetExpensesTransactionList(TransactionSummaryData ObjData)
		{
			List<TransactionSummaryData> result = null;
			try
			{
				{
					SqlParameter[] arParms = new SqlParameter[8];

					arParms[0] = new SqlParameter("@TransactionID", SqlDbType.Int);
					arParms[0].Value = ObjData.TransactionID;

					arParms[1] = new SqlParameter("@dateFrom", SqlDbType.DateTime);
					arParms[1].Value = ObjData.DateFrom;

					arParms[2] = new SqlParameter("@dateto", SqlDbType.DateTime);
					arParms[2].Value = ObjData.DateTo;

					arParms[3] = new SqlParameter("@CollectedByID", SqlDbType.BigInt);
					arParms[3].Value = ObjData.CollectedByID;

					arParms[4] = new SqlParameter("@AccountState", SqlDbType.Int);
					arParms[4].Value = ObjData.AccountState;

					arParms[5] = new SqlParameter("@LoginName", SqlDbType.VarChar);
					arParms[5].Value = ObjData.EmpName;

					arParms[6] = new SqlParameter("@Paymode", SqlDbType.Int);
					arParms[6].Value = ObjData.Paymode;

					arParms[7] = new SqlParameter("@AccountID", SqlDbType.Int);
					arParms[7].Value = ObjData.AccountID;

					SqlDataReader sqlReader = null;
					sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_PHR_GetExpensesTransactionList", arParms);
					List<TransactionSummaryData> listexpensestran = ORHelper<TransactionSummaryData>.FromDataReaderToList(sqlReader);
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mediqura.CommonData.MedStore;
using Mediqura.DAL.MedStore;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;

namespace Mediqura.BOL.MedStore
{
	public class TransactionSummaryBO
	{
		public List<TransactionSummaryData> GetIncomeTransactionList(TransactionSummaryData ObjData)
		{
			List<TransactionSummaryData> result = null;
			try
			{
				TransactionSummaryDA objDA = new TransactionSummaryDA();
				result = objDA.GetIncomeTransactionList(ObjData);
			}
			catch (Exception ex)
			{
				PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
				LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
				throw new BusinessProcessException("4000001", ex);
			}
			return result;
		}

		public List<TransactionSummaryData> GetExpensesTransactionList(TransactionSummaryData ObjData)
		{
			List<TransactionSummaryData> result = null;
			try
			{
				TransactionSummaryDA objDA = new TransactionSummaryDA();
				result = objDA.GetExpensesTransactionList(ObjData);
			}
			catch (Exception ex)
			{
				PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
				LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
				throw new BusinessProcessException("4000001", ex);
			}
			return result;
		}
	}
}

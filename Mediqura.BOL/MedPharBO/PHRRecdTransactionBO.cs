using Mediqura.CommonData.MedPharData;
using Mediqura.DAL.MedPharDA;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.BOL.MedPharBO
{
	public class PHRRecdTransactionBO
	{
		public List<PHRRecdTransactionData> GetTransactionList(PHRRecdTransactionData objData)
		{
			List<PHRRecdTransactionData> result = null;
			try
			{
				PHRRecdTransactionDA objDA = new PHRRecdTransactionDA();
				result = objDA.GetTransactionList(objData);
			}
			catch (Exception ex)
			{
				PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
				LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
				throw new BusinessProcessException("4000001", ex);
			}
			return result;
		}
		public List<PHRRecdTransactionData> GetManualTransactionList(PHRRecdTransactionData objData)
		{
			List<PHRRecdTransactionData> result = null;
			try
			{
				PHRRecdTransactionDA objDA = new PHRRecdTransactionDA();
				result = objDA.GetManualTransactionList(objData);
			}
			catch (Exception ex)
			{
				PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
				LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
				throw new BusinessProcessException("4000001", ex);
			}
			return result;
		}
		public List<PHRRecdTransactionData> Get_TransactionDetailsByVoucherNo(PHRRecdTransactionData objData)
		{
			List<PHRRecdTransactionData> result = null;
			try
			{
				PHRRecdTransactionDA objDA = new PHRRecdTransactionDA();
				result = objDA.Get_TransactionDetailsByVoucherNo(objData);
			}
			catch (Exception ex)
			{
				PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
				LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
				throw new BusinessProcessException("4000001", ex);
			}
			return result;
		}
		public List<PHRRecdTransactionData> GetIncomeTransactionList(PHRRecdTransactionData objData)
		{
			List<PHRRecdTransactionData> result = null;
			try
			{
				PHRRecdTransactionDA objDA = new PHRRecdTransactionDA();
				result = objDA.GetIncomeTransactionList(objData);
			}
			catch (Exception ex)
			{
				PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
				LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
				throw new BusinessProcessException("4000001", ex);
			}
			return result;
		}
		
		public int DeleteIncomeByID(PHRRecdTransactionData objstd)
		{
			int result = 0;

			try
			{
				PHRRecdTransactionDA objDA = new PHRRecdTransactionDA();
				result = objDA.DeleteIncomeTranByID(objstd);

			}
			catch (Exception ex)
			{
				PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
				LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
				throw new BusinessProcessException("4000001", ex);
			}
			return result;

		}

		public int DeleteAccountTransactionDetailsByID(PHRRecdTransactionData objData)
		{
			int result = 0;
			try
			{
				PHRRecdTransactionDA objDA = new PHRRecdTransactionDA();
				result = objDA.DeleteAccountTransactionDetailsByID(objData);
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

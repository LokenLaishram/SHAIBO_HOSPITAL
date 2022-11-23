using Mediqura.CommonData.MedAccount;
using Mediqura.DAL.MedAccount;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.BOL.MedAccount
{
    public class TransactionDefaultBO
    {
        public int UpdateTransactionDetails(TransactionDefaultData objCountryMSTData)
        {
            int result = 0;
            try
            {
                TransactionDefautDA objCountryMSTDA = new TransactionDefautDA();
                result = objCountryMSTDA.UpdateTransactionDetails(objCountryMSTData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<TransactionDefaultData> GetTransaction(TransactionDefaultData objCountryMSTData)
        {
            List<TransactionDefaultData> result = null;
            try
            {
                TransactionDefautDA objCountryMSTDA = new TransactionDefautDA();
                result = objCountryMSTDA.GetTransaction(objCountryMSTData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<TransactionDefaultData> GetTransactionDetailsByID(TransactionDefaultData objCountryMSTData)
        {
            List<TransactionDefaultData> result = null;

            try
            {
                TransactionDefautDA objCountryMSTDA = new TransactionDefautDA();
                result = objCountryMSTDA.GetTransactionDetailsByID(objCountryMSTData);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeleteTransactionDetailsByID(TransactionDefaultData objCountryMSTData)
        {
            int result = 0;
            try
            {
                TransactionDefautDA objCountryMSTDA = new TransactionDefautDA();
                result = objCountryMSTDA.DeleteTransactionDetailsByID(objCountryMSTData);
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

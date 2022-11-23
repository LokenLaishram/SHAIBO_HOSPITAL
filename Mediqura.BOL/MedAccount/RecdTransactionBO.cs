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
    public class RecdTransactionBO
    {
        public List<RecdTransactionData> GetTransactionList(RecdTransactionData objData)
        {
            List<RecdTransactionData> result = null;
            try
            {
                RecdTransactionDA objDA = new RecdTransactionDA();
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
        public List<RecdTransactionData> GetManualTransactionList(RecdTransactionData objData)
        {
            List<RecdTransactionData> result = null;
            try
            {
                RecdTransactionDA objDA = new RecdTransactionDA();
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
        public List<RecdTransactionData> Get_TransactionDetailsByVoucherNo(RecdTransactionData objData)
        {
            List<RecdTransactionData> result = null;
            try
            {
                RecdTransactionDA objDA = new RecdTransactionDA();
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
        public List<RecdTransactionData> GetIncomeTransactionList(RecdTransactionData objData)
        {
            List<RecdTransactionData> result = null;
            try
            {
                RecdTransactionDA objDA = new RecdTransactionDA();
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
        public List<RecdTransactionData> GetPassList(RecdTransactionData objData)
        {
            List<RecdTransactionData> result = null;
            try
            {
                RecdTransactionDA objDA = new RecdTransactionDA();
                result = objDA.GetPassList(objData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeleteIncomeByID(RecdTransactionData objstd)
        {
            int result = 0;

            try
            {
                RecdTransactionDA objDA = new RecdTransactionDA();
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
        public int DeleteVehiclePassByID(RecdTransactionData objstd)
        {
            int result = 0;

            try
            {
                RecdTransactionDA objDA = new RecdTransactionDA();
                result = objDA.DeleteVehiclePassByID(objstd);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public int DeleteAccountTransactionDetailsByID(RecdTransactionData objData)
        {
            int result = 0;
            try
            {
                RecdTransactionDA objDA = new RecdTransactionDA();
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

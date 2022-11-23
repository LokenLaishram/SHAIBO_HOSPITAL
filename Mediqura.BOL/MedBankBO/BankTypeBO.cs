using Mediqura.CommonData.MedBankData;
using Mediqura.DAL.MedBankDA;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.BOL.MedBankBO
{
    public class BankTypeBO
    {
        public int UpdateBankTypeDetails(BankTypeData objBankTypeMasterData)
        {
            int result = 0;
            try
            {
                BankTypeDA objBankTypeMasterDA = new BankTypeDA();
                result = objBankTypeMasterDA.UpdateBankTypeDetails(objBankTypeMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<BankTypeData> SearchBankTypeDetails(BankTypeData objBankTypeMasterData)
        {
            List<BankTypeData> result = null;
            try
            {
                BankTypeDA objBankTypeMasterDA = new BankTypeDA();
                result = objBankTypeMasterDA.SearchBankTypeDetails(objBankTypeMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<BankTypeData> GetBankTypeDetailsByID(BankTypeData objBankTypeMasterData)
        {
            List<BankTypeData> result = null;

            try
            {
                BankTypeDA objBankTypeMasterDA = new BankTypeDA();
                result = objBankTypeMasterDA.GetBankTypeDetailsByID(objBankTypeMasterData);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeleteBankTypeDetailsByID(BankTypeData objBankTypeMasterData)
        {
            int result = 0;
            try
            {
                BankTypeDA objBankTypeMasterDA = new BankTypeDA();
                result = objBankTypeMasterDA.DeleteBankTypeDetailsByID(objBankTypeMasterData);
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

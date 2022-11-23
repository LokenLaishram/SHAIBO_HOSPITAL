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

    public class BankMasterBO
    {
        public int UpdateBankChkBookIssueDetails(BankMasterData objBankMasterMasterData)
        {

            int result = 0;
            try
            {
                BankMasterDA objBankMasterMasterDA = new BankMasterDA();
                result = objBankMasterMasterDA.UpdateBankChkBookIssueDetails(objBankMasterMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
         public List<BankMasterData> SearchBankChkBookIssueDetails(BankMasterData objBankMasterMasterData)
        {
            List<BankMasterData> result = null;
            try
            {
                BankMasterDA objBankMasterMasterDA = new BankMasterDA();
                result = objBankMasterMasterDA.SearchBankChkBookIssueDetails(objBankMasterMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
         public List<BankMasterData> GetBankChkBookIssueDetailsByID(BankMasterData objBankMasterMasterData)
        {
            List<BankMasterData> result = null;

            try
            {
                BankMasterDA objBankMasterMasterDA = new BankMasterDA();
                result = objBankMasterMasterDA.GetBankChkBookIssueDetailsByID(objBankMasterMasterData);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
         public int DeleteBankChkBookIssueDetailsByID(BankMasterData objBankMasterMasterData)
        {
            int result = 0;
            try
            {
                BankMasterDA objBankMasterMasterDA = new BankMasterDA();
                result = objBankMasterMasterDA.DeleteBankChkBookIssueDetailsByID(objBankMasterMasterData);
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

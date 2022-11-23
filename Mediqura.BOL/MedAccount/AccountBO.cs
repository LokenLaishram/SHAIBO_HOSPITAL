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
   public class AccountBO
    {
       public int UpdateAccntGrpMaster(AcountData objdata)
        {
            int result = 0;
            try
            {
                AccountDA objDA = new AccountDA();
                result = objDA.UpdateAccountGroup(objdata);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
       public List<AccountGroupMasterData> GetAccntGrpList(AcountData objData)
        {
            List<AccountGroupMasterData> result = null;
            try
            {
                AccountDA objDA = new AccountDA();
                result = objDA.GetAccntGrpList(objData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
       public int DeleteAccountGroupByID(AcountData objstd)
       {
           int result = 0;

           try
           {
               AccountDA objDA = new AccountDA();
               result = objDA.DeleteAccountGroupByID(objstd);

           }
           catch (Exception ex)
           {
               PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
               LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
               throw new BusinessProcessException("4000001", ex);
           }
           return result;

       }

       public int UpdateAccntLedgerMaster(AcountLedgerData objdata)
        {
            int result = 0;
            try
            {
                AccountDA objDA = new AccountDA();
                result = objDA.UpdateAccountLedger(objdata);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
       public List<AccountLedgerMasterData> GetAccntLedgerList(AcountLedgerData objData)
        {
            List<AccountLedgerMasterData> result = null;
            try
            {
                AccountDA objDA = new AccountDA();
                result = objDA.GetAccntLedgerList(objData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
       public int DeleteAccountLedgerByID(AcountLedgerData objstd)
       {
           int result = 0;

           try
           {
               AccountDA objDA = new AccountDA();
               result = objDA.DeleteAccountLedgerByID(objstd);

           }
           catch (Exception ex)
           {
               PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
               LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
               throw new BusinessProcessException("4000001", ex);
           }
           return result;

       }
       public List<AccountMappingMasterData> GetAcntServiceMappingList(AccountMappingMasterData objData)
       {
           List<AccountMappingMasterData> result = null;
           try
           {
               AccountDA objDA = new AccountDA();
               result = objDA.GetAcntServiceMappingList(objData);
           }
           catch (Exception ex)
           {
               PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
               LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
               throw new BusinessProcessException("4000001", ex);
           }
           return result;

       }
       public int UpdateAccntMappingMaster(AccountMappingMasterData objdata)
       {
           int result = 0;
           try
           {
               AccountDA objDA = new AccountDA();
               result = objDA.UpdateAccntMappingMaster(objdata);
           }
           catch (Exception ex)
           {
               PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
               LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
               throw new BusinessProcessException("4000001", ex);
           }
           return result;
       }
       public List<AcountLedgerData> SearchLedgerByName(AcountLedgerData objLedger)
       {
           List<AcountLedgerData> result = null;
           try
           {
               AccountDA objDA = new AccountDA();
               result = objDA.SearchLedgerByName(objLedger);
           }
           catch (Exception ex)
           {
               PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
               LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
               throw new BusinessProcessException("4000001", ex);
           }
           return result;
       }
       public AccountTransactionOutput UpdateAccountTransaction(AccountTransactionData objdata)
       {


           AccountTransactionOutput outputdata = new AccountTransactionOutput();
          
           try
           {
               AccountDA objDA = new AccountDA();
               outputdata = objDA.UpdateAccountTransaction(objdata);
           }
           catch (Exception ex)
           {
               PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
               LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
               throw new BusinessProcessException("4000001", ex);
           }
           return outputdata;
       }
        public AccountTransactionOutput UpdateIncomeTransaction(AccountTransactionData objdata)
        {


            AccountTransactionOutput outputdata = new AccountTransactionOutput();

            try
            {
                AccountDA objDA = new AccountDA();
                outputdata = objDA.UpdateIncomeTransaction(objdata);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return outputdata;
        }
    }
}

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
	public class PHRAccountBO
	{
		 public int UpdateAccntGrpMaster(PHRAcountData objdata)
        {
            int result = 0;
            try
            {
				PHRAccountDA objDA = new PHRAccountDA();
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
		 public List<PHRAccountGroupMasterData> GetAccntGrpList(PHRAcountData objData)
        {
            List<PHRAccountGroupMasterData> result = null;
            try
            {
                PHRAccountDA objDA = new PHRAccountDA();
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
       public int DeleteAccountGroupByID(PHRAcountData objstd)
       {
           int result = 0;

           try
           {
               PHRAccountDA objDA = new PHRAccountDA();
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

       public int UpdateAccntLedgerMaster(PHRAcountLedgerData objdata)
        {
            int result = 0;
            try
            {
                PHRAccountDA objDA = new PHRAccountDA();
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
       public List<PHRAccountLedgerMasterData> GetAccntLedgerList(PHRAcountLedgerData objData)
        {
            List<PHRAccountLedgerMasterData> result = null;
            try
            {
                PHRAccountDA objDA = new PHRAccountDA();
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
       public int DeleteAccountLedgerByID(PHRAcountLedgerData objstd)
       {
           int result = 0;

           try
           {
               PHRAccountDA objDA = new PHRAccountDA();
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
       public List<PHRAccountMappingMasterData> GetAcntServiceMappingList(PHRAccountMappingMasterData objData)
       {
           List<PHRAccountMappingMasterData> result = null;
           try
           {
               PHRAccountDA objDA = new PHRAccountDA();
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
       public int UpdateAccntMappingMaster(PHRAccountMappingMasterData objdata)
       {
           int result = 0;
           try
           {
               PHRAccountDA objDA = new PHRAccountDA();
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
       public List<PHRAcountLedgerData> SearchLedgerByName(PHRAcountLedgerData objLedger)
       {
           List<PHRAcountLedgerData> result = null;
           try
           {
               PHRAccountDA objDA = new PHRAccountDA();
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
       public PHRAccountTransactionOutput UpdateAccountTransaction(PHRAccountTransactionData objdata)
       {
           PHRAccountTransactionOutput outputdata = new PHRAccountTransactionOutput();
          
           try
           {
               PHRAccountDA objDA = new PHRAccountDA();
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
        public PHRAccountTransactionOutput UpdateIncomeTransaction(PHRAccountTransactionData objdata)
        {


            PHRAccountTransactionOutput outputdata = new PHRAccountTransactionOutput();

            try
            {
                PHRAccountDA objDA = new PHRAccountDA();
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

		public List<PHRAccountTransactionData> GetIncomeTransactionList(PHRAccountTransactionData ObjData)
		{
			List<PHRAccountTransactionData> result = null;
			try
			{
				PHRAccountDA objDA = new PHRAccountDA();
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

		public List<PHRAccountTransactionData> GetExpensesTransactionList(PHRAccountTransactionData ObjData)
		{
			List<PHRAccountTransactionData> result = null;
			try
			{
				PHRAccountDA objDA = new PHRAccountDA();
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

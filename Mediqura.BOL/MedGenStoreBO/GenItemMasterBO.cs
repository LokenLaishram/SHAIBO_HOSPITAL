using Mediqura.CommonData.MedStore;
using Mediqura.DAL.MedGenStoreDA;
using Mediqura.DAL.MedStore;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.BOL.MedGenStoreBO
{
    public class GenItemMasterBO
    {
        public int UpdateItemMasterDetails(GenItemMasterData objGenItemMasterData)
        {
            int result = 0;
            try
            {
                GenStoreItemMasterDA objItemMasteDA = new GenStoreItemMasterDA();
                result = objItemMasteDA.UpdateItemMasterDetails(objGenItemMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<GenItemMasterData> GetItemMasterDetailsByID(GenItemMasterData objGenItemMasterData)
        {
            List<GenItemMasterData> result = null;

            try
            {
                GenStoreItemMasterDA objGenStoreItemMasterDA = new GenStoreItemMasterDA();
                result = objGenStoreItemMasterDA.GetItemMasterDetailsByID(objGenItemMasterData);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeleteItemMasterDetailsByID(GenItemMasterData objGenItemMasterData)
        {
            int result = 0;
            try
            {
                GenStoreItemMasterDA objItemMasteDA = new GenStoreItemMasterDA();
                result = objItemMasteDA.DeleteItemMasterDetailsByID(objGenItemMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<GenItemMasterData> SearchItemMasterDetails(GenItemMasterData objGenItemMasterData)
        {
            List<GenItemMasterData> result = null;
            try
            {
                GenStoreItemMasterDA objItemMasteDA = new GenStoreItemMasterDA();
                result = objItemMasteDA.SearchItemMasterDetails(objGenItemMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<GenItemMasterData> SearchItemDetails(GenItemMasterData objGenItemMasterData)
        {
            List<GenItemMasterData> result = null;
            try
            {
                GenStoreItemMasterDA objItemMasteDA = new GenStoreItemMasterDA();
                result = objItemMasteDA.SearchItemDetails(objGenItemMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<StockIssueData> GetItemDetails(StockIssueData objD)
        {
            List<StockIssueData> result = null;
            try
            {
                StockIssueDA objpatientDA = new StockIssueDA();
                result = objpatientDA.GetItemDetails(objD);

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

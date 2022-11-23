using Mediqura.CommonData.MedStore;
using Mediqura.DAL.MedStore;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.BOL.MedStore
{
    public class MedStore_ItemMasterBO
    {
        public int UpdateItemMasterDetails(ItemMasterData objItemMasterData)
        {
            int result = 0;
            try
            {
                MedStore_ItemMasterDA objItemMasteDA = new MedStore_ItemMasterDA();
                result = objItemMasteDA.UpdateItemMasterDetails(objItemMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<ItemMasterData> GetItemMasterDetailsByID(ItemMasterData objItemMasterData)
        {
            List<ItemMasterData> result = null;

            try
            {
                ItemMasterDA objItemMasterDA = new ItemMasterDA();
                result = objItemMasterDA.GetItemMasterDetailsByID(objItemMasterData);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
     
        public List<ItemMasterData> SearchItemMasterDetails(ItemMasterData objItemMasterData)
        {
            List<ItemMasterData> result = null;
            try
            {
                MedStore_ItemMasterDA objItemMasteDA = new MedStore_ItemMasterDA();
                result = objItemMasteDA.SearchItemMasterDetails(objItemMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<ItemMasterData> SearchItemDetails(ItemMasterData objItemMasterData)
        {
            List<ItemMasterData> result = null;
            try
            {
                MedStore_ItemMasterDA objItemMasteDA = new MedStore_ItemMasterDA();
                result = objItemMasteDA.SearchItemDetails(objItemMasterData);
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

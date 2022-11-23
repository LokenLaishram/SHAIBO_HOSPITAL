using Mediqura.CommonData.MedStoreData;
using Mediqura.DAL.MedStoreDA;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.BOL.MedStoreBO
{
    public class ItemGroupMasterBO
    {
        public int UpdateItemGroupDetails(ItemGroupMasterData objitemgroupData)
        {
            int result = 0;
            try
            {
                ItemGroupMasterDA objItemTypeMasteDA = new ItemGroupMasterDA();
                result = objItemTypeMasteDA.UpdateItemGroupDetails(objitemgroupData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<ItemGroupMasterData> GetItemTypeDetailsByID(ItemGroupMasterData objItemTypeMasterData)
        {
            List<ItemGroupMasterData> result = null;

            try
            {
                ItemGroupMasterDA objItemTypeMasteDA = new ItemGroupMasterDA();
                result = objItemTypeMasteDA.GetItemTypeDetailsByID(objItemTypeMasterData);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeleteItemTypeDetailsByID(ItemGroupMasterData objItemTypeMasterData)
        {
            int result = 0;
            try
            {
                ItemGroupMasterDA objItemTypeMasteDA = new ItemGroupMasterDA();
                result = objItemTypeMasteDA.DeleteItemTypeDetailsByID(objItemTypeMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<ItemGroupMasterData> SearchItemTypeDetails(ItemGroupMasterData objItemTypeMasterData)
        {
            List<ItemGroupMasterData> result = null;
            try
            {
                ItemGroupMasterDA objItemTypeMasteDA = new ItemGroupMasterDA();
                result = objItemTypeMasteDA.SearchItemTypeDetails(objItemTypeMasterData);
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

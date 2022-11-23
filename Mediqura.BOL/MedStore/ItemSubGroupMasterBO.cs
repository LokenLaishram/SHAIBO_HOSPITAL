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
    public class ItemSubGroupMasterBO
    {
        public int UpdateItemSubGroupDetails(ItemSubGroupMasterData objitemsubgroupData)
        {
            int result = 0;
            try
            {
                ItemSubGroupMasterDA objItemSubGroupMasterDA = new ItemSubGroupMasterDA();
                result = objItemSubGroupMasterDA.UpdateItemSubGroupDetails(objitemsubgroupData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<ItemSubGroupMasterData> GetItemSubGroupTypeDetailsByID(ItemSubGroupMasterData objItemSubGroupTypeMasterData)
        {
            List<ItemSubGroupMasterData> result = null;

            try
            {
                ItemSubGroupMasterDA objItemSubGroupTypeMasteDA = new ItemSubGroupMasterDA();
                result = objItemSubGroupTypeMasteDA.GetItemSubGroupTypeDetailsByID(objItemSubGroupTypeMasterData);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeleteItemSubGroupTypeDetailsByID(ItemSubGroupMasterData objItemSubGroupTypeMasterData)
        {
            int result = 0;
            try
            {
                ItemSubGroupMasterDA objItemSubGroupTypeMasteDA = new ItemSubGroupMasterDA();
                result = objItemSubGroupTypeMasteDA.DeleteItemSubGroupTypeDetailsByID(objItemSubGroupTypeMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<ItemSubGroupMasterData> SearchItemTypeDetails(ItemSubGroupMasterData objItemTypeMasterData)
        {
            List<ItemSubGroupMasterData> result = null;
            try
            {
                ItemSubGroupMasterDA objItemTypeMasteDA = new ItemSubGroupMasterDA();
                result = objItemTypeMasteDA.SearchItemSubGroupTypeDetails(objItemTypeMasterData);
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


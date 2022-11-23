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
    public class GenItemGroupBO
    {
        public int UpdateGenItemGroupDetails(GenItemGroupData objitemgroupData)
        {
            int result = 0;
            try
            {
                GenItemGroupDA objItemTypeMasteDA = new GenItemGroupDA();
                result = objItemTypeMasteDA.UpdateGenItemGroupDetails(objitemgroupData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<GenItemGroupData> GetItemTypeDetailsByID(GenItemGroupData objItemTypeMasterData)
        {
            List<GenItemGroupData> result = null;

            try
            {
                GenItemGroupDA objItemTypeMasteDA = new GenItemGroupDA();
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
        public int DeleteItemTypeDetailsByID(GenItemGroupData objItemTypeMasterData)
        {
            int result = 0;
            try
            {
                GenItemGroupDA objItemTypeMasteDA = new GenItemGroupDA();
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
        public List<GenItemGroupData> SearchItemTypeDetails(GenItemGroupData objItemTypeMasterData)
        {
            List<GenItemGroupData> result = null;
            try
            {
                GenItemGroupDA objItemTypeMasteDA = new GenItemGroupDA();
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

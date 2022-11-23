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
    public class GenItemSubGroupBO
    {
        public int UpdateItemSubGroupDetails(GenItemSubGroupData objitemsubgroupData)
        {
            int result = 0;
            try
            {
                GenItemSubGroupDA objGenItemSubGroupDA = new GenItemSubGroupDA();
                result = objGenItemSubGroupDA.UpdateItemSubGroupDetails(objitemsubgroupData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<GenItemSubGroupData> GetItemSubGroupTypeDetailsByID(GenItemSubGroupData objItemSubGroupTypeMasterData)
        {
            List<GenItemSubGroupData> result = null;

            try
            {
                GenItemSubGroupDA objItemSubGroupTypeMasteDA = new GenItemSubGroupDA();
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
        public int DeleteItemSubGroupTypeDetailsByID(GenItemSubGroupData objItemSubGroupTypeMasterData)
        {
            int result = 0;
            try
            {
                GenItemSubGroupDA objItemSubGroupTypeMasteDA = new GenItemSubGroupDA();
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
        public List<GenItemSubGroupData> SearchItemTypeDetails(GenItemSubGroupData objItemTypeMasterData)
        {
            List<GenItemSubGroupData> result = null;
            try
            {
                GenItemSubGroupDA objItemTypeMasteDA = new GenItemSubGroupDA();
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

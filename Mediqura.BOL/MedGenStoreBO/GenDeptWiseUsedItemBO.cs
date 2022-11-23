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
    public class GenDeptWiseUsedItemBO
    {
        public List<GenDeptWiseUsedItemData> GetItemNameListInStore(GenDeptWiseUsedItemData objD)
        {
            List<GenDeptWiseUsedItemData> result = null;
            try
            {
                GenDeptWiseUsedItemDA objpatientDA = new GenDeptWiseUsedItemDA();
                result = objpatientDA.GetItemNameListInStore(objD);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<GenDeptWiseUsedItemData> GetRecordNo(GenDeptWiseUsedItemData objD)
        {
            List<GenDeptWiseUsedItemData> result = null;
            try
            {
                GenDeptWiseUsedItemDA objpatientDA = new GenDeptWiseUsedItemDA();
                result = objpatientDA.GetRecordNo(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<GenDeptWiseUsedItemData> GetItemDetailsByItemID(GenDeptWiseUsedItemData objD)
        {
            List<GenDeptWiseUsedItemData> result = null;
            try
            {
                GenDeptWiseUsedItemDA objpatientDA = new GenDeptWiseUsedItemDA();
                result = objpatientDA.GetItemDetailsByItemID(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public int UpdateAvailableBalAfterdeleting(GenDeptWiseUsedItemData objstock)
        {
            int result = 0;
            try
            {
                GenDeptWiseUsedItemDA objDA = new GenDeptWiseUsedItemDA();
                result = objDA.UpdateAvailableBalAfterdeleting(objstock);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int UpdateAvailableBalAfterUsed(GenDeptWiseUsedItemData objstock)
        {
            int result = 0;
            try
            {
                GenDeptWiseUsedItemDA objDA = new GenDeptWiseUsedItemDA();
                result = objDA.UpdateAvailableBalAfterUsed(objstock);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<GenDeptWiseUsedItemData> UpdateDepartmentWiseItemUsedRecordDetails(GenDeptWiseUsedItemData objstock)
        {
            List<GenDeptWiseUsedItemData> result = null;
            try
            {
                GenDeptWiseUsedItemDA objDA = new GenDeptWiseUsedItemDA();
                result = objDA.UpdateDepartmentWiseItemUsedRecordDetails(objstock);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<GenDeptWiseUsedItemData> GetDeptWiseItemList(GenDeptWiseUsedItemData objbill)
        {
            List<GenDeptWiseUsedItemData> result = null;
            try
            {
                GenDeptWiseUsedItemDA objDA = new GenDeptWiseUsedItemDA();
                result = objDA.GetDeptWiseItemList(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public int DeleteGenDeptWiseUsedItemDetailsByRecNo(GenDeptWiseUsedItemData objData)
        {
            int result = 0;
            try
            {
                GenDeptWiseUsedItemDA objMasteDA = new GenDeptWiseUsedItemDA();
                result = objMasteDA.DeleteGenDeptWiseUsedItemDetailsByRecNo(objData);
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

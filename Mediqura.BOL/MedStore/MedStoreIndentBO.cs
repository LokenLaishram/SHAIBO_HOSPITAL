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
    public class MedStoreIndentBO
    {
		public List<MedIndentData> GetIndentnotification()
        {
            List<MedIndentData> result = null;
            try
            {
                MedStoreIndentDA objindentDA = new MedStoreIndentDA();
				result = objindentDA.GetIndentnotification();
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<MedIndentData> GetItemAvailabilty(MedIndentData objD)
        {
            List<MedIndentData> result = null;
            try
            {
                MedStoreIndentDA objindentDA = new MedStoreIndentDA();
                result = objindentDA.GetItemAvailabilty(objD);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<MedIndentData> GetMainStockitemAvailabilty(MedIndentData objD)
        {
            List<MedIndentData> result = null;
            try
            {
                MedStoreIndentDA objindentDA = new MedStoreIndentDA();
                result = objindentDA.GetMainStockitemAvailabilty(objD);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<MedIndentData> GetItemNameListInStore(MedIndentData objD)
        {
            List<MedIndentData> result = null;
            try
            {
                MedStoreIndentDA objindentDA = new MedStoreIndentDA();
                result = objindentDA.GetItemNameListInStore(objD);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<MedIndentData> GetItemNameListInStoreByBatchNo(MedIndentData objD)
        {
            List<MedIndentData> result = null;
            try
            {
                MedStoreIndentDA objindentDA = new MedStoreIndentDA();
                result = objindentDA.GetItemNameListInStoreByBatchNo(objD);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<MedIndentData> GetItemNameListInSubStore(MedIndentData objD)
        {
            List<MedIndentData> result = null;
            try
            {
                MedStoreIndentDA objindentDA = new MedStoreIndentDA();
                result = objindentDA.GetItemNameListInSubStore(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<MedIndentData> GetItemNameListInStoreBySubstockID(MedIndentData objD)
        {
            List<MedIndentData> result = null;
            try
            {
                MedStoreIndentDA objindentDA = new MedStoreIndentDA();
                result = objindentDA.GetItemNameListInStoreBySubstockID(objD);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<MedIndentData> SearchDruglistByComposition(MedIndentData objD)
        {
            List<MedIndentData> result = null;
            try
            {
                MedStoreIndentDA objindentDA = new MedStoreIndentDA();
                result = objindentDA.SearchDruglistByComposition(objD);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<MedIndentData> GetreturnItemNameListInStore(MedIndentData objD)
        {
            List<MedIndentData> result = null;
            try
            {
                MedStoreIndentDA objindentDA = new MedStoreIndentDA();
                result = objindentDA.GetreturnItemNameListInStore(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<MedIndentData> GetItemDetailsByItemID(MedIndentData objD)
        {
            List<MedIndentData> result = null;
            try
            {
                MedStoreIndentDA objindentDA = new MedStoreIndentDA();
                result = objindentDA.GetItemDetailsByItemID(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<MedIndentData> GetItemDetailsByBatchNo(MedIndentData objD)
        {
            List<MedIndentData> result = null;
            try
            {
                MedStoreIndentDA objindentDA = new MedStoreIndentDA();
                result = objindentDA.GetItemDetailsByBatchNo(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<MedIndentData> UpdateIndentItemDetails(MedIndentData objstock)
        {
            List<MedIndentData> result = null;
            try
            {
                MedStoreIndentDA objindentDA = new MedStoreIndentDA();
                result = objindentDA.UpdateIndentItemDetails(objstock);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<MedIndentData> UpdateMainShortIteemList(MedIndentData objstock)
        {
            List<MedIndentData> result = null;
            try
            {
                MedStoreIndentDA objindentDA = new MedStoreIndentDA();
                result = objindentDA.UpdateMainShortIteemList(objstock);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<MedIndentData> GetIndentItemList(MedIndentData objbill)
        {
            List<MedIndentData> result = null;
            try
            {
                MedStoreIndentDA objindentDA = new MedStoreIndentDA();
                result = objindentDA.GetIndentItemList(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<MedIndentData> GetSubstockItemRequestableList(MedIndentData objbill)
        {
            List<MedIndentData> result = null;
            try
            {
                MedStoreIndentDA objindentDA = new MedStoreIndentDA();
                result = objindentDA.GetSubstockItemRequestableList(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<MedIndentData> GeMainStockSortItemList(MedIndentData objbill)
        {
            List<MedIndentData> result = null;
            try
            {
                MedStoreIndentDA objindentDA = new MedStoreIndentDA();
                result = objindentDA.GeMainStockSortItemList(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeleteIndentReqByID(MedIndentData objstd)
        {
            int result = 0;

            try
            {
                MedStoreIndentDA objindentDA = new MedStoreIndentDA();
                result = objindentDA.DeleteIndentReqByID(objstd);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<MedIndentData> GetAutoStockAvailability(MedIndentData objbill)
        {
            List<MedIndentData> result = null;
            try
            {
                MedStoreIndentDA objindentDA = new MedStoreIndentDA();
                result = objindentDA.GetAutoStockAvailability(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<MedIndentData> GetIndentList(MedIndentData objbill)
        {
            List<MedIndentData> result = null;
            try
            {
                MedStoreIndentDA objindentDA = new MedStoreIndentDA();
                result = objindentDA.GetIndentList(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<MedIndentData> GetIndentDetailsByIndentNo(MedIndentData objbill)
        {
            List<MedIndentData> result = null;
            try
            {
                MedStoreIndentDA objindentDA = new MedStoreIndentDA();
                result = objindentDA.GetIndentDetailsByIndentNo(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<MedIndentData> GetPreapproveditemlistbyindentnumber(MedIndentData objbill)
        {
            List<MedIndentData> result = null;
            try
            {
                MedStoreIndentDA objindentDA = new MedStoreIndentDA();
                result = objindentDA.GetPreapproveditemlistbyindentnumber(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<MedIndentData> CheckStockitemavailable(MedIndentData objbill)
        {
            List<MedIndentData> result = null;
            try
            {
                MedStoreIndentDA objindentDA = new MedStoreIndentDA();
                result = objindentDA.CheckStockitemavailable(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<MedIndentData> GetItemDetailsByStockNumbers(MedIndentData objbill)
        {
            List<MedIndentData> result = null;
            try
            {
                MedStoreIndentDA objindentDA = new MedStoreIndentDA();
                result = objindentDA.GetItemDetailsByStockNumbers(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<MedIndentData> UpdateMedIndentApprovedQtyDetail(MedIndentData objstock)
        {
            List<MedIndentData> result = null;
            try
            {
                MedStoreIndentDA objindentDA = new MedStoreIndentDA();
                result = objindentDA.UpdateMedIndentApprovedQtyDetail(objstock);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeletepreapprovedItem(MedIndentData objstock)
        {
            int result = 0;
            try
            {
                MedStoreIndentDA objindentDA = new MedStoreIndentDA();
                result = objindentDA.DeletepreapprovedItem(objstock);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int UpdateMedIndentDetail(MedIndentData objstock)
        {
            int result = 0;
            try
            {
                MedStoreIndentDA objindentDA = new MedStoreIndentDA();
                result = objindentDA.UpdateMedIndentDetail(objstock);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<MedIndentData> GetMedSubstockDetails(MedIndentData objbill)
        {
            List<MedIndentData> result = null;
            try
            {
                MedStoreIndentDA objindentDA = new MedStoreIndentDA();
                result = objindentDA.GetMedSubstockDetails(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<MedIndentData> UpdateStockReturnDetails(MedIndentData objD)
        {
            List<MedIndentData> result = null;
            try
            {
                MedStoreIndentDA objindentDA = new MedStoreIndentDA();
                result = objindentDA.UpdateStockReturnDetails(objD);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<MedIndentData> GetReturnItemList(MedIndentData objbill)
        {
            List<MedIndentData> result = null;
            try
            {
                MedStoreIndentDA objindentDA = new MedStoreIndentDA();
                result = objindentDA.GetReturnItemList(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public int DeleteStockReurnByID(MedIndentData objstd)
        {
            int result = 0;

            try
            {
                MedStoreIndentDA objindentDA = new MedStoreIndentDA();
                result = objindentDA.DeleteStockReurnByID(objstd);

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

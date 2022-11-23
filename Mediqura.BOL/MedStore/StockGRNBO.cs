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
    public class StockGRNBO
    {
        public List<StockGRNData> GetCompanyName(StockGRNData objD)
        {
            List<StockGRNData> result = null;
            try
            {
                StockGRNDA objpatientDA = new StockGRNDA();
                result = objpatientDA.GetCompanyName(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<StockGRNData> Get_ItemName(StockGRNData objD)
        {
            List<StockGRNData> result = null;
            try
            {
                StockGRNDA objpatientDA = new StockGRNDA();
                result = objpatientDA.Get_ItemName(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        //Get Return Item  Name//
        public List<StockGRNData> GetReturnItemName(StockGRNData objD)
        {
            List<StockGRNData> result = null;
            try
            {
                StockGRNDA objdrugDA = new StockGRNDA();
                result = objdrugDA.GetReturnItemName(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<StockGRNData> GetItemNameWithID(StockGRNData objD)
        {
            List<StockGRNData> result = null;
            try
            {
                StockGRNDA objpatientDA = new StockGRNDA();
                result = objpatientDA.GetItemNameWithID(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<StockGRNData> GetStockItemDetailsBySubStockID(StockGRNData objD)
        {
            List<StockGRNData> result = null;
            try
            {
                StockGRNDA objpatientDA = new StockGRNDA();
                result = objpatientDA.GetStockItemDetailsBySubStockID(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<StockGRNData> GenerateMedStoreTransactionID(StockGRNData objD)
        {
            List<StockGRNData> result = null;
            try
            {
                StockGRNDA objpatientDA = new StockGRNDA();
                result = objpatientDA.GenerateMedStoreTransactionID(objD);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<StockGRNData> UpdateMedStoreTransactionID(StockGRNData objD)
        {
            List<StockGRNData> result = null;
            try
            {
                StockGRNDA objpatientDA = new StockGRNDA();
                result = objpatientDA.UpdateMedStoreTransactionID(objD);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<StockGRNData> UpdateMedStockTransactiondetails(StockGRNData objD)
        {
            List<StockGRNData> result = null;
            try
            {
                StockGRNDA objpatientDA = new StockGRNDA();
                result = objpatientDA.UpdateMedStockTransactiondetails(objD);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<StockGRNData> GetMedStockTransactiondetails(StockGRNData objD)
        {
            List<StockGRNData> result = null;
            try
            {
                StockGRNDA objpatientDA = new StockGRNDA();
                result = objpatientDA.GetMedStockTransactiondetails(objD);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<StockGRNData> GetItemName(StockGRNData objD)
        {
            List<StockGRNData> result = null;
            try
            {
                StockGRNDA objpatientDA = new StockGRNDA();
                result = objpatientDA.GetItemName(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<StockGRNData> GetStockItemNames(StockGRNData objD)
        {
            List<StockGRNData> result = null;
            try
            {
                StockGRNDA objpatientDA = new StockGRNDA();
                result = objpatientDA.GetStockItemNames(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<StockGRNData> GetGenItemName(StockGRNData objD)
        {
            List<StockGRNData> result = null;
            try
            {
                StockGRNDA objpatientDA = new StockGRNDA();
                result = objpatientDA.GetGenItemName(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<StockGRNData> GetItemNameGEN(StockGRNData objD)
        {
            List<StockGRNData> result = null;
            try
            {
                StockGRNDA objpatientDA = new StockGRNDA();
                result = objpatientDA.GetItemNameGEN(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<StockGRNData> GetPhrItemName(StockGRNData objD)
        {
            List<StockGRNData> result = null;
            try
            {
                StockGRNDA objpatientDA = new StockGRNDA();
                result = objpatientDA.GetPhrItemName(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<StockGRNData> GetItemNames(StockGRNData objD)
        {
            List<StockGRNData> result = null;
            try
            {
                StockGRNDA objpatientDA = new StockGRNDA();
                result = objpatientDA.GetItemNames(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<StockGRNData> GetrecdNo(StockGRNData objD)
        {
            List<StockGRNData> result = null;
            try
            {
                StockGRNDA objpatientDA = new StockGRNDA();
                result = objpatientDA.GetrecdNo(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<StockGRNData> GetPONo(StockGRNData objD)
        {
            List<StockGRNData> result = null;
            try
            {
                StockGRNDA objpatientDA = new StockGRNDA();
                result = objpatientDA.GetPONo(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<StockGRNData> GetautoPONo(StockGRNData objD)
        {
            List<StockGRNData> result = null;
            try
            {
                StockGRNDA objpatientDA = new StockGRNDA();
                result = objpatientDA.GetautoPONo(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<StockGRNData> GetStockItemTransferList(StockGRNData objD)
        {
            List<StockGRNData> result = null;
            try
            {
                StockGRNDA objpatientDA = new StockGRNDA();
                result = objpatientDA.GetStockItemTransferList(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<StockGRNData> GetStockTransferList(StockGRNData objD)
        {
            List<StockGRNData> result = null;
            try
            {
                StockGRNDA objpatientDA = new StockGRNDA();
                result = objpatientDA.GetStockTransferList(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }

        public int UpdateStockTransfer(StockGRNData objstock)
        {
            int result = 0;
            try
            {
                StockGRNDA objDA = new StockGRNDA();
                result = objDA.UpdateStockTransfer(objstock);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeleteStockTransferByIssueno(StockGRNData objstd)
        {
            int result = 0;

            try
            {
                StockGRNDA objDA = new StockGRNDA();
                result = objDA.DeleteStockTransferByIssueno(objstd);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }

        public List<StockGRNData> GetSupplier(StockGRNData objD)
        {
            List<StockGRNData> result = null;
            try
            {
                StockGRNDA objpatientDA = new StockGRNDA();
                result = objpatientDA.GetSupplier(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<StockGRNData> Get_ItemNameByID(StockGRNData objD)
        {
            List<StockGRNData> result = null;
            try
            {
                StockGRNDA objpatientDA = new StockGRNDA();
                result = objpatientDA.Get_ItemNameByID(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<StockGRNData> GetSupplierName(StockGRNData objD)
        {
            List<StockGRNData> result = null;
            try
            {
                StockGRNDA objpatientDA = new StockGRNDA();
                result = objpatientDA.GetSupplierName(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public int UpdateStockItemDetails(StockGRNData objstock)
        {
            int result = 0;
            try
            {
                StockGRNDA objDA = new StockGRNDA();
                result = objDA.UpdateStockItemDetails(objstock);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<StockGRNData> GetStockItemList(StockGRNData objbill)
        {
            List<StockGRNData> result = null;
            try
            {
                StockGRNDA objDA = new StockGRNDA();
                result = objDA.GetStockItemList(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<StockGRNData> GetItemCheckList(StockGRNData objbill)
        {
            List<StockGRNData> result = null;
            try
            {
                StockGRNDA objDA = new StockGRNDA();
                result = objDA.GetItemCheckList(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<StockGRNData> GetItemApprovalNoList(StockGRNData objbill)
        {
            List<StockGRNData> result = null;
            try
            {
                StockGRNDA objDA = new StockGRNDA();
                result = objDA.GetItemApprovalNoList(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<StockGRNData> GetItemApprovalList(StockGRNData objbill)
        {
            List<StockGRNData> result = null;
            try
            {
                StockGRNDA objDA = new StockGRNDA();
                result = objDA.GetItemApprovalList(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public int DeletePurchaseLIstByID(StockGRNData objstd)
        {
            int result = 0;

            try
            {
                StockGRNDA objDA = new StockGRNDA();
                result = objDA.DeletePurchaseLIstByID(objstd);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public int DeletePOCheckLIstByID(StockGRNData objstd)
        {
            int result = 0;

            try
            {
                StockGRNDA objDA = new StockGRNDA();
                result = objDA.DeletePOCheckLIstByID(objstd);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<StockGRNData> GetStockistItemList(StockGRNData objbill)
        {
            List<StockGRNData> result = null;
            try
            {
                StockGRNDA objDA = new StockGRNDA();
                result = objDA.GetStockistItemList(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public int DeleteStockReturnItemListByID(StockGRNData objstd)
        {
            int result = 0;

            try
            {
                StockGRNDA objDA = new StockGRNDA();
                result = objDA.DeleteStockReturnItemListByID(objstd);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public int DeleteStockistReturnItemListByID(StockGRNData objstd)
        {
            int result = 0;

            try
            {
                StockGRNDA objDA = new StockGRNDA();
                result = objDA.DeleteStockistReturnItemListByID(objstd);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<StockGRNData> GetItemList(StockGRNData objbill)
        {
            List<StockGRNData> result = null;
            try
            {
                StockGRNDA objDA = new StockGRNDA();
                result = objDA.GetItemList(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<StockGRNData> GetDiscountreqDetailsforPayment(StockGRNData objbill)
        {
            List<StockGRNData> result = null;
            try
            {
                StockGRNDA objDA = new StockGRNDA();
                result = objDA.GetDiscountreqDetailsforPayment(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeleteItemListByID(StockGRNData objstd)
        {
            int result = 0;

            try
            {
                StockGRNDA objDA = new StockGRNDA();
                result = objDA.DeleteItemListByID(objstd);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public int DeleteStockItemListByID(StockGRNData objstd)
        {
            int result = 0;

            try
            {
                StockGRNDA objDA = new StockGRNDA();
                result = objDA.DeleteStockItemListByID(objstd);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeleteMedstoreTransactionByID(StockGRNData objstd)
        {
            int result = 0;

            try
            {
                StockGRNDA objDA = new StockGRNDA();
                result = objDA.DeleteMedstoreTransactionByID(objstd);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public int DeleteMedDiscountRequestByID(StockGRNData objstd)
        {
            int result = 0;

            try
            {
                StockGRNDA objDA = new StockGRNDA();
                result = objDA.DeleteMedDiscountRequestByID(objstd);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public int DeleteMedOPDbill(StockGRNData objstd)
        {
            int result = 0;

            try
            {
                StockGRNDA objDA = new StockGRNDA();
                result = objDA.DeleteMedOPDbill(objstd);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public int UpdateStockCondemnedItemDetails(StockGRNData objstock)
        {
            int result = 0;
            try
            {
                StockGRNDA objDA = new StockGRNDA();
                result = objDA.UpdateStockCondemnedItemDetails(objstock);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int UpdateStockistReturn(StockGRNData objstock)
        {
            int result = 0;
            try
            {
                StockGRNDA objDA = new StockGRNDA();
                result = objDA.UpdateStockistReturn(objstock);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<StockGRNData> GetStockistItemReturnList(StockGRNData objbill)
        {
            List<StockGRNData> result = null;
            try
            {
                StockGRNDA objDA = new StockGRNDA();
                result = objDA.GetStockistItemReturnList(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public int UpdatePurchaseCheckItemList(StockGRNData objstock)
        {
            int result = 0;
            try
            {
                StockGRNDA objDA = new StockGRNDA();
                result = objDA.UpdatePurchaseCheckItemList(objstock);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int UpdatePurchaseOrder(StockGRNData objstock)
        {
            int result = 0;
            try
            {
                StockGRNDA objDA = new StockGRNDA();
                result = objDA.UpdatePurchaseOrder(objstock);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int UpdatePOCrossChecK(StockGRNData objstock)
        {
            int result = 0;
            try
            {
                StockGRNDA objDA = new StockGRNDA();
                result = objDA.UpdatePOCrossChecK(objstock);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<StockGRNData> GetPurchaseList(StockGRNData objbill)
        {
            List<StockGRNData> result = null;
            try
            {
                StockGRNDA objDA = new StockGRNDA();
                result = objDA.GetPurchaseList(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<StockGRNData> GetPOCheckedList(StockGRNData objbill)
        {
            List<StockGRNData> result = null;
            try
            {
                StockGRNDA objDA = new StockGRNDA();
                result = objDA.GetPOCheckedList(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<StockGRNData> GetPurchseOrderList(StockGRNData objbill)
        {
            List<StockGRNData> result = null;
            try
            {
                StockGRNDA objDA = new StockGRNDA();
                result = objDA.GetPurchaseOrderList(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;  
        }

        //------------------------------------Start Discount Request List Tab 1--------------------------------

        public List<StockGRNData> GetRQNumberAuto(StockGRNData ObjDisReqData)
        {
            List<StockGRNData> result = null;
            try
            {
                StockGRNDA ObjDisReqDA = new StockGRNDA();
                result = ObjDisReqDA.GetRQNumberAuto(ObjDisReqData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<StockGRNData> GetDiscountRequestList(StockGRNData ObjDisReqData)
        {
            List<StockGRNData> result = null;
            try
            {
                StockGRNDA ObjDisReqDA = new StockGRNDA();
                result = ObjDisReqDA.GetDiscountRequestList(ObjDisReqData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<StockGRNData> GetRequestDetailListByRQNumber(StockGRNData ObjDisReqData)
        {
            List<StockGRNData> result = null;
            try
            {
                StockGRNDA ObjDisReqDA = new StockGRNDA();
                result = ObjDisReqDA.GetRequestDetailListByRQNumber(ObjDisReqData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        //------------------------------------End Discount Request List Tab 1--------------------------------
        //------------------------------------Start Discount Request List Tab 2--------------------------------
        public int UpdateDiscountApprovedList(StockGRNData ObjDisReqData)
        {
            int result = 0;
            try
            {
                StockGRNDA ObjDisReqDA = new StockGRNDA();
                result = ObjDisReqDA.UpdateDiscountApprovedList(ObjDisReqData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }    
        //------------------------------------End Discount Request List Tab 2--------------------------------

        //------------------------------------Start Discount Request After Billing Tab 1--------------------------------
        public List<StockGRNData> GetBillNumberAuto(StockGRNData ObjDisReqData)
        {
            List<StockGRNData> result = null;
            try
            {
                StockGRNDA ObjDisReqDA = new StockGRNDA();
                result = ObjDisReqDA.GetBillNumberAuto(ObjDisReqData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }

        public List<StockGRNData> GetPatientDetailsByBillNo(StockGRNData ObjDisReqData)
        {
            List<StockGRNData> result = null;
            try
            {
                StockGRNDA ObjDisReqDA = new StockGRNDA();
                result = ObjDisReqDA.GetPatientDetailsByBillNo(ObjDisReqData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }

        public string SaveDiscountRequestAfterBilling(StockGRNData ObjDisReqData)
        {
            string result = null;
            try
            {
                StockGRNDA ObjDisReqDA = new StockGRNDA();
                result = ObjDisReqDA.SaveDiscountRequestAfterBilling(ObjDisReqData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }


        //------------------------------------End Discount Request After Billing Tab 1--------------------------------
        //------------------------------------Start Discount Request After Billing Tab 2--------------------------------

        public List<StockGRNData> GetDiscountRequestListAfterBilling(StockGRNData ObjDisReqData)
        {
            List<StockGRNData> result = null;
            try
            {
                StockGRNDA ObjDisReqDA = new StockGRNDA();
                result = ObjDisReqDA.GetDiscountRequestListAfterBilling(ObjDisReqData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<StockGRNData> GetDiscountApproveDetailsForRefund(StockGRNData ObjDisReqData)
        {
            List<StockGRNData> result = null;
            try
            {
                StockGRNDA ObjDisReqDA = new StockGRNDA();
                result = ObjDisReqDA.GetDiscountApproveDetailsForRefund(ObjDisReqData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeleteDiscountRequestByReqNo(StockGRNData ObjDisReqData)
        {
            int result = 0;
            try
            {
                StockGRNDA ObjDisReqDA = new StockGRNDA();
                result = ObjDisReqDA.DeleteDiscountRequestByReqNo(ObjDisReqData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        } 

        //------------------------------------End Discount Request After Billing Tab 2--------------------------------
        //------------------------------------Start Discount Request After Billing Tab 3--------------------------------
        public string SaveRefundDiscountRequest(StockGRNData ObjDisReqData)
        {
            string result = null;
            try
            {
                StockGRNDA ObjDisReqDA = new StockGRNDA();
                result = ObjDisReqDA.SaveRefundDiscountRequest(ObjDisReqData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }

        //------------------------------------End Discount Request After Billing Tab 3--------------------------------

        //------------------------------------Start Discount Request After Billing Tab 4--------------------------------
        public List<StockGRNData> GetRFNumberAuto(StockGRNData ObjDisReqData)
        {
            List<StockGRNData> result = null;
            try
            {
                StockGRNDA ObjDisReqDA = new StockGRNDA();
                result = ObjDisReqDA.GetRFNumberAuto(ObjDisReqData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        
        public List<StockGRNData> GetRefundListForAfterBiling(StockGRNData ObjDisReqData)
        {
            List<StockGRNData> result = null;
            try
            {
                StockGRNDA ObjDisReqDA = new StockGRNDA();
                result = ObjDisReqDA.GetRefundListForAfterBiling(ObjDisReqData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeleteRefundDiscountAfterBillingByReqNo(StockGRNData ObjDisReqData)
        {
            int result = 0;
            try
            {
                StockGRNDA ObjDisReqDA = new StockGRNDA();
                result = ObjDisReqDA.DeleteRefundDiscountAfterBillingByReqNo(ObjDisReqData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        } 

        //------------------------------------End Discount Request After Billing Tab 4--------------------------------

		public List<StockGRNData> GetStockItemDetailsByStockID(StockGRNData objStock)
		{
			List<StockGRNData> result = null;
			try
			{
				StockGRNDA objpatientDA = new StockGRNDA();
				result = objpatientDA.GetStockItemDetailsByStockID(objStock);

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

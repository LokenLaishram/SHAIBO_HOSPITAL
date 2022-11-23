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
    public class StockIssueBO
    {
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
        public List<StockIssueData> SearchIssueditem(StockIssueData objD)
        {
            List<StockIssueData> result = null;
            try
            {
                StockIssueDA objpatientDA = new StockIssueDA();
                result = objpatientDA.SearchIssueditem(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<StockIssueData> GetItemtoissueList(StockIssueData objD)
        {
            List<StockIssueData> result = null;
            try
            {
                StockIssueDA objpatientDA = new StockIssueDA();
                result = objpatientDA.GetItemtoissueList(objD);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }

        public List<StockIssueData> GetItemNameListinSubStock(StockIssueData objD)
        {
            List<StockIssueData> result = null;
            try
            {
                StockIssueDA objpatientDA = new StockIssueDA();
                result = objpatientDA.GetItemNameListinSubStock(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }

        public List<StockIssueData> GetStockPrices(StockIssueData objD)
        {
            List<StockIssueData> result = null;
            try
            {
                StockIssueDA objpatientDA = new StockIssueDA();
                result = objpatientDA.GetStockPrices(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public int UpdateStockIssueDetails(StockIssueData objstock)
        {
            int result = 0;
            try
            {
                StockIssueDA objDA = new StockIssueDA();
                result = objDA.UpdateStockIssueDetails(objstock);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<StockIssueData> GetIssueNo(StockIssueData objD)
        {
            List<StockIssueData> result = null;
            try
            {
                StockIssueDA objpatientDA = new StockIssueDA();
                result = objpatientDA.GetIssueNo(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<StockIssueData> GetIndentNo(StockIssueData objD)
        {
            List<StockIssueData> result = null;
            try
            {
                StockIssueDA objpatientDA = new StockIssueDA();
                result = objpatientDA.GetindentNo(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<StockIssueData> GetReturnNo(StockIssueData objD)
        {
            List<StockIssueData> result = null;
            try
            {
                StockIssueDA objpatientDA = new StockIssueDA();
                result = objpatientDA.GetReturnNo(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<StockIssueData> GetItemName(StockIssueData objD)
        {
            List<StockIssueData> result = null;
            try
            {
                StockIssueDA objpatientDA = new StockIssueDA();
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
        public List<StockIssueData> GetStockIssuedList(StockIssueData objbill)
        {
            List<StockIssueData> result = null;
            try
            {
                StockIssueDA objDA = new StockIssueDA();
                result = objDA.GetStockIssuedList(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public int DeleteStockIssuedItemListByID(StockIssueData objstd)
        {
            int result = 0;

            try
            {
                StockIssueDA objDA = new StockIssueDA();
                result = objDA.DeleteStockIssuedItemListByID(objstd);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<StockIssueData> GetItemNameWithStockNO(StockIssueData objD)
        {
            List<StockIssueData> result = null;
            try
            {
                StockIssueDA objpatientDA = new StockIssueDA();
                result = objpatientDA.GetItemNameWithStockNO(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<StockIssueData> GetStockReturnList(StockIssueData objbill)
        {
            List<StockIssueData> result = null;
            try
            {
                StockIssueDA objDA = new StockIssueDA();
                result = objDA.GetStockReturnList(objbill);
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

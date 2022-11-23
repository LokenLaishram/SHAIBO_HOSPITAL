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
    public class GenStockStatusBO
    {
        public List<StockStatusData> Get_StockStatus(StockStatusData objDischargeIntimationData)
        {
            List<StockStatusData> result = null;
            try
            {
                GenStockStatusDA objLabSubGroupTypeMasteDA = new GenStockStatusDA();
                result = objLabSubGroupTypeMasteDA.Get_StockStatus(objDischargeIntimationData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<StockStatusData> GetBatchNo(StockStatusData objD)
        {
            List<StockStatusData> result = null;
            try
            {
                GenStockStatusDA objpatientDA = new GenStockStatusDA();
                result = objpatientDA.GetBatchNo(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<StockStatusData> GetRecNumber(StockStatusData objD)
        {
            List<StockStatusData> result = null;
            try
            {
                GenStockStatusDA objpatientDA = new GenStockStatusDA();
                result = objpatientDA.GetRecNumber(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<StockStatusData> GetPoNumbers(StockStatusData objD)
        {
            List<StockStatusData> result = null;
            try
            {
                GenStockStatusDA objpatientDA = new GenStockStatusDA();
                result = objpatientDA.GetPoNumbers(objD);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<StockStatusData> GetStockNumbers(StockStatusData objD)
        {
            List<StockStatusData> result = null;
            try
            {
                GenStockStatusDA objpatientDA = new GenStockStatusDA();
                result = objpatientDA.GetStockNumbers(objD);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int UpdateStockIssueDetails(StockStatusData objdischarge)
        {
            int result = 0;
            try
            {
                GenStockStatusDA objDA = new GenStockStatusDA();
                result = objDA.UpdateStockIssueDetails(objdischarge);
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

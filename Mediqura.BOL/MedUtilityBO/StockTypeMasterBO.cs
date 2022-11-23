using Mediqura.CommonData.MedUtilityData;
using Mediqura.DAL.MedUtilityDA;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.BOL.MedUtilityBO
{
    public class StockTypeMasterBO
    {
        public int UpdateStockTypeDetails(StockTypeMasterData objPaymentTypeMasterData)
        {
            int result = 0;
            try
            {
                StockTypeMasterDA objPaymentTypeMasteDA = new StockTypeMasterDA();
                result = objPaymentTypeMasteDA.UpdateStockTypeDetails(objPaymentTypeMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<StockTypeMasterData> GetStockTypeDetailsByID(StockTypeMasterData objPaymentTypeMasterData)
        {
            List<StockTypeMasterData> result = null;

            try
            {
                StockTypeMasterDA objPaymentTypeMasteDA = new StockTypeMasterDA();
                result = objPaymentTypeMasteDA.GetStockTypeDetailsByID(objPaymentTypeMasterData);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeleteStockTypeDetailsByID(StockTypeMasterData objPaymentTypeMasterData)
        {
            int result = 0;
            try
            {
                StockTypeMasterDA objPaymentTypeMasterDA = new StockTypeMasterDA();
                result = objPaymentTypeMasterDA.DeleteStockTypeDetailsByID(objPaymentTypeMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<StockTypeMasterData> SearchStockTypeDetails(StockTypeMasterData objPaymentTypeMasterData)
        {
            List<StockTypeMasterData> result = null;
            try
            {
                StockTypeMasterDA objPaymentTypeMasteDA = new StockTypeMasterDA();
                result = objPaymentTypeMasteDA.SearchStockTypeDetails(objPaymentTypeMasterData);
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


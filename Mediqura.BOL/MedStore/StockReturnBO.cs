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
    public class StockReturnBO
    {
        public int UpdateStockReturnDetails(StockReturnData objstock)
        {
            int result = 0;
            try
            {
                StockReturnDA objDA = new StockReturnDA();
                result = objDA.UpdateStockReturnDetails(objstock);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<StockReturnData> GetStockReturnList(StockReturnData objbill)
        {
            List<StockReturnData> result = null;
            try
            {
                StockReturnDA objDA = new StockReturnDA();
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
        public List<StockReturnData> Getstock_ReturnDetails(StockReturnData objbill)
        {
            List<StockReturnData> result = null;
            try
            {
                StockReturnDA objDA = new StockReturnDA();
                result = objDA.Getstock_ReturnDetails(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public int DeleteStockReturnItemListByID(StockReturnData objstd)
        {
            int result = 0;

            try
            {
                StockReturnDA objDA = new StockReturnDA();
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

        //---------------------------------------------Start Pharmacy Stock Return----------------------------------------------

        public List<StockReturnData> GetStockReturnListItem(StockReturnData ObjStockReturnData)
        {
            List<StockReturnData> result = null;
            try
            {
                StockReturnDA objDA = new StockReturnDA();
                result = objDA.GetStockReturnListItem(ObjStockReturnData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public string SaveStockReturnList(StockReturnData ObjStockReturnData)
        {
            string result = null;
            try
            {
                StockReturnDA objDA = new StockReturnDA();
                result = objDA.SaveStockReturnList(ObjStockReturnData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }

        //---------------------------------------------End Pharmacy Stock Return----------------------------------------------

    }
}

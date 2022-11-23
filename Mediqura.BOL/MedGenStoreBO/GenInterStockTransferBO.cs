using Mediqura.CommonData.MedStore;
using Mediqura.DAL.MedGenStoreDA;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.BOL.MedGenStoreBO
{
    public class GenInterStockTransferBO
    {
        public List<GenInterStockTransferData> GetSubStockItemName(GenInterStockTransferData objD)
        {
            List<GenInterStockTransferData> result = null;
            try
            {
                GenInterStockTransferDA objTransferDA = new GenInterStockTransferDA();
                result = objTransferDA.GetSubStockItemName(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<GenInterStockTransferData> GetInterItemDetailsByStockNumbers(GenInterStockTransferData objbill)
        {
            List<GenInterStockTransferData> result = null;
            try
            {
                GenInterStockTransferDA objDA = new GenInterStockTransferDA();
                result = objDA.GetInterItemDetailsByStockNumbers(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<GenInterStockTransferData> UpdateInterStockTransferDetails(GenInterStockTransferData objstock)
        {
            List<GenInterStockTransferData> result = null;
            try
            {
                GenInterStockTransferDA objDA = new GenInterStockTransferDA();
                result = objDA.UpdateInterStockTransferDetails(objstock);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        //-------------TAB2-----------------//
        public List<GenInterStockTransferData> GetTransferStockItemList(GenInterStockTransferData objcondemn)
        {
            List<GenInterStockTransferData> result = null;
            try
            {
                GenInterStockTransferDA objDA = new GenInterStockTransferDA();
                result = objDA.GetTransferStockItemList(objcondemn);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public int DeleteTranferStockByID(GenInterStockTransferData objtransfer)
        {
            int result = 0;

            try
            {
                GenInterStockTransferDA objDA = new GenInterStockTransferDA();
                result = objDA.DeleteTranferStockByID(objtransfer);

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

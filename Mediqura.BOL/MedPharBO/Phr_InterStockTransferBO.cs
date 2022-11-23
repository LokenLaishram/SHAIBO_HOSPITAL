using Mediqura.CommonData.MedPharData;
using Mediqura.DAL.MedPharDA;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.BOL.MedPharBO
{
    public class Phr_InterStockTransferBO
    {
        public List<Phr_InterStockTransferData> GetMedSubStockItemName(Phr_InterStockTransferData objD)
        {
            List<Phr_InterStockTransferData> result = null;
            try
            {
                Phr_InterStockTransferDA objTransferDA = new Phr_InterStockTransferDA();
                result = objTransferDA.GetMedSubStockItemName(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<Phr_InterStockTransferData> Get_Phr_ItemDetailsBySubStockNo(Phr_InterStockTransferData objbill)
        {
            List<Phr_InterStockTransferData> result = null;
            try
            {
                Phr_InterStockTransferDA objDA = new Phr_InterStockTransferDA();
                result = objDA.Get_Phr_ItemDetailsBySubStockNo(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<Phr_InterStockTransferData> Update_Phr_InterStockTransferDetails(Phr_InterStockTransferData objstock)
        {
            List<Phr_InterStockTransferData> result = null;
            try
            {
                Phr_InterStockTransferDA objDA = new Phr_InterStockTransferDA();
                result = objDA.Update_Phr_InterStockTransferDetails(objstock);
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
        public List<Phr_InterStockTransferData> GetTransferStockItemList(Phr_InterStockTransferData objcondemn)
        {
            List<Phr_InterStockTransferData> result = null;
            try
            {
                Phr_InterStockTransferDA objDA = new Phr_InterStockTransferDA();
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
        public int DeleteTranferStockByID(Phr_InterStockTransferData objtransfer)
        {
            int result = 0;

            try
            {
                Phr_InterStockTransferDA objDA = new Phr_InterStockTransferDA();
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

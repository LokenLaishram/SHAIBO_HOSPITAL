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
    public class CondemnReqApprovedBO
    {
        public List<CondemnReqApprovedData> GetSubStockItemName(CondemnReqApprovedData objD)
        {
            List<CondemnReqApprovedData> result = null;
            try
            {
                CondemnReqApprovedDA objCondemnDA = new CondemnReqApprovedDA();
                result = objCondemnDA.GetSubStockItemName(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<CondemnReqApprovedData> UpdateCondemnStockItemDetails(CondemnReqApprovedData objstock)
        {
            List<CondemnReqApprovedData> result = null;
            try
            {
                CondemnReqApprovedDA objDA = new CondemnReqApprovedDA();
                result = objDA.UpdateCondemnStockItemDetails(objstock);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        //----- TAB 2 -------
        public List<CondemnReqApprovedData> GetCondemnStockItemList(CondemnReqApprovedData objcondemn)
        {
            List<CondemnReqApprovedData> result = null;
            try
            {
                CondemnReqApprovedDA objDA = new CondemnReqApprovedDA();
                result = objDA.GetCondemnStockItemList(objcondemn);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<CondemnReqApprovedData> SearchChildDetailByNo(CondemnReqApprovedData objitemData)
        {
            List<CondemnReqApprovedData> result = null;

            try
            {
                CondemnReqApprovedDA objitemDA = new CondemnReqApprovedDA();
                result = objitemDA.SearchChildDetailByNo(objitemData);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeleteCondemnStockItemListByID(CondemnReqApprovedData objstd)
        {
            int result = 0;

            try
            {
                CondemnReqApprovedDA objDA = new CondemnReqApprovedDA();
                result = objDA.DeleteCondemnStockItemListByID(objstd);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        ////----- Approval Page -------
      
        public List<CondemnReqApprovedData> GetCondemnList(CondemnReqApprovedData objbill)
        {
            List<CondemnReqApprovedData> result = null;
            try
            {
                CondemnReqApprovedDA objDA = new CondemnReqApprovedDA(); 
                result = objDA.GetCondemnList(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<CondemnReqApprovedData> GetCondemnDetailsList(CondemnReqApprovedData objcondemn)
        {
            List<CondemnReqApprovedData> result = null;
            try
            {
                CondemnReqApprovedDA objDA = new CondemnReqApprovedDA();
                result = objDA.GetCondemnDetailsList(objcondemn);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public int UpdateCondemnApptrovedItem(CondemnReqApprovedData objConAppr)
        {
            int result = 0;
            try
            {
                CondemnReqApprovedDA objDA = new CondemnReqApprovedDA();
                result = objDA.UpdateCondemnApptrovedItem(objConAppr);
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

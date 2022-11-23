using Mediqura.CommonData.MedStore;
using Mediqura.DAL.MedGenStoreDA;
using Mediqura.DAL.POCreationDA;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.BOL.MedGenStoreBO
{
    public class POCrossCheckBO
    {
        public List<StockGRNData> GetPurchseOrderList(StockGRNData objbill)
        {
            List<StockGRNData> result = null;
            try
            {
                POCrossCheckDA objDA = new POCrossCheckDA();
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
        public int UpdatePOCrossChecK(StockGRNData objstock)
        {
            int result = 0;
            try
            {
                POCrossCheckDA objDA = new POCrossCheckDA();
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
        public List<StockGRNData> GetPOCheckedList(StockGRNData objbill)
        {
            List<StockGRNData> result = null;
            try
            {
                POCrossCheckDA objDA = new POCrossCheckDA();
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
  

    }
}

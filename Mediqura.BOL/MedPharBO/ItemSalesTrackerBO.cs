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
    public class ItemSalesTrackerBO
    {
        public List<ItemSalesTrackerData> GetItemName(ItemSalesTrackerData objD)
        {
            List<ItemSalesTrackerData> result = null;
            try
            {
                ItemSalesTrackerDA objSaleDA = new ItemSalesTrackerDA();
                result = objSaleDA.GetItemName(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<ItemSalesTrackerData> GetSaleItemList(ItemSalesTrackerData objcondemn)
        {
            List<ItemSalesTrackerData> result = null;
            try
            {
                ItemSalesTrackerDA objDA = new ItemSalesTrackerDA();
                result = objDA.GetSaleItemList(objcondemn);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<ItemSalesTrackerData> GetItemSaleDetailsList(ItemSalesTrackerData objcondemn)
        {
            List<ItemSalesTrackerData> result = null;
            try
            {
                ItemSalesTrackerDA objDA = new ItemSalesTrackerDA();
                result = objDA.GetItemSaleDetailsList(objcondemn);
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

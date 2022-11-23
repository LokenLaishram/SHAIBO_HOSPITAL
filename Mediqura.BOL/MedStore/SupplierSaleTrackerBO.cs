using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mediqura.CommonData.MedStore;
using Mediqura.DAL.MedStore;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;

namespace Mediqura.BOL.MedStore
{
    public class SupplierSaleTrackerBO
    {
        public List<SupplierSaleTrackerData> GetItemName(SupplierSaleTrackerData ObjData)
        {
            List<SupplierSaleTrackerData> result = null;
            try
            {
                SupplierSaleTrackerDA ObjDA = new SupplierSaleTrackerDA();
                result = ObjDA.GetItemName(ObjData);   
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result; 
        }
        public List<SupplierSaleTrackerData> GetSupplierSaleItemList(SupplierSaleTrackerData ObjData)
        {
            List<SupplierSaleTrackerData> result = null;
            try
            {
                SupplierSaleTrackerDA ObjDA = new SupplierSaleTrackerDA();
                result = ObjDA.GetSupplierSaleItemList(ObjData);
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

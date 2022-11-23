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
    public class VendorStockReturnBO
    {
        public List<VendorStockReturnData> GetAutoRecieptNo(VendorStockReturnData objD)
        {
            List<VendorStockReturnData> result = null;
            try
            {
                VendorStockReturnDA objVendorReturnDA = new VendorStockReturnDA();
                result = objVendorReturnDA.GetAutoRecieptNo(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<VendorStockReturnData> GetReturnStockList(VendorStockReturnData objreturn)
        {
            List<VendorStockReturnData> result = null;
            try
            {
                VendorStockReturnDA objDA = new VendorStockReturnDA();
                result = objDA.GetReturnStockList(objreturn);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<VendorStockReturnData> UpdateVendorReturnStockDetails(VendorStockReturnData objReturnStock)
        {
            List<VendorStockReturnData> result = null;
            try
            {
                VendorStockReturnDA objDA = new VendorStockReturnDA();
                result = objDA.UpdateVendorReturnStockDetails(objReturnStock);
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

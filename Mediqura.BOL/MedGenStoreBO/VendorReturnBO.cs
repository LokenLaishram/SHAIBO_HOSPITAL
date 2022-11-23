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
    public class VendorReturnBO
    {
        public List<GENStrData> GetPONo(GENStrData objD)
        {
            List<GENStrData> result = null;
            try
            {
                VendorReturnDA objpatientDA = new VendorReturnDA();
                result = objpatientDA.GetPONo(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<GENStrData> GetStockistItemList(GENStrData objbill)
        {
            List<GENStrData> result = null;
            try
            {
                VendorReturnDA objDA = new VendorReturnDA();
                result = objDA.GetStockistItemList(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public int UpdateStockistReturn(GENStrData objstock)
        {
            int result = 0;
            try
            {
                VendorReturnDA objDA = new VendorReturnDA();
                result = objDA.UpdateStockistReturn(objstock);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<GENStrData> GetStockistItemReturnList(GENStrData objbill)
        {
            List<GENStrData> result = null;
            try
            {
                VendorReturnDA objDA = new VendorReturnDA();
                result = objDA.GetStockistItemReturnList(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public int DeleteStockistReturnItemListByID(GENStrData objstd)
        {
            int result = 0;

            try
            {
                VendorReturnDA objDA = new VendorReturnDA();
                result = objDA.DeleteStockistReturnItemListByID(objstd);

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

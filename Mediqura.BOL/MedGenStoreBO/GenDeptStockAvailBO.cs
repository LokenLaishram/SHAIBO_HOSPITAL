using Mediqura.CommonData.MedStore;
using Mediqura.DAL.MedGenStoreDA;
using Mediqura.DAL.MedStore;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Mediqura.BOL.MedGenStoreBO
{
    public class GenDeptStockAvailBO
    {
        public List<GenDeptStockAvailibilityData> GetStockAvailList(GenDeptStockAvailibilityData objbill)
        {
            List<GenDeptStockAvailibilityData> result = null;
            try
            {
                GenDeptStockAvailDA objDA = new GenDeptStockAvailDA();
                result = objDA.GetStockAvailList(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public int UpdateDeptStockAvails(GenDeptStockAvailibilityData obj)
        {
            int result = 0;
            try
            {
                GenDeptStockAvailDA objDA = new GenDeptStockAvailDA();
                result = objDA.UpdateDeptStockAvails(obj);
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

using Mediqura.CommonData.MedStore;
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
    public class POCreationBO
    {
        public List<GENStrData> GetItemCheckList(GENStrData objbill)
        {
            List<GENStrData> result = null;
            try
            {
                POCreationDA objDA = new POCreationDA();
                result = objDA.GetItemCheckList(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public int UpdatePurchaseCheckItemList(GENStrData objstock)
        {
            int result = 0;
            try
            {
                POCreationDA objDA = new POCreationDA();
                result = objDA.UpdatePurchaseCheckItemList(objstock);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<GENStrData> GetItemList(GENStrData objbill)
        {
            List<GENStrData> result = null;
            try
            {
                POCreationDA objDA = new POCreationDA();
                result = objDA.GetItemList(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public int DeleteItemListByID(GENStrData objstd)
        {
            int result = 0;

            try
            {
                POCreationDA objDA = new POCreationDA();
                result = objDA.DeleteItemListByID(objstd);

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

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
    public class POApprovalBO
    {
        public List<GENStrData> GetItemApprovalNoList(GENStrData objbill)
        {
            List<GENStrData> result = null;
            try
            {
                POApprovalDA objDA = new POApprovalDA();
                result = objDA.GetItemApprovalNoList(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<GENStrData> GetItemApprovalList(GENStrData objbill)
        {
            List<GENStrData> result = null;
            try
            {
                POApprovalDA objDA = new POApprovalDA();
                result = objDA.GetItemApprovalList(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<GENStrData> UpdatePurchaseOrder(GENStrData objstock)
        {
            List<GENStrData> result;
            try
            {
                POApprovalDA objDA = new POApprovalDA();
                result = objDA.UpdatePurchaseOrder(objstock);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<GENStrData> GetPurchaseList(GENStrData objbill)
        {
            List<GENStrData> result = null;
            try
            {
                POCreationDA objDA = new POCreationDA();
                result = objDA.GetPurchaseList(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<GENStrData> GetautoPONo(GENStrData objD)
        {
            List<GENStrData> result = null;
            try
            {
                POApprovalDA objpatientDA = new POApprovalDA();
                result = objpatientDA.GetautoPONo(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public int DeletePurchaseLIstByID(GENStrData objstd)
        {
            int result = 0;

            try
            {
                POApprovalDA objDA = new POApprovalDA();
                result = objDA.DeletePurchaseLIstByID(objstd);

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

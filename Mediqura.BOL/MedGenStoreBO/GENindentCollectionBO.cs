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
    public class GENindentCollectionBO
    {
        public List<GenIndentData> GetIndentList(GenIndentData objbill)
        {
            List<GenIndentData> result = null;
            try
            {
                GENindentCollectionDA objDA = new GENindentCollectionDA();
                result = objDA.GetIndentList(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<GenIndentData> bindIndentRecvList(GenIndentData objbill)
        {
            List<GenIndentData> result = null;
            try
            {
                GENindentCollectionDA objDA = new GENindentCollectionDA();
                result = objDA.bindIndentRecvList(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<GenIndentData> GetRecvList(GenIndentData objbill)
        {
            List<GenIndentData> result = null;
            try
            {
                GENindentCollectionDA objDA = new GENindentCollectionDA();
                result = objDA.GetRecvList(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<GenIndentData> GetApprvList(GenIndentData objbill)
        {
            List<GenIndentData> result = null;
            try
            {
                GENindentCollectionDA objDA = new GENindentCollectionDA();
                result = objDA.GetApprvList(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }

        public List<GenIndentData> GetItemNameListInStock(GenIndentData objD)
        {
            List<GenIndentData> result = null;
            try
            {
                GENindentCollectionDA objpatientDA = new GENindentCollectionDA();
                result = objpatientDA.GetItemNameListInStock(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<GenIndentData> GetItemDetailsByStockID(GenIndentData objD)
        {
            List<GenIndentData> result = null;
            try
            {
                GENindentCollectionDA objpatientDA = new GENindentCollectionDA();
                result = objpatientDA.GetItemDetailsByStockID(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public int UpdateIndentItemDetails(GenIndentData objstock)
        {
            int result = 0;
            try
            {
                GENindentCollectionDA objDA = new GENindentCollectionDA();
                result = objDA.UpdateIndentItemDetails(objstock);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<GenIndentData> GetIndentList1(GenIndentData objbill)
        {
            List<GenIndentData> result = null;
            try
            {
                GENindentCollectionDA objDA = new GENindentCollectionDA();
                result = objDA.GetIndentList1(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<GenIndentData> GetApprvDetail(GenIndentData objbill)
        {
            List<GenIndentData> result = null;
            try
            {
                GENindentCollectionDA objDA = new GENindentCollectionDA();
                result = objDA.GetApprvDetail(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<GenIndentData> GetHndoverDetail(GenIndentData objbill)
        {
            List<GenIndentData> result = null;
            try
            {
                GENindentCollectionDA objDA = new GENindentCollectionDA();
                result = objDA.GetHndoverDetail(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public int UpdateIndent_status(GenIndentData objdischarge)
        {
            int result = 0;
            try
            {
                GENindentCollectionDA objDA = new GENindentCollectionDA();
                result = objDA.UpdateIndent_status(objdischarge);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<GenIndentData> IndentListDetailsByID(GenIndentData objIndentStatusMasterData)
        {
            List<GenIndentData> result = null;

            try
            {
                GENindentCollectionDA objIndentStatusDA = new GENindentCollectionDA();
                result = objIndentStatusDA.IndentListDetailsByID(objIndentStatusMasterData);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int UpdateIndentDetail(GenIndentData objstock)
        {
            int result = 0;
            try
            {
                GENindentCollectionDA objDA = new GENindentCollectionDA();
                result = objDA.UpdateIndentDetail(objstock);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int UpdateHandoverDetail(GenIndentData objstock)
        {
            int result = 0;
            try
            {
                GENindentCollectionDA objDA = new GENindentCollectionDA();
                result = objDA.UpdateHandoverDetail(objstock);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int UpdateReceivedDetail(GenIndentData objstock)
        {
            int result = 0;
            try
            {
                GENindentCollectionDA objDA = new GENindentCollectionDA();
                result = objDA.UpdateReceivedDetail(objstock);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<GenIndentData> GetHandOverList(GenIndentData objbill)
        {
            List<GenIndentData> result = null;
            try
            {
                GENindentCollectionDA objDA = new GENindentCollectionDA();
                result = objDA.GetHandOverList(objbill);
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

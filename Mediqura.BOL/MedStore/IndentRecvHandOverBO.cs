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
    public class IndentRecvHandOverBO      
    {
        public List<IndentRecvHandOverData> GetIndentList(IndentRecvHandOverData objbill)
        {
            List<IndentRecvHandOverData> result = null;
            try
            {
                IndentRecvHandOverDA objDA = new IndentRecvHandOverDA();
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
        public List<IndentRecvHandOverData> bindIndentRecvList(IndentRecvHandOverData objbill)
        {
            List<IndentRecvHandOverData> result = null;
            try
            {
                IndentRecvHandOverDA objDA = new IndentRecvHandOverDA();
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
        public List<IndentRecvHandOverData> GetRecvList(IndentRecvHandOverData objbill)
        {
            List<IndentRecvHandOverData> result = null;
            try
            {
                IndentRecvHandOverDA objDA = new IndentRecvHandOverDA();
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
        public List<IndentRecvHandOverData> GetApprvList(IndentRecvHandOverData objbill)
        {
            List<IndentRecvHandOverData> result = null;
            try
            {
                IndentRecvHandOverDA objDA = new IndentRecvHandOverDA();
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

        public List<IndentRecvHandOverData> GetItemNameListInStock(IndentRecvHandOverData objD)
        {
            List<IndentRecvHandOverData> result = null;
            try
            {
                IndentRecvHandOverDA objpatientDA = new IndentRecvHandOverDA();
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
        public List<IndentRecvHandOverData> GetItemDetailsByStockID(IndentRecvHandOverData objD)
        {
            List<IndentRecvHandOverData> result = null;
            try
            {
                IndentRecvHandOverDA objpatientDA = new IndentRecvHandOverDA();
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
        public int UpdateIndentItemDetails(IndentRecvHandOverData objstock)
        {
            int result = 0;
            try
            {
                IndentRecvHandOverDA objDA = new IndentRecvHandOverDA();
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
        public List<IndentRecvHandOverData> GetIndentList1(IndentRecvHandOverData objbill)
        {
            List<IndentRecvHandOverData> result = null;
            try
            {
                IndentRecvHandOverDA objDA = new IndentRecvHandOverDA();
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
        public List<IndentRecvHandOverData> GetApprvDetail(IndentRecvHandOverData objbill)
        {
            List<IndentRecvHandOverData> result = null;
            try
            {
                IndentRecvHandOverDA objDA = new IndentRecvHandOverDA();
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
        public List<IndentRecvHandOverData> GetHndoverDetail(IndentRecvHandOverData objbill)
        {
            List<IndentRecvHandOverData> result = null;
            try
            {
                IndentRecvHandOverDA objDA = new IndentRecvHandOverDA();
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
        public int UpdateIndent_status(IndentRecvHandOverData objdischarge)
        {
            int result = 0;
            try
            {
                IndentRecvHandOverDA objDA = new IndentRecvHandOverDA();
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
        public List<IndentRecvHandOverData> IndentListDetailsByID(IndentRecvHandOverData objIndentStatusMasterData)   
        {
            List<IndentRecvHandOverData> result = null;

            try
            {
                IndentRecvHandOverDA objIndentStatusDA = new IndentRecvHandOverDA();
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
        public int UpdateIndentDetail(IndentRecvHandOverData objstock)
        {
            int result = 0;
            try
            {
                IndentRecvHandOverDA objDA = new IndentRecvHandOverDA();
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
        public int UpdateHandoverDetail(IndentRecvHandOverData objstock)
        {
            int result = 0;
            try
            {
                IndentRecvHandOverDA objDA = new IndentRecvHandOverDA();
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
        public int UpdateReceivedDetail(IndentRecvHandOverData objstock)
        {
            int result = 0;
            try
            {
                IndentRecvHandOverDA objDA = new IndentRecvHandOverDA();
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
        public List<IndentRecvHandOverData> GetHandOverList(IndentRecvHandOverData objbill)
        {
            List<IndentRecvHandOverData> result = null;
            try
            {
                IndentRecvHandOverDA objDA = new IndentRecvHandOverDA();
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

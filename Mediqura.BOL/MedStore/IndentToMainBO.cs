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
    public class IndentToMainBO
    {
        public List<IndentToMainData> GetItemNameListInStock(IndentToMainData objD)
        {
            List<IndentToMainData> result = null;
            try
            {
                IndentToMainDA objpatientDA = new IndentToMainDA();
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
        public List<IndentToMainData> GetItemDetailsByStockID(IndentToMainData objD)
        {
            List<IndentToMainData> result = null;
            try
            {
                IndentToMainDA objpatientDA = new IndentToMainDA();
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
        public int UpdateIndentItemDetails(IndentToMainData objstock)
        {
            int result = 0;
            try
            {
                IndentToMainDA objDA = new IndentToMainDA();
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
        public List<IndentToMainData> GetIndentItemList(IndentToMainData objbill)
        {
            List<IndentToMainData> result = null;
            try
            {
                IndentToMainDA objDA = new IndentToMainDA();
                result = objDA.GetIndentItemList(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public int UpdateIndent_status(IndentToMainData objdischarge)
        {
            int result = 0;
            try
            {
                IndentToMainDA objDA = new IndentToMainDA();
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
        public List<IndentToMainData> IndentListDetailsByID(IndentToMainData objIndentStatusMasterData)
        {
            List<IndentToMainData> result = null;

            try
            {
                IndentToMainDA objIndentStatusDA = new IndentToMainDA();
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
        public int DeleteIndentReqByID(IndentToMainData objstd)
        {
            int result = 0;

            try
            {
                IndentToMainDA objDA = new IndentToMainDA();
                result = objDA.DeleteIndentReqByID(objstd);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<IndentToMainData> GetIndentList1(IndentToMainData objbill)
        {
            List<IndentToMainData> result = null;
            try
            {
                IndentToMainDA objDA = new IndentToMainDA();
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

    }
}

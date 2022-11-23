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
   public class GenIndentApprovedBO
    {
        public List<GenIndentData> GetIndentList(GenIndentData objbill)
        {
            List<GenIndentData> result = null;
            try
            {
                GenIndentApprovedDA objDA = new GenIndentApprovedDA();
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
        public List<GenIndentData> GetIndentListforVerification(GenIndentData objbill)
        {
            List<GenIndentData> result = null;
            try
            {
                GenIndentApprovedDA objDA = new GenIndentApprovedDA();
                result = objDA.GetIndentListforVerification(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<GenIndentData> GetPreapproveditemlistbyindentnumber(GenIndentData objbill)
        {
            List<GenIndentData> result = null;
            try
            {
                GenIndentApprovedDA objDA = new GenIndentApprovedDA();
                result = objDA.GetPreapproveditemlistbyindentnumber(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<GenIndentData> GetItemDetailsByStockNumbers(GenIndentData objbill)
        {
            List<GenIndentData> result = null;
            try
            {
                GenIndentApprovedDA objDA = new GenIndentApprovedDA();
                result = objDA.GetItemDetailsByStockNumbers(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<GenIndentData> GetCondemnItemDetailsByStockNumbers(GenIndentData objbill)
        {
            List<GenIndentData> result = null;
            try
            {
                GenIndentApprovedDA objDA = new GenIndentApprovedDA();
                result = objDA.GetCondemnItemDetailsByStockNumbers(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<GenIndentData> CheckStockitemavailable(GenIndentData objbill)
        {
            List<GenIndentData> result = null;
            try
            {
                GenIndentApprovedDA objDA = new GenIndentApprovedDA();
                result = objDA.CheckStockitemavailable(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<GenIndentData> GetAutoStockAvailability(GenIndentData objbill)
        {
            List<GenIndentData> result = null;
            try
            {
                GenIndentApprovedDA objDA = new GenIndentApprovedDA();
                result = objDA.GetAutoStockAvailability(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<GenIndentData> GetIndentNumberbyStockID(GenIndentData objbill)
        {
            List<GenIndentData> result = null;
            try
            {
                GenIndentApprovedDA objDA = new GenIndentApprovedDA();
                result = objDA.GetIndentNumberbyStockID(objbill);
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
                GenIndentApprovedDA objDA = new GenIndentApprovedDA();
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
                GenIndentApprovedDA objDA = new GenIndentApprovedDA();
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
                GenIndentApprovedDA objDA = new GenIndentApprovedDA();
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
                GenIndentApprovedDA objpatientDA = new GenIndentApprovedDA();
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
                GenIndentApprovedDA objpatientDA = new GenIndentApprovedDA();
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
                GenIndentApprovedDA objDA = new GenIndentApprovedDA();
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
        public int DeletepreapprovedItem(GenIndentData objstock)
        {
            int result = 0;
            try
            {
                GenIndentApprovedDA objDA = new GenIndentApprovedDA();
                result = objDA.DeletepreapprovedItem(objstock);
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
                GenIndentApprovedDA objDA = new GenIndentApprovedDA();
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
                GenIndentApprovedDA objDA = new GenIndentApprovedDA();
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
                GenIndentApprovedDA objDA = new GenIndentApprovedDA();
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
                GenIndentApprovedDA objDA = new GenIndentApprovedDA();
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
                GenIndentApprovedDA objIndentStatusDA = new GenIndentApprovedDA();
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
        public int UpdateGENIndentDetail(GenIndentData objstock)
        {
            int result = 0;
            try
            {
                GenIndentApprovedDA objDA = new GenIndentApprovedDA();
                result = objDA.UpdateGENIndentDetail(objstock);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int UpdateIndentVerification(GenIndentData objstock)
        {
            int result = 0;
            try
            {
                GenIndentApprovedDA objDA = new GenIndentApprovedDA();
                result = objDA.UpdateIndentVerification(objstock);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<GenIndentData> UpdateGENIndentApprovedQtyDetail(GenIndentData objstock)
        {
            List<GenIndentData> result = null;
            try
            {
                GenIndentApprovedDA objDA = new GenIndentApprovedDA();
                result = objDA.UpdateGENIndentApprovedQtyDetail(objstock);
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
                GenIndentApprovedDA objDA = new GenIndentApprovedDA();
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
                GenIndentApprovedDA objDA = new GenIndentApprovedDA();
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
                GenIndentApprovedDA objDA = new GenIndentApprovedDA();
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

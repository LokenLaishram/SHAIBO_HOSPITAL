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
    public class GenIndentBO
    {
        public List<GenIndentData> GetItemNameListInStore(GenIndentData objD)
        {
            List<GenIndentData> result = null;
            try
            {
                GenIndentDA objpatientDA = new GenIndentDA();
                result = objpatientDA.GetItemNameListInStore(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<GenIndentData> GetItemAvailabilty(GenIndentData objD)
        {
            List<GenIndentData> result = null;
            try
            {
                GenIndentDA objpatientDA = new GenIndentDA();
                result = objpatientDA.GetItemAvailabilty(objD);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<GenIndentData> GetItemDetailsByItemID(GenIndentData objD)
        {
            List<GenIndentData> result = null;
            try
            {
                GenIndentDA objpatientDA = new GenIndentDA();
                result = objpatientDA.GetItemDetailsByItemID(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<GenIndentData> GetUserByDeptID(GenIndentData objD)
        {
            List<GenIndentData> result = null;
            try
            {
                GenIndentDA objpatientDA = new GenIndentDA();
                result = objpatientDA.GetUserByDeptID(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<GenIndentData> GetDeptByEmpID(GenIndentData objD)
        {
            List<GenIndentData> result = null;
            try
            {
                GenIndentDA objpatientDA = new GenIndentDA();
                result = objpatientDA.GetDeptByEmpID(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<GenIndentData> UpdateIndentItemDetails(GenIndentData objstock)
        {
            List<GenIndentData> result = null;
            try
            {
                GenIndentDA objDA = new GenIndentDA();
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
        public List<GenIndentData> GetIndentItemList(GenIndentData objbill)
        {
            List<GenIndentData> result = null;
            try
            {
                GenIndentDA objDA = new GenIndentDA();
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
        public int UpdateIndent_status(GenIndentData objdischarge)
        {
            int result = 0;
            try
            {
                GenIndentDA objDA = new GenIndentDA();
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
                GenIndentDA objIndentStatusDA = new GenIndentDA();
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
        public int DeleteIndentReqByID(GenIndentData objstd)
        {
            int result = 0;

            try
            {
                GenIndentDA objDA = new GenIndentDA();
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
        public List<GenIndentData> GetIndentList1(GenIndentData objbill)
        {
            List<GenIndentData> result = null;
            try
            {
                GenIndentDA objDA = new GenIndentDA();
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
        public List<GenIndentData> GetAvailableByID(GenIndentData objdata)
        {
            List<GenIndentData> result = null;
            try
            {
                GenIndentDA objDA = new GenIndentDA();
                result = objDA.GetAvailableByID(objdata);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<GenIndentData> GetItemNameListInApprvTemp(GenIndentData objD)
        {
            List<GenIndentData> result = null;
            try
            {
                GenIndentDA objpatientDA = new GenIndentDA();
                result = objpatientDA.GetItemNameListInApprvTemp(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<GenIndentData> UpdateStockReturnDetails(GenIndentData objD)
        {
            List<GenIndentData> result = null;
            try
            {
                GenIndentDA objpatientDA = new GenIndentDA();
                result = objpatientDA.UpdateStockReturnDetails(objD);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<GenIndentData> GetReturnItemList(GenIndentData objbill)
        {
            List<GenIndentData> result = null;
            try
            {
                GenIndentDA objDA = new GenIndentDA();
                result = objDA.GetReturnItemList(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public int DeleteStockReurnByID(GenIndentData objstd)
        {
            int result = 0;

            try
            {
                GenIndentDA objDA = new GenIndentDA();
                result = objDA.DeleteStockReurnByID(objstd);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<GenIndentData> GetDeptStockList(GenIndentData objbill)
        {
            List<GenIndentData> result = null;
            try
            {
                GenIndentDA objDA = new GenIndentDA();
                result = objDA.GetDeptStockList(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<GenIndentData> GetNeedtoPurchageitemlist(GenIndentData objbill)
        {
            List<GenIndentData> result = null;
            try
            {
                GenIndentDA objDA = new GenIndentDA();
                result = objDA.GetNeedtoPurchageitemlist(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public int UpdateDeptStock(GenIndentData objoutsource)
        {
            int result = 0;
            try
            {
                GenIndentDA objDA = new GenIndentDA();
                result = objDA.UpdateDeptStock(objoutsource);
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

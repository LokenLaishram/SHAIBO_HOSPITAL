using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mediqura.CommonData.MedStore;
using Mediqura.DAL.MedStore;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;

namespace Mediqura.BOL.MedStore
{
    public class PurchaseRequisitionBO
    {
        public List<PurchaseRequisitionData> GetItemNameAuto(PurchaseRequisitionData ObjPurReqData)
        {
            List<PurchaseRequisitionData> result = null;
            try
            {
                PurchaseRequisitionDA ObjPurReqDA = new PurchaseRequisitionDA();
                result = ObjPurReqDA.GetItemNameAuto(ObjPurReqData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<PurchaseRequisitionData> GetUnitDescriptionByID(PurchaseRequisitionData ObjPurReqData)
        {
            List<PurchaseRequisitionData> result = null;
            try
            {
                PurchaseRequisitionDA ObjPurReqDA = new PurchaseRequisitionDA();
                result = ObjPurReqDA.GetUnitDescriptionByID(ObjPurReqData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }          
        public List<PurchaseRequisitionData> SavePurchaseRequisitionList(PurchaseRequisitionData ObjPurReqData)
        {
            List<PurchaseRequisitionData> result = null;
            try
            {
                PurchaseRequisitionDA objDA = new PurchaseRequisitionDA();
                result = objDA.SavePurchaseRequisitionList(ObjPurReqData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<PurchaseRequisitionData> UpdatePurchaseRequisitionList(PurchaseRequisitionData ObjPurReqData)
        {
            List<PurchaseRequisitionData> result = null;
            try
            {
                PurchaseRequisitionDA objDA = new PurchaseRequisitionDA();
                result = objDA.UpdatePurchaseRequisitionList(ObjPurReqData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }

        //------------------------------------- End Tab 1 ---------------------------------------
        //------------------------------------- Start Tab 2 ---------------------------------------

        public List<PurchaseRequisitionData> GetRQNumberAuto(PurchaseRequisitionData ObjPurReqData)
        {
            List<PurchaseRequisitionData> result = null;
            try
            {
                PurchaseRequisitionDA ObjPurReqDA = new PurchaseRequisitionDA();
                result = ObjPurReqDA.GetRQNumberAuto(ObjPurReqData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<PurchaseRequisitionData> GetPurchaseRequisitionList(PurchaseRequisitionData ObjPurReqData)
        {
            List<PurchaseRequisitionData> result = null;
            try
            {
                PurchaseRequisitionDA ObjPurReqDA = new PurchaseRequisitionDA();
                result = ObjPurReqDA.GetPurchaseRequisitionList(ObjPurReqData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }

        public int DeletePurchaseRequisitionByID(PurchaseRequisitionData ObjPurReqData)
        {
            int result = 0;
            try
            {
                PurchaseRequisitionDA objDA = new PurchaseRequisitionDA();
                result = objDA.DeletePurchaseRequisitionByID(ObjPurReqData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        //------------------------------------- End Purchase Requisition Page Tab 2 ---------------------------------------

        //------------------------------------- Start Purchase Approval Page Tab 1 ---------------------------------------
        public List<PurchaseRequisitionData> GetPurchaseRequisitionListForApproval(PurchaseRequisitionData ObjPurReqData)
        {
            List<PurchaseRequisitionData> result = null;
            try
            {
                PurchaseRequisitionDA ObjPurReqDA = new PurchaseRequisitionDA();
                result = ObjPurReqDA.GetPurchaseRequisitionListForApproval(ObjPurReqData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        
        public List<PurchaseRequisitionData> GetPurReqListByRQNumber(PurchaseRequisitionData ObjPurReqData)
        {
            List<PurchaseRequisitionData> result = null;
            try
            {
                PurchaseRequisitionDA ObjPurReqDA = new PurchaseRequisitionDA();
                result = ObjPurReqDA.GetPurReqListByRQNumber(ObjPurReqData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        //------------------------------------- End Purchase Approval Page Tab 1 ---------------------------------------
        //------------------------------------- Start Purchase Approval Page Tab 2 ---------------------------------------

        public List<PurchaseRequisitionData> GetPONumberAuto(PurchaseRequisitionData ObjPurReqData)
        {
            List<PurchaseRequisitionData> result = null;
            try
            {
                PurchaseRequisitionDA ObjPurReqDA = new PurchaseRequisitionDA();
                result = ObjPurReqDA.GetPONumberAuto(ObjPurReqData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int UpdatePurchaseApproveList(PurchaseRequisitionData ObjPurReqData)
        {
            int result = 0;
            try
            {
                PurchaseRequisitionDA objDA = new PurchaseRequisitionDA();
                result = objDA.UpdatePurchaseApproveList(ObjPurReqData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }

        //------------------------------------- End Purchase Approval Page Tab 2 ---------------------------------------
        //------------------------------------- Start Purchase Order Page Tab 1 ---------------------------------------
        public List<PurchaseRequisitionData> GetPurchaseRequisitionListForPO(PurchaseRequisitionData ObjPurReqData)
        {
            List<PurchaseRequisitionData> result = null;
            try
            {
                PurchaseRequisitionDA ObjPurReqDA = new PurchaseRequisitionDA();
                result = ObjPurReqDA.GetPurchaseRequisitionListForPO(ObjPurReqData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<PurchaseRequisitionData> GetPurReqListByRQNumberForPO(PurchaseRequisitionData ObjPurReqData)
        {
            List<PurchaseRequisitionData> result = null;
            try
            {
                PurchaseRequisitionDA ObjPurReqDA = new PurchaseRequisitionDA();
                result = ObjPurReqDA.GetPurReqListByRQNumberForPO(ObjPurReqData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }

        //------------------------------------- End Purchase Order Page Tab 1 ---------------------------------------
        //------------------------------------- Start Purchase Order Page Tab 2 ---------------------------------------
        public List<PurchaseRequisitionData> GeneratePurchaseOrderList(PurchaseRequisitionData ObjPurReqData)
        {
            List<PurchaseRequisitionData> result = null;
            try
            {
                PurchaseRequisitionDA objDA = new PurchaseRequisitionDA();
                result = objDA.GeneratePurchaseOrderList(ObjPurReqData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }

        //------------------------------------- End Purchase Order Page Tab 2 ---------------------------------------
        //------------------------------------- Start Purchase Cross Checking Page Tab 1 ---------------------------------------
        public List<PurchaseRequisitionData> GetPurchaseOrderList(PurchaseRequisitionData ObjPurReqData)
        {
            List<PurchaseRequisitionData> result = null;
            try
            {
                PurchaseRequisitionDA ObjPurReqDA = new PurchaseRequisitionDA();
                result = ObjPurReqDA.GetPurchaseOrderList(ObjPurReqData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }

        public List<PurchaseRequisitionData> GetPurchaseOrderListByRQNumber(PurchaseRequisitionData ObjPurReqData)
        {
            List<PurchaseRequisitionData> result = null;
            try
            {
                PurchaseRequisitionDA ObjPurReqDA = new PurchaseRequisitionDA();
                result = ObjPurReqDA.GetPurchaseOrderListByRQNumber(ObjPurReqData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        //------------------------------------- End Purchase Cross Checking Page Tab 1 ---------------------------------------
        //------------------------------------- Start Purchase Cross Checking Page Tab 2 ---------------------------------------

        public int UpdatePurchaseOrderRecievedList(PurchaseRequisitionData ObjPurReqData)
        {
            int result = 0;
            try
            {
                PurchaseRequisitionDA objDA = new PurchaseRequisitionDA();
                result = objDA.UpdatePurchaseOrderRecievedList(ObjPurReqData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }

        //------------------------------------- End Purchase Cross Checking Page Tab 2 ---------------------------------------
    }
}


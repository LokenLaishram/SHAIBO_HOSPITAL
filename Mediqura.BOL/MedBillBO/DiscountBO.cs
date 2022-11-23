using Mediqura.CommonData.MedBillData;
using Mediqura.DAL.MedBillDA;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.BOL.MedBillBO
{
   public class DiscountBO
    {
       public List<DiscountRequestData> GetBillNoByServiceType(DiscountRequestData objData)
        {
            List<DiscountRequestData> result = null;
            try
            {
                DiscountDA objDA = new DiscountDA();
                result = objDA.GetBillNoByServiceType(objData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
       public List<DiscountRequestData> GetPatientDetailsByBillNo(DiscountRequestData objData)
       {
           List<DiscountRequestData> result = null;
           try
           {
               DiscountDA objDA = new DiscountDA();
               result = objDA.GetPatientDetailsByBillNo(objData);
           }
           catch (Exception ex)
           {
               PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
               LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
               throw new BusinessProcessException("4000001", ex);
           }
           return result;

       }
       public List<DiscountRequestData> GetServiceListByBillNo(DiscountRequestData objData)
       {
           List<DiscountRequestData> result = null;
           try
           {
               DiscountDA objDA = new DiscountDA();
               result = objDA.GetServiceListByBillNo(objData);
           }
           catch (Exception ex)
           {
               PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
               LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
               throw new BusinessProcessException("4000001", ex);
           }
           return result;

       }
        public List<DiscountOutput> UpdateDiscoutRequest(DiscountRequestServiceData objdata)
        {


            List<DiscountOutput> result;

            try
            {
                DiscountDA objDA = new DiscountDA();
                result = objDA.UpdateDiscoutRequest(objdata);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<DiscountCount> GetDiscountNotification(int ID)
        {
            List<DiscountCount> result = null;
            try
            {
                DiscountDA objDA = new DiscountDA();
                result = objDA.GetDiscountNotification(ID);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<DiscountRequestListData> GetDiscountList(DiscountRequestListData objData)
        {
            List<DiscountRequestListData> result = null;
            try
            {
                DiscountDA objDA = new DiscountDA();
                result = objDA.GetDiscountList(objData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<DiscountRequestListData> GetDiscountListAdmin(DiscountRequestListData objData)
        {
            List<DiscountRequestListData> result = null;
            try
            {
                DiscountDA objDA = new DiscountDA();
                result = objDA.GetDiscountListAdmin(objData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public int DeleteDiscountRequestByID(DiscountRequestListData objData)
        {
            int result = 0;

            try
            {
                DiscountDA objDA = new DiscountDA();
                result = objDA.DeleteDiscountRequestByID(objData);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<DiscountRequestServiceData> GetServiceListByReqNo(DiscountRequestData objData)
        {
            List<DiscountRequestServiceData> result = null;
            try
            {
                DiscountDA objDA = new DiscountDA();
                result = objDA.GetServiceListByReqNo(objData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public int UpdateDiscoutApproval(DiscountRequestServiceData objdata)
        {


            int result;

            try
            {
                DiscountDA objDA = new DiscountDA();
                result = objDA.UpdateDiscoutApproval(objdata);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<DiscountRequestServiceData> GetApprovalServiceListByReqNo(DiscountRequestData objData)
        {
            List<DiscountRequestServiceData> result = null;
            try
            {
                DiscountDA objDA = new DiscountDA();
                result = objDA.GetApprovalServiceListByReqNo(objData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public int UpdateApprovalShare(DiscountRequestServiceData objdata)
        {


            int result;

            try
            {
                DiscountDA objDA = new DiscountDA();
                result = objDA.UpdateApprovalShare(objdata);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<DiscountRefundData> GetDiscountDetailsByBillID(Int64 ID)
        {
            List<DiscountRefundData> result = null;
            try
            {
                DiscountDA objDA = new DiscountDA();
                result = objDA.GetDiscountDetailsByBillID(ID);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<DiscountOutput> UpdateDiscountRequestForAdmission(AdmissionDiscountData objdata)
        {


            List<DiscountOutput> result;

            try
            {
                DiscountDA objDA = new DiscountDA();
                result = objDA.UpdateDiscountRequestForAdmission(objdata);
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

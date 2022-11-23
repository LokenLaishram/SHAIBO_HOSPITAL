using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mediqura.CommonData.AdmissionData;
using Mediqura.DAL.MedBillDA;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using Mediqura.DAL.AdmissionDA;
using Mediqura.CommonData.MedHrData;

namespace Mediqura.BOL.AdmissionBO
{
    public class DueCollectionBO
    {
        public List<DueCollectionData> GetDueCollectionList(DueCollectionData objbill)
        {
            List<DueCollectionData> result = null;
            try
            {
                DueCollectionDA objDA = new DueCollectionDA();
                result = objDA.GetDueCollectionList(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<EmployeeData> GetRespEmployees(EmployeeData objbill)
        {
            List<EmployeeData> result = null;
            try
            {
                DueCollectionDA objDA = new DueCollectionDA();
                result = objDA.GetRespEmployees(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<DueCollectionData> GetPatientDueCollectionList(DueCollectionData objbill)
        {
            List<DueCollectionData> result = null;
            try
            {
                DueCollectionDA objDA = new DueCollectionDA();
                result = objDA.GetPatientDueCollectionList(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<DueCollectionData> DueDetailByBillNo(DueCollectionData objbill)
        {
            List<DueCollectionData> result = null;
            try
            {
                DueCollectionDA objDA = new DueCollectionDA();
                result = objDA.DueDetailByBillNo(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<DueCollectionData> UpdateDueDetail(DueCollectionData objD)
        {
            List<DueCollectionData> result = null;
            try
            {
                DueCollectionDA objpatientDA = new DueCollectionDA();
                result = objpatientDA.UpdateDueDetail(objD);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int  UpdateDueDiscount(DueCollectionData objD)
        {
            int result = 0;
            try
            {
                DueCollectionDA objpatientDA = new DueCollectionDA();
                result = objpatientDA.UpdateDueDiscount(objD);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<DueCollectionData> GetPatientCollectionList(DueCollectionData objbill)
        {
            List<DueCollectionData> result = null;
            try
            {
                DueCollectionDA objDA = new DueCollectionDA();
                result = objDA.GetPatientCollectionList(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<DueCollectionData> GetPatientNameList(DueCollectionData objadmission)
        {
            List<DueCollectionData> result = null;
            try
            {
                DueCollectionDA objempientDA = new DueCollectionDA();
                result = objempientDA.GetPatientNameList(objadmission);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<DueCollectionData> GetDueBillList(DueCollectionData objD)
        {
            List<DueCollectionData> result = null;
            try
            {
                DueCollectionDA objpatientDA = new DueCollectionDA();
                result = objpatientDA.GetDueBillList(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<DueCollectionData> GetIpnoEmrgNoList(DueCollectionData objadmission)
        {
            List<DueCollectionData> result = null;
            try
            {
                DueCollectionDA objempientDA = new DueCollectionDA();
                result = objempientDA.getIPNoNemrgNoList(objadmission);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeleteDueCollectionDetail(DueCollectionData objstd)
        {
            int result = 0;

            try
            {
                DueCollectionDA objDA = new DueCollectionDA();
                result = objDA.DeleteDueCollectionDetail(objstd);
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

using Mediqura.CommonData.MedPharData;
using Mediqura.DAL.MedPharDA;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.BOL.MedPharBO
{
    public class EmgDrugIssueBO
    {
        public List<EmgDrugIssueData> GetEmgPatientName(EmgDrugIssueData objD)
        {
            List<EmgDrugIssueData> result = null;
            try
            {
                EmgDrugIssueDA objpatientDA = new EmgDrugIssueDA();
                result = objpatientDA.GetEmgPatientName(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<EmgDrugIssueData> GetPatientDetailsByEmgNo(EmgDrugIssueData objD)
        {
            List<EmgDrugIssueData> result = null;
            try
            {
                EmgDrugIssueDA objpatientDA = new EmgDrugIssueDA();
                result = objpatientDA.GetPatientDetailsByEmgNo(objD);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<EmgDrugIssueData> GetDrugName(EmgDrugIssueData objD)
        {
            List<EmgDrugIssueData> result = null;
            try
            {
                EmgDrugIssueDA objdrugDA = new EmgDrugIssueDA();
                result = objdrugDA.GetDrugName(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<EmgDrugIssueData> GetDoctorName(EmgDrugIssueData objD)
        {
            List<EmgDrugIssueData> result = null;
            try
            {
                EmgDrugIssueDA objdrugDA = new EmgDrugIssueDA();
                result = objdrugDA.GetDoctorName(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<EmgDrugIssueData> UpdateEmgDrugIssueDetails(EmgDrugIssueData objpat)
        {
            List<EmgDrugIssueData> result = null;
            try
            {
                EmgDrugIssueDA objpatientDA = new EmgDrugIssueDA();
                result = objpatientDA.UpdateEmgDrugIssueDetails(objpat);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<EmgDrugIssueData> GetEmgDrugRecordList(EmgDrugIssueData objD)
        {
            List<EmgDrugIssueData> result = null;
            try
            {
                EmgDrugIssueDA objMediDA = new EmgDrugIssueDA();
                result = objMediDA.GetEmgDrugRecordList(objD);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        ////-----TAB2------//
        public List<EmgDrugIssueData> Get_EmgDrugRecordList(EmgDrugIssueData objD)
        {
            List<EmgDrugIssueData> result = null;
            try
            {
                EmgDrugIssueDA objMediDA = new EmgDrugIssueDA();
                result = objMediDA.Get_EmgDrugRecordList(objD);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public int DeleteEmgPatientDrugRecordByID(EmgDrugIssueData objDrgData)
        {
            int result = 0;
            try
            {
                EmgDrugIssueDA objDrgDA = new EmgDrugIssueDA();
                result = objDrgDA.DeleteEmgPatientDrugRecordByID(objDrgData);
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

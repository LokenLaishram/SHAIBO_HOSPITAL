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
    public class Phar_IPIssueBO
    {
        public List<PharIPIssueData> GetAllIPPatientName(PharIPIssueData objD)
        {
            List<PharIPIssueData> result = null;
            try
            {
                PharIPIssueDA objpatientDA = new PharIPIssueDA();
                result = objpatientDA.GetAllIPPatientName(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<PharIPIssueData> GetIPPatientName(PharIPIssueData objD)
        {
            List<PharIPIssueData> result = null;
            try
            {
                PharIPIssueDA objpatientDA = new PharIPIssueDA();
                result = objpatientDA.GetIPPatientName(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<PharIPIssueData> GetPatientDetailsByIPNO(PharIPIssueData objD)
        {
            List<PharIPIssueData> result = null;
            try
            {
                PharIPIssueDA objpatientDA = new PharIPIssueDA();
                result = objpatientDA.GetPatientDetailsByIPNO(objD);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<PharIPIssueData> GetDrugName(PharIPIssueData objD)
        {
            List<PharIPIssueData> result = null;
            try
            {
                PharIPIssueDA objdrugDA = new PharIPIssueDA();
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
        public List<PharIPIssueData> GetDoctorName(PharIPIssueData objD)
        {
            List<PharIPIssueData> result = null;
            try
            {
                PharIPIssueDA objdrugDA = new PharIPIssueDA();
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
        public List<PharIPIssueData> UpdateIPDrugIssueDetails(PharIPIssueData objpat)
        {
            List<PharIPIssueData> result = null;

            try
            {
                PharIPIssueDA objpatientDA = new PharIPIssueDA();
                result = objpatientDA.UpdateIPDrugIssueDetails(objpat);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<PharIPIssueData> GetIPDrugRecordList(PharIPIssueData objD)
        {
            List<PharIPIssueData> result = null;
            try
            {
                PharIPIssueDA objMediDA = new PharIPIssueDA();
                result = objMediDA.GetIPDrugRecordList(objD);

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
        public List<PharIPIssueData> Get_IPDrugRecordList(PharIPIssueData objD)
        {
            List<PharIPIssueData> result = null;
            try
            {
                PharIPIssueDA objMediDA = new PharIPIssueDA();
                result = objMediDA.Get_IPDrugRecordList(objD);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public int DeleteIPPatientDrugRecordByID(PharIPIssueData objDrgData)
        {
            int result = 0;
            try
            {
                PharIPIssueDA objDrgDA = new PharIPIssueDA();
                result = objDrgDA.DeleteIPPatientDrugRecordByID(objDrgData);
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

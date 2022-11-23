using Mediqura.CommonData.OTData;
using Mediqura.DAL.OTDA;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.BOL.OTBO
{
    public class PatientConsentTypeBO
    {
        public int UpdateConsentTypeDetails(PatientConsentTypeData objOTRoleMasterData)
        {
            int result = 0;
            try
            {
                PatientConsentTypeDA objOTRoleMasterDA = new PatientConsentTypeDA();
                result = objOTRoleMasterDA.UpdateConsentTypeDetails(objOTRoleMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<PatientConsentTypeData> SearchConsentType(PatientConsentTypeData objOTRoleMasterData)
        {
            List<PatientConsentTypeData> result = null;
            try
            {
                PatientConsentTypeDA objOTRoleMasterDA = new PatientConsentTypeDA();
                result = objOTRoleMasterDA.SearchConsentType(objOTRoleMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<PatientConsentTypeData> GetConsentTypeDetailsByID(PatientConsentTypeData objOTRoleMasterData)
        {
            List<PatientConsentTypeData> result = null;

            try
            {
                PatientConsentTypeDA objOTRoleMasterDA = new PatientConsentTypeDA();
                result = objOTRoleMasterDA.GetEmployeeTypeDetailsByID(objOTRoleMasterData);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeleteConsentTypeDetailsByID(PatientConsentTypeData objOTRoleMasterData)
        {
            int result = 0;
            try
            {
                PatientConsentTypeDA objOTRoleMasterDA = new PatientConsentTypeDA();
                result = objOTRoleMasterDA.DeleteConsentTypeDetailsByID(objOTRoleMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<PatientConsentData> GetConsentTemplateByID(PatientConsentData objRadioReportMaster)
        {
            List<PatientConsentData> result = null;

            try
            {
                PatientConsentTypeDA objMasterDA = new PatientConsentTypeDA();
                result = objMasterDA.GetConsentTemplateByID(objRadioReportMaster);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int UpdateConsentReport(PatientConsentData objRadioReportMaster)
        {
            int result = 0;
            try
            {
                PatientConsentTypeDA objMasterDA = new PatientConsentTypeDA();
                result = objMasterDA.UpdateConsentReport(objRadioReportMaster);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<PatientConsentData> GetConsentTemplateByIPNO(PatientConsentData objRadioReportMaster)
        {
            List<PatientConsentData> result = null;

            try
            {
                PatientConsentTypeDA objMasterDA = new PatientConsentTypeDA();
                result = objMasterDA.GetConsentTemplateByIPNO(objRadioReportMaster);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int UpdatePatientConsent(PatientConsentData objData)
        {
            int result = 0;
            try
            {
                PatientConsentTypeDA objDA = new PatientConsentTypeDA();
                result = objDA.UpdatePatientConsent(objData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<PatientConsentData> SearchConsentTemplateByIPNO(PatientConsentData objRadioReportMaster)
        {
            List<PatientConsentData> result = null;

            try
            {
                PatientConsentTypeDA objMasterDA = new PatientConsentTypeDA();
                result = objMasterDA.SearchConsentTemplateByIPNO(objRadioReportMaster);

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

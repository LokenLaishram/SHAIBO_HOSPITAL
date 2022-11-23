using Mediqura.CommonData.MedUtilityData;
using Mediqura.DAL.MedUtilityDA;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.BOL.MedUtilityBO
{
    public class PatientTypeMasterBO
    {
        public int UpdatePatientTypeDetails(PatientTypeMasterData objPatientTypeMasterData)
        {
            int result = 0;
            try
            {
                PatientTypeMasterDA objPatientTypeMasteDA = new PatientTypeMasterDA();
                result = objPatientTypeMasteDA.UpdatePatientTypeDetails(objPatientTypeMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int UpdateOutPatientTypeDetails(PatientTypeMasterData objPatientTypeMasterData)
        {
            int result = 0;
            try
            {
                PatientTypeMasterDA objPatientTypeMasteDA = new PatientTypeMasterDA();
                result = objPatientTypeMasteDA.UpdateOutPatientTypeDetails(objPatientTypeMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<PatientTypeMasterData> GetPatientTypeDetailsByID(PatientTypeMasterData objPatientTypeMasterData)
        {
            List<PatientTypeMasterData> result = null;

            try
            {
                PatientTypeMasterDA objPatientTypeMasteDA = new PatientTypeMasterDA();
                result = objPatientTypeMasteDA.GetPatientTypeDetailsByID(objPatientTypeMasterData);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }

        public int DeletePatientTypeDetailsByID(PatientTypeMasterData objPatientTypeMasterData)
        {
            int result = 0;
            try
            {
                PatientTypeMasterDA objPatientTypeMasteDA = new PatientTypeMasterDA();
                result = objPatientTypeMasteDA.DeletePatientTypeDetailsByID(objPatientTypeMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<PatientTypeMasterData> SearchPatientTypeDetails(PatientTypeMasterData objPatientTypeMasterData)
        {
            List<PatientTypeMasterData> result = null;
            try
            {
                PatientTypeMasterDA objPatientTypeMasteDA = new PatientTypeMasterDA();
                result = objPatientTypeMasteDA.SearchPatientTypeDetails(objPatientTypeMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<PatientTypeMasterData> SearchPatientCategoryDetails(PatientTypeMasterData objPatientTypeMasterData)
        {
            List<PatientTypeMasterData> result = null;
            try
            {
                PatientTypeMasterDA objPatientTypeMasteDA = new PatientTypeMasterDA();
                result = objPatientTypeMasteDA.SearchPatientCategoryDetails(objPatientTypeMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<PatientTypeMasterData> GetPatientCategoryDetailsByID(PatientTypeMasterData objPatientTypeMasterData)
        {
            List<PatientTypeMasterData> result = null;

            try
            {
                PatientTypeMasterDA objPatientTypeMasteDA = new PatientTypeMasterDA();
                result = objPatientTypeMasteDA.GetPatientCategoryDetailsByID(objPatientTypeMasterData);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeletePatientCategoryDetailsByID(PatientTypeMasterData objPatientTypeMasterData)
        {
            int result = 0;
            try
            {
                PatientTypeMasterDA objPatientTypeMasteDA = new PatientTypeMasterDA();
                result = objPatientTypeMasteDA.DeletePatientCategoryDetailsByID(objPatientTypeMasterData);
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

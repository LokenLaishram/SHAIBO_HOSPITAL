using Mediqura.CommonData.MedUtilityData;
using Mediqura.DAL.MedUtilityDA;
using Mediqura.DAL.MedOPDDA;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mediqura.CommonData.MedOPDData;
using Mediqura.CommonData.PatientData;

namespace Mediqura.BOL.MedOPDBO
{
    public class PatientDiagnosisBO
    {
        public int UpdateICDCodeList(ICDData objstock)
        {
            int result = 0;
            try
            {
                PatientDiagnosisDA objDA = new PatientDiagnosisDA();
                result = objDA.UpdateICDCodeList(objstock);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<ICDData> GetPatientDiagnosisDetails(ICDData objpat)
        {

            List<ICDData> result = null;

            try
            {
                PatientDiagnosisDA objpatientDA = new PatientDiagnosisDA();
                result = objpatientDA.GetPatientDiagnosisDetails(objpat);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;


        }
        public int DeletePatientDiagnosisDetails(ICDData objstd)
        {
            int result = 0;

            try
            {
                PatientDiagnosisDA objpatientDA = new PatientDiagnosisDA();
                result = objpatientDA.DeletePatientDiagnosisDetails(objstd);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<ICDData> GetPatientDiagnosisDetailsByID(ICDData objpat)
        {

            List<ICDData> result = null;

            try
            {
                PatientDiagnosisDA objpatientDA = new PatientDiagnosisDA();
                result = objpatientDA.GetPatientDiagnosisDetailsByID(objpat);
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

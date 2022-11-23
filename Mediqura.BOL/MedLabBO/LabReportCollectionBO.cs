using Mediqura.CommonData.MedLab;
using Mediqura.DAL.MedLab;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.BOL.MedLabBO
{
   public class LabReportCollectionBO
    {
       public List<LadReportCollectionData> GetTestPatientList(LadReportCollectionData objpatient)
        {
            List<LadReportCollectionData> result = null;
            try
            {
                LabReportCollectionDA objDA = new LabReportCollectionDA();
                result = objDA.GetTestPatientList(objpatient);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
       public List<LadReportCollectionData> GetPatientTestList(LadReportCollectionData objData)
       {
           List<LadReportCollectionData> result = null;

           try
           {
               LabReportCollectionDA objDA = new LabReportCollectionDA();
               result = objDA.GetPatientTestList(objData);
           }
           catch (Exception ex)
           {
               PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
               LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
               throw new BusinessProcessException("4000001", ex);
           }
           return result;
       }
        public List<LadReportCollectionData> GetInvestigationPatientList(LadReportCollectionData objpatient)
        {
            List<LadReportCollectionData> result = null;
            try
            {
                LabReportCollectionDA objDA = new LabReportCollectionDA();
                result = objDA.GetInvestigationPatientList(objpatient);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<LadReportCollectionData> GetRadioTemplateByID(LadReportCollectionData objRadioReportMaster)
        {
            List<LadReportCollectionData> result = null;

            try
            {
                LabReportCollectionDA objMasterDA = new LabReportCollectionDA();
                result = objMasterDA.GetRadioReportTemplateByTestId(objRadioReportMaster);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
       public int UpdateRadioReportVerification(LadReportCollectionData objRadioReportMaster)
        {
            int result = 0;
            try
            {
                LabReportCollectionDA objDA = new LabReportCollectionDA();
                result = objDA.UpdateReportPrintStatus(objRadioReportMaster);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int UpdateEmaildeliveryStatus(LadReportCollectionData objRadioReportMaster)
        {
            int result = 0;
            try
            {
                LabReportCollectionDA objDA = new LabReportCollectionDA();
                result = objDA.UpdateEmaildeliveryStatus(objRadioReportMaster);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<LadReportCollectionData> GetLabTestList(LadReportCollectionData objpatient)
       {
           List<LadReportCollectionData> result = null;
           try
           {
               LabReportCollectionDA objDA = new LabReportCollectionDA();
               result = objDA.GetLabTestList(objpatient);
           }
           catch (Exception ex)
           {
               PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
               LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
               throw new BusinessProcessException("4000001", ex);
           }
           return result;

       }
       public List<LadReportCollectionData> GetPatientListReport(LadReportCollectionData objpatient)
       {
           List<LadReportCollectionData> result = null;
           try
           {
               LabReportCollectionDA objDA = new LabReportCollectionDA();
               result = objDA.GetPatientListReport(objpatient);
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

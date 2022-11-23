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
   public class RadioLabReportVerificationBO
    {
       public List<RadioLabReportVerificationData> GetPatientList(RadioLabReportVerificationData objpatient)
        {
            List<RadioLabReportVerificationData> result = null;
            try
            {
                RadioLabReportVerificationDA objDA = new RadioLabReportVerificationDA();
                result = objDA.GetPatientList(objpatient);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
       public List<RadioLabReportVerificationData> GetRadioTemplateByID(RadioLabReportVerificationData objRadioReportMaster)
       {
           List<RadioLabReportVerificationData> result = null;

           try
           {
               RadioLabReportVerificationDA objMasterDA = new RadioLabReportVerificationDA();
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
       public int UpdateRadioReportVerification(RadioLabReportVerificationData objRadioReportMaster)
       {
           int result = 0;
           try
           {
               RadioLabReportVerificationDA objDA = new RadioLabReportVerificationDA();
               result = objDA.UpdateReportTemplateVerification(objRadioReportMaster);
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

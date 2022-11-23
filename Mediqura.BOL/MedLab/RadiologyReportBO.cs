using Mediqura.CommonData;
using Mediqura.DAL.MedLab;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.BOL.MedLab
{
  public  class RadiologyReportBO
    {
       public List<RadiologyReportData> GetPatientList(RadiologyReportData objpatient)
        {
            List<RadiologyReportData> result = null;
            try
            {
                RadiologyReportDA objDA = new RadiologyReportDA();
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
       public List<RadiologyReportData> GetRadioTemplateByID(RadiologyReportData objRadioReportMaster)
       {
           List<RadiologyReportData> result = null;

           try
           {
               RadiologyReportDA objMasterDA = new RadiologyReportDA();
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

       public int UpdateRadioReport(RadiologyReportData objRadioReportMaster)
       {
           int result = 0;
           try
           {
               RadiologyReportDA objDA = new RadiologyReportDA();
               result = objDA.UpdateReportTemplate(objRadioReportMaster);
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

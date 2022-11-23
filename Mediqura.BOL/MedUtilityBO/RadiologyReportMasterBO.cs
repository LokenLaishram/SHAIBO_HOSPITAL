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
  public class RadiologyReportMasterBO
    {
      public int UpdateRadioReport(RadiologyReportMasterData objRadioReportMaster)
        {
            int result = 0;
            try
            {
                RadiologyReportMasterDA objMasterDA = new RadiologyReportMasterDA();
                result = objMasterDA.UpdateReportTemplate(objRadioReportMaster);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
      public List<RadiologyReportMasterData> GetRadioTemplateByID(RadiologyReportMasterData objRadioReportMaster)
        {
            List<RadiologyReportMasterData> result = null;

            try
            {
                RadiologyReportMasterDA objMasterDA = new RadiologyReportMasterDA();
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
    }
}

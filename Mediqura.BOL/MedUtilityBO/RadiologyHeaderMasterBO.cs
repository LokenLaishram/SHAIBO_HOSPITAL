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
    public class RadiologyHeaderMasterBO
    {
        public int UpdateRadioReport(RadiologyHeaderMasterData objRadioReportMaster)
        {
            int result = 0;
            try
            {
                RadiologyHeaderMasterDA objMasterDA = new RadiologyHeaderMasterDA();
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

        public List<RadiologyHeaderMasterData> SearchRadioHeaderDetaiks(RadiologyHeaderMasterData objRadioReportMaster)
        {
            List<RadiologyHeaderMasterData> result = null;

            try
            {
                RadiologyHeaderMasterDA objMasterDA = new RadiologyHeaderMasterDA();
                result = objMasterDA.SearchRadioHeaderDetaiks(objRadioReportMaster);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }

        public List<RadiologyHeaderMasterData> GetRadioHeaderByID(RadiologyHeaderMasterData objRadioReportMaster)
        {
            List<RadiologyHeaderMasterData> result = null;

            try
            {
                RadiologyHeaderMasterDA objMasterDA = new RadiologyHeaderMasterDA();
                result = objMasterDA.GetRadioHeaderByID(objRadioReportMaster);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }

        public int DeleteRadioHeaderByID(RadiologyHeaderMasterData objRadioReportMaster)
        {
            int result = 0;

            try
            {
                RadiologyHeaderMasterDA objMasterDA = new RadiologyHeaderMasterDA();
                result = objMasterDA.DeleteRadioHeaderByID(objRadioReportMaster);

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

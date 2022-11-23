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
    public class ExpiryReportMasterBO
    {
        public int UpdateExpiryReport(ExpiryReportMasterData objRadioReportMaster)
        {
            int result = 0;
            try
            {
                ExpiryReportMasterDA objMasterDA = new ExpiryReportMasterDA();
                result = objMasterDA.UpdateExpiryReport(objRadioReportMaster);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeleteExpiryReport(ExpiryReportMasterData objstd)
        {
            int result = 0;

            try
            {
                ExpiryReportMasterDA objDA = new ExpiryReportMasterDA();
                result = objDA.DeleteExpiryReport(objstd);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
   
        public int UpdateExpiryReportMaker(ExpiryReportMasterData objRadioReportMaster)
        {
            int result = 0;
            try
            {
                ExpiryReportMasterDA objMasterDA = new ExpiryReportMasterDA();
                result = objMasterDA.UpdateExpiryReportMaker(objRadioReportMaster);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }

        public List<ExpiryReportMasterData> GetPatientDetailsByID(ExpiryReportMasterData objRadioReportMaster)
        {
            List<ExpiryReportMasterData> result = null;

            try
            {
                ExpiryReportMasterDA objMasterDA = new ExpiryReportMasterDA();
                result = objMasterDA.GetPatientDetailsByID(objRadioReportMaster);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }

        public List<ExpiryReportMasterData> GetExpiryTemplateByID(ExpiryReportMasterData objRadioReportMaster)
        {
            List<ExpiryReportMasterData> result = null;

            try
            {
                ExpiryReportMasterDA objMasterDA = new ExpiryReportMasterDA();
                result = objMasterDA.GetExpiryTemplateByID(objRadioReportMaster);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<ExpiryReportMasterData> GetExpiryReportList(ExpiryReportMasterData objRadioReportMaster)
        {
            List<ExpiryReportMasterData> result = null;

            try
            {
                ExpiryReportMasterDA objMasterDA = new ExpiryReportMasterDA();
                result = objMasterDA.GetExpiryReportList(objRadioReportMaster);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<ExpiryReportMasterData> GetExpiryReportDetails(ExpiryReportMasterData objRadioReportMaster)
        {
            List<ExpiryReportMasterData> result = null;

            try
            {
                ExpiryReportMasterDA objMasterDA = new ExpiryReportMasterDA();
                result = objMasterDA.GetExpiryReportDetails(objRadioReportMaster);

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

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
    public class LabCommentMakerBO
    {
        public int UpdateRadioReport(LabCommentMakerData objRadioReportMaster)
        {
            int result = 0;
            try
            {
                LabCommentMakerDA objMasterDA = new LabCommentMakerDA();
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
        public List<LabCommentMakerData> GetRadioTemplateByID(LabCommentMakerData objRadioReportMaster)
        {
            List<LabCommentMakerData> result = null;

            try
            {
                LabCommentMakerDA objMasterDA = new LabCommentMakerDA();
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
        public int UpdateTestComment(LabCommentMakerData objRadioReportMaster)
        {
            int result = 0;
            try
            {
                LabCommentMakerDA objMasterDA = new LabCommentMakerDA();
                result = objMasterDA.UpdateTestComment(objRadioReportMaster);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<LabCommentMakerData> GetTestCommentTemplateByID(LabCommentMakerData objRadioReportMaster)
        {
            List<LabCommentMakerData> result = null;

            try
            {
                LabCommentMakerDA objMasterDA = new LabCommentMakerDA();
                result = objMasterDA.GetTestCommentTemplateByID(objRadioReportMaster);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<LabCommentMakerData> GetTestCommentList(LabCommentMakerData objRadioReportMaster)
        {
            List<LabCommentMakerData> result = null;

            try
            {
                LabCommentMakerDA objMasterDA = new LabCommentMakerDA();
                result = objMasterDA.GetTestCommentList(objRadioReportMaster);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<LabCommentMakerData> GetTestCommentTemplate(LabCommentMakerData objRadioReportMaster)
        {
            List<LabCommentMakerData> result = null;

            try
            {
                LabCommentMakerDA objMasterDA = new LabCommentMakerDA();
                result = objMasterDA.GetTestCommentTemplate(objRadioReportMaster);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeleteTestComment(LabCommentMakerData objstd)
        {
            int result = 0;

            try
            {
                LabCommentMakerDA objDA = new LabCommentMakerDA();
                result = objDA.DeleteTestComment(objstd);

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

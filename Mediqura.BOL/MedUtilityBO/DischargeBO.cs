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
    public class DischargeBO
    {
        public int UpdateDischargeDetails(DischargeData objDischargeMasterData)
        {
            int result = 0;
            try
            {
                DischargeDA objDischargeMasterDA = new DischargeDA();
                result = objDischargeMasterDA.UpdateDischargeDetails(objDischargeMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<DischargeData> SearchDischargeDetails(DischargeData objDischargeMasterData)
        {
            List<DischargeData> result = null;
            try
            {
                DischargeDA objDischargeMasterDA = new DischargeDA();
                result = objDischargeMasterDA.SearchDischargeDetails(objDischargeMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<DischargeData> GetDischargeDetailsByID(DischargeData objDischargeMasterData)
        {
            List<DischargeData> result = null;

            try
            {
                DischargeDA objDischargeMasterDA = new DischargeDA();
                result = objDischargeMasterDA.GetDischargeDetailsByID(objDischargeMasterData);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeleteDischargeDetailsByID(DischargeData objDischargeMasterData)
        {
            int result = 0;
            try
            {
                DischargeDA objDischargeMasterDA = new DischargeDA();
                result = objDischargeMasterDA.DeleteDischargeDetailsByID(objDischargeMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int UpdateDishaegeReport(DischargeData objRadioReportMaster)
        {
            int result = 0;
            try
            {
                DischargeDA objMasterDA = new DischargeDA();
                result = objMasterDA.UpdateDishaegeReport(objRadioReportMaster);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<DischargeData> GetRadioTemplateByID(DischargeData objRadioReportMaster)
        {
            List<DischargeData> result = null;

            try
            {
                DischargeDA objMasterDA = new DischargeDA();
                result = objMasterDA.GetRadioTemplateByID(objRadioReportMaster);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<DischargeData> GetDischargeTemplate(DischargeData objRadioReportMaster)
        {
            List<DischargeData> result = null;

            try
            {
                DischargeDA objMasterDA = new DischargeDA();
                result = objMasterDA.GetDischargeTemplate(objRadioReportMaster);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        //public List<DischargeData> GetPatientList(DischargeData objpatient)
        //{
        //    List<DischargeData> result = null;
        //    try
        //    {
        //        DischargeDA objDA = new DischargeDA();
        //        result = objDA.GetPatientList(objpatient);
        //    }
        //    catch (Exception ex)
        //    {
        //        PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
        //        LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
        //        throw new BusinessProcessException("4000001", ex);
        //    }
        //    return result;

        //}
        public List<DischargeData> GetFnalBillList(DischargeData objpatient)
        {
            List<DischargeData> result = null;
            try
            {
                DischargeDA objDA = new DischargeDA();
                result = objDA.GetFnalBillList(objpatient);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<DischargeData> GetSummaryList(DischargeData objpatient)
        {
            List<DischargeData> result = null;
            try
            {
                DischargeDA objDA = new DischargeDA();
                result = objDA.GetSummaryList(objpatient);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<DischargeData> GetDischargeTemplateByID(DischargeData objRadioReportMaster)
        {
            List<DischargeData> result = null;

            try
            {
                DischargeDA objMasterDA = new DischargeDA();
                result = objMasterDA.GetDischargeTemplateByID(objRadioReportMaster);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int UpdateSummaryReport(DischargeData objRadioReportMaster)
        {
            int result = 0;
            try
            {
                DischargeDA objDA = new DischargeDA();
                result = objDA.UpdateSummaryReport(objRadioReportMaster);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<DischargeData> GetDischargeTemplateByIPNO(DischargeData objRadioReportMaster)
        {
            List<DischargeData> result = null;

            try
            {
                DischargeDA objMasterDA = new DischargeDA();
                result = objMasterDA.GetDischargeTemplateByIPNO(objRadioReportMaster);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<DischargeData> getIPNoDish(DischargeData objadmission)
        {
            List<DischargeData> result = null;
            try
            {
                DischargeDA objempientDA = new DischargeDA();
                result = objempientDA.getIPNoDish(objadmission);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<DischargeData> getIPNoDishList(DischargeData objadmission)
        {
            List<DischargeData> result = null;
            try
            {
                DischargeDA objempientDA = new DischargeDA();
                result = objempientDA.getIPNoDishList(objadmission);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<DischargeData> GetDischargeList(DischargeData objpatient)
        {
            List<DischargeData> result = null;
            try
            {
                DischargeDA objDA = new DischargeDA();
                result = objDA.GetDischargeList(objpatient);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<DischargeData> GetDischargeReadyList(DischargeData objpatient)
        {
            List<DischargeData> result = null;
            try
            {
                DischargeDA objDA = new DischargeDA();
                result = objDA.GetDischargeReadyList(objpatient);
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

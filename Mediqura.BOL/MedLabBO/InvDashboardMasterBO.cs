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
    public class InvDashboardMasterBO
    {
        public List<InvDashboardMasterData> GetInvNo(InvDashboardMasterData objpatient)
        {
            List<InvDashboardMasterData> result = null;
            try
            {
                InvDashboardDA objDA = new InvDashboardDA();
                result = objDA.GetInvNo(objpatient);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<InvDashboardMasterData> GetInvNoWithContext(InvDashboardMasterData objpatient)
        {
            List<InvDashboardMasterData> result = null;
            try
            {
                InvDashboardDA objDA = new InvDashboardDA();
                result = objDA.GetInvNoWithContext(objpatient);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<InvDashboardMasterData> getIPNoNemrgNo(InvDashboardMasterData objpatient)
        {
            List<InvDashboardMasterData> result = null;
            try
            {
                InvDashboardDA objDA = new InvDashboardDA();
                result = objDA.getIPNoNemrgNo(objpatient);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<InvDashboardMasterData> GetInvestigationDetails(InvDashboardMasterData objpatient)
        {
            List<InvDashboardMasterData> result = null;
            try
            {
                InvDashboardDA objDA = new InvDashboardDA();
                result = objDA.GetInvestigationDetails(objpatient);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<InvDashboardMasterData> GetRadioDetails(InvDashboardMasterData objpatient)
        {
            List<InvDashboardMasterData> result = null;
            try
            {
                InvDashboardDA objDA = new InvDashboardDA();
                result = objDA.GetRadioDetails(objpatient);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<InvDashboardMasterData> GetEndosDetails(InvDashboardMasterData objpatient)
        {
            List<InvDashboardMasterData> result = null;
            try
            {
                InvDashboardDA objDA = new InvDashboardDA();
                result = objDA.GetEndosDetails(objpatient);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<InvDashboardMasterData> GetInvestigationReport(InvDashboardMasterData objpatient)
        {
            List<InvDashboardMasterData> result = null;
            try
            {
                InvDashboardDA objDA = new InvDashboardDA();
                result = objDA.GetInvestigationReport(objpatient);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<InvTatData> GetInvestigationTat(InvTatData objData)
        {
            List<InvTatData> result = null;
            try
            {
                InvDashboardDA objDA = new InvDashboardDA();
                result = objDA.GetInvestigationTat(objData);
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

using Mediqura.CommonData.MedNurseData;
using Mediqura.DAL.MedNurseDA;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.BOL.MedNurseBO
{
    public class NurseSupervisorBO
    {
        public int UpdateSupervisorDetails(SupervisorData objSupervisorMSTData)
        {
            int result = 0;
            try
            {
                NurseSupervisorDA objSupervisorMSTDA = new NurseSupervisorDA();
                result = objSupervisorMSTDA.UpdateSupervisorDetails(objSupervisorMSTData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<SupervisorData> SearchNurseSupervisorDetails(SupervisorData objSupervisorMSTData)
        {
            List<SupervisorData> result = null;
            try
            {
                NurseSupervisorDA objSupervisorMSTDA = new NurseSupervisorDA();
                result = objSupervisorMSTDA.SearchNurseSupervisorDetails(objSupervisorMSTData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int UpdateAsstSupervisorDetails(SupervisorData objSupervisorMSTData)
        {
            int result = 0;
            try
            {
                NurseSupervisorDA objSupervisorMSTDA = new NurseSupervisorDA();
                result = objSupervisorMSTDA.UpdateAsstSupervisorDetails(objSupervisorMSTData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int CancelSupervisorBoundstock(SupervisorData objSupervisorMSTData)
        {
            int result = 0;
            try
            {
                NurseSupervisorDA objSupervisorMSTDA = new NurseSupervisorDA();
                result = objSupervisorMSTDA.CancelSupervisorBoundstock(objSupervisorMSTData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<SupervisorData> SearchAsstNurseSupervisorDetails(SupervisorData objSupervisorMSTData)
        {
            List<SupervisorData> result = null;
            try
            {
                NurseSupervisorDA objSupervisorMSTDA = new NurseSupervisorDA();
                result = objSupervisorMSTDA.SearchAsstNurseSupervisorDetails(objSupervisorMSTData);
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

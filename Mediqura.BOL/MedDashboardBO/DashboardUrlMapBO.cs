using Mediqura.CommonData.MedDashboardData;
using Mediqura.DAL.MedDashboardDA;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Mediqura.BOL.MedDashboardBO
{
    public class DashboardUrlMapBO
    {
        public int UpdateDashboardURLDetails(DashboardUrlMapData objData)
        {
            int result = 0;
            try
            {
                DashboardUrlMapDA objDA = new DashboardUrlMapDA();
                result = objDA.UpdateDashboardURLDetails(objData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<DashboardUrlMapData> SearchDashboardURLDetails(DashboardUrlMapData objData)
        {
            List<DashboardUrlMapData> result = null;
            try
            {
                DashboardUrlMapDA objDA = new DashboardUrlMapDA();
                result = objDA.SearchDashboardURLDetails(objData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<DashboardUrlMapData> GetDashboardURLDetailsByID(DashboardUrlMapData objData)
        {
            List<DashboardUrlMapData> result = null;

            try
            {
                DashboardUrlMapDA objDA = new DashboardUrlMapDA();
                result = objDA.GetDashboardURLDetailsByID(objData);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeleteDashboardURLDetailsByID(DashboardUrlMapData objData)
        {
            int result = 0;
            try
            {
                DashboardUrlMapDA objDA = new DashboardUrlMapDA();
                result = objDA.DeleteDashboardURLDetailsByID(objData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }

        public int UpdateDashRoleWiseDetails(DashboardRoleWiseMapData objData)
        {
            int result = 0;
            try
            {
                DashboardUrlMapDA objDA = new DashboardUrlMapDA();
                result = objDA.UpdateDashRoleWiseDetails(objData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<DashboardRoleWiseMapData> SearchDashboarRoleWiseDetails(DashboardRoleWiseMapData objData)
        {
            List<DashboardRoleWiseMapData> result = null;
            try
            {
                DashboardUrlMapDA objDA = new DashboardUrlMapDA();
                result = objDA.SearchDashboarRoleWiseDetails(objData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<DashboardRoleWiseMapData> GetDashboardRoleWiseDetailsByID(DashboardRoleWiseMapData objData)
        {
            List<DashboardRoleWiseMapData> result = null;

            try
            {
                DashboardUrlMapDA objDA = new DashboardUrlMapDA();
                result = objDA.GetDashboardRoleWiseDetailsByID(objData);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeleteDashboardRoleWiseDetailsByID(DashboardRoleWiseMapData objData)
        {
            int result = 0;
            try
            {
                DashboardUrlMapDA objDA = new DashboardUrlMapDA();
                result = objDA.DeleteDashboardRoleWiseDetailsByID(objData);
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

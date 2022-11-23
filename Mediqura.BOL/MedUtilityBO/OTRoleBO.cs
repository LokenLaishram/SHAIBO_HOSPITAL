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
    public class OTRoleBO
    {
        public int UpdateOTRoleDetails(OTRolesData objOTRoleMasterData)
        {
            int result = 0;
            try
            {
                OTRoleDA objOTRoleMasterDA = new OTRoleDA();
                result = objOTRoleMasterDA.UpdateOTRoleDetails(objOTRoleMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<OTRolesData> SearchOTRoleDetails(OTRolesData objOTRoleMasterData)
        {
            List<OTRolesData> result = null;
            try
            {
                OTRoleDA objOTRoleMasterDA = new OTRoleDA();
                result = objOTRoleMasterDA.SearchOTRoleDetails(objOTRoleMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<OTRolesData> GetOTRoleDetailsByID(OTRolesData objOTRoleMasterData)
        {
            List<OTRolesData> result = null;

            try
            {
                OTRoleDA objOTRoleMasterDA = new OTRoleDA();
                result = objOTRoleMasterDA.GetOTRoleDetailsByID(objOTRoleMasterData);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeleteOTRoleDetailsByID(OTRolesData objOTRoleMasterData)
        {
            int result = 0;
            try
            {
                OTRoleDA objOTRoleMasterDA = new OTRoleDA();
                result = objOTRoleMasterDA.DeleteOTRoleDetailsByID(objOTRoleMasterData);
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

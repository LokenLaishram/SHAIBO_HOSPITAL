using Mediqura.CommonData.LoginData;
using Mediqura.DAL.UserDA;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.BOL.UserBO
{
    public class AddRolesBO
    {
        public int UpdateRoleDetails(RolesData objCast)
        {
            int result = 0;
            try
            {
                AddRolesDA objAddRolesDA = new AddRolesDA();
                result = objAddRolesDA.UpdateRoleDetails(objCast);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                 LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<RolesData> SearchRoleDetails(RolesData objCast)
        {
            List<RolesData> result = null;
            try
            {
                AddRolesDA objAddRolesDA = new AddRolesDA();
                result = objAddRolesDA.SearchRoleDetails(objCast);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                 LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<RolesData> GetRoleDetailsByID(RolesData objCast)
        {
            List<RolesData> result = null;

            try
            {
                AddRolesDA objAddRolesDA = new AddRolesDA();
                result = objAddRolesDA.GetRoleDetailsByID(objCast);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                 LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeleteRoleDetailsByID(RolesData objCast)
        {
            int result = 0;
            try
            {
                AddRolesDA objAddRolesDA = new AddRolesDA();
                result = objAddRolesDA.DeleteRoleDetailsByID(objCast);
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

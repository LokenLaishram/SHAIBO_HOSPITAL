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
    public class AddUsersBO
    {
        public int UpdateUserDetails(AddUsersData objCast)
        {
            int result = 0;
            try
            {
                AddUsersDA objAddUsersDA = new AddUsersDA();
                result = objAddUsersDA.UpdateUserDetails(objCast);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<AddUsersData> SearchUserDetails(AddUsersData objCast)
        {

            List<AddUsersData> result = null;

            try
            {
                AddUsersDA objAddUsersDA = new AddUsersDA();
                result = objAddUsersDA.SearchUserDetails(objCast);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<AddUsersData> GetUserDetailsByID(AddUsersData objCast)
        {
            List<AddUsersData> result = null;

            try
            {
                AddUsersDA objAddUsersDA = new AddUsersDA();
                result = objAddUsersDA.GetUserDetailsByID(objCast);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public int DeleteUserDetailsByID(AddUsersData objCast)
        {
            int result = 0;
            try
            {
                AddUsersDA objAddUsersDA = new AddUsersDA();
                result = objAddUsersDA.DeleteUserDetailsByID(objCast);

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

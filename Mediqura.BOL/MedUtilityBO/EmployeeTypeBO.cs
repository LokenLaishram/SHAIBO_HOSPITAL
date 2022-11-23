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
    public class EmployeeTypeBO
    {
        public int UpdateEmplyeeTypeDetails(EmpTypeData objOTRoleMasterData)
        {
            int result = 0;
            try
            {
                EmployeeTypeDA objOTRoleMasterDA = new EmployeeTypeDA();
                result = objOTRoleMasterDA.UpdateEmplyeeTypeDetails(objOTRoleMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<EmpTypeData> SearchEmployeeType(EmpTypeData objOTRoleMasterData)
        {
            List<EmpTypeData> result = null;
            try
            {
                EmployeeTypeDA objOTRoleMasterDA = new EmployeeTypeDA();
                result = objOTRoleMasterDA.SearchEmployeeType(objOTRoleMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<EmpTypeData> GetEmployeeTypeDetailsByID(EmpTypeData objOTRoleMasterData)
        {
            List<EmpTypeData> result = null;

            try
            {
                EmployeeTypeDA objOTRoleMasterDA = new EmployeeTypeDA();
                result = objOTRoleMasterDA.GetEmployeeTypeDetailsByID(objOTRoleMasterData);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeleteEmplyeeTypeDetailsByID(EmpTypeData objOTRoleMasterData)
        {
            int result = 0;
            try
            {
                EmployeeTypeDA objOTRoleMasterDA = new EmployeeTypeDA();
                result = objOTRoleMasterDA.DeleteEmplyeeTypeDetailsByID(objOTRoleMasterData);
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

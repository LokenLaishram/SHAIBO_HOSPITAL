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
    public class EmpGradeBO
    {
        public int UpdateEmplyeeGradeDetails(EmpGradeData objOTRoleMasterData)
        {
            int result = 0;
            try
            {
                EmpGradeDA objOTRoleMasterDA = new EmpGradeDA();
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
        public List<EmpGradeData> SearchEmployeeGrade(EmpGradeData objOTRoleMasterData)
        {
            List<EmpGradeData> result = null;
            try
            {
                EmpGradeDA objOTRoleMasterDA = new EmpGradeDA();
                result = objOTRoleMasterDA.SearchEmployeeGrade(objOTRoleMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<EmpGradeData> GetEmployeeGradeDetailsByID(EmpGradeData objOTRoleMasterData)
        {
            List<EmpGradeData> result = null;

            try
            {
                EmpGradeDA objOTRoleMasterDA = new EmpGradeDA();
                result = objOTRoleMasterDA.GetEmployeeGradeDetailsByID(objOTRoleMasterData);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeleteEmplyeeGradeDetailsByID(EmpGradeData objOTRoleMasterData)
        {
            int result = 0;
            try
            {
                EmpGradeDA objOTRoleMasterDA = new EmpGradeDA();
                result = objOTRoleMasterDA.DeleteEmplyeeGradeDetailsByID(objOTRoleMasterData);
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

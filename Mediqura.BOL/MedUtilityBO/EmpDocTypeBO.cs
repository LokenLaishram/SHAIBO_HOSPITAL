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
    public class EmpDocTypeBO
    {
        public int UpdateEmplyeeDocType(EmpDocTypeData objOTRoleMasterData)
        {
            int result = 0;
            try
            {
                EmpDocTypeDA objOTRoleMasterDA = new EmpDocTypeDA();
                result = objOTRoleMasterDA.UpdateEmplyeeDocType(objOTRoleMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<EmpDocTypeData> SearchEmployeeDocType(EmpDocTypeData objOTRoleMasterData)
        {
            List<EmpDocTypeData> result = null;
            try
            {
                EmpDocTypeDA objOTRoleMasterDA = new EmpDocTypeDA();
                result = objOTRoleMasterDA.SearchEmployeeDocType(objOTRoleMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<EmpDocTypeData> GetEmpDocTypeDetailsByID(EmpDocTypeData objOTRoleMasterData)
        {
            List<EmpDocTypeData> result = null;

            try
            {
                EmpDocTypeDA objOTRoleMasterDA = new EmpDocTypeDA();
                result = objOTRoleMasterDA.GetEmpDocTypeDetailsByID(objOTRoleMasterData);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeleteEmpDocTypeDetailsByID(EmpDocTypeData objOTRoleMasterData)
        {
            int result = 0;
            try
            {
                EmpDocTypeDA objOTRoleMasterDA = new EmpDocTypeDA();
                result = objOTRoleMasterDA.DeleteEmpDocTypeDetailsByID(objOTRoleMasterData);
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

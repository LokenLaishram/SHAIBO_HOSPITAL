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
    public class DepartmentTypeMasterBO
    {
        public int UpdateDepartmentTypeDetails(DepartmentTypeMasterData objDepartmentTypeMasterData)
        {
            int result = 0;
            try
            {
                DepartmentTypeMasterDA objDepartmentTypeMasteDA = new DepartmentTypeMasterDA();
                result = objDepartmentTypeMasteDA.UpdateDepartmentTypeDetails(objDepartmentTypeMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<DepartmentTypeMasterData> GetDepartmentTypeDetailsByID(DepartmentTypeMasterData objDepartmentTypeMasterData)
        {
            List<DepartmentTypeMasterData> result = null;

            try
            {
                DepartmentTypeMasterDA objDepartmentTypeMasteDA = new DepartmentTypeMasterDA();
                result = objDepartmentTypeMasteDA.GetDepartmentTypeDetailsByID(objDepartmentTypeMasterData);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeleteDepartmentTypeDetailsByID(DepartmentTypeMasterData objDepartmentTypeMasterData)
        {
            int result = 0;
            try
            {
                DepartmentTypeMasterDA objPatientTypeMasteDA = new DepartmentTypeMasterDA();
                result = objPatientTypeMasteDA.DeleteDepartmentTypeDetailsByID(objDepartmentTypeMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<DepartmentTypeMasterData> SearchDepartmentTypeDetails(DepartmentTypeMasterData objDepartmentTypeMasterData)
        {
            List<DepartmentTypeMasterData> result = null;
            try
            {
                DepartmentTypeMasterDA objPatientTypeMasteDA = new DepartmentTypeMasterDA();
                result = objPatientTypeMasteDA.SearchDepartmentTypeDetails(objDepartmentTypeMasterData);
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

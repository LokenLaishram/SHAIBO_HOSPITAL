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
    public class DesignationTypeMasterBO
    {
        public int UpdateDesignationTypeDetails(DesignationTypeMasterData objDesignationTypeMasterData)
        {
            int result = 0;
            try
            {
                DesignationTypeMasterDA objDesignationTypeMasteDA = new DesignationTypeMasterDA();
                result = objDesignationTypeMasteDA.UpdateDesignationTypeDetails(objDesignationTypeMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<DesignationTypeMasterData> GetDesignationTypeDetailsByID(DesignationTypeMasterData objDesignationTypeMasterData)
        {
            List<DesignationTypeMasterData> result = null;

            try
            {
                DesignationTypeMasterDA objDesignationTypeMasteDA = new DesignationTypeMasterDA();
                result = objDesignationTypeMasteDA.GetDesignationTypeDetailsByID(objDesignationTypeMasterData);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeleteDesignationTypeDetailsByID(DesignationTypeMasterData objDesignationTypeMasterData)
        {
            int result = 0;
            try
            {
                DesignationTypeMasterDA objDepartmentTypeMasteDA = new DesignationTypeMasterDA();
                result = objDepartmentTypeMasteDA.DeleteDesignationTypeDetailsByID(objDesignationTypeMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<DesignationTypeMasterData> SearchDesignationTypeDetails(DesignationTypeMasterData objDesignationTypeMasterData)
        {
            List<DesignationTypeMasterData> result = null;
            try
            {
                DesignationTypeMasterDA objPatientTypeMasteDA = new DesignationTypeMasterDA();
                result = objPatientTypeMasteDA.SearchDesignationTypeDetails(objDesignationTypeMasterData);
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

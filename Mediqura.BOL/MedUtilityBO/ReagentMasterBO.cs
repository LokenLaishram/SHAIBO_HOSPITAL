using Mediqura.CommonData.MedUtilityData;
using Mediqura.DAL;
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
    public class ReagentMasterBO
    {
        public int UpdateReagentTypeDetails(ReagentMasterData objData)
        {
            int result = 0;
            try
            {
                ReagentMasterDA objDesignationTypeMasteDA = new ReagentMasterDA();
                result = objDesignationTypeMasteDA.UpdateReagentTypeDetails(objData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<ReagentMasterData> GetReagentTypeDetailsByID(ReagentMasterData objData)
        {
            List<ReagentMasterData> result = null;

            try
            {
                ReagentMasterDA objDesignationTypeMasteDA = new ReagentMasterDA();
                result = objDesignationTypeMasteDA.GetReagentTypeDetailsByID(objData);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeleteReagentTypeDetailsByID(ReagentMasterData objData)
        {
            int result = 0;
            try
            {
                ReagentMasterDA objDepartmentTypeMasteDA = new ReagentMasterDA();
                result = objDepartmentTypeMasteDA.DeleteReagentTypeDetailsByID(objData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<ReagentMasterData> SearchReagentTypeDetails(ReagentMasterData objData)
        {
            List<ReagentMasterData> result = null;
            try
            {
                ReagentMasterDA objPatientTypeMasteDA = new ReagentMasterDA();
                result = objPatientTypeMasteDA.SearchReagentTypeDetails(objData);
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

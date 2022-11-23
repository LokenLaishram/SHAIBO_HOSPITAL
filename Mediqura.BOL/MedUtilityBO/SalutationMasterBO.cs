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
    public class SalutationMasterBO
    {
        public int UpdateSalutationDetails(SalutationMasterData objFloorMasterData)
        {
            int result = 0;
            try
            {
                SalutationMasterDA objFloorMasteDA = new SalutationMasterDA();
                result = objFloorMasteDA.UpdateSalutationDetails(objFloorMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<SalutationMasterData> SearchSalutationTypeDetails(SalutationMasterData objFloorTypeMasterData)
        {
            List<SalutationMasterData> result = null;
            try
            {
                SalutationMasterDA objFloorTypeMasteDA = new SalutationMasterDA();
                result = objFloorTypeMasteDA.SearchSalutationTypeDetails(objFloorTypeMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<SalutationMasterData> GetSaluatationDetailsByID(SalutationMasterData objDepartmentTypeMasterData)
        {
            List<SalutationMasterData> result = null;

            try
            {
                SalutationMasterDA objDepartmentTypeMasteDA = new SalutationMasterDA();
                result = objDepartmentTypeMasteDA.GetSaluatationDetailsByID(objDepartmentTypeMasterData);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeleteSalutationDetailsByID(SalutationMasterData objDepartmentTypeMasterData)
        {
            int result = 0;
            try
            {
                SalutationMasterDA objPatientTypeMasteDA = new SalutationMasterDA();
                result = objPatientTypeMasteDA.DeleteSalutationDetailsByID(objDepartmentTypeMasterData);
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

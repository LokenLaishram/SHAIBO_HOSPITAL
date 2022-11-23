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
    public class StaffCategoryBO
    {
        public int UpdateStaffCategoryDetails(StaffCategoryData objOTRoleMasterData)
        {
            int result = 0;
            try
            {
                StaffCategoryDA objOTRoleMasterDA = new StaffCategoryDA();
                result = objOTRoleMasterDA.UpdateStaffCategoryDetails(objOTRoleMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<StaffCategoryData> SearchStaffCategory(StaffCategoryData objOTRoleMasterData)
        {
            List<StaffCategoryData> result = null;
            try
            {
                StaffCategoryDA objOTRoleMasterDA = new StaffCategoryDA();
                result = objOTRoleMasterDA.SearchStaffCategory(objOTRoleMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<StaffCategoryData> GetStaffCategoryDetailsByID(StaffCategoryData objOTRoleMasterData)
        {
            List<StaffCategoryData> result = null;

            try
            {
                StaffCategoryDA objOTRoleMasterDA = new StaffCategoryDA();
                result = objOTRoleMasterDA.GetStaffCategoryDetailsByID(objOTRoleMasterData);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeleteStaffCategoryDetailsByID(StaffCategoryData objOTRoleMasterData)
        {
            int result = 0;
            try
            {
                StaffCategoryDA objOTRoleMasterDA = new StaffCategoryDA();
                result = objOTRoleMasterDA.DeleteStaffCategoryDetailsByID(objOTRoleMasterData);
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

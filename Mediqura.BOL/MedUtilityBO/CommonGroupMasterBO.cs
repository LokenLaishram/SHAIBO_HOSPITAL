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
    public class CommonGroupMasterBO
    {
        public int UpdateCommonGroupTypeDetails(CommonGroupMasterData objLabGroupTypeMasterData)
        {
            int result = 0;
            try
            {
                CommonGroupMasterDA objLabGroupTypeMasterDA = new CommonGroupMasterDA();
                result = objLabGroupTypeMasterDA.UpdateCommonGroupTypeDetails(objLabGroupTypeMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<CommonGroupMasterData> GetCommonGroupTypeDetailsByID(CommonGroupMasterData objLabGroupTypeMasterData)
        {
            List<CommonGroupMasterData> result = null;

            try
            {
                CommonGroupMasterDA objLabGroupTypeMasteDA = new CommonGroupMasterDA();
                result = objLabGroupTypeMasteDA.GetCommonGroupTypeDetailsByID(objLabGroupTypeMasterData);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeleteCommonGroupTypeDetailsByID(CommonGroupMasterData objLabGroupTypeMasterData)
        {
            int result = 0;
            try
            {
                CommonGroupMasterDA objDepartmentTypeMasteDA = new CommonGroupMasterDA();
                result = objDepartmentTypeMasteDA.DeleteCommonGroupTypeDetailsByID(objLabGroupTypeMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<CommonGroupMasterData> SearchCommonGroupTypeDetails(CommonGroupMasterData objLabGroupTypeMasterData)
        {
            List<CommonGroupMasterData> result = null;
            try
            {
                CommonGroupMasterDA objLabGroupTypeMasteDA = new CommonGroupMasterDA();
                result = objLabGroupTypeMasteDA.SearchCommonGroupTypeDetails(objLabGroupTypeMasterData);
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

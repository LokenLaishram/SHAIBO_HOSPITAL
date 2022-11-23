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
    public class CommonSubGroupMasterBO
    {
        public int UpdateCommonSubGroupDetails(CommonSubGroupMasterData objLabSubGroupTypeMasterData)
        {
            int result = 0;
            try
            {
                CommonSubGroupMasterDA objLabSubGroupTypeMasterDA = new CommonSubGroupMasterDA();
                result = objLabSubGroupTypeMasterDA.UpdateCommonSubGroupDetails(objLabSubGroupTypeMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<CommonSubGroupMasterData> GetCommonSubGroupTypeDetailsByID(CommonSubGroupMasterData objLabSubGroupTypeMasterData)
        {
            List<CommonSubGroupMasterData> result = null;

            try
            {
                CommonSubGroupMasterDA objLabGroupTypeMasteDA = new CommonSubGroupMasterDA();
                result = objLabGroupTypeMasteDA.GetCommonSubGroupTypeDetailsByID(objLabSubGroupTypeMasterData);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeleteCommonSubGroupTypeDetailsByID(CommonSubGroupMasterData objLabSubGroupTypeMasterData)
        {
            int result = 0;
            try
            {
                CommonSubGroupMasterDA objSubGroupTypeMasterDA = new CommonSubGroupMasterDA();
                result = objSubGroupTypeMasterDA.DeleteCommonSubGroupTypeDetailsByID(objLabSubGroupTypeMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<CommonSubGroupMasterData> SearchSubGroupDetails(CommonSubGroupMasterData objLabSubGroupTypeMasterData)
        {
            List<CommonSubGroupMasterData> result = null;
            try
            {
                CommonSubGroupMasterDA objLabSubGroupTypeMasteDA = new CommonSubGroupMasterDA();
                result = objLabSubGroupTypeMasteDA.SearchSubGroupDetails(objLabSubGroupTypeMasterData);
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

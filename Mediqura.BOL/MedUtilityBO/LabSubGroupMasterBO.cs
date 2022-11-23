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
    public class LabSubGroupMasterBO
    {
        public int UpdateLabSubGroupDetails(LabSubGroupMasterData objLabSubGroupTypeMasterData)
        {
            int result = 0;
            try
            {
                LabSubGroupTypeMasterDA objLabSubGroupTypeMasterDA = new LabSubGroupTypeMasterDA();
                result = objLabSubGroupTypeMasterDA.UpdateLabSubGroupTypeDetails(objLabSubGroupTypeMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<LabSubGroupMasterData> GetLabSubGroupTypeDetailsByID(LabSubGroupMasterData objLabSubGroupTypeMasterData)
        {
            List<LabSubGroupMasterData> result = null;

            try
            {
                LabSubGroupTypeMasterDA objLabGroupTypeMasteDA = new LabSubGroupTypeMasterDA();
                result = objLabGroupTypeMasteDA.GetLabSubGroupTypeDetailsByID(objLabSubGroupTypeMasterData);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeleteLabSubGroupTypeDetailsByID(LabSubGroupMasterData objLabSubGroupTypeMasterData)
        {
            int result = 0;
            try
            {
                LabSubGroupTypeMasterDA objSubGroupTypeMasterDA = new LabSubGroupTypeMasterDA();
                result = objSubGroupTypeMasterDA.DeleteLabSubGroupTypeDetailsByID(objLabSubGroupTypeMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<LabSubGroupMasterData> SearchSubGroupDetails(LabSubGroupMasterData objLabSubGroupTypeMasterData)
        {
            List<LabSubGroupMasterData> result = null;
            try
            {
                LabSubGroupTypeMasterDA objLabSubGroupTypeMasteDA = new LabSubGroupTypeMasterDA();
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
        public List<LabSubGroupMasterData> SearchSubGroupTypeDetails(LabSubGroupMasterData objLabSubGroupTypeMasterData)
        {
            List<LabSubGroupMasterData> result = null;
            try
            {
                LabSubGroupTypeMasterDA objLabSubGroupTypeMasteDA = new LabSubGroupTypeMasterDA();
                result = objLabSubGroupTypeMasteDA.SearchSubGroupTypeDetails(objLabSubGroupTypeMasterData);
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

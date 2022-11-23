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
    public class LabGroupTypeMasterBO
    {
        public int UpdateLabGroupTypeDetails(LabGroupTypeMasterData objLabGroupTypeMasterData)
        {
            int result = 0;
            try
            {
                LabGroupTypeMasterDA objLabGroupTypeMasterDA = new LabGroupTypeMasterDA();
                result = objLabGroupTypeMasterDA.UpdateLabGroupTypeDetails(objLabGroupTypeMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<LabGroupTypeMasterData> GetLabGroupTypeDetailsByID(LabGroupTypeMasterData objLabGroupTypeMasterData)
        {
            List<LabGroupTypeMasterData> result = null;

            try
            {
                LabGroupTypeMasterDA objLabGroupTypeMasteDA = new LabGroupTypeMasterDA();
                result = objLabGroupTypeMasteDA.GetLabGroupTypeDetailsByID(objLabGroupTypeMasterData);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeleteLabGroupTypeDetailsByID(LabGroupTypeMasterData objLabGroupTypeMasterData)
        {
            int result = 0;
            try
            {
                LabGroupTypeMasterDA objDepartmentTypeMasteDA = new LabGroupTypeMasterDA();
                result = objDepartmentTypeMasteDA.DeleteLabGroupTypeDetailsByID(objLabGroupTypeMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<LabGroupTypeMasterData> SearchLabGroupTypeDetails(LabGroupTypeMasterData objLabGroupTypeMasterData)
        {
            List<LabGroupTypeMasterData> result = null;
            try
            {
                LabGroupTypeMasterDA objLabGroupTypeMasteDA = new LabGroupTypeMasterDA();
                result = objLabGroupTypeMasteDA.SearchLabGroupTypeDetails(objLabGroupTypeMasterData);
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

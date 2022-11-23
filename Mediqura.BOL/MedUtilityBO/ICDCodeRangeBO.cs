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
    public class ICDCodeRangeBO
    {
        public int UpdateLabSubGroupDetails(ICDCodeRangeData objLabSubGroupTypeMasterData)
        {
            int result = 0;
            try
            {
                ICDCodeRangeDA objLabSubGroupTypeMasterDA = new ICDCodeRangeDA();
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
        public List<ICDCodeRangeData> GetLabSubGroupTypeDetailsByID(ICDCodeRangeData objLabSubGroupTypeMasterData)
        {
            List<ICDCodeRangeData> result = null;

            try
            {
                ICDCodeRangeDA objLabGroupTypeMasteDA = new ICDCodeRangeDA();
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
        public int DeleteLabSubGroupTypeDetailsByID(ICDCodeRangeData objLabSubGroupTypeMasterData)
        {
            int result = 0;
            try
            {
                ICDCodeRangeDA objSubGroupTypeMasterDA = new ICDCodeRangeDA();
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
        public List<ICDCodeRangeData> SearchSubGroupDetails(ICDCodeRangeData objLabSubGroupTypeMasterData)
        {
            List<ICDCodeRangeData> result = null;
            try
            {
                ICDCodeRangeDA objLabSubGroupTypeMasteDA = new ICDCodeRangeDA();
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

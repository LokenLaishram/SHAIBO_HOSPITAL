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
     public class WardTypeMasterBO
    {
        public int UpdateWardDetails(WardMasterData objWardMasterData)
        {
            int result = 0;
            try
            {
                WardMasterDA objBlockMasteDA = new WardMasterDA();
                result = objBlockMasteDA.UpdateWardDetails(objWardMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<WardMasterData> GetWardTypeDetailsByID(WardMasterData objBlockTypeMasterData)
        {
            List<WardMasterData> result = null;

            try
            {
                WardMasterDA objBlockTypeMasteDA = new WardMasterDA();
                result = objBlockTypeMasteDA.GetWardTypeDetailsByID(objBlockTypeMasterData);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeleteWardTypeDetailsByID(WardMasterData objBlockTypeMasterData)
        {
            int result = 0;
            try
            {
                WardMasterDA objBlockTypeMasteDA = new WardMasterDA();
                result = objBlockTypeMasteDA.DeleteWardTypeDetailsByID(objBlockTypeMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<WardMasterData> SearchWardTypeDetails(WardMasterData objBlockTypeMasterData)
        {
            List<WardMasterData> result = null;
            try
            {
                WardMasterDA objBlockTypeMasteDA = new WardMasterDA();
                result = objBlockTypeMasteDA.SearchWardTypeDetails(objBlockTypeMasterData);
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

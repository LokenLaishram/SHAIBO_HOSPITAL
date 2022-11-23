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
    public class ICDcodecategoryBO
    {
        public int UpdateICDCodeCategoryDetails(ICDcodecategoryData objData)
        {
            int result = 0;
            try
            {
                ICDcodecategoryDA objMasteDA = new ICDcodecategoryDA();
                result = objMasteDA.UpdateICDCodeCategoryDetails(objData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<ICDcodecategoryData> GetICDCodeDetailsByID(ICDcodecategoryData objMasterData)
        {
            List<ICDcodecategoryData> result = null;

            try
            {
                ICDcodecategoryDA objMasteDA = new ICDcodecategoryDA();
                result = objMasteDA.GetICDCodeDetailsByID(objMasterData);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeleteICDCOdeDetailsByID(ICDcodecategoryData objData)
        {
            int result = 0;
            try
            {
                ICDcodecategoryDA objMasteDA = new ICDcodecategoryDA();
                result = objMasteDA.DeleteICDCOdeDetailsByID(objData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<ICDcodecategoryData> SearchICDCodeCategoryDetails(ICDcodecategoryData objData)
        {
            List<ICDcodecategoryData> result = null;
            try
            {
                ICDcodecategoryDA objMasteDA = new ICDcodecategoryDA();
                result = objMasteDA.SearchICDCodeCategoryDetails(objData);
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

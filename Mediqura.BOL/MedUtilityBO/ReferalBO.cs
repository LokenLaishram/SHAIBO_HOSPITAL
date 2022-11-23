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
    public class ReferalBO
    {
        public int UpdateReferalDetails(ReferalData objReferalData)
        {
            int result = 0;
            try
            {
                ReferalDA objreferalDA = new ReferalDA();
                result = objreferalDA.UpdateReferalDetails(objReferalData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<ReferalData> GetReferalDetailsByID(ReferalData objBlockTypeMasterData)
        {
            List<ReferalData> result = null;

            try
            {
                ReferalDA objBlockTypeMasteDA = new ReferalDA();
                result = objBlockTypeMasteDA.GetReferalDetailsByID(objBlockTypeMasterData);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeleteReferalDetailsByID(ReferalData objBlockTypeMasterData)
        {
            int result = 0;
            try
            {
                ReferalDA objBlockTypeMasteDA = new ReferalDA();
                result = objBlockTypeMasteDA.DeleteReferalDetailsByID(objBlockTypeMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<ReferalData> SearchReferalDetails(ReferalData objBlockTypeMasterData)
        {
            List<ReferalData> result = null;
            try
            {
                ReferalDA objBlockTypeMasteDA = new ReferalDA();
                result = objBlockTypeMasteDA.SearchReferalDetails(objBlockTypeMasterData);
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

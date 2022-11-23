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
    public class GuestDocBO
    {
        public int UpdateGuestDocDetails(GuestDocData objGuestDocData)
        {
            int result = 0;
            try
            {
                GuestDocDA objGuestDocDA = new GuestDocDA();
                result = objGuestDocDA.UpdateGuestDocDetails(objGuestDocData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<GuestDocData> GetGuestDocDetailsByID(GuestDocData objBlockTypeMasterData)
        {
            List<GuestDocData> result = null;

            try
            {
                GuestDocDA objBlockTypeMasteDA = new GuestDocDA();
                result = objBlockTypeMasteDA.GetGuestDocDetailsByID(objBlockTypeMasterData);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeleteGuestDocDetailsByID(GuestDocData objBlockTypeMasterData)
        {
            int result = 0;
            try
            {
                GuestDocDA objBlockTypeMasteDA = new GuestDocDA();
                result = objBlockTypeMasteDA.DeleteGuestDocDetailsByID(objBlockTypeMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<GuestDocData> SearchGuestDocDetails(GuestDocData objBlockTypeMasterData)
        {
            List<GuestDocData> result = null;
            try
            {
                GuestDocDA objBlockTypeMasteDA = new GuestDocDA();
                result = objBlockTypeMasteDA.SearchGuestDocDetails(objBlockTypeMasterData);
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

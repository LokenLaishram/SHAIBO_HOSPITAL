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
    public class RoomBO
    {
        public int UpdateBedDetails(RoomData objOTRoleMasterData)
        {
            int result = 0;
            try
            {
                RoomDA objOTRoleMasterDA = new RoomDA();
                result = objOTRoleMasterDA.UpdateBedDetails(objOTRoleMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<RoomData> SearchBedDetails(RoomData objOTRoleMasterData)
        {
            List<RoomData> result = null;
            try
            {
                RoomDA objOTRoleMasterDA = new RoomDA();
                result = objOTRoleMasterDA.SearchBedDetails(objOTRoleMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<RoomData> GetOTRoleDetailsByID(RoomData objOTRoleMasterData)
        {
            List<RoomData> result = null;

            try
            {
                RoomDA objOTRoleMasterDA = new RoomDA();
                result = objOTRoleMasterDA.GetOTRoleDetailsByID(objOTRoleMasterData);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeleteOTRoleDetailsByID(RoomData objOTRoleMasterData)
        {
            int result = 0;
            try
            {
                RoomDA objOTRoleMasterDA = new RoomDA();
                result = objOTRoleMasterDA.DeleteOTRoleDetailsByID(objOTRoleMasterData);
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

using Mediqura.CommonData.MedUtilityData;
using Mediqura.DAL.MedUtilityDA;
using Mediqura.DAL.MedOPDDA;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mediqura.CommonData.MedOPDData;
using Mediqura.CommonData.MedNurseData;
using Mediqura.DAL.MedNurseDA;


namespace Mediqura.BOL.MedNurseBO
{
    public class NurseStationBO
    {
        public int UpdateOTRoleDetails(NurseStationData objOTRoleMasterData)
        {
            int result = 0;
            try
            {
                NurseStationDA objOTRoleMasterDA = new NurseStationDA();
                result = objOTRoleMasterDA.UpdateOTRoleDetails(objOTRoleMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<NurseStationData> SearchOTRoleDetails(NurseStationData objOTRoleMasterData)
        {
            List<NurseStationData> result = null;
            try
            {
                NurseStationDA objOTRoleMasterDA = new NurseStationDA();
                result = objOTRoleMasterDA.SearchOTRoleDetails(objOTRoleMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<NurseStationData> GetOTRoleDetailsByID(NurseStationData objOTRoleMasterData)
        {
            List<NurseStationData> result = null;

            try
            {
                NurseStationDA objOTRoleMasterDA = new NurseStationDA();
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
        public int DeleteOTRoleDetailsByID(NurseStationData objOTRoleMasterData)
        {
            int result = 0;
            try
            {
                NurseStationDA objOTRoleMasterDA = new NurseStationDA();
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

        public int UpdateWardToNurseDetails(WardToStationData objOTRoleMasterData)
        {
            int result = 0;
            try
            {
                NurseStationDA objOTRoleMasterDA = new NurseStationDA();
                result = objOTRoleMasterDA.UpdateWardToNurseDetails(objOTRoleMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<WardToStationData> SearchWardToNurseDetails(WardToStationData objOTRoleMasterData)
        {
            List<WardToStationData> result = null;
            try
            {
                NurseStationDA objOTRoleMasterDA = new NurseStationDA();
                result = objOTRoleMasterDA.SearchWardToNurseDetails(objOTRoleMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<WardToStationData> GetWardToNurseDetailsByID(WardToStationData objOTRoleMasterData)
        {
            List<WardToStationData> result = null;

            try
            {
                NurseStationDA objOTRoleMasterDA = new NurseStationDA();
                result = objOTRoleMasterDA.GetWardToNurseDetailsByID(objOTRoleMasterData);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeleteWardToNurseDetailsByID(WardToStationData objOTRoleMasterData)
        {
            int result = 0;
            try
            {
                NurseStationDA objOTRoleMasterDA = new NurseStationDA();
                result = objOTRoleMasterDA.DeleteWardToNurseDetailsByID(objOTRoleMasterData);
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

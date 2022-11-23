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
    public class MachineMasterBO
    {
        public int UpdateMachineDetails(MachineMasterData objData)
        {
            int result = 0;
            try
            {
                MachineMasterDA objMasteDA = new MachineMasterDA();
                result = objMasteDA.UpdateDesignationTypeDetails(objData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<MachineMasterData> GetMachineTypeDetailsByID(MachineMasterData objDesignationTypeMasterData)
        {
            List<MachineMasterData> result = null;

            try
            {
                MachineMasterDA objMasteDA = new MachineMasterDA();
                result = objMasteDA.GetDesignationTypeDetailsByID(objDesignationTypeMasterData);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeleteMachineTypeDetailsByID(MachineMasterData objDesignationTypeMasterData)
        {
            int result = 0;
            try
            {
                MachineMasterDA objMasteDA = new MachineMasterDA();
                result = objMasteDA.DeleteDesignationTypeDetailsByID(objDesignationTypeMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<MachineMasterData> SearchMachineTypeDetails(MachineMasterData objDesignationTypeMasterData)
        {
            List<MachineMasterData> result = null;
            try
            {
                MachineMasterDA objMasteDA = new MachineMasterDA();
                result = objMasteDA.SearchMachineTypeDetails(objDesignationTypeMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<MachineMasterData> SearchLabMachineTypeDetails(MachineMasterData objDesignationTypeMasterData)
        {
            List<MachineMasterData> result = null;
            try
            {
                MachineMasterDA objMasteDA = new MachineMasterDA();
                result = objMasteDA.SearchLabMachineTypeDetails(objDesignationTypeMasterData);
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

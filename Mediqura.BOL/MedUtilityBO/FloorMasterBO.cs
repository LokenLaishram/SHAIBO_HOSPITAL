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
    public class FloorMasterBO
    {
        public int UpdateFloorDetails(FloorMasterData objFloorMasterData)
        {
            int result = 0;
            try
            {
                FloorMasterDA objFloorMasteDA = new FloorMasterDA();
                result = objFloorMasteDA.UpdateFloorDetails(objFloorMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<FloorMasterData> GetFloorTypeDetailsByID(FloorMasterData objFloorTypeMasterData)
        {
            List<FloorMasterData> result = null;

            try
            {
                FloorMasterDA objBlockTypeMasteDA = new FloorMasterDA();
                result = objBlockTypeMasteDA.GetFloorTypeDetailsByID(objFloorTypeMasterData);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeleteFloorTypeDetailsByID(FloorMasterData objFloorTypeMasterData)
        {
            int result = 0;
            try
            {
                FloorMasterDA objFloorTypeMasteDA = new FloorMasterDA();
                result = objFloorTypeMasteDA.DeleteFloorTypeDetailsByID(objFloorTypeMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<FloorMasterData> SearchFloorTypeDetails(FloorMasterData objFloorTypeMasterData)
        {
            List<FloorMasterData> result = null;
            try
            {
                FloorMasterDA objFloorTypeMasteDA = new FloorMasterDA();
                result = objFloorTypeMasteDA.SearchFloorTypeDetails(objFloorTypeMasterData);
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

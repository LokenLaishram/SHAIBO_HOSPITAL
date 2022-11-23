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
    public class LabUnitBO
    {
        public int UpdateLabUnitDetails(LabUnitData objLabUnitMasterData)
        {
            int result = 0;
            try
            {
                LabUnitDA objLabUnitMasterDA = new LabUnitDA();
                result = objLabUnitMasterDA.UpdateLabUnitDetails(objLabUnitMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<LabUnitData> SearchLabUnitDetails(LabUnitData objLabUnitMasterData)
        {
            List<LabUnitData> result = null;
            try
            {
                LabUnitDA objLabUnitMasterDA = new LabUnitDA();
                result = objLabUnitMasterDA.SearchLabUnitDetails(objLabUnitMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<LabUnitData> GetLabUnitDetailsByID(LabUnitData objLabUnitMasterData)
        {
            List<LabUnitData> result = null;

            try
            {
                LabUnitDA objLabUnitMasterDA = new LabUnitDA();
                result = objLabUnitMasterDA.GetLabUnitDetailsByID(objLabUnitMasterData);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeleteLabUnitDetailsByID(LabUnitData objLabUnitMasterData)
        {
            int result = 0;
            try
            {
                LabUnitDA objLabUnitMasterDA = new LabUnitDA();
                result = objLabUnitMasterDA.DeleteLabUnitDetailsByID(objLabUnitMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<LabUnitData> SearchLabTypeUnitDetails(LabUnitData objLabUnitMasterData)
        {
            List<LabUnitData> result = null;
            try
            {
                LabUnitDA objLabUnitMasterDA = new LabUnitDA();
                result = objLabUnitMasterDA.SearchLabTypeUnitDetails(objLabUnitMasterData);
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

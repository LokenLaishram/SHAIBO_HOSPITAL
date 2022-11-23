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
    public class LabRangeMasterBO
    {
        public int UpdateLabRangeDetails(LabRangeMasterData objLabRangeMasterData)
        {
            int result = 0;
            try
            {
                LabRangeMasterDA objLabRangeMasterDA = new LabRangeMasterDA();
                result = objLabRangeMasterDA.UpdateLabRangeDetails(objLabRangeMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<LabRangeMasterData> GetLabRangeDetailsByID(LabRangeMasterData objLabRangeMasterData)
        {
            List<LabRangeMasterData> result = null;

            try
            {
                LabRangeMasterDA objLabRangeMasteDA = new LabRangeMasterDA();
                result = objLabRangeMasteDA.GetLabRangeDetailsByID(objLabRangeMasterData);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeleteLabRangeDetailsByID(LabRangeMasterData objLabRangeMasterData)
        {
            int result = 0;
            try
            {
                LabRangeMasterDA objLabRangeMasteDA = new LabRangeMasterDA();
                result = objLabRangeMasteDA.DeleteLabRangeDetailsByID(objLabRangeMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<LabRangeMasterData> SearchLabRangeDetails(LabRangeMasterData objLabRangeMasterData)
        {
            List<LabRangeMasterData> result = null;
            try
            {
                LabRangeMasterDA objLabRangeMasteDA = new LabRangeMasterDA();
                result = objLabRangeMasteDA.SearchLabRangeDetails(objLabRangeMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }

        public int DeleteLabRangeTypeDetailsByID(LabRangeMasterData objLabRangeMasterData)
        {
            throw new NotImplementedException();
        }
        public List<LabRangeMasterData> GetTestName(LabRangeMasterData objD)
        {
            List<LabRangeMasterData> result = null;
            try
            {
                LabRangeMasterDA objpatientDA = new LabRangeMasterDA();
                result = objpatientDA.GetTestName(objD);

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

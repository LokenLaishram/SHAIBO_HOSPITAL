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
    public class LabTemplateBO
    {
        public int UpdateLabTemplateDetails(LabTemplateData objMasterData)
        {
            int result = 0;
            try
            {
                LabTemplateDA objLabUnitMasterDA = new LabTemplateDA();
                result = objLabUnitMasterDA.UpdateLabTemplateDetails(objMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<LabTemplateData> SearchLabTemplateDetails(LabTemplateData objLabUnitMasterData)
        {
            List<LabTemplateData> result = null;
            try
            {
                LabTemplateDA objLabUnitMasterDA = new LabTemplateDA();
                result = objLabUnitMasterDA.SearchLabTemplateDetails(objLabUnitMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<LabTemplateData> GetLabTemplateDetailsByID(LabTemplateData objLabUnitMasterData)
        {
            List<LabTemplateData> result = null;

            try
            {
                LabTemplateDA objLabUnitMasterDA = new LabTemplateDA();
                result = objLabUnitMasterDA.GetLabTemplateDetailsByID(objLabUnitMasterData);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<LabTemplateData> SearchLabTemplate(LabTemplateData objLabUnitMasterData)
        {
            List<LabTemplateData> result = null;
            try
            {
                LabTemplateDA objLabUnitMasterDA = new LabTemplateDA();
                result = objLabUnitMasterDA.SearchLabTemplate(objLabUnitMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
    
        public int DeleteLabTemplateDetailsByID(LabTemplateData objLabUnitMasterData)
        {
            int result = 0;
            try
            {
                LabTemplateDA objLabUnitMasterDA = new LabTemplateDA();
                result = objLabUnitMasterDA.DeleteLabTemplateDetailsByID(objLabUnitMasterData);
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

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
    public class TPAMasterBO
    {
        public int UpdateTPADetails(TPAMasterData objData)
        {
            int result = 0;
            try
            {
                TPAMasterDA objMasteDA = new TPAMasterDA();
                result = objMasteDA.UpdateTPADetails(objData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<TPAMasterData> GetTPADetailsByID(TPAMasterData objMasterData)
        {
            List<TPAMasterData> result = null;

            try
            {
                TPAMasterDA objDA = new TPAMasterDA();
                result = objDA.GetTPADetailsByID(objMasterData);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeletePTATypeDetailsByID(TPAMasterData objData)
        {
            int result = 0;
            try
            {
                TPAMasterDA objPatientTypeMasteDA = new TPAMasterDA();
                result = objPatientTypeMasteDA.DeletePTATypeDetailsByID(objData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<TPAMasterData> SearchTPATypeDetails(TPAMasterData objData)
        {
            List<TPAMasterData> result = null;
            try
            {
                TPAMasterDA objMasteDA = new TPAMasterDA();
                result = objMasteDA.SearchTPATypeDetails(objData);
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

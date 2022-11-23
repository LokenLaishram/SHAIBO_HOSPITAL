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
    public class NurseTypeMasterBO
    {
        public int UpdateNurseTypeDetails(NurseTypeMasterData objData)
        {
            int result = 0;
            try
            {
                NurseTypeMasterDA objMasteDA = new NurseTypeMasterDA();
                result = objMasteDA.UpdateNurseTypeDetails(objData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<NurseTypeMasterData> GetNurseTyeDetailsByID(NurseTypeMasterData objMasterData)
        {
            List<NurseTypeMasterData> result = null;

            try
            {
                NurseTypeMasterDA objDA = new NurseTypeMasterDA();
                result = objDA.GetNurseTyeDetailsByID(objMasterData);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeleteNurseTypeDetailsByID(NurseTypeMasterData objData)
        {
            int result = 0;
            try
            {
                NurseTypeMasterDA objPatientTypeMasteDA = new NurseTypeMasterDA();
                result = objPatientTypeMasteDA.DeleteNurseTypeDetailsByID(objData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<NurseTypeMasterData> SearchTPATypeDetails(NurseTypeMasterData objData)
        {
            List<NurseTypeMasterData> result = null;
            try
            {
                NurseTypeMasterDA objMasteDA = new NurseTypeMasterDA();
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

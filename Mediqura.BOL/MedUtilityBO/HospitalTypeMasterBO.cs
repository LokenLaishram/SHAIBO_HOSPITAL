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
    public class HospitalTypeMasterBO
    {
        public int UpdateHospitalTypeDetails(HospitalTypeMasterData objHospitalTypeMasterData)
        {
            int result = 0;
            try
            {
                HospitalTypeMasterDA objHospitalTypeMasterDA = new HospitalTypeMasterDA();
                result = objHospitalTypeMasterDA.UpdateHospitalTypeDetails(objHospitalTypeMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<HospitalTypeMasterData> GetHospitalTypeDetailsByID(HospitalTypeMasterData objHospitalTypeMasterData)
        {
            List<HospitalTypeMasterData> result = null;

            try
            {
                HospitalTypeMasterDA objHospitalTypeMasteDA = new HospitalTypeMasterDA();
                result = objHospitalTypeMasteDA.GetHospitalTypeDetailsByID(objHospitalTypeMasterData);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeleteHospitalTypeDetailsByID(HospitalTypeMasterData objHospitalTypeMasterData)
        {
            int result = 0;
            try
            {
                HospitalTypeMasterDA objHospitalTypeMasteDA = new HospitalTypeMasterDA();
                result = objHospitalTypeMasteDA.DeleteHospitalTypeDetailsByID(objHospitalTypeMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<HospitalTypeMasterData> SearchHospitalTypeDetails(HospitalTypeMasterData objHospitalTypeMasterData)
        {
            List<HospitalTypeMasterData> result = null;
            try
            {
                HospitalTypeMasterDA objHospitalTypeMasteDA = new HospitalTypeMasterDA();
                result = objHospitalTypeMasteDA.SearchHospitalTypeDetails(objHospitalTypeMasterData);
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

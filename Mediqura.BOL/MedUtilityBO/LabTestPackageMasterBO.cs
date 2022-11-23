using System;
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
    public class LabTestPackageMasterBO
    {
        public int UpdateLabTestPackageDetails(LabTestPackageMasterData objData)
        {
            int result = 0;
            try
            {
                LabTestPackageMasterDA objMasteDA = new LabTestPackageMasterDA();
                result = objMasteDA.UpdateLabTestPackageDetails(objData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<LabTestPackageMasterData> GetLabTestPackageDetailsByID(LabTestPackageMasterData objMasterData)
        {
            List<LabTestPackageMasterData> result = null;

            try
            {
                LabTestPackageMasterDA objDA = new LabTestPackageMasterDA();
                result = objDA.GetLabTestPackageDetailsByID(objMasterData);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeleteLabTestPackageDetailsByID(LabTestPackageMasterData objData)
        {
            int result = 0;
            try
            {
                LabTestPackageMasterDA objPatientTypeMasteDA = new LabTestPackageMasterDA();
                result = objPatientTypeMasteDA.DeleteLabTestPackageDetailsByID(objData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<LabTestPackageMasterData> SearchLabTestPackageDetails(LabTestPackageMasterData objData)
        {
            List<LabTestPackageMasterData> result = null;
            try
            {
                LabTestPackageMasterDA objMasteDA = new LabTestPackageMasterDA();
                result = objMasteDA.SearchLabTestPackageDetails(objData);
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

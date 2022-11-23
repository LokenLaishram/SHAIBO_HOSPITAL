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
    public class PackageMasterBO
    {
        public int UpdatePackageDetails(PackageMasterData objData)
        {
            int result = 0;
            try
            {
                PackageMasterDA objMasteDA = new PackageMasterDA();
                result = objMasteDA.UpdatePackageDetails(objData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<PackageMasterData> GetPackageDetailsByID(PackageMasterData objMasterData)
        {
            List<PackageMasterData> result = null;

            try
            {
                PackageMasterDA objDA = new PackageMasterDA();
                result = objDA.GetPackageDetailsByID(objMasterData);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeletePackageTypeDetailsByID(PackageMasterData objData)
        {
            int result = 0;
            try
            {
                PackageMasterDA objPatientTypeMasteDA = new PackageMasterDA();
                result = objPatientTypeMasteDA.DeletePackageTypeDetailsByID(objData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<PackageMasterData> SearchPackageTypeDetails(PackageMasterData objData)
        {
            List<PackageMasterData> result = null;
            try
            {
                PackageMasterDA objMasteDA = new PackageMasterDA();
                result = objMasteDA.SearchPackageTypeDetails(objData);
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

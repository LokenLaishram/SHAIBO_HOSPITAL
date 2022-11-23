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
    public class PackageWiseServiceBO
    {
        public int UpdatePackageServiceDetails(PackageWiseServiceData objData)
        {
            int result = 0;
            try
            {
                PackageWiseServiceDA objMasteDA = new PackageWiseServiceDA();
                result = objMasteDA.UpdatePackageServiceDetails(objData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<PackageWiseServiceData> GetPackageServiceDetailsByID(PackageWiseServiceData objMasterData)
        {
            List<PackageWiseServiceData> result = null;

            try
            {
                PackageWiseServiceDA objDA = new PackageWiseServiceDA();
                result = objDA.GetPackageServiceDetailsByID(objMasterData);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeletePackageServiceDetailsByID(PackageWiseServiceData objData)
        {
            int result = 0;
            try
            {
                PackageWiseServiceDA objPatientTypeMasteDA = new PackageWiseServiceDA();
                result = objPatientTypeMasteDA.DeletePackageServiceDetailsByID(objData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<PackageWiseServiceData> GetPackageService(PackageWiseServiceData objData)
        {
            List<PackageWiseServiceData> result = null;
            try
            {
                PackageWiseServiceDA objMasteDA = new PackageWiseServiceDA();
                result = objMasteDA.GetPackageService(objData);
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

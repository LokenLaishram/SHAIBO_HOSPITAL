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
    public class DistrictBO
    {
        public int UpdateDistrictDetails(DistrictData objCountryMSTData)
        {
            int result = 0;
            try
            {
                DistrictDA objCountryMSTDA = new DistrictDA();
                result = objCountryMSTDA.UpdateDistrictDetails(objCountryMSTData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<DistrictData> SearchDistrictDetails(DistrictData objCountryMSTData)
        {
            List<DistrictData> result = null;
            try
            {
                DistrictDA objCountryMSTDA = new DistrictDA();
                result = objCountryMSTDA.SearchDistrictDetails(objCountryMSTData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<DistrictData> GetDistrictDetailsByID(DistrictData objCountryMSTData)
        {
            List<DistrictData> result = null;

            try
            {
                DistrictDA objCountryMSTDA = new DistrictDA();
                result = objCountryMSTDA.GetDistrictDetailsByID(objCountryMSTData);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeleteDistrictDetailsByID(DistrictData objCountryMSTData)
        {
            int result = 0;
            try
            {
                DistrictDA objCountryMSTDA = new DistrictDA();
                result = objCountryMSTDA.DeleteDistrictDetailsByID(objCountryMSTData);
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

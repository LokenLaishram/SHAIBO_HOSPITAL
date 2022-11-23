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
    public class CountryBO
    {
        public int UpdateCountryDetails(CountryData objCountryMSTData)
        {
            int result = 0;
            try
            {
                CountryDA objCountryMSTDA = new CountryDA();
                result = objCountryMSTDA.UpdateCountryDetails(objCountryMSTData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<CountryData> SearchCountryDetails(CountryData objCountryMSTData)
        {
            List<CountryData> result = null;
            try
            {
                CountryDA objCountryMSTDA = new CountryDA();
                result = objCountryMSTDA.SearchCountryDetails(objCountryMSTData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<CountryData> GetCountryDetailsByID(CountryData objCountryMSTData)
        {
            List<CountryData> result = null;

            try
            {
                CountryDA objCountryMSTDA = new CountryDA();
                result = objCountryMSTDA.GetCountryDetailsByID(objCountryMSTData);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeleteCountryDetailsByID(CountryData objCountryMSTData)
        {
            int result = 0;
            try
            {
                CountryDA objCountryMSTDA = new CountryDA();
                result = objCountryMSTDA.DeleteCountryDetailsByID(objCountryMSTData);
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

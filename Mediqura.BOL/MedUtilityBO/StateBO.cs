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
   public class StateBO
    {
       public int UpdateStateDetails(StateData objCountryMSTData)
        {
            int result = 0;
            try
            {
                StateDA objCountryMSTDA = new StateDA();
                result = objCountryMSTDA.UpdateStateDetails(objCountryMSTData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<StateData> SearchStateDetails(StateData objCountryMSTData)
        {
            List<StateData> result = null;
            try
            {
                StateDA objCountryMSTDA = new StateDA();
                result = objCountryMSTDA.SearchStateDetails(objCountryMSTData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<StateData> GetStateDetailsByID(StateData objCountryMSTData)
        {
            List<StateData> result = null;

            try
            {
                StateDA objCountryMSTDA = new StateDA();
                result = objCountryMSTDA.GetStateDetailsByID(objCountryMSTData);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeleteStateDetailsByID(StateData objCountryMSTData)
        {
            int result = 0;
            try
            {
                StateDA objCountryMSTDA = new StateDA();
                result = objCountryMSTDA.DeleteStateDetailsByID(objCountryMSTData);
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

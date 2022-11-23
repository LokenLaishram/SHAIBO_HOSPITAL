using Mediqura.CommonData.Common;
using Mediqura.DAL.CommonDA;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.BOL.CommonBO
{
    public class ApplicationBO
    {
        public List<ApplicationData> GetApplicationDetails(ApplicationData objdata)
        {
            List<ApplicationData> result = null;
            try
            {
                ApplicationDA objDA = new ApplicationDA();
                result = objDA.GetApplicationDetails(objdata);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<ApplicationData> UpdateFinancialyear(ApplicationData objdata)
        {
            List<ApplicationData> result = null;
            try
            {
                ApplicationDA objDA = new ApplicationDA();
                result = objDA.UpdateFinancialyear(objdata);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int UpdateApplicationUtility(ApplicationData objstock)
        {
            int result = 0;
            try
            {
                ApplicationDA objDA = new ApplicationDA();
                result = objDA.UpdateApplicationUtility(objstock);
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

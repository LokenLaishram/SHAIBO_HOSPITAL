using Mediqura.CommonData.MIS;
using Mediqura.DAL.MIS;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.BOL.MIS
{
    public class ServicesReportBO
    { 
        public List<ServicesReportData> GetServicesDetailList(ServicesReportData objData)
        {
            List<ServicesReportData> result = null;
            try
            {
                ServicesReportDA objDA = new ServicesReportDA();
                result = objDA.GetServicesDetailList(objData);
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

   
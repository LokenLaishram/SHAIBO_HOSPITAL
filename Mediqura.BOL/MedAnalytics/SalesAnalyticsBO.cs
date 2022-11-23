using Mediqura.CommonData.MedAnalytics;
using Mediqura.DAL.MedAnalytics;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.BOL.MedAnalytics
{
  public class SalesAnalyticsBO
    {
      public List<SaleAnalyticsGetData> GetSalesAnalytics(SalesAnalyticsData objsaleAnalytics)
        {
            List<SaleAnalyticsGetData> result = null;
            try
            {
                SaleAnalyticsDA objDA = new SaleAnalyticsDA();
                result = objDA.GetSalesAnalytics(objsaleAnalytics);
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

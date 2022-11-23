using Mediqura.CommonData.MedStore;
using Mediqura.DAL.MedStore;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.BOL.MedStore
{
    public class UtilizationReportBO
    {
        public List<UtilizationReportData> GetSubStockItemName(UtilizationReportData objUtili)
        {
            List<UtilizationReportData> result = null;
            try
            {
                UtilizationReportDA objUtilDA = new UtilizationReportDA();
                result = objUtilDA.GetSubStockItemName(objUtili);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<UtilizationReportData> GetUtilizationItemList(UtilizationReportData objutil)
        {
            List<UtilizationReportData> result = null;
            try
            {
                UtilizationReportDA objDA = new UtilizationReportDA();
                result = objDA.GetUtilizationItemList(objutil);
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

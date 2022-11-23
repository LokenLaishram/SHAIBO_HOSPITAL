using Mediqura.CommonData.MIS;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.BOL.MIS
{
   public class MisReportBO
    {
        public List<MisReportData> GetTransactionList(MisReportData objData)
        {
            List<MisReportData> result = null;
            try
            {
                MisReportDA objDA = new MisReportDA();
                result = objDA.GetTransactionList(objData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<MisSummaryReport> GetSummaryTransactionList(MisReportData objData)
        {
            List<MisSummaryReport> result = null;
            try
            {
                MisReportDA objDA = new MisReportDA();
                result = objDA.GetTransactionSummaryList(objData);
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

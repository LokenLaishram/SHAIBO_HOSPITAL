using Mediqura.CommonData.MedUtilityData;
using Mediqura.DAL.MedUtilityDA;
using Mediqura.DAL.MedOPDDA;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mediqura.CommonData.MedNurseData;
using Mediqura.DAL.MedNurseDA;

namespace Mediqura.BOL.MedNurseBO
{
    public class InvestigationChartBO
    {
        public List<InvestigationChartData> GetPatientName(InvestigationChartData objD)
        {
            List<InvestigationChartData> result = null;
            try
            {
                InvestigationChartDA objpatientDA = new InvestigationChartDA();
                result = objpatientDA.GetPatientName(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<InvestigationChartData> GetInvestPatientDetail(InvestigationChartData objD)
        {
            List<InvestigationChartData> result = null;
            try
            {
                InvestigationChartDA objpatientDA = new InvestigationChartDA();
                result = objpatientDA.GetInvestPatientDetail(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<InvestigationChartData> SearchInvestPatientDetail(InvestigationChartData objD)
        {
            List<InvestigationChartData> result = null;
            try
            {
                InvestigationChartDA objpatientDA = new InvestigationChartDA();
                result = objpatientDA.SearchInvestPatientDetail(objD);
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

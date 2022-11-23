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
    public class IntakeOutputChartBO
    {
        public List<IntakeOutputChartData> GetPatientName(IntakeOutputChartData objD)
        {
            List<IntakeOutputChartData> result = null;
            try
            {
                IntakeOutputChartDA objpatientDA = new IntakeOutputChartDA();
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
        public List<IntakeOutputChartData> GetIntakeOutputPatientDetail(IntakeOutputChartData objD)
        {
            List<IntakeOutputChartData> result = null;
            try
            {
                IntakeOutputChartDA objpatientDA = new IntakeOutputChartDA();
                result = objpatientDA.GetIntakeOutputPatientDetail(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }

        public int InsertIntakeOutputdetails(IntakeOutputChartData objpat)
        {
            int result = 0;

            try
            {
                IntakeOutputChartDA objpatientDA = new IntakeOutputChartDA();
                result = objpatientDA.InsertIntakeOutputdetails(objpat);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public int CancelIntakeOutputChartDataLists(IntakeOutputChartData objpat)
        {
            int result = 0;

            try
            {
                IntakeOutputChartDA objpatientDA = new IntakeOutputChartDA();
                result = objpatientDA.CancelIntakeOutputChartDataLists(objpat);

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

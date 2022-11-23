using Mediqura.CommonData.MedHrData;
using Mediqura.DAL.MedHrDA;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.BOL.MedHrBO
{
    public class RunnerDetailsBO
    {
        public int UpdateRunnerDetails(RunnerDetailsData objData)
        {
            int result = 0;
            try
            {
                RunnerDetailsDA objDocTaxDA = new RunnerDetailsDA();
                result = objDocTaxDA.UpdateRunnerDetails(objData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<RunnerDetailsData> SearchRunnerTaxDetails(RunnerDetailsData objData)
        {
            List<RunnerDetailsData> result = null;
            try
            {
                RunnerDetailsDA objDA = new RunnerDetailsDA();
                result = objDA.SearchRunnerTaxDetails(objData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<RunnerDetailsData> GetRunnerDetailsByID(RunnerDetailsData objData)
        {
            List<RunnerDetailsData> result = null;

            try
            {
                RunnerDetailsDA objDA = new RunnerDetailsDA();
                result = objDA.GetRunnerDetailsByID(objData);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeleteRunnerDetailsByID(RunnerDetailsData objData)
        {
            int result = 0;
            try
            {
                RunnerDetailsDA objDA = new RunnerDetailsDA();
                result = objDA.DeleteRunnerDetailsByID(objData);
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

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
    public class NursingCareBO
    {
        public List<NursingCareReportData> GetNursingCareList(NursingCareReportData objData)
        {
            List<NursingCareReportData> result = null;
            try
            {
                NursingCareDA objDA = new NursingCareDA();
                result = objDA.GetNursingCareList(objData);
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

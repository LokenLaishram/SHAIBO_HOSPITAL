using Mediqura.CommonData.PatientData;
using Mediqura.DAL.PatientDA;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.BOL.PatientBO
{
   public class PatientCaseHistoryBO
    {
        public int SavePatientDocuments(PatientCaseHistoryData objpat)
        {
            int result = 0;

            try
            {
                PatientDocumentDA objpatientDA = new PatientDocumentDA();
                result = objpatientDA.SavePatientDocuments(objpat);

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

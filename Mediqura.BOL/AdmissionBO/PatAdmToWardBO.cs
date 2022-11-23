using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mediqura.CommonData.AdmissionData;
using Mediqura.DAL.MedBillDA;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using Mediqura.DAL.AdmissionDA;
using Mediqura.CommonData.MedHouseKeepingData;
using Mediqura.DAL.MedHouseKeepingDA;

namespace Mediqura.BOL.AdmissionBO
{
    public class PatAdmToWardBO
    {
        public List<PatAdmToWardData> GetPatAdmToWardDetails(PatAdmToWardData objdata)
        {
            List<PatAdmToWardData> result = null;
            try
            {
                PatAdmToWardDA objDA = new PatAdmToWardDA();
                result = objDA.GetPatAdmToWardDetails(objdata);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public int UpdateAdmittedToWardStatus(PatAdmToWardData objData)
        {
            int result = 0;
            try
            {
                PatAdmToWardDA objDA = new PatAdmToWardDA();
                result = objDA.UpdateAdmittedToWardStatus(objData);
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

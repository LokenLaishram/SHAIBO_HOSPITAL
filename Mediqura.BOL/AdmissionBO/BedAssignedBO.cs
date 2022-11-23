using Mediqura.CommonData.MedUtilityData;
using Mediqura.CommonData.AdmissionData;
using Mediqura.DAL.AdmissionDA;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.BOL.AdmissionBO
{
    public class BedAssignedBO
    {
        public int UpdateBedAdmissionDetails(BedAssignData objadmission)
        {
            int result = 0;
            try
            {
                BedAssignedDA objDA = new BedAssignedDA();
                result = objDA.UpdateBedAdmissionDetails(objadmission);
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

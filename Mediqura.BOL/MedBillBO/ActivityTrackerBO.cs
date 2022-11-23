using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mediqura.CommonData.AdmissionData;
using Mediqura.CommonData.MedBillData;
using Mediqura.DAL.MedBillDA;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using Mediqura.DAL.AdmissionDA;

namespace Mediqura.BOL.MedBillBO
{
    public class ActivityTrackerBO
    {

        public List<ActivityTrackerData> GetPatientActivity(ActivityTrackerData objtracker)
        {
            List<ActivityTrackerData> result = null;
            try
            {
                ActivityTrackerDA objDA = new ActivityTrackerDA();
                result = objDA.GetPatientActivity(objtracker);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<ActivityTrackerData> GetPatientAdmissionDetailsByUHID(ActivityTrackerData objD)
        {
            List<ActivityTrackerData> result = null;
            try
            {
                ActivityTrackerDA objpatientDA = new ActivityTrackerDA();
                result = objpatientDA.GetPatientAdmissionDetailsByUHID(objD);

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

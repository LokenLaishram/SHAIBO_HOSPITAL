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
using Mediqura.CommonData.MedOPDData;
using Mediqura.CommonData.MedNurseData;
using Mediqura.DAL.MedNurseDA;

namespace Mediqura.BOL.MedNurseBO
{
    public class NurseRoasterBO
    {
        public List<NurseRoasterData> GetSchedule(NurseRoasterData objScheduleData)
        {
            List<NurseRoasterData> result = null;
            try
            {
                NurseRoasterDA objLabSubGroupTypeMasteDA = new NurseRoasterDA();
                result = objLabSubGroupTypeMasteDA.GetSchedule(objScheduleData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int UpdateRosterDetails(NurseRoasterData objAppmt)
        {
            int result = 0;
            try
            {
                NurseRoasterDA objDA = new NurseRoasterDA();
                result = objDA.UpdateRosterDetails(objAppmt);
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

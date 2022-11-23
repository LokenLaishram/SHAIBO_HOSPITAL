using Mediqura.CommonData.MedUtilityData;
using Mediqura.DAL.MedUtilityDA;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Mediqura.BOL.MedUtilityBO
{
    public class AppoinmentScheduleBO
    {
        public List<AppoinmentScheduleData> GetSchedule(AppoinmentScheduleData objScheduleData)
        {
            List<AppoinmentScheduleData> result = null;
            try
            {
                AppoinmentScheduleDA objLabSubGroupTypeMasteDA = new AppoinmentScheduleDA();
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
        public List<AppoinmentScheduleData> GetYear(AppoinmentScheduleData objD)
        {
            List<AppoinmentScheduleData> result = null;
            try
            {
                AppoinmentScheduleDA objpatientDA = new AppoinmentScheduleDA();
                result = objpatientDA.GetYear(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public int UpdateAppoinmentDetails(AppoinmentScheduleData objAppmt)
        {
            int result = 0;
            try
            {
                AppoinmentScheduleDA objDA = new AppoinmentScheduleDA();
                result = objDA.UpdateAppoinmentDetails(objAppmt);
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

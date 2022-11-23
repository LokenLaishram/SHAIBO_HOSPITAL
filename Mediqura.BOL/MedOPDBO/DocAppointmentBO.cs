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

namespace Mediqura.BOL.MedOPDBO
{
    public class DocAppointmentBO
    {

        public List<DocAppoinmentData> GetSchedule(DocAppoinmentData objScheduleData)
        {
            List<DocAppoinmentData> result = null;
            try
            {
                DocAppoinmentDA objLabSubGroupTypeMasteDA = new DocAppoinmentDA();
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
        public List<DocAppoinmentData> GetSchedulePageload(DocAppoinmentData objScheduleData)
        {
            List<DocAppoinmentData> result = null;
            try
            {
                DocAppoinmentDA objLabSubGroupTypeMasteDA = new DocAppoinmentDA();
                result = objLabSubGroupTypeMasteDA.GetSchedulePageload(objScheduleData);
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

using Mediqura.CommonData.MedUtilityData;
using Mediqura.CommonData.MedLab;
using Mediqura.DAL.MedUtilityDA;
using Mediqura.DAL.MedLab;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mediqura.CommonData.MedHrData;
using Mediqura.CommonData.PatientData;
using Mediqura.DAL.PatientDA;
using Mediqura.CommonData;

namespace Mediqura.BOL.MedLabBO
{
    public class LabOutsourceManagerBO
    {
        public List<LabOutsourceManagerData> GetLabList(LabOutsourceManagerData objSampleCollectionData)
        {
            List<LabOutsourceManagerData> result = null;
            try
            {
                OutsourceManagerDA objSampleCollectionMasteDA = new OutsourceManagerDA();
                result = objSampleCollectionMasteDA.GetLabList(objSampleCollectionData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<LabOutsourceManagerData> GetOutsourcePageload(LabOutsourceManagerData objScheduleData)
        {
            List<LabOutsourceManagerData> result = null;
            try
            {
                OutsourceManagerDA objLabSubGroupTypeMasteDA = new OutsourceManagerDA();
                result = objLabSubGroupTypeMasteDA.GetOutsourcePageload(objScheduleData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int UpdateLabOutsource(LabOutsourceManagerData objoutsource)
        {
            int result = 0;
            try
            {
                OutsourceManagerDA objDA = new OutsourceManagerDA();
                result = objDA.UpdateLabOutsource(objoutsource);
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

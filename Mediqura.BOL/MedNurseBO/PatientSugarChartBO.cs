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
    public class PatientSugarChartBO
    {
        public int UpdatePatientSugar(PatientSugarChartData objOTRoleMasterData)
        {
            int result = 0;
            try
            {
                PatientSugarChartDA objOTRoleMasterDA = new PatientSugarChartDA();
                result = objOTRoleMasterDA.UpdatePatientSugar(objOTRoleMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<PatientSugarChartData> GetSugarDetailByIPNo(PatientSugarChartData objOTRoleMasterData)
        {
            List<PatientSugarChartData> result = null;
            try
            {
                PatientSugarChartDA objOTRoleMasterDA = new PatientSugarChartDA();
                result = objOTRoleMasterDA.GetSugarDetailByIPNo(objOTRoleMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        //public List<PatientSugarChartData> GetOTRoleDetailsByID(PatientSugarChartData objOTRoleMasterData)
        //{
        //    List<PatientSugarChartData> result = null;

        //    try
        //    {
        //        PatientSugarChartDA objOTRoleMasterDA = new PatientSugarChartDA();
        //        result = objOTRoleMasterDA.GetOTRoleDetailsByID(objOTRoleMasterData);

        //    }
        //    catch (Exception ex)
        //    {
        //        PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
        //        LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
        //        throw new BusinessProcessException("4000001", ex);
        //    }
        //    return result;
        //}
        public int CancelSugarDetails(PatientSugarChartData objpat)
        {
            int result = 0;

            try
            {
                PatientSugarChartDA objpatientDA = new PatientSugarChartDA();
                result = objpatientDA.CancelSugarDetails(objpat);

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

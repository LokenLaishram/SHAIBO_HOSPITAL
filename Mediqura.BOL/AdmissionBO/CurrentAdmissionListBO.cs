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


namespace Mediqura.BOL.AdmissionBO
{
    public class CurrentAdmissionListBO
    {
        public List<CurrentAdmissionListData> GetAdmissionDetailList(CurrentAdmissionListData objbill)
        {
            List<CurrentAdmissionListData> result = null;
            try
            {
                CurrentAdmissionListDA objDA = new CurrentAdmissionListDA();
                result = objDA.GetAdmissionDetailList(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<CurrentAdmissionListData> GetPHRAdmissionDetailList(CurrentAdmissionListData objbill)
        {
            List<CurrentAdmissionListData> result = null;
            try
            {
                CurrentAdmissionListDA objDA = new CurrentAdmissionListDA();
                result = objDA.GetPHRAdmissionDetailList(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public int UpdateCreditAlowed(CurrentAdmissionListData objadmission)
        {
            int result = 0;
            try
            {
                CurrentAdmissionListDA objDA = new CurrentAdmissionListDA();
                result = objDA.UpdateCreditAlowed(objadmission);
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

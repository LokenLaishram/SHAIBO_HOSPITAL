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
    public class IPCreditMasterBO
    {
        public List<IPCreditMasterData> GetPatientDepositDetails(IPCreditMasterData objbill)
        {
            List<IPCreditMasterData> result = null;
            try
            {
                IPCreditMasterDA objDA = new IPCreditMasterDA();
                result = objDA.GetPatientDepositDetails(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public int UpdateIPCreditLimitDetails(IPCreditMasterData objadmission)
        {
            int result = 0;
            try
            {
                IPCreditMasterDA objDA = new IPCreditMasterDA();
                result = objDA.UpdateIPCreditLimitDetails(objadmission);
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

using Mediqura.CommonData.MedAccount;
using Mediqura.DAL.MedAccount;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.BOL.MedAccount
{
    public class InsuranceReceivableBO
    {
        public List<InsuranceReceivableData> GetBillNo(InsuranceReceivableData objData)
        {
            List<InsuranceReceivableData> result = null;
            try
            {
                InsuranceReceivableDA objDA = new InsuranceReceivableDA();
                result = objDA.GetBillNo(objData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<InsuranceReceivableData> GetExtraDiscountedList(InsuranceReceivableData objData)
        {
            List<InsuranceReceivableData> result = null;
            try
            {
                InsuranceReceivableDA objDA = new InsuranceReceivableDA();
                result = objDA.GetExtraDiscountedList(objData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<InsuranceReceivableData> GetExtraDiscountedLists(InsuranceReceivableData objData)
        {
            List<InsuranceReceivableData> result = null;
            try
            {
                InsuranceReceivableDA objDA = new InsuranceReceivableDA();
                result = objDA.GetExtraDiscountedLists(objData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int UpdateInsuranceReceivableDetails(InsuranceReceivableData objbill)
        {
            int result = 0;
            try
            {
                InsuranceReceivableDA objDA = new InsuranceReceivableDA();
                result = objDA.UpdateInsuranceReceivableDetails(objbill);
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

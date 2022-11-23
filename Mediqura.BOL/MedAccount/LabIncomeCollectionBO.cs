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
    public class LabIncomeCollectionBO
    {
        public List<LabIncomeCollectionData> GetLabIncomeList(LabIncomeCollectionData objbill)
        {
            List<LabIncomeCollectionData> result = null;
            try
            {
                LabIncomeCollectionDA objDA = new LabIncomeCollectionDA();
                result = objDA.GetLabIncomeList(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }

        public List<LabIncomeCollectionData> GetLabExpenditureList(LabIncomeCollectionData objbill)
        {
            List<LabIncomeCollectionData> result = null;
            try
            {
                LabIncomeCollectionDA objDA = new LabIncomeCollectionDA();
                result = objDA.GetLabExpenditureList(objbill);
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

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
    public class LedgerBookBO
    {
        public List<LedgerBookData> GetLedgerList(LedgerBookData objData)
        {
            List<LedgerBookData> result = null;
            try
            {
                LedgerBookDA objDA = new LedgerBookDA();
                result = objDA.GetLedgerList(objData);
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

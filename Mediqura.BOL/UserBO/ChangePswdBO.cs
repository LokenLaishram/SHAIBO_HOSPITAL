using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mediqura.CommonData.LoginData;
using Mediqura.DAL.UserDA;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;


namespace Mediqura.BOL.UserBO
{
    public class ChangePswdBO
    {
        public int getLogDatareset(ChangePswdData objchdPwdData)
        {
            int result = 0;
            try
            {
                ChangePswdDA objPaymentTypeMasteDA = new ChangePswdDA();
                result = objPaymentTypeMasteDA.getLogDatareset(objchdPwdData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<ChangePswdData> GetPswd(ChangePswdData objD)
        {
            List<ChangePswdData> result = null;
            try
            {
                ChangePswdDA objpatientDA = new ChangePswdDA();
                result = objpatientDA.GetPswd(objD);

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

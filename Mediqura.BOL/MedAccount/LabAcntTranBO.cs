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
    public class LabAcntTranBO
    {
        public List<LabAcntTranData> UpdateLabAccountTransaction(LabAcntTranData ObjData)
        {
            List<LabAcntTranData> result = null;
            try
            {
                LabAcntTranDA objDA = new LabAcntTranDA();
                result = objDA.UpdateLabAccountTransaction(ObjData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<LabAcntTranData> GetIncomeTransactionList(LabAcntTranData ObjData)
        {
            List<LabAcntTranData> result = null;
            try
            {
                LabAcntTranDA objDA = new LabAcntTranDA();
                result = objDA.GetIncomeTransactionList(ObjData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }

        public List<LabAcntTranData> GetExpensesTransactionList(LabAcntTranData ObjData)
        {
            List<LabAcntTranData> result = null;
            try
            {
                LabAcntTranDA objDA = new LabAcntTranDA();
                result = objDA.GetExpensesTransactionList(ObjData);
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

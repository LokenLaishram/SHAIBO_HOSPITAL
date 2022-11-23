using Mediqura.CommonData.MedBillData;
using Mediqura.DAL.MedBillDA;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.BOL.MedBill
{
    public class DepositBO
    {
        public List<DepositData> UpdateDepositDetails(DepositData objbill)
        {
            List<DepositData> result = null;
            try
            {
                DepositDA objDA = new DepositDA();
                result = objDA.UpdateDepositDetails(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<DepositData> UpdatePhrDepositDetails(DepositData objbill)
        {
            List<DepositData> result=null;
            try
            {
                DepositDA objDA = new DepositDA();
                result = objDA.UpdatePhrDepositDetails(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }

        public List<DepositData> GetDepositList(DepositData objbill)
        {
            List<DepositData> result = null;
            try
            {
                DepositDA objDA = new DepositDA();
                result = objDA.GetDepositList(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<DepositData> GetPHRDepositList(DepositData objbill)
        {
            List<DepositData> result = null;
            try
            {
                DepositDA objDA = new DepositDA();
                result = objDA.GetPHRDepositList(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }

        public int DeleteDepositByID(DepositData objstd)
        {
            int result = 0;

            try
            {
                DepositDA objDA = new DepositDA();
                result = objDA.DeleteDepositByID(objstd);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public int DeletePHRDepositByID(DepositData objstd)
        {
            int result = 0;

            try
            {
                DepositDA objDA = new DepositDA();
                result = objDA.DeletePHRDepositByID(objstd);

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

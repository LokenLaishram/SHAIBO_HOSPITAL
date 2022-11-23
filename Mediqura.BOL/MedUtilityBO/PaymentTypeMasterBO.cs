using Mediqura.CommonData.MedUtilityData;
using Mediqura.DAL.MedUtilityDA;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.BOL.MedUtilityBO
{
    public class PaymentTypeMasterBO
    {
        public int UpdatePaymentTypeDetails(PaymentTypeMasterData objPaymentTypeMasterData)
        {
            int result = 0;
            try
            {
                PaymentTypeMasterDA objPaymentTypeMasteDA = new PaymentTypeMasterDA();
                result = objPaymentTypeMasteDA.UpdatePaymentTypeDetails(objPaymentTypeMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<PaymentTypeMasterData> GetPaymentTypeDetailsByID(PaymentTypeMasterData objPaymentTypeMasterData)
        {
            List<PaymentTypeMasterData> result = null;

            try
            {
                PaymentTypeMasterDA objPaymentTypeMasteDA = new PaymentTypeMasterDA();
                result = objPaymentTypeMasteDA.GetPaymentTypeDetailsByID(objPaymentTypeMasterData);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeletePaymentTypeDetailsByID(PaymentTypeMasterData objPaymentTypeMasterData)
        {
            int result = 0;
            try
            {
                PaymentTypeMasterDA objPaymentTypeMasterDA = new PaymentTypeMasterDA();
                result = objPaymentTypeMasterDA.DeletePaymentTypeDetailsByID(objPaymentTypeMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<PaymentTypeMasterData> SearchPaymentTypeDetails(PaymentTypeMasterData objPaymentTypeMasterData)
        {
            List<PaymentTypeMasterData> result = null;
            try
            {
                PaymentTypeMasterDA objPaymentTypeMasteDA = new PaymentTypeMasterDA();
                result = objPaymentTypeMasteDA.SearchPaymentTypeDetails(objPaymentTypeMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<PaymentTypeMasterData> GetPayment(PaymentTypeMasterData objservice)
        {
            List<PaymentTypeMasterData> result = null;

            try
            {
                PaymentTypeMasterDA objServiceDA = new PaymentTypeMasterDA();
                result = objServiceDA.GetPayment(objservice);

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

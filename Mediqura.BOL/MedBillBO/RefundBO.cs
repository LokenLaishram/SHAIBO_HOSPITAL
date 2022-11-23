using Mediqura.CommonData.MedBillData;
using Mediqura.DAL.MedBillDA;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.BOL.MedBillBO
{
    public class RefundBO
    {
     
        public List<RefundData> UpdateRefundDetails(RefundData objbill)
        {
            List<RefundData> result = null;
            try
            {
                RefundDA objDA = new RefundDA();
                result = objDA.UpdateRefundDetails(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<RefundData> UpdatePhrRefundDetails(RefundData objbill)
        {
            List<RefundData> result = null;
            try
            {
                RefundDA objDA = new RefundDA();
                result = objDA.UpdatePhrRefundDetails(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<RefundData> GetPhrRefundList(RefundData objbill)
        {
            List<RefundData> result = null;
            try
            {
                RefundDA objDA = new RefundDA();
                result = objDA.GetPhrRefundList(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
     
        public List<RefundData> GetRefundList(RefundData objbill)
        {
            List<RefundData> result = null;
            try
            {
                RefundDA objDA = new RefundDA();
                result = objDA.GetRefundList(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public int DeleteRefundByID(RefundData objstd)
        {
            int result = 0;

            try
            {
                RefundDA objDA = new RefundDA();
                result = objDA.DeleteRefundByID(objstd);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }

        public int DeletePHRRefundByID(RefundData objstd)
        {
            int result = 0;

            try
            {
                RefundDA objDA = new RefundDA();
                result = objDA.DeletePHRRefundByID(objstd);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<DiscountRefundData> GetDiscountRefundList(DiscountRefundData objData)
        {
            List<DiscountRefundData> result = null;
            try
            {
                RefundDA objDA = new RefundDA();
                result = objDA.GetDiscountRefundList(objData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<DiscountRefundData> UpdateDiscountRefundDetails(DiscountRefundData objData)
        {
            List<DiscountRefundData> result;
            try
            {
                RefundDA objDA = new RefundDA();
                result = objDA.UpdateDiscountRefundDetails(objData);
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

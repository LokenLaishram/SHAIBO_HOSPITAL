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
    public class IPDrugIndentBO
    {
        public List<IPDrugIndentData> GetItemDetailsByStockID(IPDrugIndentData objD)
        {
            List<IPDrugIndentData> result = null;
            try
            {
                IPDrugIndentDA objpatientDA = new IPDrugIndentDA();
                result = objpatientDA.GetItemDetailsByStockID(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public int UpdateIndentItemDetails(IPDrugIndentData objstock)
        {
            int result = 0;
            try
            {
                IPDrugIndentDA objDA = new IPDrugIndentDA();
                result = objDA.UpdateIndentItemDetails(objstock);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<IPDrugIndentData> GetIndentItemList(IPDrugIndentData objbill)
        {
            List<IPDrugIndentData> result = null;
            try
            {
                IPDrugIndentDA objDA = new IPDrugIndentDA();
                result = objDA.GetIndentItemList(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public int DeleteIndentReqByID(IPDrugIndentData objstd)
        {
            int result = 0;

            try
            {
                IPDrugIndentDA objDA = new IPDrugIndentDA();
                result = objDA.DeleteIndentReqByID(objstd);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<IPDrugIndentData> GetIndentList(IPDrugIndentData objbill)
        {
            List<IPDrugIndentData> result = null;
            try
            {
                IPDrugIndentDA objDA = new IPDrugIndentDA();
                result = objDA.GetIndentList(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<IPDrugIndentData> GetIndentList1(IPDrugIndentData objbill)
        {
            List<IPDrugIndentData> result = null;
            try
            {
                IPDrugIndentDA objDA = new IPDrugIndentDA();
                result = objDA.GetIndentList1(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public int UpdateIndentDetailPHR(IPDrugIndentData objstock)
        {
            int result = 0;
            try
            {
                IPDrugIndentDA objDA = new IPDrugIndentDA();
                result = objDA.UpdateIndentDetailPHR(objstock);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<IPDrugIndentData> GetHandOverList(IPDrugIndentData objbill)
        {
            List<IPDrugIndentData> result = null;
            try
            {
                IPDrugIndentDA objDA = new IPDrugIndentDA();
                result = objDA.GetHandOverList(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<IPDrugIndentData> bindIndentRecvList(IPDrugIndentData objbill)
        {
            List<IPDrugIndentData> result = null;
            try
            {
                IPDrugIndentDA objDA = new IPDrugIndentDA();
                result = objDA.bindIndentRecvList(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<IPDrugIndentData> GetHndoverDetail(IPDrugIndentData objbill)
        {
            List<IPDrugIndentData> result = null;
            try
            {
                IPDrugIndentDA objDA = new IPDrugIndentDA();
                result = objDA.GetHndoverDetail(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public int UpdateReceivedDetail(IPDrugIndentData objstock)
        {
            int result = 0;
            try
            {
                IPDrugIndentDA objDA = new IPDrugIndentDA();
                result = objDA.UpdateReceivedDetail(objstock);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<IPDrugIndentData> GetRecvList(IPDrugIndentData objbill)
        {
            List<IPDrugIndentData> result = null;
            try
            {
                IPDrugIndentDA objDA = new IPDrugIndentDA();
                result = objDA.GetRecvList(objbill);
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

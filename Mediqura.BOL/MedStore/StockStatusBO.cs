using Mediqura.CommonData.MedStore;
using Mediqura.DAL.MedStore;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.BOL.MedStore
{
    public class StockStatusBO
    {
        public List<StockStatusData> GetBatchNo(StockStatusData objD)
        {
            List<StockStatusData> result = null;
            try
            {
                StockStatusDA objpatientDA = new StockStatusDA();
                result = objpatientDA.GetBatchNo(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<StockStatusData> Get_StockStatus(StockStatusData objDischargeIntimationData)
        {
            List<StockStatusData> result = null;
            try
            {
                StockStatusDA objLabSubGroupTypeMasteDA = new StockStatusDA();
                result = objLabSubGroupTypeMasteDA.Get_StockStatus(objDischargeIntimationData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int UpdateStockIssueDetails(StockStatusData objdischarge)
        {
            int result = 0;
            try
            {
                StockStatusDA objDA = new StockStatusDA();
                result = objDA.UpdateStockIssueDetails(objdischarge);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int UpdateSubstockStockDetails(StockStatusData objdischarge)
        {
            int result = 0;
            try
            {
                StockStatusDA objDA = new StockStatusDA();
                result = objDA.UpdateSubstockStockDetails(objdischarge);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<MedOPDSalesData> UpdateOPsales(MedOPDSalesData objdata)
        {
            List<MedOPDSalesData> result = null;
            try
            {
                StockStatusDA ObjDA = new StockStatusDA();
                result = ObjDA.UpdateOPsales(objdata);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<MedOPDSalesData> GetdiscountRequestList(MedOPDSalesData objdata)
        {
            List<MedOPDSalesData> result = null;
            try
            {
                StockStatusDA ObjDA = new StockStatusDA();
                result = ObjDA.GetdiscountRequestList(objdata);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<MedOPDSalesData> GetMedOpbillList(MedOPDSalesData objdata)
        {
            List<MedOPDSalesData> result = null;
            try
            {
                StockStatusDA ObjDA = new StockStatusDA();
                result = ObjDA.GetMedOpbillList(objdata);
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

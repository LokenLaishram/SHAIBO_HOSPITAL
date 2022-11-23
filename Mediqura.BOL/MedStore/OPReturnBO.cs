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
    public class OPReturnBO
    {
        public List<OPReturnData> GetAutoPhrOPBills(OPReturnData objD)
        {
            List<OPReturnData> result = null;
            try
            {
                OPReturnDA objpatientDA = new OPReturnDA();
                result = objpatientDA.GetAutoPhrOPBills(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<OPReturnData> GetOPIssueListByBillNo(OPReturnData objbill)
        {
            List<OPReturnData> result = null;
            try
            {
                OPReturnDA objDA = new OPReturnDA();
                result = objDA.GetOPIssueListByBillNo(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public string UpdateOPReturnDetails(OPReturnData objstock)
        {
            string result = null;
            try
            {
                OPReturnDA objDA = new OPReturnDA();
                result = objDA.UpdateOPReturnDetails(objstock);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<OPReturnData> GetOPReturnList(OPReturnData objbill)
        {
            List<OPReturnData> result = null;
            try
            {
                OPReturnDA objDA = new OPReturnDA();
                result = objDA.GetOPReturnList(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public int DeleteOPReturnItemListByID(OPReturnData objstd)
        {
            int result = 0;

            try
            {
                OPReturnDA objDA = new OPReturnDA();
                result = objDA.DeleteOPReturnItemListByID(objstd);

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

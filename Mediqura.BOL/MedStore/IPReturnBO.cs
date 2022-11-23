using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mediqura.CommonData.MedStore;
using Mediqura.DAL.MedStore;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
namespace Mediqura.BOL.MedStore
{
    public class IPReturnBO
    {
        public List<IPReturnData> GetadvanceSearchIPNo(IPReturnData ObjData)
        {
            List<IPReturnData> result = null;
            try
            {
                IPReturnDA ObjDA = new IPReturnDA();
                result = ObjDA.GetadvanceSearchIPNo(ObjData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }

		public List<IPReturnData> GetadvanceSearchAfterBillingIPNo(IPReturnData ObjData)
		{
			List<IPReturnData> result = null;
			try
			{
				IPReturnDA ObjDA = new IPReturnDA();
				result = ObjDA.GetadvanceSearchAfterBillingIPNo(ObjData);
			}
			catch (Exception ex)
			{
				PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
				LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
				throw new BusinessProcessException("4000001", ex);
			}
			return result;
		}

        public List<IPReturnData> GetIPReturnItem(IPReturnData ObjData)
        {
            List<IPReturnData> result = null;
            try
            {
                IPReturnDA ObjDA = new IPReturnDA();
                result = ObjDA.GetIPReturnItem(ObjData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }

        public List<IPReturnData> GetItemIssueByIPDrgIssueNo(IPReturnData ObjData)
        {
            List<IPReturnData> result = null;
            try
            {
                IPReturnDA ObjDA = new IPReturnDA();
                result = ObjDA.GetItemIssueByIPDrgIssueNo(ObjData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
		public List<IPReturnData> UpdateIPReturnAfterBillingDetails(IPReturnData objdata)
		{
			List<IPReturnData> result = null;
			try
			{
				IPReturnDA objDA = new IPReturnDA();
				result = objDA.UpdateIPReturnAfterBillingDetails(objdata);
			}
			catch (Exception ex)
			{
				PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
				LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
				throw new BusinessProcessException("4000001", ex);
			}
			return result;
		}
        public List<IPReturnData> UpdateIPReturnDetails(IPReturnData objdata)
        {
            List<IPReturnData> result = null;
            try
            {
                IPReturnDA objDA = new IPReturnDA();
                result = objDA.UpdateIPReturnDetails(objdata);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<IPReturnData> GetIPReturnNo(IPReturnData ObjData)
        {
            List<IPReturnData> result = null;
            try
            {
                IPReturnDA ObjDA = new IPReturnDA();
                result = ObjDA.GetIPReturnNo(ObjData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }

        public List<IPReturnData> GetiPReturnList1(IPReturnData objdata)
        {
            List<IPReturnData> result = null;
            try
            {
                IPReturnDA ObjDA = new IPReturnDA();
                result = ObjDA.GetiPReturnList1(objdata);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }

		public List<IPReturnData> GetiPReturnListAfterBill1(IPReturnData objdata)
		{
			List<IPReturnData> result = null;
			try
			{
				IPReturnDA ObjDA = new IPReturnDA();
				result = ObjDA.GetiPReturnListAfterBill1(objdata);
			}
			catch (Exception ex)
			{
				PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
				LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
				throw new BusinessProcessException("4000001", ex);
			}
			return result;
		}

        public int DeleteIPReturnItemByReturnNo(IPReturnData objdata)
        {
            int result = 0;
            try
            {
                IPReturnDA objDA = new IPReturnDA();
                result = objDA.DeleteIPReturnItemByReturnNo(objdata);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }

		public int CheckdischargestatusByIPNO(IPReturnData ObjData)
		{
			int result = 0;
			try
			{
				IPReturnDA objDA = new IPReturnDA();
				result = objDA.CheckdischargestatusByIPNO(ObjData);
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

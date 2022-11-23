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
	public class EmgReturnBO
	{
		public List<EmgReturnData> GetadvanceSearchEmergNo(EmgReturnData ObjData)
		{
			List<EmgReturnData> result = null;
			try
			{
				EmgReturnDA ObjDA = new EmgReturnDA();
				result = ObjDA.GetadvanceSearchEmergNo(ObjData);
			}
			catch (Exception ex)
			{
				PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
				LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
				throw new BusinessProcessException("4000001", ex);
			}
			return result;
		}

		public List<EmgReturnData> GetEmergReturnItem(EmgReturnData ObjData)
		{
			List<EmgReturnData> result = null;
			try
			{
				EmgReturnDA ObjDA = new EmgReturnDA();
				result = ObjDA.GetEmergReturnItem(ObjData);
			}
			catch (Exception ex)
			{
				PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
				LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
				throw new BusinessProcessException("4000001", ex);
			}
			return result;
		}

		public List<EmgReturnData> GetItemIssueByEmergDrgIssueNo(EmgReturnData ObjData)
		{
			List<EmgReturnData> result = null;
			try
			{
				EmgReturnDA ObjDA = new EmgReturnDA();
				result = ObjDA.GetItemIssueByEmergDrgIssueNo(ObjData);
			}
			catch (Exception ex)
			{
				PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
				LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
				throw new BusinessProcessException("4000001", ex);
			}
			return result;
		}

        public List<EmgReturnData> UpdateEmergReturnDetails(EmgReturnData objdata)
		{
            List<EmgReturnData> result = null;
			try
			{
				EmgReturnDA objDA = new EmgReturnDA();
				result = objDA.UpdateEmergReturnDetails(objdata);
			}
			catch (Exception ex)
			{
				PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
				LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
				throw new BusinessProcessException("4000001", ex);
			}
			return result;
		}

		public List<EmgReturnData> GetEmergReturnNo(EmgReturnData ObjData)
		{
			List<EmgReturnData> result = null;
			try
			{
				EmgReturnDA ObjDA = new EmgReturnDA();
				result = ObjDA.GetEmergReturnNo(ObjData);
			}
			catch (Exception ex)
			{
				PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
				LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
				throw new BusinessProcessException("4000001", ex);
			}
			return result;
		}

		public List<EmgReturnData> GetEmergReturnList1(EmgReturnData objdata)
		{
			List<EmgReturnData> result = null;
			try
			{
				EmgReturnDA ObjDA = new EmgReturnDA();
				result = ObjDA.GetEmergReturnList1(objdata);
			}
			catch (Exception ex)
			{
				PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
				LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
				throw new BusinessProcessException("4000001", ex);
			}
			return result;
		}

		public int DeleteEmergReturnItemByReturnNo(EmgReturnData objdata)
		{
			int result = 0;
			try
			{
				EmgReturnDA objDA = new EmgReturnDA();
				result = objDA.DeleteEmergReturnItemByReturnNo(objdata);
			}
			catch (Exception ex)
			{
				PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
				LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
				throw new BusinessProcessException("4000001", ex);
			}
			return result;
		}

        //-----------------------------------------------Start Tab 1 Emergency Return After Billing-------------------------------------------
        public List<EmgReturnData> GetadvanceSearchAfterBillingEmergNo(EmgReturnData ObjData)
        {
            List<EmgReturnData> result = null;
            try
            {
                EmgReturnDA ObjDA = new EmgReturnDA();
                result = ObjDA.GetadvanceSearchAfterBillingEmergNo(ObjData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }

        public List<EmgReturnData> UpdateEmergReturnAfterBillingDetails(EmgReturnData objdata)
        {
            List<EmgReturnData> result = null;
            try
            {
                EmgReturnDA objDA = new EmgReturnDA();
                result = objDA.UpdateEmergReturnAfterBillingDetails(objdata);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        //-----------------------------------------------End Tab 1 Emergency Return After Billing-------------------------------------------

        //-----------------------------------------------Start Tab 2 Emergency Return After Billing-------------------------------------------

        public List<EmgReturnData> GetEmergAfterBilligReturnNo(EmgReturnData ObjData)
        {
            List<EmgReturnData> result = null;
            try
            {
                EmgReturnDA ObjDA = new EmgReturnDA();
                result = ObjDA.GetEmergAfterBilligReturnNo(ObjData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<EmgReturnData> GetEmergReturnListAfterBill1(EmgReturnData objdata)
        {
            List<EmgReturnData> result = null;
            try
            {
                EmgReturnDA ObjDA = new EmgReturnDA();
                result = ObjDA.GetEmergReturnListAfterBill1(objdata);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeleteEmergReturnAfterBillingItemByReturnNo(EmgReturnData objdata)
        {
            int result = 0;
            try
            {
                EmgReturnDA objDA = new EmgReturnDA();
                result = objDA.DeleteEmergReturnAfterBillingItemByReturnNo(objdata);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        //-----------------------------------------------End Tab 2 Emergency Return After Billing-------------------------------------------


	}
}

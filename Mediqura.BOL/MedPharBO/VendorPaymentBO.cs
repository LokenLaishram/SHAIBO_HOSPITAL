using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mediqura.CommonData.MedPharData;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using Mediqura.DAL.MedPharDA;
using Mediqura.CommonData.MedBillData;

namespace Mediqura.BOL.MedPharBO
{
	public class VendorPaymentBO
	{

		public List<VendorPaymentData> GetVendorPurchaseDetails(VendorPaymentData objdata)
		{
			List<VendorPaymentData> result = null;
			try
			{
				VendorPaymentDA Objda = new VendorPaymentDA();
				result = Objda.GetVendorPurchaseDetails(objdata);
			}
			catch (Exception ex)
			{
				PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
				LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
				throw new BusinessProcessException("4000001", ex);
			}
			return result;
		}

		public List<VendorPaymentData> GetVendorPaymentDetails(VendorPaymentData objdata)
		{
			List<VendorPaymentData> result = null;
			try
			{
				VendorPaymentDA Objda = new VendorPaymentDA();
				result = Objda.GetVendorPaymentDetails(objdata);
			}
			catch (Exception ex)
			{
				PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
				LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
				throw new BusinessProcessException("4000001", ex);
			}
			return result;
		}

		public List<VendorPaymentData> GetVendorPaymentbySupplierID(VendorPaymentData objdata)
		{
			List<VendorPaymentData> result = null;
			try
			{
				VendorPaymentDA Objda = new VendorPaymentDA();
				result = Objda.GetVendorPaymentbySupplierID(objdata);
			}
			catch (Exception ex)
			{
				PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
				LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
				throw new BusinessProcessException("4000001", ex);
			}
			return result;
		}

		public string UpdateVendorPaymentDetails(VendorPaymentData objdata)
		{
			string result = "";
			try
			{
				VendorPaymentDA Objda = new VendorPaymentDA();
				result = Objda.UpdateVendorPaymentDetails(objdata);
			}
			catch (Exception ex)
			{
				PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
				LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
				throw new BusinessProcessException("4000001", ex);
			}
			return result;
		}

		public List<VendorPaymentData> GetVendorPaymentList(VendorPaymentData objdata)
		{
			List<VendorPaymentData> result = null;
			try
			{
				VendorPaymentDA Objda = new VendorPaymentDA();
				result = Objda.GetVendorPaymentList(objdata);
			}
			catch (Exception ex)
			{
				PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
				LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
				throw new BusinessProcessException("4000001", ex);
			}
			return result;
		}

		public List<VendorPaymentData> SearchChildvendorpaymentDetails(VendorPaymentData objdata)
		{
			List<VendorPaymentData> result = null;
			try
			{
				VendorPaymentDA Objda = new VendorPaymentDA();
				result = Objda.SearchChildvendorpaymentDetails(objdata);
			}
			catch (Exception ex)
			{
				PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
				LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
				throw new BusinessProcessException("4000001", ex);
			}
			return result;
		}

		public List<VendorPaymentData> SearchChildvendorpaymentByPaymentNo(VendorPaymentData objdata)
		{
			List<VendorPaymentData> result = null;
			try
			{
				VendorPaymentDA Objda = new VendorPaymentDA();
				result = Objda.SearchChildvendorpaymentByPaymentNo(objdata);
			}
			catch (Exception ex)
			{
				PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
				LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
				throw new BusinessProcessException("4000001", ex);
			}
			return result;
		}

		public List<BankDetail> Getbanklist(BankDetail objbankdetail)
		{
			List<BankDetail> result = null;
			try
			{
				VendorPaymentDA Objda = new VendorPaymentDA();
				result = Objda.Getbanklist(objbankdetail);
			}
			catch (Exception ex)
			{
				PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
				LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
				throw new BusinessProcessException("4000001", ex);
			}
			return result;
		}

		public List<VendorPaymentData> GetVendorpaymentno(VendorPaymentData objdata)
		{
			List<VendorPaymentData> result = null;
			try
			{
				VendorPaymentDA Objda = new VendorPaymentDA();
				result = Objda.GetVendorpaymentno(objdata);
			}
			catch (Exception ex)
			{
				PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
				LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
				throw new BusinessProcessException("4000001", ex);
			}
			return result;
		}

		public int DeleteVendorPaymentByPaymentNo(VendorPaymentData objdata)
		{
			int result = 0;
			try
			{
				VendorPaymentDA Objda = new VendorPaymentDA();
				result = Objda.DeleteVendorPaymentByPaymentNo(objdata);
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

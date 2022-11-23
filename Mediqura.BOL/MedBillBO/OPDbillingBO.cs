using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mediqura.CommonData.MedBillData;
using Mediqura.DAL.MedBillDA;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using Mediqura.CommonData.AdmissionData;
using Mediqura.CommonData.Common;

namespace Mediqura.BOL.MedBillBO
{
    public class OPDbillingBO
    {
        public List<OPDbillingData> GetServiceChargeByID(OPDbillingData objbill)
        {
            List<OPDbillingData> result = null;
            try
            {
                OPDbillingDA objDA = new OPDbillingDA();
                result = objDA.GetServiceChargeByID(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public int UpdatePaymentVerification(OPDbillingData objbill)
        {
            int result = 0;
            try
            {
                OPDbillingDA objDA = new OPDbillingDA();
                result = objDA.UpdatePaymentVerification(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<LabBillingData> GetLabServiceAmountByIDTestCenterID(LabBillingData objbill)
        {
            List<LabBillingData> result = null;
            try
            {
                OPDbillingDA objDA = new OPDbillingDA();
                result = objDA.GetLabServiceAmountByIDTestCenterID(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<OPDbillingData> UpdateOPDBill(OPDbillingData objbill)
        {
            List<OPDbillingData> result = null;
            try
            {
                OPDbillingDA objDA = new OPDbillingDA();
                result = objDA.UpdateOPDBill(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<BankDetail> Getbanklist(BankDetail objbill)
        {
            List<BankDetail> result = null;
            try
            {
                OPDbillingDA objDA = new OPDbillingDA();
                result = objDA.Getbanklist(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<OPDbillingData> GetOPPatientBillList(OPDbillingData objbill)
        {
            List<OPDbillingData> result = null;
            try
            {
                OPDbillingDA objDA = new OPDbillingDA();
                result = objDA.GetOPPatientBillList(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<OPDbillingData> GetOPBillList(OPDbillingData objbill)
        {
            List<OPDbillingData> result = null;
            try
            {
                OPDbillingDA objDA = new OPDbillingDA();
                result = objDA.GetOPBillList(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<OPDbillingData> GetOPPaymentList(OPDbillingData objbill)
        {
            List<OPDbillingData> result = null;
            try
            {
                OPDbillingDA objDA = new OPDbillingDA();
                result = objDA.GetOPPaymentList(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<OPDbillingData> GetOPBillListReport(OPDbillingData objbill)
        {
            List<OPDbillingData> result = null;
            try
            {
                OPDbillingDA objDA = new OPDbillingDA();
                result = objDA.GetOPBillListReport(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<DoctorPayoutData> GetDoctorsPayableservices(DoctorPayoutData objbill)
        {
            List<DoctorPayoutData> result = null;
            try
            {
                OPDbillingDA objDA = new OPDbillingDA();
                result = objDA.GetDoctorsPayableservices(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<DoctorPayoutData> GetDoctorsEarnings(DoctorPayoutData objbill)
        {
            List<DoctorPayoutData> result = null;
            try
            {
                OPDbillingDA objDA = new OPDbillingDA();
                result = objDA.GetDoctorsEarnings(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<DoctorPayoutData> GetPaymentlist(DoctorPayoutData objbill)
        {
            List<DoctorPayoutData> result = null;
            try
            {
                OPDbillingDA objDA = new OPDbillingDA();
                result = objDA.GetPaymentlist(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public int UpdateLabPaymentVerification(LabBillingData objbill)
        {
            int result = 0;
            try
            {
                OPDbillingDA objDA = new OPDbillingDA();
                result = objDA.UpdateLabPaymentVerification(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<DoctorPayoutData> GetdueDates(DoctorPayoutData objbill)
        {
            List<DoctorPayoutData> result = null;
            try
            {
                OPDbillingDA objDA = new OPDbillingDA();
                result = objDA.GetdueDates(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<DoctorPayoutData> PaidDoctorsServices(DoctorPayoutData objbill)
        {
            List<DoctorPayoutData> result = null;
            try
            {
                OPDbillingDA objDA = new OPDbillingDA();
                result = objDA.PaidDoctorsServices(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<OPDbillingData> GetautoOPbills(OPDbillingData objbill)
        {
            List<OPDbillingData> result = null;
            try
            {
                OPDbillingDA objDA = new OPDbillingDA();
                result = objDA.GetautoOPbills(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<OPDbillingData> GetPatientDetailbybillNo(OPDbillingData objbill)
        {
            List<OPDbillingData> result = null;
            try
            {
                OPDbillingDA objDA = new OPDbillingDA();
                result = objDA.GetPatientDetailbybillNo(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<LabBillingData> GetPatientDetailbyLabbillNo(LabBillingData objbill)
        {
            List<LabBillingData> result = null;
            try
            {
                OPDbillingDA objDA = new OPDbillingDA();
                result = objDA.GetPatientDetailbyLabbillNo(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<LabBillingData> GetLabServiceChargeByID(LabBillingData objbill)
        {
            List<LabBillingData> result = null;
            try
            {
                OPDbillingDA objDA = new OPDbillingDA();
                result = objDA.GetLabServiceChargeByID(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<LabBillingData> GetIPServiceChargeByID(LabBillingData objbill)
        {
            List<LabBillingData> result = null;
            try
            {
                OPDbillingDA objDA = new OPDbillingDA();
                result = objDA.GetIPServiceChargeByID(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<LabBillingData> GetOTServiceChargeByID(LabBillingData objbill)
        {
            List<LabBillingData> result = null;
            try
            {
                OPDbillingDA objDA = new OPDbillingDA();
                result = objDA.GetOTServiceChargeByID(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<LabBillingData> UpdateOPDLabBill(LabBillingData objbill)
        {
            List<LabBillingData> result = null;
            try
            {
                OPDbillingDA objDA = new OPDbillingDA();
                result = objDA.UpdateOPDLabBill(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<LabBillingData> GetLabBillList(LabBillingData objbill)
        {
            List<LabBillingData> result = null;
            try
            {
                OPDbillingDA objDA = new OPDbillingDA();
                result = objDA.GetLabBillList(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<LabBillingData> GetLabPaymentList(LabBillingData objbill)
        {
            List<LabBillingData> result = null;
            try
            {
                OPDbillingDA objDA = new OPDbillingDA();
                result = objDA.GetLabPaymentList(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<LabBillingData> GetLabBillListReport(LabBillingData objbill)
        {
            List<LabBillingData> result = null;
            try
            {
                OPDbillingDA objDA = new OPDbillingDA();
                result = objDA.GetLabBillListReport(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }

        public List<LabBillingData> GetPatientLabBillList(LabBillingData objbill)
        {
            List<LabBillingData> result = null;
            try
            {
                OPDbillingDA objDA = new OPDbillingDA();
                result = objDA.GetPatientLabBillList(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }

        public int DeleteOPDBillByID(OPDbillingData objstd)
        {
            int result = 0;

            try
            {
                OPDbillingDA objDA = new OPDbillingDA();
                result = objDA.DeleteOPDBillByID(objstd);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public int ChecDoctorAvailability(OPdoctoravailable objdoct)
        {
            int result = 0;

            try
            {
                OPDbillingDA objDA = new OPDbillingDA();
                result = objDA.ChecDoctorAvailability(objdoct);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }

        public int DeleteIPDBillByID(FinalBillData objstd)
        {
            int result = 0;

            try
            {
                OPDbillingDA objDA = new OPDbillingDA();
                result = objDA.DeleteIPDBillByID(objstd);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeleteOPDLabBillByID(LabBillingData objstd)
        {
            int result = 0;
            try
            {
                OPDbillingDA objDA = new OPDbillingDA();
                result = objDA.DeleteOPDLabBillByID(objstd);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<PHRbillingData> GetOPServiceChargeByID(PHRbillingData objbill)
        {
            List<PHRbillingData> result = null;
            try
            {
                OPDbillingDA objDA = new OPDbillingDA();
                result = objDA.GetOPServiceChargeByID(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<OPDbillingData> GetOpservicesbypatientvisitcount(OPDbillingData objbill)
        {
            List<OPDbillingData> result = null;
            try
            {
                OPDbillingDA objDA = new OPDbillingDA();
                result = objDA.GetOpservicesbypatientvisitcount(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<LabBillingData> GetPackageservices(LabBillingData objbill)
        {
            List<LabBillingData> result = null;
            try
            {
                OPDbillingDA objDA = new OPDbillingDA();
                result = objDA.GetPackageservices(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public int UpdateOPDPhrBill(PHRbillingData objbill)
        {
            int result = 0;
            try
            {
                OPDbillingDA objDA = new OPDbillingDA();
                result = objDA.UpdateOPDPhrBill(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<PHRbillingData> GetOPDPhrBillList(PHRbillingData objbill)
        {
            List<PHRbillingData> result = null;
            try
            {
                OPDbillingDA objDA = new OPDbillingDA();
                result = objDA.GetOPDPhrBillList(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public int DeleteOPDPhrBillByID(PHRbillingData objstd)
        {
            int result = 0;

            try
            {
                OPDbillingDA objDA = new OPDbillingDA();
                result = objDA.DeleteOPDPhrBillByID(objstd);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<LabBillingData> GetPhrServiceChargeById(LabBillingData objbill)
        {
            List<LabBillingData> result = null;
            try
            {
                OPDbillingDA objDA = new OPDbillingDA();
                result = objDA.GetPharmacyItemChargeByID(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<LabBillingData> GetBillNo(LabBillingData objbill)
        {
            List<LabBillingData> result = null;
            try
            {
                OPDbillingDA objDA = new OPDbillingDA();
                result = objDA.GetBillNo(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<LabBillingData> GetLabServiceList(LabBillingData objbill)
        {
            List<LabBillingData> result = null;
            try
            {
                OPDbillingDA objDA = new OPDbillingDA();
                result = objDA.GetLabServiceList(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<LabBillingData> UpdateLabServiceCancelRecord(LabBillingData objbill)
        {
            List<LabBillingData> result;
            try
            {
                OPDbillingDA objDA = new OPDbillingDA();
                result = objDA.UpdateLabServiceCancelRecord(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<LabBillingData> GetAdjustmentNo(LabBillingData objbill)
        {
            List<LabBillingData> result = null;
            try
            {
                OPDbillingDA objDA = new OPDbillingDA();
                result = objDA.GetAdjustmentNo(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<LabBillingData> GetAdjustmentList(LabBillingData objbill)
        {
            List<LabBillingData> result = null;
            try
            {
                OPDbillingDA objDA = new OPDbillingDA();
                result = objDA.GetAdjustmentList(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public int DeleteAdjustmentByID(LabBillingData objstd)
        {
            int result = 0;

            try
            {
                OPDbillingDA objDA = new OPDbillingDA();
                result = objDA.DeleteAdjustmentByID(objstd);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<LabBillingData> GetTestStatusByBillNO(LabBillingData objbill)
        {
            List<LabBillingData> result = null;
            try
            {
                OPDbillingDA objDA = new OPDbillingDA();
                result = objDA.GetTestStatusByBillNO(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<LabBillingData> GetDiscountServiceByID(Int64 ID)
        {
            List<LabBillingData> result = null;
            try
            {
                OPDbillingDA objDA = new OPDbillingDA();
                result = objDA.GetDiscountServiceByID(ID);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<OPDbillingData> GetDiscountOpServiceByID(Int64 ID)
        {
            List<OPDbillingData> result = null;
            try
            {
                OPDbillingDA objDA = new OPDbillingDA();
                result = objDA.GetDiscountOpServiceByID(ID);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<PatientQRdata> GetPatientQRData(Int64 UHID)
        {
            List<PatientQRdata> result = null;
            try
            {
                OPDbillingDA objDA = new OPDbillingDA();
                result = objDA.GetPatientQRData(UHID);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public int checkMsbDoctor(Int64 doctorID)
        {
            int result = 0;

            try
            {
                OPDbillingDA objDA = new OPDbillingDA();
                result = objDA.checkMsbDoctor(doctorID);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<LabBillingData> GetBillDetailsByBillNo(LabBillingData objbill)
        {
            List<LabBillingData> result = null;
            try
            {
                OPDbillingDA objDA = new OPDbillingDA();
                result = objDA.GetBillDetailsByBillNo(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<LabBillingData> UpdateReferalByBillNo(LabBillingData objbill)
        {
            List<LabBillingData> result = null;
            try
            {
                OPDbillingDA objDA = new OPDbillingDA();
                result = objDA.UpdateReferalByBillNo(objbill);
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

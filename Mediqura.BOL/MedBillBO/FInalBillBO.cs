using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mediqura.CommonData.AdmissionData;
using Mediqura.DAL.MedBillDA;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using Mediqura.DAL.AdmissionDA;
using Mediqura.CommonData.MedEmergencyData;

namespace Mediqura.BOL.MedBillBO
{
    public class FInalBillBO
    {
        public List<IPData> getIPNo(IPData objadmission)
        {
            List<IPData> result = null;
            try
            {
                FinalBillDA objempientDA = new FinalBillDA();
                result = objempientDA.getIPNo(objadmission);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<IPData> GetIntimatedIPNo(IPData objadmission)
        {
            List<IPData> result = null;
            try
            {
                FinalBillDA objempientDA = new FinalBillDA();
                result = objempientDA.GetIntimatedIPNo(objadmission);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<IPData> GetIntimatedIPNos(IPData objadmission)
        {
            List<IPData> result = null;
            try
            {
                FinalBillDA objempientDA = new FinalBillDA();
                result = objempientDA.GetIntimatedIPNos(objadmission);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<IPData> GetPHRIntimatedIPNos(IPData objadmission)
        {
            List<IPData> result = null;
            try
            {
                FinalBillDA objempientDA = new FinalBillDA();
                result = objempientDA.GetPHRIntimatedIPNos(objadmission);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<IPData> GetFinalIPNo(IPData objadmission)
        {
            List<IPData> result = null;
            try
            {
                FinalBillDA objempientDA = new FinalBillDA();
                result = objempientDA.GetFinalIPNo(objadmission);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<FinalBillData> GetPatientAdmissionDetailsByIPNo(FinalBillData objD)
        {
            List<FinalBillData> result = null;
            try
            {
                FinalBillDA objpatientDA = new FinalBillDA();
                result = objpatientDA.GetPatientAdmissionDetailsByIPNo(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<IPfinalbillData> GetIPfinalbillDetails(IPfinalbillData objD)
        {
            List<IPfinalbillData> result = null;
            try
            {
                FinalBillDA objpatientDA = new FinalBillDA();
                result = objpatientDA.GetIPfinalbillDetails(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<BillAdjustmentData> GetBilldetails(BillAdjustmentData objD)
        {
            List<BillAdjustmentData> result = null;
            try
            {
                FinalBillDA objpatientDA = new FinalBillDA();
                result = objpatientDA.GetBilldetails(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int UpdateServiceRecord(BillAdjustmentData objD)
        {
            int result = 0;
            try
            {
                FinalBillDA objpatientDA = new FinalBillDA();
                result = objpatientDA.UpdateServiceRecord(objD);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeleteServiceRecordbyID(BillAdjustmentData objstd)
        {
            int result = 0;

            try
            {
                FinalBillDA objpatientDA = new FinalBillDA();
                result = objpatientDA.DeleteServiceRecordbyID(objstd);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<IPDiscountListData> GetDiscountListForIP(IPfinalbillData objbill)
        {
            List<IPDiscountListData> result = null;
            try
            {
                FinalBillDA objDA = new FinalBillDA();
                result = objDA.GetDiscountListForIP(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<FinalBillData> Get_IP_Summarised_Bills(FinalBillData objD)
        {
            List<FinalBillData> result = null;
            try
            {
                FinalBillDA objpatientDA = new FinalBillDA();
                result = objpatientDA.Get_IP_Summarised_Bills(objD);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int Update_Final_BillDetails(FinalBillData objD)
        {
            int result = 0;
            try
            {
                FinalBillDA objpatientDA = new FinalBillDA();
                result = objpatientDA.Update_Final_BillDetails(objD);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<FinalBillData> Get_IP_BilldetailsbyID(FinalBillData objD)
        {
            List<FinalBillData> result = null;
            try
            {
                FinalBillDA objpatientDA = new FinalBillDA();
                result = objpatientDA.Get_IP_BilldetailsbyID(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<FinalBillData> Get_IPD_Collection_List(FinalBillData objD)
        {
            List<FinalBillData> result = null;
            try
            {
                FinalBillDA objpatientDA = new FinalBillDA();
                result = objpatientDA.Get_IPD_Collection_List(objD);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<InterimBillData> Get_IPD_Iterimbill_List(InterimBillData objD)
        {
            List<InterimBillData> result = null;
            try
            {
                FinalBillDA objpatientDA = new FinalBillDA();
                result = objpatientDA.Get_IPD_Iterimbill_List(objD);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public int Update_interimbilldetails(InterimBillData objD)
        {
            int result = 0;
            try
            {
                FinalBillDA objpatientDA = new FinalBillDA();
                result = objpatientDA.Update_interimbilldetails(objD);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<FinalBillData> GetIntimationList(FinalBillData objbill)
        {
            List<FinalBillData> result = null;
            try
            {
                FinalBillDA objDA = new FinalBillDA();
                result = objDA.GetIntimationList(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<FinalBillData> GetCurrentIntimationList(FinalBillData objbill)
        {
            List<FinalBillData> result = null;
            try
            {
                FinalBillDA objDA = new FinalBillDA();
                result = objDA.GetCurrentIntimationList(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<FinalBillData> GetNestedList(FinalBillData objbill)
        {
            List<FinalBillData> result = null;
            try
            {
                FinalBillDA objDA = new FinalBillDA();
                result = objDA.GetNestedList(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<FinalBillData> bindNestedService(FinalBillData objbill)
        {
            List<FinalBillData> result = null;
            try
            {
                FinalBillDA objDA = new FinalBillDA();
                result = objDA.bindNestedService(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public int DeleteIPservices(IPfinalbillData objbill)
        {
            int result = 0;
            try
            {
                FinalBillDA objDA = new FinalBillDA();
                result = objDA.DeleteIPservices(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeleteIPFinalbill(IPfinalbillData objbill)
        {
            int result = 0;
            try
            {
                FinalBillDA objDA = new FinalBillDA();
                result = objDA.DeleteIPFinalbill(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<IPfinalbillData> UpdatFinalBill(IPfinalbillData objD)
        {
            List<IPfinalbillData> result = null;
            try
            {
                FinalBillDA objpatientDA = new FinalBillDA();
                result = objpatientDA.UpdatFinalBill(objD);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<IPfinalbillData> GetIPDfinalbillList(IPfinalbillData objD)
        {
            List<IPfinalbillData> result = null;
            try
            {
                FinalBillDA objpatientDA = new FinalBillDA();
                result = objpatientDA.GetIPDfinalbillList(objD);
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

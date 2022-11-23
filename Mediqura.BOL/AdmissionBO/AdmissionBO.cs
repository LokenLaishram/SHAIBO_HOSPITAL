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

namespace Mediqura.BOL.AdmissionBO
{
    public class AdmissionBO
    {

        public List<AdmissionData> UpdateIPDAdmissionDetails(AdmissionData objadmission)
        {
            List<AdmissionData> result;
            try
            {
                AdmissionDA objDA = new AdmissionDA();
                result = objDA.UpdateIPDAdmissionDetails(objadmission);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int Deleleteoccupiedbed(AdmissionData objadmission)
        {
            int result = 0;
            try
            {
                AdmissionDA objDA = new AdmissionDA();
                result = objDA.Deleleteoccupiedbed(objadmission);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int UpdateBedpost(AdmissionData objadmission)
        {
            int result = 0;
            try
            {
                AdmissionDA objDA = new AdmissionDA();
                result = objDA.UpdateBedpost(objadmission);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int UpdateIPDBedMasterDetails(AdmissionData objadmission)
        {
            int result = 0;
            try
            {
                AdmissionDA objDA = new AdmissionDA();
                result = objDA.UpdateIPDBedMasterDetails(objadmission);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int Updatecreditlimit(AdmissionData objadmission)
        {
            int result = 0;
            try
            {
                AdmissionDA objDA = new AdmissionDA();
                result = objDA.Updatecreditlimit(objadmission);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int UpdateCurrentBedstatus(AdmissionData objadmission)
        {
            int result = 0;
            try
            {
                AdmissionDA objDA = new AdmissionDA();
                result = objDA.UpdateCurrentBedstatus(objadmission);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int UpdatepatientbedStatus(AdmissionData objadmission)
        {
            int result = 0;
            try
            {
                AdmissionDA objDA = new AdmissionDA();
                result = objDA.UpdatepatientbedStatus(objadmission);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int UpdateIPDBedTransferDetails(AdmissionData objadmission)
        {
            int result = 0;
            try
            {
                AdmissionDA objDA = new AdmissionDA();
                result = objDA.UpdateIPDBedTransferDetails(objadmission);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<IPData> getIPNo(IPData objadmission)
        {
            List<IPData> result = null;
            try
            {
                AdmissionDA objempientDA = new AdmissionDA();
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
        public List<IPData> GetIPPatientName(IPData objadmission)
        {
            List<IPData> result = null;
            try
            {
                AdmissionDA objempientDA = new AdmissionDA();
                result = objempientDA.GetIPPatientName(objadmission);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<IPData> getIPNoWithNameAgeNAddress(IPData objadmission)
        {
            List<IPData> result = null;
            try
            {
                AdmissionDA objempientDA = new AdmissionDA();
                result = objempientDA.getIPNoWithNameAgeNAddress(objadmission);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<IPData> getIPNoEmrgNoWithNameAgeNAddress(IPData objadmission)
        {
            List<IPData> result = null;
            try
            {
                AdmissionDA objempientDA = new AdmissionDA();
                result = objempientDA.getIPNoEmrgNoWithNameAgeNAddress(objadmission);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }

        public List<IPData> getIPNoByBedID(IPData objadmission)
        {
            List<IPData> result = null;
            try
            {
                AdmissionDA objempientDA = new AdmissionDA();
                result = objempientDA.getIPNoByBedID(objadmission);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<AdmissionData> GetAdmissionDetailList(AdmissionData objbill)
        {
            List<AdmissionData> result = null;
            try
            {
                AdmissionDA objDA = new AdmissionDA();
                result = objDA.GetAdmissionDetailList(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }

        public List<AdmissionData> GetAdmissionList(AdmissionData objbill)
        {
            List<AdmissionData> result = null;
            try
            {
                AdmissionDA objDA = new AdmissionDA();
                result = objDA.GetAdmissionList(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<AdmissionData> GetAdmissionListReport(AdmissionData objbill)
        {
            List<AdmissionData> result = null;
            try
            {
                AdmissionDA objDA = new AdmissionDA();
                result = objDA.GetAdmissionListReport(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<AdmissionData> GetBedTransferList(AdmissionData objbill)
        {
            List<AdmissionData> result = null;
            try
            {
                AdmissionDA objDA = new AdmissionDA();
                result = objDA.GetBedTransferList(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<IPData> GetIPNo(IPData objadmission)
        {
            List<IPData> result = null;
            try
            {
                AdmissionDA objempientDA = new AdmissionDA();
                result = objempientDA.GetIPNo(objadmission);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeleteIPDAdmissionByID(AdmissionData objstd)
        {
            int result = 0;

            try
            {
                AdmissionDA objDA = new AdmissionDA();
                result = objDA.DeleteIPDAdmissionByID(objstd);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<AdmissionData> GetBedList(AdmissionData objbill)
        {
            List<AdmissionData> result = null;
            try
            {
                AdmissionDA objDA = new AdmissionDA();
                result = objDA.GetBedList(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<AdmissionData> GetIPDBedList(AdmissionData objbill)
        {
            List<AdmissionData> result = null;
            try
            {
                AdmissionDA objDA = new AdmissionDA();
                result = objDA.GetIPDBedList(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<AdmissionData> GetIPDavailablebedList(AdmissionData objbill)
        {
            List<AdmissionData> result = null;
            try
            {
                AdmissionDA objDA = new AdmissionDA();
                result = objDA.GetIPDavailablebedList(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<AdmissionData> GetBedListByIPNo(AdmissionData objbill)
        {
            List<AdmissionData> result = null;
            try
            {
                AdmissionDA objDA = new AdmissionDA();
                result = objDA.GetBedListByIPNo(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<IPServiceRecordData> getIPNo(IPServiceRecordData objadmission)
        {
            List<IPServiceRecordData> result = null;
            try
            {
                IPServiceRecordDA objempientDA = new IPServiceRecordDA();
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
        public int UpdateIPIssueRecord(IPServiceRecordData objbill)
        {
            int result = 0;
            try
            {
                IPServiceRecordDA objDA = new IPServiceRecordDA();
                result = objDA.UpdateIPIssueRecord(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeleteIPDIssueRecordByIPNo(IPServiceRecordData objstd)
        {
            int result = 0;

            try
            {
                IPServiceRecordDA objDA = new IPServiceRecordDA();
                result = objDA.DeleteIPDIssueRecordByIPNo(objstd);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<AdmissionData> GetBedOccupancy(AdmissionData objbill)
        {
            List<AdmissionData> result = null;
            try
            {
                AdmissionDA objDA = new AdmissionDA();
                result = objDA.GetBedOccupancy(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<AdmissionData> UpdateIPDCode(AdmissionData objadmission)
        {
            List<AdmissionData> result;
            try
            {
                AdmissionDA objDA = new AdmissionDA();
                result = objDA.UpdateIPDCode(objadmission);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<IPData> getIPNoNemrgNo(IPData objadmission)
        {
            List<IPData> result = null;
            try
            {
                AdmissionDA objempientDA = new AdmissionDA();
                result = objempientDA.getIPNoNemrgNo(objadmission);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<AdmissionData> GetIPadmissionDetailsByIPNo(AdmissionData objadmission)
        {
            List<AdmissionData> result = null;

            try
            {
                AdmissionDA objDA = new AdmissionDA();
                result = objDA.GetIPadmissionDetailsByIPNo(objadmission);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int UpdateIPadmission(AdmissionData objadmission)
        {
            int result = 0;
            try
            {
                AdmissionDA objDA = new AdmissionDA();
                result = objDA.UpdateIPadmission(objadmission);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<IPData> GetIpnoEmrgNoList(IPData objadmission)
        {
            List<IPData> result = null;
            try
            {
                AdmissionDA objempientDA = new AdmissionDA();
                result = objempientDA.getIPNoNemrgNo(objadmission);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<AdmissionData> GetAdmissionListDepartmentWise(AdmissionData objbill)
        {
            List<AdmissionData> result = null;
            try
            {
                AdmissionDA objDA = new AdmissionDA();
                result = objDA.GetAdmissionListDepartmentWise(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<AdmissionData> GetAdmissionListDoctorWise(AdmissionData objbill)
        {
            List<AdmissionData> result = null;
            try
            {
                AdmissionDA objDA = new AdmissionDA();
                result = objDA.GetAdmissionListDoctorWise(objbill);
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

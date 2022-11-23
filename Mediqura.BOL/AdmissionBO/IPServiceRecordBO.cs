using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mediqura.CommonData.AdmissionData;
using Mediqura.CommonData.MedEmergencyData;
using Mediqura.DAL.MedBillDA;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using Mediqura.DAL.AdmissionDA;

namespace Mediqura.BOL.AdmissionBO
{
    public class IPServiceRecordBO
    {
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
        public List<IPServiceRecordData> getOTIPNo(IPServiceRecordData objadmission)
        {
            List<IPServiceRecordData> result = null;
            try
            {
                IPServiceRecordDA objempientDA = new IPServiceRecordDA();
                result = objempientDA.getOTIPNo(objadmission);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<IPServiceRecordData> GetOTpassnumber(IPServiceRecordData objadmission)
        {
            List<IPServiceRecordData> result = null;
            try
            {
                IPServiceRecordDA objempientDA = new IPServiceRecordDA();
                result = objempientDA.GetOTpassnumber(objadmission);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<IPServiceRecordData> Getipservicenumber(IPServiceRecordData objadmission)
        {
            List<IPServiceRecordData> result = null;
            try
            {
                IPServiceRecordDA objempientDA = new IPServiceRecordDA();
                result = objempientDA.Getipservicenumber(objadmission);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<IPServiceRecordData> GetIpinvnumber(IPServiceRecordData objadmission)
        {
            List<IPServiceRecordData> result = null;
            try
            {
                IPServiceRecordDA objempientDA = new IPServiceRecordDA();
                result = objempientDA.GetIpinvnumber(objadmission);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<IPServiceRecordData> getOTRegisteredIPNo(IPServiceRecordData objadmission)
        {
            List<IPServiceRecordData> result = null;
            try
            {
                IPServiceRecordDA objempientDA = new IPServiceRecordDA();
                result = objempientDA.getOTRegisteredIPNo(objadmission);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }

        public List<IPServiceRecordData> GetDepartmentByIPNo(IPServiceRecordData objadmission)
        {
            List<IPServiceRecordData> result = null;
            try
            {
                IPServiceRecordDA objempientDA = new IPServiceRecordDA();
                result = objempientDA.GetDepartmentByIPNo(objadmission);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<IPServiceRecordData> UpdateIPServiceRecord(IPServiceRecordData objbill)
        {
            List<IPServiceRecordData> result;
            try
            {
                IPServiceRecordDA objDA = new IPServiceRecordDA();
                result = objDA.UpdateIPServiceRecord(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<IPServiceRecordData> UpdateIPLabServiceRecord(IPServiceRecordData objbill)
        {
             List<IPServiceRecordData> result;
            try
            {
                IPServiceRecordDA objDA = new IPServiceRecordDA();
                result = objDA.UpdateIPLabServiceRecord(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<IPServiceRecordData> GetIPDServiceList(IPServiceRecordData objbill)
        {
            List<IPServiceRecordData> result = null;
            try
            {
                IPServiceRecordDA objDA = new IPServiceRecordDA();
                result = objDA.GetIPDServiceList(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<IPServiceRecordData> GetIPDIssueServiceList(IPServiceRecordData objbill)
        {
            List<IPServiceRecordData> result = null;
            try
            {
                IPServiceRecordDA objDA = new IPServiceRecordDA();
                result = objDA.GetIPDIssueServiceList(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }

        public List<IPServiceRecordData> GetIPDLabServiceList(IPServiceRecordData objbill)
        {
            List<IPServiceRecordData> result = null;
            try
            {
                IPServiceRecordDA objDA = new IPServiceRecordDA();
                result = objDA.GetIPDLabServiceList(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }

        public int DeleteIPDServiceRecordByIPNo(IPServiceRecordData objstd)
        {
            int result = 0;

            try
            {
                IPServiceRecordDA objDA = new IPServiceRecordDA();
                result = objDA.DeleteIPDServiceRecordByIPNo(objstd);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public int DeleteIPDLabServiceRecordByIPNo(IPServiceRecordData objstd)
        {
            int result = 0;

            try
            {
                IPServiceRecordDA objDA = new IPServiceRecordDA();
                result = objDA.DeleteIPDLabServiceRecordByIPNo(objstd);

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

        public int UpdateOTServiceRecord(IPServiceRecordData objbill)
        {
            int result = 0;
            try
            {
                IPServiceRecordDA objDA = new IPServiceRecordDA();
                result = objDA.UpdateOTServiceRecord(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<IPServiceRecordData> GetOPServiceList(IPServiceRecordData objbill)
        {
            List<IPServiceRecordData> result = null;
            try
            {
                IPServiceRecordDA objDA = new IPServiceRecordDA();
                result = objDA.GetOPServiceList(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeleteOTServiceRecordByIPNo(IPServiceRecordData objstd)
        {
            int result = 0;

            try
            {
                IPServiceRecordDA objDA = new IPServiceRecordDA();
                result = objDA.DeleteOTServiceRecordByIPNo(objstd);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<IPServiceRecordData> GetOTServiceList(IPServiceRecordData objbill)
        {
            List<IPServiceRecordData> result = null;
            try
            {
                IPServiceRecordDA objDA = new IPServiceRecordDA();
                result = objDA.GetOTServiceList(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<IPDBillSettingData> GetIPDServiceListSetting(IPDBillSettingData objbill)
        {
            List<IPDBillSettingData> result = null;
            try
            {
                IPServiceRecordDA objDA = new IPServiceRecordDA();
                result = objDA.GetIPDServiceListSetting(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<IPfinalbillData> GetIPDBillSetting(IPfinalbillData objbill)
        {
            List<IPfinalbillData> result = null;
            try
            {
                IPServiceRecordDA objDA = new IPServiceRecordDA();
                result = objDA.GetIPDBillSetting(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<IPDBillSettingData> UpdateIPDBillSettingRecord(IPDBillSettingData objbill)
        {
            List<IPDBillSettingData> result;
            try
            {
                IPServiceRecordDA objDA = new IPServiceRecordDA();
                result = objDA.UpdateIPDBillSettingRecord(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
     
        public int UpdateIPNoServiceCharge(IPDBillSettingData objData)
        {
            int result = 0;
            try
            {
                IPServiceRecordDA objDA = new IPServiceRecordDA();
                result = objDA.UpdateIPNoServiceCharge(objData);
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

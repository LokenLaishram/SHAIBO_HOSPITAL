using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mediqura.CommonData.AdmissionData;
using Mediqura.CommonData.PatientData;
using Mediqura.CommonData.OTData;
using Mediqura.DAL.MedBillDA;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using Mediqura.DAL.AdmissionDA;
using Mediqura.DAL.PatientDA;
using Mediqura.DAL.OTDA;

namespace Mediqura.BOL.OTBO
{
    public class OTRegnBO
    {
        public List<OTRegnData> GetCase(OTRegnData objD)
        {
            List<OTRegnData> result = null;
            try
            {
                OTRegnDA objpatientDA = new OTRegnDA();
                result = objpatientDA.GetCase(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<OTRegnData> GetNurseName(OTRegnData objD)
        {
            List<OTRegnData> result = null;
            try
            {
                OTRegnDA objpatientDA = new OTRegnDA();
                result = objpatientDA.GetNurseName(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<OTRegnData> UpdateOTRegnDetails(OTRegnData objdischarge)
        {
            List<OTRegnData> result = null;

            try
            {
                OTRegnDA objDA = new OTRegnDA();
                result = objDA.UpdateOTRegnDetails(objdischarge);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeleteOT_RegnByID(OTRegnData objstd)
        {
            int result = 0;

            try
            {
                OTRegnDA objDA = new OTRegnDA();
                result = objDA.DeleteOT_RegnByID(objstd);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public int UpdateOTshare(OTRegnData objstd)
        {
            int result = 0;

            try
            {
                OTRegnDA objDA = new OTRegnDA();
                result = objDA.UpdateOTshare(objstd);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<OTRegnData> GetOT_Registration(OTRegnData objpat)
        {

            List<OTRegnData> result = null;

            try
            {
                OTRegnDA objpatientDA = new OTRegnDA();
                result = objpatientDA.GetOT_Registration(objpat);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<OTRegnData> GetOT_RegistrationList(OTRegnData objpat)
        {

            List<OTRegnData> result = null;

            try
            {
                OTRegnDA objpatientDA = new OTRegnDA();
                result = objpatientDA.GetOT_RegistrationList(objpat);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<OTRegnData> GetOT_RegistrationListCustom(OTRegnData objpat)
        {

            List<OTRegnData> result = null;

            try
            {
                OTRegnDA objpatientDA = new OTRegnDA();
                result = objpatientDA.GetOT_RegistrationListCustom(objpat);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<OTRegnData> GetOT_RegistrationListReport(OTRegnData objpat)
        {

            List<OTRegnData> result = null;

            try
            {
                OTRegnDA objpatientDA = new OTRegnDA();
                result = objpatientDA.GetOT_RegistrationListReport(objpat);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<OTRegnData> GetOT_CompletedOtlist(OTRegnData objpat)
        {
            List<OTRegnData> result = null;
            try
            {
                OTRegnDA objpatientDA = new OTRegnDA();
                result = objpatientDA.GetOT_CompletedOtlist(objpat);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<OTRegnData> GetCompletedOtdetailByID(OTRegnData objpat)
        {
            List<OTRegnData> result = null;
            try
            {
                OTRegnDA objpatientDA = new OTRegnDA();
                result = objpatientDA.GetCompletedOtdetailByID(objpat);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<OTRegnData> Edit_RegistrationList(OTRegnData objpat)
        {

            List<OTRegnData> result = null;

            try
            {
                OTRegnDA objpatientDA = new OTRegnDA();
                result = objpatientDA.Edit_RegistrationList(objpat);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<OTRegnData> Edit_Otroledetails(OTRegnData objpat)
        {
            List<OTRegnData> result = null;
            try
            {
                OTRegnDA objpatientDA = new OTRegnDA();
                result = objpatientDA.Edit_Otroledetails(objpat);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<OTRegnData> Edit_Otanasthesiadetails(OTRegnData objpat)
        {
            List<OTRegnData> result = null;
            try
            {
                OTRegnDA objpatientDA = new OTRegnDA();
                result = objpatientDA.Edit_Otanasthesiadetails(objpat);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<OTRegnData> Edit_OtProceduredetails(OTRegnData objpat)
        {
            List<OTRegnData> result = null;
            try
            {
                OTRegnDA objpatientDA = new OTRegnDA();
                result = objpatientDA.Edit_OtProceduredetails(objpat);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeleteOTByID(OTRegnData objstd)
        {
            int result = 0;

            try
            {
                OTRegnDA objDA = new OTRegnDA();
                result = objDA.DeleteOTByID(objstd);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int UpdateOtstatus(OTRegnData objstd)
        {
            int result = 0;

            try
            {
                OTRegnDA objDA = new OTRegnDA();
                result = objDA.UpdateOtstatus(objstd);
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

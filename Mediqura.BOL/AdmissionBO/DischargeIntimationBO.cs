using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mediqura.CommonData.AdmissionData;
using Mediqura.CommonData.PatientData;
using Mediqura.DAL.MedBillDA;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using Mediqura.DAL.AdmissionDA;
using Mediqura.DAL.PatientDA;



namespace Mediqura.BOL.AdmissionBO
{
    public class DischargeIntimationBO
    {
        public int UpdateDischargeIntimationDetails(DischargeIntimationData objdischarge)
        {
            int result = 0;
            try
            {
                DischargeIntimationDA objDA = new DischargeIntimationDA();
                result = objDA.UpdateDischargeIntimationDetails(objdischarge);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<DischargeIntimationData> SearchDisch_intimationDetails(DischargeIntimationData objDischargeIntimationData)
        {
            List<DischargeIntimationData> result = null;
            try
            {
                DischargeIntimationDA objLabSubGroupTypeMasteDA = new DischargeIntimationDA();
                result = objLabSubGroupTypeMasteDA.SearchDisch_intimationDetails(objDischargeIntimationData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<DischargeIntimationData> GetDisch_IntimationList2(DischargeIntimationData objDischargeIntimationData)
        {
            List<DischargeIntimationData> result = null;
            try
            {
                DischargeIntimationDA objLabSubGroupTypeMasteDA = new DischargeIntimationDA();
                result = objLabSubGroupTypeMasteDA.GetDisch_IntimationList2(objDischargeIntimationData);
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
                DischargeIntimationDA objempientDA = new DischargeIntimationDA();
               // AdmissionDA objempientDA = new AdmissionDA();
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
        public List<DischargeIntimationData> GetPatientName(DischargeIntimationData objD)
        {
            List<DischargeIntimationData> result = null;
            try
            {
                DischargeIntimationDA objpatientDA = new DischargeIntimationDA();
                result = objpatientDA.GetPatientName(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }         
        public List<DischargeIntimationData> GetDocName(DischargeIntimationData objadmission)
        {
            List<DischargeIntimationData> result = null;
            try
            {
                DischargeIntimationDA objempientDA = new DischargeIntimationDA();
                // AdmissionDA objempientDA = new AdmissionDA();
                result = objempientDA.GetDocName(objadmission);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<DischargeIntimationData> GetIPNoList(DischargeIntimationData objadmission)
        {
            List<DischargeIntimationData> result = null;
            try
            {
                DischargeIntimationDA objempientDA = new DischargeIntimationDA();
                // AdmissionDA objempientDA = new AdmissionDA();
                result = objempientDA.GetIPNoList(objadmission);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeleteDischargeIntimationByID(DischargeIntimationData objstd)
        {
            int result = 0;

            try
            {
                DischargeIntimationDA objDA = new DischargeIntimationDA();
                result = objDA.DeleteDischargeIntimationByID(objstd);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<DischargeIntimationData> GetPatientAdmissionDetailsByIPNo(DischargeIntimationData objD)
        {
            List<DischargeIntimationData> result = null;
            try
            {
                DischargeIntimationDA objpatientDA = new DischargeIntimationDA();
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

        public List<PatientData> GetPatientName(PatientData objD)
        {
            List<PatientData> result = null;
            try
            {
                RegistrationDA objpatientDA = new RegistrationDA();
                result = objpatientDA.GetPatientName(objD);

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

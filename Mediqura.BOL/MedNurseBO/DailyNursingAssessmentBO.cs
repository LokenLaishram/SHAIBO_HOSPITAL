using Mediqura.CommonData.MedUtilityData;
using Mediqura.DAL.MedUtilityDA;
using Mediqura.DAL.MedOPDDA;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mediqura.CommonData.MedOPDData;
using Mediqura.CommonData.MedNurseData;
using Mediqura.DAL.MedNurseDA;

namespace Mediqura.BOL.MedNurseBO
{
    public class DailyNursingAssessmentBO
    {
        public List<NurseNotesData> GetNurseNotesList(NurseNotesData objD)
        {
            List<NurseNotesData> result = null;
            try
            {
                NurseNotesDA objpatientDA = new NurseNotesDA();
                result = objpatientDA.GetNurseNotesList(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<DailyNursingAssessmentData> GetPatientDetailByID(DailyNursingAssessmentData objD)
        {
            List<DailyNursingAssessmentData> result = null;
            try
            {
                DailyNursingAssessmentDA objpatientDA = new DailyNursingAssessmentDA();
                result = objpatientDA.GetPatientDetailByID(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<DailyNursingAssessmentData> GetNursingAssementByIPNo(DailyNursingAssessmentData objD)
        {
            List<DailyNursingAssessmentData> result = null;
            try
            {
                DailyNursingAssessmentDA objpatientDA = new DailyNursingAssessmentDA();
                result = objpatientDA.GetNursingAssementByIPNo(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public int UpdateDailyNursingAssessment(DailyNursingAssessmentData objpat)
        {
            int result = 0;

            try
            {
                DailyNursingAssessmentDA objpatientDA = new DailyNursingAssessmentDA();
                result = objpatientDA.UpdateDailyNursingAssessment(objpat);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public int UpdateNurseProgressSheet(NurseNotesData objpat)
        {
            int result = 0;

            try
            {
                NurseNotesDA objpatientDA = new NurseNotesDA();
                result = objpatientDA.UpdateNurseProgressSheet(objpat);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public int CancelNurseNotesDataLists(NurseNotesData objpat)
        {
            int result = 0;

            try
            {
                NurseNotesDA objpatientDA = new NurseNotesDA();
                result = objpatientDA.CancelNurseNotesDataLists(objpat);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public int DeleteNurseProgressSheet(NurseNotesData objpat)
        {
            int result = 0;

            try
            {
                NurseNotesDA objpatientDA = new NurseNotesDA();
                result = objpatientDA.DeleteNurseProgressSheet(objpat);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<NurseNotesData> GetNurseProgressSheetByID(NurseNotesData objpat)
        {

            List<NurseNotesData> result = null;

            try
            {
                NurseNotesDA objpatientDA = new NurseNotesDA();
                result = objpatientDA.GetNurseProgressSheetByID(objpat);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<DailyNursingAssessmentData> GetPtientDeatilbyID(DailyNursingAssessmentData objpat)
        {

            List<DailyNursingAssessmentData> result = null;

            try
            {
                DailyNursingAssessmentDA objpatientDA = new DailyNursingAssessmentDA();
                result = objpatientDA.GetPtientDeatilbyID(objpat);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<DailyNursingAssessmentData> GetPatientDetailsByIPNo(DailyNursingAssessmentData objD)
        {
            List<DailyNursingAssessmentData> result = null;
            try
            {
                DailyNursingAssessmentDA objpatientDA = new DailyNursingAssessmentDA();
                result = objpatientDA.GetPatientDetailsByIPNo(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<DailyNursingAssessmentData> SearchPatientList(DailyNursingAssessmentData objpat)
        {

            List<DailyNursingAssessmentData> result = null;

            try
            {
                DailyNursingAssessmentDA objpatientDA = new DailyNursingAssessmentDA();
                result = objpatientDA.SearchPatientList(objpat);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<DailyNursingAssessmentData> SearchPatientDetails(DailyNursingAssessmentData objpat)
        {

            List<DailyNursingAssessmentData> result = null;

            try
            {
                DailyNursingAssessmentDA objpatientDA = new DailyNursingAssessmentDA();
                result = objpatientDA.SearchPatientDetails(objpat);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeletePatientDetailsByID(DailyNursingAssessmentData objstd)
        {
            int result = 0;

            try
            {
                DailyNursingAssessmentDA objpatientDA = new DailyNursingAssessmentDA();
                result = objpatientDA.DeletePatientDetailsByID(objstd);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<DailyNursingAssessmentData> GetAdmittedPatientDetails(DailyNursingAssessmentData objD)
        {
            List<DailyNursingAssessmentData> result = null;
            try
            {
                DailyNursingAssessmentDA objpatientDA = new DailyNursingAssessmentDA();
                result = objpatientDA.GetAdmittedPatientDetails(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<DailyNursingAssessmentData> GetNurseProgressSheet(DailyNursingAssessmentData objD)
        {
            List<DailyNursingAssessmentData> result = null;
            try
            {
                DailyNursingAssessmentDA objpatientDA = new DailyNursingAssessmentDA();
                result = objpatientDA.GetNurseProgressSheet(objD);

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

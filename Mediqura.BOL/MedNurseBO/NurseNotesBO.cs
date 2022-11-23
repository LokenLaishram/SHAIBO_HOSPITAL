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
    public class NurseNotesBO
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
        public List<NurseNotesData> GetPatientDetailByID(NurseNotesData objD)
        {
            List<NurseNotesData> result = null;
            try
            {
                NurseNotesDA objpatientDA = new NurseNotesDA();
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
        public List<NurseNotesData> GetNurseProgressSheet(NurseNotesData objD)
        {
            List<NurseNotesData> result = null;
            try
            {
                NurseNotesDA objpatientDA = new NurseNotesDA();
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
        public int InsertNurseNotesdetails(NurseNotesData objpat)
        {
            int result = 0;

            try
            {
                NurseNotesDA objpatientDA = new NurseNotesDA();
                result = objpatientDA.InsertNurseNotesdetails(objpat);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public int InsertNurseProgressSheet(NurseNotesData objpat)
        {
            int result = 0;

            try
            {
                NurseNotesDA objpatientDA = new NurseNotesDA();
                result = objpatientDA.InsertNurseProgressSheet(objpat);

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
        public List<NurseNotesData> GetAdmittedPatientDetails(NurseNotesData objD)
        {
            List<NurseNotesData> result = null;
            try
            {
                NurseNotesDA objpatientDA = new NurseNotesDA();
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
    }
}

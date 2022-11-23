using Mediqura.CommonData.MedMedication;
using Mediqura.DAL.MedicationDA;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.BOL.MedicationBO
{
    public class NurseRecordSheetBO
    {
        public List<NureseRecordSheetData> GetIPPatientName(NureseRecordSheetData objD)
        {
            List<NureseRecordSheetData> result = null;
            try
            {
                NurseRecordSheetDA objpatientDA = new NurseRecordSheetDA();
                result = objpatientDA.GetIPPatientName(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<NureseRecordSheetData> GetPatientDetailsByIPNO(NureseRecordSheetData objD)
        {
            List<NureseRecordSheetData> result = null;
            try
            {
                NurseRecordSheetDA objpatientDA = new NurseRecordSheetDA();
                result = objpatientDA.GetPatientDetailsByIPNO(objD);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int UpdateNurseRecordSheet(NureseRecordSheetData objpat)
        {
            int result = 0;

            try
            {
                NurseRecordSheetDA objpatientDA = new NurseRecordSheetDA();
                result = objpatientDA.UpdateNurseRecordSheet(objpat);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<NureseRecordSheetData> GetNurseRecordList(NureseRecordSheetData objD)
        {
            List<NureseRecordSheetData> result = null;
            try
            {
                NurseRecordSheetDA objMediDA = new NurseRecordSheetDA();
                result = objMediDA.GetNurseRecordList(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<NureseRecordSheetData> GetNurseRecordEntryByID(NureseRecordSheetData objDrgEData)
        {
            List<NureseRecordSheetData> result = null;

            try
            {
                NurseRecordSheetDA objDrgEDA = new NurseRecordSheetDA();
                result = objDrgEDA.GetNurseRecordEntryByID(objDrgEData);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeleteNurseRecordEntryByID(NureseRecordSheetData objDrgData)
        {
            int result = 0;
            try
            {
                NurseRecordSheetDA objDrgDA = new NurseRecordSheetDA();
                result = objDrgDA.DeleteNurseRecordEntryByID(objDrgData);
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

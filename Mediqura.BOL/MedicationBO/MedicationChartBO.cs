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
    public class MedicationChartBO
    {
        public List<MedicationChartData> GetIPPatientName(MedicationChartData objD)
        {
            List<MedicationChartData> result = null;
            try
            {
                MedicationDA objpatientDA = new MedicationDA();
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
        public List<MedicationChartData> GetPatientDetailsByIPNO(MedicationChartData objD)
        {
            List<MedicationChartData> result = null;
            try
            {
                MedicationDA objpatientDA = new MedicationDA();
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
        public List<MedicationChartData> GetDrugName(MedicationChartData objD)
        {
            List<MedicationChartData> result = null;
            try
            {
                MedicationDA objdrugDA = new MedicationDA();
                result = objdrugDA.GetDrugName(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<MedicationChartData> GetDoctorName(MedicationChartData objD)
        {
            List<MedicationChartData> result = null;
            try
            {
                MedicationDA objdrugDA = new MedicationDA();
                result = objdrugDA.GetDoctorName(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int UpdateMedicationChart(MedicationChartData objpat)
        {
            int result = 0;

            try
            {
                MedicationDA objpatientDA = new MedicationDA();
                result = objpatientDA.UpdateMedicationChart(objpat);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<MedicationChartData> GetMedicationList(MedicationChartData objD)
        {
            List<MedicationChartData> result = null;
            try
            {
                MedicationDA objMediDA = new MedicationDA();
                result = objMediDA.GetMedicationList(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<MedicationChartData> GetIPPatientDrugEntryByID(MedicationChartData objDrgEData)
        {
            List<MedicationChartData> result = null;

            try
            {
                MedicationDA objDrgEDA = new MedicationDA();
                result = objDrgEDA.GetIPPatientDrugEntryByID(objDrgEData);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeleteIPPatientDrugEntryByID(MedicationChartData objDrgData)
        {
            int result = 0;
            try
            {
                MedicationDA objDrgDA = new MedicationDA();
                result = objDrgDA.DeleteIPPatientDrugEntryByID(objDrgData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        //-----TAB2------//
        public List<MedicationChartData> GetMedicationDetailsList(MedicationChartData objcondemn)
        {
            List<MedicationChartData> result = null;
            try
            {
                MedicationDA objDA = new MedicationDA();
                result = objDA.GetMedicationDetailsList(objcondemn);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        
        public int UpdateDrugMedication(MedicationChartData objdrg)
        {
            int result = 0;

            try
            {
                MedicationDA objpatientDA = new MedicationDA();
                result = objpatientDA.UpdateDrugMedication(objdrg);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;  
        }
        public List<MedicationChartData> GetDrugMedicationList(MedicationChartData objD)
        {
            List<MedicationChartData> result = null;
            try
            {
                MedicationDA objMediDA = new MedicationDA();
                result = objMediDA.GetDrugMedicationList(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<MedicationChartData> GetDrugMedicationEntryByID(MedicationChartData objDrgEData)
        {
            List<MedicationChartData> result = null;

            try
            {
                MedicationDA objDrgEDA = new MedicationDA();
                result = objDrgEDA.GetDrugMedicationEntryByID(objDrgEData);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeleteDrugMedicationEntryByID(MedicationChartData objDrgData)
        {
            int result = 0;
            try
            {
                MedicationDA objDrgDA = new MedicationDA();
                result = objDrgDA.DeleteDrugMedicationEntryByID(objDrgData);
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

using Mediqura.CommonData.MedUtilityData;
using Mediqura.DAL.MedUtilityDA;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mediqura.CommonData.MedHrData;
using Mediqura.CommonData.PatientData;
using Mediqura.DAL.PatientDA;
using Mediqura.CommonData;
using Mediqura.CommonData.AdmissionData;
using Mediqura.CommonData.MedLab;

namespace Mediqura.BOL.MedUtilityBO
{
    public class LabSampleCollctionBO
    {
        public int UpdateSampleCollectionDetails(SampleCollectionData objSampleCollectionData)
        {
            int result = 0;
            try
            {
                LabSampleCollectionDA objLabSampleCollectionDA = new LabSampleCollectionDA();
                result = objLabSampleCollectionDA.UpdateSampleCollectionDetails(objSampleCollectionData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<SampleCollectionData> InvDetailByInvNo(SampleCollectionData objbill)
        {
            List<SampleCollectionData> result = null;
            try
            {
                LabSampleCollectionDA objDA = new LabSampleCollectionDA();
                result = objDA.InvDetailByInvNo(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public int UpdateIPSampleCollectionDetails(SampleCollectionData objSampleCollectionData)
        {
            int result = 0;
            try
            {
                LabSampleCollectionDA objLabSampleCollectionDA = new LabSampleCollectionDA();
                result = objLabSampleCollectionDA.UpdateIPSampleCollectionDetails(objSampleCollectionData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<SampleCollectionData> GetLabInvestigationno(SampleCollectionData objD)
        {
            List<SampleCollectionData> result = null;
            try
            {
                LabSampleCollectionDA objpatientDA = new LabSampleCollectionDA();
                result = objpatientDA.GetLabInvestigationno(objD);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<SampleCollectionData> GetLabInvestigationForIP(SampleCollectionData objD)
        {
            List<SampleCollectionData> result = null;
            try
            {
                LabSampleCollectionDA objpatientDA = new LabSampleCollectionDA();
                result = objpatientDA.GetLabInvestigationForIP(objD);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<SampleCollectionData> GetDevicecompletedInvestigationno(SampleCollectionData objD)
        {
            List<SampleCollectionData> result = null;
            try
            {
                LabSampleCollectionDA objpatientDA = new LabSampleCollectionDA();
                result = objpatientDA.GetDevicecompletedInvestigationno(objD);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<SampleCollectionData> GetOPInvnumber(SampleCollectionData objD)
        {
            List<SampleCollectionData> result = null;
            try
            {
                LabSampleCollectionDA objpatientDA = new LabSampleCollectionDA();
                result = objpatientDA.GetOPInvnumber(objD);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<SampleCollectionData> GetLadeviceInitiationDetail(SampleCollectionData objSampleCollectionData)
        {
            List<SampleCollectionData> result = null;

            try
            {
                LabSampleCollectionDA objSampleCollectionMasteDA = new LabSampleCollectionDA();
                result = objSampleCollectionMasteDA.GetLadeviceInitiationDetail(objSampleCollectionData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<LabResultData> GetLabResults(LabResultData objSampleCollectionData)
        {
            List<LabResultData> result = null;

            try
            {
                LabSampleCollectionDA objSampleCollectionMasteDA = new LabSampleCollectionDA();
                result = objSampleCollectionMasteDA.GetLabResults(objSampleCollectionData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<LabResultData> GetCommentedLabResults(LabResultData objSampleCollectionData)
        {
            List<LabResultData> result = null;

            try
            {
                LabSampleCollectionDA objSampleCollectionMasteDA = new LabSampleCollectionDA();
                result = objSampleCollectionMasteDA.GetCommentedLabResults(objSampleCollectionData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        ///----- Modified Area for result Entry-------///
        public List<SampleCollectionData> GetTestPatientList(SampleCollectionData LabTestList)
        {
            List<SampleCollectionData> result = null;

            try
            {
                LabSampleCollectionDA objDA = new LabSampleCollectionDA();
                result = objDA.GetTestPatientList(LabTestList);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<SampleCollectionData> GetResultentrytestlist(SampleCollectionData objSampleCollectionData)
        {
            List<SampleCollectionData> result = null;

            try
            {
                LabSampleCollectionDA objSampleCollectionMasteDA = new LabSampleCollectionDA();
                result = objSampleCollectionMasteDA.GetResultentrytestlist(objSampleCollectionData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        /// Culture ///
        public List<LabResultData> GetMicroLabResults(LabResultData objSampleCollectionData)
        {
            List<LabResultData> result = null;

            try
            {
                LabSampleCollectionDA objSampleCollectionMasteDA = new LabSampleCollectionDA();
                result = objSampleCollectionMasteDA.GetMicroLabResults(objSampleCollectionData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int UpdateLabCultureResultEntryDetails(LabResultData objSampleCollectionData)
        {
            int result = 0;
            try
            {
                LabSampleCollectionDA objLabSampleCollectionDA = new LabSampleCollectionDA();
                result = objLabSampleCollectionDA.UpdateLabCultureResultEntryDetails(objSampleCollectionData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        // END OF CULTURE //
        //---- END OF Modified Area-------------------------
        public List<SampleCollectionData> GetCommentResultentrytestlist(SampleCollectionData objSampleCollectionData)
        {
            List<SampleCollectionData> result = null;

            try
            {
                LabSampleCollectionDA objSampleCollectionMasteDA = new LabSampleCollectionDA();
                result = objSampleCollectionMasteDA.GetCommentResultentrytestlist(objSampleCollectionData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int UpdateDeviceInitiation(SampleCollectionData objSampleCollectionData)
        {
            int result = 0;
            try
            {
                LabSampleCollectionDA objSampleCollectionMasteDA = new LabSampleCollectionDA();
                result = objSampleCollectionMasteDA.UpdateDeviceInitiation(objSampleCollectionData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int UpdateMLTrecievingtime(SampleCollectionData objSampleCollectionData)
        {
            int result = 0;
            try
            {
                LabSampleCollectionDA objSampleCollectionMasteDA = new LabSampleCollectionDA();
                result = objSampleCollectionMasteDA.UpdateMLTrecievingtime(objSampleCollectionData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<SampleCollectionData> GetLabPatientNames(SampleCollectionData objD)
        {
            List<SampleCollectionData> result = null;
            try
            {
                LabSampleCollectionDA objpatientDA = new LabSampleCollectionDA();
                result = objpatientDA.GetLabPatientNames(objD);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<SampleCollectionData> GetDeviceCompletedPatientName(SampleCollectionData objD)
        {
            List<SampleCollectionData> result = null;
            try
            {
                LabSampleCollectionDA objpatientDA = new LabSampleCollectionDA();
                result = objpatientDA.GetDeviceCompletedPatientName(objD);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<SampleCollectionData> GetDeviceCompletedTestNames(SampleCollectionData objD)
        {
            List<SampleCollectionData> result = null;
            try
            {
                LabSampleCollectionDA objpatientDA = new LabSampleCollectionDA();
                result = objpatientDA.GetDeviceCompletedTestNames(objD);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int UpdateDeviceInitiationDetails(SampleCollectionData objSampleCollectionData)
        {
            int result = 0;
            try
            {
                LabSampleCollectionDA objLabSampleCollectionDA = new LabSampleCollectionDA();
                result = objLabSampleCollectionDA.UpdateDeviceInitiationDetails(objSampleCollectionData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int UpdateLabResultEntryDetails(LabResultData objSampleCollectionData)
        {
            int result = 0;
            try
            {
                LabSampleCollectionDA objLabSampleCollectionDA = new LabSampleCollectionDA();
                result = objLabSampleCollectionDA.UpdateLabResultEntryDetails(objSampleCollectionData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int UpdateCommentedLabResult(LabResultData objSampleCollectionData)
        {
            int result = 0;
            try
            {
                LabSampleCollectionDA objLabSampleCollectionDA = new LabSampleCollectionDA();
                result = objLabSampleCollectionDA.UpdateCommentedLabResult(objSampleCollectionData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<SampleCollectionData> GetLabSampleCollectedDetails(SampleCollectionData objSampleCollectionData)
        {
            List<SampleCollectionData> result = null;

            try
            {
                LabSampleCollectionDA objSampleCollectionMasteDA = new LabSampleCollectionDA();
                result = objSampleCollectionMasteDA.GetLabSampleCollectedDetails(objSampleCollectionData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }

        public List<SampleCollectionData> GetLabResultCollectedDetails(SampleCollectionData objSampleCollectionData)
        {
            List<SampleCollectionData> result = null;

            try
            {
                LabSampleCollectionDA objSampleCollectionMasteDA = new LabSampleCollectionDA();
                result = objSampleCollectionMasteDA.GetLabREsultCollectedDetails(objSampleCollectionData);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }

        public List<SampleCollectionData> GetLabSampleCollectedDataDetails(SampleCollectionData objSampleCollectionData)
        {
            List<SampleCollectionData> result = null;

            try
            {
                LabSampleCollectionDA objSampleCollectionMasteDA = new LabSampleCollectionDA();
                result = objSampleCollectionMasteDA.GetLabSampleCollectedDataDetails(objSampleCollectionData);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<SampleCollectionData> SearchLabDeviceDetails(SampleCollectionData objSampleCollectionData)
        {
            List<SampleCollectionData> result = null;

            try
            {
                LabSampleCollectionDA objSampleCollectionMasteDA = new LabSampleCollectionDA();
                result = objSampleCollectionMasteDA.SearchLabDeviceDetails(objSampleCollectionData);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }

        public List<SampleCollectionData> GetLabDeviceInitiatedDataDetails(SampleCollectionData objSampleCollectionData)
        {
            List<SampleCollectionData> result = null;

            try
            {
                LabSampleCollectionDA objSampleCollectionMasteDA = new LabSampleCollectionDA();
                result = objSampleCollectionMasteDA.GetLabDeviceInitiatedDataDetails(objSampleCollectionData);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<SampleCollectionData> SearchOPSampleCollectionDetails(SampleCollectionData objSampleCollectionData)
        {
            List<SampleCollectionData> result = null;
            try
            {
                LabSampleCollectionDA objSampleCollectionMasteDA = new LabSampleCollectionDA();
                result = objSampleCollectionMasteDA.SearchOPSampleCollectionDetails(objSampleCollectionData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }

        public List<SampleCollectionData> GetSampleCollectionDetailsByID(SampleCollectionData objSampleCollectionData)
        {
            List<SampleCollectionData> result = null;

            try
            {
                LabSampleCollectionDA objSampleCollectionMasteDA = new LabSampleCollectionDA();
                result = objSampleCollectionMasteDA.GetSampleCollectionDetailsByID(objSampleCollectionData);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeleteSampleCollectionDetailsByID(SampleCollectionData objSampleCollectionData)
        {
            int result = 0;
            try
            {
                LabSampleCollectionDA objSampleCollectionMasteDA = new LabSampleCollectionDA();
                result = objSampleCollectionMasteDA.DeleteSampleCollectionDetailsByID(objSampleCollectionData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<SampleCollectionData> SearchSampleCollectionDetails(SampleCollectionData objSampleCollectionData)
        {
            List<SampleCollectionData> result = null;
            try
            {
                LabSampleCollectionDA objSampleCollectionMasteDA = new LabSampleCollectionDA();
                result = objSampleCollectionMasteDA.SearchSampleCollectionDetails(objSampleCollectionData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<SampleCollectionData> SearchIPSampleCollectionDetails(SampleCollectionData objSampleCollectionData)
        {
            List<SampleCollectionData> result = null;
            try
            {
                LabSampleCollectionDA objSampleCollectionMasteDA = new LabSampleCollectionDA();
                result = objSampleCollectionMasteDA.SearchIPSampleCollectionDetails(objSampleCollectionData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<SampleCollectionData> SearchIPSampleCollectedDetails(SampleCollectionData objSampleCollectionData)
        {
            List<SampleCollectionData> result = null;
            try
            {
                LabSampleCollectionDA objSampleCollectionMasteDA = new LabSampleCollectionDA();
                result = objSampleCollectionMasteDA.SearchIPSampleCollectedDetails(objSampleCollectionData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }

        public List<SampleCollectionData> SearchIPDSampleCollectionDetails(SampleCollectionData objSampleCollectionData)
        {
            List<SampleCollectionData> result = null;
            try
            {
                LabSampleCollectionDA objSampleCollectionMasteDA = new LabSampleCollectionDA();
                result = objSampleCollectionMasteDA.SearchIPDSampleCollectionDetails(objSampleCollectionData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<SampleCollectionData> GetLabSampleDetails(SampleCollectionData objSampleCollectionData)
        {
            List<SampleCollectionData> result = null;
            try
            {
                LabSampleCollectionDA objSampleCollectionMasteDA = new LabSampleCollectionDA();
                result = objSampleCollectionMasteDA.GetLabSampleDetails(objSampleCollectionData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<SampleCollectionData> GetIPDLabSampleDetails(SampleCollectionData objSampleCollectionData)
        {
            List<SampleCollectionData> result = null;
            try
            {
                LabSampleCollectionDA objSampleCollectionMasteDA = new LabSampleCollectionDA();
                result = objSampleCollectionMasteDA.GetIPDLabSampleDetails(objSampleCollectionData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<SampleCollectionData> SearchLabSampleCollectionDetails(SampleCollectionData objSampleCollectionData)
        {
            List<SampleCollectionData> result = null;
            try
            {
                LabSampleCollectionDA objSampleCollectionMasteDA = new LabSampleCollectionDA();
                result = objSampleCollectionMasteDA.SearchLabSampleCollectionDetails(objSampleCollectionData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<SampleCollectionData> SearchLabResultCollectionDetails(SampleCollectionData objSampleCollectionData)
        {
            List<SampleCollectionData> result = null;
            try
            {
                LabSampleCollectionDA objSampleCollectionMasteDA = new LabSampleCollectionDA();
                result = objSampleCollectionMasteDA.SearchLabResultCollectionDetails(objSampleCollectionData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }

        public List<SampleCollectionData> GetTestName(SampleCollectionData objD)
        {
            List<SampleCollectionData> result = null;
            try
            {
                LabSampleCollectionDA objpatientDA = new LabSampleCollectionDA();
                result = objpatientDA.GetTestName(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<SampleCollectionData> GetSubTestName(SampleCollectionData objD)
        {
            List<SampleCollectionData> result = null;
            try
            {
                LabSampleCollectionDA objpatientDA = new LabSampleCollectionDA();
                result = objpatientDA.GetSubTestName(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<SampleCollectionData> GetTestNameDeviceinitiated(SampleCollectionData objD)
        {
            List<SampleCollectionData> result = null;
            try
            {
                LabSampleCollectionDA objpatientDA = new LabSampleCollectionDA();
                result = objpatientDA.GetTestNameDeviceinitiated(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }

        public List<SampleCollectionData> GetLabID(SampleCollectionData objD)
        {
            List<SampleCollectionData> result = null;
            try
            {
                LabSampleCollectionDA objpatientDA = new LabSampleCollectionDA();
                result = objpatientDA.GetLabID(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<SampleCollectionData> GetPatientAdmissionDetailsByLabID(SampleCollectionData objD)
        {
            List<SampleCollectionData> result = null;
            try
            {
                LabSampleCollectionDA objpatientDA = new LabSampleCollectionDA();
                result = objpatientDA.GetPatientAdmissionDetailsByLabID(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<SampleCollectionData> GetSampleDescription(SampleCollectionData objD)
        {
            List<SampleCollectionData> result = null;
            try
            {
                LabSampleCollectionDA objpatientDA = new LabSampleCollectionDA();
                result = objpatientDA.GetSampleDescription(objD);

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
                LabSampleCollectionDA objempientDA = new LabSampleCollectionDA();
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
        public List<RadioLabReportVerificationData> GetLabCommentTemplateByID(RadioLabReportVerificationData obj)
        {
            List<RadioLabReportVerificationData> result = null;

            try
            {
                LabSampleCollectionDA objempientDA = new LabSampleCollectionDA();
                result = objempientDA.GetLabCommentTemplateByID(obj);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<SampleCollectionData> GetRadioInvestigationno(SampleCollectionData objD)
        {
            List<SampleCollectionData> result = null;
            try
            {
                LabSampleCollectionDA objpatientDA = new LabSampleCollectionDA();
                result = objpatientDA.GetRadioInvestigationno(objD);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<SampleCollectionData> GetRadioPatientName(SampleCollectionData objD)
        {
            List<SampleCollectionData> result = null;
            try
            {
                LabSampleCollectionDA objpatientDA = new LabSampleCollectionDA();
                result = objpatientDA.GetRadioPatientName(objD);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<SampleCollectionData> GetRadioTestNames(SampleCollectionData objD)
        {
            List<SampleCollectionData> result = null;
            try
            {
                LabSampleCollectionDA objpatientDA = new LabSampleCollectionDA();
                result = objpatientDA.GetRadioTestNames(objD);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<SampleCollectionData> GetRadioTestList(SampleCollectionData objSampleCollectionData)
        {
            List<SampleCollectionData> result = null;

            try
            {
                LabSampleCollectionDA objSampleCollectionMasteDA = new LabSampleCollectionDA();
                result = objSampleCollectionMasteDA.GetRadioTestList(objSampleCollectionData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<LabResultData> GetRadioReportDetails(LabResultData objSampleCollectionData)
        {
            List<LabResultData> result = null;

            try
            {
                LabSampleCollectionDA objSampleCollectionMasteDA = new LabSampleCollectionDA();
                result = objSampleCollectionMasteDA.GetRadioReportDetails(objSampleCollectionData);
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

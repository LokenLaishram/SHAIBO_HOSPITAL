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
using Mediqura.DAL.MedEmergencyDA;

namespace Mediqura.BOL.MedEmergencyBO
{
    public class EmrgAdmissionBO
    {

        public List<EmrgAdmissionData> UpdateEmrgAdmissionDetails(EmrgAdmissionData objadmission)
        {
            List<EmrgAdmissionData> result = null;
            try
            {
                EmrgAdmissionDA objDA = new EmrgAdmissionDA();
                result = objDA.UpdateEmrgAdmissionDetails(objadmission);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<EmrgAdmissionData> GetEMRGNo(EmrgAdmissionData objadmission)
        {
            List<EmrgAdmissionData> result = null;
            try
            {
                EmrgAdmissionDA objempientDA = new EmrgAdmissionDA();
                result = objempientDA.GetEMRGNo(objadmission);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }

        public List<EmrgAdmissionData> GetEmrgList(EmrgAdmissionData objbill)
        {
            List<EmrgAdmissionData> result = null;
            try
            {
                EmrgAdmissionDA objDA = new EmrgAdmissionDA();
                result = objDA.GetEmrgList(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<EmrgAdmissionData> GetEmrgNo(EmrgAdmissionData objD)
        {
            List<EmrgAdmissionData> result = null;
            try
            {
                EmrgAdmissionDA objpatientDA = new EmrgAdmissionDA();
                result = objpatientDA.GetEmrgNo(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<EmrgAdmissionData> GetPhrEmrgNo(EmrgAdmissionData objD)
        {
            List<EmrgAdmissionData> result = null;
            try
            {
                EmrgAdmissionDA objpatientDA = new EmrgAdmissionDA();
                result = objpatientDA.GetPhrEmrgNo(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<EmrgAdmissionData> GetFEmrgNo(EmrgAdmissionData objD)
        {
            List<EmrgAdmissionData> result = null;
            try
            {
                EmrgAdmissionDA objpatientDA = new EmrgAdmissionDA();
                result = objpatientDA.GetFEmrgNo(objD);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<EmrgAdmissionData> EmergencyInvnumber(EmrgAdmissionData objD)
        {
            List<EmrgAdmissionData> result = null;
            try
            {
                EmrgAdmissionDA objpatientDA = new EmrgAdmissionDA();
                result = objpatientDA.EmergencyInvnumber(objD);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<EmrgAdmissionData> Getemergnecyservicenumber(EmrgAdmissionData objD)
        {
            List<EmrgAdmissionData> result = null;
            try
            {
                EmrgAdmissionDA objpatientDA = new EmrgAdmissionDA();
                result = objpatientDA.Getemergnecyservicenumber(objD);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<EmrgAdmissionData> GetEmrgDetailsByID(EmrgAdmissionData objEmrgData)
        {
            List<EmrgAdmissionData> result = null;

            try
            {
                EmrgAdmissionDA objEmrgDA = new EmrgAdmissionDA();
                result = objEmrgDA.GetEmrgDetailsByID(objEmrgData);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<EmrgAdmissionData> GetPatientsDetailsByEmrgNo(EmrgAdmissionData objD)
        {
            List<EmrgAdmissionData> result = null;
            try
            {
                EmrgAdmissionDA objpatientDA = new EmrgAdmissionDA();
                result = objpatientDA.GetPatientsDetailsByEmrgNo(objD);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<EmrgAdmissionData> GetemrgfinalbillDetail(EmrgAdmissionData objD)
        {
            List<EmrgAdmissionData> result = null;
            try
            {
                EmrgAdmissionDA objpatientDA = new EmrgAdmissionDA();
                result = objpatientDA.GetemrgfinalbillDetail(objD);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<PHREmrgFinalData> Get_PHR_emrgfinalBilldetails(PHREmrgFinalData objD)
        {
            List<PHREmrgFinalData> result = null;
            try
            {
                EmrgAdmissionDA objpatientDA = new EmrgAdmissionDA();
                result = objpatientDA.Get_PHR_emrgfinalBilldetails(objD);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<PHRIPFinalData> Get_PHR_IPfinalBilldetails(PHRIPFinalData objD)
        {
            List<PHRIPFinalData> result = null;
            try
            {
                EmrgAdmissionDA objpatientDA = new EmrgAdmissionDA();
                result = objpatientDA.Get_PHR_IPfinalBilldetails(objD);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<EmrgAdmissionData> GetEmrgServices(EmrgAdmissionData objservice)
        {
            List<EmrgAdmissionData> result = null;

            try
            {
                EmrgAdmissionDA objServiceDA = new EmrgAdmissionDA();
                result = objServiceDA.GetEmrgServices(objservice);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<EmrgAdmissionData> GetEmrgPHRServices(EmrgAdmissionData objservice)
        {
            List<EmrgAdmissionData> result = null;

            try
            {
                EmrgAdmissionDA objServiceDA = new EmrgAdmissionDA();
                result = objServiceDA.GetEmrgPHRServices(objservice);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<EmrgAdmissionData> GetServiceChargeByID(EmrgAdmissionData objbill)
        {
            List<EmrgAdmissionData> result = null;
            try
            {
                EmrgAdmissionDA objDA = new EmrgAdmissionDA();
                result = objDA.GetServiceChargeByID(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<EmrgAdmissionData> UpdateEMRGServiceRecord(EmrgAdmissionData objbill)
        {
            List<EmrgAdmissionData> result = null;
            try
            {
                EmrgAdmissionDA objDA = new EmrgAdmissionDA();
                result = objDA.UpdateEMRGServiceRecord(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int UpdateEMRGServiceRecordPHR(EmrgAdmissionData objbill)
        {
            int result = 0;
            try
            {
                EmrgAdmissionDA objDA = new EmrgAdmissionDA();
                result = objDA.UpdateEMRGServiceRecordPHR(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<EmrgAdmissionData> GetEMRGServiceList(EmrgAdmissionData objbill)
        {
            List<EmrgAdmissionData> result = null;
            try
            {
                EmrgAdmissionDA objDA = new EmrgAdmissionDA();
                result = objDA.GetEMRGServiceList(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<EmrgAdmissionData> GetEMRGDrugList(EmrgAdmissionData objbill)
        {
            List<EmrgAdmissionData> result = null;
            try
            {
                EmrgAdmissionDA objDA = new EmrgAdmissionDA();
                result = objDA.GetEMRGDrugList(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeleteEMGServiceRecordByEMRGNo(EmrgAdmissionData objstd)
        {
            int result = 0;
            try
            {
                EmrgAdmissionDA objDA = new EmrgAdmissionDA();
                result = objDA.DeleteEMGServiceRecordByEMRGNo(objstd);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int Deleteemrgfinalbill(EmrgAdmissionData objstd)
        {
            int result = 0;
            try
            {
                EmrgAdmissionDA objDA = new EmrgAdmissionDA();
                result = objDA.Deleteemrgfinalbill(objstd);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeletePHRemrgfinalbill(PHREmrgFinalData objstd)
        {
            int result = 0;
            try
            {
                EmrgAdmissionDA objDA = new EmrgAdmissionDA();
                result = objDA.DeletePHRemrgfinalbill(objstd);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeletePHRipfinalbill(PHRIPFinalData objstd)
        {
            int result = 0;
            try
            {
                EmrgAdmissionDA objDA = new EmrgAdmissionDA();
                result = objDA.DeletePHRipfinalbill(objstd);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeleteEMGDrugRecordByEMRGNo(EmrgAdmissionData objstd)
        {
            int result = 0;

            try
            {
                EmrgAdmissionDA objDA = new EmrgAdmissionDA();
                result = objDA.DeleteEMGDrugRecordByEMRGNo(objstd);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public int DeleteEmrgRegnDetailsByID(EmrgAdmissionData objstd)
        {

            int result = 0;

            try
            {
                EmrgAdmissionDA objDA = new EmrgAdmissionDA();
                result = objDA.DeleteEmrgRegnDetailsByID(objstd);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<EmrgAdmissionData> SearchEMRGSampleCollectionDetails(EmrgAdmissionData objSampleCollectionData)
        {
            List<EmrgAdmissionData> result = null;
            try
            {
                EmrgAdmissionDA objSampleCollectionMasteDA = new EmrgAdmissionDA();
                result = objSampleCollectionMasteDA.SearchEMRGSampleCollectionDetails(objSampleCollectionData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<EmrgAdmissionData> GetSampleDescription(EmrgAdmissionData objD)
        {
            List<EmrgAdmissionData> result = null;
            try
            {
                EmrgAdmissionDA objpatientDA = new EmrgAdmissionDA();
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
        public int UpdateEmrgSampleCollectionDetails(EmrgAdmissionData objSampleCollectionData)
        {
            int result = 0;
            try
            {
                EmrgAdmissionDA objLabSampleCollectionDA = new EmrgAdmissionDA();
                result = objLabSampleCollectionDA.UpdateEmrgSampleCollectionDetails(objSampleCollectionData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<EmrgAdmissionData> GetEMRGPhrDrugList(EmrgAdmissionData objbill)
        {
            List<EmrgAdmissionData> result = null;
            try
            {
                EmrgAdmissionDA objDA = new EmrgAdmissionDA();
                result = objDA.GetEMRGPhrDrugList(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<EmrgAdmissionData> GetEMRGetEMRGServicesGPhrDrugList(EmrgAdmissionData objbill)
        {
            List<EmrgAdmissionData> result = null;
            try
            {
                EmrgAdmissionDA objDA = new EmrgAdmissionDA();
                result = objDA.GetEMRGServices(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<EmrgAdmissionData> GetEMRGServices(EmrgAdmissionData objbill)
        {
            List<EmrgAdmissionData> result = null;
            try
            {
                EmrgAdmissionDA objDA = new EmrgAdmissionDA();
                result = objDA.GetEMRGServices(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<EmrgAdmissionData> Get_EMRG_BilldetailsbyID(EmrgAdmissionData objD)
        {
            List<EmrgAdmissionData> result = null;
            try
            {
                EmrgAdmissionDA objpatientDA = new EmrgAdmissionDA();
                result = objpatientDA.Get_EMRG_BilldetailsbyID(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<EmrgAdmissionData> Update_EMRGFinal_BillDetails(EmrgAdmissionData objD)
        {
            List<EmrgAdmissionData> result = null;
            try
            {
                EmrgAdmissionDA objpatientDA = new EmrgAdmissionDA();
                result = objpatientDA.Update_EMRGFinal_BillDetails(objD);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<PHREmrgFinalData> Update_PHR_EMRGFinal_BillDetails(PHREmrgFinalData objD)
        {
            List<PHREmrgFinalData> result = null;
            try
            {
                EmrgAdmissionDA objpatientDA = new EmrgAdmissionDA();
                result = objpatientDA.Update_PHR_EMRGFinal_BillDetails(objD);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<PHRIPFinalData> Update_PHR_IPFinal_BillDetails(PHRIPFinalData objD)
        {
            List<PHRIPFinalData> result = null;
            try
            {
                EmrgAdmissionDA objpatientDA = new EmrgAdmissionDA();
                result = objpatientDA.Update_PHR_IPFinal_BillDetails(objD);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<EmrgAdmissionData> GetEMRGbillList(EmrgAdmissionData objD)
        {
            List<EmrgAdmissionData> result = null;
            try
            {
                EmrgAdmissionDA objpatientDA = new EmrgAdmissionDA();
                result = objpatientDA.GetEMRGbillList(objD);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<PHREmrgFinalData> GetPharamacyFinalBillList(PHREmrgFinalData objD)
        {
            List<PHREmrgFinalData> result = null;
            try
            {
                EmrgAdmissionDA objpatientDA = new EmrgAdmissionDA();
                result = objpatientDA.GetPharamacyFinalBillList(objD);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<PHRIPFinalData> GetPharamacyIPFinalBillList(PHRIPFinalData objD)
        {
            List<PHRIPFinalData> result = null;
            try
            {
                EmrgAdmissionDA objpatientDA = new EmrgAdmissionDA();
                result = objpatientDA.GetPharamacyIPFinalBillList(objD);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int Update_EMRGPhr_BillDetails(EmrgAdmissionData objD)
        {
            int result = 0;
            try
            {
                EmrgAdmissionDA objpatientDA = new EmrgAdmissionDA();
                result = objpatientDA.Update_EMRGPhr_BillDetails(objD);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<EmrgAdmissionData> GetEMRGPhrbillList(EmrgAdmissionData objD)
        {
            List<EmrgAdmissionData> result = null;
            try
            {
                EmrgAdmissionDA objpatientDA = new EmrgAdmissionDA();
                result = objpatientDA.GetEMRGPhrbillList(objD);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<EMRDiscountListData> GetDiscountListForEmergency(EmrgAdmissionData objbill)
        {
            List<EMRDiscountListData> result = null;
            try
            {
                EmrgAdmissionDA objDA = new EmrgAdmissionDA();
                result = objDA.GetDiscountListForEmergency(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<EmrgAdmissionData> UpdateEmrgCodes(EmrgAdmissionData objadmission)
        {
            List<EmrgAdmissionData> result = null;
            try
            {
                EmrgAdmissionDA objDA = new EmrgAdmissionDA();
                result = objDA.UpdateEmrgCodes(objadmission);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<EmrgAdmissionData> GetEmrgListReport(EmrgAdmissionData objEmrgdata)
        {
            List<EmrgAdmissionData> result = null;
            try
            {
                EmrgAdmissionDA objDA = new EmrgAdmissionDA();
                result = objDA.GetEmrgListReport(objEmrgdata);
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

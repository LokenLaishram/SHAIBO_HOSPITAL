using Mediqura.CommonData.MedHrData;
using Mediqura.CommonData.MedUtilityData;
using Mediqura.CommonData.PatientData;
using Mediqura.DAL.PatientDA;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.BOL.PatientBO
{
    public class RegistrationBO
    {
        public int UpdatePatientRegistration(PatientData objpat)
        {
            int result = 0;

            try
            {
                RegistrationDA objpatientDA = new RegistrationDA();
                result = objpatientDA.UpdatePatientRegistration(objpat);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public int UpdatePatientAge(int Age, int Month, int Day, Int64 UHID)
        {
            int result = 0;

            try
            {
                RegistrationDA objpatientDA = new RegistrationDA();
                result = objpatientDA.UpdatePatientAge(Age, Month, Day, UHID);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<PatientAgeData> GetpatientDOB(PatientAgeData objpat)
        {

            List<PatientAgeData> result = null;

            try
            {
                RegistrationDA objpatientDA = new RegistrationDA();
                result = objpatientDA.GetpatientDOB(objpat);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<ReferalData> GetReferalDetails(ReferalData objD)
        {
            List<ReferalData> result = null;
            try
            {
                RegistrationDA objpatientDA = new RegistrationDA();
                result = objpatientDA.GetReferalDetails(objD);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<PatientData> GetRadioUHID(PatientData objD)
        {
            List<PatientData> result = null;
            try
            {
                RegistrationDA objpatientDA = new RegistrationDA();
                result = objpatientDA.GetRadioUHID(objD);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<ReferalData> GetPayableReferalDetails(ReferalData objD)
        {
            List<ReferalData> result = null;
            try
            {
                RegistrationDA objpatientDA = new RegistrationDA();
                result = objpatientDA.GetPayableReferalDetails(objD);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<PatientData> GetAdvncUHID(PatientData objD)
        {
            List<PatientData> result = null;
            try
            {
                RegistrationDA objpatientDA = new RegistrationDA();
                result = objpatientDA.GetAdvncUHID(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int CancelAppointment(PatientData objpat)
        {
            int result = 0;

            try
            {
                RegistrationDA objpatientDA = new RegistrationDA();
                result = objpatientDA.CancelAppointment(objpat);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public int DeleteDependent(PatientData objpat)
        {
            int result = 0;

            try
            {
                RegistrationDA objpatientDA = new RegistrationDA();
                result = objpatientDA.DeleteDependent(objpat);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public int DeleteEmployeeDiscount(PatientData objpat)
        {
            int result = 0;

            try
            {
                RegistrationDA objpatientDA = new RegistrationDA();
                result = objpatientDA.DeleteEmployeeDiscount(objpat);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public int InsertBookingdetails(BookingData objpat)
        {
            int result = 0;

            try
            {
                RegistrationDA objpatientDA = new RegistrationDA();
                result = objpatientDA.InsertBookingdetails(objpat);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public int AddPatientConsultantSheet(PatientData objpat)
        {
            int result = 0;

            try
            {
                RegistrationDA objpatientDA = new RegistrationDA();
                result = objpatientDA.AddPatientConsultantSheetDA(objpat);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<PatientData> SearchPatientList(PatientData objpat)
        {

            List<PatientData> result = null;

            try
            {
                RegistrationDA objpatientDA = new RegistrationDA();
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

        public List<PatientData> SearchPatientDetails(PatientData objpat)
        {

            List<PatientData> result = null;

            try
            {
                RegistrationDA objpatientDA = new RegistrationDA();
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
        public List<PatientData> Getpatientlastvisitdetails(PatientData objpat)
        {

            List<PatientData> result = null;

            try
            {
                RegistrationDA objpatientDA = new RegistrationDA();
                result = objpatientDA.Getpatientlastvisitdetails(objpat);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;


        }
        public List<PatientData> OPDVisitHistorydata(PatientData objpat)
        {

            List<PatientData> result = null;

            try
            {
                RegistrationDA objpatientDA = new RegistrationDA();
                result = objpatientDA.OPDVisitHistorydata(objpat);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<PatientData> GetPatientOPDdata(PatientData objpat)
        {

            List<PatientData> result = null;

            try
            {
                RegistrationDA objpatientDA = new RegistrationDA();
                result = objpatientDA.OPDVisitHistoryDA(objpat);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<PatientData> Getconsultingsheetlist(PatientData objpat)
        {
            List<PatientData> result = null;
            try
            {
                RegistrationDA objpatientDA = new RegistrationDA();
                result = objpatientDA.Getconsultingsheetlist(objpat);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<PatientData> GetUHID(PatientData objD)
        {
            List<PatientData> result = null;
            try
            {
                RegistrationDA objpatientDA = new RegistrationDA();
                result = objpatientDA.GetUHID(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }

        public List<PatientData> GetPatientNameWithUHID(PatientData objD)
        {
            List<PatientData> result = null;
            try
            {
                RegistrationDA objpatientDA = new RegistrationDA();
                result = objpatientDA.GetPatientNameWithUHID(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<PatientData> GetAvancedepositpatientdetail(PatientData objD)
        {
            List<PatientData> result = null;
            try
            {
                RegistrationDA objpatientDA = new RegistrationDA();
                result = objpatientDA.GetAvancedepositpatientdetail(objD);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<PatientData> GetPhrAvancedepositpatientdetail(PatientData objD)
        {
            List<PatientData> result = null;
            try
            {
                RegistrationDA objpatientDA = new RegistrationDA();
                result = objpatientDA.GetPhrAvancedepositpatientdetail(objD);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<PatientData> GetPatientNumberByBillCategory(PatientData objD)
        {
            List<PatientData> result = null;
            try
            {
                RegistrationDA objpatientDA = new RegistrationDA();
                result = objpatientDA.GetPatientNumberByBillCategory(objD);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<PatientData> GetOPDNumber(PatientData objD)
        {
            List<PatientData> result = null;
            try
            {
                RegistrationDA objpatientDA = new RegistrationDA();
                result = objpatientDA.GetOPDNumber(objD);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }

        public List<PatientData> GetEmgPatientName(PatientData objD)
        {
            List<PatientData> result = null;
            try
            {
                RegistrationDA objpatientDA = new RegistrationDA();
                result = objpatientDA.GetEmgPatientName(objD);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<PatientData> GetAutoUHIDIPNo(PatientData objD)
        {
            List<PatientData> result = null;
            try
            {
                RegistrationDA objpatientDA = new RegistrationDA();
                result = objpatientDA.GetAutoUHIDIPNo(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<PatientData> GetDetailUHID(PatientData objD)
        {
            List<PatientData> result = null;
            try
            {
                RegistrationDA objpatientDA = new RegistrationDA();
                result = objpatientDA.GetDetailUHID(objD);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<PatientData> GetDetailUHIDwithOpnumber(PatientData objD)
        {
            List<PatientData> result = null;
            try
            {
                RegistrationDA objpatientDA = new RegistrationDA();
                result = objpatientDA.GetDetailUHIDwithOpnumber(objD);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<PatientData> GetPatientDetailsByUHIDandOpnumber(PatientData objD)
        {
            List<PatientData> result = null;
            try
            {
                RegistrationDA objpatientDA = new RegistrationDA();
                result = objpatientDA.GetPatientDetailsByUHIDandOpnumber(objD);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<PatientData> GetPatientDetailByOPnumber(PatientData objD)
        {
            List<PatientData> result = null;
            try
            {
                RegistrationDA objpatientDA = new RegistrationDA();
                result = objpatientDA.GetPatientDetailByOPnumber(objD);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<PatientData> GetLabDeviceUHID(PatientData objD)
        {
            List<PatientData> result = null;
            try
            {
                RegistrationDA objpatientDA = new RegistrationDA();
                result = objpatientDA.GetLabDeviceUHID(objD);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<PatientData> GetLabDevicecompletedUHID(PatientData objD)
        {
            List<PatientData> result = null;
            try
            {
                RegistrationDA objpatientDA = new RegistrationDA();
                result = objpatientDA.GetLabDevicecompletedUHID(objD);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<PatientData> GetIPDLabIPNo(PatientData objD)
        {
            List<PatientData> result = null;
            try
            {
                RegistrationDA objpatientDA = new RegistrationDA();
                result = objpatientDA.GetIPDLabIPNo(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }

        public List<PatientData> GetLabUHID(PatientData objD)
        {
            List<PatientData> result = null;
            try
            {
                RegistrationDA objpatientDA = new RegistrationDA();
                result = objpatientDA.GetLabUHID(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<PatientData> GetLabIPNo(PatientData objD)
        {
            List<PatientData> result = null;
            try
            {
                RegistrationDA objpatientDA = new RegistrationDA();
                result = objpatientDA.GetLabIPNo(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<PatientData> GetLabDevicecompletedIPNo(PatientData objD)
        {
            List<PatientData> result = null;
            try
            {
                RegistrationDA objpatientDA = new RegistrationDA();
                result = objpatientDA.GetLabDevicecompletedIPNo(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }

        public List<PatientData> GetOTTeamList(PatientData objpat)
        {

            List<PatientData> result = null;

            try
            {
                RegistrationDA objpatientDA = new RegistrationDA();
                result = objpatientDA.GetOTTeamList(objpat);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;


        }
        public List<BookingData> GetPatientList(BookingData objpat)
        {

            List<BookingData> result = null;

            try
            {
                RegistrationDA objpatientDA = new RegistrationDA();
                result = objpatientDA.GetPatientList(objpat);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;


        }
        public List<BookingData> GetDocWisePatientList(BookingData objpat)
        {

            List<BookingData> result = null;

            try
            {
                RegistrationDA objpatientDA = new RegistrationDA();
                result = objpatientDA.GetDocWisePatientList(objpat);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;


        }
        public List<PatientData> GetPhrPatientDetailsByUHID(PatientData objD)
        {
            List<PatientData> result = null;
            try
            {
                RegistrationDA objpatientDA = new RegistrationDA();
                result = objpatientDA.GetPhrPatientDetailsByUHID(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<PatientData> GetPatientDetailsByUHID(PatientData objD)
        {
            List<PatientData> result = null;
            try
            {
                RegistrationDA objpatientDA = new RegistrationDA();
                result = objpatientDA.GetPatientDetailsByUHID(objD);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<PatientData> GetPatientDepositDetailByUHID(PatientData objD)
        {
            List<PatientData> result = null;
            try
            {
                RegistrationDA objpatientDA = new RegistrationDA();
                result = objpatientDA.GetPatientDepositDetailByUHID(objD);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<PatientData> GetPatientDetailsByUHIDwithdeposittype(PatientData objD)
        {
            List<PatientData> result = null;
            try
            {
                RegistrationDA objpatientDA = new RegistrationDA();
                result = objpatientDA.GetPatientDetailsByUHIDwithdeposittype(objD);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<OPDdata> GetPatientNumber(OPDdata objD)
        {
            List<OPDdata> result = null;
            try
            {
                RegistrationDA objpatientDA = new RegistrationDA();
                result = objpatientDA.GetPatientNumber(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<PatientData> GetLabDeviceInitiatedDetailsByUHID(PatientData objD)
        {
            List<PatientData> result = null;
            try
            {
                RegistrationDA objpatientDA = new RegistrationDA();
                result = objpatientDA.GetLabDeviceInitiatedDetailsByUHID(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }

        public List<PatientData> GetLabPatientDetailsByUHID(PatientData objD)
        {
            List<PatientData> result = null;
            try
            {
                RegistrationDA objpatientDA = new RegistrationDA();
                result = objpatientDA.GetLabPatientDetailsByUHID(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<PatientData> GetLabSampleCollectedDetailsByUHID(PatientData objD)
        {
            List<PatientData> result = null;
            try
            {
                RegistrationDA objpatientDA = new RegistrationDA();
                result = objpatientDA.GetLabSampleCollectedDetailsByUHID(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<PatientData> GetPatientOTDetailsByIPNo(PatientData objD)
        {
            List<PatientData> result = null;
            try
            {
                RegistrationDA objpatientDA = new RegistrationDA();
                result = objpatientDA.GetPatientOTDetailsByIPNo(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<PatientData> GetPatientAdmissionDetailsByUHID(PatientData objD)
        {
            List<PatientData> result = null;
            try
            {
                RegistrationDA objpatientDA = new RegistrationDA();
                result = objpatientDA.GetPatientAdmissionDetailsByUHID(objD);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<PatientData> GetPatientAdmissionDetailsByUHIDIPNo(PatientData objD)
        {
            List<PatientData> result = null;
            try
            {
                RegistrationDA objpatientDA = new RegistrationDA();
                result = objpatientDA.GetPatientAdmissionDetailsByUHIDIPNo(objD);

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
        public List<PatientData> GetIPpatientName(PatientData objD)
        {
            List<PatientData> result = null;
            try
            {
                RegistrationDA objpatientDA = new RegistrationDA();
                result = objpatientDA.GetIPpatientName(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<PatientData> GetPatientNameIP(PatientData objD)
        {
            List<PatientData> result = null;
            try
            {
                RegistrationDA objpatientDA = new RegistrationDA();
                result = objpatientDA.GetPatientNameIP(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<PatientData> GetDuePatientName(PatientData objD)
        {
            List<PatientData> result = null;
            try
            {
                RegistrationDA objpatientDA = new RegistrationDA();
                result = objpatientDA.GetDuePatientName(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<PatientData> GetDueBill(PatientData objD)
        {
            List<PatientData> result = null;
            try
            {
                RegistrationDA objpatientDA = new RegistrationDA();
                result = objpatientDA.GetDueBill(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<PatientData> GetotPatientName(PatientData objD)
        {
            List<PatientData> result = null;
            try
            {
                RegistrationDA objpatientDA = new RegistrationDA();
                result = objpatientDA.GetotPatientName(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<PatientData> GetDiscountedPatientName(PatientData objD)
        {
            List<PatientData> result = null;
            try
            {
                RegistrationDA objpatientDA = new RegistrationDA();
                result = objpatientDA.GetDiscountedPatientName(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<PatientData> GetContactno(PatientData objD)
        {
            List<PatientData> result = null;
            try
            {
                RegistrationDA objpatientDA = new RegistrationDA();
                result = objpatientDA.GetContactno(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public int DeletePatientDetailsByID(PatientData objstd)
        {
            int result = 0;

            try
            {
                RegistrationDA objpatientDA = new RegistrationDA();
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
        public List<PatientData> GetPtientDeatilbyID(PatientData objpat)
        {

            List<PatientData> result = null;

            try
            {
                RegistrationDA objpatientDA = new RegistrationDA();
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
        public List<PatientData> GetPatientDetailsByIPNo(PatientData objD)
        {
            List<PatientData> result = null;
            try
            {
                RegistrationDA objpatientDA = new RegistrationDA();
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

        public List<PatientData> GetAdmittedPatientName(PatientData objD)
        {
            List<PatientData> result = null;
            try
            {
                RegistrationDA objpatientDA = new RegistrationDA();
                result = objpatientDA.GetAdmittedPatientName(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }

        public List<PatientData> GetotPatientDetailsByIPNo(PatientData objD)
        {
            List<PatientData> result = null;
            try
            {
                RegistrationDA objpatientDA = new RegistrationDA();
                result = objpatientDA.GetotPatientDetailsByIPNo(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }

        public List<PatientData> GetPatientDetailsByOtpassnumber(PatientData objD)
        {
            List<PatientData> result = null;
            try
            {
                RegistrationDA objpatientDA = new RegistrationDA();
                result = objpatientDA.GetPatientDetailsByOtpassnumber(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<PatientData> GetIpdNestedData(PatientData objpat)
        {
            List<PatientData> result = null;

            try
            {
                RegistrationDA objpatientDA = new RegistrationDA();
                result = objpatientDA.GetIpdNestedData(objpat);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;


        }
        public List<PatientData> IpdPatientEnquiry(PatientData objpat)
        {
            List<PatientData> result = null;

            try
            {
                RegistrationDA objpatientDA = new RegistrationDA();
                result = objpatientDA.IpdPatientEnquiryDA(objpat);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<PatientData> OPDTreatmentStatus(PatientData objpat)
        {

            List<PatientData> result = null;

            try
            {
                RegistrationDA objpatientDA = new RegistrationDA();
                result = objpatientDA.OPDTreatmentStatusDA(objpat);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;


        }
        public int UpdateTreatmentStatus(PatientData objpat)
        {
            int result = 0;

            try
            {
                RegistrationDA objpatientDA = new RegistrationDA();
                result = objpatientDA.UpdateTreatmentStatusDA(objpat);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<PatientData> GetPatientDetailsByBillNo(PatientData objD)
        {
            List<PatientData> result = null;
            try
            {
                RegistrationDA objpatientDA = new RegistrationDA();
                result = objpatientDA.GetPatientDetailsByBillNo(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }

        public List<PatientData> GetDoctorName(PatientData objD)
        {
            List<PatientData> result = null;
            try
            {
                RegistrationDA objpatientDA = new RegistrationDA();
                result = objpatientDA.GetDoctorName(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<OTSchedulingData> GetDoctorNameForOTScheduling(OTSchedulingData objD)
        {
            List<OTSchedulingData> result = null;
            try
            {
                RegistrationDA objpatientDA = new RegistrationDA();
                result = objpatientDA.GetDoctorNameForOTScheduling(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<ICDData> GetICDCode(ICDData objD)
        {
            List<ICDData> result = null;
            try
            {
                RegistrationDA objpatientDA = new RegistrationDA();
                result = objpatientDA.GetICDCode(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<PatientData> GetPatientNameforOTScheduling(PatientData objD)
        {
            List<PatientData> result = null;
            try
            {
                RegistrationDA objpatientDA = new RegistrationDA();
                result = objpatientDA.GetPatientNameforOTScheduling(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<PatientData> GetRadioIPNo(PatientData objD)
        {
            List<PatientData> result = null;
            try
            {
                RegistrationDA objpatientDA = new RegistrationDA();
                result = objpatientDA.GetRadioIPNo(objD);

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

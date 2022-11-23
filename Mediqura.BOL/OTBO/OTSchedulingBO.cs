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
    public class OTSchedulingBO
    {
        public List<OTSchedulingData> GetPatientList(OTSchedulingData objD)
        {
            List<OTSchedulingData> result = null;
            try
            {
                OTSchedulingDA objpatientDA = new OTSchedulingDA();
                result = objpatientDA.GetPatientList(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public int UpdateOTSchedulingDetails(OTSchedulingData objbill)
        {
            int result = 0;
            try
            {
                OTSchedulingDA objDA = new OTSchedulingDA();
                result = objDA.UpdateOTSchedulingDetails(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<OTSchedulingData> GetOTTodayList(OTSchedulingData objD)
        {
            List<OTSchedulingData> result = null;
            try
            {
                OTSchedulingDA objpatientDA = new OTSchedulingDA();
                result = objpatientDA.GetOTTodayList(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public int CancelOTScheduling(OTSchedulingData objpat)
        {
            int result = 0;

            try
            {
                OTSchedulingDA objpatientDA = new OTSchedulingDA();
                result = objpatientDA.CancelOTScheduling(objpat);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<OTSchedulingData> GetOTPatientList(OTSchedulingData objD)
        {
            List<OTSchedulingData> result = null;
            try
            {
                OTSchedulingDA objpatientDA = new OTSchedulingDA();
                result = objpatientDA.GetOTPatientList(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public int DeleteOTScheduleByID(OTSchedulingData objtransfer)
        {
            int result = 0;

            try
            {
                OTSchedulingDA objDA = new OTSchedulingDA();
                result = objDA.DeleteOTScheduleByID(objtransfer);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        //*************NEW OT SCHEDULER*******************//

        public List<OTSchedulingData> GetPatientDetailsByUHID(OTSchedulingData objD)
        {
            List<OTSchedulingData> result = null;
            try
            {
                OTSchedulingDA objpatientDA = new OTSchedulingDA();
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
        public int UpdateOTScheduler(OTSchedulingData objpat)
        {
            int result = 0;

            try
            {
                OTSchedulingDA objpatientDA = new OTSchedulingDA();
                result = objpatientDA.UpdateOTScheduler(objpat);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<OTSchedulingData> GetOTScheduleByID(OTSchedulingData objotedit)
        {

            List<OTSchedulingData> result = null;

            try
            {
                OTSchedulingDA objOTDA = new OTSchedulingDA();
                result = objOTDA.GetOTScheduleByID(objotedit);

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

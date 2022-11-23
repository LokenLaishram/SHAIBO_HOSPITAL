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
    public class OTStatusBO
    {
        public List<OTStatusData> GetOT_StatusList(OTStatusData objpat)
        {

            List<OTStatusData> result = null;

            try
            {
                OTStatusDA objpatientDA = new OTStatusDA();
                result = objpatientDA.GetOT_StatusList(objpat);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;


        }
        public int UpdateOT_status(OTStatusData objdischarge)
        {
            int result = 0;
            try
            {
                OTStatusDA objDA = new OTStatusDA();
                result = objDA.UpdateOT_status(objdischarge);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<OTStatusData> GetOT_Status(OTStatusData objD)
        {
            List<OTStatusData> result = null;
            try
            {
                OTStatusDA objpatientDA = new OTStatusDA();
                result = objpatientDA.GetOT_Status(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<OTStatusData> getIPNo(OTStatusData objadmission)
        {
            List<OTStatusData> result = null;
            try
            {
                OTStatusDA objempientDA = new OTStatusDA();
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
    }
}

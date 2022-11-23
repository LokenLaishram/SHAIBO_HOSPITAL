using Mediqura.CommonData.MedUtilityData;
using Mediqura.CommonData.MedHrData;
using Mediqura.DAL.MedUtilityDA;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Mediqura.BOL.MedUtilityBO
{
    public class UploadFileBO
    {
        public List<UploadFileData> GetPatientAdmissionDetailsByName(UploadFileData objD)
        {
            List<UploadFileData> result = null;
            try
            {
                UploadFileDA objpatientDA = new UploadFileDA();
                result = objpatientDA.GetPatientAdmissionDetailsByName(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public int UpdateOPDLabBill(UploadFileData objUploadData)
        {
            int result = 0;
            try
            {
                UploadFileDA objUploadDA = new UploadFileDA();
                result = objUploadDA.UpdateOPDLabBill(objUploadData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int UpdateEmpFile(EmpFileData objUploadData)
        {
            int result = 0;
            try
            {
                UploadFileDA objUploadDA = new UploadFileDA();
                result = objUploadDA.UpdateEmpFile(objUploadData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<UploadFileData> GetUploadList(UploadFileData objDischargeIntimationData)
        {
            List<UploadFileData> result = null;
            try
            {
                UploadFileDA objLabSubGroupTypeMasteDA = new UploadFileDA();
                result = objLabSubGroupTypeMasteDA.GetUploadList(objDischargeIntimationData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeleteFileByID(UploadFileData objOTRoleMasterData)
        {
            int result = 0;
            try
            {
                UploadFileDA objOTRoleMasterDA = new UploadFileDA();
                result = objOTRoleMasterDA.DeleteFileByID(objOTRoleMasterData);
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

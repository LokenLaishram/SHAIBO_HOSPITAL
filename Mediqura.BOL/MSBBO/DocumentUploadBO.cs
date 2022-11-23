using Mediqura.CommonData.MedUtilityData;
using Mediqura.CommonData.MSBData;
using Mediqura.DAL.MSBDA;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Mediqura.BOL.MSBBO
{
    public class DocumentUploadBO
    {
        public List<DocumentUploadData> GetPatientAdmissionDetailsByName(DocumentUploadData objD)
        {
            List<DocumentUploadData> result = null;
            try
            {
                DocumentUploaderDA objpatientDA = new DocumentUploaderDA();
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
        //public int UpdateOPDLabBill(DocumentUploadData objUploadData)
        //{
        //    int result = 0;
        //    try
        //    {
        //        DocumentUploaderDA objUploadDA = new DocumentUploaderDA();
        //        result = objUploadDA.UpdateOPDLabBill(objUploadData);
        //    }
        //    catch (Exception ex)
        //    {
        //        PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
        //        LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
        //        throw new BusinessProcessException("4000001", ex);
        //    }
        //    return result;
        //}
        public int UpdateEmpFile(DocumentUploadData objUploadData)
        {
            int result = 0;
            try
            {
                DocumentUploaderDA objUploadDA = new DocumentUploaderDA();
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
        public List<DocumentUploadData> GetUploadList(DocumentUploadData objDischargeIntimationData)
        {
            List<DocumentUploadData> result = null;
            try
            {
                DocumentUploaderDA objLabSubGroupTypeMasteDA = new DocumentUploaderDA();
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
        public int DeleteDepFiledetailByID(DocumentUploadData objOTRoleMasterData)
        {
            int result = 0;
            try
            {
                DocumentUploaderDA objOTRoleMasterDA = new DocumentUploaderDA();
                result = objOTRoleMasterDA.DeleteDepFiledetailByID(objOTRoleMasterData);
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

using Mediqura.CommonData.MedHrData;
using Mediqura.DAL.MedHrDA;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Mediqura.BOL.MedHrBO
{
    public class EmpFileBO
    {
        public List<EmpFileData> GetUploadList(EmpFileData objDischargeIntimationData)
        {
            List<EmpFileData> result = null;
            try
            {
                EmpFileDA objLabSubGroupTypeMasteDA = new EmpFileDA();
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
        public int DeleteEmpFiledetailByID(EmpFileData objOTRoleMasterData)
        {
            int result = 0;
            try
            {
                EmpFileDA objOTRoleMasterDA = new EmpFileDA();
                result = objOTRoleMasterDA.DeleteEmpFiledetailByID(objOTRoleMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<EmployeeData> GetPhotoList(EmployeeData objDischargeIntimationData)
        {
            List<EmployeeData> result = null;
            try
            {
                EmpFileDA objLabSubGroupTypeMasteDA = new EmpFileDA();
                result = objLabSubGroupTypeMasteDA.GetPhotoList(objDischargeIntimationData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int UpdateEmpPhoto(EmployeeData objoutsource)
        {
            int result = 0;
            try
            {
                EmpFileDA objDA = new EmpFileDA();
                result = objDA.UpdateEmpPhoto(objoutsource);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int UpLoadPhotoEmp(EmployeeData objoutsource)
        {
            int result = 0;
            try
            {
                EmpFileDA objDA = new EmpFileDA();
                result = objDA.UpLoadPhotoEmp(objoutsource);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int UpLoadSignEmp(EmployeeData objoutsource)
        {
            int result = 0;
            try
            {
                EmpFileDA objDA = new EmpFileDA();
                result = objDA.UpLoadSignEmp(objoutsource);
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

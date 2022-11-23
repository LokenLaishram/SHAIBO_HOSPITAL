using Mediqura.CommonData.MedUtilityData;
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
    public class LabSampleBO
    {
        public int UpdateSampleTypeDetails(LabSampleData objSampleTypeMasterData)
        {
            int result = 0;
            try
            {
                LabSampleDA objPaymentTypeMasteDA = new LabSampleDA();
                result = objPaymentTypeMasteDA.UpdateSampleTypeDetails(objSampleTypeMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<LabSampleData> SearchSampleTypeDetails(LabSampleData objPaymentTypeMasterData)
        {
            List<LabSampleData> result = null;
            try
            {
                LabSampleDA objPaymentTypeMasteDA = new LabSampleDA();
                result = objPaymentTypeMasteDA.SearchSampleTypeDetails(objPaymentTypeMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<LabSampleData> GetSampleTypeDetailsByID(LabSampleData objPaymentTypeMasterData)
        {
            List<LabSampleData> result = null;

            try
            {
                LabSampleDA objPaymentTypeMasteDA = new LabSampleDA();
                result = objPaymentTypeMasteDA.GetSampleTypeDetailsByID(objPaymentTypeMasterData);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeleteSampleTypeDetailsByID(LabSampleData objPaymentTypeMasterData)
        {
            int result = 0;
            try
            {
                LabSampleDA objPaymentTypeMasterDA = new LabSampleDA();
                result = objPaymentTypeMasterDA.DeleteSampleTypeDetailsByID(objPaymentTypeMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<LabSampleData> SearchSampleDetails(LabSampleData objPaymentTypeMasterData)
        {
            List<LabSampleData> result = null;
            try
            {
                LabSampleDA objPaymentTypeMasteDA = new LabSampleDA();
                result = objPaymentTypeMasteDA.SearchSampleDetails(objPaymentTypeMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }

        public List<LabSampleData> GetSampleType(LabSampleData objPaymentTypeMasterData)
        {
            List<LabSampleData> result = null;

            try
            {
                LabSampleDA objPaymentTypeMasteDA = new LabSampleDA();
                result = objPaymentTypeMasteDA.GetSampleType(objPaymentTypeMasterData);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }

        public List<LabSampleData> GetLabTestUnits(LabSampleData objPaymentTypeMasterData)
        {
            List<LabSampleData> result = null;

            try
            {
                LabSampleDA objPaymentTypeMasteDA = new LabSampleDA();
                result = objPaymentTypeMasteDA.GetLabTestUnits(objPaymentTypeMasterData);

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

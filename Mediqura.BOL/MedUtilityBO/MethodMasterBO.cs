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
    public  class MethodMasterBO
    {
        public int UpdateMethodDetails(MethodMasterData objData)
        {
            int result = 0;
            try
            {
                MethodMasterDA objMasteDA = new MethodMasterDA();
                result = objMasteDA.UpdateMethodDetails(objData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<MethodMasterData> GetMethodTypeDetailsByID(MethodMasterData objDesignationTypeMasterData)
        {
            List<MethodMasterData> result = null;

            try
            {
                MethodMasterDA objMasteDA = new MethodMasterDA();
                result = objMasteDA.GetMethodTypeDetailsByID(objDesignationTypeMasterData);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeleteMethodTypeDetailsByID(MethodMasterData objDesignationTypeMasterData)
        {
            int result = 0;
            try
            {
                MethodMasterDA objMasteDA = new MethodMasterDA();
                result = objMasteDA.DeleteMethodTypeDetailsByID(objDesignationTypeMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<MethodMasterData> SearchMethodDetails(MethodMasterData objDesignationTypeMasterData)
        {
            List<MethodMasterData> result = null;
            try
            {
                MethodMasterDA objMasteDA = new MethodMasterDA();
                result = objMasteDA.SearchMethodDetails(objDesignationTypeMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<MethodMasterData> SearchLabMethodDetails(MethodMasterData objDesignationTypeMasterData)
        {
            List<MethodMasterData> result = null;
            try
            {
                MethodMasterDA objMasteDA = new MethodMasterDA();
                result = objMasteDA.SearchLabMethodDetails(objDesignationTypeMasterData);
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

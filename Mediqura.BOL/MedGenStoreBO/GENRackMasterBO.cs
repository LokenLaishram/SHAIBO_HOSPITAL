using Mediqura.CommonData.MedStore;
using Mediqura.DAL.MedGenStoreDA;
using Mediqura.DAL.MedStore;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Mediqura.BOL.MedGenStoreBO
{
    public class GENRackMasterBO
    {
        public int UpdateRackDetails(GENRackMasterData objData)
        {
            int result = 0;
            try
            {
                GENRackMasterDA objDA = new GENRackMasterDA();
                result = objDA.UpdateRackDetails(objData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int UpdateItemSubRackDetails(GENRackMasterData objData)
        {
            int result = 0;
            try
            {
                GENRackMasterDA objDA = new GENRackMasterDA();
                result = objDA.UpdateItemSubRackDetails(objData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<GENRackMasterData> GetRackTypeDetailsByID(GENRackMasterData objItemMasterData)
        {
            List<GENRackMasterData> result = null;

            try
            {
                GENRackMasterDA objItemMasterDA = new GENRackMasterDA();
                result = objItemMasterDA.GetRackTypeDetailsByID(objItemMasterData);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeleteRackTypeDetailsByID(GENRackMasterData objItemMasterData)
        {
            int result = 0;
            try
            {
                GENRackMasterDA objItemMasteDA = new GENRackMasterDA();
                result = objItemMasteDA.DeleteRackTypeDetailsByID(objItemMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<GENRackMasterData> SearchRackTypeDetails(GENRackMasterData objItemMasterData)
        {
            List<GENRackMasterData> result = null;
            try
            {
                GENRackMasterDA objItemMasteDA = new GENRackMasterDA();
                result = objItemMasteDA.SearchRackTypeDetails(objItemMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<GENRackMasterData> SearchSubRackDetails(GENRackMasterData objItemMasterData)
        {
            List<GENRackMasterData> result = null;
            try
            {
                GENRackMasterDA objItemMasteDA = new GENRackMasterDA();
                result = objItemMasteDA.SearchSubRackDetails(objItemMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<GENRackMasterData> GetItemSubRackTypeDetailsByID(GENRackMasterData objItemMasterData)
        {
            List<GENRackMasterData> result = null;

            try
            {
                GENRackMasterDA objItemMasterDA = new GENRackMasterDA();
                result = objItemMasterDA.GetItemSubRackTypeDetailsByID(objItemMasterData);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeleteSubRackTypeDetailsByID(GENRackMasterData objItemMasterData)
        {
            int result = 0;
            try
            {
                GENRackMasterDA objItemMasteDA = new GENRackMasterDA();
                result = objItemMasteDA.DeleteSubRackTypeDetailsByID(objItemMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<GENRackMasterData> SearchItemLocationDetails(GENRackMasterData objItemMasterData)
        {
            List<GENRackMasterData> result = null;
            try
            {
                GENRackMasterDA objItemMasteDA = new GENRackMasterDA();
                result = objItemMasteDA.SearchItemLocationDetails(objItemMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int UpdateItemLocationDetails(GENRackMasterData objData)
        {
            int result = 0;
            try
            {
                GENRackMasterDA objDA = new GENRackMasterDA();
                result = objDA.UpdateItemLocationDetails(objData);
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

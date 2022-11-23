using Mediqura.CommonData.MedStore;
using Mediqura.DAL.MedStore;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.BOL.MedStore
{
    public class RackMasterBO
    {
        public int UpdateRackDetails(RackMasterData objstock)
        {
            int result = 0;
            try
            {
                RackMasterDA objDA = new RackMasterDA();
                result = objDA.UpdateRackDetails(objstock);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int UpdateItemSubRackDetails(RackMasterData objstock)
        {
            int result = 0;
            try
            {
                RackMasterDA objDA = new RackMasterDA();
                result = objDA.UpdateItemSubRackDetails(objstock);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<RackMasterData> GetRackTypeDetailsByID(RackMasterData objItemMasterData)
        {
            List<RackMasterData> result = null;

            try
            {
                RackMasterDA objItemMasterDA = new RackMasterDA();
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
        public int DeleteRackTypeDetailsByID(RackMasterData objItemMasterData)
        {
            int result = 0;
            try
            {
                RackMasterDA objItemMasteDA = new RackMasterDA();
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
        public List<RackMasterData> SearchRackTypeDetails(RackMasterData objItemMasterData)
        {
            List<RackMasterData> result = null;
            try
            {
                RackMasterDA objItemMasteDA = new RackMasterDA();
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
        public List<RackMasterData> SearchSubRackDetails(RackMasterData objItemMasterData)
        {
            List<RackMasterData> result = null;
            try
            {
                RackMasterDA objItemMasteDA = new RackMasterDA();
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
        public List<RackMasterData> GetItemSubRackTypeDetailsByID(RackMasterData objItemMasterData)
        {
            List<RackMasterData> result = null;

            try
            {
                RackMasterDA objItemMasterDA = new RackMasterDA();
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
        public int DeleteSubRackTypeDetailsByID(RackMasterData objItemMasterData)
        {
            int result = 0;
            try
            {
                RackMasterDA objItemMasteDA = new RackMasterDA();
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
        public List<RackMasterData> SearchItemLocationDetails(RackMasterData objItemMasterData)
        {
            List<RackMasterData> result = null;
            try
            {
                RackMasterDA objItemMasteDA = new RackMasterDA();
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
        public int UpdateItemLocationDetails(RackMasterData objstock)
        {
            int result = 0;
            try
            {
                RackMasterDA objDA = new RackMasterDA();
                result = objDA.UpdateItemLocationDetails(objstock);
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

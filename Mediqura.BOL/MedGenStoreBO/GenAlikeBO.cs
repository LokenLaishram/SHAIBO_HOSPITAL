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
    public class GenAlikeBO
    {
        public int UpdateGenLookAlikeDetails(AlikeData objGenItemMasterData)
        {
            int result = 0;
            try
            {
                GenAlikeDA objItemMasteDA = new GenAlikeDA();
                result = objItemMasteDA.UpdateGenLookAlikeDetails(objGenItemMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<AlikeData> GetGenLookAlikeDetailsByID(AlikeData objGenItemMasterData)
        {
            List<AlikeData> result = null;

            try
            {
                GenAlikeDA objGenStoreItemMasterDA = new GenAlikeDA();
                result = objGenStoreItemMasterDA.GetGenLookAlikeDetailsByID(objGenItemMasterData);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeleteGenLookDetailsByID(AlikeData objGenItemMasterData)
        {
            int result = 0;
            try
            {
                GenAlikeDA objItemMasteDA = new GenAlikeDA();
                result = objItemMasteDA.DeleteGenLookDetailsByID(objGenItemMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<AlikeData> SearchLookAlikeDetails(AlikeData objGenItemMasterData)
        {
            List<AlikeData> result = null;
            try
            {
                GenAlikeDA objItemMasteDA = new GenAlikeDA();
                result = objItemMasteDA.SearchLookAlikeDetails(objGenItemMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<AlikeData> SearchLookAlikeType(AlikeData objGenItemMasterData)
        {
            List<AlikeData> result = null;
            try
            {
                GenAlikeDA objItemMasteDA = new GenAlikeDA();
                result = objItemMasteDA.SearchLookAlikeType(objGenItemMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int UpdateGenSoundAlikeDetails(AlikeData objGenItemMasterData)
        {
            int result = 0;
            try
            {
                GenAlikeDA objItemMasteDA = new GenAlikeDA();
                result = objItemMasteDA.UpdateGenSoundAlikeDetails(objGenItemMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<AlikeData> GetGenSoundAlikeDetailsByID(AlikeData objGenItemMasterData)
        {
            List<AlikeData> result = null;

            try
            {
                GenAlikeDA objGenStoreItemMasterDA = new GenAlikeDA();
                result = objGenStoreItemMasterDA.GetGenSoundAlikeDetailsByID(objGenItemMasterData);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeleteGenSoundDetailsByID(AlikeData objGenItemMasterData)
        {
            int result = 0;
            try
            {
                GenAlikeDA objItemMasteDA = new GenAlikeDA();
                result = objItemMasteDA.DeleteGenSoundDetailsByID(objGenItemMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<AlikeData> SearchSoundAlikeDetails(AlikeData objGenItemMasterData)
        {
            List<AlikeData> result = null;
            try
            {
                GenAlikeDA objItemMasteDA = new GenAlikeDA();
                result = objItemMasteDA.SearchSoundAlikeDetails(objGenItemMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<AlikeData> SearchSoundAlikeType(AlikeData objGenItemMasterData)
        {
            List<AlikeData> result = null;
            try
            {
                GenAlikeDA objItemMasteDA = new GenAlikeDA();
                result = objItemMasteDA.SearchSoundAlikeType(objGenItemMasterData);
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

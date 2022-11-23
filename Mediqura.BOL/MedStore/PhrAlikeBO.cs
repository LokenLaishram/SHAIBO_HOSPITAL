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

namespace Mediqura.BOL.MedStore
{
    public class PhrAlikeBO
    {
        public int UpdatePhrLookAlikeDetails(AlikeData objData)
        {
            int result = 0;
            try
            {
                PhrAlikeDA objItemMasteDA = new PhrAlikeDA();
                result = objItemMasteDA.UpdatePhrLookAlikeDetails(objData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<AlikeData> GetPhrLookAlikeDetailsByID(AlikeData objData)
        {
            List<AlikeData> result = null;

            try
            {
                PhrAlikeDA objGenStoreItemMasterDA = new PhrAlikeDA();
                result = objGenStoreItemMasterDA.GetPhrLookAlikeDetailsByID(objData);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeletePhrLookDetailsByID(AlikeData objGenItemMasterData)
        {
            int result = 0;
            try
            {
                PhrAlikeDA objItemMasteDA = new PhrAlikeDA();
                result = objItemMasteDA.DeletePhrLookDetailsByID(objGenItemMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<AlikeData> SearchLookAlikeDetails(AlikeData objData)
        {
            List<AlikeData> result = null;
            try
            {
                PhrAlikeDA objItemMasteDA = new PhrAlikeDA();
                result = objItemMasteDA.SearchLookAlikeDetails(objData);
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
                PhrAlikeDA objItemMasteDA = new PhrAlikeDA();
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
        public int UpdatePhrSoundAlikeDetails(AlikeData objGenItemMasterData)
        {
            int result = 0;
            try
            {
                PhrAlikeDA objItemMasteDA = new PhrAlikeDA();
                result = objItemMasteDA.UpdatePhrSoundAlikeDetails(objGenItemMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<AlikeData> GetPhrSoundAlikeDetailsByID(AlikeData objGenItemMasterData)
        {
            List<AlikeData> result = null;

            try
            {
                PhrAlikeDA objGenStoreItemMasterDA = new PhrAlikeDA();
                result = objGenStoreItemMasterDA.GetPhrSoundAlikeDetailsByID(objGenItemMasterData);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeletePhrSoundDetailsByID(AlikeData objData)
        {
            int result = 0;
            try
            {
                PhrAlikeDA objItemMasteDA = new PhrAlikeDA();
                result = objItemMasteDA.DeletePhrSoundDetailsByID(objData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<AlikeData> SearchPhrSoundAlikeDetails(AlikeData objData)
        {
            List<AlikeData> result = null;
            try
            {
                PhrAlikeDA objItemMasteDA = new PhrAlikeDA();
                result = objItemMasteDA.SearchPhrSoundAlikeDetails(objData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<AlikeData> SearchPhrSoundAlikeType(AlikeData objData)
        {
            List<AlikeData> result = null;
            try
            {
                PhrAlikeDA objItemMasteDA = new PhrAlikeDA();
                result = objItemMasteDA.SearchPhrSoundAlikeType(objData);
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

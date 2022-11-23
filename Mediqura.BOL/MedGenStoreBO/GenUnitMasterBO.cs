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
    public class GenUnitMasterBO
    {
        public int UpdateGenStrUnitDetails(GenUnitMasterData objData)
        {
            int result = 0;
            try
            {
                GenUnitMasterDA objMasteDA = new GenUnitMasterDA();
                result = objMasteDA.UpdateGenStrUnitDetails(objData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<GenUnitMasterData> GetGenUnitDetailsByID(GenUnitMasterData objData)
        {
            List<GenUnitMasterData> result = null;

            try
            {
                GenUnitMasterDA objMasteDA = new GenUnitMasterDA();
                result = objMasteDA.GetGenUnitDetailsByID(objData);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeleteStrUnitTypeDetailsByID(GenUnitMasterData objData)
        {
            int result = 0;
            try
            {
                GenUnitMasterDA objMasteDA = new GenUnitMasterDA();
                result = objMasteDA.DeleteStrUnitTypeDetailsByID(objData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<GenUnitMasterData> GetStrUnitDetails(GenUnitMasterData objData)
        {
            List<GenUnitMasterData> result = null;
            try
            {
                GenUnitMasterDA objMasteDA = new GenUnitMasterDA();
                result = objMasteDA.GetStrUnitDetails(objData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<GenUnitMasterData> GetStrUnitTypeDetails(GenUnitMasterData objData)
        {
            List<GenUnitMasterData> result = null;
            try
            {
                GenUnitMasterDA objMasteDA = new GenUnitMasterDA();
                result = objMasteDA.GetStrUnitTypeDetails(objData);
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

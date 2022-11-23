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
    public class MfgCompanyTypeMasterBO
    {
        public int UpdateCompanyTypeDetails(MfgCompanyMasterData objCompanyMasterData)
        {
            int result = 0;
            try
            {
                MfgCompanyTypeMasterDA objItemMasteDA = new MfgCompanyTypeMasterDA();
                result = objItemMasteDA.UpdateCompanyTypeDetails(objCompanyMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<MfgCompanyMasterData> GetCompanyTypeDetailsByID(MfgCompanyMasterData objItemMasterData)
        {
            List<MfgCompanyMasterData> result = null;

            try
            {
                MfgCompanyTypeMasterDA objItemMasterDA = new MfgCompanyTypeMasterDA();
                result = objItemMasterDA.GetCompanyTypeDetailsByID(objItemMasterData);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeleteMfgCompanyTypeDetailsByID(MfgCompanyMasterData objItemMasterData)
        {
            int result = 0;
            try
            {
                MfgCompanyTypeMasterDA objItemMasteDA = new MfgCompanyTypeMasterDA();
                result = objItemMasteDA.DeleteMfgCompanyTypeDetailsByID(objItemMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<MfgCompanyMasterData> SearchCompanyTypeExcel(MfgCompanyMasterData objItemMasterData)
        {
            List<MfgCompanyMasterData> result = null;
            try
            {
                MfgCompanyTypeMasterDA objItemMasteDA = new MfgCompanyTypeMasterDA();
                result = objItemMasteDA.SearchCompanyTypeExcel(objItemMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<MfgCompanyMasterData> SearchCompanyTypeDetails(MfgCompanyMasterData objItemMasterData)
        {
            List<MfgCompanyMasterData> result = null;
            try
            {
                MfgCompanyTypeMasterDA objItemMasteDA = new MfgCompanyTypeMasterDA();
                result = objItemMasteDA.SearchCompanyMasterDetails(objItemMasterData);
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

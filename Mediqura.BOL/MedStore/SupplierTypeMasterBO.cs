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
    public class SupplierTypeMasterBO
    {
        public int UpdateSupplierTypeDetails(SupplierTypeMasterData objSupplierMasterData)
        {
            int result = 0;
            try
            {
                SupplierTypeMasterDA objItemMasteDA = new SupplierTypeMasterDA();
                result = objItemMasteDA.UpdateSupplierTypeMasterDetails(objSupplierMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<SupplierTypeMasterData> GetSupplierTypeDetailsByID(SupplierTypeMasterData objItemMasterData)
        {
            List<SupplierTypeMasterData> result = null;

            try
            {
                SupplierTypeMasterDA objItemMasterDA = new SupplierTypeMasterDA();
                result = objItemMasterDA.GetISupplierMasterDetailsByID(objItemMasterData);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeleteSupplierTypeDetailsByID(SupplierTypeMasterData objItemMasterData)
        {
            int result = 0;
            try
            {
                SupplierTypeMasterDA objItemMasteDA = new SupplierTypeMasterDA();
                result = objItemMasteDA.DeleteSupplierMasterDetailsByID(objItemMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<SupplierTypeMasterData> SearchSupplierTypeExcel(SupplierTypeMasterData objItemMasterData)
        {
            List<SupplierTypeMasterData> result = null;
            try
            {
                SupplierTypeMasterDA objItemMasteDA = new SupplierTypeMasterDA();
                result = objItemMasteDA.SearchSupplierTypeExcel(objItemMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<SupplierTypeMasterData> SearchSupplierTypeDetails(SupplierTypeMasterData objItemMasterData)
        {
            List<SupplierTypeMasterData> result = null;
            try
            {
                SupplierTypeMasterDA objItemMasteDA = new SupplierTypeMasterDA();
                result = objItemMasteDA.SearchSupplierMasterDetails(objItemMasterData);
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

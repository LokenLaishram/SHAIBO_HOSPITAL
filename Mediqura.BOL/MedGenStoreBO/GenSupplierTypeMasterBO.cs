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
    public class GenSupplierTypeMasterBO
    {
        public int UpdateSupplierTypeDetails(GenSupplierTypeMasterData objSupplierMasterData)
        {
            int result = 0;
            try
            {
                GenSupplierTypeMasterDA objItemMasteDA = new GenSupplierTypeMasterDA();
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
        public List<GenSupplierTypeMasterData> GetSupplierTypeDetailsByID(GenSupplierTypeMasterData objMasterData)
        {
            List<GenSupplierTypeMasterData> result = null;

            try
            {
                GenSupplierTypeMasterDA objItemMasterDA = new GenSupplierTypeMasterDA();
                result = objItemMasterDA.GetISupplierMasterDetailsByID(objMasterData);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeleteSupplierTypeDetailsByID(GenSupplierTypeMasterData objMasterData)
        {
            int result = 0;
            try
            {
                GenSupplierTypeMasterDA objItemMasteDA = new GenSupplierTypeMasterDA();
                result = objItemMasteDA.DeleteSupplierMasterDetailsByID(objMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<GenSupplierTypeMasterData> SearchSupplierTypeExcel(GenSupplierTypeMasterData objMasterData)
        {
            List<GenSupplierTypeMasterData> result = null;
            try
            {
                GenSupplierTypeMasterDA objItemMasteDA = new GenSupplierTypeMasterDA();
                result = objItemMasteDA.SearchSupplierTypeExcel(objMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<GenSupplierTypeMasterData> SearchSupplierTypeDetails(GenSupplierTypeMasterData objItemMasterData)
        {
            List<GenSupplierTypeMasterData> result = null;
            try
            {
                GenSupplierTypeMasterDA objItemMasteDA = new GenSupplierTypeMasterDA();
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

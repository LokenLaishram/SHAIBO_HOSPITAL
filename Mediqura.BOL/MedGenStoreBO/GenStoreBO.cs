using Mediqura.CommonData.MedStore;
using Mediqura.DAL;
using Mediqura.DAL.MedGenStoreDA;
using Mediqura.DAL.MedStore;
using Mediqura.Utility;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using Mediqura.Utility.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.BOL.MedGenStoreBO
{
    public class GenStoreBO
    {
        public List<GENStrData> GetPONo(GENStrData objD)
        {
            List<GENStrData> result = null;
            try
            {
                GenStoreDA objpatientDA = new GenStoreDA();
                result = objpatientDA.GetPONo(objD);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<GENStrData> Get_ItemNameByID(GENStrData objD)
        {
            List<GENStrData> result = null;
            try
            {
                GenStoreDA objpatientDA = new GenStoreDA();
                result = objpatientDA.Get_ItemNameByID(objD);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<GENStrData> GetItemName(GENStrData objD)
        {
            List<GENStrData> result = null;
            try
            {
                GenStoreDA objpatientDA = new GenStoreDA();
                result = objpatientDA.GetItemName(objD);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<GENStrData> GetReturnItemBysupplireID(GENStrData objD)
        {
            List<GENStrData> result = null;
            try
            {
                GenStoreDA objpatientDA = new GenStoreDA();
                result = objpatientDA.GetReturnItemBysupplireID(objD);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<GENStrData> GetItemNames(GENStrData objD)
        {
            List<GENStrData> result = null;
            try
            {
                GenStoreDA objpatientDA = new GenStoreDA();
                result = objpatientDA.GetItemNames(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<GENStrData> GetCompanyName(GENStrData objD)
        {
            List<GENStrData> result = null;
            try
            {
                GenStoreDA objpatientDA = new GenStoreDA();
                result = objpatientDA.GetCompanyName(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<GENStrData> GetSupplierName(GENStrData objD)
        {
            List<GENStrData> result = null;
            try
            {
                GenStoreDA objpatientDA = new GenStoreDA();
                result = objpatientDA.GetSupplierName(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<GENStrData> GetItemNameWithID(GENStrData objD)
        {
            List<GENStrData> result = null;
            try
            {
                GenStoreDA objpatientDA = new GenStoreDA();
                result = objpatientDA.GetItemNameWithID(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public int UpdateStockCondemnedItemDetails(GENStrData objstock)
        {
            int result = 0;
            try
            {
                GenStoreDA objDA = new GenStoreDA();
                result = objDA.UpdateStockCondemnedItemDetails(objstock);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<GENStrData> GetrecdNo(GENStrData objD)
        {
            List<GENStrData> result = null;
            try
            {
                GenStoreDA objpatientDA = new GenStoreDA();
                result = objpatientDA.GetrecdNo(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public int UpdateStockItemDetails(GENStrData objstock)
        {
            int result = 0;
            try
            {
                GenStoreDA objDA = new GenStoreDA();
                result = objDA.UpdateStockItemDetails(objstock);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<GENStrData> GetStockItemList(GENStrData objbill)
        {
            List<GENStrData> result = null;
            try
            {
                GenStoreDA objDA = new GenStoreDA();
                result = objDA.GetStockItemList(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public int DeleteStockItemListByID(GENStrData objstd)
        {
            int result = 0;

            try
            {
                GenStoreDA objDA = new GenStoreDA();
                result = objDA.DeleteStockItemListByID(objstd);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<GENStrData> GetUnitByItemID(GENStrData objD)
        {
            List<GENStrData> result = null;
            try
            {
                GenStoreDA objpatientDA = new GenStoreDA();
                result = objpatientDA.GetUnitByItemID(objD);

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

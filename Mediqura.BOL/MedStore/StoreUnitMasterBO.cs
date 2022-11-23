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
    public class StoreUnitMasterBO
    {
        public int UpdateStoreUnitDetails(StoreUnitMasterData objStoreUnitMasterData)
        {
            int result = 0;
            try
            {
                StoreUnitMasterDA objStoreUnitMasterDA = new StoreUnitMasterDA();
                result = objStoreUnitMasterDA.UpdateStoreUnitDetails(objStoreUnitMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<StoreUnitMasterData> SearchStoreUnitDetails(StoreUnitMasterData objStoreUnitMasterData)
        {
            List<StoreUnitMasterData> result = null;
            try
            {
                StoreUnitMasterDA objStoreUnitMasterDA = new StoreUnitMasterDA();
                result = objStoreUnitMasterDA.SearchStoreUnitDetails(objStoreUnitMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<StoreUnitMasterData> SearchStoreUnitExcel(StoreUnitMasterData objStoreUnitMasterData)
        {
            List<StoreUnitMasterData> result = null;
            try
            {
                StoreUnitMasterDA objStoreUnitMasterDA = new StoreUnitMasterDA();
                result = objStoreUnitMasterDA.SearchStoreUnitExcel(objStoreUnitMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<StoreUnitMasterData> GetStoreUnitDetailsByID(StoreUnitMasterData objStoreUnitMasterData)
        {
            List<StoreUnitMasterData> result = null;

            try
            {
                StoreUnitMasterDA objStoreUnitMasterDA = new StoreUnitMasterDA();
                result = objStoreUnitMasterDA.GetStoreUnitDetailsByID(objStoreUnitMasterData);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeleteStoreUnitDetailsByID(StoreUnitMasterData objStoreUnitMasterData)
        {
            int result = 0;
            try
            {
                StoreUnitMasterDA objStoreUnitMasterDA = new StoreUnitMasterDA();
                result = objStoreUnitMasterDA.DeleteStoreUnitDetailsByID(objStoreUnitMasterData);
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

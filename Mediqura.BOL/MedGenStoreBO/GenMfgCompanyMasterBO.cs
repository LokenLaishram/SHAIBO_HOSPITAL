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
    public class GenMfgCompanyMasterBO
    {
         public int UpdateGenStrCompanyDetails(GenMfgCompanyMasterData objdata)
            {
                int result = 0;
                try
                {
                    GenMfgCompanyMasterDA objDA = new GenMfgCompanyMasterDA();
                    result = objDA.UpdateGenStrCompanyDetails(objdata);
                }
                catch (Exception ex)
                {
                    PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                    LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                    throw new BusinessProcessException("4000001", ex);
                }
                return result;
            }
         public List<GenMfgCompanyMasterData> GetGenMfgCompanyDetailsByID(GenMfgCompanyMasterData objData)
        {
            List<GenMfgCompanyMasterData> result = null;

            try
            {
                GenMfgCompanyMasterDA objMasteDA = new GenMfgCompanyMasterDA();
                result = objMasteDA.GetGenMfgCompanyDetailsByID(objData);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeleteStrCompanyTypeDetailsByID(GenMfgCompanyMasterData objData)
        {
            int result = 0;
            try
            {
                GenMfgCompanyMasterDA objMasteDA = new GenMfgCompanyMasterDA();
                result = objMasteDA.DeleteStrCompanyTypeDetailsByID(objData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<GenMfgCompanyMasterData> GetStrCompanyType(GenMfgCompanyMasterData objData)
        {
            List<GenMfgCompanyMasterData> result = null;
            try
            {
                GenMfgCompanyMasterDA objMasteDA = new GenMfgCompanyMasterDA();
                result = objMasteDA.GetStrCompanyType(objData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<GenMfgCompanyMasterData> GetStrCompanyTypeDetails(GenMfgCompanyMasterData objData)
        {
            List<GenMfgCompanyMasterData> result = null;
            try
            {
                GenMfgCompanyMasterDA objMasteDA = new GenMfgCompanyMasterDA();
                result = objMasteDA.GetStrCompanyTypeDetails(objData);
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


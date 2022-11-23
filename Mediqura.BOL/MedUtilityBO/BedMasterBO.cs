using Mediqura.CommonData.MedUtilityData;
using Mediqura.DAL.MedUtilityDA;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.BOL.MedUtilityBO
{
     public class BedMasterBO
    {
        public int UpdateBedDetails(BedMasterData objWardMasterData)
        {
            int result = 0;
            try
            {
                BedMasterDA objBlockMasteDA = new BedMasterDA();
                result = objBlockMasteDA.UpdateBedDetails(objWardMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<BedMasterData> GetBedTypeDetailsByID(BedMasterData objBlockTypeMasterData)
        {
            List<BedMasterData> result = null;

            try
            {
                BedMasterDA objBlockTypeMasteDA = new BedMasterDA();
                result = objBlockTypeMasteDA.GetBedTypeDetailsByID(objBlockTypeMasterData);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<BedMasterData> Getautorooms(BedMasterData objromm)
        {
            List<BedMasterData> result = null;

            try
            {
                BedMasterDA objBlockTypeMasteDA = new BedMasterDA();
                result = objBlockTypeMasteDA.Getautorooms(objromm);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<BedMasterData> Getautobednos(BedMasterData objbed)
        {
            List<BedMasterData> result = null;

            try
            {
                BedMasterDA objBlockTypeMasteDA = new BedMasterDA();
                result = objBlockTypeMasteDA.Getautobednos(objbed);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeleteBedTypeDetailsByID(BedMasterData objBlockTypeMasterData)
        {
            int result = 0;
            try
            {
                BedMasterDA objBlockTypeMasteDA = new BedMasterDA();
                result = objBlockTypeMasteDA.DeleteBedTypeDetailsByID(objBlockTypeMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<BedMasterData> SearchBedTypeDetails(BedMasterData objBlockTypeMasterData)
        {
            List<BedMasterData> result = null;
            try
            {
                BedMasterDA objBlockTypeMasteDA = new BedMasterDA();
                result = objBlockTypeMasteDA.SearchBedTypeDetails(objBlockTypeMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<BedMasterData> SearchBedDetails(BedMasterData objBlockTypeMasterData)
        {
            List<BedMasterData> result = null;
            try
            {
                BedMasterDA objBlockTypeMasteDA = new BedMasterDA();
                result = objBlockTypeMasteDA.SearchBedDetails(objBlockTypeMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<BedDasboard> NurseBedDashBoard(Int64 user)
        {
            List<BedDasboard> result = null;
            try
            {
                BedMasterDA objBlockTypeMasteDA = new BedMasterDA();
                result = objBlockTypeMasteDA.NurseBedDashBoard(user);
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

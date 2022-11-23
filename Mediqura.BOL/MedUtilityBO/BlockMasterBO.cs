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
    public class BlockMasterBO
    {
        public int UpdateBlockDetails(BlockMasterData objBlockMasterData)
        {
            int result = 0;
            try
            {
                BlockMasterDA objBlockMasteDA = new BlockMasterDA();
                result = objBlockMasteDA.UpdateBlockDetails(objBlockMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<BlockMasterData> GetBlockTypeDetailsByID(BlockMasterData objBlockTypeMasterData)
        {
            List<BlockMasterData> result = null;

            try
            {
                BlockMasterDA objBlockTypeMasteDA = new BlockMasterDA();
                result = objBlockTypeMasteDA.GetBlockTypeDetailsByID(objBlockTypeMasterData);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeleteBlockTypeDetailsByID(BlockMasterData objBlockTypeMasterData)
        {
            int result = 0;
            try
            {
                BlockMasterDA objBlockTypeMasteDA = new BlockMasterDA();
                result = objBlockTypeMasteDA.DeleteBlockTypeDetailsByID(objBlockTypeMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<BlockMasterData> SearchBlockTypeDetails(BlockMasterData objBlockTypeMasterData)
        {
            List<BlockMasterData> result = null;
            try
            {
                BlockMasterDA objBlockTypeMasteDA = new BlockMasterDA();
                result = objBlockTypeMasteDA.SearchBlockTypeDetails(objBlockTypeMasterData);
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

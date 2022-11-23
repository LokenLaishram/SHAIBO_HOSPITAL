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
   public class DocTypeBO
    {
       public int UpdateDocTypeDetails(DocTypeData objOTRoleMasterData)
        {
            int result = 0;
            try
            {
                DocTypeDA objOTRoleMasterDA = new DocTypeDA();
                result = objOTRoleMasterDA.UpdateDocTypeDetails(objOTRoleMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<DocTypeData> SearchDocTypeDetails(DocTypeData objOTRoleMasterData)
        {
            List<DocTypeData> result = null;
            try
            {
                DocTypeDA objOTRoleMasterDA = new DocTypeDA();
                result = objOTRoleMasterDA.SearchDocTypeDetails(objOTRoleMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<DocTypeData> GetDocTypeDetailsByID(DocTypeData objOTRoleMasterData)
        {
            List<DocTypeData> result = null;

            try
            {
                DocTypeDA objOTRoleMasterDA = new DocTypeDA();
                result = objOTRoleMasterDA.GetDocTypeDetailsByID(objOTRoleMasterData);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeleteDocTypeDetailsByID(DocTypeData objOTRoleMasterData)
        {
            int result = 0;
            try
            {
                DocTypeDA objOTRoleMasterDA = new DocTypeDA();
                result = objOTRoleMasterDA.DeleteDocTypeDetailsByID(objOTRoleMasterData);
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

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
    public class DocTaxBO
    {
        public int UpdateDocTaxDetails(DocTaxData objDocTaxData)
        {
            int result = 0;
            try
            {
                DocTaxDA objDocTaxDA = new DocTaxDA();
                result = objDocTaxDA.UpdateDocTaxDetails(objDocTaxData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<DocTaxData> GetDocTaxDetailsByID(DocTaxData objBlockTypeMasterData)
        {
            List<DocTaxData> result = null;

            try
            {
                DocTaxDA objBlockTypeMasteDA = new DocTaxDA();
                result = objBlockTypeMasteDA.GetDocTaxDetailsByID(objBlockTypeMasterData);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeleteDocTaxDetailsByID(DocTaxData objBlockTypeMasterData)
        {
            int result = 0;
            try
            {
                DocTaxDA objBlockTypeMasteDA = new DocTaxDA();
                result = objBlockTypeMasteDA.DeleteDocTaxDetailsByID(objBlockTypeMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<DocTaxData> SearchDocTaxDetails(DocTaxData objBlockTypeMasterData)
        {
            List<DocTaxData> result = null;
            try
            {
                DocTaxDA objBlockTypeMasteDA = new DocTaxDA();
                result = objBlockTypeMasteDA.SearchDocTaxDetails(objBlockTypeMasterData);
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

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
    public class IndentStatusBO
    {
        public int UpdateIndentStatusDetails(IndentStatusMasterData objIndentStatusMasterData)
        {
            int result = 0;
            try
            {
                IndentStatusDA objIndentStatusDA = new IndentStatusDA();
                result = objIndentStatusDA.UpdateIndentStatusDetails(objIndentStatusMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<IndentStatusMasterData> SearchIndentStatusDetails(IndentStatusMasterData objIndentStatusMasterData)
        {
            List<IndentStatusMasterData> result = null;
            try
            {
                IndentStatusDA objIndentStatusDA = new IndentStatusDA();
                result = objIndentStatusDA.SearchIndentStatusDetails(objIndentStatusMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<IndentStatusMasterData> GetIndentStatusDetailsByID(IndentStatusMasterData objIndentStatusMasterData)
        {
            List<IndentStatusMasterData> result = null;

            try
            {
                IndentStatusDA objIndentStatusDA = new IndentStatusDA();
                result = objIndentStatusDA.GetIndentStatusDetailsByID(objIndentStatusMasterData);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeleteIndentStatusDetailsByID(IndentStatusMasterData objIndentStatusMasterData)
        {
            int result = 0;
            try
            {
                IndentStatusDA objIndentStatusDA = new IndentStatusDA();
                result = objIndentStatusDA.DeleteIndentStatusDetailsByID(objIndentStatusMasterData);
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

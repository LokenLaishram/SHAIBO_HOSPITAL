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
    public class IndentRequestTypeBO
    {
        public int UpdateIndentStatusDetails(IndentRequestTypeData objIndentRequestTypeData)
        {
            int result = 0;
            try
            {
                IndentRequestDA objIndentRequestDA = new IndentRequestDA();
                result = objIndentRequestDA.UpdateIndentStatusDetails(objIndentRequestTypeData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<IndentRequestTypeData> SearchIndentStatusDetails(IndentRequestTypeData objIndentRequestTypeData)
        {
            List<IndentRequestTypeData> result = null;
            try
            {
                IndentRequestDA objIndentRequestDA = new IndentRequestDA();
                result = objIndentRequestDA.SearchIndentStatusDetails(objIndentRequestTypeData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<IndentRequestTypeData> GetIndentStatusDetailsByID(IndentRequestTypeData objIndentRequestTypeData)
        {
            List<IndentRequestTypeData> result = null;

            try
            {
                IndentRequestDA objIndentRequestDA = new IndentRequestDA();
                result = objIndentRequestDA.GetIndentStatusDetailsByID(objIndentRequestTypeData);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeleteIndentStatusDetailsByID(IndentRequestTypeData objIndentRequestTypeData)
        {
            int result = 0;
            try
            {
                IndentRequestDA objIndentRequestDA = new IndentRequestDA();
                result = objIndentRequestDA.DeleteIndentStatusDetailsByID(objIndentRequestTypeData);
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

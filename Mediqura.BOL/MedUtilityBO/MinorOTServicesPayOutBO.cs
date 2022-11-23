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
    public class MinorOTServicesPayOutBO
    {
        public List<MinorOTServicesPayOutData> SearchMinorOTPayOutDetails(MinorOTServicesPayOutData objData)
        {
            List<MinorOTServicesPayOutData> result = null;
            try
            {
                MinorOTServicesPayOutDA objDA = new MinorOTServicesPayOutDA();
                result = objDA.SearchMinorOTPayOutDetails(objData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int UpdateMinorOTServicesMST(MinorOTServicesPayOutData objData)
        {
            int result = 0;
            try
            {
                MinorOTServicesPayOutDA objDA = new MinorOTServicesPayOutDA();
                result = objDA.UpdateMinorOTServicesMST(objData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<MinorOTServicesPayOutData> SearchMajorOTPayOutDetails(MinorOTServicesPayOutData objData)
        {
            List<MinorOTServicesPayOutData> result = null;
            try
            {
                MinorOTServicesPayOutDA objDA = new MinorOTServicesPayOutDA();
                result = objDA.SearchMajorOTPayOutDetails(objData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int UpdateMajorOTServicesMST(MinorOTServicesPayOutData objData)
        {
            int result = 0;
            try
            {
                MinorOTServicesPayOutDA objDA = new MinorOTServicesPayOutDA();
                result = objDA.UpdateMajorOTServicesMST(objData);
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

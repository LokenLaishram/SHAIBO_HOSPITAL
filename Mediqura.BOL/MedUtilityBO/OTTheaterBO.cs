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
    public class OTTheaterBO
    {
        public int UpdateOTTheaterDetails(OTTheaterData objOTTheaterData)
        {
            int result = 0;
            try
            {
                OTTheaterDA objOTTheaterDA = new OTTheaterDA();
                result = objOTTheaterDA.UpdateOTTheaterDetails(objOTTheaterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<OTTheaterData> SearchOTTheaterDetails(OTTheaterData objOTTheaterData)
        {
            List<OTTheaterData> result = null;
            try
            {
                OTTheaterDA objOTTheaterDA = new OTTheaterDA();
                result = objOTTheaterDA.SearchOTTheaterDetails(objOTTheaterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<OTTheaterData> GetOTTheaterDetailsByID(OTTheaterData objOTTheaterData)
        {
            List<OTTheaterData> result = null;

            try
            {
                OTTheaterDA objOTTheaterDA = new OTTheaterDA();
                result = objOTTheaterDA.GetOTTheaterDetailsByID(objOTTheaterData);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeleteOTTheaterDetailsByID(OTTheaterData objOTTheaterData)
        {
            int result = 0;
            try
            {
                OTTheaterDA objOTTheaterDA = new OTTheaterDA();
                result = objOTTheaterDA.DeleteOTTheaterDetailsByID(objOTTheaterData);
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

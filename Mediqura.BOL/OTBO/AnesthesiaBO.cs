using Mediqura.CommonData.OTData;
using Mediqura.DAL.OTDA;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.BOL.OTBO
{
    public class AnesthesiaBO
    {
        public int UpdateAnesTHDetails(AnesthesiaData objOTRoleMasterData)
        {
            int result = 0;
            try
            {
                AnesthesiaDA objOTRoleMasterDA = new AnesthesiaDA();
                result = objOTRoleMasterDA.UpdateAnesTHDetails(objOTRoleMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<AnesthesiaData> SearchAnesTHDetails(AnesthesiaData objOTRoleMasterData)
        {
            List<AnesthesiaData> result = null;
            try
            {
                AnesthesiaDA objOTRoleMasterDA = new AnesthesiaDA();
                result = objOTRoleMasterDA.SearchAnesTHDetails(objOTRoleMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<AnesthesiaData> GetAnesTHDetailsByID(AnesthesiaData objOTRoleMasterData)
        {
            List<AnesthesiaData> result = null;

            try
            {
                AnesthesiaDA objOTRoleMasterDA = new AnesthesiaDA();
                result = objOTRoleMasterDA.GetAnesTHDetailsByID(objOTRoleMasterData);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeleteAnesTHDetailsByID(AnesthesiaData objOTRoleMasterData)
        {
            int result = 0;
            try
            {
                AnesthesiaDA objOTRoleMasterDA = new AnesthesiaDA();
                result = objOTRoleMasterDA.DeleteAnesTHDetailsByID(objOTRoleMasterData);
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

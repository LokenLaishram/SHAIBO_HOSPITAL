using Mediqura.CommonData.MedLabData;
using Mediqura.DAL.MedLabDA;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.BOL.MedLabBO
{
    public class LabCenterCommissionBO
    {
        public int UpdateOutsourceDetails(OutsourceCommissionData objOTRoleMasterData)
        {
            int result = 0;
            try
            {
                LabtestCommissionDA objOTRoleMasterDA = new LabtestCommissionDA();
                result = objOTRoleMasterDA.UpdateOutsourceDetails(objOTRoleMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<OutsourceCommissionData> SearchOutsourceDetails(OutsourceCommissionData objOTRoleMasterData)
        {
            List<OutsourceCommissionData> result = null;
            try
            {
                LabtestCommissionDA objOTRoleMasterDA = new LabtestCommissionDA();
                result = objOTRoleMasterDA.SearchOutsourceDetails(objOTRoleMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<OutsourceCommissionData> GetOutsourceDetailsByID(OutsourceCommissionData objOTRoleMasterData)
        {
            List<OutsourceCommissionData> result = null;

            try
            {
                LabtestCommissionDA objOTRoleMasterDA = new LabtestCommissionDA();
                result = objOTRoleMasterDA.GetOutsourceDetailsByID(objOTRoleMasterData);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeleteOutsourceDetailsByID(OutsourceCommissionData objOTRoleMasterData)
        {
            int result = 0;
            try
            {
                LabtestCommissionDA objOTRoleMasterDA = new LabtestCommissionDA();
                result = objOTRoleMasterDA.DeleteOutsourceDetailsByID(objOTRoleMasterData);
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

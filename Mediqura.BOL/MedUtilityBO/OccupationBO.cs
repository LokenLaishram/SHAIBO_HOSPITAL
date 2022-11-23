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
    public class OccupationBO
    {
        public int UpdateOccupationDetails(OccupationData objDesignationTypeMasterData)
        {
            int result = 0;
            try
            {
                OccupationDA objDesignationTypeMasteDA = new OccupationDA();
                result = objDesignationTypeMasteDA.UpdateOccupationDetails(objDesignationTypeMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<OccupationData> GetOccupationByID(OccupationData objDesignationTypeMasterData)
        {
            List<OccupationData> result = null;

            try
            {
                OccupationDA objDesignationTypeMasteDA = new OccupationDA();
                result = objDesignationTypeMasteDA.GetOccupationByID(objDesignationTypeMasterData);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeleteOccupationByID(OccupationData objDesignationTypeMasterData)
        {
            int result = 0;
            try
            {
                OccupationDA objDepartmentTypeMasteDA = new OccupationDA();
                result = objDepartmentTypeMasteDA.DeleteOccupationByID(objDesignationTypeMasterData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<OccupationData> SearchOccupationDetails(OccupationData objDesignationTypeMasterData)
        {
            List<OccupationData> result = null;
            try
            {
                OccupationDA objPatientTypeMasteDA = new OccupationDA();
                result = objPatientTypeMasteDA.SearchOccupationDetails(objDesignationTypeMasterData);
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

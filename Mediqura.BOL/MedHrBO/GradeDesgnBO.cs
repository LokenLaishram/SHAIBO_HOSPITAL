using Mediqura.CommonData.MedHrData;
using Mediqura.DAL.MedHrDA;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.BOL.MedHrBO
{
    public class GradeDesgnBO
    {
        public List<GradeDesgnData> GetDesignationList(GradeDesgnData objData)
        {
            List<GradeDesgnData> result = null;
            try
            {
                GradeDesgnDA objDA = new GradeDesgnDA();
                result = objDA.GetDesignationList(objData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<GradeDesgnData> GetNursingList(GradeDesgnData objData)
        {
            List<GradeDesgnData> result = null;
            try
            {
                GradeDesgnDA objDA = new GradeDesgnDA();
                result = objDA.GetNursingList(objData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int UpdateDesignationAssignDetails(GradeDesgnData obj)
        {
            int result = 0;
            try
            {
                GradeDesgnDA objDA = new GradeDesgnDA();
                result = objDA.UpdateDesignationAssignDetails(obj);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<GradeDesgnData> GetDesgnName(GradeDesgnData objD)
        {
            List<GradeDesgnData> result = null;
            try
            {
                GradeDesgnDA objempientDA = new GradeDesgnDA();
                result = objempientDA.GetDesgnName(objD);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public int DeleteDesgnDetailsByID(GradeDesgnData objdata)
        {
            int result = 0;
            try
            {
                GradeDesgnDA objDA = new GradeDesgnDA();
                result = objDA.DeleteDesgnDetailsByID(objdata);
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

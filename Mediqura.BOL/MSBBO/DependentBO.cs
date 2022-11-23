using Mediqura.CommonData.MSBData;
using Mediqura.DAL.MSBDA;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.BOL.MSBBO
{
    public class DependentBO
    {
        public List<DepandentData> GetDependentEligibilityList(DepandentData objData)
        {
            List<DepandentData> result = null;
            try
            {
                DependentDA objDA = new DependentDA();
                result = objDA.GetDependentEligibilityList(objData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public int UpdateDependentEligibility(DepandentData objData)
        {
            int result = 0;
            try
            {
                DependentDA objDA = new DependentDA();
                result = objDA.UpdateDependentEligibility(objData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeleteDependentEligibilityByID(DepandentData objData)
        {
            int result = 0;

            try
            {
                DependentDA objDA = new DependentDA();
                result = objDA.DeleteDependentEligibilityByID(objData);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<MsbDepandentData> GetDependentDetailByUHID(MsbDepandentData objData)
        {
            List<MsbDepandentData> result = null;
            try
            {
                DependentDA objDA = new DependentDA();
                result = objDA.GetDependentDetailByUHID(objData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<MsbDepandentData> GetEmpDetailByEmpID(MsbDepandentData objData)
        {
            List<MsbDepandentData> result = null;
            try
            {
                DependentDA objDA = new DependentDA();
                result = objDA.GetEmpDetailByEmpID(objData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<MsbDepandentData> CheckMsbDependentEligibility(MsbDepandentData objData)
        {
            List<MsbDepandentData> result = null;
            try
            {
                DependentDA objDA = new DependentDA();
                result = objDA.CheckMsbDependentEligibility(objData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public int UpdateMsbDependent(MsbDepandentData objData)
        {
            int result = 0;
            try
            {
                DependentDA objDA = new DependentDA();
                result = objDA.UpdateMsbDependent(objData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<MsbDepandentData> GetDependentList(MsbDepandentData objData)
        {
            List<MsbDepandentData> result = null;
            try
            {
                DependentDA objDA = new DependentDA();
                result = objDA.GetDependentList(objData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public int DeleteMSBDependentByID(MsbDepandentData objData)
        {
            int result = 0;

            try
            {
                DependentDA objDA = new DependentDA();
                result = objDA.DeleteMSBDependentByID(objData);

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

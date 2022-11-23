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
    public class EmployeeTypeBO
    {
        public List<EmployeeTypeData> Getemplyeetypelist(EmployeeTypeData objbill)
        {
            List<EmployeeTypeData> result = null;
            try
            {
                EmployeeTypeDA objDA = new EmployeeTypeDA();
                result = objDA.Getemplyeetypelist(objbill);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public int UpdateEmployeeMSBeligibility(EmployeeTypeData objadmission)
        {
            int result = 0;
            try
            {
                EmployeeTypeDA objDA = new EmployeeTypeDA();
                result = objDA.UpdateEmployeeMSBeligibility(objadmission);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }

        public int UpdateEmployeeMSBapplicability(EmployeeDependentData objData)
        {
            int result = 0;
            try
            {
                EmployeeTypeDA objDA = new EmployeeTypeDA();
                result = objDA.UpdateEmployeeMSBapplicability(objData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<EmployeeDependentData> GetMsbApplicability()
        {
            List<EmployeeDependentData> result = null;
            try
            {
                EmployeeTypeDA objDA = new EmployeeTypeDA();
                result = objDA.GetMsbApplicability();
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<BenefitData> GetMsbBenefit()
        {
            List<BenefitData> result = null;
            try
            {
                EmployeeTypeDA objDA = new EmployeeTypeDA();
                result = objDA.GetMsbBenefit();
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public int UpdateMsbBenefit(BenefitData objdata)
        {
            int result = 0;
            try
            {
                EmployeeTypeDA objDA = new EmployeeTypeDA();
                result = objDA.UpdateMsbBenefit(objdata);
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

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
    public class EmployeeBO
    {
        public int UpdateEmployeeDetails(EmployeeData objemp)
        {
            int result = 0;

            try
            {
                EmployeeDA objempientDA = new EmployeeDA();
                result = objempientDA.UpdateEmployeeDetails(objemp);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<EmployeeData> SearchEmployeetDetails(EmployeeData objemp)
        {
            List<EmployeeData> result = null;
            try
            {
                EmployeeDA objempientDA = new EmployeeDA();
                result = objempientDA.SearchEmployeetDetails(objemp);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int Updategenstockemployee(GenStockEmployeeData objemp)
        {
            int result = 0;

            try
            {
                EmployeeDA objempientDA = new EmployeeDA();
                result = objempientDA.Updategenstockemployee(objemp);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public int UpdateMedEmployees(MedStockEmployeeData objemp)
        {
            int result = 0;

            try
            {
                EmployeeDA objempientDA = new EmployeeDA();
                result = objempientDA.UpdateMedEmployees(objemp);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public int Updatemedstockemployee(MedStockEmployeeData objemp)
        {
            int result = 0;

            try
            {
                EmployeeDA objempientDA = new EmployeeDA();
                result = objempientDA.Updatemedstockemployee(objemp);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public int UpdategenstockemployeeRequestEnable(GenStockEmployeeData objemp)
        {
            int result = 0;

            try
            {
                EmployeeDA objempientDA = new EmployeeDA();
                result = objempientDA.UpdategenstockemployeeRequestEnable(objemp);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public int UpdatemedstockemployeeRequestEnable(MedStockEmployeeData objemp)
        {
            int result = 0;

            try
            {
                EmployeeDA objempientDA = new EmployeeDA();
                result = objempientDA.UpdatemedstockemployeeRequestEnable(objemp);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public int UpdategenstockemployeeVerifyEnable(GenStockEmployeeData objemp)
        {
            int result = 0;

            try
            {
                EmployeeDA objempientDA = new EmployeeDA();
                result = objempientDA.UpdategenstockemployeeVerifyEnable(objemp);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public int UpdatemedstockemployeeVerifyEnable(MedStockEmployeeData objemp)
        {
            int result = 0;

            try
            {
                EmployeeDA objempientDA = new EmployeeDA();
                result = objempientDA.UpdatemedstockemployeeVerifyEnable(objemp);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public int UpdategenstockemployeeApproveEnable(GenStockEmployeeData objemp)
        {
            int result = 0;

            try
            {
                EmployeeDA objempientDA = new EmployeeDA();
                result = objempientDA.UpdategenstockemployeeApproveEnable(objemp);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public int UpdatemedstockemployeeApproveEnable(MedStockEmployeeData objemp)
        {
            int result = 0;

            try
            {
                EmployeeDA objempientDA = new EmployeeDA();
                result = objempientDA.UpdatemedstockemployeeApproveEnable(objemp);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public int UpdategenstockemployeeHandoverEnable(GenStockEmployeeData objemp)
        {
            int result = 0;

            try
            {
                EmployeeDA objempientDA = new EmployeeDA();
                result = objempientDA.UpdategenstockemployeeHandoverEnable(objemp);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public int UpdatemedstockemployeeHandoverEnable(MedStockEmployeeData objemp)
        {
            int result = 0;

            try
            {
                EmployeeDA objempientDA = new EmployeeDA();
                result = objempientDA.UpdatemedstockemployeeHandoverEnable(objemp);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<GenStockEmployeeData> GetGenStockEmployees(GenStockEmployeeData objemp)
        {
            List<GenStockEmployeeData> result = null;
            try
            {
                EmployeeDA objempientDA = new EmployeeDA();
                result = objempientDA.GetGenStockEmployees(objemp);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<MedStockEmployeeData> GetMedStockEmployees(MedStockEmployeeData objemp)
        {
            List<MedStockEmployeeData> result = null;
            try
            {
                EmployeeDA objempientDA = new EmployeeDA();
                result = objempientDA.GetMedStockEmployees(objemp);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<EmployeeData> SearchEmployeeDetails(EmployeeData objemp)
        {

            List<EmployeeData> result = null;

            try
            {
                EmployeeDA objempientDA = new EmployeeDA();
                result = objempientDA.SearchEmployeeDetails(objemp);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;


        }

        public List<EmployeeData> GetEmployeeNo(EmployeeData objD)
        {
            List<EmployeeData> result = null;
            try
            {
                EmployeeDA objempientDA = new EmployeeDA();
                result = objempientDA.GetEmployeeNo(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<EmployeeData> GetEmployeeName(EmployeeData objD)
        {
            List<EmployeeData> result = null;
            try
            {
                EmployeeDA objempientDA = new EmployeeDA();
                result = objempientDA.GetEmployeeName(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<EmployeeData> GetNurseName(EmployeeData objD)
        {
            List<EmployeeData> result = null;
            try
            {
                EmployeeDA objempientDA = new EmployeeDA();
                result = objempientDA.GetNurseName(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }

        public List<EmployeeData> GetEmpdetails(EmployeeData objD)
        {
            List<EmployeeData> result = null;
            try
            {
                EmployeeDA objempientDA = new EmployeeDA();
                result = objempientDA.GetEmpdetails(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<EmployeeData> GetdiscountBy(EmployeeData objD)
        {
            List<EmployeeData> result = null;
            try
            {
                EmployeeDA objempientDA = new EmployeeDA();
                result = objempientDA.GetdiscountBy(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<EmployeeData> GetContactno(EmployeeData objD)
        {
            List<EmployeeData> result = null;
            try
            {
                EmployeeDA objempientDA = new EmployeeDA();
                result = objempientDA.GetContactno(objD);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }

        public int DeleteEmployeeDetailsByID(EmployeeData objstd)
        {
            int result = 0;

            try
            {
                EmployeeDA objempientDA = new EmployeeDA();
                result = objempientDA.DeleteEmployeeDetailsByID(objstd);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<EmployeeData> GetEmployeeDetailbyID(EmployeeData objemp)
        {

            List<EmployeeData> result = null;

            try
            {
                EmployeeDA objempientDA = new EmployeeDA();
                result = objempientDA.GetEmployeeDetailbyID(objemp);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public List<EmployeeData> GetEmployeeList(EmployeeData objemp)
        {

            List<EmployeeData> result = null;

            try
            {
                EmployeeDA objempientDA = new EmployeeDA();
                result = objempientDA.GetEmployeeList(objemp);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;


        }
        public List<EmployeeData> GetDependentList(EmployeeData objemp)
        {

            List<EmployeeData> result = null;

            try
            {
                EmployeeDA objempientDA = new EmployeeDA();
                result = objempientDA.GetDependentList(objemp);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;


        }
        public int UpdateEmployeeDependent(EmployeeData objemp)
        {
            int result = 0;

            try
            {
                EmployeeDA objempientDA = new EmployeeDA();
                result = objempientDA.UpdateEmployeeDependent(objemp);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }
        public int UpdateEmployeeDiscount(EmployeeData objemp)
        {
            int result = 0;

            try
            {
                EmployeeDA objempientDA = new EmployeeDA();
                result = objempientDA.UpdateEmployeeDiscount(objemp);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;

        }

        public int UpdateEmpRole(EmployeeData objData)
        {
            int result = 0;
            try
            {
                EmployeeDA objDA = new EmployeeDA();
                result = objDA.UpdateEmpRole(objData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<EmployeeData> SearchEmployeeRoleDetails(EmployeeData objemp)
        {

            List<EmployeeData> result = null;

            try
            {
                EmployeeDA objempientDA = new EmployeeDA();
                result = objempientDA.SearchEmployeeRoleDetails(objemp);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;


        }
        public List<EmployeeData> SearchEmployeeRoleExcel(EmployeeData objemp)
        {

            List<EmployeeData> result = null;

            try
            {
                EmployeeDA objempientDA = new EmployeeDA();
                result = objempientDA.SearchEmployeeRoleExcel(objemp);

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

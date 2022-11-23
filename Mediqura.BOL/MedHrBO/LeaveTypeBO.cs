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
    public class LeaveTypeBO
    {
        public int UpdateLeaveTypeDetails(LeaveTypeData objLeaveMSTData)
        {
            int result = 0;
            try
            {
                LeaveTypeDA objLeaveMSTDA = new LeaveTypeDA();
                result = objLeaveMSTDA.UpdateLeaveTypeDetails(objLeaveMSTData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<LeaveTypeData> SearchLeaveTypeDetails(LeaveTypeData objLeaveMSTData)
        {
            List<LeaveTypeData> result = null;
            try
            {
                LeaveTypeDA objLeaveMSTDA = new LeaveTypeDA();
                result = objLeaveMSTDA.SearchLeaveTypeDetails(objLeaveMSTData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public List<LeaveTypeData> GetLeaveTypeDetailsByID(LeaveTypeData objLeaveMSTData)
        {
            List<LeaveTypeData> result = null;

            try
            {
                LeaveTypeDA objLeaveMSTDA = new LeaveTypeDA();
                result = objLeaveMSTDA.GetLeaveTypeDetailsByID(objLeaveMSTData);

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int DeleteLeaveTypeDetailsByID(LeaveTypeData objLeaveMSTData)
        {
            int result = 0;
            try
            {
                LeaveTypeDA objLeaveMSTDA = new LeaveTypeDA();
                result = objLeaveMSTDA.DeleteLeaveTypeDetailsByID(objLeaveMSTData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }
        public int UpdateLeaveDetailsList(LeaveTypeData objLeaveMSTData)
        {
            int result = 0;
            try
            {
                LeaveTypeDA objLeaveMSTDA = new LeaveTypeDA();
                result = objLeaveMSTDA.UpdateLeaveDetailsList(objLeaveMSTData);
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

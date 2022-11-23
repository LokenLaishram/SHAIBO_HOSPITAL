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
    public class LeaveApplicationBO
    {
		public List<LeaveApplicationData> GetLeaveApproverByDeptID(Int64 ID)
		{
			List<LeaveApplicationData> ApproverLists = null;
			try
			{
				LeaveApplicationDA objLeaveDA = new LeaveApplicationDA();
				List<LeaveApplicationData> ApproverList = objLeaveDA.GetLeaveApproverByDeptID(ID);
				ApproverLists = ApproverList;
			}
			catch (Exception ex) //Exception of the business layer(itself)//unhandle
			{
				PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "330001");
				LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
				throw new BusinessProcessException("4000001", ex);
			}
			return ApproverLists;
		}

        public List<LeaveApplicationData> GetEmployeeLeaveDetailsByID(LeaveApplicationData objLeaveData)
        {
            List<LeaveApplicationData> result = null;
            try
            {
                LeaveApplicationDA objLeaveDA = new LeaveApplicationDA();
                result = objLeaveDA.GetEmployeeLeaveDetailsByID(objLeaveData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }

		public List<LeaveApplicationData> UpdateLeaveDetails(LeaveApplicationData objleaveData)
        {
			List<LeaveApplicationData> result = null;
            try
            {
                LeaveApplicationDA objLeaveDA = new LeaveApplicationDA();
                result = objLeaveDA.UpdateLeaveDetails(objleaveData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }

        public List<LeaveApplicationData> GetLeaveRecord(LeaveApplicationData objleaveData)
        {
            List<LeaveApplicationData> result = null;
            try
            {
                LeaveApplicationDA objLeaveDA = new LeaveApplicationDA();
                result = objLeaveDA.GetLeaveRecord(objleaveData);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }

		public List<LeaveApplicationData> GetLeaveRecordDetails(LeaveApplicationData objleaveData)
		{
			List<LeaveApplicationData> result = null;
			try
			{
				LeaveApplicationDA objLeaveDA = new LeaveApplicationDA();
				result = objLeaveDA.GetLeaveRecordDetails(objleaveData);
			}
			catch (Exception ex)
			{
				PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
				LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
				throw new BusinessProcessException("4000001", ex);
			}
			return result;
		}

        public List<LeaveApplicationData> GetEmployeeLeaveRecordByID(LeaveApplicationData objdata)
        {
            List<LeaveApplicationData> result = null;
            try
            {
                LeaveApplicationDA objLeaveDA = new LeaveApplicationDA();
                result = objLeaveDA.GetEmployeeLeaveRecordByID(objdata);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;
        }

		public List<LeaveApplicationData> DeleteEmployeeLeaveRecordByID(LeaveApplicationData objdata)
        {
			List<LeaveApplicationData> result = null;
            try
            {
                LeaveApplicationDA objLeaveDA = new LeaveApplicationDA();
                result = objLeaveDA.DeleteEmployeeLeaveRecordByID(objdata);
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
                throw new BusinessProcessException("4000001", ex);
            }
            return result;   
        }

		public List<LeaveApplicationData> ApproveEmployeeLeaveRecordByID(LeaveApplicationData objdata)
		{
			List<LeaveApplicationData> result = null;
			try
			{
				LeaveApplicationDA objLeaveDA = new LeaveApplicationDA();
				result = objLeaveDA.ApproveEmployeeLeaveRecordByID(objdata);
			}
			catch (Exception ex)
			{
				PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
				LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
				throw new BusinessProcessException("4000001", ex);
			}
			return result;
		}

		public List<LeaveApplicationData> RejectEmployeeLeaveRecordByID(LeaveApplicationData objdata)
		{
			List<LeaveApplicationData> result = null;
			try
			{
				LeaveApplicationDA objLeaveDA = new LeaveApplicationDA();
				result = objLeaveDA.RejectEmployeeLeaveRecordByID(objdata);
			}
			catch (Exception ex)
			{
				PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "4000001");
				LogManager.LogMedError(ex, EnumErrorLogSourceTier.BO);
				throw new BusinessProcessException("4000001", ex);
			}
			return result;
		}

		public List<LeaveApplicationData> AdjustApproveleaveRecord(LeaveApplicationData objleaveData)
		{
			List<LeaveApplicationData> result = null;
			try
			{
				LeaveApplicationDA objLeaveDA = new LeaveApplicationDA();
				result = objLeaveDA.AdjustApproveleaveRecord(objleaveData);
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

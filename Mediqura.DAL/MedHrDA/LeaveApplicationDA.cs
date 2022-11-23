using Mediqura.CommonData.MedHrData;
using Mediqura.Utility;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using Mediqura.Utility.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediqura.DAL.MedHrDA
{
    public class LeaveApplicationDA
    {
        public List<LeaveApplicationData> GetEmployeeLeaveDetailsByID(LeaveApplicationData objleaveData)
        {
            List<LeaveApplicationData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[0].Value = objleaveData.EmployeeID;

                    arParms[1] = new SqlParameter("@LeaveID", SqlDbType.Int);
                    arParms[1].Value = objleaveData.LeaveID;

					
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetEmployeeLeaveDetailsByID", arParms);
                    List<LeaveApplicationData> lstLeaveDetails = ORHelper<LeaveApplicationData>.FromDataReaderToList(sqlReader);
                    result = lstLeaveDetails;
                }
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.DataAccessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
                throw new DataAccessException("5000001", ex);
            }
            return result;
        }

		public List<LeaveApplicationData> UpdateLeaveDetails(LeaveApplicationData objleaveData)
        {
			List<LeaveApplicationData> result = null;
            try
            {

                SqlParameter[] arParms = new SqlParameter[18];

                
                arParms[0] = new SqlParameter("@LeaveID", SqlDbType.Int);
                arParms[0].Value = objleaveData.LeaveID;

                arParms[1] = new SqlParameter("@datefrom", SqlDbType.DateTime);
                arParms[1].Value = objleaveData.datefrom;

                arParms[2] = new SqlParameter("@dateto", SqlDbType.DateTime);
                arParms[2].Value = objleaveData.dateto;

                arParms[3] = new SqlParameter("@LeaveCategoryID", SqlDbType.Int);
                arParms[3].Value = objleaveData.LeaveCategoryID;

                arParms[4] = new SqlParameter("@Leavereason", SqlDbType.VarChar);
                arParms[4].Value = objleaveData.Leavereason;
                
                arParms[5] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                arParms[5].Value = objleaveData.EmployeeID;

				arParms[6] = new SqlParameter("@DepartmentID", SqlDbType.Int);
				arParms[6].Value = objleaveData.DepartmentID;

                arParms[7] = new SqlParameter("@HospitalID", SqlDbType.Int);
                arParms[7].Value = objleaveData.HospitalID;

                arParms[8] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                arParms[8].Value = objleaveData.FinancialYearID;

                arParms[9] = new SqlParameter("@ActionType", SqlDbType.SmallInt);
                arParms[9].Value = objleaveData.ActionType;

                arParms[10] = new SqlParameter("@AddedBy", SqlDbType.VarChar);
                arParms[10].Value = objleaveData.AddedBy;

                arParms[11] = new SqlParameter("@LeaveRecordID", SqlDbType.BigInt);
                arParms[11].Value = objleaveData.LeaveRecordID;

				arParms[12] = new SqlParameter("@Noofdays", SqlDbType.Float);
				arParms[12].Value = objleaveData.Noofdays;

				arParms[13] = new SqlParameter("@Leaveconsumed", SqlDbType.Float);
				arParms[13].Value = objleaveData.Leaveconsumed;

				arParms[14] = new SqlParameter("@Leaveavailable", SqlDbType.Float);
				arParms[14].Value = objleaveData.Leaveavailable;

				arParms[15] = new SqlParameter("@CC_TO", SqlDbType.VarChar);
				arParms[15].Value = objleaveData.CC_TO;

				arParms[16] = new SqlParameter("@CC_IDs", SqlDbType.VarChar);
				arParms[16].Value = objleaveData.CC_IDs;

				arParms[17] = new SqlParameter("@Remarks", SqlDbType.VarChar);
				arParms[17].Value = objleaveData.Remarks;
				

				SqlDataReader sqlReader = null;
				sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_UpdateEmployeeLeaveDetails", arParms);
				List<LeaveApplicationData> lstLeaveDetails = ORHelper<LeaveApplicationData>.FromDataReaderToList(sqlReader);
				result = lstLeaveDetails;

               
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.DataAccessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
                throw new DataAccessException("5000001", ex);
            }
            return result;
        }

        public List<LeaveApplicationData> GetLeaveRecord(LeaveApplicationData objleaveData)
        {
             List<LeaveApplicationData> result = null;
            try
            {

                SqlParameter[] arParms = new SqlParameter[12];

                
                arParms[0] = new SqlParameter("@LeaveID", SqlDbType.Int);
                arParms[0].Value = objleaveData.LeaveID;

                arParms[1] = new SqlParameter("@datefrom", SqlDbType.DateTime);
                arParms[1].Value = objleaveData.datefrom;

                arParms[2] = new SqlParameter("@dateto", SqlDbType.DateTime);
                arParms[2].Value = objleaveData.dateto;

                arParms[3] = new SqlParameter("@LeaveCategoryID", SqlDbType.Int);
                arParms[3].Value = objleaveData.LeaveCategoryID;

                arParms[4] = new SqlParameter("@Leavereason", SqlDbType.VarChar);
                arParms[4].Value = objleaveData.Leavereason;
                
                arParms[5] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                arParms[5].Value = objleaveData.EmployeeID;
                                                             
                arParms[6] = new SqlParameter("@HospitalID", SqlDbType.Int);
                arParms[6].Value = objleaveData.HospitalID;

                arParms[7] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                arParms[7].Value = objleaveData.FinancialYearID;

                arParms[8] = new SqlParameter("@SearchType", SqlDbType.Int);
                arParms[8].Value = objleaveData.SearchType;

				arParms[9] = new SqlParameter("@LeaveEmployeeID", SqlDbType.BigInt);
				arParms[9].Value = objleaveData.LeaveEmployeeID;

				arParms[10] = new SqlParameter("@leaveaction", SqlDbType.Int);
				arParms[10].Value = objleaveData.leaveaction;

				arParms[11] = new SqlParameter("@LeaveRecordID", SqlDbType.BigInt);
				arParms[11].Value = objleaveData.LeaveRecordID;

                SqlDataReader sqlReader = null;
                sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetEmployeeLeaveRecord", arParms);
                List<LeaveApplicationData> lstresult = ORHelper<LeaveApplicationData>.FromDataReaderToList(sqlReader);
                result = lstresult;
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.DataAccessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
                throw new DataAccessException("5000001", ex);
            }
            return result;
        }

		public List<LeaveApplicationData> GetLeaveRecordDetails(LeaveApplicationData objleaveData)
		{
			List<LeaveApplicationData> result = null;
			try
			{

				SqlParameter[] arParms = new SqlParameter[2];


				arParms[0] = new SqlParameter("@LeaveRecordID", SqlDbType.BigInt);
				arParms[0].Value = objleaveData.LeaveRecordID;


				arParms[1] = new SqlParameter("@LeaveEmployeeID", SqlDbType.BigInt);
				arParms[1].Value = objleaveData.LeaveEmployeeID;


				SqlDataReader sqlReader = null;
				sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetEmployeeLeaveRecordDetails", arParms);
				List<LeaveApplicationData> lstresult = ORHelper<LeaveApplicationData>.FromDataReaderToList(sqlReader);
				result = lstresult;
			}
			catch (Exception ex) //Exception of the business layer(itself)//unhandle
			{
				PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.DataAccessExceptionPolicy, ex, "330001");
				LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
				throw new DataAccessException("5000001", ex);
			}
			return result;
		}


        public List<LeaveApplicationData> GetEmployeeLeaveRecordByID(LeaveApplicationData objdata)
        {
            List<LeaveApplicationData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[0].Value = objdata.EmployeeID;

                    arParms[1] = new SqlParameter("@LeaveRecordID", SqlDbType.BigInt);
                    arParms[1].Value = objdata.LeaveRecordID;


                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetEmployeeLeaveRecordByID", arParms);
                    List<LeaveApplicationData> lstLeaveDetails = ORHelper<LeaveApplicationData>.FromDataReaderToList(sqlReader);
                    result = lstLeaveDetails;
                }
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.DataAccessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
                throw new DataAccessException("5000001", ex);
            }
            return result;
        }

		public List<LeaveApplicationData> DeleteEmployeeLeaveRecordByID(LeaveApplicationData objdata)
        {
			List<LeaveApplicationData> result = null;
            try
            {

                SqlParameter[] arParms = new SqlParameter[3];


                arParms[0] = new SqlParameter("@LeaveRecordID", SqlDbType.BigInt);
                arParms[0].Value = objdata.LeaveRecordID;

                arParms[1] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                arParms[1].Value = objdata.EmployeeID;

                arParms[2] = new SqlParameter("@Remarks", SqlDbType.VarChar);
                arParms[2].Value = objdata.Remarks;


				SqlDataReader sqlReader = null;
				sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_DeleteEmployeeLeaveRecordByID", arParms);
				List<LeaveApplicationData> lstLeaveDetails = ORHelper<LeaveApplicationData>.FromDataReaderToList(sqlReader);
				result = lstLeaveDetails;

               
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.DataAccessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
                throw new DataAccessException("5000001", ex);
            }
            return result;
        }

		public List<LeaveApplicationData> ApproveEmployeeLeaveRecordByID(LeaveApplicationData objdata)
		{
			List<LeaveApplicationData> result = null;
			try
			{

				SqlParameter[] arParms = new SqlParameter[4];


				arParms[0] = new SqlParameter("@LeaveRecordID", SqlDbType.BigInt);
				arParms[0].Value = objdata.LeaveRecordID;

				arParms[1] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
				arParms[1].Value = objdata.EmployeeID;

				arParms[2] = new SqlParameter("@Remarks", SqlDbType.VarChar);
				arParms[2].Value = objdata.Remarks;

				arParms[3] = new SqlParameter("@LeaveEmployeeID", SqlDbType.BigInt);
				arParms[3].Value = objdata.LeaveEmployeeID;

				SqlDataReader sqlReader = null;
				sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_ApproveEmployeeLeaveRecordByID", arParms);
				List<LeaveApplicationData> lstLeaveDetails = ORHelper<LeaveApplicationData>.FromDataReaderToList(sqlReader);
				result = lstLeaveDetails;
			}
			catch (Exception ex) //Exception of the business layer(itself)//unhandle
			{
				PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.DataAccessExceptionPolicy, ex, "330001");
				LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
				throw new DataAccessException("5000001", ex);
			}
			return result;
		}

		public List<LeaveApplicationData> RejectEmployeeLeaveRecordByID(LeaveApplicationData objdata)
		{
			List<LeaveApplicationData> result = null;
			try
			{

				SqlParameter[] arParms = new SqlParameter[3];


				arParms[0] = new SqlParameter("@LeaveRecordID", SqlDbType.BigInt);
				arParms[0].Value = objdata.LeaveRecordID;

				arParms[1] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
				arParms[1].Value = objdata.EmployeeID;

				arParms[2] = new SqlParameter("@Remarks", SqlDbType.VarChar);
				arParms[2].Value = objdata.Remarks;


				SqlDataReader sqlReader = null;
				sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_RejectEmployeeLeaveRecordByID", arParms);
				List<LeaveApplicationData> lstLeaveDetails = ORHelper<LeaveApplicationData>.FromDataReaderToList(sqlReader);
				result = lstLeaveDetails;


			}
			catch (Exception ex) //Exception of the business layer(itself)//unhandle
			{
				PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.DataAccessExceptionPolicy, ex, "330001");
				LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
				throw new DataAccessException("5000001", ex);
			}
			return result;
		}

		public List<LeaveApplicationData> AdjustApproveleaveRecord(LeaveApplicationData objleaveData)
		{
			List<LeaveApplicationData> result = null;
			try
			{

				SqlParameter[] arParms = new SqlParameter[7];


				arParms[0] = new SqlParameter("@XMLadjustApproveleave", SqlDbType.Xml);
				arParms[0].Value = objleaveData.XMLadjustApproveleave;

				arParms[1] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
				arParms[1].Value = objleaveData.EmployeeID;

				arParms[2] = new SqlParameter("@Remarks", SqlDbType.VarChar);
				arParms[2].Value = objleaveData.Remarks;

				arParms[3] = new SqlParameter("@LeaveAdjustedtypes", SqlDbType.VarChar);
				arParms[3].Value = objleaveData.LeaveAdjustedtypes;

				arParms[4] = new SqlParameter("@LeaveAdjustcategory", SqlDbType.VarChar);
				arParms[4].Value = objleaveData.LeaveAdjustcategory;

				arParms[5] = new SqlParameter("@LeaveEmployeeID", SqlDbType.BigInt);
				arParms[5].Value = objleaveData.LeaveEmployeeID;

				arParms[6] = new SqlParameter("@LeaveRecordID", SqlDbType.BigInt);
				arParms[6].Value = objleaveData.LeaveRecordID;

				SqlDataReader sqlReader = null;
				sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Employee_AdjustApproveleaveRecord", arParms);
				List<LeaveApplicationData> lstLeaveDetails = ORHelper<LeaveApplicationData>.FromDataReaderToList(sqlReader);
				result = lstLeaveDetails;

				
			}
			catch (Exception ex) //Exception of the business layer(itself)//unhandle
			{
				PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.DataAccessExceptionPolicy, ex, "330001");
				LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
				throw new DataAccessException("5000001", ex);
			}
			return result;
		}


		public List<LeaveApplicationData> GetLeaveApproverByDeptID(Int64 ID)
		{
			List<LeaveApplicationData> ApproverLists = null;
			try
			{
				SqlParameter[] arParms = new SqlParameter[1];
				arParms[0] = new SqlParameter("@ID", SqlDbType.BigInt);
				arParms[0].Value = ID;



				SqlDataReader sqlReader = null;
				sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Employee_GetLeaveApproverID", arParms);
				ApproverLists = ORHelper<LeaveApplicationData>.FromDataReaderToList(sqlReader);
				sqlReader.Close();
			}
			catch (Exception ex) //Exception of the business layer(itself)//unhandle
			{
				PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "330001");
				LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
				throw new BusinessProcessException("5000001", ex);
			}
			return ApproverLists;
		}
	}
}

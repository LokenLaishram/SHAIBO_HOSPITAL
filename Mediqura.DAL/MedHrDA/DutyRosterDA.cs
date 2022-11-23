using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mediqura.CommonData.MedHrData;
using Mediqura.Utility;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using System.Data.SqlClient;
using System.Data;
using Mediqura.Utility.Util;
namespace Mediqura.DAL.MedHrDA
{
	public class DutyRosterDA
	{
		public List<DutyRosterData> SearchDutyRoster(DutyRosterData objdutyData)
		{
			List<DutyRosterData> result = null;
			try
			{
				{
					SqlParameter[] arParms = new SqlParameter[3];

					arParms[0] = new SqlParameter("@DepartmentID", SqlDbType.Int);
					arParms[0].Value = objdutyData.DepartmentID;

					arParms[1] = new SqlParameter("@Month", SqlDbType.Int);
					arParms[1].Value = objdutyData.Month;

					arParms[2] = new SqlParameter("@Year", SqlDbType.Int);
					arParms[2].Value = objdutyData.Year;

					SqlDataReader sqlReader = null;
					sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetEmployeeDutyRosterDetails", arParms);
					List<DutyRosterData> listrosterdetails = ORHelper<DutyRosterData>.FromDataReaderToList(sqlReader);
					result = listrosterdetails;
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

		public int UpdateDutyRoster(DutyRosterData objdutyData)
		{
			int result = 0;
			try
			{
				{
					SqlParameter[] arParms = new SqlParameter[8];

					arParms[0] = new SqlParameter("@XMLEmployeeDuty", SqlDbType.Xml);
					arParms[0].Value = objdutyData.XMLEmployeeDuty;

					
					arParms[1] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
					arParms[1].Value = objdutyData.EmployeeID;

					arParms[2] = new SqlParameter("@HospitalID", SqlDbType.Int);
					arParms[2].Value = objdutyData.HospitalID;

					
					arParms[3] = new SqlParameter("@Output", SqlDbType.SmallInt);
					arParms[3].Direction = ParameterDirection.Output;

					
					arParms[4] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
					arParms[4].Value = objdutyData.IPaddress;

					arParms[5] = new SqlParameter("@DepartmentID", SqlDbType.Int);
					arParms[5].Value = objdutyData.DepartmentID;

					arParms[6] = new SqlParameter("@Month", SqlDbType.Int);
					arParms[6].Value = objdutyData.Month;

					arParms[7] = new SqlParameter("@Year", SqlDbType.Int);
					arParms[7].Value = objdutyData.Year;


					int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_UpdateEmployeesDutyRoster", arParms);
					if (result_ > 0 || result_ == -1)
						result = Convert.ToInt32(arParms[3].Value);
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

		public int UpdateEmployeesDutySchedule(DutyRosterData objdutyData)
		{
			int result = 0;
			try
			{
				{
					SqlParameter[] arParms = new SqlParameter[7];
					

					arParms[0] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
					arParms[0].Value = objdutyData.EmployeeID;

					arParms[1] = new SqlParameter("@HospitalID", SqlDbType.Int);
					arParms[1].Value = objdutyData.HospitalID;


					arParms[2] = new SqlParameter("@Output", SqlDbType.SmallInt);
					arParms[2].Direction = ParameterDirection.Output;


					arParms[3] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
					arParms[3].Value = objdutyData.IPaddress;

					arParms[4] = new SqlParameter("@DepartmentID", SqlDbType.Int);
					arParms[4].Value = objdutyData.DepartmentID;

					arParms[5] = new SqlParameter("@Month", SqlDbType.Int);
					arParms[5].Value = objdutyData.Month;

					arParms[6] = new SqlParameter("@Year", SqlDbType.Int);
					arParms[6].Value = objdutyData.Year;


					int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_UpdateEmployeesDutySchedule", arParms);
					if (result_ > 0 || result_ == -1)
						result = Convert.ToInt32(arParms[2].Value);
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
	}
	}


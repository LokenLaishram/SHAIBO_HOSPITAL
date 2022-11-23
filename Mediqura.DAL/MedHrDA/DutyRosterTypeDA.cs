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
	public class DutyRosterTypeDA
	{
		public int UpdateDutyRosterType(DutyRosterTypeData objdutyData)
		{
			int result = 0;
			try
			{
				{
					SqlParameter[] arParms = new SqlParameter[30];

					arParms[0] = new SqlParameter("@RosterID", SqlDbType.Int);
					arParms[0].Value = objdutyData.RosterID;

					arParms[1] = new SqlParameter("@RosterCode", SqlDbType.VarChar);
					arParms[1].Value = objdutyData.RosterCode;

					arParms[2] = new SqlParameter("@RosterDescp", SqlDbType.VarChar);
					arParms[2].Value = objdutyData.RosterDescp;

					arParms[3] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
					arParms[3].Value = objdutyData.EmployeeID;

					arParms[4] = new SqlParameter("@HospitalID", SqlDbType.Int);
					arParms[4].Value = objdutyData.HospitalID;

					arParms[5] = new SqlParameter("@ActionType", SqlDbType.Int);
					arParms[5].Value = objdutyData.ActionType;

					arParms[6] = new SqlParameter("@Output", SqlDbType.SmallInt);
					arParms[6].Direction = ParameterDirection.Output;

					arParms[7] = new SqlParameter("@IsActive", SqlDbType.Bit);
					arParms[7].Value = objdutyData.IsActive;

					arParms[8] = new SqlParameter("@IPAddress", SqlDbType.VarChar);
					arParms[8].Value = objdutyData.IPaddress;

					arParms[9] = new SqlParameter("@ShiftTypeID", SqlDbType.Int);
					arParms[9].Value = objdutyData.ShiftTypeID;

					arParms[10] = new SqlParameter("@Shift_I_SummerStartTime", SqlDbType.DateTime);
					arParms[10].Value = objdutyData.Shift_I_SummerStartTime;

					arParms[11] = new SqlParameter("@Shift_II_SummerStartTime", SqlDbType.DateTime);
					arParms[11].Value = objdutyData.Shift_II_SummerStartTime;

					arParms[12] = new SqlParameter("@Shift_I_SummerEndTime", SqlDbType.DateTime);
					arParms[12].Value = objdutyData.Shift_I_SummerEndTime;

					arParms[13] = new SqlParameter("@Shift_II_SummerEndTime", SqlDbType.DateTime);
					arParms[13].Value = objdutyData.Shift_II_SummerEndTime;

					arParms[14] = new SqlParameter("@Shift_I_SummerNextDay", SqlDbType.Int);
					arParms[14].Value = objdutyData.Shift_I_SummerNextDay;

					arParms[15] = new SqlParameter("@Shift_II_SummerNextDay", SqlDbType.Int);
					arParms[15].Value = objdutyData.Shift_II_SummerNextDay;

					arParms[16] = new SqlParameter("@Shift_I_SummerInrelaxation", SqlDbType.DateTime);
					arParms[16].Value = objdutyData.Shift_I_SummerInrelaxation;

					arParms[17] = new SqlParameter("@Shift_II_SummerInrelaxation", SqlDbType.DateTime);
					arParms[17].Value = objdutyData.Shift_II_SummerInrelaxation;

					arParms[18] = new SqlParameter("@Shift_I_SummerOutrelaxation", SqlDbType.DateTime);
					arParms[18].Value = objdutyData.Shift_I_SummerOutrelaxation;

					arParms[19] = new SqlParameter("@Shift_II_SummerOutrelaxation", SqlDbType.DateTime);
					arParms[19].Value = objdutyData.Shift_II_SummerOutrelaxation;

					arParms[20] = new SqlParameter("@Shift_I_WinterStartTime", SqlDbType.DateTime);
					arParms[20].Value = objdutyData.Shift_I_WinterStartTime;

					arParms[21] = new SqlParameter("@Shift_II_WinterStartTime", SqlDbType.DateTime);
					arParms[21].Value = objdutyData.Shift_II_WinterStartTime;

					arParms[22] = new SqlParameter("@Shift_I_WinterEndTime", SqlDbType.DateTime);
					arParms[22].Value = objdutyData.Shift_I_WinterEndTime;

					arParms[23] = new SqlParameter("@Shift_II_WinterEndTime", SqlDbType.DateTime);
					arParms[23].Value = objdutyData.Shift_II_WinterEndTime;

					arParms[24] = new SqlParameter("@Shift_I_WinterNextDay", SqlDbType.Int);
					arParms[24].Value = objdutyData.Shift_I_WinterNextDay;

					arParms[25] = new SqlParameter("@Shift_II_WinterNextDay", SqlDbType.Int);
					arParms[25].Value = objdutyData.Shift_II_WinterNextDay;

					arParms[26] = new SqlParameter("@Shift_I_WinterInrelaxation", SqlDbType.DateTime);
					arParms[26].Value = objdutyData.Shift_I_WinterInrelaxation;

					arParms[27] = new SqlParameter("@Shift_II_WinterInrelaxation", SqlDbType.DateTime);
					arParms[27].Value = objdutyData.Shift_II_WinterInrelaxation;

					arParms[28] = new SqlParameter("@Shift_I_WinterOutrelaxation", SqlDbType.DateTime);
					arParms[28].Value = objdutyData.Shift_I_WinterOutrelaxation;

					arParms[29] = new SqlParameter("@Shift_II_WinterOutrelaxation", SqlDbType.DateTime);
					arParms[29].Value = objdutyData.Shift_II_WinterOutrelaxation;

					int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Employees_UpdateRosterTypeMST", arParms);
					if (result_ > 0 || result_ == -1)
						result = Convert.ToInt32(arParms[6].Value);
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

		public List<DutyRosterTypeData> SearchRosterType(DutyRosterTypeData objdutyData)
		{
			List<DutyRosterTypeData> result = null;
			try
			{
				{
					SqlParameter[] arParms = new SqlParameter[3];

					arParms[0] = new SqlParameter("@IsActive", SqlDbType.Bit);
					arParms[0].Value = objdutyData.IsActive;

					arParms[1] = new SqlParameter("@RosterID", SqlDbType.Int);
					arParms[1].Value = objdutyData.RosterID;

					arParms[2] = new SqlParameter("@ShiftTypeID", SqlDbType.Int);
					arParms[2].Value = objdutyData.ShiftTypeID;

					SqlDataReader sqlReader = null;
					sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Employees_GetRosterTypeLIST", arParms);
					List<DutyRosterTypeData> listrosterdetails = ORHelper<DutyRosterTypeData>.FromDataReaderToList(sqlReader);
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
	}
}

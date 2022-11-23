using Mediqura.CommonData.MedUtilityData;
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

namespace Mediqura.DAL.MedUtilityDA
{
	public class SeasonTypeDA
	{
		public int UpdateSeasonDetails(SeasonTypeData objseason)
		{
			int result = 0;
			try
			{
				{
					SqlParameter[] arParms = new SqlParameter[15];

					arParms[0] = new SqlParameter("@SeasonID", SqlDbType.Int);
					arParms[0].Value = objseason.SeasonID;
					
					arParms[1] = new SqlParameter("@ActionType", SqlDbType.Int);
					arParms[1].Value = objseason.ActionType;

					arParms[2] = new SqlParameter("@Output", SqlDbType.SmallInt);
					arParms[2].Direction = ParameterDirection.Output;

					arParms[3] = new SqlParameter("@JanuaryID", SqlDbType.Int);
					arParms[3].Value = objseason.January;

					arParms[4] = new SqlParameter("@FebruaryID", SqlDbType.Int);
					arParms[4].Value = objseason.February;

					arParms[5] = new SqlParameter("@MarchID", SqlDbType.Int);
					arParms[5].Value = objseason.March;

					arParms[6] = new SqlParameter("@AprilID", SqlDbType.Int);
					arParms[6].Value = objseason.April;

					arParms[7] = new SqlParameter("@MayID", SqlDbType.Int);
					arParms[7].Value = objseason.May;

					arParms[8] = new SqlParameter("@JuneID", SqlDbType.Int);
					arParms[8].Value = objseason.June;

					arParms[9] = new SqlParameter("@JulyID", SqlDbType.Int);
					arParms[9].Value = objseason.July;

					arParms[10] = new SqlParameter("@AugustID", SqlDbType.Int);
					arParms[10].Value = objseason.August;

					arParms[11] = new SqlParameter("@SeptemberID", SqlDbType.Int);
					arParms[11].Value = objseason.September;

					arParms[12] = new SqlParameter("@OctoberID", SqlDbType.Int);
					arParms[12].Value = objseason.October;

					arParms[13] = new SqlParameter("@NovemberID", SqlDbType.Int);
					arParms[13].Value = objseason.November;

					arParms[14] = new SqlParameter("@DecemberID", SqlDbType.Int);
					arParms[14].Value = objseason.December;
										

					int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_UpdateSeasonDetailsMST", arParms);
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

		public List<SeasonTypeData> SearchSeasonDetails(SeasonTypeData objseason)
		{
			List<SeasonTypeData> result = null;
			try
			{
				{
					SqlParameter[] arParms = new SqlParameter[1];

					arParms[0] = new SqlParameter("@SeasonID", SqlDbType.Int);
					arParms[0].Value = objseason.SeasonID;
													
					
					SqlDataReader sqlReader = null;
					sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_SearchSeasonDetails", arParms);
					List<SeasonTypeData> lstSeasonDetails = ORHelper<SeasonTypeData>.FromDataReaderToList(sqlReader);
					result = lstSeasonDetails;
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

		public List<SeasonTypeData> GetSeasonDetailsByID(SeasonTypeData objseason)
		{
			List<SeasonTypeData> result = null;
			try
			{
				{
					SqlParameter[] arParms = new SqlParameter[1];

					arParms[0] = new SqlParameter("@SeasonID", SqlDbType.Int);
					arParms[0].Value = objseason.SeasonID;



					SqlDataReader sqlReader = null;
					sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_GetSeasonDetailsByID", arParms);
					List<SeasonTypeData> lstSeasonDetails = ORHelper<SeasonTypeData>.FromDataReaderToList(sqlReader);
					result = lstSeasonDetails;
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

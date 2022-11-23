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
	public class HRControllManagerDA
	{
		public List<HRControllManagerData> GetHRControlManagerList(HRControllManagerData objcontrols)
		{
			List<HRControllManagerData> result = null;
			try
			{
				{
					SqlParameter[] arParms = new SqlParameter[3];

					arParms[0] = new SqlParameter("@DepartmentID", SqlDbType.Int);
					arParms[0].Value = objcontrols.DepartmentID;

					arParms[1] = new SqlParameter("@DesignationID", SqlDbType.Int);
					arParms[1].Value = objcontrols.DesignationID;

					arParms[2] = new SqlParameter("@EmployeeTypeID", SqlDbType.Int);
					arParms[2].Value = objcontrols.EmployeeTypeID;

					SqlDataReader sqlReader = null;
					sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Employee_GetHRControlManagerList", arParms);
					List<HRControllManagerData> controlist = ORHelper<HRControllManagerData>.FromDataReaderToList(sqlReader);
					result = controlist;
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

		public int UpdateHRControlManagerList(HRControllManagerData objcontrol)
		{
			int result = 0;
			try
			{
				{
					SqlParameter[] arParms = new SqlParameter[2];

					arParms[0] = new SqlParameter("@ControllisttoXML", SqlDbType.Xml);
					arParms[0].Value = objcontrol.ControllisttoXML;
					
					arParms[1] = new SqlParameter("@Output", SqlDbType.BigInt);
					arParms[1].Direction = ParameterDirection.Output;

					int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Employee_UpdateHRControlManagerList", arParms);
					if (result_ > 0 || result_ == -1)
					{
						result = Convert.ToInt32(arParms[1].Value);
					}
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

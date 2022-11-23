using Mediqura.CommonData.AdmissionData;
using Mediqura.CommonData.MedHouseKeepingData;
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

namespace Mediqura.DAL.AdmissionDA
{
    public class PatAdmToWardDA
    {
        public List<PatAdmToWardData> GetPatAdmToWardDetails(PatAdmToWardData obj)
        {
            List<PatAdmToWardData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[4];

                    arParms[0] = new SqlParameter("@WardID", SqlDbType.Int);
                    arParms[0].Value = obj.WardID;

                    arParms[1] = new SqlParameter("@IPNo", SqlDbType.VarChar);
                    arParms[1].Value = obj.IPNo;

                    arParms[2] = new SqlParameter("@AdmToWardStatusID", SqlDbType.Int);
                    arParms[2].Value = obj.AdmToWardStatusID;

                    arParms[3] = new SqlParameter("@Date", SqlDbType.DateTime);
                    arParms[3].Value = obj.Date;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetPatAdmToWardDetails", arParms);
                    List<PatAdmToWardData> listdetails = ORHelper<PatAdmToWardData>.FromDataReaderToList(sqlReader);
                    result = listdetails;
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
        public int UpdateAdmittedToWardStatus(PatAdmToWardData obj)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[4];

                    arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                    arParms[0].Value = obj.ID;

                    arParms[1] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[1].Value = obj.EmployeeID;

                    arParms[2] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[2].Direction = ParameterDirection.Output;

                    arParms[3] = new SqlParameter("@AdmToWardStatusID", SqlDbType.Int);
                    arParms[3].Value = obj.AdmToWardStatusID;


                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_UpdateAdmittedToWardStatus", arParms);
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

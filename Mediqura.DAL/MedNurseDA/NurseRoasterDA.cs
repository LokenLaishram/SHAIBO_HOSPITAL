using Mediqura.CommonData.AdmissionData;
using Mediqura.CommonData.PatientData;
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
using Mediqura.CommonData.OTData;
using Mediqura.CommonData.MedNurseData;

namespace Mediqura.DAL.MedNurseDA
{
    public class NurseRoasterDA
    {
        public List<NurseRoasterData> GetSchedule(NurseRoasterData objSchedule)
        {
            List<NurseRoasterData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[6];

                    //arParms[0] = new SqlParameter("@nurseType", SqlDbType.Int);
                    //arParms[0].Value = objSchedule.nurseType;

                    arParms[0] = new SqlParameter("@nurseID", SqlDbType.Int);
                    arParms[0].Value = objSchedule.nurseID;

                    arParms[1] = new SqlParameter("@weekID", SqlDbType.Int);
                    arParms[1].Value = objSchedule.weekID;

                    arParms[2] = new SqlParameter("@monthID", SqlDbType.Int);
                    arParms[2].Value = objSchedule.monthID;

                    arParms[3] = new SqlParameter("@Year", SqlDbType.Int);
                    arParms[3].Value = objSchedule.yearID;

                    arParms[4] = new SqlParameter("@AddedBy", SqlDbType.BigInt);
                    arParms[4].Value = objSchedule.EmployeeID;

                    arParms[5] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[5].Value = objSchedule.HospitalID;



                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_NUR_Get_Rosterdetails", arParms);
                    List<NurseRoasterData> lstLabGroupTypeDetails = ORHelper<NurseRoasterData>.FromDataReaderToList(sqlReader);
                    result = lstLabGroupTypeDetails;
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
        public int UpdateRosterDetails(NurseRoasterData objAppmt)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[7];


                    arParms[0] = new SqlParameter("@week", SqlDbType.Int);
                    arParms[0].Value = objAppmt.weekID;

                    arParms[1] = new SqlParameter("@HospitaID", SqlDbType.BigInt);
                    arParms[1].Value = objAppmt.HospitalID;

                    arParms[2] = new SqlParameter("@MonthID", SqlDbType.Int);
                    arParms[2].Value = objAppmt.monthID;

                    arParms[3] = new SqlParameter("@Year", SqlDbType.Int);
                    arParms[3].Value = objAppmt.yearID;

                    arParms[4] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[4].Direction = ParameterDirection.Output;

                    arParms[5] = new SqlParameter("@XMLData", SqlDbType.Xml);
                    arParms[5].Value = objAppmt.XMLData;

                    arParms[6] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[6].Value = objAppmt.EmployeeID;


                    //arParms[0] = new SqlParameter("@NurseType", SqlDbType.VarChar);
                    //arParms[0].Value = objAppmt.Nursedesgn;


                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_util_UpdateNurseRoaster", arParms);
                    if (result_ > 0 || result_ == -1)
                        result = Convert.ToInt32(arParms[4].Value);
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

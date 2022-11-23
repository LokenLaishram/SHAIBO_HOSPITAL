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
   public class DoctorMasterDA
    {
       public List<DoctorMasterData> SearchDoctorByType(DoctorMasterData objDoctordata)
        {
            List<DoctorMasterData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@DoctorName", SqlDbType.VarChar);
                    arParms[0].Value = objDoctordata.DoctorName;

                    arParms[1] = new SqlParameter("@DoctorType", SqlDbType.Int);
                    arParms[1].Value = objDoctordata.DoctorType;


                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetDoctorListByType", arParms);
                    List<DoctorMasterData> lstDoctor = ORHelper<DoctorMasterData>.FromDataReaderToList(sqlReader);
                    result = lstDoctor;
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
       public List<DoctorMasterData> SearchDoctorByTypeAndDept(DoctorMasterData objDoctordata)
       {
           List<DoctorMasterData> result = null;
           try
           {
               {
                   SqlParameter[] arParms = new SqlParameter[3];

                   arParms[0] = new SqlParameter("@DoctorName", SqlDbType.VarChar);
                   arParms[0].Value = objDoctordata.DoctorName;

                   arParms[1] = new SqlParameter("@DoctorType", SqlDbType.Int);
                   arParms[1].Value = objDoctordata.DoctorType;

                   arParms[2] = new SqlParameter("@ID", SqlDbType.Int);
                   arParms[2].Value = objDoctordata.DepartmentID;

                   SqlDataReader sqlReader = null;
                   sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_SearchDoctorListByTypeNDept", arParms);
                   List<DoctorMasterData> lstDoctor = ORHelper<DoctorMasterData>.FromDataReaderToList(sqlReader);
                   result = lstDoctor;
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

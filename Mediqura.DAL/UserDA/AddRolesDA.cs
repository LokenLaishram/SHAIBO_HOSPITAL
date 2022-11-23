using Mediqura.CommonData.LoginData;
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

namespace Mediqura.DAL.UserDA
{
    public class AddRolesDA
    {
        public int UpdateRoleDetails(RolesData objrole)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[7];

                    arParms[0] = new SqlParameter("@RoleID", SqlDbType.Int);
                    arParms[0].Value = objrole.RoleID;

                    arParms[1] = new SqlParameter("@RoleCode", SqlDbType.VarChar);
                    arParms[1].Value = objrole.RoleCode;

                    arParms[2] = new SqlParameter("@RoleName", SqlDbType.VarChar);
                    arParms[2].Value = objrole.RoleName;

                    arParms[3] = new SqlParameter("@AddedBy", SqlDbType.VarChar);
                    arParms[3].Value = objrole.AddedBy;

                    arParms[4] = new SqlParameter("@UserId", SqlDbType.Int);
                    arParms[4].Value = objrole.EmployeeID;

                    arParms[5] = new SqlParameter("@CompanyID", SqlDbType.Int);
                    arParms[5].Value = objrole.CompanyID;

                    arParms[5] = new SqlParameter("@ActionType", SqlDbType.Int);
                    arParms[5].Value = objrole.ActionType;

                    arParms[6] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[6].Direction = ParameterDirection.Output;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_ADM_MDQ_UpdateRoleDetail", arParms);
                    if (result_ > 0 || result_ == -1)
                        result = Convert.ToInt32(arParms[6].Value);
                }
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.DataAccessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex);
                throw new DataAccessException("5000001", ex);
            }
            return result;
        }
        public List<RolesData> SearchRoleDetails(RolesData objrole)
        {
            List<RolesData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[5];

                    arParms[0] = new SqlParameter("@RoleCode", SqlDbType.VarChar);
                    arParms[0].Value = objrole.RoleCode;

                    arParms[1] = new SqlParameter("@RoleName", SqlDbType.VarChar);
                    arParms[1].Value = objrole.RoleName;

                    arParms[2] = new SqlParameter("@PageSize", SqlDbType.Int);
                    arParms[2].Value = objrole.PageSize;

                    arParms[3] = new SqlParameter("@CurrentIndex", SqlDbType.Int);
                    arParms[3].Value = objrole.CurrentIndex;

                    arParms[4] = new SqlParameter("@ActionType", SqlDbType.Int);
                    arParms[4].Value = objrole.ActionType;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_ADM_SearchRoleDetail", arParms);
                    List<RolesData> lstRoleDetails = ORHelper<RolesData>.FromDataReaderToList(sqlReader);
                    result = lstRoleDetails;
                }
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.DataAccessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex);
                throw new DataAccessException("5000001", ex);
            }
            return result;
        }
        public List<RolesData> GetRoleDetailsByID(RolesData objrole)
        {
            List<RolesData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@RoleID", SqlDbType.Int);
                    arParms[0].Value = objrole.RoleID;
                    arParms[1] = new SqlParameter("@ActionType", SqlDbType.Int);
                    arParms[1].Value = objrole.ActionType;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_ADM_EditRoleDetail", arParms);
                    List<RolesData> lstRoleDetails = ORHelper<RolesData>.FromDataReaderToList(sqlReader);
                    result = lstRoleDetails;
                }
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.DataAccessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex);
                throw new DataAccessException("5000001", ex);
            }
            return result;
        }
        public int DeleteRoleDetailsByID(RolesData objrole)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[3];

                    arParms[0] = new SqlParameter("@RoleID", SqlDbType.Int);
                    arParms[0].Value = objrole.RoleID;
                    arParms[1] = new SqlParameter("@ActionType", SqlDbType.Int);
                    arParms[1].Value = objrole.ActionType;
                    arParms[2] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[2].Direction = ParameterDirection.Output;
                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_ADM_DeleteRoleDetail", arParms);
                    if (result_ > 0 || result_ == -1)
                        result = Convert.ToInt32(arParms[2].Value);
                }
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.DataAccessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex);
                throw new DataAccessException("5000001", ex);
            }
            return result;
        }
    }
}

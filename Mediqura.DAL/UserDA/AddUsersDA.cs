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
    public class AddUsersDA
    {
        public int UpdateUserDetails(AddUsersData objusers)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[11];

                    arParms[0] = new SqlParameter("@LoginID", SqlDbType.Int);
                    arParms[0].Value = objusers.LoginID;

                    arParms[1] = new SqlParameter("@EmployeeID", SqlDbType.Int);
                    arParms[1].Value = objusers.EmployeeID;

                    arParms[2] = new SqlParameter("@UserName", SqlDbType.VarChar);
                    arParms[2].Value = objusers.UserName;

                    arParms[3] = new SqlParameter("@UserPassword", SqlDbType.VarChar);
                    arParms[3].Value = objusers.UserPassword;

                    arParms[4] = new SqlParameter("@RoleID", SqlDbType.Int);
                    arParms[4].Value = objusers.RoleID;

                    arParms[5] = new SqlParameter("@AddedBy", SqlDbType.VarChar);
                    arParms[5].Value = objusers.AddedBy;

                    arParms[6] = new SqlParameter("@CompanyID", SqlDbType.Int);
                    arParms[6].Value = objusers.CompanyID;

                    arParms[7] = new SqlParameter("@ActionType", SqlDbType.Int);
                    arParms[7].Value = objusers.ActionType;

                    arParms[8] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[8].Direction = ParameterDirection.Output;

                    arParms[9] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[9].Value = objusers.FinancialYearID;

                    arParms[10] = new SqlParameter("@Notification", SqlDbType.Int);
                    arParms[10].Value = objusers.enableNotification;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Adm_UpdateUserDetail", arParms);
                    if (result_ > 0 || result_ == -1)
                        result = Convert.ToInt32(arParms[8].Value);
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
        public List<AddUsersData> SearchUserDetails(AddUsersData objusers)
        {
            List<AddUsersData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[6];

                    arParms[0] = new SqlParameter("@EmployeeID", SqlDbType.Int);
                    arParms[0].Value = objusers.EmployeeID;

                    arParms[1] = new SqlParameter("@UserName", SqlDbType.VarChar);
                    arParms[1].Value = objusers.UserName;

                    arParms[2] = new SqlParameter("@RoleID", SqlDbType.Int);
                    arParms[2].Value = objusers.RoleID;

                    arParms[3] = new SqlParameter("@PageSize", SqlDbType.Int);
                    arParms[3].Value = objusers.PageSize;

                    arParms[4] = new SqlParameter("@CurrentIndex", SqlDbType.Int);
                    arParms[4].Value = objusers.CurrentIndex;

                    arParms[5] = new SqlParameter("@ActionType", SqlDbType.Int);
                    arParms[5].Value = objusers.ActionType;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_ADM_SearchUserDetail", arParms);
                    List<AddUsersData> lstUserDetails = ORHelper<AddUsersData>.FromDataReaderToList(sqlReader);
                    result = lstUserDetails;
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
        public List<AddUsersData> GetUserDetailsByID(AddUsersData objusers)
        {
            List<AddUsersData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@LoginID", SqlDbType.Int);
                    arParms[0].Value = objusers.LoginID;
                    arParms[1] = new SqlParameter("@ActionType", SqlDbType.Int);
                    arParms[1].Value = objusers.ActionType;
                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_ADM_EditUserDetail", arParms);
                    List<AddUsersData> lstUserDetails = ORHelper<AddUsersData>.FromDataReaderToList(sqlReader);
                    result = lstUserDetails;
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
        public int DeleteUserDetailsByID(AddUsersData objusers)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[3];

                    arParms[0] = new SqlParameter("@LoginID", SqlDbType.Int);
                    arParms[0].Value = objusers.LoginID;
                    arParms[1] = new SqlParameter("@ActionType", SqlDbType.Int);
                    arParms[1].Value = objusers.ActionType;
                    arParms[2] = new SqlParameter("@Output", SqlDbType.SmallInt);
                    arParms[2].Direction = ParameterDirection.Output;
                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_ADM_DeleteUserDetail", arParms);
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

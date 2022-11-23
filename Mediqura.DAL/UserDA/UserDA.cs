using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mediqura.CommonData.LoginData;
using Mediqura.Utility;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using Mediqura.Utility.Util;

namespace Mediqura.DAL.UserDA
{
    public class UserDA
    {
        public bool UpdateLogData(LogData Objmeduser)
        {
            bool result = false;

            try
            {
                SqlParameter[] arParms = new SqlParameter[7];

                arParms[0] = new SqlParameter("@LoginID", SqlDbType.Int);
                arParms[0].Value = Objmeduser.LoginID;

                arParms[1] = new SqlParameter("@UserName", SqlDbType.VarChar);
                arParms[1].Value = Objmeduser.UserName;

                arParms[2] = new SqlParameter("@UserPassword", SqlDbType.VarChar);
                arParms[2].Value = Objmeduser.UserPassword;

                arParms[3] = new SqlParameter("@RoleID", SqlDbType.Int);
                arParms[3].Value = Objmeduser.RoleID;

                arParms[4] = new SqlParameter("@AddedBy", SqlDbType.BigInt);
                arParms[4].Value = Objmeduser.AddedBy;

                arParms[5] = new SqlParameter("@ModifiedBy", SqlDbType.BigInt);
                arParms[5].Value = Objmeduser.LastModifiedBy;

                arParms[6] = new SqlParameter("@ActionType", SqlDbType.Int);
                arParms[6].Value = Objmeduser.ActionType;


                int records = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "", arParms);
                if (records == -1 || records > 0)
                    result = true;
            }

            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.DataAccessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex);
                throw new DataAccessException("5000001", ex);
            }

            return result;
        }
        public bool UpdateLoginStatus(LogData Objmeduser)
        {
            bool result = false;

            try
            {
                SqlParameter[] arParms = new SqlParameter[5];

                arParms[0] = new SqlParameter("@UserName", SqlDbType.VarChar);
                arParms[0].Value = Objmeduser.UserName;

                arParms[1] = new SqlParameter("@UserPassword", SqlDbType.VarChar);
                arParms[1].Value = Objmeduser.UserPassword;

                arParms[2] = new SqlParameter("@RoleID", SqlDbType.Int);
                arParms[2].Value = Objmeduser.RoleID;

                arParms[3] = new SqlParameter("@IPaddress", SqlDbType.VarChar);
                arParms[3].Value = Objmeduser.IPaddress;

                arParms[4] = new SqlParameter("@LoginStatus", SqlDbType.Int);
                arParms[4].Value = Objmeduser.IsActiveLogin;


                int records = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_ADM_UpdateLoginStatus", arParms);
                if (records == -1 || records > 0)
                {
                    result = true;
                }
                else
                {
                    result = false;

                }
            }

            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.DataAccessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex);
                throw new DataAccessException("5000001", ex);
            }

            return result;
        }
        public List<SiteMapData> GetMedPagesbyRoleID(RolesData objRole)
        {
            List<SiteMapData> lstSiteMap = null;
            try
            {
                SqlParameter[] arParms = new SqlParameter[3];

                arParms[0] = new SqlParameter("@RoleID", SqlDbType.Int);
                arParms[0].Value = objRole.RoleID;

                arParms[1] = new SqlParameter("@MenuHeaderID", SqlDbType.Int);
                arParms[1].Value = objRole.MenuHeaderID;

                arParms[2] = new SqlParameter("@PageID", SqlDbType.Int);
                arParms[2].Value = objRole.PageID;

                DataSet ds = new DataSet();
                SqlHelper.FillDataset(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetMediquraPages", ds, new string[] { "SiteMap" }, arParms);
                lstSiteMap = ORHelper<SiteMapData>.FromDataTableToList(ds.Tables["SiteMap"]);

            }
            catch (Exception ex) //Exception of the layer(itself)/unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "5000001");
                LogManager.LogMedError(ex);
                throw new DataAccessException("5000001", ex);
            }

            return lstSiteMap;
        }
        public bool SavePageRole(RolesData objRole)
        {
            bool status = false;
            try
            {

                SqlParameter[] arParms = new SqlParameter[3];

                arParms[0] = new SqlParameter("@RoleId", SqlDbType.Int);
                arParms[0].Value = objRole.RoleID;

                arParms[1] = new SqlParameter("@XMLData", SqlDbType.Xml);
                arParms[1].Value = objRole.XMLData;

                arParms[2] = new SqlParameter("@LastModBy", SqlDbType.BigInt);
                arParms[2].Value = objRole.LastModifiedBy;

                int noOfEffectedRecords = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_SavePageRoles", arParms);
                if (noOfEffectedRecords == -1 || noOfEffectedRecords > 0)
                    status = true;

            }
            catch (Exception ex) //Exception of the layer(itself)/unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "5000001");
                LogManager.LogMedError(ex);
                throw new DataAccessException("5000001", ex);
            }

            return status;
        }
        public bool ClearuncompleteTransactions(LogData Objmeduser)
        {
            bool result = false;

            try
            {
                SqlParameter[] arParms = new SqlParameter[3];

                arParms[0] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                arParms[0].Value = Objmeduser.EmployeeID;

                arParms[1] = new SqlParameter("@MedSubStockID", SqlDbType.Int);
                arParms[1].Value = Objmeduser.MedSubStockID;

                arParms[2] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                arParms[2].Value = Objmeduser.FinancialYearID;

                int records = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_PHR_ClearUncompleteTransactions", arParms);
                if (records == -1 || records > 0)
                {
                    result = true;
                }
                else
                {
                    result = false;

                }
            }

            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.DataAccessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex);
                throw new DataAccessException("5000001", ex);
            }

            return result;
        }
        public bool SaveSiteMapRole(RolesData objRole)
        {
            bool status = false;
            try
            {

                SqlParameter[] arParms = new SqlParameter[5];

                arParms[0] = new SqlParameter("@RoleId", SqlDbType.Int);
                arParms[0].Value = objRole.RoleID;

                arParms[1] = new SqlParameter("@XMLData", SqlDbType.VarChar);
                arParms[1].Value = objRole.XMLData;

                arParms[2] = new SqlParameter("@AddedBy", SqlDbType.BigInt);
                arParms[2].Value = objRole.AddedBy;

                arParms[3] = new SqlParameter("@LastModBy", SqlDbType.BigInt);
                arParms[3].Value = objRole.LastModifiedBy;

                arParms[4] = new SqlParameter("@ActionType", SqlDbType.Int);
                arParms[4].Value = objRole.ActionType;

                int noOfEffectedRecords = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_SaveSiteMapRole", arParms);
                if (noOfEffectedRecords > 0)
                    status = true;

            }
            catch (Exception ex) //Exception of the layer(itself)/unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "5000001");
                LogManager.LogMedError(ex);
                throw new DataAccessException("5000001", ex);
            }
            return status;
        }
        public UserData getLogData(LogData Objmeduser)
        {
            UserData result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[4];

                    arParms[0] = new SqlParameter("@UserName", SqlDbType.VarChar);
                    arParms[0].Value = Objmeduser.UserName;

                    arParms[1] = new SqlParameter("@UserPassword", SqlDbType.VarChar);
                    arParms[1].Value = Objmeduser.UserPassword;

                    arParms[2] = new SqlParameter("@ActionType", SqlDbType.Int);
                    arParms[2].Value = Objmeduser.ActionType;

                    arParms[3] = new SqlParameter("@IPaddress", SqlDbType.VarChar);
                    arParms[3].Value = Objmeduser.IPaddress;

                    DataSet ds = new DataSet();
                    SqlHelper.FillDataset(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_ADM_GetLoginDetails", ds, new string[] { "LogData", "Role", "SiteMap" }, arParms);
                    List<SiteMapData> objSiteMap = ORHelper<SiteMapData>.FromDataTableToList(ds.Tables["SiteMap"]);
                    List<RolesData> lstRole = ORHelper<RolesData>.FromDataTableToList(ds.Tables["Role"]);
                    LogData listservice = ORHelper<LogData>.FromDataTable(ds.Tables["LogData"]);
                    if (listservice != null)
                    { 
                        result = new UserData();
                        result.objmeduser = listservice;
                        result.DashBoardUrl = listservice.DashBoardUrl;
                        result.FPData = listservice.FPData;
                        result.BillSetting = listservice.BillSetting;
                        result.RoleList = lstRole;
                        result.SiteMapList = objSiteMap;
                        result.SiteMapDT = ds.Tables["SiteMap"];
                    }
                }
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.DataAccessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex);
                throw new DataAccessException("5000001", ex);
            }
            return result;
        }
        public UserData getLogDatareset(LogData Objmeduser)
        {
            UserData result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[4];

                    arParms[0] = new SqlParameter("@UserName", SqlDbType.VarChar);
                    arParms[0].Value = Objmeduser.UserName;

                    arParms[1] = new SqlParameter("@UserPassword", SqlDbType.VarChar);
                    arParms[1].Value = Objmeduser.UserPassword;

                    arParms[2] = new SqlParameter("@ActionType", SqlDbType.Int);
                    arParms[2].Value = Objmeduser.ActionType;

                    arParms[3] = new SqlParameter("@IPaddress", SqlDbType.VarChar);
                    arParms[3].Value = Objmeduser.IPaddress;

                    DataSet ds = new DataSet();
                    SqlHelper.FillDataset(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_ADM_GetLoginDetails", ds, new string[] { "LogData", "Role", "SiteMap" }, arParms);
                    List<SiteMapData> objSiteMap = ORHelper<SiteMapData>.FromDataTableToList(ds.Tables["SiteMap"]);
                    List<RolesData> lstRole = ORHelper<RolesData>.FromDataTableToList(ds.Tables["Role"]);
                    LogData listservice = ORHelper<LogData>.FromDataTable(ds.Tables["LogData"]);
                    if (listservice != null)
                    {
                        result = new UserData();
                        result.objmeduser = listservice;
                        result.RoleList = lstRole;
                        result.SiteMapList = objSiteMap;
                        result.SiteMapDT = ds.Tables["SiteMap"];
                    }

                }
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.DataAccessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex);
                throw new DataAccessException("5000001", ex);
            }
            return result;
        }
        public List<LogData> BindLogin(LogData Objmeduser)
        {
            List<LogData> result = null;
            try
            {
                SqlParameter[] arParms = new SqlParameter[1];
                arParms[0] = new SqlParameter("@ActionType", SqlDbType.Int);
                arParms[0].Value = Objmeduser.ActionType;

                SqlDataReader sqlReader = null;
                sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "", arParms);
                List<LogData> listservice = ORHelper<LogData>.FromDataReaderToList(sqlReader);
                result = listservice;
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.DataAccessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex);
                throw new DataAccessException("5000001", ex);
            }
            return result;
        }
        public bool DeleteLogin(LogData Objmeduser)
        {
            bool result = false;
            try
            {
                SqlParameter[] arParms = new SqlParameter[2];

                arParms[0] = new SqlParameter("@LoginID", SqlDbType.Int);
                arParms[0].Value = Objmeduser.LoginID;

                arParms[1] = new SqlParameter("@ActionType", SqlDbType.Int);
                arParms[1].Value = Objmeduser.ActionType;

                int records = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "", arParms);
                if (records == -1 || records > 0)
                    result = true;
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.DataAccessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex);
                throw new DataAccessException("5000001", ex);
            }
            return result;
        }
        public LogData ScheduleLogin(LogData Objmeduser)
        {
            LogData result;

            try
            {
                SqlParameter[] arParms = new SqlParameter[4];

                arParms[0] = new SqlParameter("@LoginID", SqlDbType.Int);
                arParms[0].Value = Objmeduser.LoginID;

                arParms[1] = new SqlParameter("@LoginDate", SqlDbType.VarChar);
                arParms[1].Value = Objmeduser.LoginDate;

                arParms[2] = new SqlParameter("@LoginTime", SqlDbType.VarChar);
                arParms[2].Value = Objmeduser.LoginTime;

                arParms[3] = new SqlParameter("@ActionType", SqlDbType.Int);
                arParms[3].Value = Objmeduser.ActionType;

                SqlDataReader sqlReader = null;
                sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "", arParms);
                LogData listservice = ORHelper<LogData>.FromDataReader(sqlReader);
                result = listservice;
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.DataAccessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex);
                throw new DataAccessException("5000001", ex);
            }

            return result;
        }
        public bool ScheduleLogOut(LogData Objmeduser)
        {
            bool result = false;

            try
            {
                SqlParameter[] arParms = new SqlParameter[4];

                arParms[0] = new SqlParameter("@ID", SqlDbType.Int);
                arParms[0].Value = Objmeduser.scheduleID;

                arParms[1] = new SqlParameter("@LogOutDate", SqlDbType.VarChar);
                arParms[1].Value = Objmeduser.LogOutDate;

                arParms[2] = new SqlParameter("@LogOutTime", SqlDbType.VarChar);
                arParms[2].Value = Objmeduser.LogOutTime;

                arParms[3] = new SqlParameter("@ActionType", SqlDbType.Int);
                arParms[3].Value = Objmeduser.ActionType;

                int records = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "", arParms);
                if (records == -1 || records > 0)
                    result = true;
            }

            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.DataAccessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex);
                throw new DataAccessException("5000001", ex);
            }

            return result;
        }
        public bool CheckLogout(LogData Objmeduser)
        {
            bool result = false;

            try
            {
                SqlParameter[] arParms = new SqlParameter[0];
                int records = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "", arParms);
                if (records == -1 || records > 0)
                    result = true;
            }

            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.DataAccessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex);
                throw new DataAccessException("5000001", ex);
            }

            return result;
        }
        public List<SiteMapData> GetAllSiteMapItem(SiteMapData objSiteMap)
        {
            List<SiteMapData> lstSiteMap = null;
            try
            {
                DataSet ds = new DataSet();
                SqlHelper.FillDataset(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetAllSiteMapItem", ds, new string[] { "SiteMap" }, null);
                lstSiteMap = ORHelper<SiteMapData>.FromDataTableToList(ds.Tables["SiteMap"]);

            }
            catch (Exception ex) //Exception of the layer(itself)/unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "5000001");
                LogManager.LogMedError(ex);
                throw new DataAccessException("5000001", ex);
            }
            return lstSiteMap;

        }
        public List<SiteMapData> GetSiteMapRoleByRoleID(RolesData objRole)
        {
            List<SiteMapData> lstSiteMap = null;
            try
            {
                SqlParameter[] arParms = new SqlParameter[1];

                arParms[0] = new SqlParameter("@RoleID", SqlDbType.Int);
                arParms[0].Value = objRole.RoleID;  //objRole.UserTypeID;

                DataSet ds = new DataSet();
                SqlHelper.FillDataset(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_GetSiteMapRoleByRoleID", ds, new string[] { "SiteMap" }, arParms);
                lstSiteMap = ORHelper<SiteMapData>.FromDataTableToList(ds.Tables["SiteMap"]);

            }
            catch (Exception ex) //Exception of the layer(itself)/unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.BusinessProcessExceptionPolicy, ex, "5000001");
                LogManager.LogMedError(ex);
                throw new DataAccessException("5000001", ex);
            }

            return lstSiteMap;
        }
    }
}

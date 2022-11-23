using Mediqura.CommonData.Common;
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

namespace Mediqura.DAL.CommonDA
{
    public class PageControlDA
    {
        public List<PageControlsData> GetControlList(PageControlsData objcontrols)
        {
            List<PageControlsData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[3];

                    arParms[0] = new SqlParameter("@RoleID", SqlDbType.Int);
                    arParms[0].Value = objcontrols.RoleID;

                    arParms[1] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[1].Value = objcontrols.EmployeeID;

                    arParms[2] = new SqlParameter("@SitemapID", SqlDbType.Int);
                    arParms[2].Value = objcontrols.SitemapID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Get_ControlList", arParms);
                    List<PageControlsData> controlist = ORHelper<PageControlsData>.FromDataReaderToList(sqlReader);
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
        public List<PageControlsData> GetPageurls(PageControlsData objcontrols)
        {
            List<PageControlsData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[2];

                    arParms[0] = new SqlParameter("@RoleID", SqlDbType.Int);
                    arParms[0].Value = objcontrols.RoleID;

                    arParms[1] = new SqlParameter("@PageID", SqlDbType.Int);
                    arParms[1].Value = objcontrols.PageID;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Get_PageUrls", arParms);
                    List<PageControlsData> controlist = ORHelper<PageControlsData>.FromDataReaderToList(sqlReader);
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
        public List<PageControlsData> GetControlEnabledetails(PageControlsData objcontrols)
        {
            List<PageControlsData> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[3];

                    arParms[0] = new SqlParameter("@RoleID", SqlDbType.Int);
                    arParms[0].Value = objcontrols.RoleID;

                    arParms[1] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[1].Value = objcontrols.EmployeeID;

                    arParms[2] = new SqlParameter("@url", SqlDbType.VarChar);
                    arParms[2].Value = objcontrols.url;

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Get_ControlEnableDetails", arParms);
                    List<PageControlsData> controlist = ORHelper<PageControlsData>.FromDataReaderToList(sqlReader);
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
        public int Updatepagecontrols(PageControlsData objcontrol)
        {
            int result = 0;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[7];

                    arParms[0] = new SqlParameter("@XMLData", SqlDbType.Xml);
                    arParms[0].Value = objcontrol.XMLData;

                    arParms[1] = new SqlParameter("@EmployeeID", SqlDbType.BigInt);
                    arParms[1].Value = objcontrol.EmployeeID;

                    arParms[2] = new SqlParameter("@RoleID", SqlDbType.Int);
                    arParms[2].Value = objcontrol.RoleID;

                    arParms[3] = new SqlParameter("@HospitalID", SqlDbType.Int);
                    arParms[3].Value = objcontrol.HospitalID;

                    arParms[4] = new SqlParameter("@FinancialYearID", SqlDbType.Int);
                    arParms[4].Value = objcontrol.FinancialYearID;

                    arParms[5] = new SqlParameter("@AddedBy", SqlDbType.VarChar);
                    arParms[5].Value = objcontrol.AddedBy;

                    arParms[6] = new SqlParameter("@Output", SqlDbType.BigInt);
                    arParms[6].Direction = ParameterDirection.Output;

                    int result_ = SqlHelper.ExecuteNonQuery(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Update_Page_Controls_Access", arParms);
                    if (result_ > 0 || result_ == -1)
                    {
                        result = Convert.ToInt32(arParms[6].Value);
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

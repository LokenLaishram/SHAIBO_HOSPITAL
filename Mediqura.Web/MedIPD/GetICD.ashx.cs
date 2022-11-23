using Mediqura.CommonData.Common;
using Mediqura.CommonData.MedBillData;
using Mediqura.DAL;
using Mediqura.Utility;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using Mediqura.Utility.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace Mediqura.Web.MedIPD
{
    public class GetICD : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string keyword = context.Request.QueryString["key"];
            context.Response.ContentType = "text/json";
            List<ICDdata> result = null;
            try
            {
                {
                    SqlParameter[] arParms = new SqlParameter[1];

                    arParms[0] = new SqlParameter("@keyword", SqlDbType.VarChar);
                    arParms[0].Value = keyword;
                    

                    SqlDataReader sqlReader = null;
                    sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_search_icd10", arParms);
                    List<ICDdata> icdList = ORHelper<ICDdata>.FromDataReaderToList(sqlReader);
                    result = icdList;

                    context.Response.Write(new JavaScriptSerializer().Serialize(result));
                }
            }
            catch (Exception ex) //Exception of the business layer(itself)//unhandle
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.DataAccessExceptionPolicy, ex, "330001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.DA);
                throw new DataAccessException("5000001", ex);
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
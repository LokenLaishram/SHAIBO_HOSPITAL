using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace Mediqura.Web.Admin
{
    /// <summary>
    /// Summary description for AutocompleteLinks
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class AutocompleteLinks : System.Web.Services.WebService
    {
        public AutocompleteLinks()
        {
        }
        [WebMethod]
        public string[] GetCompletionList(string prefixText, int count, string contextKey)
        {
            if (count == 0)
            {
                count = 10;
            }
            DataTable dt = GetRecords(prefixText, contextKey);
            List<string> items = new List<string>(count);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string strName = dt.Rows[i][0].ToString();
                items.Add(strName);
            }
            return items.ToArray();
        }
        public DataTable GetRecords(string Links, String RoleID)
        {
            string strConn = ConfigurationManager.ConnectionStrings["SqlConnectionString11"].ConnectionString;
            SqlConnection con = new SqlConnection(strConn);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.Add("@RoleID", SqlDbType.Int).Value = RoleID;
            cmd.Parameters.Add("@Links", SqlDbType.VarChar).Value = Links;
            cmd.CommandText = "usp_MDQ_GetAultocompleteLinks";
            DataSet objDs = new DataSet();
            SqlDataAdapter dAdapter = new SqlDataAdapter();
            dAdapter.SelectCommand = cmd;
            con.Open();
            dAdapter.Fill(objDs);
            con.Close();
            return objDs.Tables[0];
        }
    }
}

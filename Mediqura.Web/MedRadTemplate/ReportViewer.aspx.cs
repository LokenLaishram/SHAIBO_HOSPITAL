using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mediqura.Web.MedRadTemplate
{
    public partial class ReportViewer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.ConnectionStrings["SqlConnectionString11"].ConnectionString;
            if (Request.QueryString["option"] != null)
            {
                switch (Request.QueryString["option"].ToString())
                {
                    case "ReportTemplate":
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Connection = con;
                                    cmd.CommandText = "usp_MDQ_Print_RadTemplatebyID";
                                    cmd.Parameters.Add("@TesID", SqlDbType.Int).Value = Convert.ToInt32(Request.QueryString["TestID"]);
                                    cmd.Parameters.Add("@GenID", SqlDbType.Int).Value = Convert.ToInt32(Request.QueryString["GenID"]);
                                    cmd.Parameters.Add("@TemplateType", SqlDbType.Int).Value = Convert.ToInt32(Request.QueryString["TemplateType"]);
                                    con.Open();
                                    cmd.ExecuteNonQuery();
                                    byte[] bytes = (byte[])cmd.ExecuteScalar();
                                    string base64 = Convert.ToBase64String(bytes);
                                    Response.ContentType = "application/pdf";
                                    Response.AddHeader("content-length", bytes.Length.ToString());
                                    Response.BinaryWrite(bytes);
                                }
                            }
                        }
                        break;
                    case "RadioReport":
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter())
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Connection = con;
                                    cmd.CommandText = "usp_MDQ_Print_RadReport";
                                    cmd.Parameters.Add("@TesID", SqlDbType.Int).Value = Convert.ToInt32(Request.QueryString["TestID"]);
                                    cmd.Parameters.Add("@InvNo", SqlDbType.VarChar).Value = Request.QueryString["Inv"];
                                    cmd.Parameters.Add("@UHID", SqlDbType.Int).Value = Convert.ToInt32(Request.QueryString["UHID"]);
                                    con.Open();
                                    cmd.ExecuteNonQuery();
                                    byte[] bytes = (byte[])cmd.ExecuteScalar();
                                    string base64 = Convert.ToBase64String(bytes);
                                    Response.ContentType = "application/pdf";
                                    Response.AddHeader("content-length", bytes.Length.ToString());
                                    Response.BinaryWrite(bytes);
                                }
                            }
                        }
                        break;
                }

            }
        }
    }
}
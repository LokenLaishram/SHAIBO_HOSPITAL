using Mediqura.Utility;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Mediqura.Web.MedCommon
{
    /// <summary>
    /// Summary description for ReadFile
    /// </summary>
    public class ReadFile : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            int id = int.Parse(context.Request.QueryString["Id"]);
            byte[] bytes;
            string fileName, contentType;
            string constr = GlobalConstant.ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT Filename,FileData,ContentType FROM MDQ_patient_case_history_documents WHERE ID=@Id";
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        sdr.Read();
                        bytes = (byte[])sdr["FileData"];
                        contentType = sdr["ContentType"].ToString();
                        fileName = sdr["Filename"].ToString();
                    }
                    con.Close();
                }
            }

            context.Response.Buffer = true;
            context.Response.Charset = "";
            if (context.Request.QueryString["download"] == "1")
            {
                context.Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
            }
            context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            context.Response.ContentType = "application/pdf";
            context.Response.BinaryWrite(bytes);
            context.Response.Flush();
            context.Response.End();
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
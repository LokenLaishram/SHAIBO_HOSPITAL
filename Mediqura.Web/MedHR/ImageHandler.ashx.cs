using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.IO;
using Mediqura.Web.MedCommon;
using Mediqura.Utility;

namespace Mobimp.Campusoft.Web
{
    public class ImageHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
           
            string imageid = context.Request.QueryString["ImID"];
            SqlConnection con = new SqlConnection(GlobalConstant.ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT EmployeePhoto FROM MDQ_EmployeeDetails WHERE EmployeeID=" + imageid, con);
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read(); 
            if (!dr.IsDBNull(0))
            {
                context.Response.BinaryWrite((byte[])dr[0]);
            }
            con.Close();
            context.Response.End();
        }
        public byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            return ms.ToArray();
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
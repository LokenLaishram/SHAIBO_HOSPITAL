using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.IO;
using Mediqura.Web.MedCommon;
using Mediqura.Utility;


namespace Mediqura.Web.MedHR
{
    /// <summary>
    /// Summary description for EmployeeSignHandler
    /// </summary>
    public class EmployeeSignHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string empSignID = context.Request.QueryString["empID"];
            SqlConnection con = new SqlConnection(GlobalConstant.ConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT EmployeeDigitalSign FROM MDQ_EmployeeDetails WHERE EmployeeID=" + empSignID, con);
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            if (!dr.IsDBNull(0))
            {
                context.Response.BinaryWrite((byte[])dr[0]);
            }
            //else
            //{
            //    System.Drawing.Image img = System.Drawing.Image.FromFile(context.Server.MapPath("~/MedImages/DummyImage.png.jpg"));
            //    Byte[] image = imageToByteArray(img);
            //    context.Response.BinaryWrite(image);
            //}
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
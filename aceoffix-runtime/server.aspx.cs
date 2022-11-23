//
// Warning: This code is reserved by Aceoffix and can not be modified.
//
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace aceoffix_runtime
{
    public partial class server : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Aceoffix.Xafnetrt.Server rtServer;
            rtServer = new Aceoffix.Xafnetrt.Server();

            string strOutPut = "Welcome to use Aceoffix product.\r\n";
            String strRegInfo = "";
            if (rtServer.SerialNumber != "")
            {
                strRegInfo = "LicenseKey: " + rtServer.SerialNumber + "<br>" + "Version: " + rtServer.VersionInfo + "<br>" + "User: " + rtServer.Company;
                if (rtServer.TrialEndTime != "")
                    strRegInfo = strRegInfo + "<br>" + "This product is a <span style=\"color:Red;\">trial edition</span> and it will expire on " + rtServer.TrialEndTime + ".";
                strRegInfo = strRegInfo + "<br>Aceoffix Server Version: " + rtServer.ServerVersion;
            }
            else
            {
                strRegInfo = "<span style=\"color:Red;\">The product is not registered.</span>";
            }

            strOutPut = strOutPut + "Aceoffix registration information: " + strRegInfo + "\r\n";
            strOutPut = strOutPut + "Aceoffix running information: " + rtServer.GetSysLog() + "\r\n";

            string ServerIP = Request.ServerVariables["HTTP_HOST"].ToLower();
            if ((ServerIP.StartsWith("localhost")) || (ServerIP.StartsWith("127.0.0.1")))
            {
                LabelReg.Text = strRegInfo;
                LabelLog.Text = rtServer.GetSysLog();
            }
            else
            {
                try
                {
                    FileStream fs = new FileStream(Path.GetTempPath() + "aceserver_log.txt", FileMode.Create);//D:\...\aceoffix-runtime No last "\"
                    StreamWriter sw = new StreamWriter(fs);
                    sw.Write(strOutPut);
                    sw.Flush();
                    sw.Close();
                    fs.Close();

                    LabelReg.Text = "Aceoffix system information has been generated, please view the aceserver_log.txt file in the temp folder of your web server." + "<br>Aceoffix Server Version: " + rtServer.ServerVersion;
                    LabelLog.Text = rtServer.GetSysLog();
                    //string tempFile = Path.GetTempPath();//If you cannot find the location of temp folder, please remove the comments.
                    //Response.Write(tempFile);
                }
                catch
                {
                    LabelReg.Text = "Cannot make the aceserver_log.txt file. The temp folder's permission maybe insufficient. For security reasons, you can view the system information of Aceoffix only with a local access." + "<br>Aceoffix Server Version: " + rtServer.ServerVersion;
                    LabelLog.Text = rtServer.GetSysLog();
                }
            }
        }
    }
}

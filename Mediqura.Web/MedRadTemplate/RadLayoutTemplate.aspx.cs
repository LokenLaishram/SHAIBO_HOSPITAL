using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.IO;
//using System.Web.UI.WebControls;
using DevExpress.Web.Office;
using DevExpress.XtraRichEdit;
using System.Runtime.Serialization;
using System.Data.SqlClient;
using DevExpress.Xpo.Logger;
using System.Configuration;
using System.Text;
using DevExpress.XtraRichEdit.API.Native;
using DevExpress.XtraRichEdit.Utils;
using Mediqura.Web.MedCommon;
using Mediqura.BOL.CommonBO;
using Mediqura.CommonData.MedUtilityData;
using Mediqura.BOL.MedUtilityBO;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.CommonData.Common;
using DevExpress.Web.Internal;
using System.Drawing;
using System.ComponentModel;
using DevExpress.Office.Utils;
using System.Security.Principal;

namespace Mediqura.Web.MedRadTemplate
{
    public partial class RadLayoutTemplate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //using (MemoryStream memoryStream = new MemoryStream())
                //{
                //    memoryStream.Close();
                //}
                //CreateNewdocument();
                OpenLayoutTemplate();
                clearmemorystream();

            }
        }
        private void clearmemorystream()
        {
            using (MemoryStream ms = new MemoryStream())
            {
                ms.SetLength(0);
                ms.Flush();
            }
        }
        string SessionKey1 = "EditedDocuemntID1";
        [DataMember]
        public byte[] Docbyte1 { get; set; }

        protected string EditedDocuemntID1
        {
            get { return (string)Session[SessionKey1] ?? string.Empty; }
            set { Session[SessionKey1] = value; }
        }
        protected void CreateNewdocument()
        {
            using (MemoryStream ms1 = new MemoryStream())
            {
                RichEditDocumentServer reDocumentServer = new RichEditDocumentServer();
                reDocumentServer.CreateNewDocument();
            }
        }
        private DataTable GetData1()
        {
            DataTable dt = new DataTable();
            string constr = ConfigurationManager.ConnectionStrings["SqlConnectionString11"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "usp_MDQ_util_GetRadLayoutTemplatesByID";
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        sda.Fill(dt);
                    }
                }
                return dt;
            }
        }
        protected void OpenLayoutTemplate()
        {
            if (!string.IsNullOrEmpty(EditedDocuemntID1))
            {
                DocumentManager.CloseDocument(DocumentManager.FindDocument(EditedDocuemntID1).DocumentId);
                EditedDocuemntID1 = string.Empty;
            }
            lbl_message3.Visible = false;
            div4.Visible = false;
            DataTable DataTable = new DataTable();
            DataTable = GetData1();
            DataView view = new DataView(DataTable);
            if (view.Count > 0)
            {
                EditedDocuemntID1 = "LayoutTemplate"; // Guid type 
                if (view.Count != 0)
                    RichLayoutTemplate.Open(
                        EditedDocuemntID1,
                        DevExpress.XtraRichEdit.DocumentFormat.Rtf,
                        () =>
                        {
                            byte[] docBytes1 = Encoding.ASCII.GetBytes(view.Table.Rows[0]["Template"].ToString());
                            return new MemoryStream(docBytes1);
                        }
                    );
            }
            else
            {
                EditedDocuemntID1 = "LayoutTemplate";
            }
        }
        protected void btn_updatelayout_Click(object sender, EventArgs e)
        {
            using (MemoryStream ms1 = new MemoryStream())
            {
                //int ID = Convert.ToInt32(EditedDocuemntID1 == "LayoutTemplate" ? "0" : EditedDocuemntID1);
                int ID = 0;
                RichLayoutTemplate.SaveCopy(ms1, DevExpress.XtraRichEdit.DocumentFormat.Rtf);
                byte[] arr1 = ms1.ToArray();
                Docbyte1 = arr1;
                UpdateLayoutDoc(Docbyte1, ID);
                //EditedDocuemntID1 = "";

            }
        }
        protected void UpdateLayoutDoc(byte[] Docbyte1, int ID)
        {
            string constr = ConfigurationManager.ConnectionStrings["SqlConnectionString11"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("usp_MDQ_util_UpdateRadLayoutTemplate"))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = con;
                        cmd.Parameters.Add("@Docbyte", SqlDbType.VarChar).Value = System.Text.Encoding.UTF8.GetString(Docbyte1);
                        cmd.Parameters.Add("@ID", SqlDbType.Int).Value = 1;
                        cmd.Parameters.Add("@Output", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        int result = Convert.ToInt32(cmd.Parameters.Add("@Output", SqlDbType.SmallInt).Direction);
                        if (result == 1)
                        {
                            Messagealert_.ShowMessage(lbl_message3, "save", 1);
                            div4.Visible = true;
                            div4.Attributes["class"] = "SucessAlert";
                        }
                        if (result == 2)
                        {
                            Messagealert_.ShowMessage(lbl_message3, "update", 1);
                            div4.Visible = true;
                            div4.Attributes["class"] = "SucessAlert";
                        }
                        con.Close();
                    }
                    catch (Exception ex)
                    {
                        PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                        // LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                        lbl_message3.Text = ExceptionMessage.GetMessage(ex);
                        Messagealert_.ShowMessage(lbl_message3, "system", 1);
                        div4.Attributes["class"] = "FailAlert";
                    }
                }
            }
        }
        protected void btn_reset_Click(object sender, EventArgs e)
        {
            EditedDocuemntID1 = "";
            Response.Redirect("~/MedRadTemplate/RadLayoutTemplate.aspx");
        }
    }
}
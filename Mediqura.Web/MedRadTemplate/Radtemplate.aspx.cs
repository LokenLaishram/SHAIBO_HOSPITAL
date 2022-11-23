using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.IO;
using System.Web.UI.WebControls;
using DevExpress.Web.Office;
using DevExpress.XtraRichEdit;
using System.Runtime.Serialization;
using System.Data.SqlClient;
using DevExpress.Xpo.Logger;
using System.Configuration;
using System.Text;
using DevExpress.XtraRichEdit.API.Native;
using Mediqura.Web.MedCommon;
using Mediqura.BOL.CommonBO;
using Mediqura.CommonData.MedUtilityData;
using Mediqura.BOL.MedUtilityBO;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.CommonData.Common;
using DevExpress.Web.Internal;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Mediqura.Web.MedRadTemplate
{
    public partial class Radtemplate : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblmessage.Visible = false;
                ddlbind();
                // CreateNewdocument();
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
        private void ddlbind()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddl_group, mstlookup.GetLookupsList(LookupName.InvestigationGroup));
            Commonfunction.Insertzeroitemindex(ddl_labsubgroup);
        }
        string SessionKey = "EditedDocuemntID";
        [DataMember]
        public byte[] Docbyte { get; set; }
        protected string EditedDocuemntID
        {
            get { return (string)Session[SessionKey] ?? string.Empty; }
            set { Session[SessionKey] = value; }
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetLabServices(string prefixText, int count, string contextKey)
        {
            ServicesData Objpaic = new ServicesData();
            ServiceBO objInfoBO = new ServiceBO();
            List<ServicesData> getResult = new List<ServicesData>();
            Objpaic.ServiceName = prefixText;
            Objpaic.ServiceTypeID = Convert.ToInt32(contextKey);
            getResult = objInfoBO.GetRemarktestservicesByID(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].ServiceName.ToString());
            }
            return list;
        }
        protected void CreateNewdocument()
        {
            // EditedDocuemntID = "0";
            DocumentManager.CloseDocument(EditedDocuemntID);
        }
        protected void ddl_labgroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddl_labsubgroup, mstlookup.GetSubGroupByGroupID(Convert.ToInt32(ddl_group.SelectedValue == "" ? "0" : ddl_group.SelectedValue)));
        }
        protected void ddl_labsubgroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_labsubgroup.SelectedIndex > 0)
            {
                AutoCompleteExtender2.ContextKey = ddl_labsubgroup.SelectedValue == "" ? "0" : ddl_labsubgroup.SelectedValue;
            }
            else
            {
                AutoCompleteExtender2.ContextKey = null;
            }
        }
        private void bindgrid(int page)
        {
            try
            {
                List<LabServiceMasterData> lstemp = GetLabServiceType(page);

                if (lstemp.Count > 0)
                {
                    GvLabService.VirtualItemCount = lstemp[0].MaximumRows;//total item is required for custom paging
                                                                          // GvLabService.PageIndex = page - 1;
                    GvLabService.DataSource = lstemp;
                    GvLabService.DataBind();
                    GvLabService.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total: " + lstemp[0].MaximumRows.ToString() + " Record(s) found.", 1);
                    divmsg3.Visible = true;
                    divmsg3.Attributes["class"] = "SucessAlert";
                }
                else
                {
                    GvLabService.DataSource = null;
                    GvLabService.DataBind();
                    GvLabService.Visible = true;
                    lblresult.Visible = false;

                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                //LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
            }
        }
        private List<LabServiceMasterData> GetLabServiceType(int p)
        {
            LabServiceMasterData objlabserviceData = new LabServiceMasterData();
            LabServiceMasterBO objlabserviceBO = new LabServiceMasterBO();
            objlabserviceData.LabGroupID = Convert.ToInt32(ddl_group.SelectedValue == "" ? null : ddl_group.SelectedValue);
            objlabserviceData.LabSubGroupID = Convert.ToInt32(ddl_labsubgroup.SelectedValue == "" ? null : ddl_labsubgroup.SelectedValue);
            objlabserviceData.TestID = Commonfunction.SemicolonSeparation_String_32(txt_testname.Text);
            objlabserviceData.CurrentIndex = p;
            return objlabserviceBO.GetserviceListByID(objlabserviceData);
        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            if (LogData.SearchEnable == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "SearchEnable", 0);
                divmsg3.Visible = true;
                divmsg3.Attributes["class"] = "FailAlert";
                return;
            }
            else
            {
                lblmessage.Visible = false;
            }
            bindgrid(1);
        }
        protected void txt_testname_TextChanged(object sender, EventArgs e)
        {
            bindgrid(1);
        }
        protected void gv_labtestlist_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Search")
                {
                    tabcontainer.ActiveTabIndex = 1;

                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GvLabService.Rows[i];
                    Label TestID = (Label)gr.Cells[0].FindControl("lbl_ID");
                    lbl_testID.Text = TestID.Text == "" ? "0" : TestID.Text;
                    // EditedDocuemntID = "0";
                    ddl_gender.SelectedIndex = 0;
                    ddl_templatetype.SelectedIndex = 0;
                    OpenLayoutTemplate1();
                    clearmemorystream();
                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                //  LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
                div1.Attributes["class"] = "FailAlert";
                div1.Visible = true;
                return;
            }
        }
        private DataTable GetData(int ID, int genID, int templateType)
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
                        cmd.CommandText = "usp_MDQ_util_GetRadTemplatesByID";
                        cmd.Parameters.Add("@TestID", SqlDbType.Int).Value = ID;
                        cmd.Parameters.Add("@GenID", SqlDbType.Int).Value = genID;
                        cmd.Parameters.Add("@TemplateID", SqlDbType.Int).Value = templateType;
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        sda.Fill(dt);
                    }
                }
                return dt;
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
        protected void ddl_gender_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(EditedDocuemntID))
            {
                DocumentManager.CloseDocument(DocumentManager.FindDocument(EditedDocuemntID).DocumentId);
                EditedDocuemntID = string.Empty;
            }
            lbl_message2.Visible = false;
            DataTable DataTable = new DataTable();
            int ID = Convert.ToInt32(lbl_testID.Text);
            int gednID = Convert.ToInt32(ddl_gender.SelectedValue == "" ? "0" : ddl_gender.SelectedValue);
            int templateID = Convert.ToInt32(ddl_templatetype.SelectedValue == "" ? "0" : ddl_templatetype.SelectedValue);
            DataTable = GetData(ID, gednID, templateID);

            DataView view = new DataView(DataTable);
            if (view.Count > 0)
            {
                EditedDocuemntID = view.Table.Rows[0]["ID"].ToString(); // Guid type 

                if (view.Count != 0)
                    Richteditor.Open(
                        EditedDocuemntID,
                        DevExpress.XtraRichEdit.DocumentFormat.Rtf,
                        () =>
                        {
                            byte[] docBytes = Encoding.ASCII.GetBytes(view.Table.Rows[0]["Template"].ToString());
                            return new MemoryStream(docBytes);
                        }
                    );
            }
            else
            {
                OpenLayoutTemplate1();

            }
            clearmemorystream();
        }
        protected void OpenLayoutTemplate1()
        {
            if (!string.IsNullOrEmpty(EditedDocuemntID))
            {
                DocumentManager.CloseDocument(DocumentManager.FindDocument(EditedDocuemntID).DocumentId);
                EditedDocuemntID = string.Empty;
            }
            lbl_message2.Visible = false;
            DataTable DataTable = new DataTable();
            DataTable = GetData1();
            DataView view = new DataView(DataTable);
            if (view.Count > 0)
            {
                EditedDocuemntID = "LayoutTemplate";  // Guid type 
                if (view.Count != 0)
                    Richteditor.Open(
                        EditedDocuemntID,
                        DevExpress.XtraRichEdit.DocumentFormat.Rtf,
                        () =>
                        {
                            byte[] docBytes2 = Encoding.ASCII.GetBytes(view.Table.Rows[0]["Template"].ToString());
                            return new MemoryStream(docBytes2);
                        }
                    );
            }
        }
        protected void btn_save_Click(object sender, EventArgs e)
        {
            if (ddl_gender.SelectedIndex == 0)
            {
                Messagealert_.ShowMessage(lbl_message2, "Gender", 0);
                div2.Visible = true;
                div2.Attributes["class"] = "FailAlert";
                div2.Focus();
                return;
            }
            else
            {
                lbl_message2.Visible = false;
            }
            if (ddl_templatetype.SelectedIndex == 0)
            {
                Messagealert_.ShowMessage(lbl_message2, "Template", 0);
                div2.Visible = true;
                div2.Attributes["class"] = "FailAlert";
                div2.Focus();
                return;
            }
            else
            {
                lbl_message2.Visible = false;
            }

            using (MemoryStream ms = new MemoryStream())
            {
                int ID = Convert.ToInt32(EditedDocuemntID == "LayoutTemplate" ? "0" : EditedDocuemntID);
                Richteditor.SaveCopy(ms, DevExpress.XtraRichEdit.DocumentFormat.Rtf);
                byte[] arr = ms.ToArray();
                Docbyte = arr;
                UpdateDocs(Docbyte, ID);
                //ms.SetLength(0);
                //ms.Flush();
                //ms.Close();

            }
            // CreateNewdocument();
        }
        protected void btnprint_Click(object sender, EventArgs e)
        {
            if (ddl_gender.SelectedIndex == 0)
            {
                Messagealert_.ShowMessage(lbl_message2, "Gender", 0);
                div2.Visible = true;
                div2.Attributes["class"] = "FailAlert";
                div2.Focus();
                return;
            }
            else
            {
                lbl_message2.Visible = false;
            }
            if (ddl_templatetype.SelectedIndex == 0)
            {
                Messagealert_.ShowMessage(lbl_message2, "Template", 0);
                div2.Visible = true;
                div2.Attributes["class"] = "FailAlert";
                div2.Focus();
                return;
            }
            int testID = Convert.ToInt32(lbl_testID.Text == "" ? "0" : lbl_testID.Text);
            int genID = Convert.ToInt32(ddl_gender.SelectedValue == "" ? "0" : ddl_gender.SelectedValue);
            int TemplateType = Convert.ToInt32(ddl_templatetype.SelectedValue == "" ? "0" : ddl_templatetype.SelectedValue);

            string url = "../MedRadTemplate/ReportViewer.aspx?option=ReportTemplate&TestID=" + testID + "&GenID=" + genID + "&TemplateType=" + TemplateType;
            string fullURL = "window.open('" + url + "', '_blank');";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_New_Tab", fullURL, true);
        }
        protected void UpdateDocs(byte[] Docbyte, int ID)
        {
            byte[] byteImage;
            using (MemoryStream ms = new MemoryStream())
            {
                RichEditDocumentServer reDocumentServer = new RichEditDocumentServer();
                reDocumentServer.Options.Printing.UpdateDocVariablesBeforePrint = false;
                Richteditor.ExportToPdf(ms);
                byteImage = ms.ToArray();
            }
            string constr = ConfigurationManager.ConnectionStrings["SqlConnectionString11"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("usp_MDQ_util_UpdateRadTemplate"))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = con;
                        cmd.Parameters.Add("@Docbyte", SqlDbType.VarChar).Value = System.Text.Encoding.UTF8.GetString(Docbyte);
                        cmd.Parameters.Add("@ReportImage", SqlDbType.Image).Value = byteImage;
                        cmd.Parameters.Add("@ID", SqlDbType.Int).Value = ID;
                        cmd.Parameters.Add("@TestID", SqlDbType.Int).Value = Convert.ToUInt32(lbl_testID.Text == "" ? "0" : lbl_testID.Text);
                        cmd.Parameters.Add("@GenID", SqlDbType.Int).Value = Convert.ToInt32(ddl_gender.SelectedValue == "" ? "0" : ddl_gender.SelectedValue);
                        cmd.Parameters.Add("@TemplateType", SqlDbType.Int).Value = Convert.ToInt32(ddl_templatetype.SelectedValue == "" ? "0" : ddl_templatetype.SelectedValue);
                        cmd.Parameters.Add("@EmployeeID", SqlDbType.BigInt).Value = LogData.EmployeeID;
                        cmd.Parameters.Add("@HospitalID", SqlDbType.Int).Value = LogData.HospitalID;
                        cmd.Parameters.Add("@FinancialyearID", SqlDbType.Int).Value = LogData.FinancialYearID;
                        cmd.Parameters.Add("@Output", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        int result = Convert.ToInt32(cmd.Parameters.Add("@Output", SqlDbType.SmallInt).Direction);
                        if (result == 1)
                        {
                            Messagealert_.ShowMessage(lbl_message2, "save", 1);
                            div2.Visible = true;
                            div2.Attributes["class"] = "SucessAlert";
                        }
                        if (result == 2)
                        {
                            Messagealert_.ShowMessage(lbl_message2, "update", 1);
                            div2.Visible = true;
                            div2.Attributes["class"] = "SucessAlert";
                        }
                        con.Close();
                    }
                    catch (Exception ex)
                    {
                        PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                        //LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                        lblmessage.Text = ExceptionMessage.GetMessage(ex);
                        Messagealert_.ShowMessage(lbl_message2, "system", 1);
                        div2.Attributes["class"] = "FailAlert";
                    }
                }
            }
        }
        protected void GvLabService_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label MaleID = e.Row.FindControl("lbl_maleID") as Label;
                Label FemaleID = e.Row.FindControl("lbl_femaleID") as Label;
                Label BothID = e.Row.FindControl("lbl_bothID") as Label;
                CheckBox Chk_Male = e.Row.FindControl("chk_male") as CheckBox;
                CheckBox Chk_female = e.Row.FindControl("chk_female") as CheckBox;
                CheckBox Chk_both = e.Row.FindControl("chek_both") as CheckBox;

                if (MaleID.Text == "1")
                {
                    Chk_Male.Checked = true;
                }
                else
                {
                    Chk_Male.Checked = false;
                }
                if (FemaleID.Text == "2")
                {
                    Chk_female.Checked = true;
                }
                else
                {
                    Chk_female.Checked = false;
                }
                if (BothID.Text == "3")
                {
                    Chk_both.Checked = true;
                }
                else
                {
                    Chk_both.Checked = false;
                }

            }
        }
        protected void btn_reset_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(EditedDocuemntID))
            {
                DocumentManager.CloseDocument(DocumentManager.FindDocument(EditedDocuemntID).DocumentId);
                EditedDocuemntID = string.Empty;
            }
            tabcontainer.ActiveTabIndex = 0;
            bindgrid(0);

        }

        protected void ddl_gender_SelectedIndexChanged1(object sender, EventArgs e)
        {
            ddl_templatetype.SelectedIndex = 0;
            OpenLayoutTemplate1();
        }
    }
}
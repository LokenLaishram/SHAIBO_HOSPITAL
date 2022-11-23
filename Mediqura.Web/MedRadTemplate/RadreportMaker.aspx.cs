using DevExpress.Web.Office;
using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedUtilityBO;
using Mediqura.BOL.PatientBO;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.LoginData;
using Mediqura.CommonData.MedUtilityData;
using Mediqura.CommonData.PatientData;
using Mediqura.Utility;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using Mediqura.Web.MedCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.API.Native;
using System.Drawing;
using System.Drawing.Drawing2D;
using DocumentFormat.OpenXml.Drawing.Diagrams;

namespace Mediqura.Web.MedRadTemplate
{
    public partial class RadreportMaker : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblmessage.Visible = false;

                ddlbind();
                // tab2.Visible = false;
            }
        }
        string SessionKey = "EditedDocuemntID";
        [DataMember]
        public byte[] Docbyte { get; set; }
        protected string EditedDocuemntID
        {
            get { return (string)Session[SessionKey] ?? string.Empty; }
            set { Session[SessionKey] = value; }
        }
        private void ddlbind()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddl_verifiedby, mstlookup.GetLookupsList(LookupName.Radiologist));
            Commonfunction.PopulateDdl(ddl_referal, mstlookup.GetLookupsList(LookupName.Labconsultant));
            Commonfunction.PopulateDdl(ddl_patienttype, mstlookup.GetLookupsList(LookupName.PatientCatagory));
            txtdate_from.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            txtdate_to.Text = System.DateTime.Now.ToString("dd/MM/yyyy");

        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetUHID(string prefixText, int count, string contextKey)
        {
            PatientData Objpaic = new PatientData();
            RegistrationBO objInfoBO = new RegistrationBO();
            List<PatientData> getResult = new List<PatientData>();
            Objpaic.UHID = Convert.ToInt64(prefixText);
            Objpaic.PatientType = Convert.ToInt32(contextKey);
            getResult = objInfoBO.GetRadioUHID(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].UHID.ToString());
            }
            return list;
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetInv(string prefixText, int count, string contextKey)
        {
            SampleCollectionData Objpaic = new SampleCollectionData();
            LabSampleCollctionBO objInfoBO = new LabSampleCollctionBO();
            List<SampleCollectionData> getResult = new List<SampleCollectionData>();
            Objpaic.Investigationumber = prefixText;
            Objpaic.PatientTypeID = Convert.ToInt32(contextKey);
            getResult = objInfoBO.GetRadioInvestigationno(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].Investigationumber.ToString());
            }
            return list;
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetIPNo(string prefixText, int count, string contextKey)
        {
            PatientData Objpaic = new PatientData();
            RegistrationBO objInfoBO = new RegistrationBO();
            List<PatientData> getResult = new List<PatientData>();
            Objpaic.IPNo = prefixText;
            Objpaic.PatientType = Convert.ToInt32(contextKey);
            getResult = objInfoBO.GetRadioIPNo(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].IPNo.ToString());
            }
            return list;
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetLabPatientName(string prefixText, int count, string contextKey)
        {
            SampleCollectionData Objpaic = new SampleCollectionData();
            LabSampleCollctionBO objInfoBO = new LabSampleCollctionBO();
            List<SampleCollectionData> getResult = new List<SampleCollectionData>();
            Objpaic.PatientName = prefixText;
            Objpaic.PatientTypeID = Convert.ToInt32(contextKey);
            getResult = objInfoBO.GetRadioPatientName(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].PatientName.ToString());
            }
            return list;
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetTestNames(string prefixText, int count, string contextKey)
        {
            SampleCollectionData Objpaic = new SampleCollectionData();
            LabSampleCollctionBO objInfoBO = new LabSampleCollctionBO();
            List<SampleCollectionData> getResult = new List<SampleCollectionData>();
            Objpaic.TestName = prefixText;
            Objpaic.PatientTypeID = Convert.ToInt32(contextKey);
            getResult = objInfoBO.GetRadioTestNames(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].TestName.ToString());
            }
            return list;
        }
        protected void ddl_patient_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            AutoCompleteExtender1.ContextKey = ddl_patienttype.SelectedValue == "" ? "0" : ddl_patienttype.SelectedValue;
            AutoCompleteExtender3.ContextKey = ddl_patienttype.SelectedValue == "" ? "0" : ddl_patienttype.SelectedValue;
            AutoCompleteExtender4.ContextKey = ddl_patienttype.SelectedValue == "" ? "0" : ddl_patienttype.SelectedValue;
        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            if (LogData.SearchEnable == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "SearchEnable", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                return;
            }
            else
            {
                lblmessage.Visible = false;
            }
            if (txtdate_from.Text.Trim() != "")
            {
                if (Commonfunction.isValidDate(txtdate_from.Text) == false)
                {
                    Messagealert_.ShowMessage(lblmessage, "ValidDatefrom", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    txtdate_from.Focus();
                    return;
                }
            }
            else
            {
                lblmessage.Visible = false;
                div1.Visible = false;
            }
            if (txtdate_to.Text.Trim() != "")
            {
                if (Commonfunction.isValidDate(txtdate_to.Text) == false)
                {
                    Messagealert_.ShowMessage(lblmessage, "ValidDateto", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    txtdate_to.Focus();
                    return;
                }
            }
            else
            {
                lblmessage.Visible = false;
                div1.Visible = false;
            }
            bindgrid();
        }
        private void bindgrid()
        {
            try
            {
                List<SampleCollectionData> lstemp = GetLatestList(0);
                if (lstemp.Count > 0)
                {
                    gv_Radlabtestlist.DataSource = lstemp;
                    gv_Radlabtestlist.DataBind();
                    gv_Radlabtestlist.Visible = true;
                    Messagealert_.ShowMessage(lbl_result, "Total: " + lstemp[0].MaximumRows.ToString() + " Record(s) found.", 1);
                    div6.Visible = true;
                    div6.Attributes["class"] = "SucessAlert";
                }
                else
                {
                    gv_Radlabtestlist.DataSource = null;
                    gv_Radlabtestlist.DataBind();
                    gv_Radlabtestlist.Visible = true;
                    lbl_result.Visible = false;
                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
            }
        }
        private List<SampleCollectionData> GetLatestList(int p)
        {
            SampleCollectionData objsample = new SampleCollectionData();
            LabSampleCollctionBO objlabBO = new LabSampleCollctionBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            objsample.Investigationumber = txt_invnumber.Text.Trim() == "" ? null : txt_invnumber.Text.Trim();
            objsample.PatientTypeID = Convert.ToInt32(ddl_patienttype.SelectedValue == "" ? "0" : ddl_patienttype.SelectedValue);
            objsample.PatientName = txt_patientnames.Text.Trim() == "" ? null : txt_patientnames.Text.Trim();
            DateTime from = txtdate_from.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txtdate_from.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txtdate_to.Text.Trim() == "" ? GlobalConstant.MaxSQLDateTime : DateTime.Parse(txtdate_to.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objsample.DateFrom = from;
            objsample.DateTo = To;
            objsample.UHID = Commonfunction.SemicolonSeparation_String_64(txt_patientnames.Text);
            objsample.IPNo = txt_ipnumber.Text.Trim() == "" ? null : txt_ipnumber.Text.Trim();
            objsample.ConsultantID = Convert.ToInt64(ddl_referal.SelectedValue == "" ? "0" : ddl_referal.SelectedValue);
            objsample.StatusID = Convert.ToInt32(ddl_status.SelectedValue == "" ? "0" : ddl_status.SelectedValue);
            objsample.LabServiceID = Commonfunction.SemicolonSeparation_String_32(txt_testnames.Text);
            return objlabBO.GetRadioTestList(objsample);
        }
        protected void btnresets_Click(object sender, EventArgs e)
        {
            ddl_verifiedby.SelectedIndex = 0;
            lblmessage.Visible = false;
            div1.Visible = false;
            tabradreportmaker.ActiveTabIndex = 0;
            btn_print.Visible = false;
            if (!string.IsNullOrEmpty(EditedDocuemntID))
            {
                DocumentManager.CloseDocument(DocumentManager.FindDocument(EditedDocuemntID).DocumentId);
                EditedDocuemntID = string.Empty;
            }
            lbl_testID.Text = "";
            lbl_uhids.Text = "";
            lbl_invnumber.Text = "";
            lbl_genderID.Text = "";
            ddl_templatetype.SelectedIndex = 0;
            bindgrid();
            // tab2.Visible = false;
        }
        protected void gv_Radlabtestlist_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Search")
                {

                    LabResultData objresult = new LabResultData();
                    IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = gv_Radlabtestlist.Rows[i];
                    Label UHID = (Label)gr.Cells[0].FindControl("lbl_UHID");
                    Label InvNumber = (Label)gr.Cells[0].FindControl("lvl_inv");
                    Label TestID = (Label)gr.Cells[0].FindControl("lblTestID");
                    LinkButton ReportName = (LinkButton)gr.Cells[0].FindControl("lbl_test");
                    Label mtrecivedstatus = (Label)gr.Cells[0].FindControl("lbl_reciestatus");
                    Label SexID = (Label)gr.Cells[0].FindControl("lbl_sex");
                    Label lbl_templateID = (Label)gr.Cells[0].FindControl("lbl_template");
                    if (mtrecivedstatus.Text.Trim() == "0")
                    {
                        LinkButton result2 = (LinkButton)gv_Radlabtestlist.Rows[i].Cells[0].FindControl("lbl_test");
                        result2.Focus();
                        Messagealert_.ShowMessage(lblmessage1, "MTrecivdtime", 0);
                        div2.Visible = true;
                        div2.Attributes["class"] = "FailAlert";
                        return;
                    }
                    else
                    {
                        lblmessage1.Visible = false;
                    }
                    int ID = Convert.ToInt32(TestID.Text == "" ? "0" : TestID.Text);
                    string InvNo = InvNumber.Text == "" ? "0" : InvNumber.Text;
                    Int64 UHIDS = Convert.ToInt64(UHID.Text == "" ? "0" : UHID.Text);
                    lbl_invnumber.Text = InvNumber.Text == "" ? "0" : InvNumber.Text;
                    lbl_uhids.Text = UHID.Text == "" ? "0" : UHID.Text;
                    lbl_testID.Text = TestID.Text == "" ? "0" : TestID.Text;
                    lbl_genderID.Text = SexID.Text == "" ? "0" : SexID.Text;
                    int templateID = Convert.ToInt32(lbl_templateID.Text == "" ? "0" : lbl_templateID.Text);
                    if (!string.IsNullOrEmpty(EditedDocuemntID))
                    {
                        DocumentManager.CloseDocument(DocumentManager.FindDocument(EditedDocuemntID).DocumentId);
                        EditedDocuemntID = string.Empty;
                    }
                    DataTable DataTable = new DataTable();
                    DataTable = GetRadReport(ID, UHIDS, InvNo, templateID);
                    DataView view = new DataView(DataTable);
                    if (view.Count > 0)
                    {
                        EditedDocuemntID = view.Table.Rows[0]["ID"].ToString(); // Guid type 
                        ddl_templatetype.SelectedValue = view.Table.Rows[0]["TemplateType"].ToString();
                        ddl_verifiedby.SelectedValue = view.Table.Rows[0]["ReportVerifedByID"].ToString();
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
                        btn_print.Visible = true;

                    }
                    else
                    {
                        ddl_templatetype.SelectedIndex = 0;
                        ddl_verifiedby.SelectedIndex = 0;
                        OpenLayoutTemplate1();
                    }
                    tabradreportmaker.ActiveTabIndex = 1;
                    BindReportDetails();
                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lbl_result, "system", 0);
                div1.Attributes["class"] = "FailAlert";
                div1.Visible = true;
                return;
            }
        }
        protected void bindPatientreport(int testID, Int64 UHID, string InvNo, int GenderID, int TemplateID)
        {
            DataTable DataTable = new DataTable();
            DataTable = GetRadReport(testID, UHID, InvNo, TemplateID);
            DataView view = new DataView(DataTable);
            if (view.Count > 0)
            {
                EditedDocuemntID = view.Table.Rows[0]["ID"].ToString(); // Guid type 
                ddl_templatetype.SelectedValue = view.Table.Rows[0]["TemplateType"].ToString();
                ddl_verifiedby.SelectedValue = view.Table.Rows[0]["ReportVerifedByID"].ToString();
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
                bindtemplatebyID(testID, GenderID, TemplateID);
                ddl_verifiedby.SelectedIndex = 0;
            }


        }
        protected void bindtemplatebyID(int TestID, int GenderID, int TemplatID)
        {
            if (!string.IsNullOrEmpty(EditedDocuemntID))
            {
                DocumentManager.CloseDocument(DocumentManager.FindDocument(EditedDocuemntID).DocumentId);
                EditedDocuemntID = string.Empty;
            }

            DataTable DataTable = new DataTable();
            DataTable = GetData(TestID, GenderID, TemplatID);
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
        }
        protected void OpenLayoutTemplate1()
        {
            if (!string.IsNullOrEmpty(EditedDocuemntID))
            {
                DocumentManager.CloseDocument(DocumentManager.FindDocument(EditedDocuemntID).DocumentId);
                EditedDocuemntID = string.Empty;
            }
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
        protected void BindReportDetails()
        {
            using (MemoryStream streamWithRichEditContent = new MemoryStream())
            {
                Richteditor.SaveCopy(streamWithRichEditContent, DevExpress.XtraRichEdit.DocumentFormat.Rtf);
                streamWithRichEditContent.Position = 0;
                RichEditDocumentServer reDocumentServer = new RichEditDocumentServer();
                reDocumentServer.LoadDocument(streamWithRichEditContent, DevExpress.XtraRichEdit.DocumentFormat.Rtf);

                reDocumentServer.Options.MailMerge.DataSource = new SampleData();
                reDocumentServer.Document.CalculateDocumentVariable += new CalculateDocumentVariableEventHandler(Document_CalculateDocumentVariable);
                MailMergeOptions myMergeOptions = reDocumentServer.Document.CreateMailMergeOptions();
                reDocumentServer.Document.Fields.Update();
                // header update
                Section firstSection = reDocumentServer.Document.Sections[0];
                SubDocument myHeader = firstSection.BeginUpdateHeader();
                myHeader.Fields.Update();
                firstSection.EndUpdateHeader(myHeader);
                // footer update
                Section footersection = reDocumentServer.Document.Sections[0];
                SubDocument myfooter = footersection.BeginUpdateFooter();
                myfooter.Fields.Update();
                footersection.EndUpdateFooter(myfooter);

                using (MemoryStream streamWithModifiedRichEditContent = new MemoryStream())
                {
                    reDocumentServer.Options.Printing.UpdateDocVariablesBeforePrint = false;
                    reDocumentServer.SaveDocument(streamWithModifiedRichEditContent, DevExpress.XtraRichEdit.DocumentFormat.Rtf);
                    streamWithModifiedRichEditContent.Position = 0;
                    Richteditor.Open(Guid.NewGuid().ToString(), DevExpress.XtraRichEdit.DocumentFormat.Rtf, () =>
                    {
                        return streamWithModifiedRichEditContent;
                    });
                }
            }
        }
        public static System.Drawing.Image resizeImage(System.Drawing.Image image, int new_height, int new_width)
        {
            Bitmap new_image = new Bitmap(new_width, new_height);
            Graphics g = Graphics.FromImage((System.Drawing.Image)new_image);
            g.InterpolationMode = InterpolationMode.High;
            g.DrawImage(image, 0, 0, new_width, new_height);
            return new_image;
        }
        void Document_CalculateDocumentVariable(object sender, CalculateDocumentVariableEventArgs e)
        {
            LabResultData objresult = new LabResultData();
            LabSampleCollctionBO objlabBO = new LabSampleCollctionBO();
            objresult.LabServiceID = Convert.ToInt32(lbl_testID.Text == "" ? "0" : lbl_testID.Text);
            objresult.UHID = Convert.ToInt64(lbl_uhids.Text == "" ? "0" : lbl_uhids.Text);
            objresult.Investigationumber = lbl_invnumber.Text.Trim();
            objresult.VerifiedBy = Convert.ToInt64(ddl_verifiedby.SelectedValue == "" ? "0" : ddl_verifiedby.SelectedValue);
            List<LabResultData> Result = objlabBO.GetRadioReportDetails(objresult);
            RichEditDocumentServer srv = new RichEditDocumentServer();
            switch (e.VariableName)
            {
                case "Signature":
                    if (Result[0].EmpSignature != null)
                    {
                        byte[] DGbytes = Result[0].EmpSignature;
                        string DGbase64 = Convert.ToBase64String(DGbytes);
                        System.Drawing.Image Signature;
                        using (MemoryStream ms = new MemoryStream(DGbytes))
                        {
                            Signature = System.Drawing.Image.FromStream(ms);
                        }
                        System.Drawing.Image Rsignatute = resizeImage(Signature, 50, 90);
                        srv.Document.Images.Append(Rsignatute);
                        e.Value = srv.Document;
                        e.Handled = true;
                    }
                    break;
                case "Barcode":

                    string UHID = Result[0].UHID.ToString();
                    byte[] bytes = Commonfunction.getBarcodeImage(UHID);
                    string base64 = Convert.ToBase64String(bytes);
                    System.Drawing.Image barcode;
                    using (MemoryStream ms = new MemoryStream(bytes))
                    {
                        barcode = System.Drawing.Image.FromStream(ms);
                    }
                    System.Drawing.Image newbarcode = resizeImage(barcode, 25, 100);
                    srv.Document.Images.Append(newbarcode);
                    e.Value = srv.Document;
                    e.Handled = true;
                    break;
                case "UHID":
                    string AUHID = Result[0].UHID.ToString();
                    srv.Document.AppendText(AUHID);
                    e.Value = srv.Document;
                    e.Handled = true;
                    break;
                case "NAME":
                    string PatName = Result[0].PatientName.ToString();
                    srv.Document.AppendText(PatName);
                    e.Value = srv.Document;
                    e.Handled = true;
                    break;
                case "REFERAL":
                    string Referal = Result[0].ReferalDoctor.ToString();
                    srv.Document.AppendText(Referal);
                    e.Value = srv.Document;
                    e.Handled = true;
                    break;
                case "PATNO":
                    string patnumber = Result[0].PatientNumber.ToString();
                    srv.Document.AppendText(patnumber);
                    e.Value = srv.Document;
                    e.Handled = true;
                    break;
                case "VisitType":
                    string VT = Result[0].VisitType.ToString();
                    srv.Document.AppendText(VT);
                    e.Value = srv.Document;
                    e.Handled = true;
                    break;
                case "AGE":
                    string Age = Result[0].AgeCount.ToString();
                    srv.Document.AppendText(Age);
                    e.Value = srv.Document;
                    e.Handled = true;
                    break;
                case "SEX":
                    string Sex = Result[0].SexName.ToString();
                    srv.Document.AppendText(Sex);
                    e.Value = srv.Document;
                    e.Handled = true;
                    break;
                case "ADDRESS":
                    string Address = Result[0].Address.ToString();
                    srv.Document.AppendText(Address);
                    e.Value = srv.Document;
                    e.Handled = true;
                    break;
                case "REQON":
                    string Requestedon = Result[0].RequestedOn.ToString();
                    srv.Document.AppendText(Requestedon);
                    e.Value = srv.Document;
                    e.Handled = true;
                    break;
                case "CONTACT":
                    string contact = Result[0].PatContact.ToString();
                    srv.Document.AppendText(contact);
                    e.Value = srv.Document;
                    e.Handled = true;
                    break;
                case "INVNUMBER":
                    string InvNo = Result[0].Investigationumber.ToString();
                    srv.Document.AppendText(InvNo);
                    e.Value = srv.Document;
                    e.Handled = true;
                    break;
                case "REPORTTITLE":
                    string TestName = Result[0].TestName.ToString();
                    srv.Document.AppendText(TestName);
                    e.Value = srv.Document;
                    e.Handled = true;
                    break;
                case "Paragraph":
                    string record = "The Sun is the star at the center of the Solar System. It is a nearly perfect sphere of hot plasma,[15][16] with internal convective motion that generates a magnetic field via a dynamo process.[17] It is by far the most important source of energy for life on Earth. Its diameter is about 1.39 million kilometers (864,000 miles), or 109 times that of Earth, and its mass is about 330,000 times that of Earth. It accounts for about 99.86% of the total mass of the Solar System.[18] Roughly three quarters of the Sun's mass consists of hydrogen (~73%); the rest is mostly helium (~25%), with much smaller quantities of heavier elements, including oxygen, carbon, neon, and ";
                    srv.Document.AppendText(record);
                    e.Value = srv.Document;
                    e.Handled = true;
                    break;
                //case "Header":
                //    string headerpath = "~/Images/ReportHeader.png";
                //    System.Drawing.Image Himage = System.Drawing.Image.FromFile(Server.MapPath(headerpath));
                //    System.Drawing.Image NewHImage = resizeImage(Himage, 120, 670);
                //    srv.Document.Images.Append(NewHImage);
                //    e.Value = srv.Document;
                //    e.Handled = true;
                //    break;
                case "VerifiedOn":
                    if (Result[0].VerifiedOn != null)
                    {
                        string Verifyon = Result[0].VerifiedOn.ToString();
                        srv.Document.AppendText(Verifyon);
                        e.Value = srv.Document;
                        e.Handled = true;
                    }
                    break;
                case "ReportEntryOn":
                    string ReportEntryOn = Result[0].RequestedOn.ToString();
                    srv.Document.AppendText(ReportEntryOn);
                    e.Value = srv.Document;
                    e.Handled = true;
                    break;
                default:
                    break;
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
        protected void GetLabresultlist(LabResultData result)
        {
            LabSampleCollctionBO objlabBO = new LabSampleCollctionBO();
            List<LabResultData> Result = objlabBO.GetLabResults(result);
            if (Result.Count > 0)
            {
                tab2.Visible = true;
                tabradreportmaker.ActiveTabIndex = 1;
            }
            else
            {
                tab2.Visible = false;
                tabradreportmaker.ActiveTabIndex = 0;
                ddl_verifiedby.SelectedIndex = 0;
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        protected void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                if (LogData.SaveEnable == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "SaveEnable", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (ddl_templatetype.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "Template", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    div1.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                using (MemoryStream ms = new MemoryStream())
                {
                    int ID = Convert.ToInt32(EditedDocuemntID == "LayoutTemplate" || EditedDocuemntID == "" ? "0" : EditedDocuemntID);
                    Richteditor.SaveCopy(ms, DevExpress.XtraRichEdit.DocumentFormat.Rtf);
                    byte[] arr = ms.ToArray();
                    Docbyte = arr;
                    UpdateDocs(Docbyte, ID);

                }
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                lblmessage.Text = ExceptionMessage.GetMessage(ex);
            }
        }
        protected void gv_Radlabtestlist_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label urgency = e.Row.FindControl("lbl_urgencyid") as Label;
                Label Status = e.Row.FindControl("lbl_devicestatus") as Label;
                Label ReportStausID = e.Row.FindControl("lbl_statusID") as Label;
                Label ReportStaus = e.Row.FindControl("lblstatus") as Label;


                if (urgency.Text == "0" || urgency.Text == "1")
                {
                    e.Row.Cells[1].BackColor = System.Drawing.Color.Green;
                }
                if (urgency.Text == "2")
                {
                    e.Row.Cells[1].BackColor = System.Drawing.Color.Yellow;
                }
                if (urgency.Text == "3")
                {
                    e.Row.Cells[1].BackColor = System.Drawing.Color.Red;
                }
                if (ReportStausID.Text == "1")
                {
                    e.Row.Cells[6].BackColor = System.Drawing.Color.Red;
                    ReportStaus.Text = "Not Entry";
                    ReportStaus.ForeColor = System.Drawing.Color.Black;
                }
                if (ReportStausID.Text == "2")
                {
                    e.Row.Cells[6].BackColor = System.Drawing.Color.Yellow;
                    ReportStaus.Text = "Entry Done";
                    ReportStaus.ForeColor = System.Drawing.Color.Black;
                }
                if (ReportStausID.Text == "3")
                {
                    e.Row.Cells[6].BackColor = System.Drawing.Color.Green;
                    ReportStaus.Text = "Verified";
                    ReportStaus.ForeColor = System.Drawing.Color.White;
                }

            }
        }
        protected void gv_Radlabtestlist_OnRowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            if (LogData.UpdateEnable == 0)
            {
                Messagealert_.ShowMessage(lblmessage1, "UpdateEnable", 0);
                div2.Visible = true;
                div2.Attributes["class"] = "FailAlert";
                return;
            }
            else
            {
                lblmessage1.Visible = false;
            }
            Int64 ID = Convert.ToInt32(gv_Radlabtestlist.DataKeys[e.RowIndex].Values["ID"].ToString());
            System.Web.UI.WebControls.Label invnumber = (System.Web.UI.WebControls.Label)gv_Radlabtestlist.Rows[e.RowIndex].FindControl("lvl_inv");
            System.Web.UI.WebControls.Label TestID = (System.Web.UI.WebControls.Label)gv_Radlabtestlist.Rows[e.RowIndex].FindControl("lblTestID");

            Session["currenrrow"] = e.RowIndex;

            SampleCollectionData objdevice = new SampleCollectionData();
            LabSampleCollctionBO objInfoBO = new LabSampleCollctionBO();
            objdevice.ID = ID;
            objdevice.LabServiceID = Convert.ToInt32(TestID.Text == "" ? "0" : TestID.Text);
            objdevice.Investigationumber = invnumber.Text.Trim();
            objdevice.EmployeeID = LogData.EmployeeID;
            int result = objInfoBO.UpdateMLTrecievingtime(objdevice);
            if (result > 0)
            {
                gv_Radlabtestlist.DataSource = null;
                gv_Radlabtestlist.DataBind();
                bindgrid();
                Messagealert_.ShowMessage(lblmessage1, "update", 1);
                div2.Visible = true;
                div2.Attributes["class"] = "SucessAlert";
            }
            else
            {
                lblmessage1.Visible = false;
            }
        }
        protected void btnprint_Click(object sender, EventArgs e)
        {
            if (ddl_templatetype.SelectedIndex == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "Labtemplate", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                return;
            }
            else
            {
                lblmessage.Visible = false;
            }
            string template = ddl_templatetype.SelectedItem.Text.Substring(ddl_templatetype.SelectedItem.Text.LastIndexOf(':') + 1);
            string invno = lbl_invnumber.Text;
            Int64 UHID = Convert.ToInt64(lbl_uhids.Text == "" ? "0" : lbl_uhids.Text);
            int TestID = Convert.ToInt32(lbl_testID.Text == "" ? "0" : lbl_testID.Text);
            string url = "../MedRadTemplate/ReportViewer.aspx?option=RadioReport&Inv=" + invno + "&UHID=" + UHID + "&TestID=" + TestID;
            string fullURL = "window.open('" + url + "', '_blank');";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_New_Tab", fullURL, true);
            //string Text = "option=RadioReport&Inv=" + invno + "&UHID=" + UHID + "&TestID=" + TestID;
            //string encryptedurl = HttpUtility.UrlEncode(Commonfunction.Encrypt(Text));
            //Response.Redirect(string.Format("../MedRadTemplate/ReportViewer.aspx?ID=encryptedurl", ID));
        }
        protected void ddl_gender_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(EditedDocuemntID))
            {
                DocumentManager.CloseDocument(DocumentManager.FindDocument(EditedDocuemntID).DocumentId);
                EditedDocuemntID = string.Empty;
            }
            int testID = Convert.ToInt32(lbl_testID.Text);
            Int64 UHID = Convert.ToInt64(lbl_uhids.Text);
            string InvNo = lbl_invnumber.Text;
            int GenderID = Convert.ToInt32(lbl_genderID.Text);
            int TemplateID = Convert.ToInt32(ddl_templatetype.SelectedValue == "" ? "0" : ddl_templatetype.SelectedValue);
            bindPatientreport(testID, UHID, InvNo, GenderID, TemplateID);
            BindReportDetails();
            btn_print.Visible = false;
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
                        cmd.CommandText = "usp_MDQ_util_GetRadTemplatesBySexID";
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
        private DataTable GetRadReport(int ID, Int64 UHID, string INVNo, int TemplateID)
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
                        cmd.CommandText = "usp_MDQ_Rad_Get_PatientReportByTestID";
                        cmd.Parameters.Add("@TestID", SqlDbType.Int).Value = ID;
                        cmd.Parameters.Add("@TemplateID", SqlDbType.Int).Value = TemplateID;
                        cmd.Parameters.Add("@UHID", SqlDbType.BigInt).Value = UHID;
                        cmd.Parameters.Add("@INVNo", SqlDbType.VarChar).Value = INVNo;
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        sda.Fill(dt);
                    }
                }
                return dt;
            }
        }
        protected void UpdateDocs(byte[] Docbyte, int ID)
        {
            byte[] byteImage;
            string pdfBase64;
            using (MemoryStream ms = new MemoryStream())
            {
                Richteditor.ExportToPdf(ms);
                byteImage = ms.ToArray();
                pdfBase64 = Convert.ToBase64String(byteImage);
            }
            string constr = ConfigurationManager.ConnectionStrings["SqlConnectionString11"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("usp_MDQ_util_Updater_RadioPatientReport"))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection = con;
                        cmd.Parameters.Add("@Docbyte", SqlDbType.VarChar).Value = System.Text.Encoding.UTF8.GetString(Docbyte);
                        cmd.Parameters.Add("@ReportImage", SqlDbType.Image).Value = byteImage;
                        cmd.Parameters.Add("@Base64ReportImage", SqlDbType.NVarChar).Value = pdfBase64;
                        
                        cmd.Parameters.Add("@ID", SqlDbType.Int).Value = ID;
                        cmd.Parameters.Add("@InvNo", SqlDbType.VarChar).Value = lbl_invnumber.Text;
                        cmd.Parameters.Add("@TestID", SqlDbType.Int).Value = Convert.ToUInt32(lbl_testID.Text == "" ? "0" : lbl_testID.Text);
                        cmd.Parameters.Add("@GenID", SqlDbType.Int).Value = Convert.ToInt32(lbl_genderID.Text == "" ? "0" : lbl_genderID.Text);
                        cmd.Parameters.Add("@TemplateType", SqlDbType.Int).Value = Convert.ToInt32(ddl_templatetype.SelectedValue == "" ? "0" : ddl_templatetype.SelectedValue);
                        cmd.Parameters.Add("@VerifiedBy", SqlDbType.Int).Value = Convert.ToInt32(ddl_verifiedby.SelectedValue == "" ? "0" : ddl_verifiedby.SelectedValue);
                        cmd.Parameters.Add("@UHID", SqlDbType.BigInt).Value = Convert.ToInt64(lbl_uhids.Text);
                        cmd.Parameters.Add("@EmployeeID", SqlDbType.BigInt).Value = LogData.EmployeeID;
                        cmd.Parameters.Add("@HospitalID", SqlDbType.Int).Value = LogData.HospitalID;
                        cmd.Parameters.Add("@FinancialyearID", SqlDbType.Int).Value = LogData.FinancialYearID;
                        cmd.Parameters.Add("@Output", SqlDbType.SmallInt).Direction = ParameterDirection.Output;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        int result = Convert.ToInt32(cmd.Parameters.Add("@Output", SqlDbType.SmallInt).Direction);
                        if (result == 1)
                        {
                            btn_print.Visible = true;
                            Messagealert_.ShowMessage(lblmessage, "save", 1);
                            div1.Visible = true;
                            div1.Attributes["class"] = "SucessAlert";
                            bindgrid();
                        }
                        if (result == 2)
                        {
                            Messagealert_.ShowMessage(lblmessage, "update", 1);
                            div1.Visible = true;
                            div1.Attributes["class"] = "SucessAlert";
                            bindgrid();
                        }
                        con.Close();
                    }
                    catch (Exception ex)
                    {
                        PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                        // LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                        lblmessage.Text = ExceptionMessage.GetMessage(ex);
                        Messagealert_.ShowMessage(lblmessage, "system", 1);
                        div2.Attributes["class"] = "FailAlert";
                    }
                }
            }
        }
        protected void ddl_verifiedby_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindReportDetails();
        }
        protected void txt_testnames_TextChanged(object sender, EventArgs e)
        {
            bindgrid();
        }
    }
}
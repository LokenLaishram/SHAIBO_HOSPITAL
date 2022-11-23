using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedUtilityBO;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.MedUtilityData;
using Mediqura.Utility;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using Mediqura.Web.MedCommon;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mediqura.Web.MedLab
{
    public partial class SampleCollection : BasePage
    {
        public static String Inv = "";
        public static String UHID = "";
        public static String PatName = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlbind();
                lblmessage.Visible = false;
            }
        }
        private void ddlbind()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddltakenby, mstlookup.GetLookupsList(LookupName.LabTech));
            Commonfunction.PopulateDdl(ddl_referal, mstlookup.GetLookupsList(LookupName.Labconsultant));
            Commonfunction.PopulateDdl(ddl_patienttype, mstlookup.GetLookupsList(LookupName.PatientCatagory));
            txt_datefrom.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            txt_dateto.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetInv(string prefixText, int count, string contextKey)
        {
            SampleCollectionData Objpaic = new SampleCollectionData();
            LabSampleCollctionBO objInfoBO = new LabSampleCollctionBO();
            List<SampleCollectionData> getResult = new List<SampleCollectionData>();
            Objpaic.Investigationumber = prefixText;
            Objpaic.PatientTypeID = Convert.ToInt32(contextKey);
            getResult = objInfoBO.GetLabInvestigationno(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].Investigationumber.ToString());
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
            getResult = objInfoBO.GetLabPatientNames(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].PatientName.ToString());
            }
            return list;
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

            if (ddl_status.SelectedIndex == 0)
            {
                if (txt_invno.Text.Trim() == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "InvNo", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    txt_invno.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                    div1.Visible = false;
                }
                ddltakenby.Attributes.Remove("disabled");
                btnsave.Attributes.Remove("disabled");
            }
            else
            {
                btnsave.Attributes["disabled"] = "disabled";
                ddltakenby.Attributes["disabled"] = "disabled";
            }

            bindgrid();
        }
        private void bindgrid()
        {
            try
            {
                List<SampleCollectionData> lstemp = GetLabTestSamplelist(0);
                if (lstemp.Count > 0)
                {
                    Inv = txt_invno.Text;
                    PatName = lstemp[0].PatientName.ToString();
                    gvSample.DataSource = lstemp;
                    gvSample.DataBind();
                    gvSample.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total: " + lstemp[0].MaximumRows.ToString() + " Record(s) found.", 1);
                    divmsg3.Visible = true;
                    divmsg3.Attributes["class"] = "SucessAlert";
                   // ddltakenby.SelectedValue = lstemp[0].SampleCollectedBy.ToString();
                    txt_Reamrks.Text = lstemp[0].Comment.ToString();

                }
                else
                {
                    gvSample.DataSource = null;
                    gvSample.DataBind();
                    gvSample.Visible = true;
                    lblresult.Visible = false;
                    ddltakenby.SelectedIndex = 0;
                    txt_Reamrks.Text = "";

                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
            }
        }
        private List<SampleCollectionData> GetLabTestSamplelist(int p)
        {
            SampleCollectionData objsample = new SampleCollectionData();
            LabSampleCollctionBO objlabBO = new LabSampleCollctionBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            objsample.Investigationumber = txt_invno.Text.Trim() == "" ? "" : txt_invno.Text.Trim();
            objsample.PatientTypeID = Convert.ToInt32(ddl_patienttype.SelectedValue == "" ? "0" : ddl_patienttype.SelectedValue);
            objsample.PatientName = txt_name.Text.Trim() == "" ? "" : txt_name.Text.Trim();
            DateTime from = txt_datefrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txt_datefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txt_dateto.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txt_dateto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objsample.DateFrom = from;
            objsample.DateTo = To;
            objsample.ConsultantID = Convert.ToInt64(ddl_referal.SelectedValue == "" ? "0" : ddl_referal.SelectedValue);
            objsample.CollectionStatus = Convert.ToInt32(ddl_status.SelectedValue == "" ? "0" : ddl_status.SelectedValue);
            return objlabBO.GetLabSampleCollectedDetails(objsample);
        }
        protected void btnreset_Click(object sender, EventArgs e)
        {

            //lblresult.Text = "";
            lblmessage2.Text = "";
            lblmessage2.Visible = false;
            //lblresult.Visible = false;
            ddltakenby.SelectedIndex = 0;
            //lblresult.Visible = false;
            divmsg2.Visible = false;
            //ddl_referal.SelectedIndex = 0;
            //txt_name.Text = "";
            //ddl_patienttype.SelectedIndex = 0;
            //txt_invno.Text = "";
            gvSampleDetail.DataSource = null;
            gvSampleDetail.DataBind();
            gvSampleDetail.Visible = false;
            txt_Reamrks.Text = "";
            btnsave.Attributes.Remove("disabled");
            //txt_invno.Attributes.Remove("disabled");
            ddltakenby.Attributes.Remove("disabled");
            txt_invList.Text = "";
            txt_nameList.Text = "";
            TabPanel1.Visible = false;
            tabcontainerSampleCollection.ActiveTabIndex = 0;
        }
        protected void btnresets_Click(object sender, EventArgs e)
        {

            lblresult.Text = "";
            lblmessage.Text = "";
            lblmessage.Visible = false;
            lblresult.Visible = false;
            //ddltakenby.SelectedIndex = 0;
            lblresult.Visible = false;
            divmsg3.Visible = false;
            ddl_referal.SelectedIndex = 0;
            txt_name.Text = "";
            ddl_patienttype.SelectedIndex = 0;
            txt_invno.Text = "";
            gvSample.DataSource = null;
            gvSample.DataBind();
            gvSample.Visible = false;
            //txt_Reamrks.Text = "";
            //btnsave.Attributes.Remove("disabled");
            txt_invno.Attributes.Remove("disabled");
            //ddltakenby.Attributes.Remove("disabled");
            TabPanel1.Visible = false;
            tabcontainerSampleCollection.ActiveTabIndex = 0;
        }
        protected void txt_BillNo_TextChanged(object sender, EventArgs e)
        {

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
                    Messagealert_.ShowMessage(lblmessage2, "SaveEnable", 0);
                    divmsg2.Visible = true;
                    divmsg2.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }

                if (txt_invList.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage2, "InvNo", 0);
                    divmsg2.Visible = true;
                    divmsg2.Attributes["class"] = "FailAlert";
                    txt_invList.Focus();
                    return;
                }
                else
                {
                    lblmessage2.Visible = false;
                    divmsg2.Visible = false;
                }
                if (txt_Reamrks.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage2, "Casehistory", 0);
                    divmsg2.Visible = true;
                    divmsg2.Attributes["class"] = "FailAlert";
                    txt_Reamrks.Focus();
                    return;
                }
                else
                {
                    lblmessage2.Visible = false;
                    divmsg2.Visible = false;
                }
                if (ddltakenby.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage2, "takenBY", 0);
                    divmsg2.Visible = true;
                    divmsg2.Attributes["class"] = "FailAlert";
                    ddltakenby.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;

                }
                List<SampleCollectionData> Listbill = new List<SampleCollectionData>();
                LabSampleCollctionBO objLabSampleBO = new LabSampleCollctionBO();
                SampleCollectionData objSampleData = new SampleCollectionData();
                foreach (GridViewRow row in gvSampleDetail.Rows)
                {

                    DropDownList testcenterID = (DropDownList)gvSampleDetail.Rows[row.RowIndex].Cells[0].FindControl("ddl_testcenter");
                    Label testID = (Label)gvSampleDetail.Rows[row.RowIndex].Cells[0].FindControl("lblTestID");
                    Label ID = (Label)gvSampleDetail.Rows[row.RowIndex].Cells[0].FindControl("lblID");
                    Label Inv = (Label)gvSampleDetail.Rows[row.RowIndex].Cells[0].FindControl("lbl_invnumber");
                    CheckBox chkcollection = (CheckBox)gvSampleDetail.Rows[row.RowIndex].Cells[0].FindControl("chk_collection");
                    SampleCollectionData ObjDetails = new SampleCollectionData();
                    if (chkcollection.Checked)
                    {
                        ObjDetails.ID = Convert.ToInt64(ID.Text == "" ? "0" : ID.Text);
                        ObjDetails.LabServiceID = Convert.ToInt32(testID.Text == "" ? "0" : testID.Text);
                        ObjDetails.Investigationumber = Inv.Text.Trim();
                        ObjDetails.CollectionStatus = Convert.ToInt32(chkcollection.Checked ? "1" : "0");
                        ObjDetails.IsOutsourcedTest = 0;
                        ObjDetails.TestCenterID = Convert.ToInt32(testcenterID.Text == "" ? "0" : testcenterID.Text);
                        ObjDetails.LabServiceID = Convert.ToInt32(testID.Text == "" ? "0" : testID.Text);
                        Listbill.Add(ObjDetails);
                    }
                }
                if (Listbill.Count < 1)
                {
                    Messagealert_.ShowMessage(lblmessage2, "LabCheck", 0);
                    divmsg2.Visible = true;
                    divmsg2.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    objSampleData.XMLData = XmlConvertor.SampleDatatoXML(Listbill).ToString();
                    objSampleData.TakenBy = Convert.ToInt32(ddltakenby.SelectedValue == "" ? "0" : ddltakenby.SelectedValue);
                    objSampleData.EmployeeID = LogData.EmployeeID;
                    objSampleData.FinancialYearID = LogData.FinancialYearID;
                    objSampleData.HospitalID = LogData.HospitalID;
                    objSampleData.Comment = txt_Reamrks.Text.Trim();
                    objSampleData.IPaddress = LogData.IPaddress;
                    int result = objLabSampleBO.UpdateSampleCollectionDetails(objSampleData);
                    if (result == 1 || result == 2)
                    {
                        lblmessage.Visible = true;
                        Messagealert_.ShowMessage(lblmessage2, result == 1 ? "save" : "update", 1);
                        divmsg2.Visible = true;
                        divmsg2.Attributes["class"] = "SucessAlert";
                        ddl_status.SelectedIndex = 1;
                        bindgrid();
                        btnsave.Attributes["disabled"] = "disabled";
                        txt_invno.Attributes["disabled"] = "disabled";
                    }
                    else if (result == 5)
                    {
                        divmsg2.Visible = true;
                        divmsg2.Attributes["class"] = "FailAlert";
                        Messagealert_.ShowMessage(lblmessage2, "duplicate", 0);
                    }
                    else
                    {
                        Messagealert_.ShowMessage(lblmessage2, "system", 0);
                        divmsg2.Visible = true;
                        divmsg2.Attributes["class"] = "FailAlert";
                    }
                }
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                lblmessage2.Text = ExceptionMessage.GetMessage(ex);
            }
        }
        protected void gvSample_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Search")
                {
                    SampleCollectionData objlabData = new SampleCollectionData();
                    LabSampleCollctionBO objlabBO = new LabSampleCollctionBO();
                    IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = gvSample.Rows[i];
                    Label UHID = (Label)gr.Cells[0].FindControl("lbl_UHID");
                    LinkButton InvNumber = (LinkButton)gr.Cells[0].FindControl("lnk_invnumber");
                    objlabData.Investigationumber = InvNumber.Text;
                    List<SampleCollectionData> List = new List<SampleCollectionData>();
                    List = objlabBO.InvDetailByInvNo(objlabData);
                    if (List.Count > 0)
                    {
                        txt_nameList.Text = List[0].PatientName.ToString();
                        txt_invList.Text = List[0].Investigationumber.ToString();
                        tabcontainerSampleCollection.ActiveTabIndex = 1;
                        gvSampleDetail.DataSource = List;
                        gvSampleDetail.DataBind();
                        gvSampleDetail.Visible = true;
                        Messagealert_.ShowMessage(lblresult, "Total: " + List[0].MaximumRows.ToString() + " Record(s) found.", 1);
                        divmsg3.Visible = true;
                        divmsg3.Attributes["class"] = "SucessAlert";
                        ddltakenby.SelectedValue = List[0].SampleCollectedBy.ToString();
                        txt_Reamrks.Text = List[0].Comment.ToString();
                        TabPanel1.Visible = true;
                    }
                    else
                    {
                        gvSampleDetail.DataSource = null;
                        gvSampleDetail.DataBind();
                        gvSampleDetail.Visible = true;
                        lblresult.Visible = false;
                        ddltakenby.SelectedIndex = 0;
                        txt_Reamrks.Text = "";
                        TabPanel1.Visible = false;
                    }

                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
                div1.Attributes["class"] = "FailAlert";
                div1.Visible = true;
                return;
            }
        }
        protected void gvSampleDetail_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                DropDownList ddl_testcenter = e.Row.FindControl("ddl_testcenter") as DropDownList;
                Label centerID = e.Row.FindControl("lbltestcenterid") as Label;
                Label urgency = e.Row.FindControl("lbl_urgencyid") as Label;
                Label Invnymber = e.Row.FindControl("lbl_invnumber") as Label;
                CheckBox chkcollection = e.Row.FindControl("chk_collection") as CheckBox;
                Label collectionstatus = e.Row.FindControl("lbl_chkcoll_ID") as Label;
                MasterLookupBO mstlookup = new MasterLookupBO();
                Commonfunction.PopulateDdl(ddl_testcenter, mstlookup.GetLookupsList(LookupName.TestCenter));
                if (centerID.Text != "0")
                {
                    ddl_testcenter.Items.FindByValue(centerID.Text).Selected = true;
                }
                if (collectionstatus.Text == "1")
                {
                    chkcollection.Checked = true;
                }
                else
                {
                    chkcollection.Checked = false;
                }
                if (urgency.Text == "0" || urgency.Text == "1")
                {
                    e.Row.Cells[1].BackColor = System.Drawing.Color.Green;
                    Invnymber.ForeColor = System.Drawing.Color.White;
                }
                if (urgency.Text == "2")
                {
                    e.Row.Cells[1].BackColor = System.Drawing.Color.Yellow;
                    Invnymber.ForeColor = System.Drawing.Color.Black;
                }
                if (urgency.Text == "3")
                {
                    e.Row.Cells[1].BackColor = System.Drawing.Color.Red;
                    Invnymber.ForeColor = System.Drawing.Color.Black;
                }
                if (ddl_status.SelectedIndex == 1)
                {
                    chkcollection.Enabled = false;

                }
                else
                {
                    chkcollection.Enabled = true;
                }
            }
        }
        protected void gvSampleDetail_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Print")
                {
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = gvSampleDetail.Rows[i];
                    Label lbl_invnumber = (Label)gr.Cells[0].FindControl("lbl_invnumber");
                    Label Test = (Label)gr.Cells[0].FindControl("lbl_testname");
                    string code = Commonfunction.getBarcode(lbl_invnumber.Text.ToString());
                    String barcode = " <tr><td align=\"center\"><label style=\"font-size: 9px; text - align: left;\" >" + Test.Text + "</label><br><img style=\"width:60%\" src=\"" + code + "\"/> " +
                        "</td><td align=\"center\"><label style=\"font-size: 9px; text - align: left;\" >" + PatName + "</label><br><label style=\"font-size: 9px;\">" + lbl_invnumber.Text + "</label></td></tr>" +
                             "<tr><td align=\"center\"><label style=\"font-size: 9px;\">" + lbl_invnumber.Text + "</label></td>" +
                             "<td align=\"right\"></td> </tr>";
                    ltBarcode.Text = barcode;
                    this.MDBarcode.Show();
                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);

            }
        }
        protected void ddl_patienttype_SelectedIndexChanged(object sender, EventArgs e)
        {
            AutoCompleteExtender1.ContextKey = ddl_patienttype.SelectedValue == "" ? "0" : ddl_patienttype.SelectedValue;
            AutoCompleteExtender2.ContextKey = ddl_patienttype.SelectedValue == "" ? "0" : ddl_patienttype.SelectedValue;
        }


        protected void btnClose_Click(object sender, EventArgs e)
        {

        }

        protected void btn_print_Click(object sender, EventArgs e)
        {
            string invno= txt_invList.Text== "" ? "" : txt_invList.Text;
            string param = "option=TestRequisition&Inv=" + invno;
            Commonfunction common = new Commonfunction();
            string ecryptstring = common.Encrypt(param);
            string baseurl = "../MedLab/Report/ReportViewer.aspx?ID=" + ecryptstring;
            string fullURL = "window.open('" + baseurl + "', '_blank');";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_New_Tab", fullURL, true);
        }
    }
}
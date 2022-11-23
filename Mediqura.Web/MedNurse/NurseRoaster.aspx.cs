using System;
using System.Collections.Generic;
using System.Linq;
using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedBill;
using Mediqura.BOL.PatientBO;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.PatientData;
using Mediqura.Utility;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using Mediqura.Web.MedCommon;
using OnBarcode.Barcode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using ClosedXML.Excel;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Data;
using System.Reflection;
using Mediqura.CommonData.MedUtilityData;
using Mediqura.BOL.MedUtilityBO;
using Mediqura.BOL.MedBillBO;
using Mediqura.CommonData.MedHrData;
using Mediqura.BOL.MedHrBO;
using Mediqura.CommonData.AdmissionData;
using Mediqura.BOL.AdmissionBO;
using Mediqura.BOL.MedNurseBO;
using Mediqura.CommonData.MedNurseData;

namespace Mediqura.Web.MedNurse
{
    public partial class NurseRoaster : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindddl();
                page_setting();
            }
        }
        private void bindddl()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddl_nurseType, mstlookup.GetLookupsList(LookupName.NurseType));
            Commonfunction.PopulateDdl(ddl_month, mstlookup.GetLookupsList(LookupName.month));
            Commonfunction.PopulateDdl(ddl_year, mstlookup.GetLookupsList(LookupName.Year));
            //Commonfunction.PopulateDdl(ddl_week, mstlookup.GetLookupsList(LookupName.week));


        }
        protected void page_setting()  //  to bind current month and year
        {

            String cmon = DateTime.Now.ToString("MMMM");
            String cyear = DateTime.Now.ToString("yyyy");

            ddl_month.Items.FindByText(cmon).Selected = true;
            ddl_year.Items.FindByText(cyear).Selected = true;
            Commonfunction.Insertzeroitemindex(ddl_nurse);
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddl_week, mstlookup.Getweek(Convert.ToInt32(ddl_month.SelectedValue == "" ? "0" : ddl_month.SelectedValue), Convert.ToInt32(ddl_year.SelectedItem.Text == "" ? "0" : ddl_year.SelectedItem.Text)));
        }
        protected void bindgrid()
        {
            string D1 = ""; string D2 = ""; string D3 = ""; string D4 = ""; string D5 = ""; string D6 = ""; string D7 = "";
            try
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
                if (ddl_nurseType.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "NurseType", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (ddl_week.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "Week", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }

                List<NurseRoasterData> objSchedule = GetNurseSchedule(0);
                if (objSchedule.Count > 0)
                {


                    if (ViewState["d1"] == null)
                    {
                        string concateD1 = "";
                        for (int i = 0; i <= 1; i++)
                        {
                            if (objSchedule[i].Day_I.ToString() == null)
                            {
                                D1 = "";
                            }
                            else
                            {
                                D1 = (objSchedule[i].Day_I.ToString());
                                if (D1 == "0")
                                {
                                    D1 = "";
                                }
                            }
                            concateD1 = concateD1 + D1;
                        }
                        ViewState["d1"] = concateD1;

                    }

                    if (ViewState["d2"] == null)
                    {
                        string concateD2 = "";
                        for (int i = 0; i <= 1; i++)
                        {
                            if (objSchedule[i].Day_II.ToString() == null)
                            {
                                D2 = "";
                            }
                            else
                            {
                                D2 = (objSchedule[i].Day_II.ToString());
                                if (D2 == "0")
                                {
                                    D2 = "";
                                }
                            }
                            concateD2 = concateD2 + D2;
                        }
                        ViewState["d2"] = concateD2;

                    }
                    if (ViewState["d3"] == null)
                    {
                        string concateD3 = "";
                        for (int i = 0; i <= 1; i++)
                        {
                            if (objSchedule[i].Day_III.ToString() == null)
                            {
                                D3 = "";
                            }
                            else
                            {
                                D3 = (objSchedule[i].Day_III.ToString());
                                if (D3 == "0")
                                {
                                    D3 = "";
                                }
                            }
                            concateD3 = concateD3 + D3;
                        }
                        ViewState["d3"] = concateD3;

                    }
                    if (ViewState["d4"] == null)
                    {
                        string concateD4 = "";
                        for (int i = 0; i <= 1; i++)
                        {
                            if (objSchedule[i].Day_IV.ToString() == null)
                            {
                                D4 = "";
                            }
                            else
                            {
                                D4 = (objSchedule[i].Day_IV.ToString());
                                if (D4 == "0")
                                {
                                    D4 = "";
                                }
                            }
                            concateD4 = concateD4 + D4;
                        }
                        ViewState["d4"] = concateD4;

                    }
                    if (ViewState["d5"] == null)
                    {
                        string concateD5 = "";
                        for (int i = 0; i <= 1; i++)
                        {
                            if (objSchedule[i].Day_V.ToString() == null)
                            {
                                D5 = "";
                            }
                            else
                            {
                                D5 = (objSchedule[i].Day_V.ToString());
                                if (D5 == "0")
                                {
                                    D5 = "";
                                }
                            }
                            concateD5 = concateD5 + D5;
                        }
                        ViewState["d5"] = concateD5;

                    }
                    if (ViewState["d6"] == null)
                    {
                        string concateD6 = "";
                        for (int i = 0; i <= 1; i++)
                        {
                            if (objSchedule[i].Day_VI.ToString() == null)
                            {
                                D6 = "";
                            }
                            else
                            {
                                D6 = (objSchedule[i].Day_VI.ToString());
                                if (D6 == "0")
                                {
                                    D6 = "";
                                }
                            }
                            concateD6 = concateD6 + D6;
                        }
                        ViewState["d6"] = concateD6;

                    }
                    if (ViewState["d7"] == null)
                    {
                        string concateD7 = "";
                        for (int i = 0; i <= 1; i++)
                        {
                            if (objSchedule[i].Day_VII.ToString() == null)
                            {
                                D7 = "";
                            }
                            else
                            {
                                D7 = (objSchedule[i].Day_VII.ToString());
                                if (D7 == "0")
                                {
                                    D7 = "";
                                }
                            }
                            concateD7 = concateD7 + D7;
                        }
                        ViewState["d7"] = concateD7;

                    }


                    GvNurseRoaster.DataSource = objSchedule;
                    GvNurseRoaster.DataBind();
                    GvNurseRoaster.Visible = true;
                    //Messagealert_.ShowMessage(lblresult, "Total: " + objSchedule[0].MaximumRows.ToString() + " Record(s) found.", 1);
                    divmsg3.Attributes["class"] = "SucessAlert";
                    divmsg3.Visible = false;
                    ddlexport.Visible = true;
                    btnexport.Visible = true;
                    btnsave.Visible = true;
                    
                    ddl_nurse.Attributes["disabled"] = "disabled";
                    ddl_nurseType.Attributes["disabled"] = "disabled";
                    ddl_week.Attributes["disabled"] = "disabled";
                    ddl_month.Attributes["disabled"] = "disabled";
                    ddl_year.Attributes["disabled"] = "disabled";
                }
                else
                {
                    divmsg3.Visible = false;
                    GvNurseRoaster.DataSource = null;
                    GvNurseRoaster.DataBind();
                    GvNurseRoaster.Visible = true;
                    divmsg3.Visible = false;
                    lblresult.Visible = false;
                    ddlexport.Visible = false;
                    btnexport.Visible = false;
                    ddl_nurse.Attributes.Remove("disabled");
                    ddl_nurseType.Attributes.Remove("disabled");
                    ddl_week.Attributes.Remove("disabled");
                    ddl_month.Attributes.Remove("disabled");
                    ddl_year.Attributes.Remove("disabled");

                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
            }
        }
        public List<NurseRoasterData> GetNurseSchedule(int curIndex)
        {

            NurseRoasterData objSchedule = new NurseRoasterData();
            NurseRoasterBO objscheduleBO = new NurseRoasterBO();
            objSchedule.nurseType = Convert.ToInt32(ddl_nurseType.SelectedValue == "" ? "0" : ddl_nurseType.SelectedValue);
            objSchedule.nurseID = Convert.ToInt32(ddl_nurse.SelectedValue == "" ? "0" : ddl_nurse.SelectedValue);
            objSchedule.weekID = Convert.ToInt32(ddl_week.SelectedValue == "" ? "0" : ddl_week.SelectedValue);
            objSchedule.monthID = Convert.ToInt32(ddl_month.SelectedValue == "" ? "0" : ddl_month.SelectedValue);
            objSchedule.yearID = Convert.ToInt32(ddl_year.SelectedItem.Text == "" ? "0" : ddl_year.SelectedItem.Text);
            return objscheduleBO.GetSchedule(objSchedule);
        }
        public List<NurseRoasterData> GetNurseScheduleExport(int curIndex)
        {

            NurseRoasterData objSchedule = new NurseRoasterData();
            NurseRoasterBO objscheduleBO = new NurseRoasterBO();
            objSchedule.nurseType =Convert.ToInt32(hdnNurseType.Value);
            objSchedule.nurseID = Convert.ToInt32(hdnNurseId.Value);
            objSchedule.weekID = Convert.ToInt32(hdnweek.Value);
            objSchedule.monthID = Convert.ToInt32(hdnmonth.Value);
            objSchedule.yearID = Convert.ToInt32(hdnyear.Value);
            return objscheduleBO.GetSchedule(objSchedule);
        }

        protected void btnsearch_Click(object sender, System.EventArgs e)
        {
            bindgrid();
            hdnNurseType.Value = ddl_nurseType.SelectedValue;
            hdnNurseId.Value = ddl_nurse.SelectedValue;
            hdnweek.Value = ddl_week.SelectedValue;
            hdnmonth.Value = ddl_month.SelectedValue;
            hdnyear.Value = ddl_year.SelectedItem.Text;

        }

        protected void btnreset_Click(object sender, System.EventArgs e)
        {
            GvNurseRoaster.DataSource = null;
            GvNurseRoaster.DataBind();
            GvNurseRoaster.Visible = false;
            ddl_nurse.SelectedIndex = 0;
            ddl_week.SelectedIndex = 0;
            ddl_nurseType.SelectedIndex = 0;
            btnexport.Visible = false;
            ddlexport.Visible = false;
            lblmessage.Visible = false;
            lblresult.Visible = false;
            lblresult.Text = "";
            lblmessage.Visible = false;
            btnsave.Visible = false;
            div1.Visible = false;
            lblmsg.Visible = false;
            Commonfunction.Insertzeroitemindex(ddl_nurse);
            Commonfunction.Insertzeroitemindex(ddl_week);
            page_setting();
            ddl_nurse.Attributes.Remove("disabled");
            ddl_nurseType.Attributes.Remove("disabled");
            ddl_week.Attributes.Remove("disabled");
            ddl_month.Attributes.Remove("disabled");
            ddl_year.Attributes.Remove("disabled");
            ViewState["d1"] = null;
            ViewState["d2"] = null;
            ViewState["d3"] = null;
            ViewState["d4"] = null;
            ViewState["d5"] = null;
            ViewState["d6"] = null;
            ViewState["d7"] = null;



        }

        protected void GvNurseRoaster_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                if (ViewState["d1"] != null)
                {
                    ((Label)e.Row.FindControl("lbl_day1head") as Label).Text = ViewState["d1"].ToString();
                    if (ViewState["d1"].ToString().Contains("Sunday"))
                    {
                        e.Row.Cells[3].BackColor = System.Drawing.Color.YellowGreen;

                    }
                }
                if (ViewState["d2"] != null)
                {
                    ((Label)e.Row.FindControl("lbl_day2head") as Label).Text = ViewState["d2"].ToString();
                    if (ViewState["d2"].ToString().Contains("Sunday"))
                    {
                        e.Row.Cells[4].BackColor = System.Drawing.Color.YellowGreen;
                    }
                }
                if (ViewState["d3"] != null)
                {
                    ((Label)e.Row.FindControl("lbl_day3head") as Label).Text = ViewState["d3"].ToString();
                    if (ViewState["d3"].ToString().Contains("Sunday"))
                    {
                        e.Row.Cells[5].BackColor = System.Drawing.Color.YellowGreen;
                    }
                }
                if (ViewState["d4"] != null)
                {
                    ((Label)e.Row.FindControl("lbl_day4head") as Label).Text = ViewState["d4"].ToString();
                    if (ViewState["d4"].ToString().Contains("Sunday"))
                    {
                        e.Row.Cells[6].BackColor = System.Drawing.Color.YellowGreen;
                    }
                }
                if (ViewState["d5"] != null)
                {
                    ((Label)e.Row.FindControl("lbl_day5head") as Label).Text = ViewState["d5"].ToString();
                    if (ViewState["d5"].ToString().Contains("Sunday"))
                    {
                        e.Row.Cells[7].BackColor = System.Drawing.Color.YellowGreen;

                    }
                }
                if (ViewState["d6"] != null)
                {
                    ((Label)e.Row.FindControl("lbl_day6head") as Label).Text = ViewState["d6"].ToString();
                    if (ViewState["d6"].ToString().Contains("Sunday"))
                    {
                        e.Row.Cells[8].BackColor = System.Drawing.Color.YellowGreen;
                    }
                }
                if (ViewState["d7"] != null)
                {
                    ((Label)e.Row.FindControl("lbl_day7head") as Label).Text = ViewState["d7"].ToString();
                    if (ViewState["d7"].ToString().Contains("Sunday"))
                    {
                        e.Row.Cells[9].BackColor = System.Drawing.Color.YellowGreen;
                    }
                }
           



            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label nurseID = (Label)e.Row.FindControl("lbl_nurID");
                Label tb_ID = (Label)e.Row.FindControl("lbl_ID");
                if (nurseID.Text == "0")
                {
                    e.Row.Visible = false;
                }
                if (ViewState["d1"].ToString().Contains("Sunday"))
                {
                    e.Row.Cells[3].BackColor = System.Drawing.Color.YellowGreen;
                    //e.Row.Cells[3].ForeColor = System.Drawing.Color.YellowGreen;

                }
                if (ViewState["d2"].ToString().Contains("Sunday"))
                {
                    e.Row.Cells[4].BackColor = System.Drawing.Color.YellowGreen;
                    e.Row.Cells[4].ForeColor = System.Drawing.Color.YellowGreen;
                }
                if (ViewState["d3"].ToString().Contains("Sunday"))
                {
                    e.Row.Cells[5].BackColor = System.Drawing.Color.YellowGreen;
                }
                if (ViewState["d4"].ToString().Contains("Sunday"))
                {
                    e.Row.Cells[6].BackColor = System.Drawing.Color.YellowGreen;
                }
                if (ViewState["d5"].ToString().Contains("Sunday"))
                {
                    e.Row.Cells[7].BackColor = System.Drawing.Color.YellowGreen;
                }
                if (ViewState["d6"].ToString().Contains("Sunday"))
                {
                    e.Row.Cells[8].BackColor = System.Drawing.Color.YellowGreen;
                }
                if (ViewState["d7"].ToString().Contains("Sunday"))
                {
                    e.Row.Cells[9].BackColor = System.Drawing.Color.YellowGreen;
                }
            }
        }

        protected void GvNurseRoaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GvNurseRoaster.PageIndex = e.NewPageIndex;
            bindgrid();
        }

        protected void btnsave_Click(object sender, System.EventArgs e)
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
            try
            {
                List<NurseRoasterData> Listroaster = new List<NurseRoasterData>();
                NurseRoasterBO objAppnmtBO = new NurseRoasterBO();
                NurseRoasterData objAppnmtData = new NurseRoasterData();
                foreach (GridViewRow row in GvNurseRoaster.Rows)
                {

                    Label tb_ID = (Label)GvNurseRoaster.Rows[row.RowIndex].Cells[0].FindControl("lbl_ID");
                    Label nurse_ID = (Label)GvNurseRoaster.Rows[row.RowIndex].Cells[0].FindControl("lbl_nurID");
                    Label nurse_name = (Label)GvNurseRoaster.Rows[row.RowIndex].Cells[0].FindControl("lbl_name");
                    TextBox Roast_d1 = (TextBox)GvNurseRoaster.Rows[row.RowIndex].Cells[0].FindControl("txt_day1");
                    TextBox Roast_d2 = (TextBox)GvNurseRoaster.Rows[row.RowIndex].Cells[0].FindControl("txt_day2");
                    TextBox Roast_d3 = (TextBox)GvNurseRoaster.Rows[row.RowIndex].Cells[0].FindControl("txt_day3");
                    TextBox Roast_d4 = (TextBox)GvNurseRoaster.Rows[row.RowIndex].Cells[0].FindControl("txt_day4");
                    TextBox Roast_d5 = (TextBox)GvNurseRoaster.Rows[row.RowIndex].Cells[0].FindControl("txt_day5");
                    TextBox Roast_d6 = (TextBox)GvNurseRoaster.Rows[row.RowIndex].Cells[0].FindControl("txt_day6");
                    TextBox Roast_d7 = (TextBox)GvNurseRoaster.Rows[row.RowIndex].Cells[0].FindControl("txt_day7");
                    NurseRoasterData objdetails = new NurseRoasterData();
                    if (Roast_d1.Text == "" && Roast_d2.Text == "" && Roast_d3.Text == "" && Roast_d4.Text == "" && Roast_d5.Text == "" && Roast_d6.Text == "" && Roast_d7.Text == "")
                    {
                        Messagealert_.ShowMessage(lblmessage, "BlankSchedule", 0);
                        div1.Visible = true;
                        div1.Attributes["class"] = "FailAlert";
                        Roast_d1.Focus();
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                        div1.Visible = false;

                    }
                    objdetails.ID = Convert.ToInt64(tb_ID.Text == "" ? "0" : tb_ID.Text);
                    objdetails.nurseID = Convert.ToInt32(nurse_ID.Text == "" ? "0" : nurse_ID.Text);
                    objdetails.NurseName = (nurse_name.Text == "" ? "0" : nurse_name.Text);
                    objdetails.Day_I = (Roast_d1.Text == "" ? "0" : Roast_d1.Text);
                    objdetails.Day_II = (Roast_d2.Text == "" ? "0" : Roast_d2.Text);
                    objdetails.Day_III = (Roast_d3.Text == "" ? "0" : Roast_d3.Text);
                    objdetails.Day_IV = (Roast_d4.Text == "" ? "0" : Roast_d4.Text);
                    objdetails.Day_V = (Roast_d5.Text == "" ? "0" : Roast_d5.Text);
                    objdetails.Day_VI = (Roast_d6.Text == "" ? "0" : Roast_d6.Text);
                    objdetails.Day_VII = (Roast_d7.Text == "" ? "0" : Roast_d7.Text);
                    Listroaster.Add(objdetails);

                }
                objAppnmtData.XMLData = XmlConvertor.RoasterDatatoXML(Listroaster).ToString();
                objAppnmtData.Nursedesgn = (ddl_nurseType.SelectedItem.Text == "" ? "0" : ddl_nurseType.SelectedItem.Text);
                objAppnmtData.yearID = Convert.ToInt32(ddl_year.SelectedItem.Text == "" ? "0" : ddl_year.SelectedItem.Text);
                objAppnmtData.monthID = Convert.ToInt32(ddl_month.SelectedValue == "" ? "0" : ddl_month.SelectedValue);
                objAppnmtData.weekID = Convert.ToInt32(ddl_week.SelectedValue == "" ? "0" : ddl_week.SelectedValue);
                objAppnmtData.EmployeeID = LogData.EmployeeID;
                objAppnmtData.HospitalID = LogData.HospitalID;

                int result = objAppnmtBO.UpdateRosterDetails(objAppnmtData);
                if (result == 1 || result == 2)
                {
                    Messagealert_.ShowMessage(lblmsg, result == 1 ? "save" : "update", 1);
                    div1.Attributes["class"] = "SucessAlert";
                    div1.Visible = true;
                    bindgrid();
                    return;
                }
                else if (result == 5)
                {
                    Messagealert_.ShowMessage(lblmsg, "duplicate", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                }
                else
                {
                    Messagealert_.ShowMessage(lblmsg, "system", 0);
                }
                btnsave.Enabled = false;

            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmsg, "system", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
            }
            btnsave.Enabled = false;
            
        }

        protected void btnexport_Click(object sender, System.EventArgs e)
        {
            if (LogData.ExportEnable == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "ExportEnable", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                return;
            }
            else
            {
                lblmessage.Visible = false;
            }
            if (ddlexport.SelectedIndex == 1)
            {
               ExportoExcel();
            }
            else if (ddlexport.SelectedIndex == 2)
            {
                ExportToPdf();
            }
            else
            {
                Messagealert_.ShowMessage(lblresult, "ExportType", 0);
                divmsg3.Attributes["class"] = "FailAlert";
                ddlexport.Focus();
                return;
            }
        }
        public void ExportToPdf()
        {
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    GvNurseRoaster.BorderStyle = BorderStyle.None;
                    //Hide the Column containing CheckBox
                    //GvNurseRoaster.Columns[10].Visible = false;
                    //GvAppoinmentSch.Columns[10].Visible = false;
                    //gvstockstatus.Columns[10].Visible = false;
                    //gvstockstatus.Columns[11].Visible = false;

                    GvNurseRoaster.RenderControl(hw);
                    GvNurseRoaster.HeaderRow.Style.Add("width", "15%");
                    GvNurseRoaster.HeaderRow.Style.Add("font-size", "10px");
                    GvNurseRoaster.Style.Add("text-decoration", "none");
                    GvNurseRoaster.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    GvNurseRoaster.Style.Add("font-size", "8px");
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 7f, 7f, 7f, 0f);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=NurseRoasterDetails.pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.Write(pdfDoc);
                    Response.End();
                }
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        protected void ExportoExcel()
        {
            DataTable dt = GetDatafromDatabase();
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "Deposit Details");
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=NurseRoasterDetails.xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                    ddlexport.SelectedIndex = 0;
                }
                Messagealert_.ShowMessage(lblresult, "Exported", 1);
                divmsg3.Attributes["class"] = "SucessAlert";
            }
        }
        protected DataTable GetDatafromDatabase()
        {
            List<NurseRoasterData> appmtSch = GetNurseScheduleExport(0);
            List<NurRoasterDataTOeXCEL> ListexcelData = new List<NurRoasterDataTOeXCEL>();
            int i = 0;
            foreach (NurseRoasterData row in appmtSch)
            {
                NurRoasterDataTOeXCEL Ecxeclpat = new NurRoasterDataTOeXCEL();
                Ecxeclpat.EmployeeID = appmtSch[i].EmployeeID;
                Ecxeclpat.NurseName = appmtSch[i].NurseName;
                Ecxeclpat.Day_I = appmtSch[i].Day_I;
                Ecxeclpat.Day_II = appmtSch[i].Day_II;
                Ecxeclpat.Day_III = appmtSch[i].Day_III;
                Ecxeclpat.Day_IV = appmtSch[i].Day_IV;
                Ecxeclpat.Day_V = appmtSch[i].Day_V;
                Ecxeclpat.Day_VI = appmtSch[i].Day_VI;
                Ecxeclpat.Day_VII = appmtSch[i].Day_VII;

                ListexcelData.Add(Ecxeclpat);
                i++;
            }
            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            DataTable dt = converter.ToDataTable(ListexcelData);
            return dt;
        }
        public class ListtoDataTableConverter
        {

            public DataTable ToDataTable<T>(List<T> items)
            {

                DataTable dataTable = new DataTable(typeof(T).Name);

                // Get all the properties

                PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

                foreach (PropertyInfo prop in Props)
                {

                    //  Setting column names as Property names
                    dataTable.Columns.Add(prop.Name);

                }

                foreach (T item in items)
                {

                    var values = new object[Props.Length];

                    for (int i = 0; i < Props.Length; i++)
                    {

                        //       inserting property values to datatable rows

                        values[i] = Props[i].GetValue(item, null);

                    }

                    dataTable.Rows.Add(values);

                }

                //     put a breakpoint here and check datatable

                return dataTable;

            }
        }

        protected void ddl_nurseType_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (ddl_nurseType.SelectedIndex > 0)
            {
                MasterLookupBO mstlookup = new MasterLookupBO();
                Commonfunction.PopulateDdl(ddl_nurse, mstlookup.GetNurseByNurseType(Convert.ToInt32(ddl_nurseType.SelectedValue)));
            }
        }

        protected void ddl_month_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (ddl_month.SelectedIndex > 0)
            {
                MasterLookupBO mstlookup = new MasterLookupBO();
                Commonfunction.PopulateDdl(ddl_week, mstlookup.Getweek(Convert.ToInt32(ddl_month.SelectedValue == "" ? "0" : ddl_month.SelectedValue), Convert.ToInt32(ddl_year.SelectedItem.Text == "" ? "0" : ddl_year.SelectedItem.Text)));

            }
        }


    }
}
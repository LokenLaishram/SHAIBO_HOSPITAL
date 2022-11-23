using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedUtilityBO;
using Mediqura.CommonData.Common;
using Mediqura.Utility;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using Mediqura.Web.MedCommon;
using Mediqura.BOL.AdmissionBO;
using Mediqura.CommonData.MedUtilityData;
using Mediqura.CommonData.AdmissionData;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Data;
using ClosedXML.Excel;
using System.Reflection;


namespace Mediqura.Web.MedReport
{
    public partial class DischargeReadyList : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindddl();
            }
        }
        private void bindddl()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddlDischargeBy, mstlookup.GetLookupsList(LookupName.Doctor));
            Commonfunction.PopulateDdl(ddl_ward, mstlookup.GetLookupsList(LookupName.IPDWardType));
          

        }
        protected void btnsearchList_Click(object sender, EventArgs e)
        {
            BindSummaryList();
        }
        protected void BindSummaryList()
        {
            try
            {
                if (LogData.SearchEnable == 0)
                {
                    Messagealert_.ShowMessage(lblmessage2, "SearchEnable", 0);
                    divmsg2.Visible = true;
                    divmsg2.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage2.Visible = false;
                }
                if (txtdatefromList.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage2, "DateRange", 0);
                    divmsg2.Visible = true;
                    divmsg2.Attributes["class"] = "FailAlert";
                    txtdatefromList.Focus();
                    return;
                }
                else
                {
                    lblmessage2.Visible = false;
                }
                if (txttoList.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage2, "DateRange", 0);
                    divmsg2.Visible = true;
                    divmsg2.Attributes["class"] = "FailAlert";
                    txttoList.Focus();
                    return;
                }
                else
                {
                    lblmessage2.Visible = false;
                }
                if (txtdatefromList.Text != "")
                {
                    if (Commonfunction.isValidDate(txtdatefromList.Text) == false)
                    {
                        Messagealert_.ShowMessage(lblmessage2, "ValidDatefrom", 0);
                        divmsg2.Attributes["class"] = "FailAlert";
                        divmsg2.Visible = true;
                        txtdatefromList.Focus();
                        return;
                    }
                }
                else
                {
                    lblmessage2.Visible = false;
                }
                if (txttoList.Text != "")
                {
                    if (Commonfunction.isValidDate(txttoList.Text) == false)
                    {
                        Messagealert_.ShowMessage(lblmessage2, "ValidDateto", 0);
                        divmsg2.Attributes["class"] = "FailAlert";
                        divmsg2.Visible = true;
                        txttoList.Focus();
                        return;
                    }
                }
                else
                {
                    divmsg2.Visible = false;
                }
                List<DischargeData> objdischarge = GetSummaryList(0);
                if (objdischarge.Count > 0)
                {
                    gvDischargeList.DataSource = objdischarge;
                    gvDischargeList.DataBind();
                    gvDischargeList.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total: " + objdischarge[0].MaximumRows.ToString() + " Record found", 1);
                    divmsg3.Attributes["class"] = "SucessAlert";
                    divmsg3.Visible = true;
                    ddlexport.Visible = true;
                    btnexport.Visible = true;

                }
                else
                {
                    gvDischargeList.DataSource = null;
                    gvDischargeList.DataBind();
                    gvDischargeList.Visible = true;
                    lblresult.Visible = false;
                    ddlexport.Visible = false;
                    btnexport.Visible = false;

                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage2, "system", 0);
                divmsg2.Attributes["class"] = "FailAlert";
                divmsg2.Visible = true;
            }
        }
        public List<DischargeData> GetSummaryList(int curIndex)
        {

            DischargeData objpat = new DischargeData();
            DischargeBO objBO = new DischargeBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime from = txtdatefromList.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txtdatefromList.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txttoList.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txttoList.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objpat.DateFrom = from;
            objpat.DateTo = To;
            //objpat.DischargeTypeID = Convert.ToInt32(ddl_DisTypeList.SelectedValue == "0" ? null : ddl_DisTypeList.SelectedValue);
            objpat.WardID = Convert.ToInt32(ddl_ward.SelectedValue == "0" ? null : ddl_ward.SelectedValue);
            objpat.DischargeDocID = Convert.ToInt64(ddlDischargeBy.SelectedValue == "0" ? null : ddlDischargeBy.SelectedValue);
            return objBO.GetDischargeReadyList(objpat);
        }
        protected void btnresetList_Click(object sender, EventArgs e)
        {
            txttoList.Text = "";
            txtdatefromList.Text = "";
            ddl_ward.SelectedIndex = 0;

            gvDischargeList.DataSource = null;
            gvDischargeList.DataBind();
            gvDischargeList.Visible = false;

            //txtdatefromList.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            //txttoList.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            ddlexport.Visible = false;
            btnexport.Visible = false;
            lblresult.Visible = false;
            divmsg3.Visible = false;
        }
        protected void gvDischargeList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "View")
                {
                    DischargeData objdata = new DischargeData();
                    DischargeBO objstdBO = new DischargeBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = gvDischargeList.Rows[i];
                    Label lblIPNo = (Label)gr.Cells[0].FindControl("lblIPNo");
                    objdata.IPNo = lblIPNo.Text;
                    List<DischargeData> objresult = objstdBO.GetDischargeTemplate(objdata);

                    if (objresult.Count == 1)
                    {

                        tabdisSummary.ActiveTabIndex = 0;


                    }
                    else
                    {

                    }


                }
                if (e.CommandName == "Print")
                {
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = gvDischargeList.Rows[i];
                    Label lblIPNo = (Label)gr.Cells[0].FindControl("lblIPNo");

                    string url = "../MedIPD/DischargeReportViewer.aspx?id=" + lblIPNo.Text;
                    string fullURL = "window.open('" + url + "', '_blank');";
                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_New_Tab", fullURL, true);

                }

            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage2, "system", 0);
            }

        }
        protected void gvDischargeList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDischargeList.PageIndex = e.NewPageIndex;
            BindSummaryList();
        }
        protected void btnexport_Click(object sender, EventArgs e)
        {
            if (LogData.ExportEnable == 0)
            {
                Messagealert_.ShowMessage(lblmessage2, "ExportEnable", 0);
                divmsg2.Visible = true;
                divmsg2.Attributes["class"] = "FailAlert";
                return;
            }
            else
            {
                lblmessage2.Visible = false;
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
                divmsg3.Visible = true;
                divmsg3.Attributes["class"] = "FailAlert";
                ddlexport.Focus();
                return;
            }
        }
        private void ExportToPdf()
        {
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    gvDischargeList.BorderStyle = BorderStyle.None;
                    //Hide the Column containing CheckBox
                    //gvSummaryList.Columns[4].Visible = false;
                    //gvSummaryList.Columns[5].Visible = false;
                    gvDischargeList.Columns[6].Visible = false;
                    gvDischargeList.Columns[7].Visible = false;

                    gvDischargeList.RenderControl(hw);
                    gvDischargeList.HeaderRow.Style.Add("width", "15%");
                    gvDischargeList.HeaderRow.Style.Add("font-size", "10px");
                    gvDischargeList.Style.Add("text-decoration", "none");
                    gvDischargeList.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    gvDischargeList.Style.Add("font-size", "8px");
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 7f, 7f, 7f, 0f);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=DischargeReadyList.pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.Write(pdfDoc);
                    Response.End();
                }
            }
        }
        protected void ExportoExcel()
        {
            DataTable dt = GetDatafromDatabase();
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "Patient Type Detail List");

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=DischargeReadyList.xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
        }
        private DataTable GetDatafromDatabase()
        {
            List<DischargeData> DischDetails = GetSummaryList(0);
            List<DischargeListDatatoExcel> ListexcelData = new List<DischargeListDatatoExcel>();
            int i = 0;
            foreach (DischargeData row in DischDetails)
            {
                DischargeListDatatoExcel ExcelSevice = new DischargeListDatatoExcel();
                ExcelSevice.IPNo = DischDetails[i].IPNo;
                ExcelSevice.PatientName = DischDetails[i].PatientName;
                ExcelSevice.Ward = DischDetails[i].Ward;
                ExcelSevice.DoctorName = DischDetails[i].DoctorName;
                ExcelSevice.AddedDate = DischDetails[i].AddedDate;
                //gvDischargeList.Columns[4].Visible = false;
                //gvDischargeList.Columns[5].Visible = false;
                //gvDischargeList.Columns[6].Visible = false;
                //gvDischargeList.Columns[7].Visible = false;
                ListexcelData.Add(ExcelSevice);
                i++;
            }
            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            DataTable dt = converter.ToDataTable(ListexcelData);
            return dt;
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        public class ListtoDataTableConverter
        {
            public DataTable ToDataTable<T>(List<T> items)
            {
                DataTable dataTable = new DataTable(typeof(T).Name);
                //Get all the properties
                PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (PropertyInfo prop in Props)
                {
                    //Setting column names as Property names
                    dataTable.Columns.Add(prop.Name);
                }
                foreach (T item in items)
                {
                    var values = new object[Props.Length];

                    for (int i = 0; i < Props.Length; i++)
                    {
                        //inserting property values to datatable rows
                        values[i] = Props[i].GetValue(item, null);
                    }
                    dataTable.Rows.Add(values);
                }
                //put a breakpoint here and check datatable
                return dataTable;
            }
        }
    }
}
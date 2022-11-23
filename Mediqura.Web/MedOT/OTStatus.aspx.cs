using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedBill;
using Mediqura.BOL.PatientBO;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.LoginData;
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
using Mediqura.CommonData.OTData;
using Mediqura.BOL.AdmissionBO;
using Mediqura.BOL.OTBO;

namespace Mediqura.Web.MedOT
{
    public partial class OTStatus : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }


        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetIPNo(string prefixText, int count, string contextKey)
        {
            OTStatusData Objpaic = new OTStatusData();
            OTStatusBO objInfoBO = new OTStatusBO();
            List<OTStatusData> getResult = new List<OTStatusData>();
            Objpaic.IPNo = prefixText;
            getResult = objInfoBO.getIPNo(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].IPNo.ToString());
            }
            return list;
        }
       [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetPatientName(string prefixText, int count, string contextKey)
        {
            PatientData Objpaic = new PatientData();
            RegistrationBO objInfoBO = new RegistrationBO();
            List<PatientData> getResult = new List<PatientData>();
            Objpaic.PatientName = prefixText;
            getResult = objInfoBO.GetPatientName(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].PatientName.ToString());
            }
            return list;
        }
        protected void gvOT_status_PageIndexChanging1(object sender, GridViewPageEventArgs e)
        {
            gvOT_status.PageIndex = e.NewPageIndex;
            bindgrid();
        }

        protected void gvOT_status_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                MasterLookupBO mstlookup = new MasterLookupBO();
                Label Status = (Label)e.Row.FindControl("lblstatus");
                Label StatusID = (Label)e.Row.FindControl("lblStID");
                DropDownList ddlstatus = (DropDownList)e.Row.FindControl("ddlOTstatus");
                Commonfunction.PopulateDdl(ddlstatus, mstlookup.GetLookupsList(LookupName.OT_statusType));
                if (Status.Text == "")
                {
                    ddlstatus.SelectedIndex = 0;
                }
                else
                {
                    ddlstatus.SelectedItem.Text = Status.Text;
                    ddlstatus.SelectedIndex = Convert.ToInt32(StatusID.Text);
                }
               
            }
        }
        protected void bindgrid()
        {
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
                 if (txtdatefrom.Text != "")
                {
                    if (Commonfunction.isValidDate(txtdatefrom.Text) == false)
                    {
                        Messagealert_.ShowMessage(lblmessage, "Please enter valid from date.", 0);
                        divmsg3.Attributes["class"] = "FailAlert";
                        divmsg3.Visible = true;
                        txtdatefrom.Focus();
                        return;
                    }
                }
                else
                {
                    divmsg3.Visible = false;
                }
                if (txtto.Text != "")
                {
                    if (Commonfunction.isValidDate(txtto.Text) == false)
                    {
                        Messagealert_.ShowMessage(lblmessage, "Please enter valid to date.", 0);
                        divmsg3.Attributes["class"] = "FailAlert";
                        divmsg3.Visible = true;
                        txtto.Focus();
                        return;
                    }
                }
                else
                {
                    divmsg3.Visible = false;
                }
                foreach (GridViewRow row in gvOT_status.Rows)
                {
                 
                    DropDownList Status = (DropDownList)gvOT_status.Rows[row.RowIndex].Cells[0].FindControl("ddlOTstatus");
                    Status.Enabled = false;
                }
                List<OTStatusData> objdeposit = GetOT_StatusList(0);
                if (objdeposit.Count > 0)
                {
                    gvOT_status.DataSource = objdeposit;
                    gvOT_status.DataBind();
                    gvOT_status.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total: " + objdeposit[0].MaximumRows.ToString() + " Record found", 1);
                    divmsg3.Attributes["class"] = "SucessAlert";
                    divmsg3.Visible = true;
                    ddlexport.Visible = true;
                    btnexport.Visible = true;
                    btn_update.Visible = true;
                    btn_print.Visible = true;
                    btn_update.Attributes.Remove("disabled");
                    btn_print.Attributes.Remove("disabled");
                }
                else
                {
                    divmsg3.Visible = false;
                    gvOT_status.DataSource = null;
                    gvOT_status.DataBind();
                    gvOT_status.Visible = true;
                    ddlexport.Visible = false;
                    btnexport.Visible = false;
                    divmsg3.Visible = false;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
            }

        }
        public List<OTStatusData> GetOT_StatusList(int curIndex)
        {
            OTStatusData objpat = new OTStatusData();
            OTStatusBO objbillingBO = new OTStatusBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime from = txtdatefrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txtdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txtto.Text.Trim() == "" ? GlobalConstant.MaxSQLDateTime : DateTime.Parse(txtto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objpat.DateFrom = from;
            objpat.DateTo = To;
      

            objpat.IPNo = txt_IPNo.Text == "" ? "0" : txt_IPNo.Text;
            objpat.PatientName = txt_name.Text == "" ? null : txt_name.Text.Trim();
            return objbillingBO.GetOT_StatusList(objpat);
        }
       
        protected void txt_IPNo_TextChanged(object sender, EventArgs e)
        {
            if (txt_IPNo.Text != "")
            {
                bindgrid();
            }
        }

        protected void txt_name_TextChanged(object sender, EventArgs e)
        {
            if (txt_name.Text != "")
            {
                bindgrid();
            }
        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            bindgrid();
        }

        protected void btnresets_Click(object sender, EventArgs e)
        {
            txt_IPNo.Text = "";
            txt_name.Text = "";
            txtdatefrom.Text = "";
            txtto.Text = "";
            gvOT_status.DataSource = null;
            gvOT_status.DataBind();
            gvOT_status.Visible = false;
            lblmessage.Visible = false;
            lblresult.Visible = false;
            ddlexport.Visible = false;
            btnexport.Visible = false;
            btn_update.Visible = false;
            btn_print.Visible = false;
            btn_update.Attributes["disabled"] = "disabled";
            btn_print.Attributes["disabled"] = "disabled";

        }
        protected void chkselect_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < gvOT_status.Rows.Count; i++)
            {
                CheckBox chk = (CheckBox)gvOT_status.Rows[i].Cells[0].FindControl("chkselect");
                DropDownList Status = (DropDownList)gvOT_status.Rows[i].Cells[0].FindControl("ddlOTstatus");
                Label OT_ID = (Label)gvOT_status.Rows[i].Cells[0].FindControl("lblID");

                if (chk.Checked == true)
                {

                    Status.Enabled = true;
                    Status.Focus();




                }
                else
                {
                    Status.Enabled = false;

                }
            }
        }

        protected void btnexport_Click(object sender, EventArgs e)
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
                    gvOT_status.BorderStyle = BorderStyle.None;
                    //Hide the Column containing CheckBox
                    //gvOT_status.Columns[6].Visible = false;
                    //gvOT_status.Columns[8].Visible = false;
                    //gvstockstatus.Columns[10].Visible = false;
                    //gvstockstatus.Columns[11].Visible = false;

                    gvOT_status.RenderControl(hw);
                    gvOT_status.HeaderRow.Style.Add("width", "15%");
                    gvOT_status.HeaderRow.Style.Add("font-size", "10px");
                    gvOT_status.Style.Add("text-decoration", "none");
                    gvOT_status.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    gvOT_status.Style.Add("font-size", "8px");
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 7f, 7f, 7f, 0f);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=OTStatusDetails.pdf");
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
                Response.AddHeader("content-disposition", "attachment;filename=OTStatusDetails.xlsx");
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
            List<OTStatusData> OTStatus = GetOT_StatusList(0);
            List<OTStatusListDataTOeXCEL> ListexcelData = new List<OTStatusListDataTOeXCEL>();
            int i = 0;
            foreach (OTStatusData row in OTStatus)
            {
                OTStatusListDataTOeXCEL Ecxeclpat = new OTStatusListDataTOeXCEL();
                Ecxeclpat.IPNo = OTStatus[i].IPNo;
                Ecxeclpat.PatientName = OTStatus[i].PatientName;
                Ecxeclpat.CaseName = OTStatus[i].CaseName;
                Ecxeclpat.OpernDate = OTStatus[i].OpernDate;
                for (int j = 0; j < gvOT_status.Rows.Count; j++)
                {
                    DropDownList Status = (DropDownList)gvOT_status.Rows[j].Cells[0].FindControl("ddlOTstatus");
                    OTStatusData objpat = new OTStatusData();
                    objpat.Status = Status.Text;
                    Ecxeclpat.Status = OTStatus[i].Status;
                }
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
        protected void gvOT_status_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void btn_update_Click(object sender, EventArgs e)
        {
            if (LogData.UpdateEnable == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "UpdateEnable", 0);
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
                List<OTStatusData> ListOTstatus = new List<OTStatusData>();
                OTStatusBO objLabSampleBO = new OTStatusBO();
                OTStatusData objSampleData = new OTStatusData();
                foreach (GridViewRow row in gvOT_status.Rows)
                {
                    CheckBox chk = (CheckBox)gvOT_status.Rows[row.RowIndex].Cells[0].FindControl("chkselect");
                    DropDownList Status = (DropDownList)gvOT_status.Rows[row.RowIndex].Cells[0].FindControl("ddlOTstatus");
                    Label IPno = (Label)gvOT_status.Rows[row.RowIndex].Cells[0].FindControl("lblIPNo");
                    OTStatusData ObjDetails = new OTStatusData();
                    ObjDetails.Otstatus = Convert.ToInt32(Status.SelectedValue == "" ? "0" : Status.SelectedValue);
                    ObjDetails.IPNo = IPno.Text.Trim();
                    ListOTstatus.Add(ObjDetails);
                    //Status.Enabled = true;
                }
                objSampleData.XMLData = XmlConvertor.OTStatustoXML(ListOTstatus).ToString();
                int result = objLabSampleBO.UpdateOT_status(objSampleData);
                if (result == 1 || result == 2)
                {
                    lblmessage.Visible = true;
                    Messagealert_.ShowMessage(lblmessage, result == 1 ? "save" : "update", 1);
                    div1.Attributes["class"] = "SucessAlert";
                    btn_update.Attributes["disabled"] = "disabled";
                    btn_print.Visible = true;
                    btn_print.Attributes.Remove("disabled");
                    ViewState["ID"] = null;
                }
                else if (result == 5)
                {
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    Messagealert_.ShowMessage(lblmessage, "duplicate", 0);
                }
                else
                {
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    Messagealert_.ShowMessage(lblmessage, "system", 0);
                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);

            }
          
            
            
        }

        protected void btn_print_Click(object sender, EventArgs e)
        {
            if (LogData.PrintEnable == 0)
            {
                btn_print.Attributes["disabled"] = "disabled";
            }
            else
            {
                btn_print.Attributes.Remove("disabled");
            }
        }


    }
}
using ClosedXML.Excel;
using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedUtilityBO;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.MedUtilityData;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using Mediqura.Web.MedCommon;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using ClosedXML.Excel;
using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedUtilityBO;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.MedUtilityData;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using Mediqura.Web.MedCommon;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using System.Text;
using System.Drawing;

namespace Mediqura.Web.MedUtility
{
    public partial class DistrictMST :BasePage
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
            Commonfunction.PopulateDdl(ddl_country, mstlookup.GetLookupsList(LookupName.Country));
            Commonfunction.PopulateDdl(ddl_State, mstlookup.GetLookupsList(LookupName.State));
        

        }
        private void bindgrid()
        {
            try
            {
                List<DistrictData> lstemp = GetDistrict(0);
                if (lstemp.Count > 0)
                {
                    GvDistrict.DataSource = lstemp;
                    GvDistrict.DataBind();
                    GvDistrict.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total: " + lstemp[0].MaximumRows.ToString() + " Record found", 1);
                    divmsg3.Attributes["class"] = "SucessAlert";
                    divmsg3.Visible = true;
                    ddlexport.Visible = true;
                    btnexport.Visible = true;
                }
                else
                {
                    GvDistrict.DataSource = null;
                    GvDistrict.DataBind();
                    GvDistrict.Visible = true;
                    lblresult.Visible = false;
                    ddlexport.Visible = false;
                    btnexport.Visible = false;
                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
            }
        }
        private List<DistrictData> GetDistrict(int p)
        {
            DistrictData objCountryMSTData = new DistrictData();
            DistrictBO objCountryMSTBO = new DistrictBO();
            objCountryMSTData.DistrictCode = txt_Districtcode.Text == "" ? "" : txt_Districtcode.Text;
            objCountryMSTData.DistrictDescp = txt_DistrictDescp.Text == "" ? "" : txt_DistrictDescp.Text;
            objCountryMSTData.CountryID = Convert.ToInt32(ddl_country.SelectedValue == "" ? null : ddl_country.SelectedValue);
            objCountryMSTData.StateID = Convert.ToInt32(ddl_State.SelectedValue == "" ? null : ddl_State.SelectedValue);
            objCountryMSTData.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
            return objCountryMSTBO.SearchDistrictDetails(objCountryMSTData);
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
                if (txt_Districtcode.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Code", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    txt_Districtcode.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txt_DistrictDescp.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Description", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    txt_DistrictDescp.Focus();
                    return;
                }
                {
                    lblmessage.Visible = false;
                }
                DistrictData objCountryMSTData = new DistrictData();
                DistrictBO objCountryMSTBO = new DistrictBO();
                objCountryMSTData.DistrictCode = txt_Districtcode.Text == "" ? null : txt_Districtcode.Text;
                objCountryMSTData.DistrictDescp = txt_DistrictDescp.Text == "" ? null : txt_DistrictDescp.Text;
                objCountryMSTData.CountryID = Convert.ToInt32(ddl_country.SelectedValue == "0" ? null : ddl_country.SelectedValue);
                objCountryMSTData.StateID = Convert.ToInt32(ddl_State.SelectedValue == "" ? null : ddl_State.SelectedValue);
                objCountryMSTData.EmployeeID = LogData.EmployeeID;
                objCountryMSTData.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
                objCountryMSTData.HospitalID = LogData.HospitalID;
                objCountryMSTData.FinancialYearID = LogData.FinancialYearID;
                objCountryMSTData.ActionType = Enumaction.Insert;
                if (ViewState["ID"] != null)
                {
                    if (LogData.UpdateEnable == 0)
                    {
                        Messagealert_.ShowMessage(lblmessage, "UpdateEnable", 0);
                        div1.Visible = true;
                        div1.Attributes["class"] = "FailAlert";
                        txt_Districtcode.Focus();
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                    }
                    objCountryMSTData.ActionType = Enumaction.Update;
                    objCountryMSTData.DistrictID = Convert.ToInt32(ViewState["ID"].ToString());
                }
                int result = objCountryMSTBO.UpdateDistrictDetails(objCountryMSTData);  // funtion at DAL
                if (result == 1 || result == 2)
                {
                    lblmessage.Visible = true;
                    Messagealert_.ShowMessage(lblmessage, result == 1 ? "save" : "update", 1);
                    div1.Visible = true;
                    div1.Attributes["class"] = "SucessAlert";
                    ViewState["ID"] = null;
                    bindgrid();
                }
                else if (result == 5)
                {
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    Messagealert_.ShowMessage(lblmessage, "duplicate", 0);
                }
                else
                    Messagealert_.ShowMessage(lblmessage, "system", 0);
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
            }
        }

        protected void btnresets_Click(object sender, EventArgs e)
        {
            ViewState["ID"] = null;
            clearall();
            lblmessage.Visible = false;
            lblresult.Visible = false;
        }
        private void clearall()
        {
            txt_Districtcode.Text = "";
            txt_DistrictDescp.Text = "";
            ddl_country.SelectedIndex = 0;
            ddl_State.SelectedIndex = 0;
            GvDistrict.DataSource = null;
            GvDistrict.DataBind();
            GvDistrict.Visible = false;
            ddlexport.Visible = false;
            btnexport.Visible = false;
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
            bindgrid();
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
                    GvDistrict.BorderStyle = BorderStyle.None;
                    //Hide the Column containing CheckBox
                    GvDistrict.Columns[7].Visible = false;
                    GvDistrict.Columns[8].Visible = false;
                    GvDistrict.Columns[9].Visible = false;

                    GvDistrict.RenderControl(hw);
                    GvDistrict.HeaderRow.Style.Add("width", "15%");
                    GvDistrict.HeaderRow.Style.Add("font-size", "10px");
                    GvDistrict.Style.Add("text-decoration", "none");
                    GvDistrict.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    GvDistrict.Style.Add("font-size", "8px");
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 7f, 7f, 7f, 0f);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=DistrictDetails.pdf");
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
                Response.AddHeader("content-disposition", "attachment;filename=DistrictDetails.xlsx");
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
            List<DistrictData> labunitDetails = GetDistrict(0);
            List<DistrictDatatoExcel> ListexcelData = new List<DistrictDatatoExcel>();
            int i = 0;
            foreach (DistrictData row in labunitDetails)
            {
                DistrictDatatoExcel ExcelSevice = new DistrictDatatoExcel();
                ExcelSevice.DistrictID = labunitDetails[i].DistrictID;
                ExcelSevice.DistrictCode = labunitDetails[i].DistrictCode;
                ExcelSevice.DistrictDescp = labunitDetails[i].DistrictDescp;
                ExcelSevice.Country = labunitDetails[i].Country;
                ExcelSevice.State = labunitDetails[i].State;
                ExcelSevice.AddedBy = labunitDetails[i].EmpName;
                GvDistrict.Columns[7].Visible = false;
                GvDistrict.Columns[8].Visible = false;
                GvDistrict.Columns[9].Visible = false;
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
        protected void GvDistrict_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Edits")
                {
                    if (LogData.EditEnable == 0)
                    {
                        Messagealert_.ShowMessage(lblmessage, "EditEnable", 0);
                        div1.Visible = true;
                        div1.Attributes["class"] = "FailAlert";
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                    }
                    DistrictData objCountryMSTData = new DistrictData();
                    DistrictBO objCountryMSTBO = new DistrictBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow pt = GvDistrict.Rows[i];
                    Label ID = (Label)pt.Cells[0].FindControl("code");
                    objCountryMSTData.DistrictID = Convert.ToInt32(ID.Text);
                    objCountryMSTData.ActionType = Enumaction.Select;

                    List<DistrictData> GetResult = objCountryMSTBO.GetDistrictDetailsByID(objCountryMSTData);
                    if (GetResult.Count > 0)
                    {
                        txt_Districtcode.Text = GetResult[0].DistrictCode;
                        txt_DistrictDescp.Text = GetResult[0].DistrictDescp;
                        ddl_country.SelectedValue = GetResult[0].CountryID.ToString();
                        ddl_State.SelectedValue = GetResult[0].StateID.ToString();
                        ViewState["ID"] = GetResult[0].DistrictID;
                    }
                }
                if (e.CommandName == "Deletes")
                {
                    if (LogData.DeleteEnable == 0)
                    {
                        Messagealert_.ShowMessage(lblmessage, "DeleteEnable", 0);
                        div1.Visible = true;
                        div1.Attributes["class"] = "FailAlert";
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                    }
                    DistrictData objCountryMSTData = new DistrictData();
                    DistrictBO objCountryMSTBO = new DistrictBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GvDistrict.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("code");
                    objCountryMSTData.DistrictID = Convert.ToInt32(ID.Text);
                    objCountryMSTData.EmployeeID = LogData.EmployeeID;
                    objCountryMSTData.ActionType = Enumaction.Delete;
                    TextBox txtremarks = (TextBox)gr.Cells[0].FindControl("txtremarks");
                    txtremarks.Enabled = true;
                    if (txtremarks.Text == "")
                    {
                        Messagealert_.ShowMessage(lblresult, "Remarks", 0);
                        divmsg3.Visible = true;
                        divmsg3.Attributes["class"] = "FailAlert";
                        txtremarks.Focus();
                        return;
                    }
                    else
                    {
                        objCountryMSTData.Remarks = txtremarks.Text;
                    }

                    DistrictBO objCountryMSTBO1 = new DistrictBO();
                    int Result = objCountryMSTBO1.DeleteDistrictDetailsByID(objCountryMSTData);
                    if (Result == 1)
                    {
                        Messagealert_.ShowMessage(lblmessage, "delete", 1);
                        div1.Visible = true;
                        div1.Attributes["class"] = "SucessAlert";
                        bindgrid();
                    }
                    else
                    {
                        Messagealert_.ShowMessage(lblmessage, "system", 0);
                        div1.Visible = true;
                        div1.Attributes["class"] = "FailAlert";

                    }
                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
            }
        }

        protected void GvDistrict_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GvDistrict.PageIndex = e.NewPageIndex;
            bindgrid();
        }
        }
    
}
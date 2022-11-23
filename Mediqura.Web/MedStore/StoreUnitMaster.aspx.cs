using ClosedXML.Excel;
using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedStore;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.MedUtilityData;
using Mediqura.CommonData.MedStore;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using Mediqura.Web.MedCommon;
using Mediqura.Web.MedStore;
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
namespace Mediqura.Web.MedStore
{
    public partial class StoreUnitMaster : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
        private void bindgrid(int page)
        {
            try
            {

                List<StoreUnitMasterData> lstemp = GetStoreUnit(page);
                if (lstemp.Count > 0)
                {
                    GvStoreUnit.VirtualItemCount = lstemp[0].MaximumRows;//total item is required for custom paging
                    GvStoreUnit.PageIndex = page - 1;
                    GvStoreUnit.DataSource = lstemp;
                    GvStoreUnit.DataBind();
                    GvStoreUnit.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total: " + lstemp[0].MaximumRows.ToString() + " Record found.", 1);
                    divmsg3.Attributes["class"] = "SucessAlert";
                    divmsg3.Visible = true;
                    ddlexport.Visible = true;
                    btnexport.Visible = true;
                }
                else
                {
                    GvStoreUnit.DataSource = null;
                    GvStoreUnit.DataBind();
                    GvStoreUnit.Visible = true;
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
        private List<StoreUnitMasterData> GetStoreExcel(int p)
        {
            
            StoreUnitMasterData objStoreUnitMasterData = new StoreUnitMasterData();
            StoreUnitMasterBO objStoreUnitMasterBO = new StoreUnitMasterBO();
            objStoreUnitMasterData.StoreUnitCode= txt_StoreUnitcode.Text == "" ? "" : txt_StoreUnitcode.Text;
            objStoreUnitMasterData.StoreUnitdescp = txt_StoreUnitDescription.Text == "" ? "" : txt_StoreUnitDescription.Text;
            objStoreUnitMasterData.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
            return objStoreUnitMasterBO.SearchStoreUnitExcel(objStoreUnitMasterData);
        }
        private List<StoreUnitMasterData> GetStoreUnit(int p)
        {

            StoreUnitMasterData objStoreUnitMasterData = new StoreUnitMasterData();
            StoreUnitMasterBO objStoreUnitMasterBO = new StoreUnitMasterBO();
            objStoreUnitMasterData.StoreUnitCode = txt_StoreUnitcode.Text == "" ? "" : txt_StoreUnitcode.Text;
            objStoreUnitMasterData.StoreUnitdescp = txt_StoreUnitDescription.Text == "" ? "" : txt_StoreUnitDescription.Text;
            objStoreUnitMasterData.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
            objStoreUnitMasterData.CurrentIndex = p;
            return objStoreUnitMasterBO.SearchStoreUnitDetails(objStoreUnitMasterData);
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
                if (txt_StoreUnitcode.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Code", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    txt_StoreUnitcode.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txt_StoreUnitDescription.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Description", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    txt_StoreUnitDescription.Focus();
                    return;
                }
                {
                    lblmessage.Visible = false;
                }
                StoreUnitMasterData objStoreUnitMasterData = new StoreUnitMasterData();
                StoreUnitMasterBO objStoreUnitMasterBO = new StoreUnitMasterBO();
                objStoreUnitMasterData.StoreUnitCode = txt_StoreUnitcode.Text == "" ? null : txt_StoreUnitcode.Text;
                objStoreUnitMasterData.StoreUnitdescp = txt_StoreUnitDescription.Text == "" ? null : txt_StoreUnitDescription.Text;
                objStoreUnitMasterData.EmployeeID = LogData.EmployeeID;
                objStoreUnitMasterData.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
                objStoreUnitMasterData.HospitalID = LogData.HospitalID;
                objStoreUnitMasterData.FinancialYearID = LogData.FinancialYearID;
                objStoreUnitMasterData.ActionType = Enumaction.Insert;
                if (ViewState["ID"] != null)
                {
                    if (LogData.UpdateEnable == 0)
                    {
                        Messagealert_.ShowMessage(lblmessage, "UpdateEnable", 0);
                        div1.Visible = true;
                        div1.Attributes["class"] = "FailAlert";
                        txt_StoreUnitcode.Focus();
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                    }
                    objStoreUnitMasterData.ActionType = Enumaction.Update;
                    objStoreUnitMasterData.StoreUnitID = Convert.ToInt32(ViewState["ID"].ToString());
                }
                int result = objStoreUnitMasterBO.UpdateStoreUnitDetails(objStoreUnitMasterData);  // funtion at DAL
                if (result == 1 || result == 2)
                {
                    lblmessage.Visible = true;
                    Messagealert_.ShowMessage(lblmessage, result == 1 ? "save" : "update", 1);
                    div1.Visible = true;
                    div1.Attributes["class"] = "SucessAlert";
                    ViewState["ID"] = null;
                    bindgrid(1);
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
            bindgrid(1);
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
            txt_StoreUnitcode.Text = "";
            txt_StoreUnitDescription.Text = "";
            GvStoreUnit.DataSource = null;
            GvStoreUnit.DataBind();
            GvStoreUnit.Visible = false;
            ddlexport.Visible = false;
            btnexport.Visible = false;
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
                    GvStoreUnit.AllowPaging = false;
                    GvStoreUnit.DataSource = GetStoreExcel(0);
                    GvStoreUnit.DataBind();
                    GvStoreUnit.BorderStyle = BorderStyle.None;
                    //Hide the Column containing CheckBox
                    GvStoreUnit.Columns[4].Visible = false;
                    GvStoreUnit.Columns[5].Visible = false;
                    GvStoreUnit.Columns[6].Visible = false;
                    GvStoreUnit.Columns[7].Visible = false;

                    GvStoreUnit.RenderControl(hw);
                    GvStoreUnit.HeaderRow.Style.Add("width", "15%");
                    GvStoreUnit.HeaderRow.Style.Add("font-size", "10px");
                    GvStoreUnit.Style.Add("text-decoration", "none");
                    GvStoreUnit.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    GvStoreUnit.Style.Add("font-size", "8px");
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 7f, 7f, 7f, 0f);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=StoreUnitsDetails.pdf");
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
                Response.AddHeader("content-disposition", "attachment;filename=StoreUnitsDetails.xlsx");
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
            List<StoreUnitMasterData> StoreUnitDetails = GetStoreExcel(0);
            List<StoreUnitDatatoExcel> ListexcelData = new List<StoreUnitDatatoExcel>();
            int i = 0;
            foreach (StoreUnitMasterData row in StoreUnitDetails)
            {
                StoreUnitDatatoExcel ExcelSevice = new StoreUnitDatatoExcel();
                ExcelSevice.StoreUnitID = StoreUnitDetails[i].StoreUnitID;
                ExcelSevice.StoreUnitCode = StoreUnitDetails[i].StoreUnitCode;
                ExcelSevice.StoreUnitdescp = StoreUnitDetails[i].StoreUnitdescp;
                ExcelSevice.AddedBy = StoreUnitDetails[i].EmpName;
                GvStoreUnit.Columns[4].Visible = false;
                GvStoreUnit.Columns[5].Visible = false;
                GvStoreUnit.Columns[6].Visible = false;
                GvStoreUnit.Columns[7].Visible = false;
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
        protected void GvStoreUnit_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            bindgrid(Convert.ToInt32(e.NewPageIndex + 1));
        }

        protected void GvStoreUnit_RowCommand(object sender, GridViewCommandEventArgs e)
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
                    StoreUnitMasterData objStoreUnitMasterData = new StoreUnitMasterData();
                    StoreUnitMasterBO objStoreUnitMasterBO = new StoreUnitMasterBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow pt = GvStoreUnit.Rows[i];
                    Label ID = (Label)pt.Cells[0].FindControl("code");
                    objStoreUnitMasterData.StoreUnitID = Convert.ToInt32(ID.Text);
                    objStoreUnitMasterData.ActionType = Enumaction.Select;

                    List<StoreUnitMasterData> GetResult = objStoreUnitMasterBO.GetStoreUnitDetailsByID(objStoreUnitMasterData);
                    if (GetResult.Count > 0)
                    {
                        txt_StoreUnitcode.Text = GetResult[0].StoreUnitCode;
                        txt_StoreUnitDescription.Text = GetResult[0].StoreUnitdescp;
                        ViewState["ID"] = GetResult[0].StoreUnitID;
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
                    StoreUnitMasterData objStoreUnitMasterData = new StoreUnitMasterData();
                    StoreUnitMasterBO objStoreUnitMasterBO = new StoreUnitMasterBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GvStoreUnit.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("code");
                    objStoreUnitMasterData.StoreUnitID = Convert.ToInt32(ID.Text);
                    objStoreUnitMasterData.EmployeeID = LogData.EmployeeID;
                    objStoreUnitMasterData.ActionType = Enumaction.Delete;
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
                        objStoreUnitMasterData.Remarks = txtremarks.Text;
                    }

                    StoreUnitMasterBO objStoreUnitMasterBO1 = new StoreUnitMasterBO();
                    int Result = objStoreUnitMasterBO1.DeleteStoreUnitDetailsByID(objStoreUnitMasterData);
                    if (Result == 1)
                    {
                        Messagealert_.ShowMessage(lblmessage, "delete", 1);
                        div1.Visible = true;
                        div1.Attributes["class"] = "SucessAlert";
                        bindgrid(1);
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
        
    }
}
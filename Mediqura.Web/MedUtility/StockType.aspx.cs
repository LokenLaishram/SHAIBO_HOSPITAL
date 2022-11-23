﻿using ClosedXML.Excel;
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
using Mediqura.CommonData.LoginData;

namespace Mediqura.Web.MedUtility
{
    public partial class StockType : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblmessage.Visible = false;
            }
        }
        protected void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                if (LogData.SaveEnable == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "SaveEnable", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txt_stocktypecode.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Code", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";

                    txt_stocktypecode.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txt_stocktype.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "StockType", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";

                    txt_stocktype.Focus();
                    return;
                }
                {
                    lblmessage.Visible = false;
                }
                StockTypeMasterData objPaymentTypeMasterData = new StockTypeMasterData();
                StockTypeMasterBO objPaymentTypeMasterBO = new StockTypeMasterBO();
                objPaymentTypeMasterData.StockTypeCode = txt_stocktypecode.Text == "" ? null : txt_stocktypecode.Text;
                objPaymentTypeMasterData.StockType = txt_stocktype.Text == "" ? null : txt_stocktype.Text;
                objPaymentTypeMasterData.EmployeeID = LogData.EmployeeID;
                objPaymentTypeMasterData.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
                objPaymentTypeMasterData.HospitalID = LogData.HospitalID;
                objPaymentTypeMasterData.FinancialYearID = LogData.FinancialYearID;
                objPaymentTypeMasterData.ActionType = Enumaction.Insert;
                if (ViewState["ID"] != null)
                {
                    if (LogData.UpdateEnable == 0)
                    {
                        Messagealert_.ShowMessage(lblmessage, "UpdateEnable", 0);
                        divmsg1.Visible = true;
                        divmsg1.Attributes["class"] = "FailAlert";
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                        objPaymentTypeMasterData.ActionType = Enumaction.Update;
                        objPaymentTypeMasterData.ID = Convert.ToInt32(ViewState["ID"].ToString());
                    }
                }
                int result = objPaymentTypeMasterBO.UpdateStockTypeDetails(objPaymentTypeMasterData);
                if (result == 1 || result == 2)
                {
                    lblmessage.Visible = true;
                    Messagealert_.ShowMessage(lblmessage, result == 1 ? "save" : "update", 1);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "SucessAlert";

                    ViewState["ID"] = null;
                    bindgrid();
                }
                else if (result == 5)
                {
                    Messagealert_.ShowMessage(lblmessage, "duplicate", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";

                }
                else
                {
                    Messagealert_.ShowMessage(lblmessage, "system", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                }

            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
            }
        }
        protected void GvStockType_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Edits")
                {
                    if (LogData.EditEnable == 0)
                    {
                        Messagealert_.ShowMessage(lblmessage, "EditEnable", 0);
                        divmsg1.Visible = true;
                        divmsg1.Attributes["class"] = "FailAlert";
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                    }
                    StockTypeMasterData objPaymentTypeMasterData = new StockTypeMasterData();
                    StockTypeMasterBO objPaymentTypeMasterBO = new StockTypeMasterBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow pt = GvStockType.Rows[i];
                    Label ID = (Label)pt.Cells[0].FindControl("code");
                    objPaymentTypeMasterData.ID = Convert.ToInt32(ID.Text);
                    objPaymentTypeMasterData.ActionType = Enumaction.Select;

                    List<StockTypeMasterData> GetResult = objPaymentTypeMasterBO.GetStockTypeDetailsByID(objPaymentTypeMasterData);
                    if (GetResult.Count > 0)
                    {
                        txt_stocktypecode.Text = GetResult[0].StockTypeCode;
                        txt_stocktype.Text = GetResult[0].StockType;
                        ViewState["ID"] = GetResult[0].ID;
                    }
                }
                if (e.CommandName == "Deletes")
                {
                    if (LogData.DeleteEnable == 0)
                    {
                        Messagealert_.ShowMessage(lblmessage, "DeleteEnable", 0);
                        divmsg1.Visible = true;
                        divmsg1.Attributes["class"] = "FailAlert";
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                    }
                    StockTypeMasterData objPaymentTypeMasterData = new StockTypeMasterData();
                    StockTypeMasterBO objPaymentTypeMasterBO = new StockTypeMasterBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GvStockType.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("code");
                    objPaymentTypeMasterData.ID = Convert.ToInt32(ID.Text);
                    objPaymentTypeMasterData.EmployeeID = LogData.EmployeeID;
                    objPaymentTypeMasterData.ActionType = Enumaction.Delete;
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
                        objPaymentTypeMasterData.Remarks = txtremarks.Text;
                    }

                    StockTypeMasterBO objPaymentTypeMasterBO1 = new StockTypeMasterBO();
                    int Result = objPaymentTypeMasterBO1.DeleteStockTypeDetailsByID(objPaymentTypeMasterData);
                    if (Result == 1)
                    {
                        Messagealert_.ShowMessage(lblmessage, "delete", 1);
                        divmsg1.Visible = true;
                        divmsg1.Attributes["class"] = "SucessAlert";

                        bindgrid();
                    }
                    else
                    {
                        Messagealert_.ShowMessage(lblmessage, "system", 0);
                        divmsg1.Visible = true;
                        divmsg1.Attributes["class"] = "FailAlert";

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
        private void bindgrid()
        {
            try
            {
               
                List<StockTypeMasterData> lstemp = GetStockType(0);
                if (lstemp.Count > 0)
                {
                    GvStockType.DataSource = lstemp;
                    GvStockType.DataBind();
                    GvStockType.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total: " + lstemp[0].MaximumRows.ToString() + " Record(s) found", 1);
                    divmsg3.Visible = true;
                    divmsg3.Attributes["class"] = "SucessAlert";

                    ddlexport.Visible = true;
                    btnexport.Visible = true;
                }
                else
                {
                    GvStockType.DataSource = null;
                    GvStockType.DataBind();
                    GvStockType.Visible = true;
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
        private List<StockTypeMasterData> GetStockType(int p)
        {
            StockTypeMasterData objPaymentTypeMasterData = new StockTypeMasterData();
            StockTypeMasterBO objPaymentTypeMasterBO = new StockTypeMasterBO();
            objPaymentTypeMasterData.StockTypeCode = txt_stocktypecode.Text == "" ? "" : txt_stocktypecode.Text;
            objPaymentTypeMasterData.StockType = txt_stocktype.Text == "" ? "" : txt_stocktype.Text;
            objPaymentTypeMasterData.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
            return objPaymentTypeMasterBO.SearchStockTypeDetails(objPaymentTypeMasterData);
        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            if (LogData.SearchEnable == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "SearchEnable", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                return;
            }
            else
            {
                lblmessage.Visible = false;
            }
            bindgrid();
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
            txt_stocktypecode.Text = "";
            txt_stocktype.Text = "";
            ddlstatus.SelectedIndex = 0;
            GvStockType.DataSource = null;
            GvStockType.DataBind();
            GvStockType.Visible = false;
            ddlexport.Visible = false;
            btnexport.Visible = false;
        }
        protected void btnexport_Click(object sender, EventArgs e)
        {
            if (LogData.ExportEnable == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "ExportEnable", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
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
                    GvStockType.BorderStyle = BorderStyle.None;
                    //Hide the Column containing CheckBox
                    GvStockType.Columns[4].Visible = false;
                    GvStockType.Columns[5].Visible = false;
                    GvStockType.Columns[6].Visible = false;
                    GvStockType.Columns[7].Visible = false;

                    GvStockType.RenderControl(hw);
                    GvStockType.HeaderRow.Style.Add("width", "15%");
                    GvStockType.HeaderRow.Style.Add("font-size", "10px");
                    GvStockType.Style.Add("text-decoration", "none");
                    GvStockType.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    GvStockType.Style.Add("font-size", "8px");
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 7f, 7f, 7f, 0f);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=StockTypeDetails.pdf");
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
                Response.AddHeader("content-disposition", "attachment;filename=StockTypeDetails.xlsx");
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
            List<StockTypeMasterData> PaymentTypeDetails = GetStockType(0);
            List<StockTypeDatatoExcel> ListexcelData = new List<StockTypeDatatoExcel>();
            int i = 0;
            foreach (StockTypeMasterData row in PaymentTypeDetails)
            {
                StockTypeDatatoExcel ExcelSevice = new StockTypeDatatoExcel();
                ExcelSevice.ID = PaymentTypeDetails[i].ID;
                ExcelSevice.StockTypeCode = PaymentTypeDetails[i].StockTypeCode;
                ExcelSevice.StockType = PaymentTypeDetails[i].StockType;
                ExcelSevice.AddedBy = PaymentTypeDetails[i].EmpName;
                GvStockType.Columns[4].Visible = false;
                GvStockType.Columns[5].Visible = false;
                GvStockType.Columns[6].Visible = false;
                GvStockType.Columns[7].Visible = false;
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
        protected void GvStockType_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GvStockType.PageIndex = e.NewPageIndex;
            bindgrid();
        }

    }
}
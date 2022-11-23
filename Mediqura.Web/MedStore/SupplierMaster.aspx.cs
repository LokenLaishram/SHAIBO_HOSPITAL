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
using Mediqura.CommonData.MedStore;
using Mediqura.BOL.MedStore;

namespace Mediqura.Web.MedStore
{
    public partial class SupplierMaster : BasePage
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

                if (txt_suppliercode.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Code", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    txt_suppliercode.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txt_suppliertype.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Please enter Supplier Type.", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    txt_suppliertype.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txt_contactno.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "PhoneNo", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
    
                    txt_contactno.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }

                SupplierTypeMasterData objSupplierTypeMasterData = new SupplierTypeMasterData();
                SupplierTypeMasterBO objSupplierTypeMasterBO = new SupplierTypeMasterBO();
                objSupplierTypeMasterData.SupplierTypeCode = txt_suppliercode.Text == "" ? null : txt_suppliercode.Text;
                objSupplierTypeMasterData.SupplierType = txt_suppliertype.Text == "" ? null : txt_suppliertype.Text;
                objSupplierTypeMasterData.ContactNo = Convert.ToInt64(txt_contactno.Text == "" ? null : txt_contactno.Text);
                objSupplierTypeMasterData.SupplierPercent = Convert.ToDecimal(txtSupplierPercent.Text.Trim() == "" ? "0" : txtSupplierPercent.Text.Trim());
                objSupplierTypeMasterData.HospitalID = LogData.HospitalID;
                objSupplierTypeMasterData.EmployeeID = LogData.EmployeeID;
                objSupplierTypeMasterData.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
                objSupplierTypeMasterData.FinancialYearID = LogData.FinancialYearID;
                objSupplierTypeMasterData.IPaddress = LogData.IPaddress;
                objSupplierTypeMasterData.ActionType = Enumaction.Insert;
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
                        objSupplierTypeMasterData.ActionType = Enumaction.Update;
                        objSupplierTypeMasterData.ID = Convert.ToInt32(ViewState["ID"].ToString());
                    }
                }
                int result = objSupplierTypeMasterBO.UpdateSupplierTypeDetails(objSupplierTypeMasterData);
                if (result == 1 || result == 2)
                {
                    lblmessage.Visible = true;
                    Messagealert_.ShowMessage(lblmessage, result == 1 ? "save" : "update", 1);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "SuccessAlert";
    
                    ViewState["ID"] = null;
                    bindgrid(1);
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
        protected void GvSupplierType_RowCommand(object sender, GridViewCommandEventArgs e)
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
                    SupplierTypeMasterData objDepartmentTypeMasterData = new SupplierTypeMasterData();
                    SupplierTypeMasterBO objDepartmentTypeMasterBO = new SupplierTypeMasterBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GvSupplierType.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("lblsuppliertypeID");
                    objDepartmentTypeMasterData.ID = Convert.ToInt32(ID.Text);
                    objDepartmentTypeMasterData.ActionType = Enumaction.Select;

                    List<SupplierTypeMasterData> GetResult = objDepartmentTypeMasterBO.GetSupplierTypeDetailsByID(objDepartmentTypeMasterData);
                    if (GetResult.Count > 0)
                    {
                        txt_suppliercode.Text = GetResult[0].SupplierTypeCode;
                        txt_suppliertype.Text = GetResult[0].SupplierType;
                        txt_contactno.Text = GetResult[0].ContactNo.ToString();
                        txtSupplierPercent.Text = Convert.ToInt32(GetResult[0].SupplierPercent).ToString();
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
                    SupplierTypeMasterData objDepartmentTypeMasterData = new SupplierTypeMasterData();
                    SupplierTypeMasterBO objDepartmentTypeMasterBO = new SupplierTypeMasterBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GvSupplierType.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("lblsuppliertypeID");
                    objDepartmentTypeMasterData.ID = Convert.ToInt32(ID.Text);
                    objDepartmentTypeMasterData.EmployeeID = LogData.EmployeeID;
                    objDepartmentTypeMasterData.ActionType = Enumaction.Delete;
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
                        objDepartmentTypeMasterData.Remarks = txtremarks.Text;
                    }

                    SupplierTypeMasterBO objDepartmentTypeMasterBO1 = new SupplierTypeMasterBO();
                    int Result = objDepartmentTypeMasterBO1.DeleteSupplierTypeDetailsByID(objDepartmentTypeMasterData);
                    if (Result == 1)
                    {
                        Messagealert_.ShowMessage(lblmessage, "delete", 1);
                        divmsg1.Visible = true;
                        divmsg1.Attributes["class"] = "SuccessAlert";
    
                        bindgrid(1);
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
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
     
            }
        }
        private void bindgrid(int page)
        {
            try
            {


                List<SupplierTypeMasterData> lstemp = GetSupplierType(page);

                if (lstemp.Count > 0)
                {
                    GvSupplierType.VirtualItemCount = lstemp[0].MaximumRows;//total item is required for custom paging
                    GvSupplierType.PageIndex = page - 1;
                    GvSupplierType.DataSource = lstemp;
                    GvSupplierType.DataBind();
                    GvSupplierType.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total: " + lstemp[0].MaximumRows.ToString() + " Record found", 1);
                    divmsg3.Visible = true;
                    divmsg3.Attributes["class"] = "SuccessAlert";

                    ddlexport.Visible = true;
                    btnexport.Visible = true;
                }
                else
                {
                    GvSupplierType.DataSource = null;
                    GvSupplierType.DataBind();
                    GvSupplierType.Visible = true;
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
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
     
            }
        }
        private List<SupplierTypeMasterData> GetSupplierTypeExcel(int p)
        {
            SupplierTypeMasterData objDepartmentTypeMasterData = new SupplierTypeMasterData();
            SupplierTypeMasterBO objDepartmentTypeMasterBO = new SupplierTypeMasterBO();
            objDepartmentTypeMasterData.SupplierTypeCode = txt_suppliercode.Text == "" ? "" : txt_suppliercode.Text;
            objDepartmentTypeMasterData.SupplierType = txt_suppliertype.Text == "" ? "" : txt_suppliertype.Text;
            objDepartmentTypeMasterData.ContactNo = Convert.ToInt64(txt_contactno.Text == "" ? "0" : txt_contactno.Text);
            objDepartmentTypeMasterData.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
            return objDepartmentTypeMasterBO.SearchSupplierTypeExcel(objDepartmentTypeMasterData);
        }
        private List<SupplierTypeMasterData> GetSupplierType(int p)
        {
            SupplierTypeMasterData objDepartmentTypeMasterData = new SupplierTypeMasterData();
            SupplierTypeMasterBO objDepartmentTypeMasterBO = new SupplierTypeMasterBO();
            objDepartmentTypeMasterData.SupplierTypeCode = txt_suppliercode.Text == "" ? "" : txt_suppliercode.Text;
            objDepartmentTypeMasterData.SupplierType = txt_suppliertype.Text == "" ? "" : txt_suppliertype.Text;
            objDepartmentTypeMasterData.ContactNo = Convert.ToInt64(txt_contactno.Text == "" ? "0" : txt_contactno.Text);
            objDepartmentTypeMasterData.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
            objDepartmentTypeMasterData.CurrentIndex = p;
            return objDepartmentTypeMasterBO.SearchSupplierTypeDetails(objDepartmentTypeMasterData);
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
            txt_contactno.Text = "";
            txt_suppliercode.Text = "";
            txt_suppliertype.Text = "";
            txtSupplierPercent.Text = "";
            ddlstatus.SelectedIndex = 0;
            GvSupplierType.DataSource = null;
            GvSupplierType.DataBind();
            GvSupplierType.Visible = false;
            ddlexport.Visible = false;
            btnexport.Visible = false;
        }
        private void ExportToPdf()
        {
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    GvSupplierType.AllowPaging = false;
                    GvSupplierType.DataSource = GetSupplierTypeExcel(0);
                    GvSupplierType.DataBind();
                    GvSupplierType.BorderStyle = BorderStyle.None;
                    //Hide the Column containing CheckBox
                    GvSupplierType.Columns[6].Visible = false;
                    GvSupplierType.Columns[7].Visible = false;
                    GvSupplierType.Columns[8].Visible = false;

                    GvSupplierType.RenderControl(hw);
                    GvSupplierType.HeaderRow.Style.Add("width", "15%");
                    GvSupplierType.HeaderRow.Style.Add("font-size", "10px");
                    GvSupplierType.Style.Add("text-decoration", "none");
                    GvSupplierType.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    GvSupplierType.Style.Add("font-size", "8px");
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 7f, 7f, 7f, 0f);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=SupplierTypeDetails.pdf");
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
                wb.Worksheets.Add(dt, "Supplier Type Detail List");

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=SupplierTypeDetails.xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }

        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        private DataTable GetDatafromDatabase()
        {
            List<SupplierTypeMasterData> SupplierTypeDetails = GetSupplierTypeExcel(0);
            List<SupplierDatatoExcel> ListexcelData = new List<SupplierDatatoExcel>();
            int i = 0;
            foreach (SupplierTypeMasterData row in SupplierTypeDetails)
            {
                SupplierDatatoExcel ExcelSevice = new SupplierDatatoExcel();
                ExcelSevice.ID = SupplierTypeDetails[i].ID;
                ExcelSevice.SupplierTypeCode = SupplierTypeDetails[i].SupplierTypeCode;
                ExcelSevice.SupplierType = SupplierTypeDetails[i].SupplierType;
                ExcelSevice.ContactNo= SupplierTypeDetails[i].ContactNo;     
                ExcelSevice.EmpName = SupplierTypeDetails[i].EmpName;
                GvSupplierType.Columns[5].Visible = false;
                GvSupplierType.Columns[6].Visible = false;
                GvSupplierType.Columns[7].Visible = false;
                GvSupplierType.Columns[8].Visible = false;
                ListexcelData.Add(ExcelSevice);
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
        protected void GvSupplierType_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            bindgrid(Convert.ToInt32(e.NewPageIndex + 1));
        }

    }
}
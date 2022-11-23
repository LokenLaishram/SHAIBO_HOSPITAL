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
    public partial class MfgCompanyMaster : BasePage
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

                if (txt_mfgcode.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Code", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
     
                    txt_mfgcode.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txt_mfgcompanytype.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Company", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
     
                    txt_mfgcompanytype.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                MfgCompanyMasterData objCompanyTypeMasterData = new MfgCompanyMasterData();
                MfgCompanyTypeMasterBO objCompanyTypeMasterBO = new MfgCompanyTypeMasterBO();
                objCompanyTypeMasterData.MfgCompanyTypeCode = txt_mfgcode.Text == "" ? null : txt_mfgcode.Text;
                objCompanyTypeMasterData.MfgCompanyType = txt_mfgcompanytype.Text == "" ? null : txt_mfgcompanytype.Text;
                objCompanyTypeMasterData.EmployeeID = LogData.EmployeeID;
                objCompanyTypeMasterData.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
                objCompanyTypeMasterData.FinancialYearID = LogData.FinancialYearID;
                objCompanyTypeMasterData.HospitalID = LogData.HospitalID;
                objCompanyTypeMasterData.IPaddress = LogData.IPaddress;
                objCompanyTypeMasterData.ActionType = Enumaction.Insert;
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
                        objCompanyTypeMasterData.ActionType = Enumaction.Update;
                        objCompanyTypeMasterData.ID = Convert.ToInt32(ViewState["ID"].ToString());
                    }
                }
                int result = objCompanyTypeMasterBO.UpdateCompanyTypeDetails(objCompanyTypeMasterData);
                if (result == 1 || result == 2)
                {
                    lblmessage.Visible = true;
                    Messagealert_.ShowMessage(lblmessage, result == 1 ? "save" : "update", 1);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "SucessAlert";
     
                    ViewState["ID"] = null;
                    bindgrid(1);
                }
                else if (result == 5)
                {
                    Messagealert_.ShowMessage(lblmessage, "duplicate", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    return;
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
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                return;

            }
        }
        protected void GvMfgCompanyType_RowCommand(object sender, GridViewCommandEventArgs e)
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
                    MfgCompanyMasterData objDepartmentTypeMasterData = new MfgCompanyMasterData();
                    MfgCompanyTypeMasterBO objDepartmentTypeMasterBO = new MfgCompanyTypeMasterBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GvMfgCompanyType.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("lblmfgcompanytypeID");
                    objDepartmentTypeMasterData.ID = Convert.ToInt32(ID.Text);
                    objDepartmentTypeMasterData.HospitalID = LogData.HospitalID;
                    objDepartmentTypeMasterData.IPaddress = LogData.IPaddress;
             
                    objDepartmentTypeMasterData.ActionType = Enumaction.Select;

                    List<MfgCompanyMasterData> GetResult = objDepartmentTypeMasterBO.GetCompanyTypeDetailsByID(objDepartmentTypeMasterData);
                    if (GetResult.Count > 0)
                    {
                        txt_mfgcode.Text = GetResult[0].MfgCompanyTypeCode;
                        txt_mfgcompanytype.Text = GetResult[0].MfgCompanyType;
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
                    MfgCompanyMasterData objDepartmentTypeMasterData = new MfgCompanyMasterData();
                    MfgCompanyTypeMasterBO objDepartmentTypeMasterBO = new MfgCompanyTypeMasterBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GvMfgCompanyType.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("lblmfgcompanytypeID");
                    objDepartmentTypeMasterData.ID = Convert.ToInt32(ID.Text);
                    objDepartmentTypeMasterData.EmployeeID = LogData.EmployeeID;
                    objDepartmentTypeMasterData.HospitalID = LogData.HospitalID;
                    objDepartmentTypeMasterData.IPaddress = LogData.IPaddress;
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

                    MfgCompanyTypeMasterBO objDepartmentTypeMasterBO1 = new MfgCompanyTypeMasterBO();
                    int Result = objDepartmentTypeMasterBO1.DeleteMfgCompanyTypeDetailsByID(objDepartmentTypeMasterData);
                    if (Result == 1)
                    {
                        Messagealert_.ShowMessage(lblmessage, "delete", 1);
                        divmsg1.Visible = true;
                        divmsg1.Attributes["class"] = "SucessAlert";
     
                        bindgrid(1);
                    }
                    else
                    {
                        Messagealert_.ShowMessage(lblmessage, "system", 0);
                        divmsg1.Visible = true;
                        divmsg1.Attributes["class"] = "FailAlert";
                        return;
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
                return;
            }
        }
        private void bindgrid(int page)
        {
            try
            {


                List<MfgCompanyMasterData> lstemp = GetCompanyType(page);

                if (lstemp.Count > 0)
                {
                    GvMfgCompanyType.VirtualItemCount = lstemp[0].MaximumRows;//total item is required for custom paging
                    GvMfgCompanyType.PageIndex = page - 1;
                    GvMfgCompanyType.DataSource = lstemp;
                    GvMfgCompanyType.DataBind();
                    GvMfgCompanyType.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total: " + lstemp[0].MaximumRows.ToString() + " Record(s) found", 1);
                    divmsg3.Visible = true;
                    divmsg3.Attributes["class"] = "SucessAlert";
                    ddlexport.Visible = true;
                    btnexport.Visible = true;
                }
                else
                {
                    GvMfgCompanyType.DataSource = null;
                    GvMfgCompanyType.DataBind();
                    GvMfgCompanyType.Visible = true;
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
        private List<MfgCompanyMasterData> GetCompanyExcel(int p)
        {
            MfgCompanyMasterData objDepartmentTypeMasterData = new MfgCompanyMasterData();
            MfgCompanyTypeMasterBO objDepartmentTypeMasterBO = new MfgCompanyTypeMasterBO();
            objDepartmentTypeMasterData.MfgCompanyTypeCode = txt_mfgcode.Text == "" ? "" : txt_mfgcode.Text;
            objDepartmentTypeMasterData.MfgCompanyType = txt_mfgcompanytype.Text == "" ? "" : txt_mfgcompanytype.Text;
            objDepartmentTypeMasterData.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
            return objDepartmentTypeMasterBO.SearchCompanyTypeExcel(objDepartmentTypeMasterData);
        }
        private List<MfgCompanyMasterData> GetCompanyType(int p)
        {
            MfgCompanyMasterData objDepartmentTypeMasterData = new MfgCompanyMasterData();
            MfgCompanyTypeMasterBO objDepartmentTypeMasterBO = new MfgCompanyTypeMasterBO();
            objDepartmentTypeMasterData.MfgCompanyTypeCode = txt_mfgcode.Text == "" ? "" : txt_mfgcode.Text;
            objDepartmentTypeMasterData.MfgCompanyType = txt_mfgcompanytype.Text == "" ? "" : txt_mfgcompanytype.Text;
            objDepartmentTypeMasterData.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
            objDepartmentTypeMasterData.CurrentIndex = p;
            return objDepartmentTypeMasterBO.SearchCompanyTypeDetails(objDepartmentTypeMasterData);
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
            txt_mfgcode.Text = "";
            txt_mfgcompanytype.Text = "";
            ddlstatus.SelectedIndex = 0;
            GvMfgCompanyType.DataSource = null;
            GvMfgCompanyType.DataBind();
            GvMfgCompanyType.Visible = false;
            ddlexport.Visible = false;
            btnexport.Visible = false;
        }
        private void ExportToPdf()
        {
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    GvMfgCompanyType.AllowPaging = false;
                    GvMfgCompanyType.DataSource = GetCompanyExcel(0);
                    GvMfgCompanyType.DataBind();
                    GvMfgCompanyType.BorderStyle = BorderStyle.None;
                    //Hide the Column containing CheckBox
                    GvMfgCompanyType.Columns[5].Visible = false;
                    GvMfgCompanyType.Columns[6].Visible = false;
                    GvMfgCompanyType.Columns[7].Visible = false;

                    GvMfgCompanyType.RenderControl(hw);
                    GvMfgCompanyType.HeaderRow.Style.Add("width", "15%");
                    GvMfgCompanyType.HeaderRow.Style.Add("font-size", "10px");
                    GvMfgCompanyType.Style.Add("text-decoration", "none");
                    GvMfgCompanyType.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    GvMfgCompanyType.Style.Add("font-size", "8px");
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 7f, 7f, 7f, 0f);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=MfgCompanyTypeDetails.pdf");
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
                wb.Worksheets.Add(dt, "Company Detail List");

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=MfgCompanyTypeDetails.xlsx");
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
            List<MfgCompanyMasterData> SupplierTypeDetails = GetCompanyExcel(0);
            List<MfgCompanyDatatoExcel> ListexcelData = new List<MfgCompanyDatatoExcel>();
            int i = 0;
            foreach (MfgCompanyMasterData row in SupplierTypeDetails)
            {
                MfgCompanyDatatoExcel ExcelSevice = new MfgCompanyDatatoExcel();
                ExcelSevice.ID = SupplierTypeDetails[i].ID;
                ExcelSevice.MfgCompanyTypeCode = SupplierTypeDetails[i].MfgCompanyTypeCode;
                ExcelSevice.MfgCompanyType = SupplierTypeDetails[i].MfgCompanyType;
                ExcelSevice.EmpName = SupplierTypeDetails[i].EmpName;
                ExcelSevice.AddedDate = SupplierTypeDetails[i].AddedDate;
                GvMfgCompanyType.Columns[5].Visible = false;
                GvMfgCompanyType.Columns[6].Visible = false;
                GvMfgCompanyType.Columns[7].Visible = false;
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
        protected void GvMfgCompanyType_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            bindgrid(Convert.ToInt32(e.NewPageIndex + 1));
        }
    }
}
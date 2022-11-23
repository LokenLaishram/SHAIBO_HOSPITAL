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
    public partial class DepartmentMaster : BasePage
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
                if (txtdepartmentcode.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Code", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
      
                    txtdepartmentcode.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txtdepartmenttype.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "dept", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
      
                    txtdepartmenttype.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }

                DepartmentTypeMasterData objDepartmentTypeMasterData = new DepartmentTypeMasterData();
                DepartmentTypeMasterBO objDepartmentTypeMasterBO = new DepartmentTypeMasterBO();
                objDepartmentTypeMasterData.DepartmentTypeCode = txtdepartmentcode.Text == "" ? null : txtdepartmentcode.Text;
                objDepartmentTypeMasterData.DepartmentType = txtdepartmenttype.Text == "" ? null : txtdepartmenttype.Text;
                objDepartmentTypeMasterData.EmployeeID = LogData.EmployeeID;
                objDepartmentTypeMasterData.IPaddress = LogData.IPaddress;
                objDepartmentTypeMasterData.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
                objDepartmentTypeMasterData.FinancialYearID = LogData.FinancialYearID;
                objDepartmentTypeMasterData.ActionType = Enumaction.Insert;
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
                        objDepartmentTypeMasterData.ActionType = Enumaction.Update;
                        objDepartmentTypeMasterData.ID = Convert.ToInt32(ViewState["ID"].ToString());
                    }
                }
                int result = objDepartmentTypeMasterBO.UpdateDepartmentTypeDetails(objDepartmentTypeMasterData);
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
        protected void GvDepartmentType_RowCommand(object sender, GridViewCommandEventArgs e)
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
                    DepartmentTypeMasterData objDepartmentTypeMasterData = new DepartmentTypeMasterData();
                    DepartmentTypeMasterBO objDepartmentTypeMasterBO = new DepartmentTypeMasterBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GvDepartmentType.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("lbldepartmenttypeID");
                    objDepartmentTypeMasterData.ID = Convert.ToInt32(ID.Text);
                    objDepartmentTypeMasterData.ActionType = Enumaction.Select;

                    List<DepartmentTypeMasterData> GetResult = objDepartmentTypeMasterBO.GetDepartmentTypeDetailsByID(objDepartmentTypeMasterData);
                    if (GetResult.Count > 0)
                    {
                        txtdepartmentcode.Text = GetResult[0].DepartmentTypeCode;
                        txtdepartmenttype.Text = GetResult[0].DepartmentType;
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
                    DepartmentTypeMasterData objDepartmentTypeMasterData = new DepartmentTypeMasterData();
                    DepartmentTypeMasterBO objDepartmentTypeMasterBO = new DepartmentTypeMasterBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GvDepartmentType.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("lbldepartmenttypeID");
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

                    DepartmentTypeMasterBO objDepartmentTypeMasterBO1 = new DepartmentTypeMasterBO();
                    int Result = objDepartmentTypeMasterBO1.DeleteDepartmentTypeDetailsByID(objDepartmentTypeMasterData);
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
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
            }
        }
        private void bindgrid()
        {
            try
            {
              
                List<DepartmentTypeMasterData> lstemp = GetDepartmentType(0);

                if (lstemp.Count > 0)
                {
                    GvDepartmentType.DataSource = lstemp;
                    GvDepartmentType.DataBind();
                    GvDepartmentType.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total: " + lstemp[0].MaximumRows.ToString() + " Record(s) found", 1);
                    divmsg3.Visible = true;
                    divmsg3.Attributes["class"] = "SucessAlert";
                    ddlexport.Visible = true;
                    btnexport.Visible = true;
                     
                }
                else
                {
                    GvDepartmentType.DataSource = null;
                    GvDepartmentType.DataBind();
                    GvDepartmentType.Visible = true;
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
        private List<DepartmentTypeMasterData> GetDepartmentType(int p)
        {
            DepartmentTypeMasterData objDepartmentTypeMasterData = new DepartmentTypeMasterData();
            DepartmentTypeMasterBO objDepartmentTypeMasterBO = new DepartmentTypeMasterBO();
            objDepartmentTypeMasterData.DepartmentTypeCode = txtdepartmentcode.Text == "" ? "" : txtdepartmentcode.Text;
            objDepartmentTypeMasterData.DepartmentType = txtdepartmenttype.Text == "" ? "" : txtdepartmenttype.Text;
            objDepartmentTypeMasterData.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
            return objDepartmentTypeMasterBO.SearchDepartmentTypeDetails(objDepartmentTypeMasterData);
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
            txtdepartmentcode.Text = "";
            txtdepartmenttype.Text = "";
            ddlstatus.SelectedIndex = 0;
            GvDepartmentType.DataSource = null;
            GvDepartmentType.DataBind();
            GvDepartmentType.Visible = false;
            ddlexport.Visible = false;
            btnexport.Visible = false;
                     
        }
        private void ExportToPdf()
        {
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    GvDepartmentType.BorderStyle = BorderStyle.None;
                    //Hide the Column containing CheckBox
                    GvDepartmentType.Columns[4].Visible = false;
                    GvDepartmentType.Columns[5].Visible = false;
                    GvDepartmentType.Columns[6].Visible = false;
                    GvDepartmentType.Columns[7].Visible = false;

                    GvDepartmentType.RenderControl(hw);
                    GvDepartmentType.HeaderRow.Style.Add("width", "15%");
                    GvDepartmentType.HeaderRow.Style.Add("font-size", "10px");
                    GvDepartmentType.Style.Add("text-decoration", "none");
                    GvDepartmentType.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    GvDepartmentType.Style.Add("font-size", "8px");
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 7f, 7f, 7f, 0f);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=DepartmentTypeDetails.pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.Write(pdfDoc);
                    Response.End();
                }
            }
        }
        protected void GvDepartmentType_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GvDepartmentType.PageIndex = e.NewPageIndex;
            bindgrid();
        }
        protected void ExportoExcel()
        {

            DataTable dt = GetDatafromDatabase();
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "Department Type Detail List");

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=DepartmentTypeDetails.xlsx");
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
            List<DepartmentTypeMasterData> DepartmentTypeDetails = GetDepartmentTypes(0);
            List<DepartmentDatatoExcel> ListexcelData = new List<DepartmentDatatoExcel>();
            int i = 0;
            foreach (DepartmentTypeMasterData row in DepartmentTypeDetails)
            {
                DepartmentDatatoExcel ExcelSevice = new DepartmentDatatoExcel();
                ExcelSevice.ID = DepartmentTypeDetails[i].ID;
                ExcelSevice.DepartmentTypeCode = DepartmentTypeDetails[i].DepartmentTypeCode;
                ExcelSevice.DepartmentType = DepartmentTypeDetails[i].DepartmentType;
                ExcelSevice.AddedBy = DepartmentTypeDetails[i].EmpName;
                GvDepartmentType.Columns[4].Visible = false;
                GvDepartmentType.Columns[5].Visible = false;
                GvDepartmentType.Columns[6].Visible = false;
                GvDepartmentType.Columns[7].Visible = false;
                ListexcelData.Add(ExcelSevice);
                i++;
            }
            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            DataTable dt = converter.ToDataTable(ListexcelData);
            return dt;
        }
        private List<DepartmentTypeMasterData> GetDepartmentTypes(int p)
        {
            DepartmentTypeMasterData objDepartmentTypeMasterData = new DepartmentTypeMasterData();
            DepartmentTypeMasterBO objDepartmentTypeMasterBO = new DepartmentTypeMasterBO();
            objDepartmentTypeMasterData.DepartmentTypeCode = txtdepartmentcode.Text == "" ? "" : txtdepartmentcode.Text;
            objDepartmentTypeMasterData.DepartmentType = txtdepartmenttype.Text == "" ? "" : txtdepartmenttype.Text;
            //  objPatientTypeMasterData.ServiceTypeID = Convert.ToInt32(ddlservicetype.SelectedValue == "" ? "0" : ddlservicetype.SelectedValue);
            objDepartmentTypeMasterData.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
            return objDepartmentTypeMasterBO.SearchDepartmentTypeDetails(objDepartmentTypeMasterData);
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
    }
}
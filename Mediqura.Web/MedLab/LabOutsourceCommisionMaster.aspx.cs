using ClosedXML.Excel;
using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedLabBO;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.MedLabData;
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
namespace Mediqura.Web.MedLab
{
    public partial class LabOutsourceCommisionMaster : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblmessage.Visible = false;
                ddlbind();
            }
        }
        private void ddlbind()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddl_testCenter, mstlookup.GetLookupsList(LookupName.TestCenter));
        }
        private void bindgrid()
        {
            try
            {

                List<OutsourceCommissionData> lstemp = GetCommission(0);
                if (lstemp.Count > 0)
                {
                    GvLabCommission.DataSource = lstemp;
                    GvLabCommission.DataBind();
                    GvLabCommission.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total: " + lstemp[0].MaximumRows.ToString() + " Record found", 1);
                    divmsg3.Attributes["class"] = "SucessAlert";
                    divmsg3.Visible = true;
                    ddlexport.Visible = true;
                    btnexport.Visible = true;
                }
                else
                {
                    GvLabCommission.DataSource = null;
                    GvLabCommission.DataBind();
                    GvLabCommission.Visible = true;
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
        private List<OutsourceCommissionData> GetCommission(int p)
        {

            OutsourceCommissionData objCommissionMasterData = new OutsourceCommissionData();
            LabCenterCommissionBO objCommissionMasterBO = new LabCenterCommissionBO();
            objCommissionMasterData.TestCenterID = Convert.ToInt32(ddl_testCenter.SelectedValue == "0" ? null : ddl_testCenter.SelectedValue);
            objCommissionMasterData.TestName = txt_testName.Text == "" ? "" : txt_testName.Text;
            objCommissionMasterData.HospCharge = Convert.ToDecimal(txt_HospCharge.Text == "" ? "0" : txt_HospCharge.Text);
            objCommissionMasterData.TestCenterCharge = Convert.ToDecimal(txt_outCharge.Text == "" ? "0" : txt_outCharge.Text);
            objCommissionMasterData.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
            return objCommissionMasterBO.SearchOutsourceDetails(objCommissionMasterData);
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
                if (ddl_testCenter.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "Subgroup", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    ddl_testCenter.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txt_testName.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Description", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    txt_testName.Focus();
                    return;
                }
                {
                    lblmessage.Visible = false;
                }
                if (txt_HospCharge.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Description", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    txt_HospCharge.Focus();
                    return;
                }
                {
                    lblmessage.Visible = false;
                }
                if (txt_outCharge.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Description", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    txt_outCharge.Focus();
                    return;
                }
                {
                    lblmessage.Visible = false;
                }

                OutsourceCommissionData objCommissionMasterData = new OutsourceCommissionData();
                LabCenterCommissionBO objCommissionMasterBO = new LabCenterCommissionBO();
                objCommissionMasterData.TestCenterID = Convert.ToInt32(ddl_testCenter.SelectedValue == "0" ? null : ddl_testCenter.SelectedValue);
                objCommissionMasterData.TestName = txt_testName.Text == "" ? "" : txt_testName.Text;
                objCommissionMasterData.HospCharge = Convert.ToDecimal(txt_HospCharge.Text == "" ? "" : txt_HospCharge.Text);
                objCommissionMasterData.TestCenterCharge = Convert.ToDecimal(txt_outCharge.Text == "" ? "" : txt_outCharge.Text);
                objCommissionMasterData.EmployeeID = LogData.EmployeeID;
                objCommissionMasterData.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
                objCommissionMasterData.HospitalID = LogData.HospitalID;
                objCommissionMasterData.FinancialYearID = LogData.FinancialYearID;
                objCommissionMasterData.ActionType = Enumaction.Insert;
                if (ViewState["ID"] != null)
                {
                    if (LogData.UpdateEnable == 0)
                    {
                        Messagealert_.ShowMessage(lblmessage, "UpdateEnable", 0);
                        div1.Visible = true;
                        div1.Attributes["class"] = "FailAlert";
                        ddl_testCenter.Focus();
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                    }
                    objCommissionMasterData.ActionType = Enumaction.Update;
                    objCommissionMasterData.CommissionID = Convert.ToInt32(ViewState["ID"].ToString());
                }
                int result = objCommissionMasterBO.UpdateOutsourceDetails(objCommissionMasterData);  // funtion at DAL
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

        protected void btnresets_Click(object sender, EventArgs e)
        {
            ViewState["ID"] = null;
            clearall();
            lblmessage.Visible = false;
            lblresult.Visible = false;
        }
        private void clearall()
        {
            ddl_testCenter.SelectedIndex = 0;
            txt_testName.Text = "";
            txt_HospCharge.Text = "";
            txt_outCharge.Text = "";
            GvLabCommission.DataSource = null;
            GvLabCommission.DataBind();
            GvLabCommission.Visible = false;
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
                    GvLabCommission.BorderStyle = BorderStyle.None;
                    //Hide the Column containing CheckBox
                    GvLabCommission.Columns[4].Visible = false;
                    GvLabCommission.Columns[5].Visible = false;
                    GvLabCommission.Columns[6].Visible = false;
                    GvLabCommission.Columns[7].Visible = false;

                    GvLabCommission.RenderControl(hw);
                    GvLabCommission.HeaderRow.Style.Add("width", "15%");
                    GvLabCommission.HeaderRow.Style.Add("font-size", "10px");
                    GvLabCommission.Style.Add("text-decoration", "none");
                    GvLabCommission.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    GvLabCommission.Style.Add("font-size", "8px");
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 7f, 7f, 7f, 0f);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=OutsourceCommissionDetails.pdf");
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
                Response.AddHeader("content-disposition", "attachment;filename=OutsourceCommissionDetails.xlsx");
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
            List<OutsourceCommissionData> OTRoleDetails = GetCommission(0);
            List<OutsourceCommissionDatatoExcel> ListexcelData = new List<OutsourceCommissionDatatoExcel>();
            int i = 0;
            foreach (OutsourceCommissionData row in OTRoleDetails)
            {
                OutsourceCommissionDatatoExcel ExcelSevice = new OutsourceCommissionDatatoExcel();
                ExcelSevice.CommissionID = OTRoleDetails[i].CommissionID;
                ExcelSevice.TestCenterName = OTRoleDetails[i].TestCenterName;
                ExcelSevice.TestName = OTRoleDetails[i].TestName;
                ExcelSevice.HospCharge = OTRoleDetails[i].HospCharge;
                ExcelSevice.TestCenterCharge = OTRoleDetails[i].TestCenterCharge;
                ExcelSevice.AddedBy = OTRoleDetails[i].EmpName;
                GvLabCommission.Columns[4].Visible = false;
                GvLabCommission.Columns[5].Visible = false;
                GvLabCommission.Columns[6].Visible = false;
                GvLabCommission.Columns[7].Visible = false;
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
        protected void GvLabCommission_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GvLabCommission.PageIndex = e.NewPageIndex;
            bindgrid();
        }

        protected void GvLabCommission_RowCommand(object sender, GridViewCommandEventArgs e)
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
                    OutsourceCommissionData objCommissionMasterData = new OutsourceCommissionData();
                    LabCenterCommissionBO objCommissionMasterBO = new LabCenterCommissionBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow pt = GvLabCommission.Rows[i];
                    Label ID = (Label)pt.Cells[0].FindControl("code");
                    objCommissionMasterData.CommissionID = Convert.ToInt32(ID.Text);
                    objCommissionMasterData.ActionType = Enumaction.Select;

                    List<OutsourceCommissionData> GetResult = objCommissionMasterBO.GetOutsourceDetailsByID(objCommissionMasterData);
                    if (GetResult.Count > 0)
                    {
                        ddl_testCenter.SelectedValue = GetResult[0].TestCenterID.ToString();
                        txt_testName.Text = GetResult[0].TestName;
                        txt_HospCharge.Text = GetResult[0].HospCharge.ToString();
                        txt_outCharge.Text = GetResult[0].TestCenterCharge.ToString();
                        ViewState["ID"] = GetResult[0].CommissionID;
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
                    OutsourceCommissionData objCommissionMasterData = new OutsourceCommissionData();
                    LabCenterCommissionBO objCommissionMasterBO = new LabCenterCommissionBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GvLabCommission.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("code");
                    objCommissionMasterData.CommissionID = Convert.ToInt32(ID.Text);
                    objCommissionMasterData.EmployeeID = LogData.EmployeeID;
                    objCommissionMasterData.ActionType = Enumaction.Delete;
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
                        objCommissionMasterData.Remarks = txtremarks.Text;
                    }

                    LabCenterCommissionBO objCommissionMasterBO1 = new LabCenterCommissionBO();
                    int Result = objCommissionMasterBO1.DeleteOutsourceDetailsByID(objCommissionMasterData);
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
    }
}
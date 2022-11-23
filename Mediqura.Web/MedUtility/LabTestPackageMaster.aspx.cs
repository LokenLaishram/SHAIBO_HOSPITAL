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
    public partial class LabTestPackageMaster :  BasePage
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
            Commonfunction.PopulateDdl(ddlcompany, mstlookup.GetLookupsList(LookupName.Company));
            Commonfunction.PopulateDdl(ddlpackage, mstlookup.GetLookupsList(LookupName.Package));
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
                if (ddlcompany.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "company", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";

                    ddlcompany.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (ddlpackage.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "package", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";

                    ddlpackage.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txt_labtestname.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "TestName", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";

                    txt_labtestname.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txt_testamount.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Charge", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";

                    txt_testamount.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }

                LabTestPackageMasterData objlabserviceData = new LabTestPackageMasterData();
                LabTestPackageMasterBO objlabserviceBO = new LabTestPackageMasterBO();
                objlabserviceData.CompanyID = Convert.ToInt32(ddlcompany.SelectedValue == "0" ? null : ddlcompany.SelectedValue);
                objlabserviceData.PackageID = Convert.ToInt32(ddlpackage.SelectedValue == "0" ? null : ddlpackage.SelectedValue);
                objlabserviceData.TestName = txt_labtestname.Text == "" ? null : txt_labtestname.Text;
                objlabserviceData.TestAmount = Convert.ToDecimal(txt_testamount.Text == "" ? null : txt_testamount.Text);
                objlabserviceData.EmployeeID = LogData.EmployeeID;
                objlabserviceData.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
                objlabserviceData.FinancialYearID = LogData.FinancialYearID;
                objlabserviceData.IPaddress = LogData.IPaddress;
                objlabserviceData.HospitalID = LogData.HospitalID;

                objlabserviceData.ActionType = Enumaction.Insert;
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
                        objlabserviceData.ActionType = Enumaction.Update;
                        objlabserviceData.ID = Convert.ToInt32(ViewState["ID"].ToString());
                    }
                }
                int result = objlabserviceBO.UpdateLabTestPackageDetails(objlabserviceData);
                if (result == 1 || result == 2)
                {
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
        protected void GvLabService_RowCommand(object sender, GridViewCommandEventArgs e)
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

                        LabTestPackageMasterData objlabserviceData = new LabTestPackageMasterData();
                        LabTestPackageMasterBO objlabserviceBO = new LabTestPackageMasterBO();
                        int i = Convert.ToInt16(e.CommandArgument.ToString());
                        GridViewRow gr = GvLabService.Rows[i];
                        Label ID = (Label)gr.Cells[0].FindControl("code");
                        objlabserviceData.ID = Convert.ToInt32(ID.Text);
                        objlabserviceData.ActionType = Enumaction.Select;

                        List<LabTestPackageMasterData> GetResult = objlabserviceBO.GetLabTestPackageDetailsByID(objlabserviceData);
                        if (GetResult.Count > 0)
                        {
                            ddlcompany.SelectedValue = GetResult[0].CompanyID.ToString();
                            ddlpackage.SelectedValue = GetResult[0].PackageID.ToString();
                            txt_labtestname.Text = GetResult[0].TestName;
                            txt_testamount.Text = Commonfunction.Getrounding(GetResult[0].TestAmount.ToString());
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
                        LabTestPackageMasterData objlabserviceData = new LabTestPackageMasterData();
                        LabTestPackageMasterBO objlabserviceBO = new LabTestPackageMasterBO();
                        int i = Convert.ToInt16(e.CommandArgument.ToString());
                        GridViewRow gr = GvLabService.Rows[i];
                        Label ID = (Label)gr.Cells[0].FindControl("code");
                        objlabserviceData.ID = Convert.ToInt32(ID.Text);
                        objlabserviceData.EmployeeID = LogData.EmployeeID;
                        objlabserviceData.ActionType = Enumaction.Delete;
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
                            objlabserviceData.Remarks = txtremarks.Text;
                        }
                        LabTestPackageMasterBO objlabserviceBO1 = new LabTestPackageMasterBO();
                        int Result = objlabserviceBO1.DeleteLabTestPackageDetailsByID(objlabserviceData);
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


                List<LabTestPackageMasterData> lstemp = GetLabTestPackageType(0);

                if (lstemp.Count > 0)
                {
                    GvLabService.DataSource = lstemp;
                    GvLabService.DataBind();
                    GvLabService.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total: " + lstemp[0].MaximumRows.ToString() + " Record found", 1);
                    divmsg3.Visible = true;
                    divmsg3.Attributes["class"] = "SucessAlert";
                    ddlexport.Visible = true;
                    btnexport.Visible = true;
                }
                else
                {
                    GvLabService.DataSource = null;
                    GvLabService.DataBind();
                    GvLabService.Visible = true;
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
        private List<LabTestPackageMasterData> GetLabTestPackageType(int p)
        {
            LabTestPackageMasterData objlabserviceData = new LabTestPackageMasterData();
            LabTestPackageMasterBO objlabserviceBO = new LabTestPackageMasterBO();
            objlabserviceData.CompanyID = Convert.ToInt32(ddlcompany.SelectedValue == "" ? null : ddlcompany.SelectedValue);
            objlabserviceData.PackageID = Convert.ToInt32(ddlpackage.SelectedValue == "" ? null : ddlpackage.SelectedValue);
            objlabserviceData.TestName = txt_labtestname.Text == "" ? "" : txt_labtestname.Text;
            objlabserviceData.TestAmount = Convert.ToDecimal(txt_testamount.Text == "" ? "0" : txt_testamount.Text);
            objlabserviceData.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
            return objlabserviceBO.SearchLabTestPackageDetails(objlabserviceData);
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
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddlcompany, mstlookup.GetLookupsList(LookupName.Company));
            Commonfunction.PopulateDdl(ddlpackage, mstlookup.GetLookupsList(LookupName.Package));
            ddlstatus.SelectedIndex = 0;
            txt_labtestname.Text = "";
            txt_testamount.Text = "";
            GvLabService.DataSource = null;
            GvLabService.DataBind();
            GvLabService.Visible = false;
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
        protected void ExportoExcel()
        {

            DataTable dt = GetDatafromDatabase();
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "Service Detail List");

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=LabTestPackagesDetails.xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }

        }
        public void ExportToPdf()
        {
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    GvLabService.BorderStyle = BorderStyle.None;
                    //Hide the Column containing CheckBox
                    GvLabService.Columns[6].Visible = false;
                    GvLabService.Columns[7].Visible = false;
                    GvLabService.Columns[8].Visible = false;
                    GvLabService.Columns[9].Visible = false;
                    GvLabService.RenderControl(hw);
                    GvLabService.HeaderRow.Style.Add("width", "15%");
                    GvLabService.HeaderRow.Style.Add("font-size", "10px");
                    GvLabService.Style.Add("text-decoration", "none");
                    GvLabService.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    GvLabService.Style.Add("font-size", "8px");
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 7f, 7f, 7f, 0f);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=LabTestPackagesDetails.pdf");
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
        protected DataTable GetDatafromDatabase()
        {
            List<LabTestPackageMasterData> LabServiceDetails = GetLabTestPackageType(0);
            List<LabTestPackageDataToExcel> ListexcelData = new List<LabTestPackageDataToExcel>();
            int i = 0;
            foreach (LabTestPackageMasterData row in LabServiceDetails)
            {
                LabTestPackageDataToExcel ExcelSevice = new LabTestPackageDataToExcel();
                ExcelSevice.ID = LabServiceDetails[i].ID;
                ExcelSevice.Company = LabServiceDetails[i].Company;
                ExcelSevice.Package = LabServiceDetails[i].Package;
                ExcelSevice.TestName = LabServiceDetails[i].TestName;
                ExcelSevice.TestAmount = LabServiceDetails[i].TestAmount;
                ExcelSevice.AddedBy = LabServiceDetails[i].EmpName;
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
        protected void GvLabService_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GvLabService.PageIndex = e.NewPageIndex;
            bindgrid();
        }
    }
}
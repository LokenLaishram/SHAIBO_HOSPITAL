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

namespace Mediqura.Web.MedUtility
{
    public partial class PackageMaster : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblmessage.Visible = false;
                bindddl();
            }
        }
        private void bindddl()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddl_company, mstlookup.GetLookupsList(LookupName.TPAList));
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
                if (ddl_company.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "company", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";

                    ddl_company.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txt_description.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "tpa", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    txt_description.Focus();
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
                if (ddl_sharetype.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "ShareType", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";

                    ddl_company.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                } if (txt_hospitalshare.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "HospitalShare", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";

                    txt_hospitalshare.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txt_consultantshare.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "ConsultantShare", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";

                    txt_consultantshare.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txt_reportingshare.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "ReportingShare", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    txt_reportingshare.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (ddl_sharetype.SelectedIndex == 1)
                {
                    if ((Convert.ToDouble(txt_hospitalshare.Text == "" ? "0.0" : txt_hospitalshare.Text) + Convert.ToDouble(txt_consultantshare.Text == "" ? "0.0" : txt_consultantshare.Text) + Convert.ToDouble(txt_reportingshare.Text == "" ? "0.0" : txt_reportingshare.Text)) != 100)
                    {
                        Messagealert_.ShowMessage(lblmessage, "PC", 0);
                        divmsg1.Visible = true;
                        divmsg1.Attributes["class"] = "FailAlert";
                        txt_hospitalshare.Focus();
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                    }
                }
                else if (ddl_sharetype.SelectedIndex == 2)
                {
                    if ((Convert.ToDouble(txt_hospitalshare.Text == "" ? "0.0" : txt_hospitalshare.Text) + Convert.ToDouble(txt_consultantshare.Text == "" ? "0.0" : txt_consultantshare.Text)) > Convert.ToDouble(txt_testamount.Text == "" ? "0.0" : txt_testamount.Text))
                    {
                        Messagealert_.ShowMessage(lblmessage, "Share", 0);
                        divmsg1.Visible = true;
                        divmsg1.Attributes["class"] = "FailAlert";
                        txt_consultantshare.Focus();
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                    }
                }
                PackageMasterData objData = new PackageMasterData();
                PackageMasterBO objMasterBO = new PackageMasterBO();
                objData.CompanyID = Convert.ToInt32(ddl_company.SelectedValue == "0" ? null : ddl_company.SelectedValue);
                objData.Description = txt_description.Text == "" ? "" : txt_description.Text;
                objData.Charges = Convert.ToDecimal(txt_testamount.Text == "" ? "" : txt_testamount.Text);
                objData.HospitalShare = Convert.ToDecimal(txt_hospitalshare.Text == "" ? "" : txt_hospitalshare.Text);
                objData.ConsultantShare = Convert.ToDecimal(txt_consultantshare.Text == "" ? "" : txt_consultantshare.Text);
                objData.ReportingShare = Convert.ToDecimal(txt_reportingshare.Text == "" ? "" : txt_reportingshare.Text);
                objData.ShareTypeID = Convert.ToInt32(ddl_sharetype.SelectedValue == "0" ? null : ddl_sharetype.SelectedValue);
                objData.EmployeeID = LogData.EmployeeID;
                objData.IPaddress = LogData.IPaddress;
                objData.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
                objData.FinancialYearID = LogData.FinancialYearID;
                objData.ActionType = Enumaction.Insert;
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
                        objData.ActionType = Enumaction.Update;
                        objData.ID = Convert.ToInt32(ViewState["ID"].ToString());
                    }
                }
                int result = objMasterBO.UpdatePackageDetails(objData);
                if (result == 1 || result == 2)
                {
                    bindgrid();
                    Messagealert_.ShowMessage(lblmessage, result == 1 ? "save" : "update", 1);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "SucessAlert";
                    ViewState["ID"] = null;
                }
                else if (result == 5)
                {
                    Messagealert_.ShowMessage(lblmessage, "duplicate", 0);
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
        protected void GvTPAType_RowCommand(object sender, GridViewCommandEventArgs e)
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
                    PackageMasterData objMasterData = new PackageMasterData();
                    PackageMasterBO objMasterBO = new PackageMasterBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GvTPAType.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("lblid");
                    objMasterData.ID = Convert.ToInt32(ID.Text);
                    objMasterData.ActionType = Enumaction.Select;
                    List<PackageMasterData> GetResult = objMasterBO.GetPackageDetailsByID(objMasterData);
                    if (GetResult.Count > 0)
                    {
                        ddl_company.SelectedValue = GetResult[0].CompanyID.ToString();
                        txt_description.Text = GetResult[0].Description;
                        txt_testamount.Text = Commonfunction.Getrounding(GetResult[0].Charges.ToString());
                        ddl_sharetype.SelectedValue = GetResult[0].ShareTypeID.ToString();
                        txt_hospitalshare.Text = Commonfunction.Getrounding(GetResult[0].HospitalShare.ToString());
                        txt_consultantshare.Text = Commonfunction.Getrounding(GetResult[0].ConsultantShare.ToString());
                        txt_reportingshare.Text = Commonfunction.Getrounding(GetResult[0].ReportingShare.ToString());
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
                    PackageMasterData objData = new PackageMasterData();
                    PackageMasterBO objMasterBO = new PackageMasterBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GvTPAType.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("lblid");
                    objData.ID = Convert.ToInt32(ID.Text);
                    objData.EmployeeID = LogData.EmployeeID;
                    objData.ActionType = Enumaction.Delete;
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
                        objData.Remarks = txtremarks.Text;
                    }

                    PackageMasterBO objMasterBO1 = new PackageMasterBO();
                    int Result = objMasterBO1.DeletePackageTypeDetailsByID(objData);
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
                List<PackageMasterData> lstemp = GetPackageType(0);
                if (lstemp.Count > 0)
                {
                    GvTPAType.DataSource = lstemp;
                    GvTPAType.DataBind();
                    GvTPAType.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total: " + lstemp[0].MaximumRows.ToString() + " Record(s) found.", 1);
                    divmsg3.Visible = true;
                    divmsg3.Attributes["class"] = "SucessAlert";
                    ddlexport.Visible = true;
                    btnexport.Visible = true;
                }
                else
                {
                    GvTPAType.DataSource = null;
                    GvTPAType.DataBind();
                    GvTPAType.Visible = true;
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
        private List<PackageMasterData> GetPackageType(int p)
        {
            PackageMasterData objData = new PackageMasterData();
            PackageMasterBO objMasterBO = new PackageMasterBO();
            objData.CompanyID = Convert.ToInt32(ddl_company.SelectedValue == "" ? "0" : ddl_company.SelectedValue);
            objData.Description = txt_description.Text == "" ? "" : txt_description.Text;
            objData.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
            return objMasterBO.SearchPackageTypeDetails(objData);
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
            txt_reportingshare.Text = "";
        }
        private void clearall()
        {
            txt_description.Text = "";
            ddlstatus.SelectedIndex = 0;
            GvTPAType.DataSource = null;
            GvTPAType.DataBind();
            GvTPAType.Visible = false;
            ddlexport.Visible = false;
            btnexport.Visible = false;
            ddl_company.SelectedIndex = 0;
            txt_testamount.Text = "";
            txt_hospitalshare.Text = "";
            txt_consultantshare.Text = "";
            ddl_sharetype.SelectedIndex = 0;
        }
        private void ExportToPdf()
        {
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    GvTPAType.BorderStyle = BorderStyle.None;
                    //Hide the Column containing CheckBox
                    GvTPAType.Columns[10].Visible = false;
                    GvTPAType.Columns[11].Visible = false;
                    GvTPAType.Columns[12].Visible = false;
                    GvTPAType.RenderControl(hw);
                    GvTPAType.HeaderRow.Style.Add("width", "15%");
                    GvTPAType.HeaderRow.Style.Add("font-size", "10px");
                    GvTPAType.Style.Add("text-decoration", "none");
                    GvTPAType.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    GvTPAType.Style.Add("font-size", "8px");
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 7f, 7f, 7f, 0f);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=PackageDetails.pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.Write(pdfDoc);
                    Response.End();
                }
            }
        }
        protected void GvTPAType_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GvTPAType.PageIndex = e.NewPageIndex;
            bindgrid();
        }
        protected void ExportoExcel()
        {
            DataTable dt = GetDatafromDatabase();
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "PackageDetails");
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=PackageDetails.xlsx");
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
            List<PackageMasterData> DepartmentTypeDetails = GetPackageType(0);
            List<PackageDatatoExcel> ListexcelData = new List<PackageDatatoExcel>();
            int i = 0;
            foreach (PackageMasterData row in DepartmentTypeDetails)
            {
                PackageDatatoExcel ExcelSevice = new PackageDatatoExcel();
                ExcelSevice.ID = DepartmentTypeDetails[i].ID;
                ExcelSevice.Company = DepartmentTypeDetails[i].Company.ToString();
                ExcelSevice.Description = DepartmentTypeDetails[i].Description.ToString();
                ExcelSevice.Charges = Commonfunction.Getrounding(DepartmentTypeDetails[i].Charges.ToString());
                ExcelSevice.ShareType = DepartmentTypeDetails[i].ShareType.ToString();
                ExcelSevice.HospitalShare = Commonfunction.Getrounding(DepartmentTypeDetails[i].HospitalShare.ToString());
                ExcelSevice.ConsultantShare = Commonfunction.Getrounding(DepartmentTypeDetails[i].ConsultantShare.ToString());
                ExcelSevice.ReportingShare = Commonfunction.Getrounding(DepartmentTypeDetails[i].ReportingShare.ToString());
                ExcelSevice.AddedBy = DepartmentTypeDetails[i].EmpName;
                GvTPAType.Columns[10].Visible = false;
                GvTPAType.Columns[11].Visible = false;
                GvTPAType.Columns[12].Visible = false;
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
    }
}
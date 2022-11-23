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
    public partial class DoctorTaxMST : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                lblmessage.Visible = false;
                ddlbind();
                checkSelect();
               
            }
        }
        private void ddlbind()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddl_Srvgroup, mstlookup.GetLookupsList(LookupName.ServiceType));
            Commonfunction.Insertzeroitemindex(ddl_Srvsubgroup);
            Commonfunction.PopulateDdl(ddl_Srvsubgroup, mstlookup.GetLookupsList(LookupName.LabServiceSubGroup));
            Commonfunction.PopulateDdl(ddl_DocName, mstlookup.GetLookupsList(LookupName.TaxDoctors));
        }
        public void checkSelect()
        {
            if (ddl_Srvgroup.SelectedIndex == 0)
            {
                ddl_Srvsubgroup.Attributes["disabled"] = "disabled";
            }
            else
            {
                ddl_Srvsubgroup.Attributes.Remove("disabled");
            }
            if (ddl_Srvsubgroup.SelectedIndex == 0)
            {
                ddl_DocName.Attributes["disabled"] = "disabled";
            }
            else
            {
                ddl_DocName.Attributes.Remove("disabled");
            }
            if (ddl_DocName.SelectedIndex == 0)
            {
                txt_tax.Attributes["disabled"] = "disabled";
            }
            else
            {
                txt_tax.Attributes.Remove("disabled");
            }


        }
        protected void ddl_Srvgroup_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (ddl_Srvgroup.SelectedIndex > 0)
            {
                MasterLookupBO mstlookup = new MasterLookupBO();
                Commonfunction.PopulateDdl(ddl_Srvsubgroup, mstlookup.GetServiceNameByGroupID(Convert.ToInt32(ddl_Srvgroup.SelectedValue)));
                checkSelect();
            }
        }

        protected void btnsave_Click(object sender, System.EventArgs e)
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
                if (ddl_Srvgroup.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "ServiceType", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";

                    ddl_Srvgroup.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (ddl_Srvsubgroup.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "ServiceSubGroup", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";

                    ddl_Srvsubgroup.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (ddl_DocName.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "Doctor", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";

                    ddl_DocName.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txt_tax.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Tax", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    txt_tax.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                DocTaxData objDocTaxData = new DocTaxData();
                DocTaxBO objDocTaxBO = new DocTaxBO();
                objDocTaxData.ServiceID = Convert.ToInt32(ddl_Srvgroup.SelectedValue == "0" ? null : ddl_Srvgroup.SelectedValue);
                objDocTaxData.ServiceSubGrpID = Convert.ToInt32(ddl_Srvsubgroup.SelectedValue == "0" ? null : ddl_Srvsubgroup.SelectedValue);
                objDocTaxData.DoctorID = Convert.ToInt32(ddl_DocName.SelectedValue == "" ? "0" : ddl_DocName.SelectedValue);
                objDocTaxData.Tax =Convert.ToDecimal(txt_tax.Text == "" ? null : txt_tax.Text);
                objDocTaxData.EmployeeID = LogData.EmployeeID;
                objDocTaxData.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
                objDocTaxData.HospitalID = LogData.HospitalID;
                objDocTaxData.FinancialYearID = LogData.FinancialYearID;
                objDocTaxData.ActionType = Enumaction.Insert;
                if (ViewState["ID"] != null)
                {
                    if (LogData.UpdateEnable == 0)
                    {
                        Messagealert_.ShowMessage(lblmessage, "UpdateEnable", 0);
                        divmsg1.Visible = true;
                        divmsg1.Attributes["class"] = "FailAlert";
                        ddl_Srvgroup.Focus();
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                    }
                    objDocTaxData.ActionType = Enumaction.Update;
                    objDocTaxData.ID = Convert.ToInt32(ViewState["ID"].ToString());
                }
                int result = objDocTaxBO.UpdateDocTaxDetails(objDocTaxData);  // funtion at DAL
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
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
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

        protected void btnsearch_Click(object sender, System.EventArgs e)
        {
            bindgrid();
        }
        private void bindgrid()
        {
            try
            {

                List<DocTaxData> lstemp = GetDocTax(0);
                if (lstemp.Count > 0)
                {
                    GvDocTax.DataSource = lstemp;
                    GvDocTax.DataBind();
                    GvDocTax.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total: " + lstemp[0].MaximumRows.ToString() + " Record found", 1);
                    divmsg3.Attributes["class"] = "SucessAlert";
                    divmsg3.Visible = true;
                    ddlexport.Visible = true;
                    btnexport.Visible = true;
                }
                else
                {
                    GvDocTax.DataSource = null;
                    GvDocTax.DataBind();
                    GvDocTax.Visible = true;
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
        private List<DocTaxData> GetDocTax(int p)
        {

            DocTaxData objDocTaxData = new DocTaxData();
            DocTaxBO objDocTaxBO = new DocTaxBO();
            objDocTaxData.ServiceID = Convert.ToInt32(ddl_Srvgroup.SelectedValue == "0" ? null : ddl_Srvgroup.SelectedValue);
            objDocTaxData.ServiceSubGrpID = Convert.ToInt32(ddl_Srvsubgroup.SelectedValue == "0" ? null : ddl_Srvsubgroup.SelectedValue);
            objDocTaxData.DoctorID = Convert.ToInt32(ddl_DocName.SelectedValue == "" ? "0" : ddl_DocName.SelectedValue);
            objDocTaxData.Tax = Convert.ToDecimal(txt_tax.Text == "" ? null : txt_tax.Text);
            objDocTaxData.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
            return objDocTaxBO.SearchDocTaxDetails(objDocTaxData);
        }
        protected void btnresets_Click(object sender, System.EventArgs e)
        {
            ViewState["ID"] = null;
            clearall();
            lblmessage.Visible = false;
            lblresult.Visible = false;
        }
        private void clearall()
        {
            txt_tax.Text = "";
            ddl_Srvgroup.SelectedIndex = 0;
            ddl_Srvsubgroup.SelectedIndex = 0;
            ddl_DocName.SelectedIndex = 0;
            ddl_Srvsubgroup.Attributes["disabled"] = "disabled";
            ddl_DocName.Attributes["disabled"] = "disabled";
            txt_tax.Attributes["disabled"] = "disabled";
            GvDocTax.DataSource = null;
            GvDocTax.DataBind();
            GvDocTax.Visible = false;
            ddlexport.Visible = false;
            btnexport.Visible = false;
        }
        protected void GvDocTax_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
        {
            GvDocTax.PageIndex = e.NewPageIndex;
            bindgrid();
        }

        protected void GvDocTax_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
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
                    DocTaxData objOTRoleMasterData = new DocTaxData();
                    DocTaxBO objOTRoleMasterBO = new DocTaxBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow pt = GvDocTax.Rows[i];
                    Label ID = (Label)pt.Cells[0].FindControl("lblID");
                    objOTRoleMasterData.ID = Convert.ToInt32(ID.Text);
                    objOTRoleMasterData.ActionType = Enumaction.Select;

                    List<DocTaxData> GetResult = objOTRoleMasterBO.GetDocTaxDetailsByID(objOTRoleMasterData);
                    if (GetResult.Count > 0)
                    {
                        ddl_Srvgroup.Attributes.Remove("disabled");
                        ddl_Srvsubgroup.Attributes.Remove("disabled");
                        ddl_DocName.Attributes.Remove("disabled");
                        txt_tax.Attributes.Remove("disabled");
                        ddl_Srvgroup.SelectedValue = GetResult[0].ServiceID.ToString();
                        ddl_Srvsubgroup.SelectedValue = GetResult[0].ServiceSubGrpID.ToString();
                        ddl_DocName.SelectedValue = GetResult[0].DoctorID.ToString();
                        txt_tax.Text = Commonfunction.Getrounding((Convert.ToDecimal(GetResult[0].Tax.ToString() == "" ? "0" : GetResult[0].Tax.ToString())).ToString());
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
                    DocTaxData objOTRoleMasterData = new DocTaxData();
                    DocTaxBO objOTRoleMasterBO = new DocTaxBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GvDocTax.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("lblID");
                    objOTRoleMasterData.ID = Convert.ToInt32(ID.Text);
                    objOTRoleMasterData.EmployeeID = LogData.EmployeeID;
                    objOTRoleMasterData.ActionType = Enumaction.Delete;
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
                        objOTRoleMasterData.Remarks = txtremarks.Text;
                    }

                    DocTaxBO objOTRoleMasterBO1 = new DocTaxBO();
                    int Result = objOTRoleMasterBO1.DeleteDocTaxDetailsByID(objOTRoleMasterData);
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
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
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
                    GvDocTax.BorderStyle = BorderStyle.None;
                    //Hide the Column containing CheckBox
                    GvDocTax.Columns[6].Visible = false;
                    GvDocTax.Columns[7].Visible = false;
                    GvDocTax.Columns[8].Visible = false;
                    GvDocTax.RenderControl(hw);
                    GvDocTax.HeaderRow.Style.Add("width", "15%");
                    GvDocTax.HeaderRow.Style.Add("font-size", "10px");
                    GvDocTax.Style.Add("text-decoration", "none");
                    GvDocTax.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    GvDocTax.Style.Add("font-size", "8px");
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 7f, 7f, 7f, 0f);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=DocTaxDetails.pdf");
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
                Response.AddHeader("content-disposition", "attachment;filename=DocTaxDetails.xlsx");
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
            List<DocTaxData> DocTaxDetails = GetDocTax(0);
            List<DocTaxDatatoExcel> ListexcelData = new List<DocTaxDatatoExcel>();
            int i = 0;
            foreach (DocTaxData row in DocTaxDetails)
            {
                DocTaxDatatoExcel ExcelSevice = new DocTaxDatatoExcel();
                ExcelSevice.ID = DocTaxDetails[i].ID;
                ExcelSevice.ServiceType = DocTaxDetails[i].ServiceType;
                ExcelSevice.ServiceName = DocTaxDetails[i].ServiceName;
                ExcelSevice.DocName = DocTaxDetails[i].EmpName;
                ExcelSevice.Tax = DocTaxDetails[i].Tax;
                ExcelSevice.AddedDate = DocTaxDetails[i].AddedDate;
                GvDocTax.Columns[6].Visible = false;
                GvDocTax.Columns[7].Visible = false;
                GvDocTax.Columns[8].Visible = false;
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

        protected void ddl_Srvsubgroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkSelect();
        }

        protected void ddl_DocName_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkSelect();
        }
    }
}
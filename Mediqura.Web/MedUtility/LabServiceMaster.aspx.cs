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
    public partial class LabServiceMaster : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblmessage.Visible = false;
                ddlbind();
                id_reporttpye.Visible = false;
                bindgrid(1);
            }
        }
        private void ddlbind()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddl_labgroup, mstlookup.GetLookupsList(LookupName.InvestigationGroup));
            Commonfunction.Insertzeroitemindex(ddl_labsubgroup);
            Commonfunction.PopulateDdl(ddl_testcenter, mstlookup.GetLookupsList(LookupName.TestCenter));
            Commonfunction.Insertzeroitemindex(ddl_labsubgroup);
        }
        protected void ddl_labsubgroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_labsubgroup.SelectedIndex > 0)
            {
                //txt_labtestname.ReadOnly = false;
                AutoCompleteExtender2.ContextKey = ddl_labsubgroup.SelectedValue == "" ? "0" : ddl_labsubgroup.SelectedValue;
            }
            else
            {
                AutoCompleteExtender2.ContextKey = null;
                //txt_labtestname.ReadOnly = true;
            }
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetLabServices(string prefixText, int count, string contextKey)
        {
            LabServiceMasterData Objpaic = new LabServiceMasterData();
            LabServiceMasterBO objInfoBO = new LabServiceMasterBO();
            List<LabServiceMasterData> getResult = new List<LabServiceMasterData>();
            Objpaic.TestName = prefixText;
            Objpaic.LabSubGroupID = Convert.ToInt32(contextKey);
            getResult = objInfoBO.GetTestNames(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].TestName.ToString());
            }
            return list;
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
                if (ddl_labgroup.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "Group", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    ddl_labgroup.Focus();
                    return;
                }
                else
                {
                    if (ddl_labgroup.SelectedValue == "1" && ddl_reportypes.SelectedIndex == 0)
                    {
                        Messagealert_.ShowMessage(lblmessage, "LabReportType", 0);
                        divmsg1.Visible = true;
                        divmsg1.Attributes["class"] = "FailAlert";
                        ddl_labgroup.Focus();
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                    }
                }
                if (ddl_labsubgroup.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "Subgroup", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";

                    ddl_labgroup.Focus();
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
                //if (ddl_testcenter.SelectedIndex == 0)
                //{
                //    Messagealert_.ShowMessage(lblmessage, "TestCenter", 0);
                //    divmsg1.Visible = true;
                //    divmsg1.Attributes["class"] = "FailAlert";
                //    ddl_testcenter.Focus();
                //    return;
                //}
                //else
                //{
                //    lblmessage.Visible = false;
                //}
                //if (txt_testamount.Text == "")
                //{
                //    Messagealert_.ShowMessage(lblmessage, "Charge", 0);
                //    divmsg1.Visible = true;
                //    divmsg1.Attributes["class"] = "FailAlert";

                //    txt_testamount.Focus();
                //    return;
                //}
                //else
                //{
                //    lblmessage.Visible = false;
                //}

                LabServiceMasterData objlabserviceData = new LabServiceMasterData();
                LabServiceMasterBO objlabserviceBO = new LabServiceMasterBO();
                objlabserviceData.LabGroupID = Convert.ToInt32(ddl_labgroup.SelectedValue == "0" ? null : ddl_labgroup.SelectedValue);
                objlabserviceData.LabSubGroupID = Convert.ToInt32(ddl_labsubgroup.SelectedValue == "0" ? null : ddl_labsubgroup.SelectedValue);
                objlabserviceData.TestName = txt_labtestname.Text == "" ? null : txt_labtestname.Text;
                objlabserviceData.TestCenterID = Convert.ToInt32(ddl_testcenter.SelectedValue == "0" ? null : ddl_testcenter.SelectedValue);
                objlabserviceData.TestAmount = Convert.ToDecimal(txt_testamount.Text == "" ? null : txt_testamount.Text);
                objlabserviceData.EmployeeID = LogData.EmployeeID;
                objlabserviceData.ReportTypeID = Convert.ToInt32(ddl_reportypes.SelectedValue == "0" ? null : ddl_reportypes.SelectedValue);
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
                int result = objlabserviceBO.UpdateLabServiceDetails(objlabserviceData);
                if (result == 1 || result == 2)
                {
                    Messagealert_.ShowMessage(lblmessage, result == 1 ? "save" : "update", 1);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "SucessAlert";
                    btnsave.Text = "Add";
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
                    LabServiceMasterData objlabserviceData = new LabServiceMasterData();
                    LabServiceMasterBO objlabserviceBO = new LabServiceMasterBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GvLabService.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("code");
                    objlabserviceData.ID = Convert.ToInt32(ID.Text);
                    objlabserviceData.ActionType = Enumaction.Select;

                    List<LabServiceMasterData> GetResult = objlabserviceBO.GetLabServiceDetailsByID(objlabserviceData);
                    if (GetResult.Count > 0)
                    {
                        ddl_labgroup.SelectedValue = GetResult[0].LabGroupID.ToString();
                        MasterLookupBO mstlookup = new MasterLookupBO();
                        Commonfunction.PopulateDdl(ddl_labsubgroup, mstlookup.GetSubGroupByGroupID(Convert.ToInt32(ddl_labgroup.SelectedValue == "" ? "0" : ddl_labgroup.SelectedValue)));
                        ddl_labsubgroup.SelectedValue = GetResult[0].LabSubGroupID.ToString();
                        ddl_reportypes.SelectedValue = GetResult[0].ReportTypeID.ToString();
                        txt_labtestname.Text = GetResult[0].TestName;
                        txt_testamount.Text = Commonfunction.Getrounding(GetResult[0].TestAmount.ToString());
                        ddl_testcenter.SelectedValue = GetResult[0].TestCenterID.ToString();
                        btnsave.Text = "Update";
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
                    LabServiceMasterData objlabserviceData = new LabServiceMasterData();
                    LabServiceMasterBO objlabserviceBO = new LabServiceMasterBO();
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
                    LabServiceMasterBO objlabserviceBO1 = new LabServiceMasterBO();
                    int Result = objlabserviceBO1.DeleteLabServiceDetailsByID(objlabserviceData);
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
        private void bindgrid(int page)
        {
            try
            {
                List<LabServiceMasterData> lstemp = GetLabServiceType(page);

                if (lstemp.Count > 0)
                {
                    GvLabService.VirtualItemCount = lstemp[0].MaximumRows;//total item is required for custom paging
                    GvLabService.PageIndex = page - 1;
                    GvLabService.DataSource = lstemp;
                    GvLabService.DataBind();
                    GvLabService.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total: " + lstemp[0].MaximumRows.ToString() + " Record(s) found.", 1);
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
        private List<LabServiceMasterData> GetLabServiceType(int p)
        {
            LabServiceMasterData objlabserviceData = new LabServiceMasterData();
            LabServiceMasterBO objlabserviceBO = new LabServiceMasterBO();
            objlabserviceData.LabGroupID = Convert.ToInt32(ddl_labgroup.SelectedValue == "" ? null : ddl_labgroup.SelectedValue);
            objlabserviceData.LabSubGroupID = Convert.ToInt32(ddl_labsubgroup.SelectedValue == "" ? null : ddl_labsubgroup.SelectedValue);
            objlabserviceData.ReportTypeID = Convert.ToInt32(ddl_reportypes.SelectedValue == "" ? null : ddl_reportypes.SelectedValue);
            objlabserviceData.TestName = txt_labtestname.Text == "" ? "" : txt_labtestname.Text;
            objlabserviceData.TestAmount = Convert.ToDecimal(txt_testamount.Text == "" ? "0" : txt_testamount.Text);
            objlabserviceData.CurrentIndex = p;
            objlabserviceData.TestID = 0;
            objlabserviceData.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
            return objlabserviceBO.SearchServiceDetails(objlabserviceData);
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
            ddl_reportypes.SelectedIndex = 0;
            Commonfunction.Insertzeroitemindex(ddl_labsubgroup);
            btnsave.Text = "Add";
        }
        private void clearall()
        {
            ddl_labgroup.SelectedIndex = 0;
            ddl_labsubgroup.SelectedIndex = 0;
            ddl_testcenter.SelectedIndex = 0;
            ddlstatus.SelectedIndex = 0;
            txt_labtestname.Text = "";
            txt_testamount.Text = "";
            GvLabService.DataSource = null;
            GvLabService.DataBind();
            GvLabService.Visible = false;
            ddlexport.Visible = false;
            btnexport.Visible = false;
        }
        protected void ddl_labgroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_labgroup.SelectedValue == "1")
            {
                id_reporttpye.Visible = true;
            }
            else
            {
                id_reporttpye.Visible = false;
            }
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddl_labsubgroup, mstlookup.GetSubGroupByGroupID(Convert.ToInt32(ddl_labgroup.SelectedValue == "" ? "0" : ddl_labgroup.SelectedValue)));
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
                Response.AddHeader("content-disposition", "attachment;filename=LabServiceDetails.xlsx");
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
                    GvLabService.AllowPaging = false;
                    GvLabService.DataSource = GetLabServiceTypeDetails(0);
                    GvLabService.DataBind();

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
                    Response.AddHeader("content-disposition", "attachment;filename=LabServiceDetail.pdf");
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
            List<LabServiceMasterData> LabServiceDetails = GetLabServiceTypeDetails(0);
            List<LabServicesDatatoExcel> ListexcelData = new List<LabServicesDatatoExcel>();
            int i = 0;
            foreach (LabServiceMasterData row in LabServiceDetails)
            {
                LabServicesDatatoExcel ExcelSevice = new LabServicesDatatoExcel();
                ExcelSevice.ID = LabServiceDetails[i].ID;
                ExcelSevice.ServiceGroup = LabServiceDetails[i].ServiceGroup;
                ExcelSevice.ServiceSubGroup = LabServiceDetails[i].ServiceSubGroup;
                ExcelSevice.ReportType = LabServiceDetails[i].ReportType;
                ExcelSevice.TestName = LabServiceDetails[i].TestName;
                ExcelSevice.TestAmount = Commonfunction.Getrounding(LabServiceDetails[i].TestAmount.ToString());
                ExcelSevice.AddedBy = LabServiceDetails[i].EmpName;
                ListexcelData.Add(ExcelSevice);
                i++;
            }
            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            DataTable dt = converter.ToDataTable(ListexcelData);
            return dt;
        }
        private List<LabServiceMasterData> GetLabServiceTypeDetails(int p)
        {
            LabServiceMasterData objlabserviceData = new LabServiceMasterData();
            LabServiceMasterBO objlabserviceBO = new LabServiceMasterBO();
            objlabserviceData.LabGroupID = Convert.ToInt32(ddl_labgroup.SelectedValue == "" ? null : ddl_labgroup.SelectedValue);
            objlabserviceData.LabSubGroupID = Convert.ToInt32(ddl_labsubgroup.SelectedValue == "" ? null : ddl_labsubgroup.SelectedValue);
            objlabserviceData.ReportTypeID = Convert.ToInt32(ddl_reportypes.SelectedValue == "" ? null : ddl_reportypes.SelectedValue);
            objlabserviceData.TestName = txt_labtestname.Text == "" ? "" : txt_labtestname.Text;
            objlabserviceData.TestAmount = Convert.ToDecimal(txt_testamount.Text == "" ? "0" : txt_testamount.Text);
            objlabserviceData.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
            return objlabserviceBO.SearchLabServiceDetails(objlabserviceData);
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
            bindgrid(Convert.ToInt32(e.NewPageIndex + 1));
        }
    }
}
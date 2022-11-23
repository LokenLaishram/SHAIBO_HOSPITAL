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
namespace Mediqura.Web.MedReport.Reports
{
    public partial class TestList : BasePage
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
            Commonfunction.PopulateDdl(ddl_labgroup, mstlookup.GetLookupsList(LookupName.InvestigationGroup));
            Commonfunction.Insertzeroitemindex(ddl_labsubgroup);
            Commonfunction.Insertzeroitemindex(ddl_labsubgroup);
            AutoCompleteExtender2.ContextKey = "0";
        }
        protected void ddl_labsubgroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_labsubgroup.SelectedIndex > 0)
            {
                AutoCompleteExtender2.ContextKey = ddl_labsubgroup.SelectedValue == "" ? "0" : ddl_labsubgroup.SelectedValue;
            }
            else
            {
                AutoCompleteExtender2.ContextKey = null;
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
            getResult = objInfoBO.GetTestNamesWithID(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].TestName.ToString());
            }
            return list;
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
            objlabserviceData.ReportTypeID = 0;
            objlabserviceData.TestName = "";
            bool isnumeric = txt_labtestname.Text.All(char.IsDigit);
            if (isnumeric == false)
            {
                if (txt_labtestname.Text.Contains(":"))
                {
                    bool isUHIDnumeric = txt_labtestname.Text.Substring(txt_labtestname.Text.LastIndexOf(':') + 1).All(char.IsDigit);
                    objlabserviceData.TestID = isUHIDnumeric ? Convert.ToInt32(txt_labtestname.Text.Contains(":") ? txt_labtestname.Text.Substring(txt_labtestname.Text.LastIndexOf(':') + 1) : "0") : 0;
                }
                else
                {
                    txt_labtestname.Text = "";
                    txt_labtestname.Focus();
                }
            }
            else
            {
                objlabserviceData.TestID = 0;
            }
            objlabserviceData.TestAmount = 0;
            objlabserviceData.CurrentIndex = p;
            objlabserviceData.IsActive = true;
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

            Commonfunction.Insertzeroitemindex(ddl_labsubgroup);
        }
        private void clearall()
        {
            ddl_labgroup.SelectedIndex = 0;
            ddl_labsubgroup.SelectedIndex = 0;
            txt_labtestname.Text = "";
            GvLabService.DataSource = null;
            GvLabService.DataBind();
            GvLabService.Visible = false;
            ddlexport.Visible = false;
            btnexport.Visible = false;
        }
        protected void ddl_labgroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_labgroup.SelectedIndex > 0)
            {
                MasterLookupBO mstlookup = new MasterLookupBO();
                Commonfunction.PopulateDdl(ddl_labsubgroup, mstlookup.GetSubGroupByGroupID(Convert.ToInt32(ddl_labgroup.SelectedValue == "" ? "0" : ddl_labgroup.SelectedValue)));
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
            objlabserviceData.ReportTypeID = 0;
            objlabserviceData.TestName = txt_labtestname.Text == "" ? "" : txt_labtestname.Text;
            objlabserviceData.TestAmount = 0;
            objlabserviceData.IsActive = true;
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

        protected void txt_labtestname_TextChanged(object sender, EventArgs e)
        {
            bindgrid(1);
        }
    }
}
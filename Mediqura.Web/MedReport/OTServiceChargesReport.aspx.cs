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

namespace Mediqura.Web.MedReport
{
    public partial class OTServiceChargesReport : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindddl();
            }
        }
        private void bindddl()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddlservicetype, mstlookup.GetLookupsList(LookupName.CommonGroups));
            Commonfunction.PopulateDdl(ddl_subservicetype, mstlookup.GetSubServiceTypeByGroupID(5));
            ddlservicetype.SelectedIndex = 3;
            ddlservicetype.Attributes["disabled"] = "disabled";

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
            if (ddlservicetype.SelectedIndex == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "ServiceType", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                ddlservicetype.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
            }

            bindgrid();

        }
        private void bindgrid()
        {
            try
            {


                List<ServicesData> lstemp = Getservices(0);

                if (lstemp.Count > 0)
                {
                    Gvservice.DataSource = lstemp;
                    Gvservice.DataBind();
                    Gvservice.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total: " + lstemp[0].MaximumRows.ToString() + " Record(s) found", 1);
                    divmsg3.Visible = true;
                    divmsg3.Attributes["class"] = "SucessAlert";
                    ddlexport.Visible = true;
                    btnexport.Visible = true;

                }
                else
                {
                    Gvservice.DataSource = null;
                    Gvservice.DataBind();
                    Gvservice.Visible = true;
                    lblresult.Visible = false;
                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
            }
        }
        public List<ServicesData> Getservices(int curIndex)
        {
            ServicesData objservice = new ServicesData();
            ServiceBO objServiceBO = new ServiceBO();
            // objservice.Code = txtcode.Text == "" ? "" : txtcode.Text;
            objservice.ServiceTypeID = Convert.ToInt32(ddlservicetype.SelectedValue == "" ? "0" : ddlservicetype.SelectedValue);
            objservice.SubServiceTypeID = Convert.ToInt32(ddl_subservicetype.SelectedValue == "" ? "0" : ddl_subservicetype.SelectedValue);
            var source = txt_procedures.Text.ToString();
            if (source.Contains(":"))
            {
                objservice.ID = Convert.ToInt32(source.Substring(source.LastIndexOf(':') + 1));
            }
            else
            {
                objservice.ID = 0;
                txt_procedures.Text = "";
            }
            return objServiceBO.SearchServiceDetailsReport(objservice);
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetProcedureName(string prefixText, int count, string contextKey)
        {
            ServicesData Objpaic = new ServicesData();
            ServiceBO objInfoBO = new ServiceBO();
            List<ServicesData> getResult = new List<ServicesData>();
            Objpaic.ServiceName = prefixText;
            Objpaic.SubServiceTypeID = Convert.ToInt32(contextKey);
            getResult = objInfoBO.GetAutoProcedureName(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].ServiceName.ToString());
            }
            return list;
        }
        private void clearall()
        {
            //ddlservicetype.SelectedIndex = 0;
            Commonfunction.Insertzeroitemindex(ddl_subservicetype);
            Gvservice.DataSource = null;
            Gvservice.DataBind();
            Gvservice.Visible = false;
            ddlexport.Visible = false;
            btnexport.Visible = false;

        }
        protected void btnresets_Click(object sender, EventArgs e)
        {
            ViewState["ID"] = null;
            clearall();
            lblmessage.Visible = false;
            lblresult.Visible = false;
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddlservicetype, mstlookup.GetLookupsList(LookupName.CommonGroups));
            Commonfunction.PopulateDdl(ddl_subservicetype, mstlookup.GetSubServiceTypeByGroupID(5));
            ddlservicetype.SelectedIndex = 3;
            ddlservicetype.Attributes["disabled"] = "disabled";
            txt_procedures.Text = "";
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
                Response.AddHeader("content-disposition", "attachment;filename=OTProcedureDetails.xlsx");
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
                    Gvservice.BorderStyle = BorderStyle.None;
                    //Hide the Column containing CheckBox
                    Gvservice.Columns[6].Visible = false;
                    Gvservice.Columns[7].Visible = false;
                    Gvservice.Columns[8].Visible = false;
                    Gvservice.Columns[9].Visible = false;
                    Gvservice.RenderControl(hw);
                    Gvservice.HeaderRow.Style.Add("width", "15%");
                    Gvservice.HeaderRow.Style.Add("font-size", "10px");
                    Gvservice.Style.Add("text-decoration", "none");
                    Gvservice.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    Gvservice.Style.Add("font-size", "8px");
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 7f, 7f, 7f, 0f);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=OTProcedureDetail.pdf");
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
            List<ServicesData> ServiceDetails = Getservices(0);
            List<ServicesDatatoExcel> ListexcelData = new List<ServicesDatatoExcel>();
            int i = 0;
            foreach (ServicesData row in ServiceDetails)
            {
                ServicesDatatoExcel ExcelSevice = new ServicesDatatoExcel();
                ExcelSevice.ID = ServiceDetails[i].ID;
                ExcelSevice.Code = ServiceDetails[i].Code;
                ExcelSevice.ServiceName = ServiceDetails[i].ServiceName;
                ExcelSevice.ServiceCharge = Commonfunction.Getrounding(ServiceDetails[i].ServiceCharge.ToString());
                ExcelSevice.ServiceType = ServiceDetails[i].ServiceType;
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
        protected void ddl_subservicetype_SelectedIndexChanged(object sender, EventArgs e)
        {
            AutoCompleteExtender3.ContextKey = ddl_subservicetype.SelectedValue;
            txt_procedures.Text = "";
        }
        protected void txt_procedures_TextChanged(object sender, EventArgs e)
        {
            bindgrid();
        }
    }
}
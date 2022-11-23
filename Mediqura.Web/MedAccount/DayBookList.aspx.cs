using ClosedXML.Excel;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedAccount;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.LoginData;
using Mediqura.CommonData.MedAccount;
using Mediqura.Utility;
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

namespace Mediqura.Web.MedAccount
{
    public partial class DayBookList : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindddl();
                txtdatefrom.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                txtto.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            }
        }
        private void bindddl()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddlbranches, mstlookup.GetLookupsList(LookupName.Branch));
            Commonfunction.PopulateDdl(ddl_ledgers, mstlookup.GetLookupsList(LookupName.AccountLedger));
            ddlbranches.SelectedIndex = 1;
            ddl_ledgers.SelectedIndex = 1;
        }
        protected void btnsearch1_Click(object sender, EventArgs e)
        {
            if (LogData.SearchEnable == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "SearchEnable", 0);
                divmsg3.Visible = true;
                divmsg3.Attributes["class"] = "FailAlert";
                return;
            }
            else
            {
                lblmessage.Visible = false;
            }

            bindgrid();
        }
        protected void bindgrid()
        {
            try
            {
                if (txtdatefrom.Text != "")
                {
                    if (Commonfunction.isValidDate(txtdatefrom.Text) == false)
                    {
                        Messagealert_.ShowMessage(lblmessage, "VaildDatefrom", 0);
                        divmsg3.Attributes["class"] = "FailAlert";
                        divmsg3.Visible = true;
                        txtdatefrom.Focus();
                        return;
                    }
                }
                else
                {
                    divmsg3.Visible = false;
                }
                if (txtto.Text != "")
                {
                    if (Commonfunction.isValidDate(txtto.Text) == false)
                    {
                        Messagealert_.ShowMessage(lblmessage, "VaildDateto", 0);
                        divmsg3.Attributes["class"] = "FailAlert";
                        divmsg3.Visible = true;
                        txtto.Focus();
                        return;
                    }
                }
                else
                {
                    divmsg3.Visible = false;
                }
                List<LedgerBookData> objdeposit = GetLedgerList(0);
                if (objdeposit.Count > 0)
                {
                    GVLedgers.DataSource = objdeposit;
                    GVLedgers.DataBind();
                    GVLedgers.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total:" + objdeposit[0].MaximumRows.ToString() + " Record(s) found.", 1);
                    divmsg3.Attributes["class"] = "SucessAlert";
                    //btnprints.Attributes.Remove("disabled");
                    divmsg3.Visible = true;
                    ddlexport.Visible = true;
                    btnexport.Visible = true;
                }
                else
                {
                    GVLedgers.DataSource = null;
                    GVLedgers.DataBind();
                    GVLedgers.Visible = true;
                    //ddlexport.Visible = false;
                    //btnexport.Visible = false;
                    ////txttotaladmissioncharge.Text = "0.00";
                    lblresult.Visible = false;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
            }
        }
        public List<LedgerBookData> GetLedgerList(int curIndex)
        {
            LedgerBookData objpat = new LedgerBookData();
            LedgerBookBO objbillingBO = new LedgerBookBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime from = txtdatefrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txtdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txtto.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objpat.HospitalID = Convert.ToInt32(ddlbranches.SelectedValue == "" ? "0" : ddlbranches.SelectedValue);
            objpat.LedgerID = Convert.ToInt32(ddl_ledgers.SelectedValue == "" ? "0" : ddl_ledgers.SelectedValue);
            objpat.DateFrom = from;
            objpat.DateTo = To;
            return objbillingBO.GetLedgerList(objpat);
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
            ddlbranches.SelectedIndex = 1;
            ddl_ledgers.SelectedIndex = 1;
            txtdatefrom.Text = "";
            txtto.Text = "";
            GVLedgers.DataSource = null;
            GVLedgers.DataBind();
            GVLedgers.Visible = false;
            ddlexport.Visible = false;
            btnexport.Visible = false;
            txtdatefrom.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            txtto.Text = System.DateTime.Now.ToString("dd/MM/yyyy");

        }
        protected void btnexport_Click(object sender, EventArgs e)
        {
            if (LogData.ExportEnable == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "ExportEnable", 0);
                divmsg3.Visible = true;
                divmsg3.Attributes["class"] = "FailAlert";
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
                Response.AddHeader("content-disposition", "attachment;filename=LedgerBookDetails.xlsx");
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
            List<LedgerBookData> DepartmentTypeDetails = GetLedgerList(0);
            List<LedgerBookMasterDataExcel> ListexcelData = new List<LedgerBookMasterDataExcel>();
            int i = 0;
            foreach (LedgerBookData row in DepartmentTypeDetails)
            {
                LedgerBookMasterDataExcel ExcelSevice = new LedgerBookMasterDataExcel();
                ExcelSevice.GroupName = DepartmentTypeDetails[i].GroupName;
                ExcelSevice.Ledgername = DepartmentTypeDetails[i].Ledgername;
                ExcelSevice.Amount = DepartmentTypeDetails[i].Amount;
             
                ListexcelData.Add(ExcelSevice);
                i++;
            }
            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            DataTable dt = converter.ToDataTable(ListexcelData);
            return dt;
        }
        private void ExportToPdf()
        {
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    GVLedgers.BorderStyle = BorderStyle.None;
                    //Hide the Column containing CheckBox

                    GVLedgers.RenderControl(hw);
                    GVLedgers.HeaderRow.Style.Add("width", "15%");
                    GVLedgers.HeaderRow.Style.Add("font-size", "10px");
                    GVLedgers.Style.Add("text-decoration", "none");
                    GVLedgers.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    GVLedgers.Style.Add("font-size", "8px");
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 7f, 7f, 7f, 0f);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=LedgerBookDetails.pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.Write(pdfDoc);
                    Response.End();
                }
            }
        }

    }
}
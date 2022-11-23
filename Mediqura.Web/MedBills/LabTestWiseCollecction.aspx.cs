using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedBill;
using Mediqura.BOL.PatientBO;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.MedBillData;
using Mediqura.CommonData.PatientData;
using Mediqura.Utility;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using Mediqura.Web.MedCommon;
using OnBarcode.Barcode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using ClosedXML.Excel;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Data;
using System.Reflection;
using Mediqura.CommonData.MedUtilityData;
using Mediqura.BOL.MedUtilityBO;
using Mediqura.BOL.MedBillBO;

namespace Mediqura.Web.MedBills
{
    public partial class LabTestWiseCollecction : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlbind();
            }
        }
        private void ddlbind()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();

            //Commonfunction.PopulateDdl(ddldoctortype, mstlookup.GetLookupsList(LookupName.OPDoctorType));
            Commonfunction.PopulateDdl(ddldoctortype, mstlookup.GetLookupsList(LookupName.DoctorType));
            Commonfunction.Insertzeroitemindex(ddldoctor);
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetServiceName(string prefixText, int count, string contextKey)
        {
            TestWiseCollectionData Objpaic = new TestWiseCollectionData();
            TestWiseBO objInfoBO = new TestWiseBO();
            List<TestWiseCollectionData> getResult = new List<TestWiseCollectionData>();
            Objpaic.ServiceName = prefixText;
            getResult = objInfoBO.GetServiceName(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].ServiceName.ToString());
            }
            return list;
        }
        protected void bindgrid()
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
            decimal total = 0;
            try
            {
                if (txt_servName.Text == "" && txtdatefrom.Text == "" && txtto.Text == "")
                {
                    if (ddl_servicetype.SelectedIndex <= 0)
                    {
                        Messagealert_.ShowMessage(lblmessage, "ServiceType", 0);
                        div1.Visible = true;
                        div1.Attributes["class"] = "FailAlert";
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                    }
                    if (ddldoctortype.SelectedIndex <= 0)
                    {
                        Messagealert_.ShowMessage(lblmessage, "DoctorType", 0);
                        div1.Visible = true;
                        div1.Attributes["class"] = "FailAlert";
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                    }
                    if (ddldoctor.SelectedIndex <= 0)
                    {
                        Messagealert_.ShowMessage(lblmessage, "Doctor", 0);
                        div1.Visible = true;
                        div1.Attributes["class"] = "FailAlert";
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                    }
                }
                else
                {
                    if (txtdatefrom.Text != "")
                    {
                        if (Commonfunction.isValidDate(txtdatefrom.Text) == false)
                        {
                            Messagealert_.ShowMessage(lblmessage, "VaidDate", 0);
                            div1.Attributes["class"] = "FailAlert";
                            div1.Visible = true;
                            txtdatefrom.Focus();
                            return;
                        }
                    }
                    else
                    {
                        div1.Visible = false;
                    }
                    if (txtto.Text != "")
                    {
                        if (Commonfunction.isValidDate(txtto.Text) == false)
                        {
                            Messagealert_.ShowMessage(lblmessage, "VaidDate", 0);
                            div1.Attributes["class"] = "FailAlert";
                            div1.Visible = true;
                            txtto.Focus();
                            return;
                        }
                    }
                    else
                    {
                        div1.Visible = false;
                    }
                }
                List<TestWiseCollectionData> objdeposit = GetServicesList(0);
                if (objdeposit.Count > 0)
                {
                    if (ViewState["Total"] == null)
                    {
                        ////Decimal total = 0;
                        for (int i = 0; i <= objdeposit.Count - 1; i++)
                        {
                            total = total + Convert.ToDecimal(objdeposit[i].TotalAmt.ToString());
                        }
                        ViewState["Total"] = total;
                        txt_total.Text = Convert.ToDecimal(total).ToString();
                    }
                    GvTestWiseCollection.DataSource = objdeposit;
                    GvTestWiseCollection.DataBind();
                    GvTestWiseCollection.Visible = true;
                    ddlexport.Visible = true;
                    btnexport.Visible = true;
                    lblmessage.Visible = false;
                    divmsg3.Visible = false;
                    txt_total.Visible = true;
                    divTestTotal.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total: " + objdeposit[0].MaximumRows.ToString() + " Record found", 1);
                    ddl_servicetype.Attributes["disabled"] = "disabled";
                    ddldoctortype.Attributes["disabled"] = "disabled";
                    ddldoctor.Attributes["disabled"] = "disabled";

                }
                else
                {


                    GvTestWiseCollection.DataSource = null;
                    GvTestWiseCollection.DataBind();
                    GvTestWiseCollection.Visible = true;
                    ddl_servicetype.Attributes.Remove("disabled");
                    ddldoctortype.Attributes.Remove("disabled");
                    ddldoctor.Attributes.Remove("disabled");
                    divmsg3.Visible = false;
                    lblresult.Visible = false;


                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
            }

        }
        public List<TestWiseCollectionData> GetServicesList(int curIndex)
        {
            TestWiseCollectionData objpat = new TestWiseCollectionData();
            TestWiseBO objbillingBO = new TestWiseBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime from = txtdatefrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txtdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txtto.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objpat.ServiceName = txt_servName.Text == "" ? null : txt_servName.Text;
            objpat.DateFrom = from;
            objpat.DateTo = To;
            objpat.docID = Convert.ToInt32(ddldoctor.SelectedValue == "" ? "0" : ddldoctor.SelectedValue);
            return objbillingBO.GetServicesList(objpat);
        }

        protected void GvTestWiseCollection_nested_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void txt_servName_TextChanged(object sender, EventArgs e)
        {

        }

        protected void btnreset_Click(object sender, EventArgs e)
        {
            txt_servName.Text = "";
            txtdatefrom.Text = "";
            txtto.Text = "";
            //txt_total.Text = "";
            GvTestWiseCollection.DataSource = null;
            GvTestWiseCollection.DataBind();
            GvTestWiseCollection.Visible = false;
            lblmessage.Visible = false;
            lblresult.Visible = false;
            ddlexport.Visible = false;
            btnexport.Visible = false;
            divTestTotal.Visible = false;
            ViewState["Total"] = null;
            ddl_servicetype.SelectedIndex = 0;
            ddldoctortype.SelectedIndex = 0;
            ddldoctor.SelectedIndex = 0;
            ddl_servicetype.Attributes.Remove("disabled");
            ddldoctortype.Attributes.Remove("disabled");
            ddldoctor.Attributes.Remove("disabled");
        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            bindgrid();
        }

        protected void GvTestWiseCollection_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TestWiseCollectionData objtest = new TestWiseCollectionData();
                TestWiseBO objtestdBO = new TestWiseBO();
                string Srv_id = GvTestWiseCollection.DataKeys[e.Row.RowIndex].Value.ToString();
                GridView GvNested = e.Row.FindControl("GvTestWiseCollection_nested") as GridView;

                //objtest.ServiceID = Convert.ToInt32(Srv_id == "" ? "0" : Srv_id);
                objtest.ServiceName = Srv_id == "" ? null : Srv_id;
                List<TestWiseCollectionData> listdetails = objtestdBO.GetNestedData(objtest);
                GvNested.DataSource = listdetails;
                GvNested.DataBind();
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                if (ViewState["Total"] != null)
                {
                    Label lblGrandTotal = (Label)e.Row.FindControl("lblTotal");
                    lblGrandTotal.Text = ViewState["Total"].ToString();
                }
            }
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
                divmsg3.Attributes["class"] = "FailAlert";
                ddlexport.Focus();
                return;
            }
        }
        public void ExportToPdf()
        {
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    GvTestWiseCollection.BorderStyle = BorderStyle.None;
                    //Hide the Column containing CheckBox
                    //gvOT_status.Columns[6].Visible = false;
                    //gvOT_status.Columns[8].Visible = false;
                    //gvstockstatus.Columns[10].Visible = false;
                    //gvstockstatus.Columns[11].Visible = false;

                    GvTestWiseCollection.RenderControl(hw);
                    GvTestWiseCollection.HeaderRow.Style.Add("width", "15%");
                    GvTestWiseCollection.HeaderRow.Style.Add("font-size", "10px");
                    GvTestWiseCollection.Style.Add("text-decoration", "none");
                    GvTestWiseCollection.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    GvTestWiseCollection.Style.Add("font-size", "8px");
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 7f, 7f, 7f, 0f);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=TestWiseDetails.pdf");
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
        protected void ExportoExcel()
        {
            DataTable dt = GetDatafromDatabase();
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "Deposit Details");
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=TestWiseDetails.xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                    ddlexport.SelectedIndex = 0;
                }
                Messagealert_.ShowMessage(lblresult, "Exported", 1);
                divmsg3.Attributes["class"] = "SucessAlert";
            }
        }
        protected DataTable GetDatafromDatabase()
        {
            List<TestWiseCollectionData> OTStatus = GetServicesList(0);
            List<ServiceListDataTOeXCEL> ListexcelData = new List<ServiceListDataTOeXCEL>();
            int i = 0;
            foreach (TestWiseCollectionData row in OTStatus)
            {
                ServiceListDataTOeXCEL Ecxeclpat = new ServiceListDataTOeXCEL();
                Ecxeclpat.Services = OTStatus[i].Services;
                Ecxeclpat.QTY = OTStatus[i].QTY;
                Ecxeclpat.Netamt = OTStatus[i].Netamt;
                Ecxeclpat.TotalAmt = OTStatus[i].TotalAmt;
                //for (int j = 0; j < GvTestWiseCollection.Rows.Count; j++)
                //{
                //    DropDownList Status = (DropDownList)GvTestWiseCollection.Rows[j].Cells[0].FindControl("ddlOTstatus");
                //    TestWiseCollectionData objpat = new TestWiseCollectionData();
                //    objpat.Status = Status.Text;
                //    Ecxeclpat.Status = OTStatus[i].Status;
                //}
                ListexcelData.Add(Ecxeclpat);
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

                // Get all the properties

                PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

                foreach (PropertyInfo prop in Props)
                {

                    //  Setting column names as Property names
                    dataTable.Columns.Add(prop.Name);

                }

                foreach (T item in items)
                {

                    var values = new object[Props.Length];

                    for (int i = 0; i < Props.Length; i++)
                    {

                        //       inserting property values to datatable rows

                        values[i] = Props[i].GetValue(item, null);

                    }

                    dataTable.Rows.Add(values);

                }

                //     put a breakpoint here and check datatable

                return dataTable;

            }
        }

        protected void GvTestWiseCollection_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GvTestWiseCollection.PageIndex = e.NewPageIndex;
            bindgrid();
        }

        protected void ddl_servicetype_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddldoctortype_SelectedIndexChanged(object sender, EventArgs e)
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            if (ddldoctortype.SelectedIndex != 0)
            {

                Commonfunction.PopulateDdl(ddldoctor, mstlookup.GetReferalemployees(Convert.ToInt32(ddldoctortype.SelectedValue == "" ? "0" : ddldoctortype.SelectedValue)));
                ddldoctor.Attributes.Remove("disabled");
            }

        }

    }
}
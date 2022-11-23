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
using Mediqura.CommonData.AdmissionData;
using Mediqura.BOL.AdmissionBO;
using Mediqura.CommonData.PatientData;
using Mediqura.BOL.PatientBO;
using Mediqura.Utility;

namespace Mediqura.Web.MedAdmission
{
    public partial class BedOccupancy : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindddl();
                btnprint.Attributes["disabled"] = "disabled";
             }
        }
        private void bindddl()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddl_block, mstlookup.GetLookupsList(LookupName.BlockType));
            Commonfunction.Insertzeroitemindex(ddl_floor);
            Commonfunction.Insertzeroitemindex(ddl_ward);

            //Commonfunction.PopulateDdl(ddl_floor, mstlookup.GetLookupsList(LookupName.FloorType));
            //Commonfunction.PopulateDdl(ddl_ward, mstlookup.GetLookupsList(LookupName.WardType));
        }
        protected void ddl_block_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_block.SelectedIndex > 0)
            {
                MasterLookupBO mstlookup = new MasterLookupBO();
                Commonfunction.PopulateDdl(ddl_floor, mstlookup.GetfloorByblockID(Convert.ToInt32(ddl_block.SelectedValue)));
            }
        }
        protected void ddl_floor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_floor.SelectedIndex > 0)
            {
                MasterLookupBO mstlookup = new MasterLookupBO();
                Commonfunction.PopulateDdl(ddl_ward, mstlookup.GetWardByFloorID(Convert.ToInt32(ddl_floor.SelectedValue)));
            }
        }
        protected void ddl_ward_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblresult.Visible = false;
            if (ddl_ward.SelectedIndex > 0)
            {
                bindgrid();
            }
            else
            {
                GvBedOccupancy.DataSource = null;
                GvBedOccupancy.DataBind();
                GvBedOccupancy.Visible = true;
            }

        }
        protected void GvBedOccupancy_RowDataBound(object sender, GridViewRowEventArgs e)
        {
        }
        protected void bindgrid()
        {
            List<AdmissionData> objdeposit = GetBedOccupancy(0);
            for (int i = 0; i < objdeposit.Count; i++)
            {
                if (objdeposit[i].OccupiedBy == null)
                {
                    objdeposit[i].OccupiedBy = "Available";
                }
            }
            if (objdeposit.Count > 0)
            {
                GvBedOccupancy.DataSource = objdeposit;
                GvBedOccupancy.DataBind();
                GvBedOccupancy.Visible = true;
                Messagealert_.ShowMessage(lblresult, "Total:" + objdeposit[0].MaximumRows.ToString() + " Record found", 1);
                divmsg3.Attributes["class"] = "SucessAlert";
                divmsg3.Visible = true;
                btnprint.Visible = true;
                btnprint.Attributes.Remove("disabled");
                if (LogData.PrintEnable == 0)
                {
                    btnprint.Attributes["disabled"] = "disabled";
                }
                else
                {
                    btnprint.Attributes.Remove("disabled");
                }
                ddlexport.Visible = true;
                btnexport.Visible = true;
            }
            else
            {
                GvBedOccupancy.DataSource = null;
                GvBedOccupancy.DataBind();
                GvBedOccupancy.Visible = true;
                ddlexport.Visible = false;
                btnexport.Visible = false;
            }
        }
        private List<AdmissionData> GetBedOccupancy(int p)
        {
            AdmissionData objpat = new AdmissionData();
            AdmissionBO objbillingBO = new AdmissionBO();
            objpat.BlockID = Convert.ToInt16(ddl_block.SelectedValue == "0" ? null : ddl_block.SelectedValue);
            objpat.FloorID = Convert.ToInt16(ddl_floor.SelectedValue == "0" ? null : ddl_floor.SelectedValue);
            objpat.WardID = Convert.ToInt16(ddl_ward.SelectedValue == "0" ? null : ddl_ward.SelectedValue);
            return objbillingBO.GetBedOccupancy(objpat);
        }
        protected void GvBedOccupancy_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GvBedOccupancy.PageIndex = e.NewPageIndex;
            bindgrid();
        }
        protected void btnreset_Click(object sender, EventArgs e)
        {

            GvBedOccupancy.DataSource = null;
            GvBedOccupancy.DataBind();
            GvBedOccupancy.Visible = false;
            lblmessage.Visible = false;
            lblresult.Visible = false;
            divmsg3.Visible = false;
            divmsg1.Visible = false;
            btnprint.Visible = false;
            btnexport.Visible = false;
            ddlexport.Visible = false;
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddl_block, mstlookup.GetLookupsList(LookupName.BlockType));
            Commonfunction.Insertzeroitemindex(ddl_floor);
            Commonfunction.Insertzeroitemindex(ddl_ward);

        }
    
        protected DataTable GetDatafromDatabase()
        {
            List<AdmissionData> AdmissionDetails = GetBedOccupancy(0);
            List<BedOccupancyListDataTOeXCEL> ListexcelData = new List<BedOccupancyListDataTOeXCEL>();
            int i = 0;
            foreach (AdmissionData row in AdmissionDetails)
            {
                BedOccupancyListDataTOeXCEL Ecxeclpat = new BedOccupancyListDataTOeXCEL();
                Ecxeclpat.Block = AdmissionDetails[i].Block;
                Ecxeclpat.Floor1 = AdmissionDetails[i].Floor1;
                Ecxeclpat.Ward = AdmissionDetails[i].Ward;
                Ecxeclpat.Room = AdmissionDetails[i].Room;
                Ecxeclpat.BedNo = AdmissionDetails[i].BedNo;
                Ecxeclpat.Charges = AdmissionDetails[i].Charges;
                if (AdmissionDetails[i].OccupiedBy == null)
                {
                    AdmissionDetails[i].OccupiedBy = "Available";
                    Ecxeclpat.OccupiedBy = AdmissionDetails[i].OccupiedBy;
                }
                else
                {
                    Ecxeclpat.OccupiedBy = AdmissionDetails[i].OccupiedBy;
                }
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
                    GvBedOccupancy.BorderStyle = BorderStyle.None;
                    GvBedOccupancy.RenderControl(hw);
                    GvBedOccupancy.HeaderRow.Style.Add("width", "15%");
                    GvBedOccupancy.HeaderRow.Style.Add("font-size", "10px");
                    GvBedOccupancy.Style.Add("text-decoration", "none");
                    GvBedOccupancy.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    GvBedOccupancy.Style.Add("font-size", "8px");
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 7f, 7f, 7f, 0f);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=BedOccupancyList.pdf");
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
                Response.AddHeader("content-disposition", "attachment;filename=BedOccupancyList.xlsx");
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

    }
}
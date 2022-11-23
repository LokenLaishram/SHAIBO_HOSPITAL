using System;
using System.Collections.Generic;
using System.Linq;
using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedBill;
using Mediqura.BOL.PatientBO;
using Mediqura.CommonData.Common;
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
using Mediqura.CommonData.MedHrData;
using Mediqura.BOL.MedHrBO;
using Mediqura.CommonData.AdmissionData;
using Mediqura.BOL.AdmissionBO;
using Mediqura.BOL.MedNurseBO;
using Mediqura.CommonData.MedNurseData;


namespace Mediqura.Web.MedNurse
{
    public partial class NurseStationAddWard : BasePage
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
            Commonfunction.PopulateDdl(ddl_station, mstlookup.GetLookupsList(LookupName.StationType));
            Commonfunction.PopulateDdl(ddl_ward, mstlookup.GetLookupsList(LookupName.WardType));

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
                if (ddl_station.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "StockType", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";

                    ddl_station.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }

                if (ddl_ward.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "Please select rack.", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";

                    ddl_ward.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
               
                WardToStationData objitemsubgroupData = new WardToStationData();
                NurseStationBO objitemsubgroupBO = new NurseStationBO();
                objitemsubgroupData.StationID = Convert.ToInt32(ddl_station.SelectedValue == "0" ? null : ddl_station.SelectedValue);
                objitemsubgroupData.WardID = Convert.ToInt32(ddl_ward.SelectedValue == "0" ? null : ddl_ward.SelectedValue);
                objitemsubgroupData.EmployeeID = LogData.EmployeeID;
                objitemsubgroupData.HospitalID = LogData.HospitalID;
                objitemsubgroupData.IPaddress = LogData.IPaddress;
                objitemsubgroupData.FinancialYearID = LogData.FinancialYearID;
                objitemsubgroupData.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
                objitemsubgroupData.ActionType = Enumaction.Insert;
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
                        objitemsubgroupData.ActionType = Enumaction.Update;
                        objitemsubgroupData.ID = Convert.ToInt32(ViewState["ID"].ToString());
                    }
                }
                int result = objitemsubgroupBO.UpdateWardToNurseDetails(objitemsubgroupData);
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
                    Messagealert_.ShowMessage(lblmessage, "duplicate", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    Messagealert_.ShowMessage(lblmessage, "system", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    return;
                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
            }
        }
        protected void GvWardToStn_RowCommand(object sender, GridViewCommandEventArgs e)
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
                    WardToStationData objData = new WardToStationData();
                    NurseStationBO objBO = new NurseStationBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GvWardToStn.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("lblid");
                    objData.ID = Convert.ToInt32(ID.Text);
                    objData.ActionType = Enumaction.Select;

                    List<WardToStationData> GetResult = objBO.GetWardToNurseDetailsByID(objData);
                    if (GetResult.Count > 0)
                    {
                        ddl_station.SelectedValue = GetResult[0].StationID.ToString();
                        ddl_ward.SelectedValue = GetResult[0].WardID.ToString();
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
                    WardToStationData objItemSubGroupTypeMasterData = new WardToStationData();
                    NurseStationBO objItemTypeMasterBO = new NurseStationBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GvWardToStn.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("lblsubrackid");
                    objItemSubGroupTypeMasterData.ID = Convert.ToInt32(ID.Text);
                    objItemSubGroupTypeMasterData.EmployeeID = LogData.EmployeeID;
                    objItemSubGroupTypeMasterData.ActionType = Enumaction.Delete;
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
                        objItemSubGroupTypeMasterData.Remarks = txtremarks.Text;
                    }

                    NurseStationBO objItemSubGroupTypeMasterBO1 = new NurseStationBO();
                    int Result = objItemSubGroupTypeMasterBO1.DeleteWardToNurseDetailsByID(objItemSubGroupTypeMasterData);
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
                        return;

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
                List<WardToStationData> lstemp = GetSubRackType(0);
                if (lstemp.Count > 0)
                {
                    GvWardToStn.DataSource = lstemp;
                    GvWardToStn.DataBind();
                    GvWardToStn.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total: " + lstemp[0].MaximumRows.ToString() + " Record(s) found", 1);
                    divmsg3.Visible = true;
                    divmsg3.Attributes["class"] = "SucessAlert";
                    ddlexport.Visible = true;
                    btnexport.Visible = true;
                }
                else
                {
                    GvWardToStn.DataSource = null;
                    GvWardToStn.DataBind();
                    GvWardToStn.Visible = true;
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
        private List<WardToStationData> GetSubRackType(int p)
        {
            WardToStationData objData = new WardToStationData();
            NurseStationBO objBO = new NurseStationBO();
            objData.StationID = Convert.ToInt32(ddl_station.SelectedValue == "" ? null : ddl_station.SelectedValue);
            objData.WardID = Convert.ToInt32(ddl_ward.SelectedValue == "" ? null : ddl_ward.SelectedValue);
            objData.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
            return objBO.SearchWardToNurseDetails(objData);
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
        protected void GvWardToStn_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GvWardToStn.PageIndex = e.NewPageIndex;
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
            ddl_ward.SelectedIndex = 0;
            ddl_station.SelectedIndex = 0;
            ddlstatus.SelectedIndex = 0;
            GvWardToStn.DataSource = null;
            GvWardToStn.DataBind();
            GvWardToStn.Visible = false;
            ddlexport.Visible = false;
            btnexport.Visible = false;
        }
        private void ExportToPdf()
        {
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    GvWardToStn.BorderStyle = BorderStyle.None;
                    //Hide the Column containing CheckBox
                    GvWardToStn.Columns[6].Visible = false;
                    GvWardToStn.Columns[7].Visible = false;
                    GvWardToStn.Columns[5].Visible = false;

                    GvWardToStn.RenderControl(hw);
                    GvWardToStn.HeaderRow.Style.Add("width", "15%");
                    GvWardToStn.HeaderRow.Style.Add("font-size", "10px");
                    GvWardToStn.Style.Add("text-decoration", "none");
                    GvWardToStn.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    GvWardToStn.Style.Add("font-size", "8px");
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 7f, 7f, 7f, 0f);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=WardToStationDetails.pdf");
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
                wb.Worksheets.Add(dt, "Item Type Detail List");

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=WardToStationDetails.xlsx");
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
            List<WardToStationData> TypeDetails = GetSubRackType(0);
            List<WardToStationDatatoExcel> ListexcelData = new List<WardToStationDatatoExcel>();
            int i = 0;
            foreach (WardToStationData row in TypeDetails)
            {
                WardToStationDatatoExcel ExcelSevice = new WardToStationDatatoExcel();
                ExcelSevice.ID = TypeDetails[i].ID;
                ExcelSevice.Station = TypeDetails[i].Station;
                ExcelSevice.Ward = TypeDetails[i].Ward;
                ExcelSevice.AddedBy = TypeDetails[i].EmpName;
                GvWardToStn.Columns[6].Visible = false;
                GvWardToStn.Columns[7].Visible = false;
                GvWardToStn.Columns[5].Visible = false;
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
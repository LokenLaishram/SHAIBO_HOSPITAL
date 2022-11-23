using ClosedXML.Excel;
using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedStore;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.MedStore;
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

namespace Mediqura.Web.MedStore
{
    public partial class SubRackMST : BasePage
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
            Commonfunction.PopulateDdl(ddl_store, mstlookup.GetLookupsList(LookupName.StockType));
            Commonfunction.PopulateDdl(ddl_rack, mstlookup.GetLookupsList(LookupName.Rack));
 
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
                if (ddl_store.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "StockType", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";

                    ddl_rack.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
               
                if (ddl_rack.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "Please select rack.", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";

                    ddl_rack.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txtsubrack.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Please enter code.", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    txtsubrack.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                RackMasterData objitemsubgroupData = new RackMasterData();
                RackMasterBO objitemsubgroupBO = new RackMasterBO();
                objitemsubgroupData.StockTypeID = Convert.ToInt32(ddl_store.SelectedValue == "0" ? null : ddl_store.SelectedValue);
                objitemsubgroupData.RackID = Convert.ToInt32(ddl_rack.SelectedValue == "0" ? null : ddl_rack.SelectedValue);
                objitemsubgroupData.SubRack =txtsubrack.Text == " " ? null : txtsubrack.Text;
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
                        objitemsubgroupData.SubRackID = Convert.ToInt32(ViewState["ID"].ToString());
                    }
                }
                int result = objitemsubgroupBO.UpdateItemSubRackDetails(objitemsubgroupData);
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
        protected void GvSubRackType_RowCommand(object sender, GridViewCommandEventArgs e)
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
                    RackMasterData objData = new RackMasterData();
                    RackMasterBO objBO = new RackMasterBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GvSubRackType.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("lblsubrackid");
                    objData.SubRackID = Convert.ToInt32(ID.Text);
                    objData.ActionType = Enumaction.Select;

                    List<RackMasterData> GetResult = objBO.GetItemSubRackTypeDetailsByID(objData);
                    if (GetResult.Count > 0)
                    {
                        ddl_store.SelectedValue = GetResult[0].StockTypeID.ToString();
                        ddl_rack.SelectedValue = GetResult[0].RackID.ToString();
                        txtsubrack.Text = GetResult[0].SubRack.ToString();
                        ViewState["ID"] = GetResult[0].SubRackID;
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
                    RackMasterData objItemSubGroupTypeMasterData = new RackMasterData();
                    RackMasterBO objItemTypeMasterBO = new RackMasterBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GvSubRackType.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("lblsubrackid");
                    objItemSubGroupTypeMasterData.SubRackID = Convert.ToInt32(ID.Text);
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

                    RackMasterBO objItemSubGroupTypeMasterBO1 = new RackMasterBO();
                    int Result = objItemSubGroupTypeMasterBO1.DeleteSubRackTypeDetailsByID(objItemSubGroupTypeMasterData);
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
                List<RackMasterData> lstemp = GetSubRackType(0);
                if (lstemp.Count > 0)
                {
                    GvSubRackType.DataSource = lstemp;
                    GvSubRackType.DataBind();
                    GvSubRackType.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total: " + lstemp[0].MaximumRows.ToString() + " Record(s) found", 1);
                    divmsg3.Visible = true;
                    divmsg3.Attributes["class"] = "SucessAlert";
                    ddlexport.Visible = true;
                    btnexport.Visible = true;
                }
                else
                {
                    GvSubRackType.DataSource = null;
                    GvSubRackType.DataBind();
                    GvSubRackType.Visible = true;
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
        private List<RackMasterData> GetSubRackType(int p)
        {
            RackMasterData objData = new RackMasterData();
            RackMasterBO objBO = new RackMasterBO();
            objData.StockTypeID = Convert.ToInt32(ddl_store.SelectedValue == "" ? null : ddl_store.SelectedValue);
            objData.RackID = Convert.ToInt32(ddl_rack.SelectedValue == "" ? null : ddl_rack.SelectedValue);
            objData.SubRack = txtsubrack.Text == "" ? "" : txtsubrack.Text;
            objData.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
            return objBO.SearchSubRackDetails(objData);
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
        protected void GvSubRackType_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GvSubRackType.PageIndex = e.NewPageIndex;
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
            ddl_rack.SelectedIndex = 0;
            ddlstatus.SelectedIndex = 0;
            txtsubrack.Text = "";
            GvSubRackType.DataSource = null;
            GvSubRackType.DataBind();
            GvSubRackType.Visible = false;
            ddlexport.Visible = false;
            btnexport.Visible = false;
        }
        private void ExportToPdf()
        {
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    GvSubRackType.BorderStyle = BorderStyle.None;
                    //Hide the Column containing CheckBox
                     GvSubRackType.Columns[6].Visible = false;
                    GvSubRackType.Columns[7].Visible = false;
                    GvSubRackType.Columns[8].Visible = false;

                    GvSubRackType.RenderControl(hw);
                    GvSubRackType.HeaderRow.Style.Add("width", "15%");
                    GvSubRackType.HeaderRow.Style.Add("font-size", "10px");
                    GvSubRackType.Style.Add("text-decoration", "none");
                    GvSubRackType.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    GvSubRackType.Style.Add("font-size", "8px");
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 7f, 7f, 7f, 0f);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=SubRackDetails.pdf");
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
                Response.AddHeader("content-disposition", "attachment;filename=SubRackDetails.xlsx");
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
            List<RackMasterData> TypeDetails = GetSubRackType(0);
            List<SubRackMasterDatatoExcel> ListexcelData = new List<SubRackMasterDatatoExcel>();
            int i = 0;
            foreach (RackMasterData row in TypeDetails)
            {
                SubRackMasterDatatoExcel ExcelSevice = new SubRackMasterDatatoExcel();
                ExcelSevice.SubRackID = TypeDetails[i].SubRackID;
                ExcelSevice.Stock = TypeDetails[i].Stock;
                ExcelSevice.RackNumber = TypeDetails[i].RackNumber;
                ExcelSevice.SubRack = TypeDetails[i].SubRack;
                ExcelSevice.AddedBy = TypeDetails[i].EmpName;
                GvSubRackType.Columns[6].Visible = false;
                GvSubRackType.Columns[7].Visible = false;
                GvSubRackType.Columns[8].Visible = false;
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
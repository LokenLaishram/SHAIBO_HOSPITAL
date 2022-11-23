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
using Mediqura.CommonData.MedStore;
using Mediqura.BOL.MedStore;

namespace Mediqura.Web.MedStore
{
    public partial class RackMaster : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlbind();
                lblmessage.Visible = false;
            }
        }
        private void ddlbind()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddl_store, mstlookup.GetLookupsList(LookupName.StockType));
             ViewState["ID"] = null;
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
                    ddl_store.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txt_rack.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Please enter rack.", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    txt_rack.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                

                RackMasterData objdMasterData = new RackMasterData();
                RackMasterBO objMasterBO = new RackMasterBO();
                objdMasterData.StockTypeID = Convert.ToInt16(ddl_store.SelectedValue == "0" ? null : ddl_store.SelectedValue);
                objdMasterData.RackNumber = txt_rack.Text == "" ? null : txt_rack.Text;
                objdMasterData.EmployeeID = LogData.EmployeeID;
                objdMasterData.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
                objdMasterData.HospitalID = LogData.HospitalID;
                objdMasterData.FinancialYearID = LogData.FinancialYearID;
                objdMasterData.ActionType = Enumaction.Insert;
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
                        objdMasterData.ActionType = Enumaction.Update;
                        objdMasterData.RackID = Convert.ToInt32(ViewState["ID"].ToString());
                    }
                }
                int result = objMasterBO.UpdateRackDetails(objdMasterData);
                if (result == 1 || result == 2)
                {
                    lblmessage.Visible = true;
                    Messagealert_.ShowMessage(lblmessage, result == 1 ? "save" : "update", 1);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "SucessAlert";
                    ViewState["ID"] = null;
                    bind_grid();
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
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";

            }
        }
        protected void GvRackType_RowCommand(object sender, GridViewCommandEventArgs e)
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
                    RackMasterData objBlockMasterData = new RackMasterData();
                    RackMasterBO objBlockMasterBO = new RackMasterBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow pt = GvRackType.Rows[i];
                    Label ID = (Label)pt.Cells[0].FindControl("lbl_rackid");
                    objBlockMasterData.RackID = Convert.ToInt32(ID.Text);
                    objBlockMasterData.ActionType = Enumaction.Select;

                    List<RackMasterData> GetResult = objBlockMasterBO.GetRackTypeDetailsByID(objBlockMasterData);
                    if (GetResult.Count > 0)
                    {
                        ddl_store.SelectedValue = GetResult[0].StockTypeID.ToString();
                        txt_rack.Text = GetResult[0].RackNumber.ToString();
                        ViewState["ID"] = GetResult[0].RackID;
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
                    RackMasterData objBlockMasterData = new RackMasterData();
                    RackMasterBO objBlockMasterBO = new RackMasterBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GvRackType.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("lbl_rackid");
                    objBlockMasterData.RackID = Convert.ToInt32(ID.Text);
                    objBlockMasterData.EmployeeID = LogData.EmployeeID;
                    objBlockMasterData.ActionType = Enumaction.Delete;
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
                        objBlockMasterData.Remarks = txtremarks.Text;
                    }

                    RackMasterBO objBlockMasterBO1 = new RackMasterBO();
                    int Result = objBlockMasterBO1.DeleteRackTypeDetailsByID(objBlockMasterData);
                    if (Result == 1)
                    {
                        Messagealert_.ShowMessage(lblmessage, "delete", 1);
                        divmsg1.Visible = true;
                        divmsg1.Attributes["class"] = "SuccesAlert";
                        bind_grid();
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
        private void bind_grid()
        {
            try
            {

                List<RackMasterData> lstemp = GetRackType(0);

                if (lstemp.Count > 0)
                {
                    GvRackType.DataSource = lstemp;
                    GvRackType.DataBind();
                    GvRackType.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total: " + lstemp[0].MaximumRows.ToString() + " Record(s) found.", 1);
                    divmsg3.Visible = true;
                    divmsg3.Attributes["class"] = "SucessAlert";
                    ddlexport.Visible = true;
                    btnexport.Visible = true;

                }
                else
                {
                    GvRackType.DataSource = null;
                    GvRackType.DataBind();
                    GvRackType.Visible = true;
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
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
            }
        }
        private List<RackMasterData> GetRackType(int p)
        {
            RackMasterData objFloorMasterData = new RackMasterData();
            RackMasterBO objBlockMasterBO = new RackMasterBO();
            objFloorMasterData.StockTypeID = Convert.ToInt16(ddl_store.SelectedValue == "0" ? null : ddl_store.SelectedValue);
            objFloorMasterData.RackNumber = txt_rack.Text == "" ? null : txt_rack.Text;
            objFloorMasterData.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
            return objBlockMasterBO.SearchRackTypeDetails(objFloorMasterData);
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


            bind_grid();
        }
        protected void btnresets_Click(object sender, EventArgs e)
        {
            clear_all();
        }
        private void clear_all()
        {
            lblmessage.Visible = false;
            lblresult.Visible = false;
            ddl_store.SelectedValue = "0";
            ddlexport.Visible = false;
            btnexport.Visible = false;
            ddlexport.Visible = false;
            ViewState["ID"] = null;
            txt_rack.Text = "";
            GvRackType.DataSource = null;
            GvRackType.DataBind();
            GvRackType.Visible = false;
         }
        private void ExportToPdf()
        {
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    GvRackType.BorderStyle = BorderStyle.None;
                    //Hide the Column containing CheckBox
                    GvRackType.Columns[5].Visible = false;
                    GvRackType.Columns[6].Visible = false;
                    GvRackType.Columns[7].Visible = false;
                    GvRackType.RenderControl(hw);
                    GvRackType.HeaderRow.Style.Add("width", "15%");
                    GvRackType.HeaderRow.Style.Add("font-size", "10px");
                    GvRackType.Style.Add("text-decoration", "none");
                    GvRackType.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    GvRackType.Style.Add("font-size", "8px");
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 7f, 7f, 7f, 0f);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=RackTypeDetails.pdf");
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
                wb.Worksheets.Add(dt, "Bed Type Detail List");
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=RackTypeDetails.xlsx");
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
            List<RackMasterData> TypeDetails = GetRackType(0);
            List<RackTypeDatatoExcel> ListexcelData1 = new List<RackTypeDatatoExcel>();
            int i = 0;
            foreach (RackMasterData row in TypeDetails)
            {
                RackTypeDatatoExcel ExcelSevice = new RackTypeDatatoExcel();
                ExcelSevice.RackID = TypeDetails[i].RackID;
                ExcelSevice.Stock = TypeDetails[i].Stock;
                ExcelSevice.RackNumber = TypeDetails[i].RackNumber;
                ExcelSevice.AddedBy = TypeDetails[i].EmpName;
                GvRackType.Columns[4].Visible = false;
                GvRackType.Columns[5].Visible = false;
                GvRackType.Columns[6].Visible = false;
                GvRackType.Columns[7].Visible = false;
                ListexcelData1.Add(ExcelSevice);
                //i++;
            }
            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            DataTable dt = converter.ToDataTable(ListexcelData1);
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
        protected void GvRackType_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GvRackType.PageIndex = e.NewPageIndex;
            bind_grid();
        }
    }
}
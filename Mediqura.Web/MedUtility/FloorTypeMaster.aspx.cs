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
    public partial class FloorTypeMaster : BasePage
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
            Commonfunction.PopulateDdl(ddl_block, mstlookup.GetLookupsList(LookupName.BlockType));
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

                if (ddl_block.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "Block", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    ddl_block.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
              
                if (txt_floor.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Code", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    txt_floortype.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txt_floortype.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "FloorType", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    txt_floortype.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }

                FloorMasterData objFloorMasterData = new FloorMasterData();
                FloorMasterBO objFloorMasterBO = new FloorMasterBO();
                objFloorMasterData.BlockID = Convert.ToInt16(ddl_block.SelectedValue == "0" ? null : ddl_block.SelectedValue);
                objFloorMasterData.FloorTypeCode = txt_floor.Text == "" ? null : txt_floor.Text;
                objFloorMasterData.FloorType = txt_floortype.Text == "" ? null : txt_floortype.Text;
                objFloorMasterData.EmployeeID = LogData.EmployeeID;
                objFloorMasterData.IsActive = ddl_status.SelectedValue == "0" ? true : false;
                objFloorMasterData.HospitalID = LogData.HospitalID;
                objFloorMasterData.FinancialYearID = LogData.FinancialYearID;
                objFloorMasterData.IPaddress = LogData.IPaddress;
          
          
                objFloorMasterData.ActionType = Enumaction.Insert;
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
                        objFloorMasterData.ActionType = Enumaction.Update;
                        objFloorMasterData.ID = Convert.ToInt32(ViewState["ID"].ToString());
                    }
                }
                int result = objFloorMasterBO.UpdateFloorDetails(objFloorMasterData);
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
        protected void GvFloorType_RowCommand(object sender, GridViewCommandEventArgs e)
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
                    FloorMasterData objBlockMasterData = new FloorMasterData();
                    FloorMasterBO objBlockMasterBO = new FloorMasterBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow pt = GvFloorType.Rows[i];
                    Label ID = (Label)pt.Cells[0].FindControl("lblfloortypecodeID");
                    objBlockMasterData.ID = Convert.ToInt32(ID.Text);
                    objBlockMasterData.ActionType = Enumaction.Select;

                    List<FloorMasterData> GetResult = objBlockMasterBO.GetFloorTypeDetailsByID(objBlockMasterData);
                    if (GetResult.Count > 0)
                    {
                        ddl_block.SelectedValue = GetResult[0].BlockID.ToString();
                        txt_floor.Text = GetResult[0].FloorTypeCode;
                        txt_floortype.Text = GetResult[0].FloorType;
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
                    FloorMasterData objBlockMasterData = new FloorMasterData();
                    FloorMasterBO objBlockMasterBO = new FloorMasterBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GvFloorType.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("lblfloortypecodeID");
                    objBlockMasterData.ID = Convert.ToInt32(ID.Text);
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

                    FloorMasterBO objBlockMasterBO1 = new FloorMasterBO();
                    int Result = objBlockMasterBO1.DeleteFloorTypeDetailsByID(objBlockMasterData);
                    if (Result == 1)
                    {
                        Messagealert_.ShowMessage(lblmessage, "delete", 1);
                        divmsg1.Visible = true;
                        divmsg1.Attributes["class"] = "SucessAlert";

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
                
                List<FloorMasterData> lstemp = GetFloorType(0);

                if (lstemp.Count > 0)
                {
                    GvFloorType.DataSource = lstemp;
                    GvFloorType.DataBind();
                    GvFloorType.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total: " + lstemp[0].MaximumRows.ToString() + " Record(s) found", 1);
                    divmsg3.Visible = true;
                    divmsg3.Attributes["class"] = "SucessAlert";
                    ddlexport.Visible = true;
                    btnexport.Visible = true;
                }
                else
                {
                    GvFloorType.DataSource = null;
                    GvFloorType.DataBind();
                    GvFloorType.Visible = true;
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
        private List<FloorMasterData> GetFloorType(int p)
        {
            FloorMasterData objFloorMasterData = new FloorMasterData();
            FloorMasterBO objBlockMasterBO = new FloorMasterBO();
            objFloorMasterData.BlockID = Convert.ToInt16(ddl_block.SelectedValue == "0" ? null : ddl_block.SelectedValue);
            objFloorMasterData.FloorTypeCode = txt_floor.Text == "" ? "" : txt_floor.Text;
            objFloorMasterData.FloorType = txt_floortype.Text == "" ? "" : txt_floortype.Text;
            objFloorMasterData.IsActive = ddl_status.SelectedValue == "0" ? true : false;
            return objBlockMasterBO.SearchFloorTypeDetails(objFloorMasterData);
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
            ddl_block.SelectedValue = "0";
            ViewState["ID"] = null;
            clear_all();
            lblmessage.Visible = false;
            lblresult.Visible = false;
        }
        private void clear_all()
        {
            txt_floor.Text = "";
            txt_floortype.Text = "";
            GvFloorType.DataSource = null;
            GvFloorType.DataBind();
            GvFloorType.Visible = false;
            ddlexport.Visible = false;
            btnexport.Visible = false;
        }
        private void ExportToPdf()
        {
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    GvFloorType.BorderStyle = BorderStyle.None;
                    //Hide the Column containing CheckBox
                    GvFloorType.Columns[4].Visible = false;
                    GvFloorType.Columns[5].Visible = false;
                    GvFloorType.Columns[6].Visible = false;
                    GvFloorType.Columns[7].Visible = false;
                    GvFloorType.RenderControl(hw);
                    GvFloorType.HeaderRow.Style.Add("width", "15%");
                    GvFloorType.HeaderRow.Style.Add("font-size", "10px");
                    GvFloorType.Style.Add("text-decoration", "none");
                    GvFloorType.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    GvFloorType.Style.Add("font-size", "8px");
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 7f, 7f, 7f, 0f);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=BlockTypeDetails.pdf");
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
                wb.Worksheets.Add(dt, "Department Type Detail List");
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=FloorTypeDetails.xlsx");
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
            List<FloorMasterData> FloorTypeDetails = GetFloorType(0);
            List<FloorDatatoExcel> ListexcelData1 = new List<FloorDatatoExcel>();
            int i = 0;
            foreach (FloorMasterData row in FloorTypeDetails)
            {
                FloorDatatoExcel ExcelSevice = new FloorDatatoExcel();
                ExcelSevice.ID = FloorTypeDetails[i].ID;
                ExcelSevice.BlockID = FloorTypeDetails[i].BlockID;
                ExcelSevice.FloorTypeCode = FloorTypeDetails[i].FloorTypeCode;
                ExcelSevice.FloorType = FloorTypeDetails[i].FloorType;
                ExcelSevice.AddedBy = FloorTypeDetails[i].EmpName;
                GvFloorType.Columns[4].Visible = false;
                GvFloorType.Columns[5].Visible = false;
                GvFloorType.Columns[6].Visible = false;
                GvFloorType.Columns[7].Visible = false;
                ListexcelData1.Add(ExcelSevice);
                i++;
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
        protected void GvFloorType_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GvFloorType.PageIndex = e.NewPageIndex;
            bind_grid();
        }


    }
}
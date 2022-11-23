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
    public partial class FloorMaster : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblmessage.Visible = false;
            }
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
                if (txt_block.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Code", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";

                    txt_block.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txt_blocktype.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "BlockType", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";

                    txt_blocktype.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }

                BlockMasterData objBlockMasterData = new BlockMasterData();
                BlockMasterBO objBlockMasterBO = new BlockMasterBO();
                objBlockMasterData.BlockTypeCode = txt_block.Text == "" ? null : txt_block.Text;
                objBlockMasterData.BlockType = txt_blocktype.Text == "" ? null : txt_blocktype.Text;
                objBlockMasterData.EmployeeID = LogData.EmployeeID;
                objBlockMasterData.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
                objBlockMasterData.HospitalID = LogData.HospitalID;
                objBlockMasterData.FinancialYearID = LogData.FinancialYearID;
                objBlockMasterData.ActionType = Enumaction.Insert;
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
                        objBlockMasterData.ActionType = Enumaction.Update;
                        objBlockMasterData.ID = Convert.ToInt32(ViewState["ID"].ToString());
                    }
                }
                int result = objBlockMasterBO.UpdateBlockDetails(objBlockMasterData);
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
        protected void GvBlockType_RowCommand(object sender, GridViewCommandEventArgs e)
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
                    BlockMasterData objBlockMasterData = new BlockMasterData();
                    BlockMasterBO objBlockMasterBO = new BlockMasterBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow pt = GvBlockType.Rows[i];
                    Label ID = (Label)pt.Cells[0].FindControl("lblblocktypecodeID");
                    objBlockMasterData.ID = Convert.ToInt32(ID.Text);
                    objBlockMasterData.ActionType = Enumaction.Select;

                    List<BlockMasterData> GetResult = objBlockMasterBO.GetBlockTypeDetailsByID(objBlockMasterData);
                    if (GetResult.Count > 0)
                    {
                        txt_block.Text = GetResult[0].BlockTypeCode;
                        txt_blocktype.Text = GetResult[0].BlockType;
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
                    BlockMasterData objBlockMasterData = new BlockMasterData();
                    BlockMasterBO objBlockMasterBO = new BlockMasterBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GvBlockType.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("lblblocktypecodeID");
                    objBlockMasterData.ID = Convert.ToInt32(ID.Text);
                    objBlockMasterData.EmployeeID = LogData.EmployeeID;
                    objBlockMasterData.ActionType = Enumaction.Delete;
                    TextBox txtremarks = (TextBox)gr.Cells[0].FindControl("txtremarks");
                    txtremarks.Enabled = true;
                    if (txtremarks.Text == "")
                    {
                        Messagealert_.ShowMessage(lblresult, "Remarks", 0);
                        divmsg1.Visible = true;
                        divmsg1.Attributes["class"] = "FailAlert";
                        txtremarks.Focus();
                        return;
                    }
                    else
                    {
                        objBlockMasterData.Remarks = txtremarks.Text;
                    }

                    BlockMasterBO objBlockMasterBO1 = new BlockMasterBO();
                    int Result = objBlockMasterBO1.DeleteBlockTypeDetailsByID(objBlockMasterData);
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
                
                List<BlockMasterData> lstemp = GetBlockType(0);

                if (lstemp.Count > 0)
                {
                    GvBlockType.DataSource = lstemp;
                    GvBlockType.DataBind();
                    GvBlockType.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total: " + lstemp[0].MaximumRows.ToString() + " Record(s) found", 1);
                    divmsg3.Visible = true;
                    divmsg3.Attributes["class"] = "SucessAlert";

                    ddlexport.Visible = true;
                    btnexport.Visible = true;

                }
                else
                {
                    GvBlockType.DataSource = null;
                    GvBlockType.DataBind();
                    GvBlockType.Visible = true;
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
        private List<BlockMasterData> GetBlockType(int p)
        {
            BlockMasterData objBlockMasterData = new BlockMasterData();
            BlockMasterBO objBlockMasterBO = new BlockMasterBO();
            objBlockMasterData.BlockTypeCode = txt_block.Text == "" ? "" : txt_block.Text;
            objBlockMasterData.BlockType = txt_blocktype.Text == "" ? "" : txt_blocktype.Text;
            objBlockMasterData.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
            return objBlockMasterBO.SearchBlockTypeDetails(objBlockMasterData);
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
        protected void btnresets_Click(object sender, EventArgs e)
        {
            ViewState["ID"] = null;
            clearall();
            lblmessage.Visible = false;
            lblresult.Visible = false;
        }
        private void clearall()
        {
            txt_block.Text = "";
            txt_blocktype.Text = "";
            GvBlockType.DataSource = null;
            GvBlockType.DataBind();
            GvBlockType.Visible = false;
            ddlexport.Visible = false;
            btnexport.Visible = false;
        }
        private void ExportToPdf()
        {
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    GvBlockType.AllowCustomPaging = false;
                    this.bindgrid();
                    GvBlockType.BorderStyle = BorderStyle.None;
                    //Hide the Column containing CheckBox
                    GvBlockType.Columns[4].Visible = false;
                    GvBlockType.Columns[5].Visible = false;
                    GvBlockType.Columns[6].Visible = false;
                    GvBlockType.Columns[7].Visible = false;
                    GvBlockType.RenderControl(hw);
                    GvBlockType.HeaderRow.Style.Add("width", "15%");
                    GvBlockType.HeaderRow.Style.Add("font-size", "10px");
                    GvBlockType.Style.Add("text-decoration", "none");
                    GvBlockType.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    GvBlockType.Style.Add("font-size", "8px");
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
                Response.AddHeader("content-disposition", "attachment;filename=BlockTypeDetails.xlsx");
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
            List<BlockMasterData> BlockTypeDetails = GetBlockType(0);
            List<BlockDatatoExcel> ListexcelData = new List<BlockDatatoExcel>();
            int i = 0;
            foreach (BlockMasterData row in BlockTypeDetails)
            {
                BlockDatatoExcel ExcelSevice = new BlockDatatoExcel();
                ExcelSevice.ID = BlockTypeDetails[i].ID;
                ExcelSevice.BlockTypeCode = BlockTypeDetails[i].BlockTypeCode;
                ExcelSevice.BlockType = BlockTypeDetails[i].BlockType;
                ExcelSevice.AddedBy = BlockTypeDetails[i].EmpName;
                GvBlockType.Columns[4].Visible = false;
                GvBlockType.Columns[5].Visible = false;
                GvBlockType.Columns[6].Visible = false;
                GvBlockType.Columns[7].Visible = false;
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
                ddlexport.Focus();
                return;
            }
        }
        protected void GvBlockType_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GvBlockType.PageIndex = e.NewPageIndex;
            bindgrid();
        }
    }
}
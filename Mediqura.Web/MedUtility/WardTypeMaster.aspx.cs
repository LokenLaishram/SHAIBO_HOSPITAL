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
    public partial class WardTypeMaster : BasePage
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
            Commonfunction.PopulateDdl(ddl_floor, mstlookup.GetLookupsList(LookupName.FloorType));

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
                if (ddl_floor.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "Floor", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    ddl_floor.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txt_ward.Text.Trim() == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Code", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    txt_ward.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txt_wardtype.Text.Trim() == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "FloorType", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    txt_wardtype.Focus();
                    return;
                }
                {
                    lblmessage.Visible = false;
                }
                if (Convert.ToDecimal(txt_lowerlimit.Text == "" ? "" : txt_lowerlimit.Text) > Convert.ToDecimal(txt_upperlimit.Text == "" ? "" : txt_upperlimit.Text))
                {
                    Messagealert_.ShowMessage(lblmessage, "UpperLimit", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    txt_lowerlimit.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                WardMasterData objWardMasterData = new WardMasterData();
                WardTypeMasterBO objWardMasterBO = new WardTypeMasterBO();
                objWardMasterData.BlockID = Convert.ToInt16(ddl_block.SelectedValue == "0" ? null : ddl_block.SelectedValue);
                objWardMasterData.FloorID = Convert.ToInt16(ddl_floor.SelectedValue == "0" ? null : ddl_floor.SelectedValue);
                objWardMasterData.Admnchargeapplied = Convert.ToInt16(ddl_admapplied.SelectedValue == "0" ? null : ddl_admapplied.SelectedValue);
                objWardMasterData.WardTypeCode = txt_ward.Text == "" ? null : txt_ward.Text;
                objWardMasterData.WardType = txt_wardtype.Text == "" ? null : txt_wardtype.Text;
                objWardMasterData.EmployeeID = LogData.EmployeeID;
                objWardMasterData.IsActive = ddl_status.SelectedValue == "0" ? true : false;
                objWardMasterData.HospitalID = LogData.HospitalID;
                objWardMasterData.PHRloweLimit = Convert.ToDecimal(txt_lowerlimit.Text == "" ? "0" : txt_lowerlimit.Text);
                objWardMasterData.PHRupperLimit = Convert.ToDecimal(txt_upperlimit.Text == "" ? "0" : txt_upperlimit.Text);
                objWardMasterData.FinancialYearID = LogData.FinancialYearID;
                objWardMasterData.ActionType = Enumaction.Insert;
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
                        objWardMasterData.ActionType = Enumaction.Update;
                        objWardMasterData.ID = Convert.ToInt32(ViewState["ID"].ToString());
                    }
                }
                int result = objWardMasterBO.UpdateWardDetails(objWardMasterData);
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

            }
        }
        protected void GvWardType_RowCommand(object sender, GridViewCommandEventArgs e)
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
                    WardMasterData objBlockMasterData = new WardMasterData();
                    WardTypeMasterBO objBlockMasterBO = new WardTypeMasterBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow pt = GvWardType.Rows[i];
                    Label ID = (Label)pt.Cells[0].FindControl("lblwardtypecodeID");
                    objBlockMasterData.ID = Convert.ToInt32(ID.Text);
                    objBlockMasterData.ActionType = Enumaction.Select;

                    List<WardMasterData> GetResult = objBlockMasterBO.GetWardTypeDetailsByID(objBlockMasterData);
                    if (GetResult.Count > 0)
                    {
                        ddl_block.SelectedValue = GetResult[0].BlockID.ToString();
                        ddl_floor.SelectedValue = GetResult[0].FloorID.ToString();
                        ddl_admapplied.SelectedValue = GetResult[0].Admnchargeapplied.ToString();
                        txt_ward.Text = GetResult[0].WardTypeCode;
                        txt_wardtype.Text = GetResult[0].WardType;
                        txt_lowerlimit.Text = Commonfunction.Getrounding(GetResult[0].PHRloweLimit.ToString());
                        txt_upperlimit.Text = Commonfunction.Getrounding(GetResult[0].PHRupperLimit.ToString());
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
                    WardMasterData objBlockMasterData = new WardMasterData();
                    WardTypeMasterBO objBlockMasterBO = new WardTypeMasterBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GvWardType.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("lblwardtypecodeID");
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

                    WardTypeMasterBO objBlockMasterBO1 = new WardTypeMasterBO();
                    int Result = objBlockMasterBO1.DeleteWardTypeDetailsByID(objBlockMasterData);
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

                List<WardMasterData> lstemp = GetWardType(0);

                if (lstemp.Count > 0)
                {
                    GvWardType.DataSource = lstemp;
                    GvWardType.DataBind();
                    GvWardType.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total: " + lstemp[0].MaximumRows.ToString() + " Record(s) found", 1);
                    divmsg3.Visible = true;
                    divmsg3.Attributes["class"] = "SucessAlert";

                    ddlexport.Visible = true;
                    btnexport.Visible = true;
                }
                else
                {
                    GvWardType.DataSource = null;
                    GvWardType.DataBind();
                    GvWardType.Visible = true;
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
        private List<WardMasterData> GetWardType(int p)
        {
            WardMasterData objFloorMasterData = new WardMasterData();
            WardTypeMasterBO objBlockMasterBO = new WardTypeMasterBO();
            objFloorMasterData.BlockID = Convert.ToInt16(ddl_block.SelectedValue == "0" ? null : ddl_block.SelectedValue);
            objFloorMasterData.FloorID = Convert.ToInt16(ddl_floor.SelectedValue == "0" ? null : ddl_floor.SelectedValue);
            objFloorMasterData.WardTypeCode = txt_ward.Text == "" ? "" : txt_ward.Text;
            objFloorMasterData.WardType = txt_wardtype.Text == "" ? "" : txt_wardtype.Text;
            objFloorMasterData.IsActive = ddl_status.SelectedValue == "0" ? true : false;
            return objBlockMasterBO.SearchWardTypeDetails(objFloorMasterData);
        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            bind_grid();
        }
        protected void btnresets_Click(object sender, EventArgs e)
        {
            ddl_block.SelectedValue = "0";
            ddl_floor.SelectedValue = "0";
            ViewState["ID"] = null;
            clear_all();
            lblmessage.Visible = false;
            lblresult.Visible = false;
            ddl_admapplied.SelectedIndex = 0;
            txt_lowerlimit.Text = "";
            txt_upperlimit.Text = "";
        }
        private void clear_all()
        {
            txt_ward.Text = "";
            txt_wardtype.Text = "";
            GvWardType.DataSource = null;
            GvWardType.DataBind();
            GvWardType.Visible = false;
            ddlexport.Visible = false;
            btnexport.Visible = false;
        }
        private void ExportToPdf()
        {
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    GvWardType.AllowPaging = false;
                    this.bind_grid();
                    GvWardType.BorderStyle = BorderStyle.None;
                    //Hide the Column containing CheckBox
                    GvWardType.Columns[7].Visible = false;
                    GvWardType.Columns[8].Visible = false;
                    GvWardType.Columns[9].Visible = false;
                    GvWardType.RenderControl(hw);
                    GvWardType.HeaderRow.Style.Add("width", "15%");
                    GvWardType.HeaderRow.Style.Add("font-size", "10px");
                    GvWardType.Style.Add("text-decoration", "none");
                    GvWardType.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    GvWardType.Style.Add("font-size", "8px");
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 7f, 7f, 7f, 0f);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=WardTypeDetails.pdf");
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
                wb.Worksheets.Add(dt, "Ward Type Detail List");
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=WardTypeDetails.xlsx");
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
            List<WardMasterData> WardTypeDetails = GetWardType(0);
            List<WardDatatoExcel> ListexcelData1 = new List<WardDatatoExcel>();
            int i = 0;
            foreach (WardMasterData row in WardTypeDetails)
            {
                WardDatatoExcel ExcelSevice = new WardDatatoExcel();
                ExcelSevice.ID = WardTypeDetails[i].ID;
                ExcelSevice.Block = WardTypeDetails[i].Block;
                ExcelSevice.Floor1 = WardTypeDetails[i].Floor1;
                ExcelSevice.WardTypeCode = WardTypeDetails[i].WardTypeCode;
                ExcelSevice.WardType = WardTypeDetails[i].WardType;
                ExcelSevice.AddedBy = WardTypeDetails[i].EmpName;
                GvWardType.Columns[4].Visible = false;
                GvWardType.Columns[5].Visible = false;
                GvWardType.Columns[6].Visible = false;
                GvWardType.Columns[7].Visible = false;
                ListexcelData1.Add(ExcelSevice);
                i++;
            }
            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            DataTable dt = converter.ToDataTable(ListexcelData1);
            return dt;
        }
        protected void ddl_block_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_block.SelectedIndex > 0)
            {
                MasterLookupBO mstlookup = new MasterLookupBO();
                Commonfunction.PopulateDdl(ddl_floor, mstlookup.GetfloorByblockID(Convert.ToInt32(ddl_block.SelectedValue)));
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

        protected void GvWardType_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GvWardType.PageIndex = e.NewPageIndex;
            bind_grid();
        }

    }
}

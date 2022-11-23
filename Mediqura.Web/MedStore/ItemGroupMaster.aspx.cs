using ClosedXML.Excel;
using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedStoreBO;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.MedStoreData;
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

namespace Mediqura.Web.MedStore
{
    public partial class ItemGroupMaster : BasePage
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
                if (txtcode.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Please enter code.", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    txtcode.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;

                }
                if (txtitemtype.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Please enter item group.", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    txtitemtype.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }

                ItemGroupMasterData objitemgroupData = new ItemGroupMasterData();
                ItemGroupMasterBO objitemgroupBO = new ItemGroupMasterBO();
                objitemgroupData.Code = txtcode.Text == "" ? null : txtcode.Text;
                objitemgroupData.ItemType = txtitemtype.Text == "" ? null : txtitemtype.Text;
                objitemgroupData.EmployeeID = LogData.EmployeeID;
                objitemgroupData.HospitalID = LogData.HospitalID;
                objitemgroupData.IPaddress = LogData.IPaddress;
                objitemgroupData.FinancialYearID = LogData.FinancialYearID;
                objitemgroupData.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
                objitemgroupData.ActionType = Enumaction.Insert;
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
                        objitemgroupData.ActionType = Enumaction.Update;
                        objitemgroupData.ID = Convert.ToInt32(ViewState["ID"].ToString());
                        lblmessage.Visible = false;
                    }
  
                }
                int result = objitemgroupBO.UpdateItemGroupDetails(objitemgroupData);
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
                return;

            }
        }
        protected void GvItemType_RowCommand(object sender, GridViewCommandEventArgs e)
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


                    ItemGroupMasterData objItemTypeMasterData = new ItemGroupMasterData();
                    ItemGroupMasterBO objItemTypeMasterBO = new ItemGroupMasterBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GvItemType.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("code");
                    objItemTypeMasterData.ID = Convert.ToInt32(ID.Text);
                    objItemTypeMasterData.ActionType = Enumaction.Select;

                    List<ItemGroupMasterData> GetResult = objItemTypeMasterBO.GetItemTypeDetailsByID(objItemTypeMasterData);
                    if (GetResult.Count > 0)
                    {
                        txtcode.Text = GetResult[0].Code;
                        txtitemtype.Text = GetResult[0].ItemType;
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
                    ItemGroupMasterData objItemTypeMasterData = new ItemGroupMasterData();
                    ItemGroupMasterBO objItemTypeMasterBO = new ItemGroupMasterBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GvItemType.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("code");
                    objItemTypeMasterData.ID = Convert.ToInt32(ID.Text);
                    objItemTypeMasterData.EmployeeID = LogData.EmployeeID;
                    objItemTypeMasterData.ActionType = Enumaction.Delete;
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
                        objItemTypeMasterData.Remarks = txtremarks.Text;
                    }

                    ItemGroupMasterBO objItemTypeMasterBO1 = new ItemGroupMasterBO();
                    int Result = objItemTypeMasterBO1.DeleteItemTypeDetailsByID(objItemTypeMasterData);
                    if (Result == 1)
                    {
                        Messagealert_.ShowMessage(lblmessage, "delete", 1);
                        divmsg1.Visible = true;
                        divmsg1.Attributes["class"] = "SucessAlert";
                        bindgrid();
                        return;
                    }
                    else
                    {
                        Messagealert_.ShowMessage(lblmessage, "system", 0);
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
               
                List<ItemGroupMasterData> lstemp = GetItemType(0);

                if (lstemp.Count > 0)
                {
                    GvItemType.DataSource = lstemp;
                    GvItemType.DataBind();
                    GvItemType.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total: " + lstemp[0].MaximumRows.ToString() + " Record found", 1);
                    divmsg3.Visible = true;
                    divmsg3.Attributes["class"] = "SucessAlert";
                    ddlexport.Visible = true;
                    btnexport.Visible = true;
                }
                else
                {
                    GvItemType.DataSource = null;
                    GvItemType.DataBind();
                    GvItemType.Visible = true;
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
                return;
            }
        }
        private List<ItemGroupMasterData> GetItemType(int p)
        {
            ItemGroupMasterData objItemTypeMasterData = new ItemGroupMasterData();
            ItemGroupMasterBO objDepartmentTypeMasterBO = new ItemGroupMasterBO();
            objItemTypeMasterData.Code = txtcode.Text == "" ? "" : txtcode.Text;
            objItemTypeMasterData.ItemType = txtitemtype.Text == "" ? "" : txtitemtype.Text;
            objItemTypeMasterData.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
            return objDepartmentTypeMasterBO.SearchItemTypeDetails(objItemTypeMasterData);
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
            txtcode.Text = "";
            txtitemtype.Text = "";
            ddlstatus.SelectedIndex = 0;
            GvItemType.DataSource = null;
            GvItemType.DataBind();
            GvItemType.Visible = false;
            ddlexport.Visible = false;
            btnexport.Visible = false;
        }
        private void ExportToPdf()
        {
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    GvItemType.BorderStyle = BorderStyle.None;
                    //Hide the Column containing CheckBox
                    GvItemType.Columns[4].Visible = false;
                    GvItemType.Columns[5].Visible = false;
                    GvItemType.Columns[6].Visible = false;
                    GvItemType.Columns[7].Visible = false;

                    GvItemType.RenderControl(hw);
                    GvItemType.HeaderRow.Style.Add("width", "15%");
                    GvItemType.HeaderRow.Style.Add("font-size", "10px");
                    GvItemType.Style.Add("text-decoration", "none");
                    GvItemType.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    GvItemType.Style.Add("font-size", "8px");
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 7f, 7f, 7f, 0f);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=ItemGroupDetails.pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.Write(pdfDoc);
                    Response.End();
                }
            }
        }
        protected void GvItemType_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GvItemType.PageIndex = e.NewPageIndex;
            bindgrid();
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
                Response.AddHeader("content-disposition", "attachment;filename=ItemGroupDetails.xlsx");
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
            List<ItemGroupMasterData> ItemTypeDetails = GetItemType(0);
            List<ItemDatatoExcel> ListexcelData = new List<ItemDatatoExcel>();
            int i = 0;
            foreach (ItemGroupMasterData row in ItemTypeDetails)
            {
                ItemDatatoExcel ExcelSevice = new ItemDatatoExcel();
                ExcelSevice.ID = ItemTypeDetails[i].ID;
                ExcelSevice.Code = ItemTypeDetails[i].Code;
                ExcelSevice.ItemType = ItemTypeDetails[i].ItemType;
                ExcelSevice.AddedBy = ItemTypeDetails[i].EmpName;
                GvItemType.Columns[4].Visible = false;
                GvItemType.Columns[5].Visible = false;
                GvItemType.Columns[6].Visible = false;
                GvItemType.Columns[7].Visible = false;
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
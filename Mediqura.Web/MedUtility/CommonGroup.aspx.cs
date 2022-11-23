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
    public partial class CommonGroup : BasePage
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
                if (txt_labgroupcode.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Code", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";

                    txt_labgroupcode.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txt_labgrouptype.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "GroupType", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";

                    txt_labgrouptype.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }

                CommonGroupMasterData objLabGroupTypeMasterData = new CommonGroupMasterData();
                CommonGroupMasterBO objLabGroupTypeMasterBO = new CommonGroupMasterBO();
                objLabGroupTypeMasterData.GroupCode = txt_labgroupcode.Text == "" ? null : txt_labgroupcode.Text;
                objLabGroupTypeMasterData.GroupType = txt_labgrouptype.Text == "" ? null : txt_labgrouptype.Text;
                objLabGroupTypeMasterData.EmployeeID = LogData.EmployeeID;
                objLabGroupTypeMasterData.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
                objLabGroupTypeMasterData.HospitalID = LogData.HospitalID;
                objLabGroupTypeMasterData.FinancialYearID = LogData.FinancialYearID;
                objLabGroupTypeMasterData.IPaddress = LogData.IPaddress;
                objLabGroupTypeMasterData.ActionType = Enumaction.Insert;
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
                        objLabGroupTypeMasterData.ActionType = Enumaction.Update;
                        objLabGroupTypeMasterData.ID = Convert.ToInt32(ViewState["ID"].ToString());
                    }
                }
                int result = objLabGroupTypeMasterBO.UpdateCommonGroupTypeDetails(objLabGroupTypeMasterData);
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

            }
        }
        protected void GvLabGroupType_RowCommand(object sender, GridViewCommandEventArgs e)
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
                    CommonGroupMasterData objLabGroupTypeMasterData = new CommonGroupMasterData();
                    CommonGroupMasterBO objLabGroupTypeMasterBO = new CommonGroupMasterBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow pt = GvLabGroupType.Rows[i];
                    Label ID = (Label)pt.Cells[0].FindControl("code");
                    objLabGroupTypeMasterData.ID = Convert.ToInt32(ID.Text);
                    objLabGroupTypeMasterData.ActionType = Enumaction.Select;

                    List<CommonGroupMasterData> GetResult = objLabGroupTypeMasterBO.GetCommonGroupTypeDetailsByID(objLabGroupTypeMasterData);
                    if (GetResult.Count > 0)
                    {
                        txt_labgroupcode.Text = GetResult[0].GroupCode;
                        txt_labgrouptype.Text = GetResult[0].GroupType;
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
                    CommonGroupMasterData objLabGroupTypeMasterData = new CommonGroupMasterData();
                    CommonGroupMasterBO objLabGroupTypeMasterBO = new CommonGroupMasterBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GvLabGroupType.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("code");
                    objLabGroupTypeMasterData.ID = Convert.ToInt32(ID.Text);
                    objLabGroupTypeMasterData.EmployeeID = LogData.EmployeeID;
                    objLabGroupTypeMasterData.ActionType = Enumaction.Delete;
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
                        objLabGroupTypeMasterData.Remarks = txtremarks.Text;
                    }

                    CommonGroupMasterBO objLabGroupTypeMasterBO1 = new CommonGroupMasterBO();
                    int Result = objLabGroupTypeMasterBO1.DeleteCommonGroupTypeDetailsByID(objLabGroupTypeMasterData);
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


                List<CommonGroupMasterData> lstemp = GetCommonGroupType(0);

                if (lstemp.Count > 0)
                {
                    GvLabGroupType.DataSource = lstemp;
                    GvLabGroupType.DataBind();
                    GvLabGroupType.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total: " + lstemp[0].MaximumRows.ToString() + " Record(s) found", 1);
                    divmsg3.Visible = true;
                    divmsg3.Attributes["class"] = "SucessAlert";

                    ddlexport.Visible = true;
                    btnexport.Visible = true;
                }
                else
                {
                    GvLabGroupType.DataSource = null;
                    GvLabGroupType.DataBind();
                    GvLabGroupType.Visible = true;
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
        private List<CommonGroupMasterData> GetCommonGroupType(int p)
        {
            CommonGroupMasterData objLabGroupTypeMasterData = new CommonGroupMasterData();
            CommonGroupMasterBO objLabGroupMasterBO = new CommonGroupMasterBO();
            objLabGroupTypeMasterData.GroupCode = txt_labgroupcode.Text == "" ? "" : txt_labgroupcode.Text;
            objLabGroupTypeMasterData.GroupType = txt_labgrouptype.Text == "" ? "" : txt_labgrouptype.Text;
            objLabGroupTypeMasterData.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
            return objLabGroupMasterBO.SearchCommonGroupTypeDetails(objLabGroupTypeMasterData);
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
            txt_labgroupcode.Text = "";
            txt_labgrouptype.Text = "";
            ddlstatus.SelectedIndex = 0;
            GvLabGroupType.DataSource = null;
            GvLabGroupType.DataBind();
            GvLabGroupType.Visible = false;
            ddlexport.Visible = false;
            btnexport.Visible = false;
            ddlstatus.SelectedIndex = 0;
        }
        private void ExportToPdf()
        {
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    GvLabGroupType.BorderStyle = BorderStyle.None;
                    //Hide the Column containing CheckBox
                    GvLabGroupType.Columns[4].Visible = false;
                    GvLabGroupType.Columns[5].Visible = false;
                    GvLabGroupType.Columns[6].Visible = false;
                    GvLabGroupType.Columns[7].Visible = false;
                    GvLabGroupType.RenderControl(hw);
                    GvLabGroupType.HeaderRow.Style.Add("width", "15%");
                    GvLabGroupType.HeaderRow.Style.Add("font-size", "10px");
                    GvLabGroupType.Style.Add("text-decoration", "none");
                    GvLabGroupType.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    GvLabGroupType.Style.Add("font-size", "8px");
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 7f, 7f, 7f, 0f);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=CommonGroupTypeDetails.pdf");
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
                Response.AddHeader("content-disposition", "attachment;filename=CommonGroupTypeDetails.xlsx");
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
            List<CommonGroupMasterData> LabGroupTypeDetails = GetCommonGroupType(0);
            List<CommonGroupDatatoExcel> ListexcelData = new List<CommonGroupDatatoExcel>();
            int i = 0;
            foreach (CommonGroupMasterData row in LabGroupTypeDetails)
            {
                CommonGroupDatatoExcel ExcelSevice = new CommonGroupDatatoExcel();
                ExcelSevice.ID = LabGroupTypeDetails[i].ID;
                ExcelSevice.GroupCode = LabGroupTypeDetails[i].GroupCode;
                ExcelSevice.GroupType = LabGroupTypeDetails[i].GroupType;
                ExcelSevice.AddedBy = LabGroupTypeDetails[i].EmpName;
                GvLabGroupType.Columns[4].Visible = false;
                GvLabGroupType.Columns[5].Visible = false;
                GvLabGroupType.Columns[6].Visible = false;
                GvLabGroupType.Columns[7].Visible = false;
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
        protected void GvLabGroupType_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GvLabGroupType.PageIndex = e.NewPageIndex;
            bindgrid();
        }

    }
}
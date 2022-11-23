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
using Mediqura.Web.MedCommon;
using System;
using Mediqura.BOL.CommonBO;
using Mediqura.CommonData.Common;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using System.Data;
using ClosedXML.Excel;
using System.Collections.Generic;
using Mediqura.CommonData.MedUtilityData;
using Mediqura.BOL.MedUtilityBO;

namespace Mediqura.Web.MedUtility
{
    public partial class ICDCodeGroupMaster :  BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblmessage.Visible = false;
                ddlbind();
            }
        }
        private void ddlbind()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddl_coderange, mstlookup.GetLookupsList(LookupName.CodeRange));
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
                if (ddl_coderange.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "Group", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";

                    ddl_coderange.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txt_icdgrouprange.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Code", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";

                    txt_icdgrouprange.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txt_title.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "SubgroupType", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";

                    txt_title.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }

                ICDCodeRangeData objlabsubgroupData = new ICDCodeRangeData();
                ICDCodeRangeBO objlabsubgroupBO = new ICDCodeRangeBO();
                objlabsubgroupData.CodeCategoryID = Convert.ToInt32(ddl_coderange.SelectedValue == "0" ? null : ddl_coderange.SelectedValue);
                objlabsubgroupData.CodeGroupRange = txt_icdgrouprange.Text == "" ? null : txt_icdgrouprange.Text;
                objlabsubgroupData.Title = txt_title.Text == "" ? null : txt_title.Text;
                objlabsubgroupData.EmployeeID = LogData.EmployeeID;
                objlabsubgroupData.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
                objlabsubgroupData.FinancialYearID = LogData.FinancialYearID;
                objlabsubgroupData.ActionType = Enumaction.Insert;
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
                        objlabsubgroupData.ActionType = Enumaction.Update;
                        objlabsubgroupData.ID = Convert.ToInt32(ViewState["ID"].ToString());
                    }
                }
                int result = objlabsubgroupBO.UpdateLabSubGroupDetails(objlabsubgroupData);
                if (result == 1 || result == 2)
                {
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
        protected void GvICDCodeGroup_RowCommand(object sender, GridViewCommandEventArgs e)
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
                    ICDCodeRangeData objlabsubgroupData = new ICDCodeRangeData();
                    ICDCodeRangeBO objlabsubgroupBO = new ICDCodeRangeBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GvICDCodeGroup.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("code");
                    objlabsubgroupData.ID = Convert.ToInt32(ID.Text);
                    objlabsubgroupData.ActionType = Enumaction.Select;

                    List<ICDCodeRangeData> GetResult = objlabsubgroupBO.GetLabSubGroupTypeDetailsByID(objlabsubgroupData);
                    if (GetResult.Count > 0)
                    {
                        ddl_coderange.SelectedValue = GetResult[0].CodeCategoryID.ToString();
                        txt_icdgrouprange.Text = GetResult[0].CodeGroupRange;
                        txt_title.Text = GetResult[0].Title;
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
                    ICDCodeRangeData objlabsubgroupData = new ICDCodeRangeData();
                    ICDCodeRangeBO objlabsubgroupBO = new ICDCodeRangeBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GvICDCodeGroup.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("code");
                    objlabsubgroupData.ID = Convert.ToInt32(ID.Text);
                    objlabsubgroupData.EmployeeID = LogData.EmployeeID;
                    objlabsubgroupData.ActionType = Enumaction.Delete;
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
                        objlabsubgroupData.Remarks = txtremarks.Text;
                    }

                    ICDCodeRangeBO objItemSubGroupTypeMasterBO1 = new ICDCodeRangeBO();
                    int Result = objItemSubGroupTypeMasterBO1.DeleteLabSubGroupTypeDetailsByID(objlabsubgroupData);
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


                List<ICDCodeRangeData> lstemp = GetLabSubGroupType(0);

                if (lstemp.Count > 0)
                {
                    GvICDCodeGroup.DataSource = lstemp;
                    GvICDCodeGroup.DataBind();
                    GvICDCodeGroup.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total: " + lstemp[0].MaximumRows.ToString() + " Record found", 1);
                    divmsg3.Visible = true;
                    divmsg3.Attributes["class"] = "SucessAlert";

                    ddlexport.Visible = true;
                    btnexport.Visible = true;
                }
                else
                {
                    GvICDCodeGroup.DataSource = null;
                    GvICDCodeGroup.DataBind();
                    GvICDCodeGroup.Visible = true;
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
        private List<ICDCodeRangeData> GetLabSubGroupType(int p)
        {
            ICDCodeRangeData objlabsubgroupData = new ICDCodeRangeData();
            ICDCodeRangeBO objlabsubgroupBO = new ICDCodeRangeBO();
            objlabsubgroupData.CodeCategoryID = Convert.ToInt32(ddl_coderange.SelectedValue == "" ? null : ddl_coderange.SelectedValue);
            objlabsubgroupData.CodeGroupRange = txt_icdgrouprange.Text == "" ? "" : txt_icdgrouprange.Text;
            objlabsubgroupData.Title = txt_title.Text == "" ? "" : txt_title.Text;
            objlabsubgroupData.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
            return objlabsubgroupBO.SearchSubGroupDetails(objlabsubgroupData);
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
            ddl_coderange.SelectedIndex = 0;
            txt_icdgrouprange.Text = "";
            txt_title.Text = "";
            ddlstatus.SelectedIndex = 0;
            GvICDCodeGroup.DataSource = null;
            GvICDCodeGroup.DataBind();
            GvICDCodeGroup.Visible = false;
            ddlexport.Visible = false;
            btnexport.Visible = false;
        }
        private void ExportToPdf()
        {
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    GvICDCodeGroup.BorderStyle = BorderStyle.None;
                    //Hide the Column containing CheckBox
                    GvICDCodeGroup.Columns[6].Visible = false;
                    GvICDCodeGroup.Columns[7].Visible = false;
                    GvICDCodeGroup.Columns[8].Visible = false;

                    GvICDCodeGroup.RenderControl(hw);
                    GvICDCodeGroup.HeaderRow.Style.Add("width", "15%");
                    GvICDCodeGroup.HeaderRow.Style.Add("font-size", "10px");
                    GvICDCodeGroup.Style.Add("text-decoration", "none");
                    GvICDCodeGroup.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    GvICDCodeGroup.Style.Add("font-size", "8px");
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 7f, 7f, 7f, 0f);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=ICDCodeGroupDetails.pdf");
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
                Response.AddHeader("content-disposition", "attachment;filename=ICDCodeGroupDetails.xlsx");
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
            List<ICDCodeRangeData> LabSubGroupTypeDetails = GetLabSubGroupType(0);
            List<ICDCodeRangeDatatoExcel> ListexcelData = new List<ICDCodeRangeDatatoExcel>();
            int i = 0;
            foreach (ICDCodeRangeData row in LabSubGroupTypeDetails)
            {
                ICDCodeRangeDatatoExcel ExcelSevice = new ICDCodeRangeDatatoExcel();
                ExcelSevice.ID = LabSubGroupTypeDetails[i].ID;
                ExcelSevice.CodeCategoryRange = LabSubGroupTypeDetails[i].CodeCategoryRange;
                ExcelSevice.CodeGroupRange = LabSubGroupTypeDetails[i].CodeGroupRange;
                ExcelSevice.Title = LabSubGroupTypeDetails[i].Title;
                ExcelSevice.AddedBy = LabSubGroupTypeDetails[i].EmpName;
                GvICDCodeGroup.Columns[5].Visible = false;
                GvICDCodeGroup.Columns[6].Visible = false;
                GvICDCodeGroup.Columns[7].Visible = false;
                GvICDCodeGroup.Columns[8].Visible = false;
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
        protected void GvICDCodeGroup_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GvICDCodeGroup.PageIndex = e.NewPageIndex;
            bindgrid();
        }
    }
}
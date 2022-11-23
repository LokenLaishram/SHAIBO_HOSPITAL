using ClosedXML.Excel;
using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedGenStoreBO;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.MedStoreData;
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
using System.Text;
using System.Drawing;
namespace Mediqura.Web.MedGenUtility
{
    public partial class GenStoreSubGroupMST : BasePage
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
            Commonfunction.PopulateDdl(Genddlgroup, mstlookup.GetLookupsList(LookupName.GenGroups));
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
                if (Genddlgroup.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "Please enter item Group.", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";

                    Genddlgroup.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (Gentxtcode.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Please enter code.", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";

                    Gentxtcode.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txtGenitemsubgrouptype.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Please enter item sub group.", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";

                    txtGenitemsubgrouptype.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }

                GenItemSubGroupData objitemsubgroupData = new GenItemSubGroupData();
                GenItemSubGroupBO objitemsubgroupBO = new GenItemSubGroupBO();
                objitemsubgroupData.GroupID = Convert.ToInt32(Genddlgroup.SelectedValue == "0" ? null : Genddlgroup.SelectedValue);
                objitemsubgroupData.Code = Gentxtcode.Text == "" ? null : Gentxtcode.Text;
                objitemsubgroupData.ItemSubGroupType = txtGenitemsubgrouptype.Text == "" ? null : txtGenitemsubgrouptype.Text;
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
                int result = objitemsubgroupBO.UpdateItemSubGroupDetails(objitemsubgroupData);
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
        protected void GvGenItemSubGroupType_RowCommand(object sender, GridViewCommandEventArgs e)
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
                    GenItemSubGroupData objItemSubGroupTypeMasterData = new GenItemSubGroupData();
                    GenItemSubGroupBO objItemSubGroupTypeMasterBO = new GenItemSubGroupBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GvGenItemSubGroupType.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("code");
                    objItemSubGroupTypeMasterData.ID = Convert.ToInt32(ID.Text);
                    objItemSubGroupTypeMasterData.ActionType = Enumaction.Select;

                    List<GenItemSubGroupData> GetResult = objItemSubGroupTypeMasterBO.GetItemSubGroupTypeDetailsByID(objItemSubGroupTypeMasterData);
                    if (GetResult.Count > 0)
                    {
                        Genddlgroup.SelectedValue = GetResult[0].GroupID.ToString();
                        Gentxtcode.Text = GetResult[0].Code;
                        txtGenitemsubgrouptype.Text = GetResult[0].ItemSubGroupType;
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
                    GenItemSubGroupData objItemSubGroupTypeMasterData = new GenItemSubGroupData();
                    GenItemSubGroupBO objItemTypeMasterBO = new GenItemSubGroupBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GvGenItemSubGroupType.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("code");
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

                    GenItemSubGroupBO objItemSubGroupTypeMasterBO1 = new GenItemSubGroupBO();
                    int Result = objItemSubGroupTypeMasterBO1.DeleteItemSubGroupTypeDetailsByID(objItemSubGroupTypeMasterData);
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


                List<GenItemSubGroupData> lstemp = GetItemSubGroupType(0);

                if (lstemp.Count > 0)
                {
                    GvGenItemSubGroupType.DataSource = lstemp;
                    GvGenItemSubGroupType.DataBind();
                    GvGenItemSubGroupType.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total: " + lstemp[0].MaximumRows.ToString() + " Record(s) found", 1);
                    divmsg3.Visible = true;
                    divmsg3.Attributes["class"] = "SucessAlert";
                    ddlexport.Visible = true;
                    btnexport.Visible = true;
                }
                else
                {
                    GvGenItemSubGroupType.DataSource = null;
                    GvGenItemSubGroupType.DataBind();
                    GvGenItemSubGroupType.Visible = true;
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
        private List<GenItemSubGroupData> GetItemSubGroupType(int p)
        {
            GenItemSubGroupData objItemSubGroupTypeMasterData = new GenItemSubGroupData();
            GenItemSubGroupBO objDepartmentTypeMasterBO = new GenItemSubGroupBO();
            objItemSubGroupTypeMasterData.GroupID = Convert.ToInt32(Genddlgroup.SelectedValue == "" ? null : Genddlgroup.SelectedValue);
            objItemSubGroupTypeMasterData.Code = Gentxtcode.Text == "" ? "" : Gentxtcode.Text;
            objItemSubGroupTypeMasterData.ItemSubGroupType = txtGenitemsubgrouptype.Text == "" ? "" : txtGenitemsubgrouptype.Text;
            objItemSubGroupTypeMasterData.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
            return objDepartmentTypeMasterBO.SearchItemTypeDetails(objItemSubGroupTypeMasterData);
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
        protected void GvGenItemSubGroupType_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GvGenItemSubGroupType.PageIndex = e.NewPageIndex;
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
            Genddlgroup.SelectedIndex = 0;
            ddlstatus.SelectedIndex = 0;
            Gentxtcode.Text = "";
            txtGenitemsubgrouptype.Text = "";
            GvGenItemSubGroupType.DataSource = null;
            GvGenItemSubGroupType.DataBind();
            GvGenItemSubGroupType.Visible = false;
            ddlexport.Visible = false;
            btnexport.Visible = false;
        }
        private void ExportToPdf()
        {
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    GvGenItemSubGroupType.BorderStyle = BorderStyle.None;
                    //Hide the Column containing CheckBox
                    GvGenItemSubGroupType.Columns[5].Visible = false;
                    GvGenItemSubGroupType.Columns[6].Visible = false;
                    GvGenItemSubGroupType.Columns[7].Visible = false;
                    GvGenItemSubGroupType.Columns[8].Visible = false;

                    GvGenItemSubGroupType.RenderControl(hw);
                    GvGenItemSubGroupType.HeaderRow.Style.Add("width", "15%");
                    GvGenItemSubGroupType.HeaderRow.Style.Add("font-size", "10px");
                    GvGenItemSubGroupType.Style.Add("text-decoration", "none");
                    GvGenItemSubGroupType.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    GvGenItemSubGroupType.Style.Add("font-size", "8px");
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 7f, 7f, 7f, 0f);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=StoreItemSubGroupDetails.pdf");
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
                Response.AddHeader("content-disposition", "attachment;filename=StoreItemSubGroupDetails.xlsx");
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
            List<GenItemSubGroupData> ItemSubGroupTypeDetails = GetItemSubGroupType(0);
            List<GenItemSubGroupDatatoExcel> ListexcelData = new List<GenItemSubGroupDatatoExcel>();
            int i = 0;
            foreach (GenItemSubGroupData row in ItemSubGroupTypeDetails)
            {
                GenItemSubGroupDatatoExcel ExcelSevice = new GenItemSubGroupDatatoExcel();
                ExcelSevice.ID = ItemSubGroupTypeDetails[i].ID;
                ExcelSevice.ItemGroupType = ItemSubGroupTypeDetails[i].ItemGroupType;
                ExcelSevice.Code = ItemSubGroupTypeDetails[i].Code;
                ExcelSevice.ItemSubGroupType = ItemSubGroupTypeDetails[i].ItemSubGroupType;
                ExcelSevice.AddedBy = ItemSubGroupTypeDetails[i].EmpName;
                GvGenItemSubGroupType.Columns[5].Visible = false;
                GvGenItemSubGroupType.Columns[6].Visible = false;
                GvGenItemSubGroupType.Columns[7].Visible = false;
                GvGenItemSubGroupType.Columns[8].Visible = false;
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
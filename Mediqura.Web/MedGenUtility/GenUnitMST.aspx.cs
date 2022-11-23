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
using Mediqura.CommonData.MedStore;
using Mediqura.BOL.MedGenStoreBO;

namespace Mediqura.Web.MedGenUtility
{
    public partial class GenUnitMST : BasePage
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
                if (txt_code.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Code", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";

                    txt_code.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txt_description.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Description", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";

                    txt_description.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }

                GenUnitMasterData objData = new GenUnitMasterData();
                GenUnitMasterBO objMasterBO = new GenUnitMasterBO();
                objData.StoreUnitCode = txt_code.Text == "" ? null : txt_code.Text;
                objData.StoreUnitdescp = txt_description.Text == "" ? null : txt_description.Text;
                objData.EmployeeID = LogData.EmployeeID;
                objData.IPaddress = LogData.IPaddress;
                objData.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
                objData.FinancialYearID = LogData.FinancialYearID;
                objData.ActionType = Enumaction.Insert;
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
                        objData.ActionType = Enumaction.Update;
                        objData.StoreUnitID = Convert.ToInt32(ViewState["ID"].ToString());
                    }
                }
                int result = objMasterBO.UpdateGenStrUnitDetails(objData);
                if (result == 1 || result == 2)
                {
                    lblmessage.Visible = true;
                    Messagealert_.ShowMessage(lblmessage, result == 1 ? "save" : "update", 1);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "SucessAlert";

                    ViewState["ID"] = null;
                    bindgrid(1);
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
        protected void GvUnitMST_RowCommand(object sender, GridViewCommandEventArgs e)
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
                    GenUnitMasterData objData = new GenUnitMasterData();
                    GenUnitMasterBO objMasterBO = new GenUnitMasterBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GvUnitMST.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("lblid");
                    objData.StoreUnitID = Convert.ToInt32(ID.Text);
                    objData.ActionType = Enumaction.Select;

                    List<GenUnitMasterData> GetResult = objMasterBO.GetGenUnitDetailsByID(objData);
                    if (GetResult.Count > 0)
                    {
                        txt_code.Text = GetResult[0].StoreUnitCode;
                        txt_description.Text = GetResult[0].StoreUnitdescp;
                        ViewState["ID"] = GetResult[0].StoreUnitID;
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
                    GenUnitMasterData objData = new GenUnitMasterData();
                    GenUnitMasterBO objMasterBO = new GenUnitMasterBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GvUnitMST.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("lblid");
                    objData.StoreUnitID = Convert.ToInt32(ID.Text);
                    objData.EmployeeID = LogData.EmployeeID;
                    objData.ActionType = Enumaction.Delete;
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
                        objData.Remarks = txtremarks.Text;
                    }

                    GenUnitMasterBO objMasterBO1 = new GenUnitMasterBO();
                    int Result = objMasterBO1.DeleteStrUnitTypeDetailsByID(objData);
                    if (Result == 1)
                    {
                        Messagealert_.ShowMessage(lblmessage, "delete", 1);
                        divmsg1.Visible = true;
                        divmsg1.Attributes["class"] = "SucessAlert";

                        bindgrid(1);
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
        private void bindgrid(int page)
        {
            try
            {

                List<GenUnitMasterData> lstemp = GetStrUnitType(page);

                if (lstemp.Count > 0)
                {
                    GvUnitMST.VirtualItemCount = lstemp[0].MaximumRows;//total item is required for custom paging
                    GvUnitMST.PageIndex = page - 1;
                    GvUnitMST.DataSource = lstemp;
                    GvUnitMST.DataBind();
                    GvUnitMST.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total: " + lstemp[0].MaximumRows.ToString() + " Record(s) found", 1);
                    divmsg3.Visible = true;
                    divmsg3.Attributes["class"] = "SucessAlert";
                    ddlexport.Visible = true;
                    btnexport.Visible = true;

                }
                else
                {
                    GvUnitMST.DataSource = null;
                    GvUnitMST.DataBind();
                    GvUnitMST.Visible = true;
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
        private List<GenUnitMasterData> GetStrUnitType(int p)
        {
            GenUnitMasterData objData = new GenUnitMasterData();
            GenUnitMasterBO objMasterBO = new GenUnitMasterBO();
            objData.StoreUnitCode = txt_code.Text == "" ? "" : txt_code.Text;
            objData.StoreUnitdescp = txt_description.Text == "" ? "" : txt_description.Text;
            objData.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
            objData.CurrentIndex = p;
            return objMasterBO.GetStrUnitDetails(objData);
        }
        private List<GenUnitMasterData> GetStrUnitTypeDetails(int p)
        {
            GenUnitMasterData objData = new GenUnitMasterData();
            GenUnitMasterBO objMasterBO = new GenUnitMasterBO();
            objData.StoreUnitCode = txt_code.Text == "" ? "" : txt_code.Text;
            objData.StoreUnitdescp = txt_description.Text == "" ? "" : txt_description.Text;
            objData.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
            return objMasterBO.GetStrUnitTypeDetails(objData);
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

            bindgrid(1);

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
            txt_code.Text = "";
            txt_description.Text = "";
            ddlstatus.SelectedIndex = 0;
            GvUnitMST.DataSource = null;
            GvUnitMST.DataBind();
            GvUnitMST.Visible = false;
            ddlexport.Visible = false;
            btnexport.Visible = false;

        }
        private void ExportToPdf()
        {
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    GvUnitMST.AllowPaging = false;
                    GvUnitMST.DataSource = GetStrUnitTypeDetails(0);
                    GvUnitMST.DataBind();
                    GvUnitMST.BorderStyle = BorderStyle.None;
                    //Hide the Column containing CheckBox
                    GvUnitMST.Columns[4].Visible = false;
                    GvUnitMST.Columns[5].Visible = false;
                    GvUnitMST.Columns[6].Visible = false;
                    GvUnitMST.Columns[7].Visible = false;

                    GvUnitMST.RenderControl(hw);
                    GvUnitMST.HeaderRow.Style.Add("width", "15%");
                    GvUnitMST.HeaderRow.Style.Add("font-size", "10px");
                    GvUnitMST.Style.Add("text-decoration", "none");
                    GvUnitMST.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    GvUnitMST.Style.Add("font-size", "8px");
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 7f, 7f, 7f, 0f);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=GenStrUnitDetails.pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.Write(pdfDoc);
                    Response.End();
                }
            }
        }
        protected void GvUnitMST_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            bindgrid(Convert.ToInt32(e.NewPageIndex + 1));
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
                Response.AddHeader("content-disposition", "attachment;filename=GenStrUnitDetails.xlsx");
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
            List<GenUnitMasterData> StrTypeDetails = GetStrUnitTypeDetails(0);
            List<StrUnitDatatoExcel> ListexcelData = new List<StrUnitDatatoExcel>();
            int i = 0;
            foreach (GenUnitMasterData row in StrTypeDetails)
            {
                StrUnitDatatoExcel ExcelSevice = new StrUnitDatatoExcel();
                ExcelSevice.StoreUnitID = StrTypeDetails[i].StoreUnitID;
                ExcelSevice.StoreUnitCode = StrTypeDetails[i].StoreUnitCode;
                ExcelSevice.StoreUnitdescp = StrTypeDetails[i].StoreUnitdescp;
                ExcelSevice.AddedBy = StrTypeDetails[i].EmpName;
                GvUnitMST.Columns[4].Visible = false;
                GvUnitMST.Columns[5].Visible = false;
                GvUnitMST.Columns[6].Visible = false;
                GvUnitMST.Columns[7].Visible = false;
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
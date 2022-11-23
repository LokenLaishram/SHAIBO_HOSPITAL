using Mediqura.BOL.MedUtilityBO;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.MedUtilityData;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using Mediqura.Web.MedCommon;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using System.Text;
using System.Drawing;
using System.Data;
using ClosedXML.Excel;
using System.Reflection;
using Mediqura.BOL.CommonBO;

namespace Mediqura.Web.MedUtility
{
    public partial class GuestDoctorMST : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlbind();
            }
        }
        private void ddlbind()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddldepartment, mstlookup.GetLookupsList(LookupName.Department));
        }
        private void bindgrid()
        {
            try
            {

                List<GuestDocData> lstemp = GetGuestDoc(0);
                if (lstemp.Count > 0)
                {
                    GvGuestDoc.DataSource = lstemp;
                    GvGuestDoc.DataBind();
                    GvGuestDoc.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total: " + lstemp[0].MaximumRows.ToString() + " Record found", 1);
                    divmsg3.Attributes["class"] = "SucessAlert";
                    divmsg3.Visible = true;
                    ddlexport.Visible = true;
                    btnexport.Visible = true;
                }
                else
                {
                    GvGuestDoc.DataSource = null;
                    GvGuestDoc.DataBind();
                    GvGuestDoc.Visible = true;
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
        private List<GuestDocData> GetGuestDoc(int p)
        {
            GuestDocData objOTTheaterMasterData = new GuestDocData();
            GuestDocBO objOTTheaterMasterBO = new GuestDocBO();
            objOTTheaterMasterData.Code = txt_GuestDoccode.Text == "" ? "" : txt_GuestDoccode.Text;
            objOTTheaterMasterData.Name = txt_GuestDocDescription.Text == "" ? "" : txt_GuestDocDescription.Text;
            objOTTheaterMasterData.DeptID = Convert.ToInt32(ddldepartment.SelectedValue == "" ? "0" : ddldepartment.SelectedValue);
            objOTTheaterMasterData.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
            return objOTTheaterMasterBO.SearchGuestDocDetails(objOTTheaterMasterData);
        }
        protected void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                if (LogData.SaveEnable == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "SaveEnable", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txt_GuestDoccode.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Code", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    txt_GuestDoccode.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txt_GuestDocDescription.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Description", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    txt_GuestDocDescription.Focus();
                    return;
                }
                {
                    lblmessage.Visible = false;
                }
                if (ddldepartment.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "Department", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    txt_GuestDocDescription.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }

                GuestDocData objOTTheaterMasterData = new GuestDocData();
                GuestDocBO objOTTheaterMasterBO = new GuestDocBO();
                objOTTheaterMasterData.Code = txt_GuestDoccode.Text == "" ? null : txt_GuestDoccode.Text;
                objOTTheaterMasterData.Name = txt_GuestDocDescription.Text == "" ? null : txt_GuestDocDescription.Text;
                objOTTheaterMasterData.DeptID = Convert.ToInt32(ddldepartment.SelectedValue == "" ? "0" : ddldepartment.SelectedValue);
                objOTTheaterMasterData.Deptname =(ddldepartment.SelectedItem.Text == "" ? "0" : ddldepartment.SelectedItem.Text);
                objOTTheaterMasterData.Address = txtadress.Text == "" ? null : txtadress.Text;
                objOTTheaterMasterData.ContactNo = txtcontact.Text == "" ? null : txtcontact.Text;
                objOTTheaterMasterData.EmployeeID = LogData.EmployeeID;
                objOTTheaterMasterData.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
                objOTTheaterMasterData.HospitalID = LogData.HospitalID;
                objOTTheaterMasterData.FinancialYearID = LogData.FinancialYearID;
                objOTTheaterMasterData.ActionType = Enumaction.Insert;
                if (ViewState["ID"] != null)
                {
                    if (LogData.UpdateEnable == 0)
                    {
                        Messagealert_.ShowMessage(lblmessage, "UpdateEnable", 0);
                        div1.Visible = true;
                        div1.Attributes["class"] = "FailAlert";
                        txt_GuestDoccode.Focus();
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                    }
                    objOTTheaterMasterData.ActionType = Enumaction.Update;
                    objOTTheaterMasterData.ID = Convert.ToInt32(ViewState["ID"].ToString());
                }
                int result = objOTTheaterMasterBO.UpdateGuestDocDetails(objOTTheaterMasterData);  // funtion at DAL
                if (result == 1 || result == 2)
                {
                    lblmessage.Visible = true;
                    Messagealert_.ShowMessage(lblmessage, result == 1 ? "save" : "update", 1);
                    div1.Visible = true;
                    div1.Attributes["class"] = "SucessAlert";
                    ViewState["ID"] = null;
                    bindgrid();
                }
                else if (result == 5)
                {
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    Messagealert_.ShowMessage(lblmessage, "duplicate", 0);
                }
                else
                    Messagealert_.ShowMessage(lblmessage, "system", 0);
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
            }
        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            if (LogData.SearchEnable == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "SearchEnable", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
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
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddldepartment, mstlookup.GetLookupsList(LookupName.Department));
        }
        private void clearall()
        {
            txt_GuestDoccode.Text = "";
            txt_GuestDocDescription.Text = "";
            txtadress.Text = "";
            txtcontact.Text = "";
            ddldepartment.SelectedIndex = 0;
            Commonfunction.Insertzeroitemindex(ddldepartment);
            GvGuestDoc.DataSource = null;
            GvGuestDoc.DataBind();
            GvGuestDoc.Visible = false;
            ddlexport.Visible = false;
            btnexport.Visible = false;
        }
        protected void btnexport_Click(object sender, EventArgs e)
        {
            if (LogData.ExportEnable == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "ExportEnable", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
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
        private void ExportToPdf()
        {
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    GvGuestDoc.BorderStyle = BorderStyle.None;
                    //Hide the Column containing CheckBox
                    GvGuestDoc.Columns[4].Visible = false;
                    GvGuestDoc.Columns[5].Visible = false;
                    GvGuestDoc.Columns[6].Visible = false;
                    GvGuestDoc.Columns[7].Visible = false;

                    GvGuestDoc.RenderControl(hw);
                    GvGuestDoc.HeaderRow.Style.Add("width", "15%");
                    GvGuestDoc.HeaderRow.Style.Add("font-size", "10px");
                    GvGuestDoc.Style.Add("text-decoration", "none");
                    GvGuestDoc.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    GvGuestDoc.Style.Add("font-size", "8px");
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 7f, 7f, 7f, 0f);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=GuestDoctorDetails.pdf");
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
                wb.Worksheets.Add(dt, "Patient Type Detail List");

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=GuestDoctorDetails.xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
        }
        private DataTable GetDatafromDatabase()
        {
            List<GuestDocData> OTTheaterDetails = GetGuestDoc(0);
            List<GuestDocDatatoExcel> ListexcelData = new List<GuestDocDatatoExcel>();
            int i = 0;
            foreach (GuestDocData row in OTTheaterDetails)
            {
                GuestDocDatatoExcel ExcelSevice = new GuestDocDatatoExcel();
                ExcelSevice.Code = OTTheaterDetails[i].Code;
                ExcelSevice.Name = OTTheaterDetails[i].Name;
                ExcelSevice.Deptname = OTTheaterDetails[i].Deptname;
                ExcelSevice.Address = OTTheaterDetails[i].Address;
                ExcelSevice.ContactNo = OTTheaterDetails[i].ContactNo;
                ExcelSevice.AddedBy = OTTheaterDetails[i].EmpName;
                GvGuestDoc.Columns[4].Visible = false;
                GvGuestDoc.Columns[5].Visible = false;
                GvGuestDoc.Columns[6].Visible = false;
                GvGuestDoc.Columns[7].Visible = false;
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
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        protected void GvGuestDoc_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GvGuestDoc.PageIndex = e.NewPageIndex;
            bindgrid();
        }

        protected void GvGuestDoc_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Edits")
                {
                    if (LogData.EditEnable == 0)
                    {
                        Messagealert_.ShowMessage(lblmessage, "EditEnable", 0);
                        div1.Visible = true;
                        div1.Attributes["class"] = "FailAlert";
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                    }
                    GuestDocData objOTTheaterMasterData = new GuestDocData();
                    GuestDocBO objOTTheaterMasterBO = new GuestDocBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow pt = GvGuestDoc.Rows[i];
                    Label ID = (Label)pt.Cells[0].FindControl("code");
                    objOTTheaterMasterData.ID = Convert.ToInt32(ID.Text);
                    objOTTheaterMasterData.ActionType = Enumaction.Select;

                    List<GuestDocData> GetResult = objOTTheaterMasterBO.GetGuestDocDetailsByID(objOTTheaterMasterData);
                    if (GetResult.Count > 0)
                    {
                        txt_GuestDoccode.Text = GetResult[0].Code;
                        txt_GuestDocDescription.Text = GetResult[0].Name;
                        txtadress.Text = GetResult[0].Code;
                        txtcontact.Text = GetResult[0].Name;
                        //ddldepartment.SelectedItem.Text = GetResult[0].Specialization;
                        ddldepartment.SelectedIndex = GetResult[0].DepartmentID;
                        txtadress.Text = GetResult[0].Address;
                        txtcontact.Text = GetResult[0].ContactNo;
                        ViewState["ID"] = GetResult[0].ID;
                    }
                }
                if (e.CommandName == "Deletes")
                {
                    if (LogData.DeleteEnable == 0)
                    {
                        Messagealert_.ShowMessage(lblmessage, "DeleteEnable", 0);
                        div1.Visible = true;
                        div1.Attributes["class"] = "FailAlert";
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                    }
                    GuestDocData objOTTheaterMasterData = new GuestDocData();
                    GuestDocBO objOTTheaterMasterBO = new GuestDocBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GvGuestDoc.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("code");
                    objOTTheaterMasterData.ID = Convert.ToInt32(ID.Text);
                    objOTTheaterMasterData.EmployeeID = LogData.EmployeeID;
                    objOTTheaterMasterData.ActionType = Enumaction.Delete;
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
                        objOTTheaterMasterData.Remarks = txtremarks.Text;
                    }

                    GuestDocBO objOTtheaterMasterBO1 = new GuestDocBO();
                    int Result = objOTtheaterMasterBO1.DeleteGuestDocDetailsByID(objOTTheaterMasterData);
                    if (Result == 1)
                    {
                        Messagealert_.ShowMessage(lblmessage, "delete", 1);
                        div1.Visible = true;
                        div1.Attributes["class"] = "SucessAlert";
                        bindgrid();
                    }
                    else
                    {
                        Messagealert_.ShowMessage(lblmessage, "system", 0);
                        div1.Visible = true;
                        div1.Attributes["class"] = "FailAlert";

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
    }
}
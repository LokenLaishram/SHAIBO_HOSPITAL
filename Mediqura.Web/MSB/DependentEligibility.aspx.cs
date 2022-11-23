using ClosedXML.Excel;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MSBBO;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.MSBData;
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

namespace Mediqura.Web.MSB
{
    public partial class Dependent_Eligibility : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlbind();
            }
        }
        public void ddlbind() {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddl_maritalstatus, mstlookup.GetLookupsList(LookupName.Marital));
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
                if (ddl_maritalstatus.SelectedIndex == 0)
                {

                    Messagealert_.ShowMessage(lblmessage, "MaritalStatus", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    ddl_maritalstatus.Focus();
                    return;

                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txt_relationship.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "RelationShip", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    txt_relationship.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txt_age.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Age", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    txt_age.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                DepandentData objdata = new DepandentData();
                DependentBO objstdBO = new DependentBO();
                objdata.MaritalStatusID = Convert.ToInt32(ddl_maritalstatus.SelectedValue == "0" ? null : ddl_maritalstatus.SelectedValue);
                objdata.Relation = txt_relationship.Text == "" ? "" : txt_relationship.Text.Trim();
                objdata.Age = Convert.ToInt32(txt_age.Text == "" ? "0" : txt_age.Text.Trim());
                objdata.EmployeeID = LogData.EmployeeID;
                objdata.HospitalID = LogData.HospitalID;
                objdata.FinancialYearID = LogData.FinancialYearID;
                objdata.ActionType = Enumaction.Insert;
                if (ViewState["ID"] != null)
                {
                    if (LogData.UpdateEnable == 0)
                    {
                        Messagealert_.ShowMessage(lblmessage, "UpdateEnable", 0);
                        div1.Visible = true;
                        div1.Attributes["class"] = "FailAlert";

                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                    }

                    objdata.ActionType = Enumaction.Update;
                    objdata.ID = Convert.ToInt32(ViewState["ID"].ToString() == "" ? "0" : ViewState["ID"].ToString());
                }
                else
                {
                    objdata.ID = 0;
                }
                int result = objstdBO.UpdateDependentEligibility(objdata);
                if (result == 1 || result == 2)
                {
                    Messagealert_.ShowMessage(lblmessage, result == 1 ? "save" : "update", 1);
                    div1.Visible = true;
                    div1.Attributes["class"] = "SucessAlert";
                    //reset();

                }
                else
                {
                    Messagealert_.ShowMessage(lblmessage, "system", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                string msg = ex.ToString();
                Messagealert_.ShowMessage(lblmessage, msg, 0);
            }

        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            bindgrid();
        }

        protected void btnresets_Click(object sender, EventArgs e)
        {
            reset();
        }
        protected void reset()
        {
            ddl_maritalstatus.SelectedIndex = 0;
            txt_relationship.Text = "";
            txt_age.Text = "";
            ViewState["ID"] = null;
            bindgrid();
        }
        protected void bindgrid()
        {
            try
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
                List<DepandentData> DependentList = GetDependentData(0);
                if (DependentList.Count > 0)
                {
                    Gvdependent.DataSource = DependentList;
                    Gvdependent.DataBind();
                    Gvdependent.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total:" + DependentList[0].MaximumRows.ToString() + " Record(s) found", 1);
                    divmsg3.Attributes["class"] = "SucessAlert";
                    divmsg3.Visible = true;
                    ddlexport.Visible = true;
                    btnexport.Visible = true;
                    lblresult.Visible = true;
                    div1.Visible = false;


                }
                else
                {

                    divmsg3.Visible = false;
                    Gvdependent.DataSource = null;
                    Gvdependent.DataBind();
                    Gvdependent.Visible = true;
                    ddlexport.Visible = false;
                    btnexport.Visible = false;
                    divmsg3.Visible = false;
                    lblresult.Visible = false;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
            }
        }
        protected void Gvdependent_RowCommand(object sender, GridViewCommandEventArgs e)
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
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = Gvdependent.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("lblID");
                    Int32 DependentID = Convert.ToInt32(ID.Text);
                    ViewState["ID"] = DependentID;
                    Label lblMaritalID = (Label)gr.Cells[0].FindControl("lblMaritalID");
                    Label lbl_relationship = (Label)gr.Cells[0].FindControl("lbl_relationship");
                    Label lbl_age = (Label)gr.Cells[0].FindControl("lbl_age");

                    ddl_maritalstatus.SelectedValue = lblMaritalID.Text.ToString();
                    txt_relationship.Text = lbl_relationship.Text;
                    txt_age.Text = lbl_age.Text;
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

                    DepandentData objdata = new DepandentData();
                    DependentBO objstdBO = new DependentBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = Gvdependent.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("lblID");
                    TextBox txtremarks = (TextBox)gr.Cells[0].FindControl("txtremarks");
                    txtremarks.Enabled = true;
                    if (txtremarks.Text == "")
                    {
                        Messagealert_.ShowMessage(lblresult, "Remarks", 0);
                        div1.Attributes["class"] = "FailAlert";
                        div1.Visible = true;
                        txtremarks.Focus();
                        return;
                    }
                    else
                    {
                        objdata.Remarks = txtremarks.Text;
                    }
                    objdata.ID = Convert.ToInt32(ID.Text);
                    objdata.EmployeeID = LogData.UserLoginId;
                    objdata.HospitalID = LogData.HospitalID;
                    objdata.IPaddress = LogData.IPaddress;
                    int Result = objstdBO.DeleteDependentEligibilityByID(objdata);
                    if (Result == 1)
                    {
                        Messagealert_.ShowMessage(lblmessage, "delete", 1);
                        div1.Attributes["class"] = "SucessAlert";
                        div1.Visible = true;
                        bindgrid();
                    }
                    else
                    {
                        Messagealert_.ShowMessage(lblmessage, "system", 0);
                        div1.Attributes["class"] = "FailAlert";
                        div1.Visible = true;
                    }

                }

            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
                div1.Attributes["class"] = "FailAlert";
                div1.Visible = true;
            }
        }
        public List<DepandentData> GetDependentData(int curIndex)
        {

            DepandentData objdata = new DepandentData();
            DependentBO objstdBO = new DependentBO();
            objdata.MaritalStatusID = Convert.ToInt32(ddl_maritalstatus.SelectedValue == "0" ? null : ddl_maritalstatus.SelectedValue);
            objdata.Relation = txt_relationship.Text == "" ? "" : txt_relationship.Text.Trim();
            objdata.Age = Convert.ToInt32(txt_age.Text == "" ? "0" : txt_age.Text.Trim());

            return objstdBO.GetDependentEligibilityList(objdata);

        }

        public void ExportToPdf()
        {
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    Gvdependent.BorderStyle = BorderStyle.None;
                    //Hide the Column containing CheckBox
                    Gvdependent.Columns[5].Visible = false;
                    Gvdependent.Columns[6].Visible = false;
                    Gvdependent.Columns[7].Visible = false;

                    Gvdependent.RenderControl(hw);
                    Gvdependent.HeaderRow.Style.Add("width", "15%");
                    Gvdependent.HeaderRow.Style.Add("font-size", "10px");
                    Gvdependent.Style.Add("text-decoration", "none");
                    Gvdependent.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    Gvdependent.Style.Add("font-size", "8px");
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                    pdfDoc.SetPageSize(iTextSharp.text.PageSize.A4.Rotate());
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=Dependent Eligibility.pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.Write(pdfDoc);
                    Response.End();
                    Messagealert_.ShowMessage(lblresult, "Exported", 1);
                    divmsg3.Attributes["class"] = "SucessAlert";
                }
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        protected void ExportoExcel()
        {

            DataTable dt = GetDatafromDatabase();
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "Dependent Eligibility");

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=Dependent Eligibility.xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                    ddlexport.SelectedIndex = 0;
                }
                Messagealert_.ShowMessage(lblresult, "Exported", 1);
                divmsg3.Attributes["class"] = "SucessAlert";
            }
        }
        protected DataTable GetDatafromDatabase()
        {
            List<DepandentData> DepData = GetDependentData(0);
            List<DepandentDataExcel> ListexcelData = new List<DepandentDataExcel>();
            int i = 0;
            foreach (DepandentData row in DepData)
            {
                DepandentDataExcel Ecxeclpat = new DepandentDataExcel();
                Ecxeclpat.ID = DepData[i].ID;
                Ecxeclpat.Relation = DepData[i].Relation;
                Ecxeclpat.MaritalStatus = DepData[i].MaritalStatus;
                Ecxeclpat.Age = DepData[i].Age;

                ListexcelData.Add(Ecxeclpat);
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
                Messagealert_.ShowMessage(lblmessage, "ExportType", 0);
                div1.Attributes["class"] = "FailAlert";
                ddlexport.Focus();
                return;
            }
        }
    }
}
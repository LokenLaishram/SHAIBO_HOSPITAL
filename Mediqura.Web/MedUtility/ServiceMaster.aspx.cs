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
    public partial class ServiceMaster : BasePage
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
            Commonfunction.PopulateDdl(ddlservicetype, mstlookup.GetLookupsList(LookupName.CommonGroups));
            Commonfunction.Insertzeroitemindex(ddl_subservicetype);

        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetServiceName(string prefixText, int count, string contextKey)
        {
            ServicesData objservice = new ServicesData();
            ServiceBO objServiceBO = new ServiceBO();
            List<ServicesData> getResult = new List<ServicesData>();
            objservice.ServiceName = prefixText;
            objservice.SubServiceTypeID = Convert.ToInt32(contextKey);
            getResult = objServiceBO.GetServiceName(objservice);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].ServiceName.ToString());
            }
            return list;
        }

        protected void ddlservicetype_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlservicetype.SelectedIndex > 0)
            {
                MasterLookupBO mstlookup = new MasterLookupBO();
                Commonfunction.PopulateDdl(ddl_subservicetype, mstlookup.GetSubServiceTypeByGroupID(Convert.ToInt32(ddlservicetype.SelectedValue)));
            }
        }

        protected void ddlsubservicetype_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_subservicetype.SelectedIndex > 0)
            {
                txtservice.ReadOnly = false;
                AutoCompleteExtender2.ContextKey = ddl_subservicetype.SelectedValue;
            }
            else
            {
                AutoCompleteExtender2.ContextKey = null;
                txtservice.ReadOnly = true;
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
                if (ddlservicetype.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "ServiceType", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";

                    ddlservicetype.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                } if (ddl_subservicetype.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "Please select sub-service type", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";

                    ddl_subservicetype.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txtservice.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Service", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";

                    txtservice.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txtservicecharge.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Charge", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";

                    txtservicecharge.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }

                ServicesData objservice = new ServicesData();
                ServiceBO objServiceBO = new ServiceBO();
                //objservice.Code = txtcode.Text == "" ? null : txtcode.Text;
                objservice.ServiceName = txtservice.Text == "" ? null : txtservice.Text;
                objservice.ServiceCharge = Convert.ToDecimal(txtservicecharge.Text == "" ? "0.00" : txtservicecharge.Text);
                objservice.ServiceTypeID = Convert.ToInt32(ddlservicetype.SelectedValue == "" ? "0" : ddlservicetype.SelectedValue);
                objservice.SubServiceTypeID = Convert.ToInt32(ddl_subservicetype.SelectedValue == "" ? "0" : ddl_subservicetype.SelectedValue);

                objservice.EmployeeID = LogData.EmployeeID;
                objservice.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
                objservice.HospitalID = LogData.HospitalID;
                objservice.FinancialYearID = LogData.FinancialYearID;
                objservice.ActionType = Enumaction.Insert;
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
                        objservice.ActionType = Enumaction.Update;
                        objservice.ID = Convert.ToInt32(ViewState["ID"].ToString());
                    }
                }
                int result = objServiceBO.UpdateServiceDetails(objservice);
                if (result == 1 || result == 2)
                {
                    lblmessage.Visible = true;
                    Messagealert_.ShowMessage(lblmessage, result == 1 ? "save" : "update", 1);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "SucessAlert";
                    txtservice.Text = "";
                    txtservicecharge.Text = "";
                    txtservice.Focus();
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
        protected void Gvservice_RowCommand(object sender, GridViewCommandEventArgs e)
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
                    ServicesData objservice = new ServicesData();
                    ServiceBO objServiceBO = new ServiceBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = Gvservice.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("lblserviceID");
                    objservice.ID = Convert.ToInt32(ID.Text);
                    objservice.ActionType = Enumaction.Select;

                    List<ServicesData> GetResult = objServiceBO.GetServiceDetailsByID(objservice);
                    if (GetResult.Count > 0)
                    {
                        // txtcode.Text = GetResult[0].Code;
                        txtservice.Text = GetResult[0].ServiceName;
                        txtservicecharge.Text = Commonfunction.Getrounding(GetResult[0].ServiceCharge.ToString());
                        ddlservicetype.SelectedValue = GetResult[0].ServiceTypeID.ToString();
                        ddl_subservicetype.SelectedValue = GetResult[0].SubServiceTypeID.ToString();
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
                    ServicesData objservice = new ServicesData();
                    ServiceBO objServiceBO = new ServiceBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = Gvservice.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("lblserviceID");
                    objservice.ID = Convert.ToInt16(e.CommandArgument.ToString());
                    objservice.ID = Convert.ToInt32(ID.Text);
                    objservice.EmployeeID = LogData.EmployeeID;
                    objservice.ActionType = Enumaction.Delete;
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
                        objservice.Remarks = txtremarks.Text;
                    }

                    int Result = objServiceBO.DeleteServiceDetailsByID(objservice);
                    if (Result == 1)
                    {
                        lblmessage.Visible = true;
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
            if (ddlservicetype.SelectedIndex == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "ServiceType", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                ddlservicetype.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
            }

            bindgrid();

        }
        protected void txtservice_TextChanged(object sender, EventArgs e)
        {

        }
        private void bindgrid()
        {
            try
            {


                List<ServicesData> lstemp = Getservices(0);

                if (lstemp.Count > 0)
                {
                    Gvservice.DataSource = lstemp;
                    Gvservice.DataBind();
                    Gvservice.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total: " + lstemp[0].MaximumRows.ToString() + " Record(s) found", 1);
                    divmsg3.Visible = true;
                    divmsg3.Attributes["class"] = "SucessAlert";
                    ddlexport.Visible = true;
                    btnexport.Visible = true;

                }
                else
                {
                    Gvservice.DataSource = null;
                    Gvservice.DataBind();
                    Gvservice.Visible = true;
                    lblresult.Visible = false;
                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
            }
        }
        public List<ServicesData> Getservices(int curIndex)
        {
            ServicesData objservice = new ServicesData();
            ServiceBO objServiceBO = new ServiceBO();
            // objservice.Code = txtcode.Text == "" ? "" : txtcode.Text;
            objservice.ServiceName = txtservice.Text == "" ? "" : txtservice.Text;
            objservice.ServiceTypeID = Convert.ToInt32(ddlservicetype.SelectedValue == "" ? "0" : ddlservicetype.SelectedValue);
            objservice.SubServiceTypeID = Convert.ToInt32(ddl_subservicetype.SelectedValue == "" ? "0" : ddl_subservicetype.SelectedValue);

            objservice.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
            return objServiceBO.SearchServiceDetails(objservice);
        }
        private void clearall()
        {
            //txtcode.Text = "";
            txtservicecharge.Text = "";
            txtservice.Text = "";
            ddlservicetype.SelectedIndex = 0;
            Commonfunction.Insertzeroitemindex(ddl_subservicetype);
            ddlstatus.SelectedIndex = 0;
            Gvservice.DataSource = null;
            Gvservice.DataBind();
            Gvservice.Visible = false;
            ddlexport.Visible = false;
            btnexport.Visible = false;

        }
        protected void btnresets_Click(object sender, EventArgs e)
        {
            ViewState["ID"] = null;
            clearall();
            lblmessage.Visible = false;
            lblresult.Visible = false;
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
        protected void ExportoExcel()
        {

            DataTable dt = GetDatafromDatabase();
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "Service Detail List");

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=ServiceDetails.xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }

        }
        public void ExportToPdf()
        {
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    Gvservice.BorderStyle = BorderStyle.None;
                    //Hide the Column containing CheckBox
                    Gvservice.Columns[6].Visible = false;
                    Gvservice.Columns[7].Visible = false;
                    Gvservice.Columns[8].Visible = false;
                    Gvservice.Columns[9].Visible = false;
                    Gvservice.RenderControl(hw);
                    Gvservice.HeaderRow.Style.Add("width", "15%");
                    Gvservice.HeaderRow.Style.Add("font-size", "10px");
                    Gvservice.Style.Add("text-decoration", "none");
                    Gvservice.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    Gvservice.Style.Add("font-size", "8px");
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 7f, 7f, 7f, 0f);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=ServiceDetail.pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.Write(pdfDoc);
                    Response.End();
                }
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        protected DataTable GetDatafromDatabase()
        {
            List<ServicesData> ServiceDetails = Getservices(0);
            List<ServicesDatatoExcel> ListexcelData = new List<ServicesDatatoExcel>();
            int i = 0;
            foreach (ServicesData row in ServiceDetails)
            {
                ServicesDatatoExcel ExcelSevice = new ServicesDatatoExcel();
                ExcelSevice.ID = ServiceDetails[i].ID;
                ExcelSevice.Code = ServiceDetails[i].Code;
                ExcelSevice.ServiceName = ServiceDetails[i].ServiceName;
                ExcelSevice.ServiceCharge = Commonfunction.Getrounding(ServiceDetails[i].ServiceCharge.ToString());
                ExcelSevice.ServiceType = ServiceDetails[i].ServiceType;
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
        protected void Gvservice_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Gvservice.PageIndex = e.NewPageIndex;
            bindgrid();
        }

    }
}
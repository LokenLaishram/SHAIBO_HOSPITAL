using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedBill;
using Mediqura.BOL.PatientBO;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.MedBillData;
using Mediqura.CommonData.PatientData;
using Mediqura.Utility;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using Mediqura.Web.MedCommon;
using OnBarcode.Barcode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using ClosedXML.Excel;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Data;
using System.Reflection;
using Mediqura.CommonData.MedUtilityData;
using Mediqura.BOL.MedUtilityBO;
using Mediqura.BOL.MedBillBO;
using Mediqura.CommonData.AdmissionData;
using Mediqura.BOL.AdmissionBO;

namespace Mediqura.Web.MedUtility
{
    public partial class PackageWiseServiceMaster : BasePage
    {
        int ServID;
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
            Commonfunction.PopulateDdl(ddl_company, mstlookup.GetLookupsList(LookupName.TPAList));
            Commonfunction.PopulateDdl(ddl_package, mstlookup.GetLookupsList(LookupName.Package));

        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetLabServices(string prefixText, int count, string contextKey)
        {
            LabServiceMasterData Objpaic = new LabServiceMasterData();
            LabServiceMasterBO objInfoBO = new LabServiceMasterBO();
            List<LabServiceMasterData> getResult = new List<LabServiceMasterData>();
            Objpaic.TestName = prefixText;
            getResult = objInfoBO.GetPackageServices(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].TestName.ToString());
                
            }
            return list;
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
                if (ddl_company.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "StockType", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";

                    ddl_company.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }

                if (ddl_package.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "Please select rack.", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";

                    ddl_package.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txtservice.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Please enter code.", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    txtservice.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
              
                PackageWiseServiceData objitemsubgroupData = new PackageWiseServiceData();
                PackageWiseServiceBO objitemsubgroupBO = new PackageWiseServiceBO();
                objitemsubgroupData.CompanyID = Convert.ToInt32(ddl_company.SelectedValue == "0" ? null : ddl_company.SelectedValue);
                objitemsubgroupData.packageID = Convert.ToInt32(ddl_package.SelectedValue == "0" ? null : ddl_package.SelectedValue);
                objitemsubgroupData.ServiceID = Convert.ToInt32(txtservice.Text.Contains(":")? txtservice.Text.Substring(txtservice.Text.LastIndexOf(':') + 1):"0");
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
                int result = objitemsubgroupBO.UpdatePackageServiceDetails(objitemsubgroupData);
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
        private void bindgrid()
        {
            try
            {
                List<PackageWiseServiceData> lstemp = GetPackageService(0);
                if (lstemp.Count > 0)
                {
                    GvPackageService.DataSource = lstemp;
                    GvPackageService.DataBind();
                    GvPackageService.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total: " + lstemp[0].MaximumRows.ToString() + " Record(s) found", 1);
                    divmsg3.Visible = true;
                    divmsg3.Attributes["class"] = "SucessAlert";
                    ddlexport.Visible = true;
                    btnexport.Visible = true;
                }
                else
                {
                    GvPackageService.DataSource = null;
                    GvPackageService.DataBind();
                    GvPackageService.Visible = true;
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
        private List<PackageWiseServiceData> GetPackageService(int p)
        {
            PackageWiseServiceData objData = new PackageWiseServiceData();
            PackageWiseServiceBO objBO = new PackageWiseServiceBO();
            objData.CompanyID = Convert.ToInt32(ddl_company.SelectedValue == "" ? null : ddl_company.SelectedValue);
            objData.packageID = Convert.ToInt32(ddl_package.SelectedValue == "" ? null : ddl_package.SelectedValue);
            objData.ServiceID = Convert.ToInt32(txtservice.Text.Contains(":") ? txtservice.Text.Substring(txtservice.Text.LastIndexOf(':') + 1) : "0");
            //objData.ServiceID = Convert.ToInt32(lblservice.Text.Trim() == "" ? null : lblservice.Text.Trim());
             objData.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
            return objBO.GetPackageService(objData);
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
            ddl_package.SelectedIndex = 0;
            ddl_company.SelectedIndex = 0;
            ddlstatus.SelectedIndex = 0;
            txtservice.Text = "";
            GvPackageService.DataSource = null;
            GvPackageService.DataBind();
            GvPackageService.Visible = false;
            ddlexport.Visible = false;
            btnexport.Visible = false;
        }
        protected void GvPackageService_RowCommand(object sender, GridViewCommandEventArgs e)
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
                    PackageWiseServiceData objData = new PackageWiseServiceData();
                    PackageWiseServiceBO objBO = new PackageWiseServiceBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GvPackageService.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("lblpackServcId");
                    Label ServID = (Label)gr.Cells[0].FindControl("lblServcId");
                    objData.ID = Convert.ToInt32(ID.Text);
                    objData.ServiceID = Convert.ToInt32(ServID.Text);
                    objData.ActionType = Enumaction.Select;

                    List<PackageWiseServiceData> GetResult = objBO.GetPackageServiceDetailsByID(objData);
                    if (GetResult.Count > 0)
                    {
                        ddl_company.SelectedValue = GetResult[0].CompanyID.ToString();
                        ddl_package.SelectedValue = GetResult[0].packageID.ToString();
                        txtservice.Text = GetResult[0].ServiceName.ToString();
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
                    PackageWiseServiceData objItemSubGroupTypeMasterData = new PackageWiseServiceData();
                    PackageWiseServiceBO objItemTypeMasterBO = new PackageWiseServiceBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GvPackageService.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("lblpackServcId");
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

                    PackageWiseServiceBO objItemSubGroupTypeMasterBO1 = new PackageWiseServiceBO();
                    int Result = objItemSubGroupTypeMasterBO1.DeletePackageServiceDetailsByID(objItemSubGroupTypeMasterData);
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

        protected void GvPackageService_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GvPackageService.PageIndex = e.NewPageIndex;
            bindgrid();
        }

        private void ExportToPdf()
        {
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    GvPackageService.BorderStyle = BorderStyle.None;
                    //Hide the Column containing CheckBox
                    GvPackageService.Columns[6].Visible = false;
                    GvPackageService.Columns[7].Visible = false;
                    GvPackageService.Columns[8].Visible = false;

                    GvPackageService.RenderControl(hw);
                    GvPackageService.HeaderRow.Style.Add("width", "15%");
                    GvPackageService.HeaderRow.Style.Add("font-size", "10px");
                    GvPackageService.Style.Add("text-decoration", "none");
                    GvPackageService.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    GvPackageService.Style.Add("font-size", "8px");
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 7f, 7f, 7f, 0f);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=PackageWiseServiceDetails.pdf");
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
                wb.Worksheets.Add(dt, "Package Service List");

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=PackageWiseServiceDetails.xlsx");
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
            List<PackageWiseServiceData> TypeDetails = GetPackageService(0);
            List<PackageWiseServiceDataToExcel> ListexcelData = new List<PackageWiseServiceDataToExcel>();
            int i = 0;
            foreach (PackageWiseServiceData row in TypeDetails)
            {
                PackageWiseServiceDataToExcel ExcelSevice = new PackageWiseServiceDataToExcel();
                ExcelSevice.ID = TypeDetails[i].ID;
                ExcelSevice.CompanyName = TypeDetails[i].CompanyName;
                ExcelSevice.packageName = TypeDetails[i].packageName;
                ExcelSevice.ServiceName = TypeDetails[i].ServiceName;
                ExcelSevice.EmpName = TypeDetails[i].EmpName;
                GvPackageService.Columns[6].Visible = false;
                GvPackageService.Columns[7].Visible = false;
                GvPackageService.Columns[8].Visible = false;
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
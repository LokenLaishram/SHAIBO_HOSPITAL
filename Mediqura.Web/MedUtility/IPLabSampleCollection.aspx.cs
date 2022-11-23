using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedBill;
using Mediqura.BOL.PatientBO;
using Mediqura.CommonData.Common;
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
using Mediqura.CommonData.MedHrData;
using Mediqura.BOL.MedHrBO;
using Mediqura.CommonData.AdmissionData;
using Mediqura.BOL.AdmissionBO;

namespace Mediqura.Web.MedUtility
{
    public partial class IPLabSampleCollection1 : BasePage
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
            Commonfunction.PopulateDdl(ddltakenby, mstlookup.GetLookupsList(LookupName.Employee));

        }

        protected void txt_IPNo_TextChanged(object sender, EventArgs e)
        {
            PatientData Objpaic = new PatientData();
            RegistrationBO objInfoBO = new RegistrationBO();
            List<PatientData> getResult = new List<PatientData>();
            Objpaic.IPNo = txt_IPNo.Text.Trim() == "" ? "null" : txt_IPNo.Text.Trim();
            getResult = objInfoBO.GetPatientDetailsByIPNo(Objpaic);
            if (getResult.Count > 0)
            {
                txt_name.Text = getResult[0].PatientName.ToString();
                txt_age.Text = getResult[0].Agecount.ToString();
                txt_gender.Text = getResult[0].GenderName.ToString();
                txt_refDoc.Text = getResult[0].DoctorName.ToString();

                //Session["ServiceList"] = null;
            }
            else
            {
                txt_name.Text = "";
                txt_refDoc.Text = "";
                txt_IPNo.Focus();
            }

            bindgrid();
        }
        protected void txtipno_TextChanged(object sender, EventArgs e)
        {
            PatientData Objpaic = new PatientData();
            RegistrationBO objInfoBO = new RegistrationBO();
            List<PatientData> getResult = new List<PatientData>();
            Objpaic.IPNo = txtipno.Text.Trim() == "" ? "null" : txtipno.Text.Trim();
            getResult = objInfoBO.GetPatientDetailsByIPNo(Objpaic);
            if (getResult.Count > 0)
            {
                txtName.Text = getResult[0].PatientName.ToString();
                txtAge.Text = getResult[0].Agecount.ToString();
                txtGender.Text = getResult[0].GenderName.ToString();
                txtRefDoc.Text = getResult[0].DoctorName.ToString();

                //Session["ServiceList"] = null;
            }
            else
            {
                txt_name.Text = "";
                txt_refDoc.Text = "";
                txt_IPNo.Focus();
            }

            bindgrid1();
        }

        private void bindgrid1()
        {
            try
            {
                List<SampleCollectionData> lstemp = GetSampleCollectionList(0);
                if (lstemp.Count > 0)
                {
                    gvsamplecollectedlist.DataSource = lstemp;
                    gvsamplecollectedlist.DataBind();
                    gvsamplecollectedlist.Visible = true;
                    Messagealert_.ShowMessage(lblresult1, "Total: " + lstemp[0].MaximumRows.ToString() + " Record(s) found", 1);
                    div4.Visible = true;
                    div4.Attributes["class"] = "SucessAlert";

                    ddlexport.Visible = true;
                    btnexport.Visible = true;


                    //divexport.Visible = true;
                }
                else
                {

                    gvsamplecollectedlist.DataSource = null;
                    gvsamplecollectedlist.DataBind();
                    gvsamplecollectedlist.Visible = true;
                    lblresult1.Visible = false;
                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
            }
        }

        private List<SampleCollectionData> GetSampleCollectionList(int p)
        {
            SampleCollectionData objLabSampleCollectionData = new SampleCollectionData();
            LabSampleCollctionBO objLabSampleCollectionMasterBO = new LabSampleCollctionBO();
            objLabSampleCollectionData.IPNo = txtipno.Text.Trim() == "" ? "null" : txtipno.Text.Trim();
            return objLabSampleCollectionMasterBO.SearchIPSampleCollectedDetails(objLabSampleCollectionData);
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetIPNo(string prefixText, int count, string contextKey)
        {
            PatientData Objpaic = new PatientData();
            RegistrationBO objInfoBO = new RegistrationBO();
            List<PatientData> getResult = new List<PatientData>();
            Objpaic.IPNo = prefixText;
            getResult = objInfoBO.GetLabIPNo(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].IPNo.ToString());
            }
            return list;
        }

        private List<SampleCollectionData> GetSampleCollection(int p)
        {
            SampleCollectionData objLabSampleCollectionData = new SampleCollectionData();
            LabSampleCollctionBO objLabRangeMasterBO = new LabSampleCollctionBO();
            objLabSampleCollectionData.IPNo = txt_IPNo.Text.Trim() == "" ? "null" : txt_IPNo.Text.Trim();
            return objLabRangeMasterBO.SearchIPSampleCollectionDetails(objLabSampleCollectionData);
        }
        private void bindgrid()
        {
            try
            {
                List<SampleCollectionData> lstemp = GetSampleCollection(0);
                if (lstemp.Count > 0)
                {
                    gvSample.DataSource = lstemp;
                    gvSample.DataBind();
                    gvSample.Visible = true;
                    //Messagealert_.ShowMessage(lblresult, "Total: " + lstemp[0].MaximumRows.ToString() + " Record found", 1);
                    divtaken.Visible = true;

                    divmsg3.Attributes["class"] = "SuccessAlert";
                    divmsg3.Visible = true;



                    //divexport.Visible = true;
                }
                else
                {

                    gvSample.DataSource = null;
                    gvSample.DataBind();
                    gvSample.Visible = true;
                    lblresult.Visible = false;
                    Messagealert_.ShowMessage(lblmessage, "status", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    return;
                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";

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
            clearall();
        }
        protected void btnreset_Click(object sender, EventArgs e)
        {
            txtName.Text = "";
            txtAge.Text = "";
            txtGender.Text = "";
            txtRefDoc.Text = "";
            lblresult.Text = "";
            txtipno.Text = "";
            gvsamplecollectedlist.DataSource = null;
            gvsamplecollectedlist.DataBind();
            gvsamplecollectedlist.Visible = false;
            lblmessage1.Visible = false;
            lblresult1.Visible = false;
            divmsg3.Visible = false;
            ddlexport.Visible = false;
            btnexport.Visible = false;

        }

        protected void clearall()
        {
            txt_name.Text = "";
            txt_age.Text = "";
            txt_gender.Text = "";
            txt_refDoc.Text = "";
            lblresult.Text = "";
            txt_IPNo.Text = "";
            lblmessage.Text = "";
            gvSample.DataSource = null;
            gvSample.DataBind();
            gvSample.Visible = false;
            lblmessage.Visible = false;
            lblresult.Visible = false;
            ddltakenby.SelectedIndex = 0;
            lblresult.Visible = false;
            divmsg3.Visible = false;
            //divexport.Visible = false;
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
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

                if (ddltakenby.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "takenby", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    ddltakenby.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;

                }
                List<SampleCollectionData> Listbill = new List<SampleCollectionData>();
                LabSampleCollctionBO objLabSampleBO = new LabSampleCollctionBO();
                SampleCollectionData objSampleData = new SampleCollectionData();
                foreach (GridViewRow row in gvSample.Rows)
                {
                    Label gridSampleType = (Label)gvSample.Rows[row.RowIndex].Cells[0].FindControl("lblSampleTypeID");
                    DropDownList gridStatus = (DropDownList)gvSample.Rows[row.RowIndex].Cells[0].FindControl("ddlstatus");
                    Label testName = (Label)gvSample.Rows[row.RowIndex].Cells[0].FindControl("lbl_testname");
                    Label testcenterID = (Label)gvSample.Rows[row.RowIndex].Cells[0].FindControl("lbltestcenterid");
                    Label urgency = (Label)gvSample.Rows[row.RowIndex].Cells[0].FindControl("lbl_urgencyid");
                    Label ID = (Label)gvSample.Rows[row.RowIndex].Cells[0].FindControl("lblID");
                    Label patientname = (Label)gvSample.Rows[row.RowIndex].Cells[0].FindControl("lblname");
                    Label referal = (Label)gvSample.Rows[row.RowIndex].Cells[0].FindControl("lblReferdoc");
                    Label pattype = (Label)gvSample.Rows[row.RowIndex].Cells[0].FindControl("lblpattype");
                    Label testID = (Label)gvSample.Rows[row.RowIndex].Cells[0].FindControl("lblTestID");
                    Label OrderID = (Label)gvSample.Rows[row.RowIndex].Cells[0].FindControl("lblOrderID");

                    SampleCollectionData ObjDetails = new SampleCollectionData();

                    ObjDetails.SampleTypeID = Convert.ToInt32(gridSampleType.Text);
                    ObjDetails.StatusID = Convert.ToInt32(gridStatus.SelectedValue);
                    ObjDetails.TestName = testName.Text == "" ? null : testName.Text;
                    ObjDetails.PatientName = patientname.Text == "" ? null : patientname.Text;
                    ObjDetails.ReferalDoctor = referal.Text == "" ? null : referal.Text;
                    ObjDetails.PatientTypeID = Convert.ToInt32(pattype.Text == "" ? "0" : pattype.Text);
                    ObjDetails.TestCenterID = Convert.ToInt32(testcenterID.Text == "" ? "0" : testcenterID.Text);
                    ObjDetails.OrderID = Convert.ToInt64(OrderID.Text == "" ? "0" : OrderID.Text);
                    ObjDetails.LabServiceID = Convert.ToInt32(testID.Text == "" ? "0" : testID.Text);
                    Listbill.Add(ObjDetails);
                    if (gridStatus.SelectedIndex == 0)
                    {
                        Messagealert_.ShowMessage(lblmessage, "Please select Status.", 0);
                        div1.Visible = true;
                        div1.Attributes["class"] = "FailAlert";
                        gridSampleType.Focus();
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                        div1.Visible = false;
                    }


                }
                objSampleData.XMLData = XmlConvertor.IPSampleDatatoXML(Listbill).ToString();
                objSampleData.IPNo = txt_IPNo.Text == "" ? "null" : txt_IPNo.Text;

                objSampleData.TakenBy = Convert.ToInt32(ddltakenby.SelectedValue == "" ? "0" : ddltakenby.SelectedValue);
                objSampleData.EmployeeID = LogData.EmployeeID;
                objSampleData.FinancialYearID = LogData.FinancialYearID;
                objSampleData.EmployeeID = LogData.EmployeeID;
                objSampleData.HospitalID = LogData.HospitalID;
                objSampleData.IPaddress = LogData.IPaddress;
                objSampleData.ActionType = Enumaction.Insert;
                int result = objLabSampleBO.UpdateIPSampleCollectionDetails(objSampleData);
                if (result == 1 || result == 2)
                {
                    lblmessage.Visible = true;
                    Messagealert_.ShowMessage(lblmessage, result == 1 ? "save" : "update", 1);
                    div1.Visible = true;
                    div1.Attributes["class"] = "SucessAlert";
                    //ViewState["ID"] = null;
                    //bindgrid();
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

            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                lblmessage.Text = ExceptionMessage.GetMessage(ex);

            }
        }

        protected void gvSample_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void gvSample_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                DropDownList type = e.Row.FindControl("ddlSampleType") as DropDownList;
                MasterLookupBO mstlookup = new MasterLookupBO();
                Commonfunction.PopulateDdl(type, mstlookup.GetLookupsList(LookupName.SmpleType));


            }
        }

        protected void ddlSampleType_SelectedIndexChanged(object sender, EventArgs e)
        {

            DropDownList sample = (DropDownList)sender;
            GridViewRow gvr = (GridViewRow)sample.NamingContainer;
            Label desc = gvr.FindControl("lbl_desc") as Label;
            string sampleName = sample.SelectedValue;
            int sampleID = 0;
            SampleCollectionData Objpaic = new SampleCollectionData();
            LabSampleCollctionBO objInfoBO = new LabSampleCollctionBO();
            List<SampleCollectionData> getResult = new List<SampleCollectionData>();
            Objpaic.SampleTypeID = Convert.ToInt32(sampleName);
            getResult = objInfoBO.GetSampleDescription(Objpaic);
            if (getResult.Count > 0)
            {

                desc.Text = getResult[0].descriptions.ToString();
                //sampleID = getResult[0].LabServiceID;
                //Session["sample_ID"] = getResult[0].LabServiceID.ToString();
                //Objpaic.ItemID = Convert.ToInt32(Session["sample_ID"] == "" ? "0" : Session["sample_ID"]);

            }

        }

        protected void gvSample_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvSample.PageIndex = e.NewPageIndex;
            bindgrid();
        }
        private void ExportToPdf()
        {
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    gvsamplecollectedlist.BorderStyle = BorderStyle.None;
                    //Hide the Column containing CheckBox
                    gvsamplecollectedlist.RenderControl(hw);
                    gvsamplecollectedlist.HeaderRow.Style.Add("width", "15%");
                    gvsamplecollectedlist.HeaderRow.Style.Add("font-size", "10px");
                    gvsamplecollectedlist.Style.Add("text-decoration", "none");
                    gvsamplecollectedlist.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    gvsamplecollectedlist.Style.Add("font-size", "8px");
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 7f, 7f, 7f, 0f);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=IPLabCollectionDetailsDetails.pdf");
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
                Response.AddHeader("content-disposition", "attachment;filename=IPLabCollectionDetails.xlsx");
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
            List<SampleCollectionData> ReagentTypeDetails = GetSampleCollectionList(0);
            List<SampleCollectionDatatoExcel> ListexcelData = new List<SampleCollectionDatatoExcel>();
            int i = 0;
            foreach (SampleCollectionData row in ReagentTypeDetails)
            {
                SampleCollectionDatatoExcel ExcelSevice = new SampleCollectionDatatoExcel();
                ExcelSevice.TestName = ReagentTypeDetails[i].TestName;
                ExcelSevice.SampleType = ReagentTypeDetails[i].SampleType;
                //  ExcelSevice.Status = ReagentTypeDetails[i].Status;
                ExcelSevice.EmpName = ReagentTypeDetails[i].EmpName;
                ExcelSevice.AddedDate = ReagentTypeDetails[i].AddedDate;
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
        protected void gvsamplecollectedlist_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvsamplecollectedlist.PageIndex = e.NewPageIndex;
            bindgrid1();
        }
    }
}
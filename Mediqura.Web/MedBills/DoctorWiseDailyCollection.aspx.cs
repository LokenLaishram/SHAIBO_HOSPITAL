using ClosedXML.Excel;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedBillBO;
using Mediqura.BOL.MedUtilityBO;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.MedBillData;
using Mediqura.CommonData.MedUtilityData;
using Mediqura.Utility;
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

namespace Mediqura.Web.MedBills
{
    public partial class DoctorWiseDailyCollection : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                ddlbind();

            }
            checkSelect();
        }
        static String departmentId;
        public void checkSelect()
        {
            if (ddl_servicetype.SelectedIndex == 0)
            {
                txt_service.ReadOnly = true;
            }
            else
            {
                txt_service.ReadOnly = false;
            }
            if (ddl_doctorType.SelectedIndex == 0)
            {
                ddldepartment.Attributes["disabled"] = "disabled";
            }
            else
            {
                ddldepartment.Attributes.Remove("disabled");
            }
            if (ddldepartment.SelectedIndex == 0)
            {
                txt_doctor.ReadOnly = true;
            }
            else
            {
                txt_doctor.ReadOnly = false;
            }
        }
        private void ddlbind()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddl_servicetype, mstlookup.GetLookupsList(LookupName.ServiceType));
            Commonfunction.PopulateDdl(ddl_doctorType, mstlookup.GetLookupsList(LookupName.DoctorType));
            Commonfunction.PopulateDdl(ddldepartment, mstlookup.GetLookupsList(LookupName.Department));
            Commonfunction.PopulateDdl(ddl_month, mstlookup.GetLookupsList(LookupName.month));
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetServiceName(string prefixText, int count, string contextKey)
        {
            ServicesData ObjServiceRange = new ServicesData();
            ServiceBO objInfoBO = new ServiceBO();
            List<ServicesData> getResult = new List<ServicesData>();
            ObjServiceRange.ServiceName = prefixText;
            ObjServiceRange.ServiceTypeID = Convert.ToInt32(contextKey);
            getResult = objInfoBO.SearchServiceDetailsByType(ObjServiceRange);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].ServiceName.ToString());
            }
            return list;
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetDoctor(string prefixText, int count, string contextKey)
        {

            DoctorMasterData ObjDoctorData = new DoctorMasterData();
            DoctorMasterBO objDoctorBO = new DoctorMasterBO();
            List<DoctorMasterData> getResult = new List<DoctorMasterData>();
            ObjDoctorData.DoctorName = prefixText;
            ObjDoctorData.DoctorType = Convert.ToInt32(contextKey);
            ObjDoctorData.DepartmentID = Convert.ToInt32(departmentId);
            getResult = objDoctorBO.SearchDoctorByTypeAndDept(ObjDoctorData);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].DoctorName.ToString());
            }


            return list;
        }

        protected void ddl_servicetype_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkSelect();
            auto_service.ContextKey = ddl_servicetype.SelectedValue;
        }

        protected void ddl_doctorType_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkSelect();
            auto_doctor.ContextKey = ddl_doctorType.SelectedValue;
        }
        protected void ddldepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkSelect();
            departmentId = ddldepartment.SelectedValue;

        }
        protected void bindgrid()
        {
            try
            {

                List<DoctorWiseCollectionMasterData> commissionDetails = GetCommissionData(0);
                if (commissionDetails.Count > 0)
                {
                    GvCollectionCommission.DataSource = commissionDetails;
                    GvCollectionCommission.DataBind();
                    GvCollectionCommission.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total:" + commissionDetails[0].MaximumRows.ToString() + " Record(s) found", 1);
                    divmsg3.Attributes["class"] = "SucessAlert";
                    txtTotalServiceCharge.Text = Commonfunction.Getrounding(commissionDetails[0].TotalServiceCharge.ToString());
                    txtTotalDocCommission.Text = Commonfunction.Getrounding(commissionDetails[0].TotalCommission.ToString());
                    txtTotalHospitalCharge.Text = Commonfunction.Getrounding(commissionDetails[0].TotalHospitalCharge.ToString());
                    txtTotalDocPayable.Text = Commonfunction.Getrounding(commissionDetails[0].TotalPayable.ToString());
                 
                    divmsg3.Visible = true;
                    ddlexport.Visible = true;
                    btnexport.Visible = true;
                    if (ddl_verified.SelectedValue == "0")
                    {
                        btnVerify.Visible = true;
                    }
                    else {
                        btnVerify.Visible = false;
                    }
                }
                else
                {

                    divmsg3.Visible = false;
                    GvCollectionCommission.DataSource = null;
                    GvCollectionCommission.DataBind();
                    GvCollectionCommission.Visible = true;
                    txtTotalServiceCharge.Text = "0.00";
                    txtTotalDocCommission.Text = "0.00";
                    txtTotalHospitalCharge.Text = "0.00";
                    txtTotalDocPayable.Text = "0.00";
                    ddlexport.Visible = false;
                    btnexport.Visible = false;
                    btnVerify.Visible = false;
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
        public List<DoctorWiseCollectionMasterData> GetCommissionData(int curIndex)
        {

            DoctorWiseCollectionMasterData objCommissionData = new DoctorWiseCollectionMasterData();
            DoctorWiseDailyCollectionBO objCommissionBO = new DoctorWiseDailyCollectionBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);

            String serviceText = txt_service.Text == "" ? null : txt_service.Text.ToString().Trim();
            String serviceid = "0";
            String servicecharge = "0";
            String doctorid = "0";
           
            String doctorText = txt_doctor.Text == "" ? null : txt_doctor.Text.ToString().Trim();
            if (doctorText != null)
            {
                String[] doctor = doctorText.Split(new[] { ":" }, StringSplitOptions.None);
                doctorid = doctor[1];
            }
            if (serviceText != null)
            {
                String[] service = serviceText.Split(new[] { ":" }, StringSplitOptions.None);
                serviceid = service[1];
                String[] serviceCharge = service[0].Trim().Split(new[] { ">", "ID" }, StringSplitOptions.None);
                servicecharge = serviceCharge[1];
            }
            objCommissionData.DepartmentID = Convert.ToInt32(ddldepartment.SelectedValue == "" ? "0" : ddl_doctorType.SelectedValue); objCommissionData.DoctorID = Convert.ToInt32(doctorid);
            objCommissionData.Doctortype = Convert.ToInt32(ddl_doctorType.SelectedValue == "" ? "0" : ddl_doctorType.SelectedValue);
            objCommissionData.Servicetype = Convert.ToInt32(ddl_servicetype.SelectedValue == "" ? "0" : ddl_servicetype.SelectedValue);
            objCommissionData.ServiceID = Convert.ToInt32(serviceid);
            objCommissionData.Month = Convert.ToInt32(ddl_month.SelectedValue == "" ? "0" : ddl_month.SelectedValue);
            objCommissionData.verify = Convert.ToInt32(ddl_verified.SelectedValue == "" ? "0" : ddl_verified.SelectedValue);
            DateTime from = txtdatefrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txtdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txtto.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            string datefrom = from.ToString("yyyy-MM-dd");
            string dateto = To.ToString("yyyy-MM-dd");
            from = Convert.ToDateTime(datefrom + " " + "12:01:00 AM");
            To = Convert.ToDateTime(dateto + " " + "11:59:00 PM");
            objCommissionData.DateFrom = from;
            objCommissionData.DateTo = To;
            return objCommissionBO.GetDoctorsDailyCollectionList(objCommissionData);

        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            if (LogData.SearchEnable == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "SearchEnable", 0);
                divmsg3.Visible = true;
                divmsg3.Attributes["class"] = "FailAlert";
                return;
            }
            else
            {
                lblmessage.Visible = false;
            }
        
           
            if (ddl_doctorType.SelectedIndex == 0)
            {

                Messagealert_.ShowMessage(lblmessage, "DoctorType", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                ddl_doctorType.Focus();
                return;

            }
            if (ddldepartment.SelectedIndex == 0)
            {

                Messagealert_.ShowMessage(lblmessage, "Department", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                ddl_servicetype.Focus();
                return;

            }
            else
            {
                lblmessage.Visible = false;
            }
            if (txt_doctor.Text == "" || !txt_doctor.Text.ToString().Contains(":"))
            {
                txt_doctor.Text = "";
                Messagealert_.ShowMessage(lblmessage, "DoctorName", 0);
                divmsg1.Attributes["class"] = "FailAlert";
                divmsg1.Visible = true;
                txt_doctor.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
            }
            if (ddl_servicetype.SelectedIndex == 0)
            {

                Messagealert_.ShowMessage(lblmessage, "ServiceType", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                ddl_servicetype.Focus();
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
            resetdata();
        }
        public void resetdata()
        {
            ddl_servicetype.SelectedIndex = 0;
            ddl_doctorType.SelectedIndex = 0;
            ddl_month.SelectedIndex = 0;
            txt_service.Text = "";
            txt_doctor.Text = "";
            ddldepartment.SelectedIndex = 0;
            txtdatefrom.Text = "";
            txtto.Text = "";
            lblmessage.Visible = false;
            lblresult.Visible = false;
            divmsg3.Visible = false;
            GvCollectionCommission.DataSource = null;
            GvCollectionCommission.DataBind();
            GvCollectionCommission.Visible = true;
            btnVerify.Visible = false;
            ddlexport.Visible = false;
            btnexport.Visible = false;
            divmsg3.Visible = false;
            checkSelect();
            txtTotalServiceCharge.Text = "0.00";
            txtTotalDocCommission.Text = "0.00";
            txtTotalHospitalCharge.Text = "0.00";
            txtTotalDocPayable.Text = "0.00";
           
        }
        protected void GvCollectionCommission_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label verified = (Label)e.Row.FindControl("lblIsverified");
                if (verified.Text == "YES")
                {
                    CheckBox cb = (CheckBox)e.Row.FindControl("checkdata");
                    cb.Checked = true;
                }
             
            }
        }

        protected DataTable GetDatafromDatabase()
        {
            List<DoctorWiseCollectionMasterData> CommissionDetails = GetCommissionData(0);
            List<DoctorWiseCollectionMasterDataToExcel> ListexcelData = new List<DoctorWiseCollectionMasterDataToExcel>();
            int i = 0;
            foreach (DoctorWiseCollectionMasterData row in CommissionDetails)
            {
                DoctorWiseCollectionMasterDataToExcel EcxeclCom = new DoctorWiseCollectionMasterDataToExcel();
                EcxeclCom.BillNo = CommissionDetails[i].BillNo;
                EcxeclCom.UHID = CommissionDetails[i].UHID;
                EcxeclCom.PatientName = CommissionDetails[i].PatientName;
                EcxeclCom.ServicetypeName = CommissionDetails[i].ServicetypeName;
                EcxeclCom.ServiceName = CommissionDetails[i].ServiceName;
                EcxeclCom.ServiceCharge = CommissionDetails[i].ServiceCharge;
                EcxeclCom.Commission = CommissionDetails[i].Commission;
                EcxeclCom.Tax = CommissionDetails[i].Tax;
                EcxeclCom.Hospitalcharge = CommissionDetails[i].Hospitalcharge;
                EcxeclCom.DoctorPayable = CommissionDetails[i].DoctorPayable;
                EcxeclCom.LastVisitDate = CommissionDetails[i].LastVisitDate;
               
                ListexcelData.Add(EcxeclCom);
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
        protected void ExportoExcel()
        {

            DataTable dt = GetDatafromDatabase();
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "DoctorWiseCollectionDetails");

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=DoctorWiseCollectionDetails.xlsx");
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
        public void ExportToPdf()
        {
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    GvCollectionCommission.BorderStyle = BorderStyle.None;
                    GvCollectionCommission.Columns[14].Visible = false;

                    GvCollectionCommission.RenderControl(hw);
                    GvCollectionCommission.HeaderRow.Style.Add("width", "15%");
                    GvCollectionCommission.HeaderRow.Style.Add("font-size", "10px");
                    GvCollectionCommission.Style.Add("text-decoration", "none");
                    GvCollectionCommission.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    GvCollectionCommission.Style.Add("font-size", "8px");
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                    pdfDoc.SetPageSize(iTextSharp.text.PageSize.A4.Rotate());
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=DoctorWiseCollectionDetails.pdf");
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
        protected void btnexport_Click(object sender, EventArgs e)
        {
            if (LogData.ExportEnable == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "ExportEnable", 0);
                divmsg3.Visible = true;
                divmsg3.Attributes["class"] = "FailAlert";
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
                divmsg3.Attributes["class"] = "FailAlert";
                ddlexport.Focus();
                return;
            }
        }
        protected void btnVerify_Click(object sender, EventArgs e) { 

           List<DoctorWiseCollectionMasterData> ListCommissionData = new List<DoctorWiseCollectionMasterData>();
            DoctorWiseCollectionMasterData objCommissionData = new DoctorWiseCollectionMasterData();
            DoctorWiseDailyCollectionBO objCommissionBO = new DoctorWiseDailyCollectionBO();

            int checkflag = 0;
            try
            {
                // get all the record from the gridview
                foreach (GridViewRow row in GvCollectionCommission.Rows)
                {
                    IFormatProvider provider = new System.Globalization.CultureInfo("en-GB", true);
                    CheckBox checkdata = (CheckBox)GvCollectionCommission.Rows[row.RowIndex].Cells[0].FindControl("checkdata");
                    if (checkdata.Checked)
                    {
                        checkflag = 1;
                        Label BillId = (Label)GvCollectionCommission.Rows[row.RowIndex].Cells[0].FindControl("lblBillId");
                        Label BillNo = (Label)GvCollectionCommission.Rows[row.RowIndex].Cells[0].FindControl("lblBillNo");
                        Label UHID = (Label)GvCollectionCommission.Rows[row.RowIndex].Cells[0].FindControl("lblUHID");
                        Label Servicetype = (Label)GvCollectionCommission.Rows[row.RowIndex].Cells[0].FindControl("lblServicetype");
                        Label DoctorID = (Label)GvCollectionCommission.Rows[row.RowIndex].Cells[0].FindControl("lblDoctorID");
                        Label Doctortype = (Label)GvCollectionCommission.Rows[row.RowIndex].Cells[0].FindControl("lblDoctortype");
                        Label DepartmentID = (Label)GvCollectionCommission.Rows[row.RowIndex].Cells[0].FindControl("lblDepartmentID");
                        Label ServiceID = (Label)GvCollectionCommission.Rows[row.RowIndex].Cells[0].FindControl("lblServiceID");
                        Label ServiceCharge = (Label)GvCollectionCommission.Rows[row.RowIndex].Cells[0].FindControl("lblServiceCharge");
                        Label Commission = (Label)GvCollectionCommission.Rows[row.RowIndex].Cells[0].FindControl("lblCommission");
                        Label Tax = (Label)GvCollectionCommission.Rows[row.RowIndex].Cells[0].FindControl("lblTax");
                        Label DoctorPayable = (Label)GvCollectionCommission.Rows[row.RowIndex].Cells[0].FindControl("lblDoctorPayable");
                        Label HospitalID = (Label)GvCollectionCommission.Rows[row.RowIndex].Cells[0].FindControl("lblHospitalID");
                        Label HospitalCharge = (Label)GvCollectionCommission.Rows[row.RowIndex].Cells[0].FindControl("lblHospitalCharge");
                        Label Addeddate = (Label)GvCollectionCommission.Rows[row.RowIndex].Cells[0].FindControl("lblAddeddate");
                        Label FinancialYearID = (Label)GvCollectionCommission.Rows[row.RowIndex].Cells[0].FindControl("lblFinancialYearID");
                        Label AddedBy = (Label)GvCollectionCommission.Rows[row.RowIndex].Cells[0].FindControl("lblAddedBy");
                        

                        DoctorWiseCollectionMasterData objSubCommissionData = new DoctorWiseCollectionMasterData();
                        objSubCommissionData.BillID = Convert.ToInt64(BillId.Text == "" ? "0" : BillId.Text);
                        objSubCommissionData.BillNo = BillNo.Text == "" ? null : BillNo.Text;
                        objSubCommissionData.UHID = Convert.ToInt64(UHID.Text==""?"0":UHID.Text);
                        objSubCommissionData.Servicetype = Convert.ToInt32(Servicetype.Text == "" ? "0" : Servicetype.Text);
                        objSubCommissionData.Doctortype = Convert.ToInt32(Doctortype.Text == "" ? "0" : Doctortype.Text);
                        objSubCommissionData.DepartmentID = Convert.ToInt32(DepartmentID.Text == "" ? "0" : DepartmentID.Text);
                        objSubCommissionData.DoctorID = Convert.ToInt32(DoctorID.Text == "" ? "0" : DoctorID.Text);
                        objSubCommissionData.ServiceID = Convert.ToInt32(ServiceID.Text == "" ? "0" : ServiceID.Text);
                        objSubCommissionData.Servicetype = Convert.ToInt32(Servicetype.Text == "" ? "0" : Servicetype.Text);
                        objSubCommissionData.HospitalID = Convert.ToInt32(HospitalID.Text == "" ? "0" : HospitalID.Text);
                        objSubCommissionData.FinancialYearID = Convert.ToInt32(FinancialYearID.Text == "" ? "0" : FinancialYearID.Text);
                        objSubCommissionData.AddedByID = Convert.ToInt64(AddedBy.Text == "" ? "0" : AddedBy.Text);
                        objSubCommissionData.LastVisitDate = Convert.ToDateTime(Addeddate.Text);
                        objSubCommissionData.ServiceCharge = Convert.ToDecimal(ServiceCharge.Text == "" ? "0" : ServiceCharge.Text);
                        objSubCommissionData.Hospitalcharge = Convert.ToDecimal(HospitalCharge.Text == "" ? "0" : HospitalCharge.Text);
                        objSubCommissionData.Commission = Convert.ToDecimal(Commission.Text == "" ? "0" : Commission.Text);
                        objSubCommissionData.DoctorPayable = Convert.ToDecimal(DoctorPayable.Text == "" ? "0" : DoctorPayable.Text);
                        objSubCommissionData.Tax = Convert.ToDecimal(Tax.Text == "" ? "0" : Tax.Text);
                        objSubCommissionData.verifyBy = LogData.EmployeeID;
                        ListCommissionData.Add(objSubCommissionData);
                    }

                }
                if (checkflag == 0) {

                    Messagealert_.ShowMessage(lblmessage, "checkbox", 0);
                    divmsg3.Visible = true;
                    divmsg3.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                objCommissionData.XMLData = XmlConvertor.DoctorWiseCollectionDatatoXML(ListCommissionData).ToString();

                int result = objCommissionBO.UpdateCollectionVerification(objCommissionData);
                if (result > 0)
                {
                   
                    Messagealert_.ShowMessage(lblmessage, "update", 1);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "SucessAlert";
                    ddl_verified.SelectedValue = "1";
                    bindgrid();
                }
                else
                {
                    
                    Messagealert_.ShowMessage(lblmessage, "Error", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                }


            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);

            }
        }

    }
}
using ClosedXML.Excel;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedBillBO;
using Mediqura.BOL.MedUtilityBO;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.LoginData;
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
    public partial class DoctorWiseCollectionPayment : BasePage
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
        decimal totalServiceCharge = 0;
        decimal totaldoctorspayable = 0;
        decimal subtotaldoctorpayable = 0;
        decimal totalhospitalCharge = 0;
        decimal totalcommission = 0;
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
        public void PH_checkSelect()
        {
          
            if (PH_ddlDoctorType.SelectedIndex == 0)
            {
                PH_ddlDepartment.Attributes["disabled"] = "disabled";
            }
            else
            {
                PH_ddlDepartment.Attributes.Remove("disabled");
            }
            if (PH_ddlDepartment.SelectedIndex == 0)
            {
                PH_txtDoctor.ReadOnly = true;
            }
            else
            {
                PH_txtDoctor.ReadOnly = false;
            }
        }
        private void ddlbind()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddl_servicetype, mstlookup.GetLookupsList(LookupName.ServiceType));
            Commonfunction.PopulateDdl(ddl_doctorType, mstlookup.GetLookupsList(LookupName.DoctorType));
            Commonfunction.PopulateDdl(ddldepartment, mstlookup.GetLookupsList(LookupName.Department));
            Commonfunction.PopulateDdl(PH_ddl_month, mstlookup.GetLookupsList(LookupName.month));
            Commonfunction.PopulateDdl(PH_ddlDoctorType, mstlookup.GetLookupsList(LookupName.DoctorType));
            Commonfunction.PopulateDdl(PH_ddlDepartment, mstlookup.GetLookupsList(LookupName.Department));
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
        protected void PH_ddl_doctorType_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkSelect();
            PH_auto_doctor.ContextKey = PH_ddlDoctorType.SelectedValue;
        }
        protected void PH_ddldepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkSelect();
            departmentId = PH_ddlDepartment.SelectedValue;

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

                   
                    divmsg3.Visible = true;
                    ddlexport.Visible = true;
                    btnexport.Visible = true;

                    if (ddl_paid.SelectedValue == "0")
                    {
                        txtTotalpaidAmount.ReadOnly = false;
                        txtTotalServiceCharge.Text = "0.00";
                        txtSubtotalPayable.Text = Commonfunction.Getrounding(commissionDetails[0].Due.ToString());
                        txtTotalDoctorCommission.Text = "0.00";
                        txt_current_due.Text = "0.00";
                        txt_total_due.Text = Commonfunction.Getrounding(commissionDetails[0].Due.ToString()); 
                        txtTotalDocPayable.Text = Commonfunction.Getrounding(commissionDetails[0].Due.ToString()); 
                        txtDuePayemnt.Text = Commonfunction.Getrounding(commissionDetails[0].Due.ToString());
                        subtotaldoctorpayable = Convert.ToDecimal(commissionDetails[0].Due.ToString());
                        totaldoctorspayable = Convert.ToDecimal(commissionDetails[0].TotalPayable.ToString());
                        btnpaid.Visible = true;
                    }
                    else
                    {
                        txtTotalServiceCharge.Text = Commonfunction.Getrounding(commissionDetails[0].TotalServiceCharge.ToString());
                        txtSubtotalPayable.Text = Commonfunction.Getrounding(commissionDetails[0].TotalPayable.ToString());
                        txtTotalDocPayable.Text = Commonfunction.Getrounding(commissionDetails[0].TotalPayable.ToString());
                        txtDuePayemnt.Text = Commonfunction.Getrounding(commissionDetails[0].Due.ToString());
                        txtTotalDoctorCommission.Text = Commonfunction.Getrounding(commissionDetails[0].TotalCommission.ToString());
                        txt_current_due.Text = "0.00";
                        txtTotalpaidAmount.Text = "0.00";
                        txtTotalpaidAmount.ReadOnly = true;
                        txt_total_due.Text = Commonfunction.Getrounding(commissionDetails[0].Due.ToString()); 
                        btnpaid.Visible = false;
                    }
                }
                else
                {

                    divmsg3.Visible = false;
                    GvCollectionCommission.DataSource = null;
                    GvCollectionCommission.DataBind();
                    GvCollectionCommission.Visible = true;
                    txtTotalServiceCharge.Text = "0.00";
                    txtSubtotalPayable.Text = "0.00";
                    txtTotalDocPayable.Text = "0.00";
                    txt_current_due.Text = "0.00";
                    txt_total_due.Text = "0.00";
                    ddlexport.Visible = false;
                    btnexport.Visible = false;
                    btnpaid.Visible = false;
                    divmsg3.Visible = false;
                    lblresult.Visible = false;
                    txtTotalpaidAmount.ReadOnly = true;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
            }
        }
        protected void PH_bindgrid()
        {
            try
            {

                List<DoctorWiseCollectionMasterData> commissionDetails = GetPaymentHistory(0);
                if (commissionDetails.Count > 0)
                {
                    GVpaymentHistory.DataSource = commissionDetails;
                    GVpaymentHistory.DataBind();
                    GVpaymentHistory.Visible = true;
                    Messagealert_.ShowMessage(lblresult5, "Total:" + commissionDetails[0].MaximumRows.ToString() + " Record(s) found", 1);
                    divmsg4.Attributes["class"] = "SucessAlert";


                    divmsg4.Visible = true;
                    PH_ddlExport.Visible = true;
                    PH_btnexport.Visible = true;

                        PH_txtTotalDocPayablePH.Text = Commonfunction.Getrounding(commissionDetails[0].TotalPayable.ToString());
                        PH_txtTotalAmountPaid.Text = Commonfunction.Getrounding(commissionDetails[0].TotalAmount.ToString());
                        PH_txtTotalAmountDue.Text = Commonfunction.Getrounding(commissionDetails[0].TotalDue.ToString());
                 
                }
                else
                {

                    divmsg4.Visible = false;
                    GVpaymentHistory.DataSource = null;
                    GVpaymentHistory.DataBind();
                    GVpaymentHistory.Visible = true;
                    PH_txtTotalDocPayablePH.Text = "0.00";
                    PH_txtTotalAmountPaid.Text = "0.00";
                    PH_txtTotalAmountDue.Text = "0.00";

                    PH_ddlExport.Visible = false;
                    PH_btnexport.Visible = false;

                    divmsg4.Visible = false;
                    lblmessage4.Visible = false;
               
                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
            }
        }
        protected void amountChange(object sender, EventArgs e)
        {

            decimal docpayable, amountpaid;
            docpayable = Convert.ToDecimal(txtTotalDocPayable.Text == "" ? "0" : txtTotalDocPayable.Text);
            amountpaid=Convert.ToDecimal(txtTotalpaidAmount.Text == "" ? "0" : txtTotalpaidAmount.Text);
            if(amountpaid>=docpayable){
                txt_current_due.Text = Commonfunction.Getrounding("0.00");
            }else
                txt_current_due.Text = Commonfunction.Getrounding("" + (docpayable - amountpaid));
            txt_total_due.Text = Commonfunction.Getrounding("" + (Convert.ToDecimal(txt_current_due.Text) + Convert.ToDecimal(txtDuePayemnt.Text)));
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
            objCommissionData.paid = Convert.ToInt32(ddl_paid.SelectedValue == "" ? "0" : ddl_paid.SelectedValue);
            DateTime from = txtdatefrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txtdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txtto.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            string datefrom = from.ToString("yyyy-MM-dd");
            string dateto = To.ToString("yyyy-MM-dd");
            from = Convert.ToDateTime(datefrom + " " + "12:01:00 AM");
            To = Convert.ToDateTime(dateto + " " + "11:59:00 PM");
            objCommissionData.DateFrom = from;
            objCommissionData.DateTo = To;
            return objCommissionBO.GetDoctorspaymentCollectionList(objCommissionData);

        }
        public List<DoctorWiseCollectionMasterData> GetPaymentHistory(int curIndex)
        {

            DoctorWiseCollectionMasterData objCommissionData = new DoctorWiseCollectionMasterData();
            DoctorWiseDailyCollectionBO objCommissionBO = new DoctorWiseDailyCollectionBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);

             String doctorid = "0";

             String doctorText = PH_txtDoctor.Text == "" ? null : PH_txtDoctor.Text.ToString().Trim();
            if (doctorText != null)
            {
                String[] doctor = doctorText.Split(new[] { ":" }, StringSplitOptions.None);
                doctorid = doctor[1];
            }

            objCommissionData.DepartmentID = Convert.ToInt32(PH_ddlDepartment.SelectedValue == "" ? "0" : PH_ddlDepartment.SelectedValue); objCommissionData.DoctorID = Convert.ToInt32(doctorid);
            objCommissionData.Doctortype = Convert.ToInt32(PH_ddlDoctorType.SelectedValue == "" ? "0" : PH_ddlDoctorType.SelectedValue);
            objCommissionData.Month = Convert.ToInt32(PH_ddl_month.SelectedValue == "" ? "0" : PH_ddl_month.SelectedValue);
            DateTime from = PH_txtdatefrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(PH_txtdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = PH_txtto.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(PH_txtto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            string datefrom = from.ToString("yyyy-MM-dd");
            string dateto = To.ToString("yyyy-MM-dd");
            from = Convert.ToDateTime(datefrom + " " + "12:01:00 AM");
            To = Convert.ToDateTime(dateto + " " + "11:59:00 PM");
            objCommissionData.DateFrom = from;
            objCommissionData.DateTo = To;
            return objCommissionBO.GetDoctorspaymentHistory(objCommissionData);

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
      
            bindgrid();
        }

        protected void btnresets_Click(object sender, EventArgs e)
        {
            resetdata();
        }
        protected void PH_btnsearch_Click(object sender, EventArgs e)
        {
            if (LogData.SearchEnable == 0)
            {
                Messagealert_.ShowMessage(lblmessage4, "SearchEnable", 0);
                divmsg4.Visible = true;
                divmsg4.Attributes["class"] = "FailAlert";
                return;
            }
            else
            {
                lblmessage4.Visible = false;
            }


            if (PH_ddlDoctorType.SelectedIndex == 0)
            {

                Messagealert_.ShowMessage(lblmessage4, "DoctorType", 0);
                divmsg4.Visible = true;
                divmsg4.Attributes["class"] = "FailAlert";
                PH_ddlDoctorType.Focus();
                return;

            }
            if (PH_ddlDepartment.SelectedIndex == 0)
            {

                Messagealert_.ShowMessage(lblmessage4, "Department", 0);
                divmsg4.Visible = true;
                divmsg4.Attributes["class"] = "FailAlert";
                PH_ddlDepartment.Focus();
                return;

            }
            else
            {
                lblmessage4.Visible = false;
            }
            if (PH_txtDoctor.Text == "" || !PH_txtDoctor.Text.ToString().Contains(":"))
            {
                PH_txtDoctor.Text = "";
                Messagealert_.ShowMessage(lblmessage4, "DoctorName", 0);
                divmsg4.Attributes["class"] = "FailAlert";
                divmsg4.Visible = true;
                PH_txtDoctor.Focus();
                return;
            }
            else
            {
                lblmessage4.Visible = false;
            }

            PH_bindgrid();
        }

        protected void PH_btnresets_Click(object sender, EventArgs e)
        {
            PH_resetdata();
        }
        public void PH_resetdata()
        {
            PH_ddlDoctorType.SelectedIndex = 0;
            PH_ddlDepartment.SelectedIndex = 0;
            PH_ddl_month.SelectedIndex = 0;
            
            PH_txtDoctor.Text = "";
            ddldepartment.SelectedIndex = 0;
            PH_txtdatefrom.Text = "";
            PH_txtto.Text = "";
            lblmessage4.Visible = false;
            lblresult5.Visible = false;
            divmsg4.Visible = false;
            GVpaymentHistory.DataSource = null;
            GVpaymentHistory.DataBind();
            GVpaymentHistory.Visible = true;

            PH_ddlExport.Visible = false;
            PH_btnexport.Visible = false;
            divmsg5.Visible = false;
            PH_checkSelect();
            txtTotalServiceCharge.Text = "0.00";
            txtSubtotalPayable.Text = "0.00";
            txtTotalDocPayable.Text = "0.00";
         
        }
        public void resetdata()
        {
            ddl_servicetype.SelectedIndex = 0;
            ddl_doctorType.SelectedIndex = 0;
            ddl_month.SelectedIndex = 0;
            ddl_paid.SelectedIndex = 0;
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
            btnpaid.Visible = false;
            ddlexport.Visible = false;
            btnexport.Visible = false;
            divmsg3.Visible = false;
            checkSelect();
            txtTotalServiceCharge.Text = "0.00";
            txtSubtotalPayable.Text = "0.00";
            txtTotalDocPayable.Text = "0.00";
            txtTotalDoctorCommission.Text = "0.00";
            txtDuePayemnt.Text = "0.00";
            txt_current_due.Text = "0.00";
            txt_total_due.Text = "0.00";
            txtTotalpaidAmount.ReadOnly = true;
            txtTotalpaidAmount.Text = "";
        }
        protected void GvCollectionCommission_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label verified = (Label)e.Row.FindControl("lblisPaid");
                Label paidDate = (Label)e.Row.FindControl("lblPaidDate");
                if (verified.Text != "NO")
                {
                    CheckBox cb = (CheckBox)e.Row.FindControl("checkdata");
                    cb.Checked = true;
                    paidDate.Visible = true;
                    verified.Visible = false;
                }
                else {
                    paidDate.Visible = false;
                    verified.Visible = true;
                }

            }
        }
        
        protected void GvCollectionPayment_checkchange(object sender, EventArgs e)
        {
            try
            {
                    foreach (GridViewRow row in GvCollectionCommission.Rows)
                    {
                        IFormatProvider provider = new System.Globalization.CultureInfo("en-GB", true);
                        CheckBox checkdata = (CheckBox)GvCollectionCommission.Rows[row.RowIndex].Cells[0].FindControl("checkdata");
                        if (checkdata.Checked)
                        {

                            Label ServiceCharge = (Label)GvCollectionCommission.Rows[row.RowIndex].Cells[0].FindControl("lblServiceCharge");
                            Label Commission = (Label)GvCollectionCommission.Rows[row.RowIndex].Cells[0].FindControl("lblCommission");

                            Label DoctorPayable = (Label)GvCollectionCommission.Rows[row.RowIndex].Cells[0].FindControl("lblDoctorPayable");

                            Label HospitalCharge = (Label)GvCollectionCommission.Rows[row.RowIndex].Cells[0].FindControl("lblHospitalCharge");

                            totalServiceCharge = totalServiceCharge + Convert.ToDecimal(ServiceCharge.Text.ToString());
                            totalhospitalCharge = totalhospitalCharge + Convert.ToDecimal(HospitalCharge.Text.ToString());
                            totaldoctorspayable = totaldoctorspayable + Convert.ToDecimal(DoctorPayable.Text.ToString());
                            subtotaldoctorpayable = subtotaldoctorpayable + Convert.ToDecimal(DoctorPayable.Text.ToString());
                            totalcommission = totalcommission + Convert.ToDecimal(Commission.Text.ToString());
                        }
                    }
                    txtTotalServiceCharge.Text = Commonfunction.Getrounding("" + totalServiceCharge);
                    txtSubtotalPayable.Text = Commonfunction.Getrounding("" + (subtotaldoctorpayable + Convert.ToDecimal(txtDuePayemnt.Text)));
                    txtTotalDocPayable.Text = Commonfunction.Getrounding("" + subtotaldoctorpayable);
                    txtTotalDoctorCommission.Text = Commonfunction.Getrounding("" + totalcommission);
                    txt_current_due.Text = "0.00";
                    txt_total_due.Text = txtDuePayemnt.Text;
                   
                    txtTotalpaidAmount.Text = "";
               
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblresult, "system", 0);
                divmsg3.Attributes["class"] = "FailAlert";
                divmsg3.Visible = true;
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
        protected DataTable PH_GetDatafromDatabase()
        {
            List<DoctorWiseCollectionMasterData> CommissionDetails = GetPaymentHistory(0);
            List<DoctorWisePaymentMasterDataToExcel> ListexcelData = new List<DoctorWisePaymentMasterDataToExcel>();
            int i = 0;
            foreach (DoctorWiseCollectionMasterData row in CommissionDetails)
            {
                DoctorWisePaymentMasterDataToExcel EcxeclCom = new DoctorWisePaymentMasterDataToExcel();
                EcxeclCom.PayID = CommissionDetails[i].PayID;
                EcxeclCom.LastDue = CommissionDetails[i].LastDue;
                EcxeclCom.Payable = CommissionDetails[i].Payable;
                EcxeclCom.SubPayable = CommissionDetails[i].SubPayable;
                EcxeclCom.Due = CommissionDetails[i].Due;
                EcxeclCom.Amount = CommissionDetails[i].Amount;
                EcxeclCom.paydate = CommissionDetails[i].paydate;
           

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
                    GvCollectionCommission.Columns[12].Visible = false;

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
                divmsg1.Attributes["class"] = "FailAlert";
                ddlexport.Focus();
                return;
            }
        }
        protected void btnpaid_Click(object sender, EventArgs e)
        {

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
                        objSubCommissionData.UHID = Convert.ToInt64(UHID.Text == "" ? "0" : UHID.Text);
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
                if (checkflag == 0)
                {

                    Messagealert_.ShowMessage(lblmessage, "checkbox", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                decimal totallDocPayable = Convert.ToDecimal(txtTotalDocPayable.Text);
                decimal totalAmount = Convert.ToDecimal(txtTotalpaidAmount.Text =="" ? "0" : txtTotalpaidAmount.Text);
                decimal totalDue = Convert.ToDecimal(txtDuePayemnt.Text);
                decimal subtotalpayable = Convert.ToDecimal(txtSubtotalPayable.Text);
                if (totalAmount<1)
                {

                    Messagealert_.ShowMessage(lblmessage, "validAmount", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (totalAmount > subtotalpayable)
                {

                    Messagealert_.ShowMessage(lblmessage, "payment", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                String doctorid = "0";

                String doctorText = txt_doctor.Text == "" ? null : txt_doctor.Text.ToString().Trim();
                if (doctorText != null)
                {
                    String[] doctor = doctorText.Split(new[] { ":" }, StringSplitOptions.None);
                    doctorid = doctor[1];
                }
              
                
                objCommissionData.XMLData = XmlConvertor.DoctorWiseCollectionDatatoXML(ListCommissionData).ToString();
                objCommissionData.TotalPayable = totallDocPayable;
                objCommissionData.TotalAmount = totalAmount;
                objCommissionData.Doctortype = Convert.ToInt32(ddl_doctorType.SelectedValue == "" ? "0" : ddl_doctorType.SelectedValue);
                objCommissionData.DoctorID = Convert.ToInt32(doctorid);
                objCommissionData.HospitalID = LogData.HospitalID;
                objCommissionData.AddedByID = LogData.EmployeeID;
                objCommissionData.FinancialYearID = LogData.FinancialYearID;
                


                int result = objCommissionBO.UpdateDoctorPaymentCollection(objCommissionData);
                if (result > 0)
                {

                    Messagealert_.ShowMessage(lblmessage, "update", 1);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "SucessAlert";
                    ddl_paid.SelectedValue = "1";
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
        protected void PH_ExportoExcel()
        {

            DataTable dt = PH_GetDatafromDatabase();
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "DoctorWisePaymentDetails");

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=DoctorWisePaymentDetails.xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                    ddlexport.SelectedIndex = 0;
                }
                Messagealert_.ShowMessage(lblmessage4, "Exported", 1);
                divmsg4.Attributes["class"] = "SucessAlert";
            }
        }
        public void PH_ExportToPdf()
        {
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    GVpaymentHistory.BorderStyle = BorderStyle.None;
                    GVpaymentHistory.Columns[10].Visible = false;

                    GVpaymentHistory.RenderControl(hw);
                    GVpaymentHistory.HeaderRow.Style.Add("width", "15%");
                    GVpaymentHistory.HeaderRow.Style.Add("font-size", "10px");
                    GVpaymentHistory.Style.Add("text-decoration", "none");
                    GVpaymentHistory.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    GVpaymentHistory.Style.Add("font-size", "8px");
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                    pdfDoc.SetPageSize(iTextSharp.text.PageSize.A4.Rotate());
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=DoctorWisePaymentDetails.pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.Write(pdfDoc);
                    Response.End();
                    Messagealert_.ShowMessage(lblmessage4, "Exported", 1);
                    divmsg4.Attributes["class"] = "SucessAlert";
                }
            }
        }
        protected void PH_btnexport_Click(object sender, EventArgs e)
        {
            if (LogData.ExportEnable == 0)
            {
                Messagealert_.ShowMessage(lblmessage4, "ExportEnable", 0);
                divmsg4.Visible = true;
                divmsg4.Attributes["class"] = "FailAlert";
                return;
            }
            else
            {
                lblmessage4.Visible = false;
            }
            if (PH_ddlExport.SelectedIndex == 1)
            {
                PH_ExportoExcel();
            }
            else if (PH_ddlExport.SelectedIndex == 2)
            {
                PH_ExportToPdf();
            }
            else
            {
                Messagealert_.ShowMessage(lblresult, "ExportType", 0);
                divmsg4.Attributes["class"] = "FailAlert";
                ddlexport.Focus();
                return;
            }
        }
    }
}
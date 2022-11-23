using ClosedXML.Excel;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedAccount;
using Mediqura.BOL.MedBillBO;
using Mediqura.BOL.PatientBO;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.LoginData;
using Mediqura.CommonData.MedAccount;
using Mediqura.CommonData.MedBillData;
using Mediqura.CommonData.PatientData;
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

namespace Mediqura.Web.MedAccount
{
    public partial class InsuranceReceivable : BasePage
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
            Commonfunction.PopulateDdl(ddl_patcategory, mstlookup.GetLookupsList(LookupName.PatientType));
            Commonfunction.PopulateDdl(ddl_subcategory, mstlookup.GetLookupsList(LookupName.TPAList));
            Commonfunction.PopulateDdl(ddlpaymentmode, mstlookup.GetLookupsList(LookupName.PaymentMode));
           
        }
        protected void ddl_patcategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_patcategory.SelectedValue == "4")
            {
                ddl_subcategory.Attributes.Remove("disabled");
                ddl_subcategory.Focus();
            }
            else
            {
                ddl_subcategory.SelectedIndex = 0;
                ddl_subcategory.Attributes["disabled"] = "disabled";
            }
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetDiscountedPatientName(string prefixText, int count, string contextKey)
        {
            PatientData Objpaic = new PatientData();
            RegistrationBO objInfoBO = new RegistrationBO();
            List<PatientData> getResult = new List<PatientData>();
            Objpaic.PatientName = prefixText;
            getResult = objInfoBO.GetDiscountedPatientName(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].PatientName.ToString());
            }
            return list;
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetBillNo(string prefixText, int count, string contextKey)
        {
            InsuranceReceivableData Objpaic = new InsuranceReceivableData();
            InsuranceReceivableBO objInfoBO = new InsuranceReceivableBO();
            List<InsuranceReceivableData> getResult = new List<InsuranceReceivableData>();
            Objpaic.BillNo = prefixText;
            getResult = objInfoBO.GetBillNo(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].BillNo.ToString());
            }
            return list;
        }
        protected void ddlpaymentmode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlpaymentmode.SelectedIndex > 0)
            {
                if (ddlpaymentmode.SelectedValue == "1")
                {
                    txtbank.Text = "";
                    txtbank.ReadOnly = true;
                }
                else if (ddlpaymentmode.SelectedValue == "2")
                {
                    GetBankName(Convert.ToInt32(ddlpaymentmode.SelectedValue == "" ? "0" : ddlpaymentmode.SelectedValue));
                    txtbank.ReadOnly = true;
                }
                else if (ddlpaymentmode.SelectedValue == "3")
                {
                    GetBankName(Convert.ToInt32(ddlpaymentmode.SelectedValue == "" ? "0" : ddlpaymentmode.SelectedValue));
                    txtbank.ReadOnly = true;
                }
                else if (ddlpaymentmode.SelectedValue == "4")
                {
                    txtbank.Text = "";
                    txtbank.ReadOnly = false;
                }
            }
            else
            {
                txtbank.Text = "";
                txtbank.ReadOnly = true;
            }
        }
        protected void GetBankName(int paymode)
        {
            OPDbillingBO objbillingBO = new OPDbillingBO();
            BankDetail objbankdetail = new BankDetail();
            objbankdetail.PaymodeID = paymode;
            List<BankDetail> banklist = objbillingBO.Getbanklist(objbankdetail);
            if (banklist.Count > 0)
            {
                txtbank.Text = banklist[0].BankName.ToString();
                hdnbankID.Value = banklist[0].BankID.ToString();
            }
            else
            {
                txtbank.Text = "";
                hdnbankID.Value = null;
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

            bindgrid(1);
        }
        protected void bindgrid(int page)
        {
            try
            {

                List<InsuranceReceivableData> objdeposit = GetExtraDiscountedLists(page);
                if (objdeposit.Count > 0)
                {
                    GVInsuranceReceivable.VirtualItemCount = objdeposit[0].MaximumRows;//total item is required for custom paging
                    GVInsuranceReceivable.PageIndex = page - 1;
                    GVInsuranceReceivable.DataSource = objdeposit;
                    GVInsuranceReceivable.DataBind();
                    GVInsuranceReceivable.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total:" + objdeposit[0].MaximumRows.ToString() + " Record(s) found.", 1);
                    divmsg3.Attributes["class"] = "SucessAlert";
                    divmsg3.Visible = true;
                    ddlexport.Visible = true;
                    btnexport.Visible = true;
                }
                else
                {
                    GVInsuranceReceivable.DataSource = null;
                    GVInsuranceReceivable.DataBind();
                    GVInsuranceReceivable.Visible = true;
                    lblresult.Visible = false;
                }
            }

            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
            }
        }
        protected void GVInsuranceReceivable_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label Received = (Label)e.Row.FindControl("lbl_IsReceived"); //find the CheckBox
                Label lblReceivable = (Label)e.Row.FindControl("lblReceivable"); //find the CheckBox
                Label isreceivable = (Label)e.Row.FindControl("lbl_isreceivable"); //find the CheckBox

                Label isreceived = (Label)e.Row.FindControl("lblisreceived"); //find the CheckBox
                CheckBox cb = (CheckBox)e.Row.FindControl("chekboxselect"); //find the CheckBox
                if (Convert.ToInt32(lblReceivable.Text) == 1)
                {
                    if (Convert.ToInt32(Received.Text) != 0)
                    {
                        cb.Checked = true;
                        cb.Enabled = false;
                        isreceivable.Text = "YES";

                    }
                    else
                    {
                        cb.Checked = false;
                        isreceivable.Text = "NO";
                    }
                }
                else {
                    cb.Visible = false;
                }

                if (Convert.ToInt32(Received.Text) == 1)
                {
                    isreceived.Text = "YES";
                }

                else
                {
                    isreceived.Text = "NO";

                }


            }
        }
        public List<InsuranceReceivableData> GetExtraDiscountedList(int curIndex)
        {
            InsuranceReceivableData objpat = new InsuranceReceivableData();
            InsuranceReceivableBO objbillingBO = new InsuranceReceivableBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            objpat.ServiceTypeID = Convert.ToInt32(ddlservicetype.SelectedValue == "" ? "0" : ddlservicetype.SelectedValue);
            objpat.PatientCategoryID = Convert.ToInt32(ddl_patcategory.SelectedValue == "" ? "0" : ddl_patcategory.SelectedValue);
            objpat.SubCategoryID = Convert.ToInt32(ddl_subcategory.SelectedValue == "" ? "0" : ddl_subcategory.SelectedValue);
            string ID;
            var source = txtpatientname.Text.ToString();
            if (source.Contains(":"))
            {
                ID = source.Substring(source.LastIndexOf(':') + 1);
                objpat.UHID = Convert.ToInt64(ID);
            }
            objpat.CurrentIndex = curIndex;
            objpat.BillNo = txt_billno.Text.Trim() == "" ? "" : txt_billno.Text.Trim();
            return objbillingBO.GetExtraDiscountedList(objpat);
        }
        protected void btupdate_Click(object sender, EventArgs e)
        {
            if (ddlpaymentmode.SelectedIndex == 0)
            {
                
                        Messagealert_.ShowMessage(lblmessage, "PaymentMode", 0);
                        div1.Visible = true;
                        div1.Attributes["class"] = "FailAlert";
                        
                        return;
                    }
                    
            else
            {
                lblmessage.Visible = false;
                div1.Visible = false;
            }
            List<InsuranceReceivableData> objlist = new List<InsuranceReceivableData>();
            InsuranceReceivableBO objbo = new InsuranceReceivableBO();
            InsuranceReceivableData objdata = new InsuranceReceivableData();
            try
            {
                // get all the record from the gridview
                foreach (GridViewRow row in GVInsuranceReceivable.Rows)
                {
                    CheckBox cb = (CheckBox)GVInsuranceReceivable.Rows[row.RowIndex].Cells[0].FindControl("chekboxselect"); //find the CheckBox
                    if (cb != null)
                    {
                        if (cb.Checked && cb.Enabled==true)
                        {
                            IFormatProvider provider = new System.Globalization.CultureInfo("en-GB", true);
                            Label ID = (Label)GVInsuranceReceivable.Rows[row.RowIndex].Cells[0].FindControl("lblID");
                            Label lblUHID = (Label)GVInsuranceReceivable.Rows[row.RowIndex].Cells[0].FindControl("lblUHID");
                            Label lblpatientName = (Label)GVInsuranceReceivable.Rows[row.RowIndex].Cells[0].FindControl("lblpatientName");
                            Label lblbillno = (Label)GVInsuranceReceivable.Rows[row.RowIndex].Cells[0].FindControl("lblbillno");
                            Label lblPatCatId = (Label)GVInsuranceReceivable.Rows[row.RowIndex].Cells[0].FindControl("lblPatCatId");
                            Label lblPatSubCatId = (Label)GVInsuranceReceivable.Rows[row.RowIndex].Cells[0].FindControl("lblPatSubCatId");
                            Label lbldiscamt = (Label)GVInsuranceReceivable.Rows[row.RowIndex].Cells[0].FindControl("lbldiscamt");
                            Label lblServicetypeID = (Label)GVInsuranceReceivable.Rows[row.RowIndex].Cells[0].FindControl("lblServicetypeID");
                            InsuranceReceivableData ObjDetails = new InsuranceReceivableData();
                            ObjDetails.ID = Convert.ToInt64(ID.Text);
                            ObjDetails.UHID = Convert.ToInt64(lblUHID.Text);
                            ObjDetails.PatientName = lblpatientName.Text;
                            ObjDetails.BillNo = lblbillno.Text;
                            ObjDetails.ServiceTypeID = Convert.ToInt32(lblServicetypeID.Text);
                            ObjDetails.PatientCategoryID = Convert.ToInt32(lblPatCatId.Text);
                            ObjDetails.SubCategoryID = Convert.ToInt32(lblPatSubCatId.Text==""?"0": lblPatSubCatId.Text);
                            ObjDetails.DiscountAmount = Convert.ToDecimal(lbldiscamt.Text);
                            objlist.Add(ObjDetails);
                        }
                     }
                  }
                  objdata.XMLData = XmlConvertor.GVInsuranceReceivableRecordDatatoXML(objlist).ToString();
                  objdata.FinancialYearID = LogData.FinancialYearID;
                  objdata.EmployeeID = LogData.EmployeeID;
                  objdata.HospitalID = LogData.HospitalID;
                  objdata.IPaddress = LogData.IPaddress;
                  objdata.ActionType = Enumaction.Insert;
                  objdata.PaymentMode = Convert.ToInt32(ddlpaymentmode.SelectedValue);
                  int result = objbo.UpdateInsuranceReceivableDetails(objdata);
                if (result > 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "update", 1);
                    div1.Visible = true;
                    div1.Attributes["class"] = "SucessAlert";
                    bindgrid(1);
                }
                else
                {
                    Messagealert_.ShowMessage(lblmessage, "Error", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                }
            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                lblmessage.Text = ExceptionMessage.GetMessage(ex);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
            }
        }
        protected void GVInsuranceReceivable_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            bindgrid(Convert.ToInt32(e.NewPageIndex + 1));
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

            else
            {
                Messagealert_.ShowMessage(lblmessage, "ExportType", 0);
                div1.Attributes["class"] = "FailAlert";
                ddlexport.Focus();
                return;
            }
        }
        protected void ExportoExcel()
        {
            DataTable dt = GetDatafromDatabase();
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "Item CheckList");
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=AppointmentBookingDetails.xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                    ddlexport.SelectedIndex = 0;
                }
                Messagealert_.ShowMessage(lblmessage, "Exported", 1);
                div1.Attributes["class"] = "SucessAlert";
            }
        }
        protected void btnresets_Click(object sender, System.EventArgs e)
        {
            lblmessage.Visible = false;
            lblresult.Visible = false;
            div1.Visible = false;
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddl_patcategory, mstlookup.GetLookupsList(LookupName.PatientType));
            Commonfunction.PopulateDdl(ddl_subcategory, mstlookup.GetLookupsList(LookupName.TPAList));
            txtpatientname.Text = "";
            txt_billno.Text = "";
            GVInsuranceReceivable.DataSource = null;
            GVInsuranceReceivable.DataBind();
            GVInsuranceReceivable.Visible = false;
            btnexport.Visible = false;
            ddlexport.Visible = false;
            ddlservicetype.SelectedIndex = 0;
            ddl_patcategory.SelectedIndex = 0;
            ddl_subcategory.SelectedIndex = 0;
        }
        protected DataTable GetDatafromDatabase()
        {
            List<InsuranceReceivableData> DepositDetails = GetExtraDiscountedLists(0);
            List<InsuranceReceivableDataToExcel> ListexcelData = new List<InsuranceReceivableDataToExcel>();
            int i = 0;
            foreach (InsuranceReceivableData row in DepositDetails)
            {
                InsuranceReceivableDataToExcel Ecxeclpat = new InsuranceReceivableDataToExcel();
                if (DepositDetails[i].ServiceTypeID == 1)
                {
                    Ecxeclpat.ServiceType = "OP Services";
                }
                else if (DepositDetails[i].ServiceTypeID == 2)
                {
                    Ecxeclpat.ServiceType = "OP INV";
                }
                else if (DepositDetails[i].ServiceTypeID == 3)
                {
                    Ecxeclpat.ServiceType = "IP Services";
                }
                else if (DepositDetails[i].ServiceTypeID == 4)
                {
                    Ecxeclpat.ServiceType = "Emergency";
                }
                else 
                {
                    Ecxeclpat.ServiceType = " ";
                }
                Ecxeclpat.UHID = DepositDetails[i].UHID.ToString();
                Ecxeclpat.PatientName = DepositDetails[i].PatientName.ToString();
                Ecxeclpat.BillNo = DepositDetails[i].BillNo.ToString();
                Ecxeclpat.PatientCategory = DepositDetails[i].PatientCategory.ToString();
                Ecxeclpat.SubCategory = DepositDetails[i].SubCategory;
                Ecxeclpat.DiscountAmount = DepositDetails[i].DiscountAmount.ToString();
                Ecxeclpat.AddedBy = DepositDetails[i].EmpName.ToString();
                if (DepositDetails[i].IsReceived == 1)
                {
                    Ecxeclpat.Received = "Received";
                }
                else
                {
                    Ecxeclpat.Received = "Not Received";
                }
            
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

                // Get all the properties

                PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

                foreach (PropertyInfo prop in Props)
                {

                    //  Setting column names as Property names
                    dataTable.Columns.Add(prop.Name);

                }

                foreach (T item in items)
                {

                    var values = new object[Props.Length];

                    for (int i = 0; i < Props.Length; i++)
                    {

                        //       inserting property values to datatable rows

                        values[i] = Props[i].GetValue(item, null);

                    }

                    dataTable.Rows.Add(values);

                }

                //     put a breakpoint here and check datatable

                return dataTable;

            }
        }
        public List<InsuranceReceivableData> GetExtraDiscountedLists(int curIndex)
        {
            InsuranceReceivableData objpat = new InsuranceReceivableData();
            InsuranceReceivableBO objbillingBO = new InsuranceReceivableBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            objpat.ServiceTypeID = Convert.ToInt32(ddlservicetype.SelectedValue == "" ? "0" : ddlservicetype.SelectedValue);
            objpat.PatientCategoryID = Convert.ToInt32(ddl_patcategory.SelectedValue == "" ? "0" : ddl_patcategory.SelectedValue);
            objpat.SubCategoryID = Convert.ToInt32(ddl_subcategory.SelectedValue == "" ? "0" : ddl_subcategory.SelectedValue);
            objpat.IsReceived = Convert.ToInt32(ddl_status.SelectedValue);
            objpat.Receivable = Convert.ToInt32(ddl_receivable.SelectedValue);

            string ID;
            var source = txtpatientname.Text.ToString();
            if (source.Contains(":"))
            {
                ID = source.Substring(source.LastIndexOf(':') + 1);
                objpat.UHID = Convert.ToInt64(ID);
            }
            objpat.BillNo = txt_billno.Text.Trim() == "" ? "" : txt_billno.Text.Trim();
            return objbillingBO.GetExtraDiscountedLists(objpat);
        }
     
    }
}
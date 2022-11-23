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

namespace Mediqura.Web.MedBills
{
    public partial class LAbServiceCancelAdjustment : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnprints.Attributes["disabled"] = "disabled";
                btnsave.Attributes["disabled"] = "disabled";
                btnprint.Attributes["disabled"] = "disabled";
            }
         }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetBillNo(string prefixText, int count, string contextKey)
        {
            LabBillingData Objpaic = new LabBillingData();
            OPDbillingBO objInfoBO = new OPDbillingBO();
            List<LabBillingData> getResult = new List<LabBillingData>();
            Objpaic.BillNo = prefixText;
            getResult = objInfoBO.GetBillNo(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].BillNo);
            }
            return list;
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetAdjustmentNo(string prefixText, int count, string contextKey)
        {
            LabBillingData Objpaic = new LabBillingData();
            OPDbillingBO objInfoBO = new OPDbillingBO();
            List<LabBillingData> getResult = new List<LabBillingData>();
            Objpaic.AdjustmentNo = prefixText;
            getResult = objInfoBO.GetAdjustmentNo(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].AdjustmentNo);
            }
            return list;
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetUHID(string prefixText, int count, string contextKey)
        {
            PatientData Objpaic = new PatientData();
            RegistrationBO objInfoBO = new RegistrationBO();
            List<PatientData> getResult = new List<PatientData>();
            Objpaic.UHID = Convert.ToInt64(prefixText);
            getResult = objInfoBO.GetUHID(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].RegDNo.ToString());
            }
            return list;
        }
        protected void txtautoUHID_TextChanged(object sender, EventArgs e)
        {
            if (txtautoUHID.Text != "")
            {
                bindgrid1();
            }
            PatientData Objpaic = new PatientData();
            RegistrationBO objInfoBO = new RegistrationBO();
            List<PatientData> getResult = new List<PatientData>();
            Objpaic.UHID = Convert.ToInt64(txtautoUHID.Text.Trim() == "" ? "0" : txtautoUHID.Text.Trim());
            getResult = objInfoBO.GetPatientDetailsByUHID(Objpaic);
            if (getResult.Count > 0)
            {
                txtpatientNames.Text = getResult[0].PatientName.ToString();
                Session["ServiceList"] = null;
            }
            else
            {
                txtpatientNames.Text = "";
                txtautoUHID.Text = "";
                txtautoUHID.Focus();
            }

        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetPatientName(string prefixText, int count, string contextKey)
        {
            PatientData Objpaic = new PatientData();
            RegistrationBO objInfoBO = new RegistrationBO();
            List<PatientData> getResult = new List<PatientData>();
            Objpaic.PatientName = prefixText;
            getResult = objInfoBO.GetPatientName(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].PatientName.ToString());
            }
            return list;
        }
        protected void txtpatientNames_TextChanged(object sender, EventArgs e)
        {
            if (txtpatientNames.Text != "")
            {
                bindgrid1();
            }
        }
        protected void txt_adjustno_TextChanged(object sender, EventArgs e)
        {
            if (txt_adjustno.Text != "")
            {
                bindgrid1();
            }
        }
        protected void txt_BillNo_TextChanged(object sender, EventArgs e)
        {
            PatientData Objpaic = new PatientData();
            RegistrationBO objInfoBO = new RegistrationBO();
            List<PatientData> getResult = new List<PatientData>();
            Objpaic.BillNo = txt_BillNo.Text.Trim() == "" ? "" : txt_BillNo.Text.Trim();
            getResult = objInfoBO.GetPatientDetailsByBillNo(Objpaic);
            if (getResult.Count > 0)
            {
                txt_UHID.Text = getResult[0].UHID.ToString();
                txtname.Text = getResult[0].PatientName.ToString();
                txt_gender.Text = getResult[0].GenderName;
                txt_age.Text = getResult[0].Age.ToString();
                txtcontactno.Text = getResult[0].ContactNo;
                txt_BillNo.ReadOnly = true;
                bindgrid();
            }
            else
            {
                txt_BillNo.Text = "";
                txt_UHID.Text = "";
                txtname.Text = "";
                txt_gender.Text = "";
                txt_age.Text = "";
                txtcontactno.Text = "";
                txt_BillNo.Focus();
                txt_BillNo.ReadOnly = false;
            }

        }
        private void bindgrid()
        {
            try
            {
                List<LabBillingData> objdeposit = GetLabServiceList(0);
                if (objdeposit.Count > 0)
                {
                    gvlabservicelist.DataSource = objdeposit;
                    gvlabservicelist.DataBind();
                    gvlabservicelist.Visible = true;
                }
                else
                {
                    gvlabservicelist.DataSource = null;
                    gvlabservicelist.DataBind();
                    gvlabservicelist.Visible = true;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
            }
        }
        private List<LabBillingData> GetLabServiceList(int p)
        {
            LabBillingData objpat = new LabBillingData();
            OPDbillingBO objBO = new OPDbillingBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            objpat.BillNo = txt_BillNo.Text.Trim() == "" ? "" : txt_BillNo.Text.Trim();
            return objBO.GetLabServiceList(objpat);
        }
        protected void gvlabservicelist_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblSerial = (Label)e.Row.FindControl("lblserialID");
                lblSerial.Text = ((gvlabservicelist.PageIndex * gvlabservicelist.PageSize) + e.Row.RowIndex + 1).ToString();
                Label lblIsdeviceInitiated = (Label)e.Row.FindControl("lblIsdeviceInitiated");
                Label lblDevice = (Label)e.Row.FindControl("lblDevice");
                CheckBox cb = (CheckBox)e.Row.FindControl("chekboxselect");

                if (Convert.ToInt32(lblIsdeviceInitiated.Text == "" ? "0" : lblIsdeviceInitiated.Text) > 0)
                {
                    cb.Checked = false;
                    cb.Enabled = false;
                    lblDevice.Visible = true;
                }
                else {
                   
                    cb.Enabled = true;
                    lblDevice.Visible = false;
                }
            }
        }
        protected void chekboxselect_CheckedChanged(object sender, EventArgs e)
        {
            txtquantity.Text = "0";
            txt_totalrefundable.Text = "0.00";
            btnsave.Attributes["disabled"] = "disabled";
            foreach (GridViewRow row in gvlabservicelist.Rows)
            {
                CheckBox cb = (CheckBox)gvlabservicelist.Rows[row.RowIndex].Cells[0].FindControl("chekboxselect"); //find the CheckBox
                if (cb.Checked)
                {
                    btnsave.Attributes.Remove("disabled");
                   

                            txtquantity.Text = ((Convert.ToInt32(txtquantity.Text == "" ? "0" : txtquantity.Text) + 1)).ToString();
                            LabBillingData obj = new LabBillingData();
                            Label NetCharge = (Label)gvlabservicelist.Rows[row.RowIndex].Cells[0].FindControl("lblnetcharges");
                            decimal NetCharges = Convert.ToDecimal(NetCharge.Text);
                            txt_totalrefundable.Text = (Convert.ToDecimal(txt_totalrefundable.Text == "" ? "0.00" : txt_totalrefundable.Text) + NetCharges).ToString();
                 }
            }
        }
        protected void btnsave_Click(object sender, EventArgs e)
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
            if (txt_BillNo.Text == "")
            {
                Messagealert_.ShowMessage(lblmessage, "Please enter Bill No.", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                txt_BillNo.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
                divmsg1.Visible = false;
            }

            List<LabBillingData> Listbill = new List<LabBillingData>();
            OPDbillingBO objiprecBO = new OPDbillingBO();
            LabBillingData objrec = new LabBillingData();

            try
            {
                // get all the record from the gridview
                foreach (GridViewRow row in gvlabservicelist.Rows)
                {
                    CheckBox cb = (CheckBox)gvlabservicelist.Rows[row.RowIndex].Cells[0].FindControl("chekboxselect"); //find the CheckBox
                    if (cb.Checked)
                    {
                        IFormatProvider provider = new System.Globalization.CultureInfo("en-GB", true);
                        Label BillNo = (Label)gvlabservicelist.Rows[row.RowIndex].Cells[0].FindControl("lbl_billno");
                        Label Particulars = (Label)gvlabservicelist.Rows[row.RowIndex].Cells[0].FindControl("lbllabparticulars");
                        Label NetCharge = (Label)gvlabservicelist.Rows[row.RowIndex].Cells[0].FindControl("lblnetcharges");
                        Label SerialID = (Label)gvlabservicelist.Rows[row.RowIndex].Cells[0].FindControl("lblserialID");
                        Label ID = (Label)gvlabservicelist.Rows[row.RowIndex].Cells[0].FindControl("lblID");
                        LabBillingData ObjDetails = new LabBillingData();

                        ObjDetails.BillNo = BillNo.Text == "" ? null : BillNo.Text;
                        ObjDetails.TestName = Particulars.Text == "" ? null : Particulars.Text;
                        ObjDetails.NetLabServiceCharge = Convert.ToDecimal(NetCharge.Text == "" ? "0" : NetCharge.Text);
                        ObjDetails.SerialID = Convert.ToInt32(SerialID.Text == "" ? "0" : SerialID.Text);
                        ObjDetails.LabServiceID = Convert.ToInt32(ID.Text == "" ? "0" : ID.Text);
                        Listbill.Add(ObjDetails);
                    }
                }
                objrec.XMLData = XmlConvertor.LabServiceRecordDatatoXML(Listbill).ToString();
                objrec.UHID = Convert.ToInt64(txt_UHID.Text == "" ? "0" : txt_UHID.Text);
                objrec.TotalRefundable = Convert.ToDecimal(txt_totalrefundable.Text == "" ? "0" : txt_totalrefundable.Text);
                objrec.BillNo = txt_BillNo.Text == "" ? "" : txt_BillNo.Text;
                objrec.Remarks = txt_Remarks.Text == "" ? null : txt_Remarks.Text;
                objrec.FinancialYearID = LogData.FinancialYearID;
                objrec.EmployeeID = LogData.EmployeeID;
                objrec.HospitalID = LogData.HospitalID;
                objrec.IPaddress = LogData.IPaddress;
                objrec.ActionType = Enumaction.Insert;

                List<LabBillingData> result = objiprecBO.UpdateLabServiceCancelRecord(objrec);
                if (result.Count > 0)
                {
                    txt_adjtno.Text = result[0].AdjustmentNo;
                    Messagealert_.ShowMessage(lblmessage, "update", 1);
                    btnsave.Attributes["disabled"] = "disabled";
                    btnprint.Attributes.Remove("disabled");
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "SucessAlert";
                    txt_BillNo.Text = "";
                    gvlabservicelist.DataSource = null;
                    gvlabservicelist.DataBind();
                    gvlabservicelist.Visible = false;
                    txtquantity.Text = "0";
                    txt_totalrefundable.Text = "0.00";
         
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
                lblmessage.Text = ExceptionMessage.GetMessage(ex);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
            }

        }
        protected void btnreset_Click(object sender, EventArgs e)
        {
            txt_BillNo.Text = "";
            txtname.Text = "";
            txt_gender.Text = "";
            txt_age.Text = "";
            txtcontactno.Text = "";
            txt_UHID.Text = "";
            txtquantity.Text = "0";
            txt_adjtno.Text = "";
            btnsave.Attributes["disabled"] = "disabled";
            btnprint.Attributes["disabled"] = "disabled";
        
            txt_totalrefundable.Text = "0.00";
            gvlabservicelist.DataSource = null;
            gvlabservicelist.DataBind();
            gvlabservicelist.Visible = false;
            lblmessage.Visible = false;
            divmsg1.Visible = false;
            txtquantity.Text = "";
            divmsg1.Visible = true;
            divmsg1.Attributes["class"] = "Blank";
            txt_BillNo.ReadOnly = false;
        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            if (LogData.SearchEnable == 0)
            {
                Messagealert_.ShowMessage(lblmessage2, "SearchEnable", 0);
                divmsg2.Visible = true;
                divmsg2.Attributes["class"] = "FailAlert";
                return;
            }
            else
            {
                lblmessage2.Visible = false;
            }
            bindgrid1();
        }
        protected void bindgrid1()
        {
            try
            {
                if (txtdatefrom.Text != "")
                {
                    if (Commonfunction.isValidDate(txtdatefrom.Text) == false)
                    {
                        Messagealert_.ShowMessage(lblmessage2, "ValidDate", 0);
                        divmsg2.Attributes["class"] = "FailAlert";
                        divmsg2.Visible = true;
                        txtdatefrom.Focus();
                        return;
                    }
                }
                else
                {
                    divmsg2.Visible = false;
                }
                if (txtto.Text != "")
                {
                    if (Commonfunction.isValidDate(txtto.Text) == false)
                    {
                        Messagealert_.ShowMessage(lblmessage2, "ValidDate", 0);
                        divmsg2.Attributes["class"] = "FailAlert";
                        divmsg2.Visible = true;
                        txtto.Focus();
                        return;
                    }
                }
                else
                {
                    divmsg2.Visible = false;
                }
                List<LabBillingData> objdeposit = GetAdjustmentList(0);
                if (objdeposit.Count > 0)
                {
                    gvadjustmentlist.DataSource = objdeposit;
                    gvadjustmentlist.DataBind();
                    gvadjustmentlist.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total:" + objdeposit[0].MaximumRows.ToString() + " Record(s) found.", 1);
                    divmsg3.Attributes["class"] = "SucessAlert";
                    btnprints.Attributes.Remove("disabled");
                    if (LogData.PrintEnable == 0)
                    {
                        btnprints.Attributes["disabled"] = "disabled";
                    }
                    else
                    {
                        btnprints.Attributes.Remove("disabled");
                    }
                    divmsg3.Visible = true;
                    ddlexport.Visible = true;
                    btnexport.Visible = true;
                }
                else
                {
                    gvadjustmentlist.DataSource = null;
                    gvadjustmentlist.DataBind();
                    gvadjustmentlist.Visible = true;
                    ddlexport.Visible = false;
                    btnexport.Visible = false;
                    lblresult.Visible = false;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
            }
        }
        public List<LabBillingData> GetAdjustmentList(int curIndex)
        {
            LabBillingData objpat = new LabBillingData();
            OPDbillingBO objbillingBO = new OPDbillingBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime from = txtdatefrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txtdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txtto.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objpat.AdjustmentNo = txt_adjustno.Text == "" ? "0" : txt_adjustno.Text;
            objpat.UHID = Convert.ToInt64(txtautoUHID.Text == "" ? "0" : txtautoUHID.Text);
            objpat.PatientName = txtpatientNames.Text == "" ? null : txtpatientNames.Text.Trim();
            objpat.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
            objpat.DateFrom = from;
            objpat.DateTo = To;
            return objbillingBO.GetAdjustmentList(objpat);
        }
        protected void btnresets_Click(object sender, EventArgs e)
        {
            txtautoUHID.Text = "";
            txtdatefrom.Text = "";
            txt_adjustno.Text = "";
            txtpatientNames.Text = "";
            txtto.Text = "";
            ddlstatus.SelectedIndex = 0;
            gvadjustmentlist.DataSource = null;
            gvadjustmentlist.DataBind();
            gvadjustmentlist.Visible = false;
            lblresult.Visible = false;
            txtpatientNames.Text = "";
            ddlexport.SelectedIndex = 0;
            ddlexport.Visible = false;
            btnexport.Visible = false;
            lblmessage2.Visible = false;
            lblresult.Visible = false;
            divmsg2.Visible = false;
            divmsg3.Visible = false;
            btnprints.Attributes["disabled"] = "disabled";
    
        }

        protected void gvadjustmentlist_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvadjustmentlist.PageIndex = e.NewPageIndex;
            bindgrid1();
        }
  
        protected void gvadjustmentlist_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Deletes")
                {
                    if (LogData.DeleteEnable == 0)
                    {
                        Messagealert_.ShowMessage(lblmessage2, "DeleteEnable", 0);
                        divmsg2.Visible = true;
                        divmsg2.Attributes["class"] = "FailAlert";
                        return;
                    }
                    else
                    {
                        lblmessage2.Visible = false;
                    }
                    LabBillingData objbill = new LabBillingData();
                    OPDbillingBO objstdBO = new OPDbillingBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = gvadjustmentlist.Rows[i];
                    Label ServiceID = (Label)gr.Cells[0].FindControl("lblID");
                    Label ID = (Label)gr.Cells[0].FindControl("lbl_ID");
                    Label AdjustmentNo = (Label)gr.Cells[0].FindControl("lblAdjno");
                    Label UHID = (Label)gr.Cells[0].FindControl("lbluhid");
                    Label name = (Label)gr.Cells[0].FindControl("lblname");
                    Label netcharges = (Label)gr.Cells[0].FindControl("lbl_netcharges");
                    Label refundedby = (Label)gr.Cells[0].FindControl("lblrefundedBy");
                    Label addeddate = (Label)gr.Cells[0].FindControl("lbladt");
                    TextBox txtremarks = (TextBox)gr.Cells[0].FindControl("txtremarks");
                    txtremarks.Enabled = true;
                    if (txtremarks.Text == "")
                    {
                        Messagealert_.ShowMessage(lblresult, "Remarks", 0);
                        divmsg3.Attributes["class"] = "FailAlert";
                        txtremarks.Focus();
                        return;
                    }
                    else
                    {
                        objbill.Remarks = txtremarks.Text;
                    }
                    objbill.AdjustmentNo = AdjustmentNo.Text.Trim();
                    objbill.UHID = Convert.ToInt64(UHID.Text == "" ? "0" : UHID.Text);
                    objbill.LabServiceID = Convert.ToInt32(ServiceID.Text == "" ? "0" : ServiceID.Text);
                    objbill.ID = Convert.ToInt64(ID.Text == "" ? "0" : ID.Text);
                    objbill.NetLabServiceCharge = Convert.ToDecimal(netcharges.Text == "" ? "0" : netcharges.Text);
                    objbill.EmployeeID = LogData.UserLoginId;
                    int Result = objstdBO.DeleteAdjustmentByID(objbill);
                    if (Result == 1)
                    {
                        Messagealert_.ShowMessage(lblmessage2, "delete", 1);
                        divmsg2.Visible = true;
                        divmsg2.Attributes["class"] = "SucessAlert";
                        bindgrid1();
                    }
                    else
                    {
                        Messagealert_.ShowMessage(lblmessage2, "system", 0);
                        divmsg2.Attributes["class"] = "FailAlert";
                        divmsg2.Visible = true;
                    }

                }

            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblresult, "system", 0);
            }
        }
        protected DataTable GetDatafromDatabase()
        {
            List<LabBillingData> DepositDetails = GetAdjustmentList(0);
            List<LabServiceAdjustmentDataTOeXCEL> ListexcelData = new List<LabServiceAdjustmentDataTOeXCEL>();
            int i = 0;
            foreach (LabBillingData row in DepositDetails)
            {
                LabServiceAdjustmentDataTOeXCEL Ecxeclpat = new LabServiceAdjustmentDataTOeXCEL();
                Ecxeclpat.AdjustmentNo = DepositDetails[i].AdjustmentNo;
                Ecxeclpat.BillNo = DepositDetails[i].BillNo;
                Ecxeclpat.UHID = DepositDetails[i].UHID;
                Ecxeclpat.PatientName = DepositDetails[i].PatientName;
                Ecxeclpat.NetCharges = DepositDetails[i].NetLabServiceCharge;
                Ecxeclpat.RefundedBy = DepositDetails[i].RefundedBy;
                Ecxeclpat.AddedDate = DepositDetails[i].AddedDate;
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
        protected void btnexport_Click(object sender, EventArgs e)
        {
            if (LogData.ExportEnable == 0)
            {
                Messagealert_.ShowMessage(lblmessage2, "ExportEnable", 0);
                divmsg2.Visible = true;
                divmsg2.Attributes["class"] = "FailAlert";
                return;
            }
            else
            {
                lblmessage2.Visible = false;
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
        public void ExportToPdf()
        {
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    gvadjustmentlist.BorderStyle = BorderStyle.None;
                    //Hide the Column containing CheckBox
                    gvadjustmentlist.Columns[8].Visible = false;
                    gvadjustmentlist.Columns[9].Visible = false;
                    gvadjustmentlist.Columns[10].Visible = false;
                 
                    gvadjustmentlist.RenderControl(hw);
                    gvadjustmentlist.HeaderRow.Style.Add("width", "15%");
                    gvadjustmentlist.HeaderRow.Style.Add("font-size", "10px");
                    gvadjustmentlist.Style.Add("text-decoration", "none");
                    gvadjustmentlist.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    gvadjustmentlist.Style.Add("font-size", "8px");
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 7f, 7f, 7f, 0f);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=LabServiceAdjustmentList.pdf");
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
        protected void ExportoExcel()
        {
            DataTable dt = GetDatafromDatabase();
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "Deposit Details");
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=LabServiceAdjustmentList.xlsx");
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
   

    }
}
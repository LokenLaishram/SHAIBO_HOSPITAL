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
using Mediqura.Web.MedCommon;
using Mediqura.CommonData.Common;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using Mediqura.BOL.CommonBO;
using Mediqura.CommonData.PatientData;
using Mediqura.BOL.PatientBO;
using Mediqura.CommonData.MedBillData;
using Mediqura.Utility;
using System;
using System.Collections.Generic;

namespace Mediqura.Web.MedBills
{
    public partial class OTServiceRecord : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindddl();
                txttotalquantity.Text = "0";

            }
        }
        private void bindddl()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddl_servicetype, mstlookup.GetLookupsList(LookupName.OTServiceType));
            ddl_servicetype.SelectedIndex = 1;
            Commonfunction.PopulateDdl(ddldoctor, mstlookup.GetLookupsList(LookupName.Doctor));
            ddldoctor.Attributes["disabled"] = "disabled";
            ddl_servicetype.Attributes["disabled"] = "disabled";
            btnsave.Attributes["disabled"] = "disabled";
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetIPNo(string prefixText, int count, string contextKey)
        {
            IPServiceRecordData Objpaic = new IPServiceRecordData();
            IPServiceRecordBO objInfoBO = new IPServiceRecordBO();
            List<IPServiceRecordData> getResult = new List<IPServiceRecordData>();
            Objpaic.IPNo = prefixText;
            getResult = objInfoBO.getIPNo(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].IPNo.ToString());
            }
            return list;
        }
        protected void txt_autoipno_TextChanged(object sender, EventArgs e)
        {
            PatientData Objpaic = new PatientData();
            RegistrationBO objInfoBO = new RegistrationBO();
            List<PatientData> getResult = new List<PatientData>();
            Objpaic.IPNo = txt_autoipno.Text.Trim() == "" ? "" : txt_autoipno.Text.Trim();
            getResult = objInfoBO.GetPatientDetailsByIPNo(Objpaic);
            if (getResult.Count > 0)
            {
                txtname.Text = getResult[0].PatientName.ToString();
                txt_gender.Text = getResult[0].GenderName.ToString();
                txt_age.Text = getResult[0].Agecount.ToString();
                txt_contact.Text = getResult[0].ContactNo.ToString();
                ddldoctor.SelectedValue = getResult[0].DoctorID.ToString();
            }
            else
            {
                txtname.Text = "";
                txt_autoipno.Text = "";
                txt_gender.Text = "";
                txt_age.Text = "";
                txt_contact.Text = "";
                txt_autoipno.Focus();
            }



        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetServices(string prefixText, int count, string contextKey)
        {
            LabServiceMasterData Objpaic = new LabServiceMasterData();
            LabServiceMasterBO objInfoBO = new LabServiceMasterBO();
            List<LabServiceMasterData> getResult = new List<LabServiceMasterData>();
            Objpaic.ServiceName = prefixText;
            getResult = objInfoBO.GetOTServices(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].ServiceName.ToString());
            }
            return list;
        }
        protected void txtservices_TextChanged(object sender, EventArgs e)
        {
            var source = txtservices.Text.ToString();
            if (source.Contains(":"))
            {
                string ID = source.Substring(source.LastIndexOf(':') + 1);

                OPDbillingBO ObjbillBO = new OPDbillingBO();
                LabBillingData ObjBillData = new LabBillingData();
                ObjBillData.ID = Convert.ToInt32(ID == "" ? "0" : ID);
                List<LabBillingData> result = ObjbillBO.GetOTServiceChargeByID(ObjBillData);
                if (result.Count > 0)
                {
                    txtservicecharge.Text = Commonfunction.Getrounding(result[0].ServiceCharge.ToString());
                    lblservicename.Text = result[0].ServiceName.ToString();
                    txtquantity.Text = "1";
                    txtservicecharge.Focus();
                    txtservices.ReadOnly = true;
                }
                else
                {
                    txtservicecharge.Text = "0.0";
                    txtquantity.Text = "0";
                    txtservices.Text = "";
                    txtservices.ReadOnly = true;
                }
            }
            else
            {
                txtservicecharge.Text = "0.0";
                txtquantity.Text = "0";
                txtservices.Text = "";
            }

        }
        protected void btnadd_Click(object sender, EventArgs e)
        {
            if (txt_autoipno.Text == "")
            {
                Messagealert_.ShowMessage(lblmessage, "IPNo", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                txt_autoipno.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
                div1.Visible = false;
            }
            string ID;
            var source = txtservices.Text.ToString();
            if (source.Contains(":"))
            {
                ID = source.Substring(source.LastIndexOf(':') + 1);
                // Check Duplicate data 
                foreach (GridViewRow row in gvipservicerecordlist.Rows)
                {
                    Label ServiceID = (Label)gvipservicerecordlist.Rows[row.RowIndex].Cells[0].FindControl("lblID");
                    if (Convert.ToInt32(ServiceID.Text) == Convert.ToInt32(ID))
                    {
                        Messagealert_.ShowMessage(lblmessage, "Listcheck", 0);

                        div1.Visible = true;
                        div1.Attributes["class"] = "FailAlert";
                        txtservices.Text = "";
                        txtquantity.Text = "";
                        txtservicecharge.Text = "";
                        txtservices.ReadOnly = false;
                        txtservices.Focus();
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                    }
                }
            }
            else
            {
                txtservices.Text = "";
                return;
            }

            List<IPServiceRecordData> IPServiceList = Session["IPServiceList"] == null ? new List<IPServiceRecordData>() : (List<IPServiceRecordData>)Session["IPServiceList"];
            IPServiceRecordData ObjService = new IPServiceRecordData();
            ObjService.ServiceCharge = Convert.ToDecimal(txtservicecharge.Text.ToString() == "" ? "0" : txtservicecharge.Text.ToString());
            ObjService.Quantity = Convert.ToInt32(txtquantity.Text == "" ? "0" : txtquantity.Text);
            ObjService.ServiceID = Convert.ToInt32(ID);
            ObjService.NetServiceCharge = Convert.ToDecimal(txtservicecharge.Text.ToString() == "" ? "0" : txtservicecharge.Text.ToString()) * Convert.ToInt32(txtquantity.Text == "" ? "0" : txtquantity.Text);
            ObjService.TestName = lblservicename.Text.Trim();
            txttotalquantity.Text = (Convert.ToInt32(txttotalquantity.Text == "" ? "0" : txttotalquantity.Text) + Convert.ToInt32(txtquantity.Text == "" ? "0" : txtquantity.Text)).ToString();
            IPServiceList.Add(ObjService);
            if (IPServiceList.Count > 0)
            {
                gvipservicerecordlist.DataSource = IPServiceList;
                gvipservicerecordlist.DataBind();
                gvipservicerecordlist.Visible = true;
                Session["IPServiceList"] = IPServiceList;
                btnsave.Attributes.Remove("disabled");
                txtservices.Text = "";
                txtservicecharge.Text = "";
                txtquantity.Text = "";
                txtservices.ReadOnly = false;
                txtservices.Focus();
            }
            else
            {
                gvipservicerecordlist.DataSource = null;
                gvipservicerecordlist.DataBind();
                gvipservicerecordlist.Visible = true;
                txtservices.ReadOnly = true;
            }
        }
        protected void btnsave_Click(object sender, EventArgs e)
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

            if (txt_autoipno.Text == "")
            {
                Messagealert_.ShowMessage(lblmessage, "IPNo", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                txt_autoipno.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
                div1.Visible = false;
            }

            List<IPServiceRecordData> Listser = new List<IPServiceRecordData>();
            IPServiceRecordBO objiprecBO = new IPServiceRecordBO();
            IPServiceRecordData objrec = new IPServiceRecordData();

            try
            {
                // get all the record from the gridview
                foreach (GridViewRow row in gvipservicerecordlist.Rows)
                {
                    IFormatProvider provider = new System.Globalization.CultureInfo("en-GB", true);
                    Label Particulars = (Label)gvipservicerecordlist.Rows[row.RowIndex].Cells[0].FindControl("lblparticulars");
                    Label amount = (Label)gvipservicerecordlist.Rows[row.RowIndex].Cells[0].FindControl("lblamount");
                    Label qty = (Label)gvipservicerecordlist.Rows[row.RowIndex].Cells[0].FindControl("lblquantity");
                    Label NetCharge = (Label)gvipservicerecordlist.Rows[row.RowIndex].Cells[0].FindControl("lblnetcharges");
                    Label SerialID = (Label)gvipservicerecordlist.Rows[row.RowIndex].Cells[0].FindControl("lblserialID");
                    Label ID = (Label)gvipservicerecordlist.Rows[row.RowIndex].Cells[0].FindControl("lblID");
                    IPServiceRecordData ObjDetails = new IPServiceRecordData();

                    ObjDetails.TestName = Particulars.Text == "" ? null : Particulars.Text;
                    ObjDetails.ServiceCharge = Convert.ToDecimal(amount.Text == "" ? "0" : amount.Text);
                    ObjDetails.Quantity = Convert.ToInt32(qty.Text == "" ? "0" : qty.Text);
                    ObjDetails.NetServiceCharge = Convert.ToDecimal(NetCharge.Text == "" ? "0" : NetCharge.Text);
                    ObjDetails.SerialID = Convert.ToInt32(SerialID.Text == "" ? "0" : SerialID.Text);
                    ObjDetails.ServiceID = Convert.ToInt32(ID.Text == "" ? "0" : ID.Text);
                    Listser.Add(ObjDetails);
                }
                objrec.XMLData = XmlConvertor.IPServiceRecordDatatoXML(Listser).ToString();
                objrec.IPNo = txt_autoipno.Text == "" ? "0" : txt_autoipno.Text;
                objrec.RefferaDocID = Convert.ToInt32(ddldoctor.SelectedValue == "" ? "0" : ddldoctor.SelectedValue);
                objrec.FinancialYearID = LogData.FinancialYearID;
                objrec.EmployeeID = LogData.EmployeeID;
                objrec.HospitalID = LogData.HospitalID;
                objrec.IPaddress = LogData.IPaddress;
                objrec.ActionType = Enumaction.Insert;

                int result = objiprecBO.UpdateOTServiceRecord(objrec);
                if (result > 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "update", 1);
                    div1.Visible = true;
                    div1.Attributes["class"] = "SucessAlert";
                    gvipservicerecord.DataSource = null;
                    gvipservicerecord.DataBind();
                    gvipservicerecord.Visible = false;
                    Session["IPServiceList"] = null;
                    btnsave.Attributes["disabled"] = "disabled";
                    txt_autoipno.Text = "";
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
        protected void gvipservicerecordlist_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Deletes")
                {
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = gvipservicerecordlist.Rows[i];
                    List<IPServiceRecordData> ItemList = Session["IPServiceList"] == null ? new List<IPServiceRecordData>() : (List<IPServiceRecordData>)Session["IPServiceList"];
                    if (ItemList.Count > 0)
                    {
                        int qty = ItemList[i].Quantity;
                        txttotalquantity.Text = (Convert.ToInt32(txttotalquantity.Text == "" ? "0" : txttotalquantity.Text) - qty).ToString();
                    }
                    ItemList.RemoveAt(i);
                    Session["IPServiceList"] = ItemList;
                    gvipservicerecordlist.DataSource = ItemList;
                    gvipservicerecordlist.DataBind();
                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                lblmessage.Text = ExceptionMessage.GetMessage(ex);
                lblmessage.Visible = true;
                lblmessage.CssClass = "Message";
            }
        }
        protected void btnreset_Click(object sender, EventArgs e)
        {
            txt_autoipno.Text = "";
            txtname.Text = "";
            txt_gender.Text = "";
            txt_age.Text = "";
            txtservicecharge.Text = "";
            txtservices.Text = "";
            txtquantity.Text = "";
            btnsave.Attributes["disabled"] = "disabled";
            Session["IPServiceList"] = null;
            gvipservicerecordlist.DataSource = null;
            gvipservicerecordlist.DataBind();
            gvipservicerecordlist.Visible = false;
            lblmessage.Visible = false;
            div1.Visible = false;
            txtservices.Text = "";
            txtquantity.Text = "";
            txttotalquantity.Text = "";
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddldoctor, mstlookup.GetLookupsList(LookupName.Doctor));
            ddldoctor.SelectedIndex = 0;
            div1.Visible = true;
            div1.Attributes["class"] = "Blank";
        }
        protected void gvipservicerecordlist_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblSerial = (Label)e.Row.FindControl("lblserialID");
                lblSerial.Text = ((gvipservicerecordlist.PageIndex * gvipservicerecordlist.PageSize) + e.Row.RowIndex + 1).ToString();
            }
        }
        protected void btnresets_Click(object sender, EventArgs e)
        {
            txtautoIPNo.Text = "";
            txtdatefrom.Text = "";
            txtto.Text = "";
            ddlstatus.SelectedIndex = 0;
            gvipservicerecord.DataSource = null;
            gvipservicerecord.DataBind();
            gvipservicerecord.Visible = false;
            lblresult.Visible = false;
            txtpatientNames.Text = "";
            ddlexport.SelectedIndex = 0;
            ddlexport.Visible = false;
            btnexport.Visible = false;
            lblmessage2.Visible = false;
            lblresult.Visible = false;
            div1.Visible = false;
            div1.Visible = false;
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
        protected void txtautoIPNo_TextChanged(object sender, EventArgs e)
        {
            bindgrid();

        }
        protected void txtpatientNames_TextChanged(object sender, EventArgs e)
        {
            if (txtpatientNames.Text != "")
            {
                bindgrid();
            }
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
            bindgrid();
        }
        protected void bindgrid()
        {
            try
            {
                if (txtautoIPNo.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage2, "IPNo", 0);
                    divmsg2.Visible = true;
                    divmsg2.Attributes["class"] = "FailAlert";
                    txtautoIPNo.Focus();
                    return;
                }
                else
                {
                    lblmessage2.Visible = false;
                    divmsg2.Visible = false;
                }

                if (txtdatefrom.Text != "")
                {
                    if (Commonfunction.isValidDate(txtdatefrom.Text) == false)
                    {
                        Messagealert_.ShowMessage(lblmessage2, "ValidDatefrom", 0);
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
                        Messagealert_.ShowMessage(lblmessage2, "ValidDateto", 0);
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
                List<IPServiceRecordData> objdeposit = GetOTServiceList(0);
                if (objdeposit.Count > 0)
                {
                    lblmessage2.Visible = false;
                    divmsg2.Visible = false;
                    gvipservicerecord.DataSource = objdeposit;
                    gvipservicerecord.DataBind();
                    gvipservicerecord.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total:" + objdeposit[0].MaximumRows.ToString() + " Record found", 1);
                    divmsg3.Attributes["class"] = "SucessAlert";
                    divmsg3.Visible = true;
                    ddlexport.Visible = true;
                    btnexport.Visible = true;
                }
                else
                {
                    gvipservicerecord.DataSource = null;
                    gvipservicerecord.DataBind();
                    gvipservicerecord.Visible = true;
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
        public List<IPServiceRecordData> GetOTServiceList(int curIndex)
        {
            IPServiceRecordData objpat = new IPServiceRecordData();
            IPServiceRecordBO objBO = new IPServiceRecordBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime from = txtdatefrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txtdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txtto.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objpat.IPNo = txtautoIPNo.Text.Trim() == "" ? null : txtautoIPNo.Text.Trim();
            objpat.PatientName = txtpatientNames.Text.Trim() == "" ? null : txtpatientNames.Text.Trim();
            objpat.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
            objpat.DateFrom = from;
            objpat.DateTo = To;
            return objBO.GetOTServiceList(objpat);
        }
        protected void gvipservicerecord_RowCommand(object sender, GridViewCommandEventArgs e)
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
                    IPServiceRecordData objadmin = new IPServiceRecordData();
                    IPServiceRecordBO obadminBO = new IPServiceRecordBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = gvipservicerecord.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("lblID");
                    Label SerialID = (Label)gr.Cells[0].FindControl("lbl_serialID");
                    Label IPNo = (Label)gr.Cells[0].FindControl("lblIPNo");
                    Label name = (Label)gr.Cells[0].FindControl("lblname");
                    Label service = (Label)gr.Cells[0].FindControl("lblservices");
                    Label charge = (Label)gr.Cells[0].FindControl("lblcharges");
                    Label quantity = (Label)gr.Cells[0].FindControl("lblquantity");
                    Label netservicecharge = (Label)gr.Cells[0].FindControl("lblamount");
                    Label addedby = (Label)gr.Cells[0].FindControl("lbladdedby");
                    Label addeddate = (Label)gr.Cells[0].FindControl("lbladt");
                    TextBox txtremarks = (TextBox)gr.Cells[0].FindControl("txtremarks");
                    txtremarks.Enabled = true;
                    if (txtremarks.Text == "")
                    {
                        Messagealert_.ShowMessage(lblresult, "Remarks", 0);
                        divmsg3.Attributes["class"] = "FailAlert";
                        divmsg3.Visible = true;
                        txtremarks.Focus();
                        return;
                    }
                    else
                    {
                        objadmin.Remarks = txtremarks.Text;
                    }

                    objadmin.IPNo = IPNo.Text == "" ? "" : IPNo.Text;
                    objadmin.ID = Convert.ToInt64(ID.Text);
                    objadmin.EmployeeID = LogData.UserLoginId;
                    objadmin.EmployeeID = LogData.EmployeeID;
                    objadmin.HospitalID = LogData.HospitalID;
                    objadmin.IPaddress = LogData.IPaddress;

                    int Result = obadminBO.DeleteOTServiceRecordByIPNo(objadmin);
                    if (Result == 1)
                    {
                        bindgrid();
                        Messagealert_.ShowMessage(lblmessage2, "delete", 1);
                        divmsg2.Attributes["class"] = "SucessAlert";
                        divmsg2.Visible = true;
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
            List<IPServiceRecordData> ServiceDetails = GetOTServiceList(0);
            List<IPDServiceListDataTOeXCEL> ListexcelData = new List<IPDServiceListDataTOeXCEL>();
            int i = 0;
            foreach (IPServiceRecordData row in ServiceDetails)
            {
                IPDServiceListDataTOeXCEL Ecxeclpat = new IPDServiceListDataTOeXCEL();
                Ecxeclpat.IPNo = ServiceDetails[i].IPNo;
                Ecxeclpat.UHID = ServiceDetails[i].UHID;
                Ecxeclpat.PatientName = ServiceDetails[i].PatientName;
                Ecxeclpat.ServiceName = ServiceDetails[i].ServiceName;
                Ecxeclpat.ServiceCharge = ServiceDetails[i].ServiceCharge;
                Ecxeclpat.NetServiceCharge = ServiceDetails[i].NetServiceCharge;
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
                    gvipservicerecordlist.BorderStyle = BorderStyle.None;
                    //Hide the Column containing CheckBox
                    gvipservicerecordlist.Columns[9].Visible = false;
                    gvipservicerecordlist.Columns[10].Visible = false;
                    gvipservicerecordlist.RenderControl(hw);
                    gvipservicerecordlist.HeaderRow.Style.Add("width", "15%");
                    gvipservicerecordlist.HeaderRow.Style.Add("font-size", "10px");
                    gvipservicerecordlist.Style.Add("text-decoration", "none");
                    gvipservicerecordlist.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    gvipservicerecordlist.Style.Add("font-size", "8px");
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 7f, 7f, 7f, 0f);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=OPDBillDetails.pdf");
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
                Response.AddHeader("content-disposition", "attachment;filename=IPDServiceRecordDetails.xlsx");
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
        protected void gvipservicerecord_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvipservicerecord.PageIndex = e.NewPageIndex;
            bindgrid();
        }
    }

}


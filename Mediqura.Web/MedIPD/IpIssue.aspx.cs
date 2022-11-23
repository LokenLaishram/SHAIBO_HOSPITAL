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
using Mediqura.Web.MedCommon;
using Mediqura.CommonData.Common;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using Mediqura.BOL.CommonBO;
using Mediqura.CommonData.PatientData;
using Mediqura.BOL.PatientBO;
using Mediqura.CommonData.MedBillData;
using Mediqura.Utility;
using Mediqura.CommonData.MedStore;
using Mediqura.BOL.MedStore;

namespace Mediqura.Web.MedIPD
{
    public partial class IpIssue : BasePage
    {
        Int64 Global_ID;
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
            Commonfunction.PopulateDdl(ddldoctor, mstlookup.GetLookupsList(LookupName.Doctor));
            Commonfunction.PopulateDdl(ddl_servicetype, mstlookup.GetLookupsList(LookupName.PHRServiceType));
            ddl_servicetype.SelectedIndex = 1;
            Commonfunction.PopulateDdl(ddl_servicetypes, mstlookup.GetLookupsList(LookupName.PHRServiceType));
            ddl_servicetypes.SelectedIndex = 1;
            hdnservicetype.Value = ddl_servicetypes.SelectedValue;
            ddl_servicetype.Attributes["disabled"] = "disabled";
            ddl_servicetypes.Attributes["disabled"] = "disabled";
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
        protected void txt_Item_TextChanged(object sender, EventArgs e)
        {

        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetItemDetails(string prefixText, int count, string contextKey)
        {
            StockIssueData Objpaic = new StockIssueData();
            StockIssueBO objInfoBO = new StockIssueBO();
            List<StockIssueData> getResult = new List<StockIssueData>();
            Objpaic.ItemName = prefixText;
            getResult = objInfoBO.GetItemDetails(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].ItemName);
            }
            return list;
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetSearchItemDetails(string prefixText, int count, string contextKey)
        {
            StockIssueData Objpaic = new StockIssueData();
            StockIssueBO objInfoBO = new StockIssueBO();
            List<StockIssueData> getResult = new List<StockIssueData>();
            Objpaic.ItemName = prefixText;
            getResult = objInfoBO.SearchIssueditem(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].ItemName);
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
                txt_contactno.Text = getResult[0].Address.ToString();
                ddldoctor.SelectedValue = getResult[0].DoctorID.ToString();
                txt_itemnames.Focus();
            }
            else
            {
                txtname.Text = "";
                txt_autoipno.Text = "";
                txt_gender.Text = "";
                txt_age.Text = "";
                txt_contactno.Text = "";
                ddldoctor.SelectedIndex = 1;
                txt_autoipno.Focus();
            }

        }
        protected void txtquantity_TextChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtquantity.Text == "" ? "0" : txtquantity.Text) > 0)
            {
                addnewitem();
            }
        }
        protected void txtItemName_TextChanged(object sender, EventArgs e)
        {
            var source = txtItemName.Text.ToString();
            if (source.Contains(":"))
            {
                string ID = source.Substring(source.LastIndexOf(':') + 1);

                OPDbillingBO ObjbillBO = new OPDbillingBO();
                PHRbillingData ObjBillData = new PHRbillingData();
                ObjBillData.StockID = Convert.ToInt32(ID == "" ? "0" : ID);
                List<PHRbillingData> result = ObjbillBO.GetOPServiceChargeByID(ObjBillData);
                if (result.Count > 0)
                {
                    txtservicecharge.Text = Commonfunction.Getrounding(result[0].ServiceCharge.ToString());
                    lblservicename.Text = result[0].ServiceName.ToString();
                    lblItemID.Text = result[0].ItemID.ToString();
                    txtdescription.Text = result[0].Remarks.ToString();
                    lblSubStockID.Text = result[0].SubStockID.ToString();
                    txtquantity.Text = "";
                    txtItemName.ReadOnly = true;
                    txtquantity.Focus();
                }
            }
            else
            {
                txtItemName.ReadOnly = false;
                txtItemName.Text = "";
                txtdescription.Text = "";
                txtItemName.Focus();
            }
        }
        protected void btnadd_Click(object sender, EventArgs e)
        {
            addnewitem();
        }
        protected void addnewitem()
        {
            if (txtItemName.Text == "")
            {
                Messagealert_.ShowMessage(lblmessage, "ItemName", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                txtItemName.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
                div1.Visible = false;
            }
            if (Convert.ToInt32(txtquantity.Text == "" ? "0" : txtquantity.Text) <= 0)
            {
                Messagealert_.ShowMessage(lblmessage, "ReqdQty", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                txtquantity.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
                div1.Visible = false;
            }
            string ID;
            var source = txtItemName.Text.ToString();
            if (source.Contains(":"))
            {
                ID = source.Substring(source.LastIndexOf(':') + 1);
                // Check Duplicate data 
                foreach (GridViewRow row in gvipservicerecord.Rows)
                {
                    Label SubStockID = (Label)gvipservicerecord.Rows[row.RowIndex].Cells[0].FindControl("lbl_SubStockID");
                    Label ItemID = (Label)gvipservicerecord.Rows[row.RowIndex].Cells[0].FindControl("lblID");
                    if (Convert.ToInt32(SubStockID.Text == "" ? "0" : SubStockID.Text) == Convert.ToInt32(ID) || Convert.ToInt32(ItemID.Text == "" ? "0" : ItemID.Text) == Convert.ToInt32(lblItemID.Text == "" ? "0" : lblItemID.Text))
                    {
                        txtItemName.Text = "";
                        txtItemName.ReadOnly = false;
                        txtservicecharge.Text = "";
                        txtdescription.Text = "";
                        txtItemName.Focus();
                        Messagealert_.ShowMessage(lblmessage, "Listcheck", 0);
                        div1.Visible = true;
                        div1.Attributes["class"] = "FailAlert";
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
                txtItemName.Text = "";
                return;
            }
            List<IPServiceRecordData> IPServiceList = Session["IPServiceList"] == null ? new List<IPServiceRecordData>() : (List<IPServiceRecordData>)Session["IPServiceList"];
            IPServiceRecordData ObjService = new IPServiceRecordData();
            ObjService.ServiceCharge = Convert.ToDecimal(txtservicecharge.Text.ToString() == "" ? "0" : txtservicecharge.Text.ToString());
            int qty = Convert.ToInt32(txtquantity.Text == "" ? "0" : txtquantity.Text) <= 0 ? 1 : Convert.ToInt32(txtquantity.Text == "" ? "0" : txtquantity.Text);
            ObjService.Quantity = qty;
            ObjService.ItemID = Convert.ToInt32(lblItemID.Text);
            ObjService.NetServiceCharge = Convert.ToDecimal(txtservicecharge.Text.ToString() == "" ? "0" : txtservicecharge.Text.ToString()) * qty;
            ObjService.DoctorID = Convert.ToInt64(ddldoctor.SelectedValue == "" ? "0" : ddldoctor.SelectedValue);
            ObjService.ServiceName = lblservicename.Text.Trim();
            ObjService.SubStockID = Convert.ToInt64(lblSubStockID.Text);
            txttotalquantity.Text = (Convert.ToInt32(txttotalquantity.Text == "" ? "0" : txttotalquantity.Text) + qty).ToString();
            IPServiceList.Add(ObjService);
            if (IPServiceList.Count > 0)
            {
                gvipservicerecord.DataSource = IPServiceList;
                gvipservicerecord.DataBind();
                gvipservicerecord.Visible = true;
                Session["IPServiceList"] = IPServiceList;
                txtItemName.Text = "";
                txtItemName.ReadOnly = false;
                txtdescription.Text = "";
                txtservicecharge.Text = "";
                txtquantity.Text = "";
                btnsave.Attributes.Remove("disabled");
                txtItemName.Focus();
            }
            else
            {
                btnsave.Attributes["disabled"] = "disabled";
                txtItemName.ReadOnly = false;
                gvipservicerecord.DataSource = null;
                gvipservicerecord.DataBind();
                gvipservicerecord.Visible = true;
            }
        }
        protected void gvipservicerecord_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblSerial = (Label)e.Row.FindControl("lblserialID");
                lblSerial.Text = ((gvipservicerecord.PageIndex * gvipservicerecordlist.PageSize) + e.Row.RowIndex + 1).ToString();
            }
        }
        protected void gvipservicerecord_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Deletes")
                {
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = gvipservicerecord.Rows[i];
                    List<IPServiceRecordData> ItemList = Session["IPServiceList"] == null ? new List<IPServiceRecordData>() : (List<IPServiceRecordData>)Session["IPServiceList"];
                    if (ItemList.Count > 0)
                    {
                        int qty = ItemList[i].Quantity;
                        txttotalquantity.Text = (Convert.ToInt32(txttotalquantity.Text == "" ? "0" : txttotalquantity.Text) - qty).ToString();
                    }
                    ItemList.RemoveAt(i);
                    Session["IPServiceList"] = ItemList;
                    if (ItemList.Count > 0)
                    {
                        gvipservicerecord.DataSource = ItemList;
                        gvipservicerecord.DataBind();
                        btnsave.Attributes.Remove("disabled");
                    }
                    else
                    {
                        gvipservicerecord.DataSource = ItemList;
                        gvipservicerecord.DataBind();
                        btnsave.Attributes["disabled"] = "disabled";
                    }
                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                lblmessage.Text = ExceptionMessage.GetMessage(ex);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
            }
        }
        protected void btnreset_Click(object sender, EventArgs e)
        {
            reset();
        }
        void reset()
        {
            txt_autoipno.Text = "";
            txtname.Text = "";
            txt_gender.Text = "";
            txt_age.Text = "";
            txtservicecharge.Text = "";
            txtquantity.Text = "";
            Session["IPServiceList"] = null;
            gvipservicerecord.DataSource = null;
            gvipservicerecord.DataBind();
            gvipservicerecord.Visible = false;
            lblmessage.Visible = false;
            div1.Visible = false;
            txtquantity.Text = "";
            txttotalquantity.Text = "";
            div1.Visible = true;
            txt_contactno.Text = "";
            div1.Attributes["class"] = "Blank";
            txtdescription.Text = "";
            txtItemName.ReadOnly = false;
            ddldoctor.SelectedIndex = 0;

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
            if (ddldoctor.SelectedValue == "")
            {
                Messagealert_.ShowMessage(lblmessage, "Consultant", 0);
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
                foreach (GridViewRow row in gvipservicerecord.Rows)
                {
                    IFormatProvider provider = new System.Globalization.CultureInfo("en-GB", true);
                    Label Particulars = (Label)gvipservicerecord.Rows[row.RowIndex].Cells[0].FindControl("lblparticulars");
                    Label amount = (Label)gvipservicerecord.Rows[row.RowIndex].Cells[0].FindControl("lblamount");
                    Label qty = (Label)gvipservicerecord.Rows[row.RowIndex].Cells[0].FindControl("lblquantity");
                    Label NetCharge = (Label)gvipservicerecord.Rows[row.RowIndex].Cells[0].FindControl("lblnetcharges");
                    Label SerialID = (Label)gvipservicerecord.Rows[row.RowIndex].Cells[0].FindControl("lblserialID");
                    Label ID = (Label)gvipservicerecord.Rows[row.RowIndex].Cells[0].FindControl("lblID");
                    Label DoctorID = (Label)gvipservicerecord.Rows[row.RowIndex].Cells[0].FindControl("lbl_doctorID");
                    Label SubStockID = (Label)gvipservicerecord.Rows[row.RowIndex].Cells[0].FindControl("lbl_SubStockID");
                    IPServiceRecordData ObjDetails = new IPServiceRecordData();

                    ObjDetails.ServiceName = Particulars.Text == "" ? null : Particulars.Text;
                    ObjDetails.ServiceCharge = Convert.ToDecimal(amount.Text == "" ? "0" : amount.Text);
                    ObjDetails.Quantity = Convert.ToInt32(qty.Text == "" ? "0" : qty.Text);
                    ObjDetails.NetServiceCharge = Convert.ToDecimal(NetCharge.Text == "" ? "0" : NetCharge.Text);
                    ObjDetails.SerialID = Convert.ToInt32(SerialID.Text == "" ? "0" : SerialID.Text);
                    ObjDetails.ItemID = Convert.ToInt32(ID.Text == "" ? "0" : ID.Text);
                    ObjDetails.SubStockID = Convert.ToInt32(SubStockID.Text == "" ? "0" : SubStockID.Text);
                    ObjDetails.DoctorID = Convert.ToInt64(DoctorID.Text == "" ? "0" : DoctorID.Text);
                    Listser.Add(ObjDetails);
                }
                objrec.XMLData = XmlConvertor.IPIssueServiceRecordDatatoXML(Listser).ToString();
                objrec.IPNo = txt_autoipno.Text == "" ? "0" : txt_autoipno.Text;
                objrec.FinancialYearID = LogData.FinancialYearID;
                objrec.EmployeeID = LogData.EmployeeID;
                objrec.HospitalID = LogData.HospitalID;
                objrec.IPaddress = LogData.IPaddress;
                objrec.ActionType = Enumaction.Insert;
                objrec.Quantity = Convert.ToInt32(txttotalquantity.Text == "" ? "0" : txttotalquantity.Text);
                objrec.ServiceTypeID = Convert.ToInt32(ddl_servicetype.SelectedValue == "" ? "0" : ddl_servicetype.SelectedValue);

                int result = objiprecBO.UpdateIPIssueRecord(objrec);
                if (result > 0)
                {
                    reset();
                    Session["ServiceList"] = null;
                    Messagealert_.ShowMessage(lblmessage, "update", 1);
                    div1.Visible = true;
                    div1.Attributes["class"] = "SucessAlert";
                    txt_autoipno.Text = "";
                    btnsave.Attributes["disabled"] = "disabled";
                }
                else
                {
                    Messagealert_.ShowMessage(lblmessage, "Error", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    btnsave.Attributes.Remove("disabled");
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


        //---------------------------------NEXT TABSTARTS-----------------------

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
            // (txtpatientNames.Text != "")
            //{
            //    ifbindgrid();
            //}
        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            bindgrid();
        }
        protected void bindgrid()
        {
            try
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
                if (txtautoIPNo.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage2, "IPNo", 0);
                    divmsg2.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    txtautoIPNo.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                    div1.Visible = false;
                }
                if (txtdatefrom.Text != "")
                {
                    if (Commonfunction.isValidDate(txtdatefrom.Text) == false)
                    {
                        Messagealert_.ShowMessage(lblmessage2, "VaidDate", 0);
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
                        Messagealert_.ShowMessage(lblmessage2, "VaidDate", 0);
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
                List<IPServiceRecordData> objdeposit = GetIPDServiceList(0);
                if (objdeposit.Count > 0)
                {
                    gvipservicerecordlist.DataSource = objdeposit;
                    gvipservicerecordlist.DataBind();
                    gvipservicerecordlist.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total:" + objdeposit[0].MaximumRows.ToString() + " Record(s) found.", 1);
                    divmsg3.Attributes["class"] = "SucessAlert";
                    divmsg3.Visible = true;
                    ddlexport.Visible = true;
                    btnexport.Visible = true;
                }
                else
                {
                    gvipservicerecordlist.DataSource = null;
                    gvipservicerecordlist.DataBind();
                    gvipservicerecordlist.Visible = true;
                    ddlexport.Visible = false;
                    btnexport.Visible = false;
                    lblresult.Visible = false;
                    div1.Visible = false;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
                divmsg2.Attributes["class"] = "FailAlert";
                divmsg2.Visible = true;
            }
        }
        public List<IPServiceRecordData> GetIPDServiceList(int curIndex)
        {
            IPServiceRecordData objpat = new IPServiceRecordData();
            IPServiceRecordBO objBO = new IPServiceRecordBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime from = txtdatefrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txtdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txtto.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objpat.IPNo = txtautoIPNo.Text == "" ? null : txtautoIPNo.Text.Trim();
            var source = txt_itemnames.Text.ToString();
            if (source.Contains(":"))
            {
                ID = source.Substring(source.LastIndexOf(':') + 1);
                objpat.ItemID = Convert.ToInt32(ID);
            }
            else
            {
                txt_itemnames.Text = "";
                objpat.ItemID = 0;
            }
            objpat.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
            objpat.ServiceTypeID = Convert.ToInt32(hdnservicetype.Value == null ? "0" : hdnservicetype.Value);
            objpat.DateFrom = from;
            objpat.DateTo = To;
            return objBO.GetIPDIssueServiceList(objpat);
        }
        protected void gvipservicerecordlist_RowCommand(object sender, GridViewCommandEventArgs e)
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
                    GridViewRow gr = gvipservicerecordlist.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("lbl_IPServiceID");
                    //Label StockID = (Label)gr.Cells[0].FindControl("");
                    Label IPNo = (Label)gr.Cells[0].FindControl("lblIPNo");
                    Label quantity = (Label)gr.Cells[0].FindControl("lblquantity");
                    Label substockID = (Label)gr.Cells[0].FindControl("lbl_substockID");
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
                        objadmin.Remarks = txtremarks.Text;
                    }
                    objadmin.IPNo = IPNo.Text == "" ? "" : IPNo.Text;
                    objadmin.EmployeeID = LogData.UserLoginId;
                    objadmin.ID = Convert.ToInt64(ID.Text == "" ? "0" : ID.Text);
                    objadmin.Quantity = Convert.ToInt32(quantity.Text == "" ? "0" : quantity.Text);
                    objadmin.SubStockID = Convert.ToInt64(substockID.Text == "" ? "0" : substockID.Text);
                    objadmin.ServiceTypeID = Convert.ToInt32(hdnservicetype.Value == null ? "0" : hdnservicetype.Value);

                    int Result = obadminBO.DeleteIPDIssueRecordByIPNo(objadmin);
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
                Messagealert_.ShowMessage(lblmessage2, "system", 0);
                divmsg2.Attributes["class"] = "FailAlert";
                divmsg2.Visible = true;
            }
        }
        protected DataTable GetDatafromDatabase()
        {
            List<IPServiceRecordData> ServiceDetails = GetIPDServiceList(0);
            List<IPServiceListDataTOeXCEL> ListexcelData = new List<IPServiceListDataTOeXCEL>();
            int i = 0;
            foreach (IPServiceRecordData row in ServiceDetails)
            {
                IPServiceListDataTOeXCEL Ecxeclpat = new IPServiceListDataTOeXCEL();
                Ecxeclpat.IPNo = ServiceDetails[i].IPNo;
                Ecxeclpat.UHID = ServiceDetails[i].UHID;
                Ecxeclpat.PatientName = ServiceDetails[i].PatientName;
                Ecxeclpat.ServiceName = ServiceDetails[i].ServiceName;
                Ecxeclpat.ServiceCharge = ServiceDetails[i].ServiceCharge;
                Ecxeclpat.Quantity = ServiceDetails[i].Quantity;
                Ecxeclpat.NetServiceCharge = ServiceDetails[i].NetServiceCharge;
                Ecxeclpat.AddedDate = ServiceDetails[i].AddedDate;

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
                    gvipservicerecordlist.Columns[11].Visible = false;
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
                    Response.AddHeader("content-disposition", "attachment;filename=IPDIssueDetails.pdf");
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
        protected void gvipservicerecordlist_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvipservicerecordlist.PageIndex = e.NewPageIndex;
            bindgrid();
        }
        protected void ExportoExcel()
        {
            DataTable dt = GetDatafromDatabase();
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "IP pharmacy Details");
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=IPpharmacydetails.xlsx");
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
        protected void btnresets_Click(object sender, EventArgs e)
        {
            txtautoIPNo.Text = "";
            txtdatefrom.Text = "";
            txtto.Text = "";
            gvipservicerecordlist.DataSource = null;
            gvipservicerecordlist.DataBind();
            gvipservicerecordlist.Visible = false;
            divmsg2.Visible = false;
            lblmessage2.Visible = false;
            lblmessage2.Text = "";
            divmsg2.Attributes["class"] = "Blank";
            ddlexport.Visible = false;
            btnexport.Visible = false;
            lblresult.Visible = false;

        }
    }
}
using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedBill;
using Mediqura.BOL.PatientBO;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.PatientData;
using Mediqura.CommonData.MedBillData;
using Mediqura.CommonData.MedEmergencyData;
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
using Mediqura.BOL.MedEmergencyBO;
using Mediqura.BOL.MedBillBO;
using Mediqura.CommonData.MedHrData;
using Mediqura.BOL.MedHrBO;
using Mediqura.CommonData.AdmissionData;
using Mediqura.BOL.AdmissionBO;

namespace Mediqura.Web.MedEmergency
{
    public partial class EmergencyDrugRecord : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindddl();
                if (Request["EM"] != null && Request["EM"] != "")
                {
                    txt_emrgno.Text = Request["EM"].ToString();
                    EmLoadData();
                }
            }
        }
        private void bindddl()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddl_servicetype, mstlookup.GetLookupsList(LookupName.EmrgPHRService));
            Commonfunction.PopulateDdl(ddldoctor, mstlookup.GetLookupsList(LookupName.EmergencyDoc));
            ddl_servicetype.SelectedIndex = 1;
            ddl_servicetype.Attributes["disabled"] = "disabled";

        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetEmrgNo(string prefixText, int count, string contextKey)
        {
            EmrgAdmissionData Objpaic = new EmrgAdmissionData();
            EmrgAdmissionBO objInfoBO = new EmrgAdmissionBO();
            List<EmrgAdmissionData> getResult = new List<EmrgAdmissionData>();
            Objpaic.EmrgNo = prefixText;
            getResult = objInfoBO.GetEmrgNo(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].EmrgNo.ToString());
            }
            return list;
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetServices(string prefixText, int count, string contextKey)
        {
            EmrgAdmissionData Objpaic = new EmrgAdmissionData();
            EmrgAdmissionBO objInfoBO = new EmrgAdmissionBO();
            List<EmrgAdmissionData> getResult = new List<EmrgAdmissionData>();
            Objpaic.ServiceName = prefixText;
            getResult = objInfoBO.GetEmrgPHRServices(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].ServiceName.ToString());
            }
            return list;
        }
        //TAB 2 //
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetEmgPatientName(string prefixText, int count, string contextKey)
        {
            PatientData Objpaic = new PatientData();
            RegistrationBO objInfoBO = new RegistrationBO();
            List<PatientData> getResult = new List<PatientData>();
            Objpaic.PatientName = prefixText;
            getResult = objInfoBO.GetEmgPatientName(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].EmgPatientName.ToString());
            }
            return list;
        }
        protected void txt_emrgno_TextChanged(object sender, EventArgs e)
        {
            EmLoadData();
        }

        private void EmLoadData()
        {
            EmrgAdmissionData Objpaic = new EmrgAdmissionData();
            EmrgAdmissionBO objInfoBO = new EmrgAdmissionBO();
            List<EmrgAdmissionData> getResult = new List<EmrgAdmissionData>();
            Objpaic.EmrgNo = txt_emrgno.Text.Trim() == "" ? "" : txt_emrgno.Text.Trim();
            getResult = objInfoBO.GetPatientsDetailsByEmrgNo(Objpaic);
            if (getResult.Count > 0)
            {
                txtname.Text = getResult[0].PatientName.ToString();
                txt_gender.Text = getResult[0].GenderName.ToString();
                txt_age.Text = getResult[0].Agecount.ToString();
                ddldoctor.SelectedValue = getResult[0].DocID.ToString();
                ddldoctor.Attributes["disabled"] = "disabled";
            }
            else
            {
                txtname.Text = "";
                txt_emrgno.Text = "";
                txt_gender.Text = "";
                txt_age.Text = "";
                ddldoctor.SelectedIndex = 0;
            }
        }
        protected void txtservices_TextChanged(object sender, EventArgs e)
        {
            var source = txtservices.Text.ToString();
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
                    lbltax.Text = result[0].Tax.ToString();
                    lblSubStockID.Text = result[0].SubStockID.ToString();
                    txtquantity.Text = "";
                    txtquantity.Focus();
                    txtservices.ReadOnly = true;
                }
            }
            else
            {
                txtservices.ReadOnly = false;
                txtservices.Text = "";
                txtservices.Focus();
            }
        }

        protected void btnadd_Click(object sender, EventArgs e)
        {
            if (txt_emrgno.Text == "")
            {
                Messagealert_.ShowMessage(lblmessage, "EMRGNO", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                txt_emrgno.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
                div1.Visible = false;
            }
            if (ddldoctor.SelectedIndex == 0)
            {

                Messagealert_.ShowMessage(lblmessage, "Doctor", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                ddldoctor.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
                div1.Visible = false;
            }
            if (txtservices.Text == "")
            {
                Messagealert_.ShowMessage(lblmessage, "ItemName", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                txtservices.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
                div1.Visible = false;
            }
            if (txtquantity.Text == "" || Convert.ToInt32(txtquantity.Text == "" ? "0" : txtquantity.Text) <= 0)
            {
                Messagealert_.ShowMessage(lblmessage, "Quantity", 0);
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
            if (txtservicecharge.Text == "")
            {
                Messagealert_.ShowMessage(lblmessage, "Charge", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                txtservicecharge.Focus();
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
                foreach (GridViewRow row in gvEMRGitemlist.Rows)
                {
                    Label StockID = (Label)gvEMRGitemlist.Rows[row.RowIndex].Cells[0].FindControl("lbl_SubStockID");
                    Label ItemID = (Label)gvEMRGitemlist.Rows[row.RowIndex].Cells[0].FindControl("lblID");
                    if (Convert.ToInt64(StockID.Text == "" ? "0" : StockID.Text) == Convert.ToInt64(ID) || Convert.ToInt64(ItemID.Text == "" ? "0" : ItemID.Text) == Convert.ToInt64(lblItemID.Text == "" ? "0" : lblItemID.Text))
                    {
                        Messagealert_.ShowMessage(lblmessage, "Listcheck", 0);
                        div1.Visible = true;
                        div1.Attributes["class"] = "FailAlert";
                        txtservices.Text = "";
                        txtservices.ReadOnly = false;
                        txtservices.Focus();
                        txtquantity.Text = "";
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                        txtquantity.Focus();
                    }
                }
            }
            else
            {
                txtservices.Text = "";
                return;
            }

            List<EmrgAdmissionData> PHRServiceList = Session["ItemList"] == null ? new List<EmrgAdmissionData>() : (List<EmrgAdmissionData>)Session["ItemList"];
            EmrgAdmissionData ObjService = new EmrgAdmissionData();
            ObjService.ServiceCharge = Convert.ToDecimal(txtservicecharge.Text.ToString() == "" ? "0" : txtservicecharge.Text.ToString());
            ObjService.Quantity = Convert.ToInt32(txtquantity.Text == "" ? "0" : txtquantity.Text);
            ObjService.Tax = Convert.ToDecimal(lbltax.Text == "" ? "0" : lbltax.Text);
            ObjService.ItemID = Convert.ToInt32(lblItemID.Text);
            ObjService.SubStockID = Convert.ToInt64(lblSubStockID.Text);
            Decimal Tax_Amount = (Convert.ToDecimal(lbltax.Text == "" ? "0" : lbltax.Text) / 100) * Convert.ToDecimal(txtservicecharge.Text.ToString() == "" ? "0" : txtservicecharge.Text.ToString()) * Convert.ToInt32(txtquantity.Text == "" ? "0" : txtquantity.Text);
            ObjService.TotalTax = Tax_Amount;
            ObjService.NetPHRServiceCharge = Convert.ToDecimal(txtservicecharge.Text.ToString() == "" ? "0" : txtservicecharge.Text.ToString()) * Convert.ToInt32(txtquantity.Text == "" ? "0" : txtquantity.Text) + Tax_Amount;
            ObjService.ServiceName = lblservicename.Text.Trim();
            txt_totalbill.Text = Commonfunction.Getrounding((Tax_Amount + Convert.ToDecimal(txt_totalbill.Text == "" ? "0" : txt_totalbill.Text) + Convert.ToInt32(txtquantity.Text == "" ? "0" : txtquantity.Text) * Convert.ToDecimal(txtservicecharge.Text.ToString() == "" ? "0" : txtservicecharge.Text.ToString())).ToString());
            txt_tax.Text = Commonfunction.Getrounding((Convert.ToDecimal(txt_tax.Text == "" ? "0" : txt_tax.Text) + Tax_Amount).ToString());
            //if (ddl_patienttype.SelectedIndex == 0)
            //{
            //    if (Convert.ToDecimal(txt_balanceinac.Text) > 0)
            //    {
            //        if (Convert.ToDecimal(txt_balanceinac.Text) >= Convert.ToDecimal(txt_totalbill.Text))
            //        {
            //            txt_adjustedamount.Text = Commonfunction.Getrounding((Convert.ToDecimal(txt_totalbill.Text == "" ? "0" : txt_totalbill.Text)).ToString());
            //            txt_paidamount.Text = "0.00";
            //        }
            //        else if (Convert.ToDecimal(txt_balanceinac.Text) < Convert.ToDecimal(txt_totalbill.Text))
            //        {
            //            txt_adjustedamount.Text = Commonfunction.Getrounding((Convert.ToDecimal(txt_balanceinac.Text == "" ? "0" : txt_balanceinac.Text)).ToString());
            //            txt_paidamount.Text = Commonfunction.Getrounding((Convert.ToDecimal(txt_totalbill.Text == "" ? "0" : txt_totalbill.Text) - Convert.ToDecimal(txt_balanceinac.Text.ToString() == "" ? "0" : txt_balanceinac.Text.ToString())).ToString());
            //        }
            //    }
            //    else
            //    {
            //        txt_adjustedamount.Text = "0.00";
            //        txt_paidamount.Text = Commonfunction.Getrounding((Convert.ToDecimal(txt_totalbill.Text == "" ? "0" : txt_totalbill.Text)).ToString());
            //    }
            //}
            //else
            //{
            //    txt_paidamount.Text = Commonfunction.Getrounding((Convert.ToDecimal(txt_totalbill.Text == "" ? "0" : txt_totalbill.Text)).ToString());
            //}
            PHRServiceList.Add(ObjService);
            if (PHRServiceList.Count > 0)
            {
                gvEMRGitemlist.DataSource = PHRServiceList;
                gvEMRGitemlist.DataBind();
                gvEMRGitemlist.Visible = true;
                Session["ItemList"] = PHRServiceList;
                txtservices.Text = "";
                txtservicecharge.Text = "";
                txtquantity.Text = "";
                txtservices.Focus();
                txtquantity.Text = "";
                txtservices.ReadOnly = false;
                btnsave.Attributes.Remove("disabled");
            }
            else
            {
                btnsave.Attributes["disabled"] = "disabled";
                gvEMRGitemlist.DataSource = null;
                gvEMRGitemlist.DataBind();
                gvEMRGitemlist.Visible = true;

            }

        }

        protected void gvEMRGitemlist_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Deletes")
                {
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = gvEMRGitemlist.Rows[i];
                    List<EmrgAdmissionData> ItemList = Session["ItemList"] == null ? new List<EmrgAdmissionData>() : (List<EmrgAdmissionData>)Session["ItemList"];
                    if (ItemList.Count > 0)
                    {
                        Decimal totalamount = ItemList[i].LabServiceCharge;
                        Decimal Tax = ItemList[i].TotalTax;
                        txt_tax.Text = Commonfunction.Getrounding((Convert.ToDecimal(txt_tax.Text == "" ? "0" : txt_tax.Text) - Tax).ToString());
                        txt_totalbill.Text = Commonfunction.Getrounding((Convert.ToDecimal(txt_totalbill.Text == "" ? "0" : txt_totalbill.Text) - totalamount).ToString());

                    }
                    ItemList.RemoveAt(i);
                    Session["ItemList"] = ItemList;
                    gvEMRGitemlist.DataSource = ItemList;
                    gvEMRGitemlist.DataBind();
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

        protected void gvEMRGitemlist_RowDataBound(object sender, GridViewRowEventArgs e)
        {

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
            if (txt_emrgno.Text == "")
            {
                Messagealert_.ShowMessage(lblmessage, "EMRGNO", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                txt_emrgno.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
                div1.Visible = false;
            }
            if (ddldoctor.SelectedIndex == 0)
            {

                Messagealert_.ShowMessage(lblmessage, "Doctor", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                ddldoctor.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
                div1.Visible = false;
            }
            //if (txtservices.Text == "")
            //{
            //    Messagealert_.ShowMessage(lblmessage, "ItemName", 0);
            //    div1.Visible = true;
            //    div1.Attributes["class"] = "FailAlert";
            //    txtservices.Focus();
            //    return;
            //}
            //else
            //{
            //    lblmessage.Visible = false;
            //    div1.Visible = false;
            //}
            //if (txtquantity.Text == "" || Convert.ToInt32(txtquantity.Text == "" ? "0" : txtquantity.Text) <= 0)
            //{
            //    Messagealert_.ShowMessage(lblmessage, "Quantity", 0);
            //    div1.Visible = true;
            //    div1.Attributes["class"] = "FailAlert";
            //    txtquantity.Focus();
            //    return;
            //}
            //else
            //{
            //    lblmessage.Visible = false;
            //    div1.Visible = false;
            //}
            //if (txtservicecharge.Text == "")
            //{
            //    Messagealert_.ShowMessage(lblmessage, "Charge", 0);
            //    div1.Visible = true;
            //    div1.Attributes["class"] = "FailAlert";
            //    txtservicecharge.Focus();
            //    return;
            //}
            //else
            //{
            //    lblmessage.Visible = false;
            //    div1.Visible = false;
            //}
            if (txt_totalbill.Text == "0.00")
            {
                Messagealert_.ShowMessage(lblmessage, "ItemName", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                txtservices.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
                div1.Visible = false;
            }
            List<EmrgAdmissionData> Listservice = new List<EmrgAdmissionData>();
            EmrgAdmissionBO objPhrServiceBO = new EmrgAdmissionBO();
            EmrgAdmissionData objEmrgService = new EmrgAdmissionData();
            try
            {
                // get all the record from the gridview
                foreach (GridViewRow row in gvEMRGitemlist.Rows)
                {
                    IFormatProvider provider = new System.Globalization.CultureInfo("en-GB", true);
                    Label Particulars = (Label)gvEMRGitemlist.Rows[row.RowIndex].Cells[0].FindControl("lbllabparticulars");
                    Label amount = (Label)gvEMRGitemlist.Rows[row.RowIndex].Cells[0].FindControl("lbl_labcharges");
                    Label qty = (Label)gvEMRGitemlist.Rows[row.RowIndex].Cells[0].FindControl("lblquantity");
                    Label NetCharge = (Label)gvEMRGitemlist.Rows[row.RowIndex].Cells[0].FindControl("lblnetcharges");
                    Label Tax = (Label)gvEMRGitemlist.Rows[row.RowIndex].Cells[0].FindControl("lbl_Tax");
                    Label TotalTax = (Label)gvEMRGitemlist.Rows[row.RowIndex].Cells[0].FindControl("lbl_totaltax");
                    Label SerialID = (Label)gvEMRGitemlist.Rows[row.RowIndex].Cells[0].FindControl("lblserialID");
                    Label ID = (Label)gvEMRGitemlist.Rows[row.RowIndex].Cells[0].FindControl("lblID");
                    Label SubStockID = (Label)gvEMRGitemlist.Rows[row.RowIndex].Cells[0].FindControl("lbl_SubStockID");
                    EmrgAdmissionData ObjDetails = new EmrgAdmissionData();

                    ObjDetails.ServiceName = Particulars.Text == "" ? null : Particulars.Text;
                    ObjDetails.ServiceCharge = Convert.ToDecimal(amount.Text == "" ? "0" : amount.Text);
                    ObjDetails.Tax = Convert.ToDecimal(Tax.Text == "" ? "0" : Tax.Text);
                    ObjDetails.Quantity = Convert.ToInt32(qty.Text == "" ? "0" : qty.Text);
                    ObjDetails.NetPHRServiceCharge = Convert.ToDecimal(NetCharge.Text == "" ? "0" : NetCharge.Text);
                    ObjDetails.SerialID = Convert.ToInt32(SerialID.Text == "" ? "0" : SerialID.Text);
                    ObjDetails.ItemID = Convert.ToInt32(ID.Text == "" ? "0" : ID.Text);
                    ObjDetails.SubStockID = Convert.ToInt64(SubStockID.Text == "" ? "0" : SubStockID.Text);
                    Listservice.Add(ObjDetails);
                }
                objEmrgService.XMLData = XmlConvertor.EmrgPhrDatatoXML(Listservice).ToString();
                objEmrgService.EmrgNo = txt_emrgno.Text == "" ? "0" : txt_emrgno.Text;
                objEmrgService.PatientName = txtname.Text == "" ? null : txtname.Text;
                objEmrgService.NetPHRServiceCharge = Convert.ToDecimal(txt_totalbill.Text == "" ? "0" : txt_totalbill.Text);
                objEmrgService.TotalTax = Convert.ToDecimal(txt_tax.Text == "" ? "0" : txt_tax.Text);
                //objEmrgService.ServiceTypeID = 4;
                objEmrgService.ServiceTypeID = Convert.ToInt32(ddl_servicetype.SelectedValue == "" ? "0" : ddl_servicetype.SelectedValue);
                objEmrgService.FinancialYearID = LogData.FinancialYearID;
                objEmrgService.EmployeeID = LogData.EmployeeID;
                objEmrgService.AddedBy = LogData.AddedBy;
                objEmrgService.HospitalID = LogData.HospitalID;
                objEmrgService.IsActive = LogData.IsActive;
                objEmrgService.IPaddress = LogData.IPaddress;
                objEmrgService.ActionType = Enumaction.Insert;
                int result = objPhrServiceBO.UpdateEMRGServiceRecordPHR(objEmrgService);
                if (result > 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "update", 1);
                    div1.Visible = true;
                    div1.Attributes["class"] = "SucessAlert";
                    //gvEMRGitemlist.DataSource = null;
                    //gvEMRGitemlist.DataBind();
                    //gvEMRGitemlist.Visible = false;
                    Session["ItemList"] = null;
                    txt_emrgno.Text = "";
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

        protected void btnreset_Click(object sender, EventArgs e)
        {
            txt_emrgno.ReadOnly = false;
            txt_emrgno.Text = "";
            txt_age.Text = "";
            txt_gender.Text = "";
            ddldoctor.SelectedIndex = 0;
            txtname.ReadOnly = true;
            txtname.Text = "";
            txtservicecharge.Text = "";
            txt_totalbill.Text = "";
            Session["ItemList"] = null;
            gvEMRGitemlist.DataSource = null;
            gvEMRGitemlist.DataBind();
            gvEMRGitemlist.Visible = false;
            lblmessage.Visible = false;
            div1.Visible = false;
            txtservices.Text = "";
            txtquantity.Text = "";
            div1.Visible = true;
            div1.Attributes["class"] = "Blank";
            txtservices.ReadOnly = false;
            btnsave.Attributes["disabled"] = "disabled";
            txt_tax.Text = "";
            ddldoctor.Attributes.Remove("disabled");
        }

        protected void btnresets_Click(object sender, EventArgs e)
        {
            txt_emrgNoList.Text = "";
            txtdatefrom.Text = "";
            txtto.Text = "";
            ddlstatus.SelectedIndex = 0;
            gvEMRGDrugrecord.DataSource = null;
            gvEMRGDrugrecord.DataBind();
            gvEMRGDrugrecord.Visible = false;
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
        protected void txtpatientNames_TextChanged(object sender, EventArgs e)
        {
            if (txtpatientNames.Text != "")
            {
                var source = txtpatientNames.Text.ToString();
                if (source.Contains(":"))
                {
                    bindgrid();
                }
                else
                {
                    txtpatientNames.Text = "";
                    txtpatientNames.Focus();
                    return;
                }
            }
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

                if (txtpatientNames.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage2, "EMRGNO", 0);
                    divmsg2.Visible = true;
                    divmsg2.Attributes["class"] = "FailAlert";
                    txtpatientNames.Focus();
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
                List<EmrgAdmissionData> objdeposit = GetEMRGDrugList(0);
                if (objdeposit.Count > 0)
                {
                    gvEMRGDrugrecord.DataSource = objdeposit;
                    gvEMRGDrugrecord.DataBind();
                    gvEMRGDrugrecord.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total:" + objdeposit[0].MaximumRows.ToString() + " Record(s) found.", 1);
                    divmsg3.Attributes["class"] = "SucessAlert";
                    divmsg3.Visible = true;
                    ddlexport.Visible = true;
                    btnexport.Visible = true;
                }
                else
                {
                    gvEMRGDrugrecord.DataSource = null;
                    gvEMRGDrugrecord.DataBind();
                    gvEMRGDrugrecord.Visible = true;
                    ddlexport.Visible = false;
                    btnexport.Visible = false;
                    lblresult.Visible = false;
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
        public List<EmrgAdmissionData> GetEMRGDrugList(int curIndex)
        {
            EmrgAdmissionData objpat = new EmrgAdmissionData();
            EmrgAdmissionBO objBO = new EmrgAdmissionBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime from = txtdatefrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txtdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txtto.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            //objpat.EmrgNo = txt_emrgNoList.Text.Trim() == "" ? null : txt_emrgNoList.Text.Trim();
            objpat.PatientName = null; // txtpatientNames.Text == "" ? null : txtpatientNames.Text.Trim();
            string EmgNo;
            var source = txtpatientNames.Text.ToString();
            if (source.Contains(":"))
            {
                EmgNo = source.Substring(source.LastIndexOf(':') + 1);
                objpat.EmrgNo = EmgNo.Trim();
            }
            else
            {
                objpat.EmrgNo = "";
            }

            //objpat.ServiceTypeID = Convert.ToInt32(ddl_servicetypes.SelectedValue == "" ? "0" : ddl_servicetypes.SelectedValue);
            //objpat.ServiceTypeID = 8;
            objpat.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
            objpat.DateFrom = from;
            objpat.DateTo = To;
            return objBO.GetEMRGDrugList(objpat);
        }

    
     

        protected void gvEMRGDrugrecord_RowCommand(object sender, GridViewCommandEventArgs e)
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
                    EmrgAdmissionData objadmin = new EmrgAdmissionData();
                    EmrgAdmissionBO obadminBO = new EmrgAdmissionBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = gvEMRGDrugrecord.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("lbl_recordID");
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

                    objadmin.EmrgNo = txt_emrgNoList.Text == "" ? "" : txt_emrgNoList.Text;
                    objadmin.SerialID = Convert.ToInt32(SerialID.Text == "" ? "0" : SerialID.Text);
                    objadmin.ID = Convert.ToInt32(ID.Text == "" ? "0" : ID.Text);
                    objadmin.EmployeeID = LogData.UserLoginId;
                    objadmin.EmployeeID = LogData.EmployeeID;
                    objadmin.HospitalID = LogData.HospitalID;
                    objadmin.IPaddress = LogData.IPaddress;

                    int Result = obadminBO.DeleteEMGDrugRecordByEMRGNo(objadmin);
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

        protected void gvEMRGDrugrecord_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvEMRGDrugrecord.PageIndex = e.NewPageIndex;
            bindgrid();
        }
        protected DataTable GetDatafromDatabase()
        {
            List<EmrgAdmissionData> ServiceDetails = GetEMRGDrugList(0);
            List<EMRGServiceListDataTOeXCEL> ListexcelData = new List<EMRGServiceListDataTOeXCEL>();
            int i = 0;
            foreach (EmrgAdmissionData row in ServiceDetails)
            {
                EMRGServiceListDataTOeXCEL Ecxeclpat = new EMRGServiceListDataTOeXCEL();
                Ecxeclpat.EmrgNo = ServiceDetails[i].EmrgNo;
                Ecxeclpat.UHID = ServiceDetails[i].UHID;
                Ecxeclpat.PatientName = ServiceDetails[i].PatientName;
                Ecxeclpat.ServiceName = ServiceDetails[i].ServiceName;
                Ecxeclpat.ServiceCharge = ServiceDetails[i].ServiceCharge;
                Ecxeclpat.Quantity = ServiceDetails[i].Quantity;
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
                    gvEMRGDrugrecord.BorderStyle = BorderStyle.None;
                    //Hide the Column containing CheckBox
                    gvEMRGDrugrecord.Columns[9].Visible = false;
                    gvEMRGDrugrecord.Columns[10].Visible = false;
                    gvEMRGDrugrecord.RenderControl(hw);
                    gvEMRGDrugrecord.HeaderRow.Style.Add("width", "15%");
                    gvEMRGDrugrecord.HeaderRow.Style.Add("font-size", "10px");
                    gvEMRGDrugrecord.Style.Add("text-decoration", "none");
                    gvEMRGDrugrecord.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    gvEMRGDrugrecord.Style.Add("font-size", "8px");
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 7f, 7f, 7f, 0f);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=EmergencyPharmacyServiceDetails.pdf");
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
                wb.Worksheets.Add(dt, "IP service record");
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=EmergencyPharmacyServiceDetails.xlsx");
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
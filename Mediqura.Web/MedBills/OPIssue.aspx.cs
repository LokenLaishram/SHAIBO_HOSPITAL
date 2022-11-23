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
    public partial class OPIssue : BasePage
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
            //Commonfunction.PopulateDdl(ddl_patienttype, mstlookup.GetLookupsList(LookupName.OutsiderPatientType));
            Commonfunction.PopulateDdl(ddlpaymentmode, mstlookup.GetLookupsList(LookupName.PaymentMode));
            Commonfunction.PopulateDdl(ddldiscountby, mstlookup.GetLookupsList(LookupName.DiscountBy));
            Commonfunction.PopulateDdl(ddl_paymode, mstlookup.GetLookupsList(LookupName.PaymentMode));
            Commonfunction.PopulateDdl(ddlcollectedby, mstlookup.GetLookupsList(LookupName.PHRcollectedBy));
            ddlpaymentmode.SelectedIndex = 1;
            txt_totalbill.Text = "0.00";
            txt_balanceinac.Text = "0.00";
            txt_adjustedamount.Text = "0.00";
            Session["ItemList"] = null;
            btnsave.Attributes["disabled"] = "disabled";
            btnprints.Attributes["disabled"] = "disabled";
            btnprint.Attributes["disabled"] = "disabled";
            txtdescription.Text = "";
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
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetAutoUHID(string prefixText, int count, string contextKey)
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
        protected void txtUHID_TextChanged(object sender, EventArgs e)
        {
            PatientData Objpaic = new PatientData();
            RegistrationBO objInfoBO = new RegistrationBO();
            List<PatientData> getResult = new List<PatientData>();
            Objpaic.UHID = Convert.ToInt64(txtUHID.Text.Trim() == "" ? "0" : txtUHID.Text.Trim());
            getResult = objInfoBO.GetPatientDetailsByUHID(Objpaic);
            if (getResult.Count > 0)
            {
                txtname.Text = getResult[0].PatientName.ToString();
                txtaddress.Text = getResult[0].Address.ToString();
                txtservices.Focus();
                txt_balanceinac.Text = Commonfunction.Getrounding((Convert.ToDecimal(getResult[0].DoctorID.ToString())).ToString());
                Session["ServiceList"] = null;
            }
            else
            {
                txtname.Text = "";
                txtaddress.Text = "";
                txtUHID.Text = "";
                txt_balanceinac.Text = "";
                txtUHID.Focus();
            }
        }
        protected void ddl_patienttype_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_patienttype.SelectedIndex == 1)
            {
                txtUHID.ReadOnly = false;
                txtname.ReadOnly = true;
                txtaddress.ReadOnly = true;
                txt_contactno.ReadOnly = true;

            }
            else
            {
                txtUHID.ReadOnly = true;
                txtname.ReadOnly = false;
                txtaddress.ReadOnly = false;
                txt_contactno.ReadOnly = false;
                txtUHID.Text = "";
                txtname.Text = "";
                txtaddress.Text = "";
            }
            lblmessage.Visible = false;
            div1.Visible = false;
        }
        protected void btnadd_Click(object sender, EventArgs e)
        {
            if (ddl_patienttype.SelectedIndex < 2)
            {
                txt_contactno.ReadOnly = true;
                if (txtUHID.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "UHID", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    txtUHID.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                    div1.Visible = false;
                }

            }
            else
            {
                txt_contactno.ReadOnly = false;
                if (txtname.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Custommer", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    txtname.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                    div1.Visible = false;
                }
                if (txtaddress.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Address", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    txtaddress.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                    div1.Visible = false;
                }
                if (txt_contactno.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "MobileNo", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    txt_contactno.Focus();
                    return;
                }
                else
                {
                    if (Commonfunction.Checkvalidmobile(txt_contactno.Text) == false)
                    {
                        Messagealert_.ShowMessage(lblmessage, "mobile", 0);
                        div1.Visible = true;
                        div1.Attributes["class"] = "FailAlert";
                        txt_contactno.Focus();
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                        div1.Visible = false;
                    }
                }

            }
            if (txtservices.Text == "")
            {
                Messagealert_.ShowMessage(lblmessage, "Service", 0);
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
                foreach (GridViewRow row in gvopitemlist.Rows)
                {
                    Label StockID = (Label)gvopitemlist.Rows[row.RowIndex].Cells[0].FindControl("lbl_SubStockID");
                    Label ItemID = (Label)gvopitemlist.Rows[row.RowIndex].Cells[0].FindControl("lblID");
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

            List<PHRbillingData> LabServiceList = Session["ItemList"] == null ? new List<PHRbillingData>() : (List<PHRbillingData>)Session["ItemList"];
            PHRbillingData ObjService = new PHRbillingData();
            ObjService.LabServiceCharge = Convert.ToDecimal(txtservicecharge.Text.ToString() == "" ? "0" : txtservicecharge.Text.ToString());
            ObjService.Quantity = Convert.ToInt32(txtquantity.Text == "" ? "0" : txtquantity.Text);
            ObjService.Tax = Convert.ToDecimal(lbltax.Text == "" ? "0" : lbltax.Text);
            ObjService.ItemID = Convert.ToInt32(lblItemID.Text);
            ObjService.SubStockID = Convert.ToInt64(lblSubStockID.Text);
            Decimal Tax_Amount = (Convert.ToDecimal(lbltax.Text == "" ? "0" : lbltax.Text) / 100) * Convert.ToDecimal(txtservicecharge.Text.ToString() == "" ? "0" : txtservicecharge.Text.ToString()) * Convert.ToInt32(txtquantity.Text == "" ? "0" : txtquantity.Text);
            ObjService.TotalTax = Tax_Amount;
            ObjService.NetLabServiceCharge = Convert.ToDecimal(txtservicecharge.Text.ToString() == "" ? "0" : txtservicecharge.Text.ToString()) * Convert.ToInt32(txtquantity.Text == "" ? "0" : txtquantity.Text) + Tax_Amount;
            ObjService.TestName = lblservicename.Text.Trim();
            txt_totalbill.Text = Commonfunction.Getrounding((Tax_Amount + Convert.ToDecimal(txt_totalbill.Text == "" ? "0" : txt_totalbill.Text) + Convert.ToInt32(txtquantity.Text == "" ? "0" : txtquantity.Text) * Convert.ToDecimal(txtservicecharge.Text.ToString() == "" ? "0" : txtservicecharge.Text.ToString())).ToString());
            txt_tax.Text = Commonfunction.Getrounding((Convert.ToDecimal(txt_tax.Text == "" ? "0" : txt_tax.Text) + Tax_Amount).ToString());
            if (ddl_patienttype.SelectedIndex == 0)
            {
                if (Convert.ToDecimal(txt_balanceinac.Text) > 0)
                {
                    if (Convert.ToDecimal(txt_balanceinac.Text) >= Convert.ToDecimal(txt_totalbill.Text))
                    {
                        txt_adjustedamount.Text = Commonfunction.Getrounding((Convert.ToDecimal(txt_totalbill.Text == "" ? "0" : txt_totalbill.Text)).ToString());
                        txt_paidamount.Text = "0.00";
                    }
                    else if (Convert.ToDecimal(txt_balanceinac.Text) < Convert.ToDecimal(txt_totalbill.Text))
                    {
                        txt_adjustedamount.Text = Commonfunction.Getrounding((Convert.ToDecimal(txt_balanceinac.Text == "" ? "0" : txt_balanceinac.Text)).ToString());
                        txt_paidamount.Text = Commonfunction.Getrounding((Convert.ToDecimal(txt_totalbill.Text == "" ? "0" : txt_totalbill.Text) - Convert.ToDecimal(txt_balanceinac.Text.ToString() == "" ? "0" : txt_balanceinac.Text.ToString())).ToString());
                    }
                }
                else
                {
                    txt_adjustedamount.Text = "0.00";
                    txt_paidamount.Text = Commonfunction.Getrounding((Convert.ToDecimal(txt_totalbill.Text == "" ? "0" : txt_totalbill.Text)).ToString());
                }
            }
            else
            {
                txt_paidamount.Text = Commonfunction.Getrounding((Convert.ToDecimal(txt_totalbill.Text == "" ? "0" : txt_totalbill.Text)).ToString());
            }
            LabServiceList.Add(ObjService);
            if (LabServiceList.Count > 0)
            {
                gvopitemlist.DataSource = LabServiceList;
                gvopitemlist.DataBind();
                gvopitemlist.Visible = true;
                Session["ItemList"] = LabServiceList;
                txtservices.Text = "";
                txtservicecharge.Text = "";
                txtquantity.Text = "";
                txtservices.Focus();
                txtquantity.Text = "";
                txtservices.ReadOnly = false;
                txtdescription.Text = "";
                btnsave.Attributes.Remove("disabled");
            }
            else
            {
                btnsave.Attributes["disabled"] = "disabled";
                gvopitemlist.DataSource = null;
                gvopitemlist.DataBind();
                gvopitemlist.Visible = true;

            }
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetServices(string prefixText, int count, string contextKey)
        {
            LabServiceMasterData Objpaic = new LabServiceMasterData();
            LabServiceMasterBO objInfoBO = new LabServiceMasterBO();
            List<LabServiceMasterData> getResult = new List<LabServiceMasterData>();
            Objpaic.ServiceName = prefixText;
            getResult = objInfoBO.GetOPPhrServices(Objpaic);
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
                PHRbillingData ObjBillData = new PHRbillingData();
                ObjBillData.StockID = Convert.ToInt32(ID == "" ? "0" : ID);
                List<PHRbillingData> result = ObjbillBO.GetOPServiceChargeByID(ObjBillData);
                if (result.Count > 0)
                {
                    txtservicecharge.Text = Commonfunction.Getrounding(result[0].ServiceCharge.ToString());
                    lblservicename.Text = result[0].ServiceName.ToString();
                    lblItemID.Text = result[0].ItemID.ToString();
                    lbltax.Text = result[0].Tax.ToString();
                    txtdescription.Text = result[0].Remarks.ToString();
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
                txtdescription.Text = "";
                txtservices.Focus();
            }
        }
        protected void gvopitemlist_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblSerial = (Label)e.Row.FindControl("lblserialID");
                lblSerial.Text = ((gvopitemlist.PageIndex * gvopitemlist.PageSize) + e.Row.RowIndex + 1).ToString();
            }
        }
        protected void ddlpaymentmode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlpaymentmode.SelectedIndex > 1)
            {
                txt_bank.ReadOnly = false;
                txt_account.ReadOnly = false;
            }
            else
            {
                txt_bank.ReadOnly = true;
                txt_account.ReadOnly = true;
            }

        }
        protected void gvopitemlist_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Deletes")
                {
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = gvopitemlist.Rows[i];
                    List<PHRbillingData> ItemList = Session["ItemList"] == null ? new List<PHRbillingData>() : (List<PHRbillingData>)Session["ItemList"];
                    if (ItemList.Count > 0)
                    {
                        Decimal totalamount = ItemList[i].LabServiceCharge;
                        Decimal Tax = ItemList[i].TotalTax;
                        txt_tax.Text = Commonfunction.Getrounding((Convert.ToDecimal(txt_tax.Text == "" ? "0" : txt_tax.Text) - Tax).ToString());
                        txt_totalbill.Text = Commonfunction.Getrounding((Convert.ToDecimal(txt_totalbill.Text == "" ? "0" : txt_totalbill.Text) - totalamount).ToString());
                        if (ddl_patienttype.SelectedIndex == 0)
                        {
                            if (Convert.ToDecimal(txt_balanceinac.Text) > 0)
                            {
                                if (Convert.ToDecimal(txt_balanceinac.Text) >= Convert.ToDecimal(txt_balanceinac.Text))
                                {
                                    txt_adjustedamount.Text = Commonfunction.Getrounding((Convert.ToDecimal(txt_totalbill.Text == "" ? "0" : txt_totalbill.Text)).ToString());
                                    txt_paidamount.Text = "0.00";
                                }
                                else if (Convert.ToDecimal(txt_balanceinac.Text) < Convert.ToDecimal(txt_totalbill.Text))
                                {
                                    txt_adjustedamount.Text = Commonfunction.Getrounding((Convert.ToDecimal(txt_balanceinac.Text == "" ? "0" : txt_balanceinac.Text)).ToString());
                                    txt_paidamount.Text = Commonfunction.Getrounding((Convert.ToDecimal(txt_totalbill.Text == "" ? "0" : txt_totalbill.Text) - Convert.ToDecimal(txt_balanceinac.Text.ToString() == "" ? "0" : txt_balanceinac.Text.ToString())).ToString());
                                }
                                //else if (Convert.ToDecimal(txtbalanceinac.Text) > Convert.ToDecimal(txttotalamount.Text))
                                //{
                                //    txtadjustedamount.Text = Commonfunction.Getrounding((Convert.ToDecimal(txttotalamount.Text == "" ? "0" : txttotalamount.Text)).ToString());
                                //    txtpaidamount.Text = "0.00";
                                //}
                                else if (Convert.ToDecimal(txt_balanceinac.Text) == Convert.ToDecimal(txt_totalbill.Text))
                                {
                                    txt_adjustedamount.Text = Commonfunction.Getrounding((Convert.ToDecimal(txt_balanceinac.Text == "" ? "0" : txt_balanceinac.Text)).ToString());
                                    txt_paidamount.Text = "0.00";
                                }
                                else if (Convert.ToDecimal(txt_totalbill.Text) == 0)
                                {
                                    txt_paidamount.Text = "0.00";
                                }
                            }
                        }
                        else
                        {
                            txt_adjustedamount.Text = "0.00";
                            txt_paidamount.Text = Commonfunction.Getrounding((Convert.ToDecimal(txt_totalbill.Text == "" ? "0" : txt_totalbill.Text)).ToString());
                        }
                    }
                    ItemList.RemoveAt(i);
                    Session["ItemList"] = ItemList;
                    gvopitemlist.DataSource = ItemList;
                    gvopitemlist.DataBind();
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
        protected void txt_Name_TextChanged(object sender, EventArgs e)
        {
            txtaddress.Focus();
        }
        protected void txt_address_TextChanged(object sender, EventArgs e)
        {
            txt_contactno.Focus();
        }
        protected void txt_contact_TextChanged(object sender, EventArgs e)
        {
            txtservices.Focus();
        }
        protected void txt_discount_TextChanged(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(txt_totalbill.Text == "" ? "0.00" : txt_totalbill.Text) >= Convert.ToDecimal(txt_discount.Text == "" ? "0.00" : txt_discount.Text))
            {
                if (Convert.ToDecimal(txt_paidamount.Text == "" ? "0.00" : txt_paidamount.Text) >= Convert.ToDecimal(txt_discount.Text == "" ? "0.00" : txt_discount.Text))
                {
                    txt_paidamount.Text = Commonfunction.Getrounding(((Convert.ToDecimal(txt_paidamount.Text == "" ? "0" : txt_paidamount.Text) - Convert.ToDecimal(txt_discount.Text == "" ? "0" : txt_discount.Text)).ToString()));
                }
                if (Convert.ToDecimal(txt_paidamount.Text == "" ? "0.00" : txt_paidamount.Text) < Convert.ToDecimal(txt_discount.Text == "" ? "0.00" : txt_discount.Text) && Convert.ToDecimal(txt_adjustedamount.Text == "" ? "0.00" : txt_adjustedamount.Text) > 0)
                {
                    txt_paidamount.Text = "0.00";
                    txt_adjustedamount.Text = Commonfunction.Getrounding(((((Convert.ToDecimal(txt_adjustedamount.Text == "" ? "0" : txt_adjustedamount.Text) + ((Convert.ToDecimal(txt_totalbill.Text == "" ? "0.00" : txt_totalbill.Text) - Convert.ToDecimal(txt_adjustedamount.Text == "" ? "0.00" : txt_adjustedamount.Text))) - Convert.ToDecimal(txt_discount.Text == "" ? "0" : txt_discount.Text))))).ToString());
                }
            }
            else
            {
                Messagealert_.ShowMessage(lblmessage, "DiscountOver", 0);
                div1.Visible = true;
                txt_discount.Text = "";
                div1.Attributes["class"] = "FailAlert";
            }
            if (Convert.ToDecimal(txt_discount.Text == "" ? "0.00" : txt_discount.Text) == 0)
            {
                if (Convert.ToDecimal(txt_balanceinac.Text == "" ? "0" : txt_balanceinac.Text) > 0)
                {
                    if (Convert.ToDecimal(txt_balanceinac.Text == "" ? "0" : txt_balanceinac.Text) >= Convert.ToDecimal(txt_totalbill.Text == "" ? "0" : txt_totalbill.Text))
                    {
                        txt_adjustedamount.Text = Commonfunction.Getrounding((Convert.ToDecimal(txt_totalbill.Text == "" ? "0" : txt_totalbill.Text)).ToString());
                        txt_paidamount.Text = "0.00";
                    }
                    else if (Convert.ToDecimal(txt_balanceinac.Text == "" ? "0" : txt_balanceinac.Text) < Convert.ToDecimal(txt_totalbill.Text == "" ? "0" : txt_totalbill.Text))
                    {
                        txt_adjustedamount.Text = Commonfunction.Getrounding((Convert.ToDecimal(txt_balanceinac.Text == "" ? "0" : txt_balanceinac.Text)).ToString());
                        txt_paidamount.Text = Commonfunction.Getrounding((Convert.ToDecimal(txt_totalbill.Text == "" ? "0" : txt_totalbill.Text) - Convert.ToDecimal(txt_balanceinac.Text.ToString() == "" ? "0" : txt_balanceinac.Text.ToString())).ToString());
                    }
                }
                else
                {
                    txt_adjustedamount.Text = "0.00";
                    txt_paidamount.Text = Commonfunction.Getrounding((Convert.ToDecimal(txt_totalbill.Text == "" ? "0" : txt_totalbill.Text)).ToString());
                }
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

            if (ddl_patienttype.SelectedIndex == 1)
            {
                txt_contactno.ReadOnly = true;
                if (txtUHID.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "UHID", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    txtUHID.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                    div1.Visible = false;
                }

            }
            else
            {
                txt_contactno.ReadOnly = false;
                if (txtname.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Custommer", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    txtname.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                    div1.Visible = false;
                }
                if (txtaddress.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Address", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    txtaddress.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                    div1.Visible = false;
                }
                if (txt_contactno.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "MobileNo", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    txt_contactno.Focus();
                    return;
                }
                else
                {
                    if (Commonfunction.Checkvalidmobile(txt_contactno.Text) == false)
                    {
                        Messagealert_.ShowMessage(lblmessage, "mobile", 0);
                        div1.Visible = true;
                        div1.Attributes["class"] = "FailAlert";
                        txt_contactno.Focus();
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                        div1.Visible = false;
                    }
                }

            }
            if (ddlpaymentmode.SelectedIndex == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "Paymode", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                ddlpaymentmode.Focus();
                return;
            }
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

            if (ddlpaymentmode.SelectedIndex > 1)
            {
                if (txt_bank.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "BankName", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    txt_bank.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                    div1.Visible = false;
                }
                if (txt_account.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Account", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    txt_account.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                    div1.Visible = false;
                }
            }
            else
            {
                lblmessage.Visible = false;
                div1.Visible = false;
            }
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
            if (Convert.ToDecimal(txt_discount.Text == "" ? "0.0" : txt_discount.Text) > 0 && ddldiscountby.SelectedIndex == 0 && txt_remarks.Text == "")
            {
                Messagealert_.ShowMessage(lblmessage, "DiscountByselect", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                txt_remarks.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
                div1.Visible = false;
            }
            List<PHRbillingData> Listbill = new List<PHRbillingData>();
            OPDbillingBO objbillingBO = new OPDbillingBO();
            PHRbillingData objdeposit = new PHRbillingData();
            //DepositBO objstdBO = new DepositBO();
            // int index = 0;
            try
            {
                // get all the record from the gridview
                foreach (GridViewRow row in gvopitemlist.Rows)
                {
                    IFormatProvider provider = new System.Globalization.CultureInfo("en-GB", true);
                    Label Particulars = (Label)gvopitemlist.Rows[row.RowIndex].Cells[0].FindControl("lbllabparticulars");
                    Label amount = (Label)gvopitemlist.Rows[row.RowIndex].Cells[0].FindControl("lbl_labcharges");
                    Label qty = (Label)gvopitemlist.Rows[row.RowIndex].Cells[0].FindControl("lblquantity");
                    Label NetCharge = (Label)gvopitemlist.Rows[row.RowIndex].Cells[0].FindControl("lblnetcharges");
                    Label Tax = (Label)gvopitemlist.Rows[row.RowIndex].Cells[0].FindControl("lbl_Tax");
                    Label TotalTax = (Label)gvopitemlist.Rows[row.RowIndex].Cells[0].FindControl("lbl_totaltax");
                    Label SerialID = (Label)gvopitemlist.Rows[row.RowIndex].Cells[0].FindControl("lblserialID");
                    Label ID = (Label)gvopitemlist.Rows[row.RowIndex].Cells[0].FindControl("lblID");
                    Label SubStockID = (Label)gvopitemlist.Rows[row.RowIndex].Cells[0].FindControl("lbl_SubStockID");
                    PHRbillingData ObjDetails = new PHRbillingData();

                    ObjDetails.ItemName = Particulars.Text == "" ? null : Particulars.Text;
                    ObjDetails.Charge = Convert.ToDecimal(amount.Text == "" ? "0" : amount.Text);
                    ObjDetails.Tax = Convert.ToDecimal(Tax.Text == "" ? "0" : Tax.Text);
                    ObjDetails.Quantity = Convert.ToInt32(qty.Text == "" ? "0" : qty.Text);
                    ObjDetails.NetCharges = Convert.ToDecimal(NetCharge.Text == "" ? "0" : NetCharge.Text);
                    ObjDetails.SerialID = Convert.ToInt32(SerialID.Text == "" ? "0" : SerialID.Text);
                    ObjDetails.ItemID = Convert.ToInt32(ID.Text == "" ? "0" : ID.Text);
                    ObjDetails.SubStockID = Convert.ToInt64(SubStockID.Text == "" ? "0" : SubStockID.Text);
                    Listbill.Add(ObjDetails);
                }
                objdeposit.XMLData = XmlConvertor.OpdPhrBillDatatoXML(Listbill).ToString();
                objdeposit.PatientType = Convert.ToInt32(ddl_patienttype.SelectedValue == "" ? "0" : ddl_patienttype.SelectedValue);
                objdeposit.PatientName = txtname.Text == "" ? null : txtname.Text;
                objdeposit.PatientAddress = txtaddress.Text == "" ? null : txtaddress.Text;
                objdeposit.ContactNo = txt_contactno.Text == "" ? null : txt_contactno.Text;
                objdeposit.TotalBillAmount = Convert.ToDecimal(txt_totalbill.Text == "" ? "0" : txt_totalbill.Text);
                objdeposit.UHID = Convert.ToInt64(txtUHID.Text == "" ? "0" : txtUHID.Text);
                objdeposit.AdjustedAmount = Convert.ToDecimal(txt_adjustedamount.Text == "" ? "0" : txt_adjustedamount.Text);
                objdeposit.DiscountedAmount = Convert.ToDecimal(txt_discount.Text == "" ? "0" : txt_discount.Text);
                objdeposit.TotalBillTax = Convert.ToDecimal(txt_tax.Text == "" ? "0" : txt_tax.Text);
                objdeposit.PaidAmount = Convert.ToDecimal(txt_paidamount.Text == "" ? "0" : txt_paidamount.Text);
                objdeposit.PaymentMode = Convert.ToInt32(ddlpaymentmode.SelectedValue == "" ? "0" : ddlpaymentmode.SelectedValue);
                objdeposit.custommertype = Convert.ToInt32(ddl_patienttype.SelectedValue == "0" || ddl_patienttype.SelectedValue == "1" ? "1" : "2");
                objdeposit.DiscountByID = Convert.ToInt64(ddldiscountby.SelectedValue == "" ? "0" : ddldiscountby.SelectedValue);
                objdeposit.BankName = txt_bank.Text == "" ? null : txt_bank.Text;
                objdeposit.Remarks = txt_remarks.Text == "" ? null : txt_remarks.Text;
                objdeposit.AccountNo = txt_account.Text == "" ? null : txt_account.Text;
                objdeposit.FinancialYearID = LogData.FinancialYearID;
                objdeposit.EmployeeID = LogData.EmployeeID;
                objdeposit.AddedBy = LogData.AddedBy;
                objdeposit.HospitalID = LogData.HospitalID;
                objdeposit.IsActive = LogData.IsActive;
                objdeposit.IPaddress = LogData.IPaddress;
                objdeposit.ActionType = Enumaction.Insert;

                int result = objbillingBO.UpdateOPDPhrBill(objdeposit);
                if (result > 0)
                {
                    txtbillNo.Text = result.ToString();
                    Messagealert_.ShowMessage(lblmessage, "update", 1);
                    Session["ItemList"] = null;
                    div1.Visible = true;
                    div1.Attributes["class"] = "SucessAlert";
                    txtUHID.Text = "";
                    btnsave.Attributes["disabled"] = "disabled";
                    if (LogData.PrintEnable == 0)
                    {
                        btnprint.Attributes["disabled"] = "disabled";
                    }
                    else
                    {
                        btnprint.Attributes.Remove("disabled");
                    }
                }
                else
                {
                    txtbillNo.Text = "";
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
        protected void btnreset_Click(object sender, EventArgs e)
        {
            ddl_patienttype.SelectedIndex = 0;
            txtUHID.ReadOnly = false;
            txtname.ReadOnly = true;
            txtaddress.ReadOnly = true;
            txt_contactno.ReadOnly = true;
            txtUHID.Text = "";
            txtname.Text = "";
            txtaddress.Text = "";
            txtbillNo.Text = "";
            txtservicecharge.Text = "";
            txt_discount.Text = "";
            ddlpaymentmode.SelectedIndex = 0;
            ddl_patienttype.SelectedIndex = 0;
            txt_bank.Text = "";
            txt_totalbill.Text = "";
            txt_account.Text = "";
            txt_bank.ReadOnly = true;
            txt_account.ReadOnly = true;
            Session["ItemList"] = null;
            gvopitemlist.DataSource = null;
            gvopitemlist.DataBind();
            gvopitemlist.Visible = false;
            lblmessage.Visible = false;
            div1.Visible = false;
            txtservices.Text = "";
            txtquantity.Text = "";
            txt_balanceinac.Text = "";
            txt_paidamount.Text = "";
            ddlpaymentmode.SelectedIndex = 1;
            div1.Visible = true;
            div1.Attributes["class"] = "Blank";
            txtservices.ReadOnly = false;
            ddldiscountby.SelectedIndex = 0;
            txt_remarks.Text = "";
            btnsave.Attributes["disabled"] = "disabled";
            btnprint.Attributes["disabled"] = "disabled";
            txtdescription.Text = "";
            txt_tax.Text = "";
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
        protected void txtautoUHID_TextChanged(object sender, EventArgs e)
        {
            if (txtautoUHID.Text != "")
            {
                bindgrid();
            }
            PatientData Objpaic = new PatientData();
            RegistrationBO objInfoBO = new RegistrationBO();
            List<PatientData> getResult = new List<PatientData>();
            Objpaic.UHID = Convert.ToInt64(txtautoUHID.Text.Trim() == "" ? "0" : txtautoUHID.Text.Trim());
            getResult = objInfoBO.GetPatientDetailsByUHID(Objpaic);
            if (getResult.Count > 0)
            {
                txtpatientNames.Text = getResult[0].PatientName.ToString();
                // txtaddress.Text = getResult[0].Address.ToString();
                // txtbalance.Text = Commonfunction.Getrounding((Convert.ToDecimal(getResult[0].BalanceAmount.ToString())).ToString());
                Session["ServiceList"] = null;
            }
            else
            {
                txtpatientNames.Text = "";
                //  txtaddress.Text = "";
                txtautoUHID.Text = "";
                //txtbalance.Text = "";
                txtautoUHID.Focus();
            }

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
                if (txtdatefrom.Text != "")
                {
                    if (Commonfunction.isValidDate(txtdatefrom.Text) == false)
                    {
                        Messagealert_.ShowMessage(lblmessage2, "VaildDatefrom", 0);
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
                        Messagealert_.ShowMessage(lblmessage2, "VaildDateto", 0);
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
                List<PHRbillingData> objdeposit = GetPhrBillList(0);
                if (objdeposit.Count > 0)
                {
                    gvopditemlist.DataSource = objdeposit;
                    gvopditemlist.DataBind();
                    gvopditemlist.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total:" + objdeposit[0].MaximumRows.ToString() + " Record(s) found.", 1);
                    divmsg3.Attributes["class"] = "SucessAlert";
                    divmsg3.Visible = true;
                    txttotalbillamount.Text = Commonfunction.Getrounding(objdeposit[0].TotalBill.ToString());
                    txtajusted.Text = Commonfunction.Getrounding(objdeposit[0].TotalAdjustedAmount.ToString());
                    txttotaldiscounted.Text = Commonfunction.Getrounding(objdeposit[0].TotalDiscountedAmount.ToString());
                    txttotalpaid.Text = Commonfunction.Getrounding(objdeposit[0].TotalPaidAmount.ToString());
                    ddlexport.Visible = true;
                    btnexport.Visible = true;
                    if (LogData.PrintEnable == 0)
                    {
                        btnprints.Attributes["disabled"] = "disabled";
                    }
                    else
                    {
                        btnprints.Attributes.Remove("disabled");
                    }
                }
                else
                {
                    gvopditemlist.DataSource = null;
                    gvopditemlist.DataBind();
                    gvopditemlist.Visible = true;
                    ddlexport.Visible = false;
                    btnexport.Visible = false;
                    txttotalbillamount.Text = "0.00";
                    txtajusted.Text = "0.00";
                    txttotaldiscounted.Text = "0.00";
                    lblresult.Visible = false;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
                divmsg3.Attributes["class"] = "SucessAlert";
                divmsg3.Visible = true;
            }
        }
        public List<PHRbillingData> GetPhrBillList(int curIndex)
        {
            PHRbillingData objpat = new PHRbillingData();
            OPDbillingBO objbillingBO = new OPDbillingBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime from = txtdatefrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txtdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txtto.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            string datefrom = from.ToString("yyyy-MM-dd");
            string timefrom = txttimepickerfrom.Text.Trim();
            from = Convert.ToDateTime(datefrom + " " + timefrom);
            objpat.DateFrom = from;
            string dateto = To.ToString("yyyy-MM-dd");
            string timeto = txttimepickerto.Text.Trim();
            To = Convert.ToDateTime(dateto + " " + timeto);
            objpat.DateTo = To;
            objpat.UHID = Convert.ToInt64(txtautoUHID.Text == "" ? "0" : txtautoUHID.Text);
            objpat.CollectedByID = Convert.ToInt64(ddlcollectedby.SelectedValue == "" ? "0" : ddlcollectedby.SelectedValue);
            objpat.PatientName = txtpatientNames.Text == "" ? null : txtpatientNames.Text.Trim();
            objpat.BillNo = txt_serachbill.Text.Trim();
            objpat.Paymode = Convert.ToInt32(ddl_paymode.SelectedValue == "" ? "0" : ddl_paymode.SelectedValue);
            objpat.custommertype = Convert.ToInt32(ddlcustomertype.SelectedValue == "" ? "0" : ddlcustomertype.SelectedValue);
            objpat.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
            return objbillingBO.GetOPDPhrBillList(objpat);
        }
        protected void btnresets_Click(object sender, EventArgs e)
        {
            txtautoUHID.Text = "";
            txtdatefrom.Text = "";
            txtto.Text = "";
            ddlstatus.SelectedIndex = 0;
            gvopditemlist.DataSource = null;
            gvopditemlist.DataBind();
            gvopditemlist.Visible = false;
            lblresult.Visible = false;
            txtpatientNames.Text = "";
            ddlpaymentmode.SelectedIndex = 0;
            ddlexport.SelectedIndex = 0;
            ddlexport.Visible = false;
            btnexport.Visible = false;
            lblmessage2.Visible = false;
            divmsg2.Visible = false;
            //divmsg3.Visible = false;
            txttotalbillamount.Text = "0.00";
            txtajusted.Text = "0.00";
            txttotaldiscounted.Text = "0.00";
            txttotalpaid.Text = "0.00";
            ddlpaymentmode.SelectedIndex = 0;
            ddlcollectedby.SelectedIndex = 0;
            txt_serachbill.Text = "";

        }
        protected void gvopditemlist_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Deletes")
                {
                    PHRbillingData objbill = new PHRbillingData();
                    OPDbillingBO objstdBO = new OPDbillingBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = gvopditemlist.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("lblID");
                    Label UHID = (Label)gr.Cells[0].FindControl("lbluhid");
                    Label name = (Label)gr.Cells[0].FindControl("lblname");
                    Label address = (Label)gr.Cells[0].FindControl("lbladdress");
                    Label totalbillamount = (Label)gr.Cells[0].FindControl("lbltotalbillamount");
                    Label adjustedamount = (Label)gr.Cells[0].FindControl("lblaajustedamount");
                    Label discountedamount = (Label)gr.Cells[0].FindControl("lbldiscountedamount");
                    Label amount = (Label)gr.Cells[0].FindControl("lblamount");
                    Label addedby = (Label)gr.Cells[0].FindControl("lbladdedBy");
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
                    objbill.BillNo = ID.Text.Trim();
                    objbill.UHID = Convert.ToInt64(UHID.Text == "" ? "0" : UHID.Text);
                    objbill.EmployeeID = LogData.EmployeeID;
                    objbill.IPaddress = LogData.IPaddress;
                    objbill.HospitalID = LogData.HospitalID;
                    objbill.FinancialYearID = LogData.FinancialYearID;
                    objbill.Amount = Convert.ToDecimal(adjustedamount.Text == "" ? "0" : adjustedamount.Text);
                    int Result = objstdBO.DeleteOPDPhrBillByID(objbill);
                    if (Result == 1)
                    {
                        Messagealert_.ShowMessage(lblmessage2, "delete", 1);
                        divmsg2.Attributes["class"] = "SucessAlert";
                        divmsg2.Visible = true;
                        bindgrid();
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
                divmsg2.Attributes["class"] = "SucessAlert";
                divmsg2.Visible = true;
            }
        }
        protected void gvoplabservicelist_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblSerial = (Label)e.Row.FindControl("lblserialID");
                DropDownList ddltestcenter = (DropDownList)e.Row.FindControl("ddl_testcenter");
                DropDownList ddlurgencyState = (DropDownList)e.Row.FindControl("ddl_urgency");
                lblSerial.Text = ((gvopditemlist.PageIndex * gvopditemlist.PageSize) + e.Row.RowIndex + 1).ToString();

                MasterLookupBO mstlookup = new MasterLookupBO();
                Commonfunction.PopulateDdl(ddltestcenter, mstlookup.GetLookupsList(LookupName.TestCenter));
                ddltestcenter.SelectedIndex = 1;
                Commonfunction.PopulateDdl(ddlurgencyState, mstlookup.GetLookupsList(LookupName.Urgency));
                ddlurgencyState.SelectedIndex = 1;
            }
        }
        protected void gvopditemlist_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton linkdelete = (LinkButton)e.Row.FindControl("lnkDelete");
                if (LogData.DeleteEnable == 0)
                {
                    linkdelete.Attributes["disabled"] = "disabled";
                }
                else
                {
                    linkdelete.Attributes.Remove("disabled");
                }
                if (LogData.PrintEnable == 0)
                {
                    gvopditemlist.Columns[13].Visible = false;
                }
                else
                {
                    gvopditemlist.Columns[13].Visible = true;
                }
            }
        }
        protected DataTable GetDatafromDatabase()
        {
            List<PHRbillingData> DepositDetails = GetPhrBillList(0);
            List<PHRbillingDataTOeXCEL> ListexcelData = new List<PHRbillingDataTOeXCEL>();
            int i = 0;
            foreach (PHRbillingData row in DepositDetails)
            {
                PHRbillingDataTOeXCEL Ecxeclpat = new PHRbillingDataTOeXCEL();
                Ecxeclpat.UHID = DepositDetails[i].UHID;
                Ecxeclpat.PatientName = DepositDetails[i].PatientName;
                Ecxeclpat.Address = DepositDetails[i].Address;
                Ecxeclpat.BillAmount = DepositDetails[i].TotalBillAmount;
                Ecxeclpat.TotalTax = DepositDetails[i].TotalTax;
                Ecxeclpat.TotalDiscountedAmount = DepositDetails[i].TotalDiscountedAmount;
                Ecxeclpat.TotalAdjustedAmount = DepositDetails[i].TotalAdjustedAmount;
                Ecxeclpat.TotalPaidAmount = DepositDetails[i].TotalPaidAmount;
                Ecxeclpat.AddedBy = DepositDetails[i].EmpName;
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
                // divmsg3.Attributes["class"] = "FailAlert";
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
                    gvopditemlist.BorderStyle = BorderStyle.None;
                    //Hide the Column containing CheckBox
                    gvopditemlist.Columns[10].Visible = false;
                    gvopditemlist.Columns[11].Visible = false;
                    gvopditemlist.Columns[12].Visible = false;
                    gvopditemlist.Columns[13].Visible = false;

                    gvopditemlist.RenderControl(hw);
                    gvopditemlist.HeaderRow.Style.Add("width", "15%");
                    gvopditemlist.HeaderRow.Style.Add("font-size", "10px");
                    gvopditemlist.Style.Add("text-decoration", "none");
                    gvopditemlist.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    gvopditemlist.Style.Add("font-size", "8px");
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 7f, 7f, 7f, 0f);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=OPDLabBillDetails.pdf");
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
                wb.Worksheets.Add(dt, "Lab Billing Details");
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=LabBillingDetails.xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                    ddlexport.SelectedIndex = 0;
                }
                Messagealert_.ShowMessage(lblresult, "Exported", 1);
                // divmsg3.Attributes["class"] = "SucessAlert";
            }
        }
        protected void gvopditemlist_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvopditemlist.PageIndex = e.NewPageIndex;
            bindgrid();
        }
    }
}
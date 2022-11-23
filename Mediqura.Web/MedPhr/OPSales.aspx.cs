using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedBillBO;
using Mediqura.BOL.MedStore;
using Mediqura.BOL.PatientBO;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.MedBillData;
using Mediqura.CommonData.MedStore;
using Mediqura.CommonData.PatientData;
using Mediqura.Utility;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using Mediqura.Web.MedCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mediqura.Web.MedPhr
{
    public partial class OPSales : BasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MasterLookupBO mstlookup = new MasterLookupBO();
                AutoCompleteExtender3.ContextKey = LogData.MedSubStockID.ToString();
                Commonfunction.PopulateDdl(ddlpaymentmode, mstlookup.GetLookupsList(LookupName.PaymentMode));
                Commonfunction.PopulateDdl(ddl_paymodes, mstlookup.GetLookupsList(LookupName.PaymentMode));
                ddlpaymentmode.SelectedIndex = 1;
                //Commonfunction.PopulateDdl(ddlpaymentmodes, mstlookup.GetLookupsList(LookupName.PaymentMode));
                Commonfunction.PopulateDdl(ddl_responsible, mstlookup.GetLookupsList(LookupName.Employee));
                hdnbillsubmittype.Value = "0";
                Commonfunction.PopulateDdl(ddl_collectedby, mstlookup.GetEmployeeByDep(47));
                if (LogData.MedSubStockID == 2 || LogData.MedSubStockID == 4)
                {
                    txt_custommer.ReadOnly = false;
                    txt_drugsname.ReadOnly = false;
                    txt_composition.ReadOnly = false;
                    btnsave.Visible = true;
                    btnreset.Visible = true;
                }
                else
                {
                    txt_custommer.ReadOnly = true;
                    txt_drugsname.ReadOnly = true;
                    txt_composition.ReadOnly = true;
                    btnsave.Visible = false;
                    btnresets.Visible = false;
                }
            }
        }
        protected void lnkclose_Click(object sender, EventArgs e)
        {

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
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetDrugs(string prefixText, int count, string contextKey)
        {
            MedIndentData Objpaic = new MedIndentData();
            MedStoreIndentBO objInfoBO = new MedStoreIndentBO();
            List<MedIndentData> getResult = new List<MedIndentData>();
            Objpaic.ItemName = prefixText;
            Objpaic.MedSubStockID = Convert.ToInt32(contextKey);
            getResult = objInfoBO.GetItemNameListInStoreBySubstockID(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].ItemName);
            }
            return list;
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> Getsearchbycomposition(string prefixText, int count, string contextKey)
        {
            MedIndentData Objpaic = new MedIndentData();
            MedStoreIndentBO objInfoBO = new MedStoreIndentBO();
            List<MedIndentData> getResult = new List<MedIndentData>();
            Objpaic.ItemName = prefixText;
            Objpaic.MedSubStockID = Convert.ToInt32(contextKey);
            getResult = objInfoBO.SearchDruglistByComposition(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].ItemName);
            }
            return list;
        }
        protected void txt_drugsname_TextChanged(object sender, EventArgs e)
        {
            List<StockGRNData> ListStock = new List<StockGRNData>();
            StockGRNData objStock = new StockGRNData();
            StockGRNBO objBO = new StockGRNBO();
            if (txt_drugsname.Text.Contains(":"))
            {
                bool isIDnumeric = txt_drugsname.Text.Substring(txt_drugsname.Text.LastIndexOf(':') + 1).All(char.IsDigit);
                if (isIDnumeric == true)
                {
                    objStock.ID = isIDnumeric ? Convert.ToInt64(txt_drugsname.Text.Contains(":") ? txt_drugsname.Text.Substring(txt_drugsname.Text.LastIndexOf(':') + 1) : "0") : 0;
                }
                else
                {
                    objStock.ID = 0;
                    txt_drugsname.Text = "";
                    txt_drugsname.Focus();
                    return;
                }
            }
            else
            {
                objStock.ID = 0;
                txt_drugsname.Text = "";
                txt_drugsname.Focus();
                return;
            }
            objStock.SubStockID = LogData.MedSubStockID;
            ListStock = objBO.GetStockItemDetailsBySubStockID(objStock);
            if (ListStock.Count > 0)
            {
                txt_composition.Text = ListStock[0].Remarks.ToString();
                txt_nounit.Text = "";
                txt_equivalentqty.Text = ListStock[0].EquivalentQtyPerUnit.ToString();
                hdnequivalentqty.Value = ListStock[0].EquivalentQtyPerUnit.ToString();
                hdnmrpperqty.Value = ListStock[0].MRPPerQty.ToString();
                txt_totalavail.Text = ListStock[0].EquivalentQtyBalance.ToString();
                txt_Rate.Text = Commonfunction.Getrounding((Convert.ToDecimal(ListStock[0].MRPPerQty) * Convert.ToDecimal(ListStock[0].EquivalentQtyPerUnit)).ToString());
                if (Convert.ToInt32(ListStock[0].ExpireDays) <= 30)
                {
                    txt_nodayscounttoexpire.Text = ListStock[0].ExpireDays + " Days to expire.";
                    txt_nodayscounttoexpire.BackColor = System.Drawing.Color.Red;
                    txt_nodayscounttoexpire.ForeColor = System.Drawing.Color.Black;
                }
                if (Convert.ToInt32(ListStock[0].ExpireDays) < 90)
                {
                    txt_nodayscounttoexpire.Text = ListStock[0].ExpireDays + " Days to expire.";
                    txt_nodayscounttoexpire.BackColor = System.Drawing.Color.Yellow;
                    txt_nodayscounttoexpire.ForeColor = System.Drawing.Color.Black;
                }
                if (Convert.ToInt32(ListStock[0].ExpireDays) > 90)
                {
                    txt_nodayscounttoexpire.Text = ListStock[0].ExpireDays + " Days to expire.";
                    txt_nodayscounttoexpire.BackColor = System.Drawing.Color.Green;
                    txt_nodayscounttoexpire.ForeColor = System.Drawing.Color.White;
                }
                txt_nounit.Focus();
            }
            else
            {
                txt_drugsname.Text = "";
                txt_composition.Text = "";
                txt_nounit.Text = "";
                txt_equivalentqty.Text = "";
                txt_totalavail.Text =
                hdnequivalentqty.Value = "";
                hdnmrpperqty.Value = "";
                txt_Rate.Text = "";
            }
        }
        protected void txt_searchcomposition_TextChanged(object sender, EventArgs e)
        {
            List<StockGRNData> ListStock = new List<StockGRNData>();
            StockGRNData objStock = new StockGRNData();
            StockGRNBO objBO = new StockGRNBO();
            if (txt_composition.Text.Contains(":"))
            {
                bool isIDnumeric = txt_composition.Text.Substring(txt_composition.Text.LastIndexOf(':') + 1).All(char.IsDigit);
                if (isIDnumeric == true)
                {
                    objStock.ID = isIDnumeric ? Convert.ToInt64(txt_composition.Text.Contains(":") ? txt_composition.Text.Substring(txt_composition.Text.LastIndexOf(':') + 1) : "0") : 0;
                }
                else
                {
                    objStock.ID = 0;
                    txt_composition.Text = "";
                }
            }
            else
            {
                objStock.ID = 0;
                txt_composition.Text = "";
            }
            objStock.SubStockID = LogData.MedSubStockID;
            ListStock = objBO.GetStockItemDetailsBySubStockID(objStock);
            if (ListStock.Count > 0)
            {
                txt_drugsname.Text = txt_composition.Text;
                txt_composition.Text = ListStock[0].Remarks.ToString();
                txt_nounit.Text = "";
                txt_equivalentqty.Text = ListStock[0].EquivalentQtyPerUnit.ToString();
                hdnequivalentqty.Value = ListStock[0].EquivalentQtyPerUnit.ToString();
                txt_totalavail.Text = ListStock[0].EquivalentQtyBalance.ToString();
                hdnmrpperqty.Value = ListStock[0].MRPPerQty.ToString();
                txt_Rate.Text = Commonfunction.Getrounding((Convert.ToDecimal(ListStock[0].MRPPerQty) * Convert.ToDecimal(ListStock[0].EquivalentQtyPerUnit)).ToString());
                if (Convert.ToInt32(ListStock[0].ExpireDays) <= 30)
                {
                    txt_nodayscounttoexpire.Text = ListStock[0].ExpireDays + " Days to expire.";
                    txt_nodayscounttoexpire.BackColor = System.Drawing.Color.Red;
                    txt_nodayscounttoexpire.ForeColor = System.Drawing.Color.Black;
                }
                if (Convert.ToInt32(ListStock[0].ExpireDays) < 90)
                {
                    txt_nodayscounttoexpire.Text = ListStock[0].ExpireDays + " Days to expire.";
                    txt_nodayscounttoexpire.BackColor = System.Drawing.Color.Yellow;
                    txt_nodayscounttoexpire.ForeColor = System.Drawing.Color.Black;
                }
                if (Convert.ToInt32(ListStock[0].ExpireDays) > 90)
                {
                    txt_nodayscounttoexpire.Text = ListStock[0].ExpireDays + " Days to expire.";
                    txt_nodayscounttoexpire.BackColor = System.Drawing.Color.Green;
                    txt_nodayscounttoexpire.ForeColor = System.Drawing.Color.White;
                }
                txt_nounit.Focus();
            }
            else
            {
                txt_drugsname.Text = "";
                txt_composition.Text = "";
                txt_totalavail.Text = "";
                txt_nounit.Text = "";
                txt_equivalentqty.Text = "";
                hdnequivalentqty.Value = "";
                hdnmrpperqty.Value = "";
                txt_Rate.Text = "";
            }
        }
        protected void txt_custommer_TextChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt64(hdntransID.Value == "" ? "0" : hdntransID.Value) == 0)
            {
                List<StockGRNData> ListStock = new List<StockGRNData>();
                StockGRNData objStock = new StockGRNData();
                StockGRNBO objBO = new StockGRNBO();
                objStock.CustomerName = txt_custommer.Text.Trim();
                objStock.SubStockID = LogData.MedSubStockID;
                objStock.EmployeeID = LogData.EmployeeID;
                objStock.FinancialYearID = LogData.FinancialYearID;
                objStock.HospitalID = LogData.HospitalID;
                objStock.ActionType = Enumaction.Insert;
                ListStock = objBO.GenerateMedStoreTransactionID(objStock);
                if (ListStock.Count > 0)
                {
                    hdntransID.Value = ListStock[0].TransactionID.ToString();
                    //  txt_custommer.ReadOnly = true;
                    txt_drugsname.Focus();
                }
                else
                {
                    // txt_custommer.ReadOnly = false;
                    hdntransID.Value = "0";
                }
            }
            if (Convert.ToInt64(hdntransID.Value == "" ? "0" : hdntransID.Value) > 0)
            {
                List<StockGRNData> ListStock = new List<StockGRNData>();
                StockGRNData objStock = new StockGRNData();
                StockGRNBO objBO = new StockGRNBO();
                objStock.CustomerName = txt_custommer.Text.Trim();
                objStock.SubStockID = LogData.MedSubStockID;
                objStock.EmployeeID = LogData.EmployeeID;
                objStock.FinancialYearID = LogData.FinancialYearID;
                objStock.HospitalID = LogData.HospitalID;
                objStock.TransactionID = Convert.ToInt64(hdntransID.Value == "" ? "0" : hdntransID.Value);
                if (txt_custommer.Text.Contains(":"))
                {
                    bool isUHIDnumeric = txt_custommer.Text.Substring(txt_custommer.Text.LastIndexOf(':') + 1).All(char.IsDigit);
                    if (isUHIDnumeric == true)
                    {
                        objStock.UHID = isUHIDnumeric ? Convert.ToInt64(txt_custommer.Text.Contains(":") ? txt_custommer.Text.Substring(txt_custommer.Text.LastIndexOf(':') + 1) : "0") : 0;
                    }
                    else
                    {
                        objStock.UHID = 0;
                        txt_drugsname.Text = "";
                    }
                }
                else
                {
                    objStock.UHID = 0;
                }
                objStock.ActionType = Enumaction.Update;
                ListStock = objBO.UpdateMedStoreTransactionID(objStock);
                if (ListStock.Count > 0)
                {
                    hdntransID.Value = ListStock[0].TransactionID.ToString();
                    //txt_custommer.ReadOnly = true;
                    txt_drugsname.Focus();
                }
                else
                {
                    //txt_custommer.ReadOnly = false;
                    hdntransID.Value = "0";
                }
            }
        }
        protected void btnadd_Click(object sender, EventArgs e)
        {
            txt_Rate.Text = Commonfunction.Getrounding((Convert.ToDecimal(txt_equivalentqty.Text == "" ? "0" : txt_equivalentqty.Text) * Convert.ToDecimal(hdnmrpperqty.Value == "" ? "0" : hdnmrpperqty.Value)).ToString());

            if (txt_custommer.Text.Trim() == "")
            {
                Messagealert_.ShowMessage(lblmessage, "Custommer", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                txt_custommer.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
                divmsg1.Visible = false;
            }
            if (txt_drugsname.Text.Trim() == "")
            {
                Messagealert_.ShowMessage(lblmessage, "ItemName", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                txt_drugsname.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
                divmsg1.Visible = false;
            }
            if (txt_nounit.Text.Trim() == "" || txt_nounit.Text.Trim() == "0")
            {
                Messagealert_.ShowMessage(lblmessage, "NoUnit", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                txt_nounit.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
                divmsg1.Visible = false;
            }
            if (txt_equivalentqty.Text.Trim() == "" || txt_equivalentqty.Text.Trim() == "0")
            {
                Messagealert_.ShowMessage(lblmessage, "ReqdQty", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                txt_equivalentqty.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
                divmsg1.Visible = false;
            }
            List<StockGRNData> ListStock = new List<StockGRNData>();
            StockGRNData objStock = new StockGRNData();
            StockGRNBO objBO = new StockGRNBO();
            objStock.CustomerName = txt_custommer.Text.Trim();
            objStock.TransactionID = Convert.ToInt64(hdntransID.Value == "" ? "0" : hdntransID.Value);

            int x = (Convert.ToInt32(txt_equivalentqty.Text == "" ? "0" : txt_equivalentqty.Text) / Convert.ToInt32(hdnequivalentqty.Value == "" ? "0" : hdnequivalentqty.Value));
            int y = (Convert.ToInt32(txt_equivalentqty.Text == "" ? "0" : txt_equivalentqty.Text) % Convert.ToInt32(hdnequivalentqty.Value == "" ? "0" : hdnequivalentqty.Value));

            string Z = (x).ToString() + "." + (y).ToString();

            objStock.NoUnit = Convert.ToDecimal(Z);
            objStock.EquivalentQty = Convert.ToInt32(txt_equivalentqty.Text == "" ? "0" : txt_equivalentqty.Text);
            // txt_Rate.Text = Commonfunction.Getrounding((Convert.ToInt32(txt_equivalentqty.Text == "" ? "0" : txt_equivalentqty.Text) * Convert.ToInt32(hdnmrpperqty.Value == "" ? "0" : hdnmrpperqty.Value)).ToString());
            objStock.NetCharge = Convert.ToDecimal(txt_Rate.Text == "" ? "0" : txt_Rate.Text);
            objStock.MRPPerQty = Convert.ToDecimal(hdnmrpperqty.Value == "" ? "0" : hdnmrpperqty.Value);
            if (txt_drugsname.Text.Contains(":"))
            {
                bool isIDnumeric = txt_drugsname.Text.Substring(txt_drugsname.Text.LastIndexOf(':') + 1).All(char.IsDigit);
                if (isIDnumeric == true)
                {
                    objStock.ID = isIDnumeric ? Convert.ToInt64(txt_drugsname.Text.Contains(":") ? txt_drugsname.Text.Substring(txt_drugsname.Text.LastIndexOf(':') + 1) : "0") : 0;
                }
                else
                {
                    objStock.ID = 0;
                    txt_drugsname.Text = "";
                }
            }
            else
            {
                objStock.ID = 0;
                txt_drugsname.Text = "";
            }
            if (txt_custommer.Text.Contains(":"))
            {
                bool isUHIDnumeric = txt_custommer.Text.Substring(txt_custommer.Text.LastIndexOf(':') + 1).All(char.IsDigit);
                if (isUHIDnumeric == true)
                {
                    objStock.UHID = isUHIDnumeric ? Convert.ToInt64(txt_custommer.Text.Contains(":") ? txt_custommer.Text.Substring(txt_custommer.Text.LastIndexOf(':') + 1) : "0") : 0;
                }
                else
                {
                    objStock.UHID = 0;
                    txt_drugsname.Text = "";
                }
            }
            else
            {
                objStock.UHID = 0;
            }
            objStock.SubStockID = LogData.MedSubStockID;
            objStock.EmployeeID = LogData.EmployeeID;
            objStock.FinancialYearID = LogData.FinancialYearID;
            objStock.HospitalID = LogData.HospitalID;
            ListStock = objBO.UpdateMedStockTransactiondetails(objStock);
            if (ListStock.Count > 0)
            {
                txt_totalamount.Text = Commonfunction.Getrounding(ListStock[0].TotalBillAmount.ToString());
                txt_PaidAmount.Text = Math.Round(Convert.ToDecimal(txt_totalamount.Text == "" ? "0" : txt_totalamount.Text)).ToString();
                GvItemlist.DataSource = ListStock;
                GvItemlist.DataBind();
                hdnmrpperqty.Value = null;
                hdnequivalentqty.Value = null;
                txt_equivalentqty.Text = "";
                txt_nounit.Text = "";
                txt_totalavail.Text = "";
                txt_composition.Text = "";
                txt_drugsname.Text = "";
                txt_Rate.Text = "";
                txt_drugsname.Focus();
                txt_nodayscounttoexpire.BackColor = System.Drawing.Color.White;
                txt_nodayscounttoexpire.Text = "";
                GvItemlist.Visible = true;
            }
            else
            {
                txt_totalamount.Text = "";
                txt_discountvalue.Text = "";
                txt_discountPC.Text = "";
                txt_PaidAmount.Text = "";
                txt_dueAmount.Text = "";
                GvItemlist.DataSource = null;
                GvItemlist.DataBind();
            }
        }
        protected void GvItemlist_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Deletes")
                {
                    StockGRNData objbill = new StockGRNData();
                    StockGRNBO objstdBO = new StockGRNBO();

                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GvItemlist.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("lblID");
                    Label Transaction = (Label)gr.Cells[0].FindControl("lbl_trasactionID");
                    Label SubstockID = (Label)gr.Cells[0].FindControl("lbl_substockID");
                    Label NoUnot = (Label)gr.Cells[0].FindControl("lbl_unit");
                    Label EquivalentQty = (Label)gr.Cells[0].FindControl("lbl_qty");
                    objbill.ID = Convert.ToInt64(ID.Text == "" ? "0" : ID.Text);
                    objbill.TransactionID = Convert.ToInt64(Transaction.Text == "" ? "0" : Transaction.Text);
                    objbill.NoUnit = Convert.ToDecimal(NoUnot.Text == "" ? "0" : NoUnot.Text);
                    objbill.EquivalentQty = Convert.ToInt32(EquivalentQty.Text == "" ? "0" : EquivalentQty.Text);
                    objbill.EmployeeID = LogData.EmployeeID;
                    objbill.SubStockID = Convert.ToInt64(SubstockID.Text == "" ? "0" : SubstockID.Text);
                    objbill.MedSubStockTypeID = LogData.MedSubStockID;
                    int Result = objstdBO.DeleteMedstoreTransactionByID(objbill);
                    if (Result == 1)
                    {
                        bindgriditemlist();
                        //Messagealert_.ShowMessage(lblmessage, "delete", 1);
                        //lblmessage.Attributes["class"] = "SucessAlert";
                        //divmsg1.Visible = true;
                    }
                    else
                    {
                        Messagealert_.ShowMessage(lblmessage, "system", 0);
                        lblmessage.Attributes["class"] = "FailAlert";
                        divmsg1.Visible = true;
                    }
                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
                lblmessage1.Attributes["class"] = "FailAlert";
                divmsg1.Visible = true;
            }
        }
        private void bindgriditemlist()
        {
            List<StockGRNData> ListStock = new List<StockGRNData>();
            StockGRNData objStock = new StockGRNData();
            StockGRNBO objBO = new StockGRNBO();
            objStock.CustomerName = txt_custommer.Text.Trim();
            objStock.TransactionID = Convert.ToInt64(hdntransID.Value == "" ? "0" : hdntransID.Value);
            objStock.UHID = 0;
            objStock.ID = 0;
            objStock.SubStockID = LogData.MedSubStockID;
            objStock.EmployeeID = LogData.EmployeeID;
            objStock.FinancialYearID = LogData.FinancialYearID;
            objStock.HospitalID = LogData.HospitalID;
            ListStock = objBO.GetMedStockTransactiondetails(objStock);
            if (ListStock.Count > 0)
            {
                GvItemlist.DataSource = ListStock;
                GvItemlist.DataBind();
                txt_totalamount.Text = Commonfunction.Getrounding(ListStock[0].TotalBillAmount.ToString());
                txt_PaidAmount.Text = Math.Round(Convert.ToDecimal(txt_totalamount.Text == "" ? "0" : txt_totalamount.Text)).ToString();
                txt_discountPC.Text = "";
                txt_discountvalue.Text = "";

            }
            else
            {
                txt_totalamount.Text = "";
                txt_PaidAmount.Text = "";
                GvItemlist.DataSource = null;
                GvItemlist.DataBind();
            }
        }
        protected void ddlpaymentmode_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtbank.Text = "";
            txt_chequenumber.Text = "";
            txtinvoicenumber.Text = "";
            txt_discountPC.Focus();
            if (ddlpaymentmode.SelectedIndex > 0)
            {

                if (ddlpaymentmode.SelectedValue == "1")
                {
                    txtbank.Text = "";
                    txtbank.ReadOnly = true;
                    txt_chequenumber.ReadOnly = true;
                    txtinvoicenumber.ReadOnly = true;
                }
                if (ddlpaymentmode.SelectedValue == "2")
                {
                    GetBankName(Convert.ToInt32(ddlpaymentmode.SelectedValue == "" ? "0" : ddlpaymentmode.SelectedValue));
                    txtbank.ReadOnly = true;
                    txt_chequenumber.ReadOnly = true;
                    txtinvoicenumber.ReadOnly = false;
                }
                if (ddlpaymentmode.SelectedValue == "3")
                {
                    GetBankName(Convert.ToInt32(ddlpaymentmode.SelectedValue == "" ? "0" : ddlpaymentmode.SelectedValue));
                    txtbank.ReadOnly = true;
                    txt_chequenumber.ReadOnly = false;
                    txtinvoicenumber.ReadOnly = true;
                }
                if (ddlpaymentmode.SelectedValue == "4")
                {
                    txtbank.Text = "";
                    txtbank.ReadOnly = false;
                    txt_chequenumber.ReadOnly = false;
                    txtinvoicenumber.ReadOnly = true;
                }
            }
            else
            {
                txtbank.Text = "";
                txtbank.ReadOnly = true;
                txt_chequenumber.ReadOnly = true;
                txtinvoicenumber.ReadOnly = true;
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
        private void save()
        {
            try
            {
                if (txt_custommer.Text.Trim() == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "Custommer", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    txt_custommer.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                    divmsg1.Visible = false;
                }
                if (ddlpaymentmode.SelectedIndex == 0 && hdnbillsubmittype.Value != "2")
                {
                    Messagealert_.ShowMessage(lblmessage, "Paymode", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                    divmsg1.Visible = false;
                }
                if (ddlpaymentmode.SelectedIndex > 1)
                {
                    if (txtbank.Text == "")
                    {
                        Messagealert_.ShowMessage(lblmessage, "BankName", 0);
                        divmsg1.Visible = true;
                        divmsg1.Attributes["class"] = "FailAlert";
                        txtbank.Focus();
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                        divmsg1.Visible = false;
                    }
                    if (txt_chequenumber.Text == "")
                    {
                        Messagealert_.ShowMessage(lblmessage, "Chequenumber", 0);
                        divmsg1.Visible = true;
                        divmsg1.Attributes["class"] = "FailAlert";
                        txt_chequenumber.Focus();
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                        divmsg1.Visible = false;
                    }
                }
                else
                {
                    lblmessage.Visible = false;
                    divmsg1.Visible = false;
                }
                if (Convert.ToDecimal(txt_dueAmount.Text.Trim() == "" ? "0" : txt_dueAmount.Text.Trim()) > 0)
                {
                    if (ddl_responsible.SelectedIndex == 0)
                    {
                        ddl_responsible.Attributes.Remove("disabled");
                        Messagealert_.ShowMessage(lblmessage, "Dueresponsible", 0);
                        divmsg1.Visible = true;
                        divmsg1.Attributes["class"] = "FailAlert";
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                        divmsg1.Visible = false;
                    }
                }
                if (Convert.ToDecimal(txt_discountvalue.Text.Trim() == "" ? "0" : txt_discountvalue.Text) > 0 || Convert.ToDecimal(txt_dueAmount.Text.Trim() == "" ? "0" : txt_dueAmount.Text.Trim()) > 0)
                {
                    if (txt_remarks.Text.Trim() == "")
                    {
                        Messagealert_.ShowMessage(lblmessage, "Remarks", 0);
                        divmsg1.Visible = true;
                        divmsg1.Attributes["class"] = "FailAlert";
                        txt_remarks.Focus();
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                        divmsg1.Visible = false;
                    }
                }
                List<MedOPDSalesData> Liststock = new List<MedOPDSalesData>();
                StockStatusBO ObjBO = new StockStatusBO();
                MedOPDSalesData objsalesData = new MedOPDSalesData();

                foreach (GridViewRow row in GvItemlist.Rows)
                {

                    Label ID = (Label)GvItemlist.Rows[row.RowIndex].Cells[0].FindControl("lblID");
                    Label SubstockID = (Label)GvItemlist.Rows[row.RowIndex].Cells[0].FindControl("lbl_substockID");
                    Label SerialID = (Label)GvItemlist.Rows[row.RowIndex].Cells[0].FindControl("lblserialID");
                    Label TransactionID = (Label)GvItemlist.Rows[row.RowIndex].Cells[0].FindControl("lbl_trasactionID");
                    Label ItemID = (Label)GvItemlist.Rows[row.RowIndex].Cells[0].FindControl("lbl_ItemID");
                    Label ItemName = (Label)GvItemlist.Rows[row.RowIndex].Cells[0].FindControl("lblitemname");
                    Label MRPperQty = (Label)GvItemlist.Rows[row.RowIndex].Cells[0].FindControl("lbl_rate");
                    Label NoUnit = (Label)GvItemlist.Rows[row.RowIndex].Cells[0].FindControl("lbl_unit");
                    Label Qty = (Label)GvItemlist.Rows[row.RowIndex].Cells[0].FindControl("lbl_qty");
                    Label NetCharge = (Label)GvItemlist.Rows[row.RowIndex].Cells[0].FindControl("lbl_netcharges");
                    Label Batch = (Label)GvItemlist.Rows[row.RowIndex].Cells[0].FindControl("lbl_batch");
                    MedOPDSalesData ObjDetails = new MedOPDSalesData();

                    ObjDetails.ID = Convert.ToInt64(ID.Text == "" ? "0" : ID.Text);
                    ObjDetails.SubStockID = Convert.ToInt64(SubstockID.Text == "" ? "0" : SubstockID.Text);
                    ObjDetails.SerialID = Convert.ToInt32(SerialID.Text == "" ? "0" : SerialID.Text);
                    ObjDetails.ItemID = Convert.ToInt32(ItemID.Text == "" ? "0" : ItemID.Text);
                    ObjDetails.ItemName = ItemName.Text.Trim();
                    ObjDetails.BatchNo = Batch.Text.Trim();
                    ObjDetails.TransactionID = Convert.ToInt64(TransactionID.Text == "" ? "0" : TransactionID.Text);
                    ObjDetails.NoUnit = Convert.ToDecimal(NoUnit.Text == "" ? "0" : NoUnit.Text);
                    ObjDetails.Quantity = Convert.ToInt32(Qty.Text == "" ? "0" : Qty.Text);
                    ObjDetails.MRPperQty = Convert.ToDecimal(MRPperQty.Text == "" ? "0" : MRPperQty.Text);
                    ObjDetails.NetCharge = Convert.ToDecimal(NetCharge.Text == "" ? "0" : NetCharge.Text);
                    Liststock.Add(ObjDetails);
                }
                objsalesData.XMLData = XmlConvertor.MedSaleStockDatatoXML(Liststock).ToString();
                objsalesData.Paymode = Convert.ToInt32(ddlpaymentmode.SelectedValue == "" ? "0" : ddlpaymentmode.SelectedValue);
                objsalesData.BankName = txtbank.Text.Trim();
                objsalesData.CardNo_ChequeNo = txt_chequenumber.Text.Trim();
                objsalesData.InvoiceNo = txtinvoicenumber.Text.Trim();
                objsalesData.TotalBillAmount = Convert.ToDecimal(txt_totalamount.Text == "" ? "0" : txt_totalamount.Text);
                objsalesData.Discount = Convert.ToDecimal(txt_discountvalue.Text == "" ? "0" : txt_discountvalue.Text);
                objsalesData.PaidAmount = Convert.ToDecimal(txt_PaidAmount.Text == "" ? "0" : txt_PaidAmount.Text);
                objsalesData.DueAmount = Convert.ToDecimal(txt_dueAmount.Text == "" ? "0" : txt_dueAmount.Text);
                if (hdnbillsubmittype.Value == "0" || hdnbillsubmittype.Value == "1" || hdnbillsubmittype.Value == "3")
                {
                    objsalesData.SubmitType = 1;
                }
                if (hdnbillsubmittype.Value == "2")
                {
                    objsalesData.SubmitType = 2;
                }
                objsalesData.CustomerName = txt_custommer.Text.Trim();
                if (txt_custommer.Text.Contains(":"))
                {
                    bool isUHIDnumeric = txt_custommer.Text.Substring(txt_custommer.Text.LastIndexOf(':') + 1).All(char.IsDigit);
                    if (isUHIDnumeric == true)
                    {
                        objsalesData.UHID = isUHIDnumeric ? Convert.ToInt64(txt_custommer.Text.Contains(":") ? txt_custommer.Text.Substring(txt_custommer.Text.LastIndexOf(':') + 1) : "0") : 0;
                    }
                    else
                    {
                        objsalesData.UHID = 0;
                        txt_drugsname.Text = "";
                    }
                }
                else
                {
                    objsalesData.UHID = 0;
                }
                objsalesData.BankID = Convert.ToInt32(hdnbankID.Value == "" || hdnbankID.Value == null ? "0" : hdnbankID.Value);
                objsalesData.TransactionID = Convert.ToInt64(hdntransID.Value == "" ? "0" : hdntransID.Value);
                objsalesData.EmployeeID = LogData.EmployeeID;
                objsalesData.DueReponsibleBy = Convert.ToInt64(ddl_responsible.SelectedValue == "" ? "0" : ddl_responsible.SelectedValue);
                objsalesData.Remarks = txt_remarks.Text.Trim();
                objsalesData.FinancialYearID = LogData.FinancialYearID;
                objsalesData.HospitalID = LogData.HospitalID;
                Liststock = ObjBO.UpdateOPsales(objsalesData);
                if (Liststock.Count > 0)
                {
                    hdnbankID.Value = null;
                    hdnequivalentqty.Value = null;
                    hdnmrpperqty.Value = null;
                    hdntransID.Value = null;
                    lblmessage.Visible = true;
                    bool result = Commonfunction.Clear_PHR_Uncompletetransactions(LogData.FinancialYearID, LogData.EmployeeID, LogData.MedSubStockID);
                    if (result == false)
                    {
                        Response.Redirect("~/Login.aspx", false);
                        return;
                    }
                    if (Liststock[0].BillNo.ToString() != "")
                    {
                        txt_billNo.Text = Liststock[0].BillNo.ToString();
                        Messagealert_.ShowMessage(lblmessage, "save", 1);
                        divmsg1.Attributes["class"] = "SucessAlert";
                        divmsg1.Visible = true;
                    }
                    if (Liststock[0].ReqNo.ToString() != "")
                    {
                        txt_billNo.Text = Liststock[0].ReqNo.ToString();
                        Messagealert_.ShowMessage(lblmessage, "Reqsent", 1);
                        divmsg1.Attributes["class"] = "SucessAlert";
                        divmsg1.Visible = true;
                    }
                    btnsave.Attributes["disabled"] = "disabled";
                    txt_nodayscounttoexpire.BackColor = System.Drawing.Color.White;
                    txt_nodayscounttoexpire.Text = "";
                }
                else
                {
                    btnsave.Attributes.Remove("disabled");
                    txt_billNo.Text = "";
                    Messagealert_.ShowMessage(lblmessage1, "system", 0);
                    lblmessage1.Attributes["class"] = "FailAlert";
                    divmsg1.Visible = true;
                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
                divmsg1.Attributes["class"] = "FailAlert";
                divmsg1.Visible = true;
            }
        }
		protected void btnyes_Click(object sender, EventArgs e)
		{
			mpconfirmation.Hide();
            save();
		}
		protected void btnno_Click(object sender, EventArgs e)
		{
			mpconfirmation.Hide();
		}
        protected void btnsave_Click(object sender, EventArgs e)
        {

			lbl_paymentmode.Text = ddlpaymentmode.SelectedItem.Text.Trim() +" "+ txtbank.Text.Trim()+" " + txt_chequenumber.Text.Trim()+" " + txtinvoicenumber.Text.Trim();
			lbl_totaldiscount.Text = txt_discountvalue.Text.Trim() == "" ? "0" : txt_discountvalue.Text.Trim();
			lbl_totpaidamount.Text = txt_PaidAmount.Text.Trim() == "" ? "0" : txt_PaidAmount.Text.Trim();
			lbl_totdueamount.Text = txt_dueAmount.Text.Trim() =="" ? "0" :txt_dueAmount.Text.Trim();
			if (hdnbillsubmittype.Value == "2")
			{
				mpconfirmation.Hide();
                save();
			}
			else
			{
				mpconfirmation.Show();
				btnyes.Focus();
			}
        }
        protected void btnresets_Click(object sender, EventArgs e)
        {
            lblmessage.Visible = false;
            txt_custommer.ReadOnly = false;
            txt_discountvalue.ReadOnly = false;
            txt_discountPC.ReadOnly = false;
            txt_PaidAmount.ReadOnly = false;
            txt_drugsname.ReadOnly = false;
            txt_composition.ReadOnly = false;
            txt_nounit.ReadOnly = false;
            txt_equivalentqty.ReadOnly = false;
            txt_billNo.Text = "";
            txt_chequenumber.Text = "";
            txt_composition.Text = "";
            txt_discountPC.Text = "";
            txt_discountvalue.Text = "";
            txt_drugsname.Text = "";
            txt_equivalentqty.Text = "";
            txt_nounit.Text = "";
            txt_PaidAmount.Text = "";
            txt_Rate.Text = "";
            txt_totalamount.Text = "";
            txt_totalavail.Text = "";
            txt_remarks.Text = "";
            //   txt_totalitemcount.Text = "";
            txtinvoicenumber.Text = "";
            txtbank.Text = "";
            hdnbankID.Value = null;
            hdnequivalentqty.Value = null;
            hdnmrpperqty.Value = null;
            hdntransID.Value = null;
            GvItemlist.DataSource = null;
            GvItemlist.DataBind();
            GvItemlist.Visible = false;
            txt_custommer.Text = "";
            ddl_responsible.SelectedIndex = 0;
            // ddlpaymentmode.SelectedIndex = 1;
            btnsave.Attributes.Remove("disabled");
            hdnbillsubmittype.Value = "0";
            txt_nodayscounttoexpire.BackColor = System.Drawing.Color.White;
            txt_nodayscounttoexpire.Text = "";
            bool result = Commonfunction.Clear_PHR_Uncompletetransactions(LogData.FinancialYearID, LogData.EmployeeID, LogData.MedSubStockID);
			if (result == false)
			{
				Response.Redirect("~/Login.aspx", false);
			}
			else {
				Response.Redirect("~/CurrentPatientlist.aspx", false);
			}
        }
        protected void btn_reqsearch_Click(object sender, EventArgs e)
        {
            bindrequestlist();
        }
        private void bindrequestlist()
        {
            List<MedOPDSalesData> Liststock = new List<MedOPDSalesData>();
            StockStatusBO ObjBO = new StockStatusBO();
            MedOPDSalesData objsalesData = new MedOPDSalesData();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime from = txt_reqdatefrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txt_reqdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txt_reqdateto.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txt_reqdateto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objsalesData.ReqNo = txt_reqno.Text.Trim() == "" ? "" : txt_reqno.Text.Trim();
            objsalesData.Datefrom = from;
            objsalesData.Dateto = To;
            objsalesData.RequestStatus = Convert.ToInt32(ddl_RequestStatus.SelectedValue == "" ? "0" : ddl_RequestStatus.SelectedValue);
            objsalesData.IsActive = ddl_status.SelectedIndex == 0 ? true : false;
            objsalesData.PatientType = 1;
            objsalesData.FinancialYearID = LogData.FinancialYearID;
            objsalesData.HospitalID = LogData.HospitalID;
            Liststock = ObjBO.GetdiscountRequestList(objsalesData);
            if (Liststock.Count > 0)
            {
                Messagealert_.ShowMessage(lbl_result3, "Total:" + Liststock[0].MaximumRows.ToString() + " Record(s) found.", 1);
                div5.Attributes["class"] = "SucessAlert";
                GvDiscountRequest.Visible = true;
                GvDiscountRequest.DataSource = Liststock;
                GvDiscountRequest.DataBind();
            }
            else
            {
                lbl_result3.Visible = false;
                GvDiscountRequest.DataSource = Liststock;
                GvDiscountRequest.DataBind();
            }
        }
        protected void GvItemlist_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton delete = (LinkButton)e.Row.FindControl("lnkDelete");
                if (hdnbillsubmittype.Value == "3")
                {
                    delete.Visible = false;
                }
                else
                {
                    delete.Visible = true;
                }
            }

        }
        protected void gvstockstatus_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label StockStaus = (Label)e.Row.FindControl("lbl_statusID");
                Label Label1 = (Label)e.Row.FindControl("lblstatus");
                LinkButton delete = (LinkButton)e.Row.FindControl("lnkDelete");
                LinkButton Pay = (LinkButton)e.Row.FindControl("lnkpay");
                if (StockStaus.Text == "1")
                {
                    e.Row.Cells[8].BackColor = System.Drawing.Color.Yellow;
                    Label1.ForeColor = System.Drawing.Color.Black;
                    delete.Visible = true;
                    Pay.Visible = false;
                }
                if (StockStaus.Text == "2")
                {
                    e.Row.Cells[8].BackColor = System.Drawing.Color.Green;
                    Label1.ForeColor = System.Drawing.Color.White;
                    delete.Visible = false;
                    Pay.Visible = true;
                }
                if (StockStaus.Text == "3")
                {
                    e.Row.Cells[8].BackColor = System.Drawing.Color.Red;
                    Label1.ForeColor = System.Drawing.Color.White;
                    delete.Visible = false;
                    Pay.Visible = false;
                }
                if (StockStaus.Text == "4")
                {
                    e.Row.Cells[8].BackColor = System.Drawing.Color.Green;
                    Label1.ForeColor = System.Drawing.Color.White;
                    delete.Visible = false;
                    Pay.Visible = false;
                }
            }
        }
        protected void GvbillList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Deletes")
                {
                    if (LogData.RoleID == 1 || LogData.RoleID == 40)
                    {
                        StockGRNData objbill = new StockGRNData();
                        StockGRNBO objstdBO = new StockGRNBO();

                        int i = Convert.ToInt16(e.CommandArgument.ToString());
                        GridViewRow gr = GvbillList.Rows[i];
                        Label ID = (Label)gr.Cells[0].FindControl("lbl_ID");
                        Label Transaction = (Label)gr.Cells[0].FindControl("lbl_transactionID");
                        Label Billno = (Label)gr.Cells[0].FindControl("lbl_billno");
                        TextBox Remarks = (TextBox)gr.Cells[0].FindControl("txtremarks");

                        objbill.ID = Convert.ToInt64(ID.Text == "" ? "0" : ID.Text);
                        objbill.TransactionID = Convert.ToInt64(Transaction.Text == "" ? "0" : Transaction.Text);
                        objbill.BillNo = Billno.Text.Trim();
                        if (Remarks.Text == "")
                        {
                            Messagealert_.ShowMessage(lbl_result3, "Remarks", 0);
                            div5.Attributes["class"] = "FailAlert";
                            div5.Visible = false;
                            Remarks.Focus();
                            return;
                        }
                        else
                        {
                            objbill.Remarks = Remarks.Text;
                        }
                        objbill.MedSubStockTypeID = LogData.MedSubStockID;
                        objbill.EmployeeID = LogData.EmployeeID;
                        objbill.HospitalID = LogData.HospitalID;
                        int Result = objstdBO.DeleteMedOPDbill(objbill);
                        if (Result == 1)
                        {
                            bindBilllist();
                            Messagealert_.ShowMessage(lblresult1, "delete", 1);
                            div2.Attributes["class"] = "SucessAlert";
                            div2.Visible = true;
                        }
                        else
                        {
                            Messagealert_.ShowMessage(lblresult1, "system", 0);
                            div2.Attributes["class"] = "FailAlert";
                            div2.Visible = true;
                        }
                    }
                    else
                    {
                        Messagealert_.ShowMessage(lblresult1, "DeleteEnable", 0);
                        div2.Attributes["class"] = "FailAlert";
                        div2.Visible = true;
                    }
                }

            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
                lblmessage1.Attributes["class"] = "FailAlert";
                divmsg1.Visible = true;
            }
        }
        protected void GvDiscountRequest_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Deletes")
                {
                    StockGRNData objbill = new StockGRNData();
                    StockGRNBO objstdBO = new StockGRNBO();

                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GvDiscountRequest.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("lbl_ID");
                    Label Transaction = (Label)gr.Cells[0].FindControl("lbl_transID");
                    Label reqNo = (Label)gr.Cells[0].FindControl("lbl_reqno");
                    TextBox Remarks = (TextBox)gr.Cells[0].FindControl("txtremarks");

                    objbill.ID = Convert.ToInt64(ID.Text == "" ? "0" : ID.Text);
                    objbill.TransactionID = Convert.ToInt64(Transaction.Text == "" ? "0" : Transaction.Text);
                    objbill.ReqNo = reqNo.Text.Trim();
                    if (Remarks.Text == "")
                    {
                        Messagealert_.ShowMessage(lbl_result3, "Remarks", 0);
                        div5.Attributes["class"] = "FailAlert";
                        div5.Visible = false;
                        Remarks.Focus();
                        return;
                    }
                    else
                    {
                        objbill.Remarks = Remarks.Text;
                    }
                    objbill.MedSubStockTypeID = LogData.MedSubStockID;
                    objbill.EmployeeID = LogData.EmployeeID;
                    objbill.HospitalID = LogData.HospitalID;
                    int Result = objstdBO.DeleteMedDiscountRequestByID(objbill);
                    if (Result == 1)
                    {
                        bindrequestlist();
                        Messagealert_.ShowMessage(lbl_result3, "delete", 1);
                        div5.Attributes["class"] = "SucessAlert";
                        div5.Visible = true;
                    }
                    else
                    {
                        Messagealert_.ShowMessage(lbl_result3, "system", 0);
                        div5.Attributes["class"] = "FailAlert";
                        div5.Visible = true;
                    }
                }
                if (e.CommandName == "Pay")
                {
                    StockGRNData objbill = new StockGRNData();
                    StockGRNBO objstdBO = new StockGRNBO();

                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = GvDiscountRequest.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("lbl_ID");
                    Label Transaction = (Label)gr.Cells[0].FindControl("lbl_transID");
                    Label reqNo = (Label)gr.Cells[0].FindControl("lbl_reqno");

                    objbill.ID = Convert.ToInt64(ID.Text == "" ? "0" : ID.Text);
                    objbill.TransactionID = Convert.ToInt64(Transaction.Text == "" ? "0" : Transaction.Text);
                    objbill.ReqNo = reqNo.Text.Trim();
                    objbill.MedSubStockTypeID = LogData.MedSubStockID;
                    List<StockGRNData> Result = objstdBO.GetDiscountreqDetailsforPayment(objbill);
                    if (Result.Count > 0)
                    {
                        hdnbillsubmittype.Value = "3";
                        txt_custommer.Text = Result[0].CustomerName.ToString();
                        txt_totalamount.Text = Commonfunction.Getrounding(Result[0].TotalBillAmount.ToString());
                        txt_discountvalue.Text = Commonfunction.Getrounding(Result[0].ApprovedAmount.ToString());
                        txt_PaidAmount.Text = Commonfunction.Getrounding((Convert.ToDecimal(Result[0].TotalBillAmount) - Convert.ToDecimal(Result[0].ApprovedAmount)).ToString());
                        hdntransID.Value = Result[0].TransactionID.ToString();
                        lblmessage.Visible = false;
                        txt_custommer.ReadOnly = true;
                        txt_discountvalue.ReadOnly = true;
                        txt_discountPC.ReadOnly = true;
                        txt_PaidAmount.ReadOnly = true;
                        txt_drugsname.ReadOnly = true;
                        txt_composition.ReadOnly = true;
                        txt_nounit.ReadOnly = true;
                        txt_equivalentqty.ReadOnly = true;
                        txt_dueAmount.Text = "";
                        txt_dueAmount.ReadOnly = true;
                        txt_remarks.Text = Result[0].Remarks.ToString();
                        GvItemlist.DataSource = Result;
                        GvItemlist.DataBind();
                        GvItemlist.Visible = true;
                        btnsave.Attributes.Remove("disabled");
                        ddl_responsible.Attributes["disabled"] = "disabled";
                        Maintabcontainor.ActiveTabIndex = 0;
                        ddlpaymentmode.SelectedIndex = 1;
                    }
                    else
                    {
                        txt_custommer.Text = "";
                        txt_totalamount.Text = "";
                        txt_discountvalue.Text = "";
                        txt_PaidAmount.Text = "";
                        hdntransID.Value = "0";
                        txt_dueAmount.ReadOnly = false;
                        txt_custommer.ReadOnly = false;
                        txt_discountvalue.ReadOnly = false;
                        txt_discountPC.ReadOnly = false;
                        txt_PaidAmount.ReadOnly = false;
                        txt_PaidAmount.ReadOnly = false;
                        txt_drugsname.ReadOnly = false;
                        txt_composition.ReadOnly = false;
                        txt_nounit.ReadOnly = false;
                        txt_equivalentqty.ReadOnly = false;
                        txt_remarks.Text = "";
                        btnsave.Attributes["disabled"] = "disabled";
                        ddl_responsible.Attributes.Remove("disabled");
                        GvItemlist.DataSource = Result;
                        GvItemlist.DataBind();
                    }
                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
                lblmessage1.Attributes["class"] = "FailAlert";
                divmsg1.Visible = true;
            }
        }
        protected void btnsearc_Click(object sender, EventArgs e)
        {
            bindBilllist();
        }
        private void bindBilllist()
        {
            List<MedOPDSalesData> listbill = new List<MedOPDSalesData>();
            StockStatusBO ObjBO = new StockStatusBO();
            MedOPDSalesData objbill = new MedOPDSalesData();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime from = txt_datefrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txt_datefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txt_dateto.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txt_dateto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objbill.BillNo = txt_billnos.Text.Trim() == "" ? "" : txt_billnos.Text.Trim();
            objbill.Datefrom = from;
            objbill.Dateto = To;
            objbill.FinancialYearID = LogData.FinancialYearID;
            objbill.Paymode = Convert.ToInt32(ddl_paymodes.SelectedValue == "" ? "0" : ddl_paymodes.SelectedValue);
            objbill.CollectedByID = Convert.ToInt64(ddl_collectedby.SelectedValue == "" ? "0" : ddl_collectedby.SelectedValue);
            objbill.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
            objbill.HospitalID = LogData.HospitalID;
            listbill = ObjBO.GetMedOpbillList(objbill);
            if (listbill.Count > 0)
            {
                txt_totalbillamount.Text = Commonfunction.Getrounding(listbill[0].SumtotalBillAmount.ToString());
                txt_totaldiscount.Text = Commonfunction.Getrounding(listbill[0].SumTotalDiscount.ToString());
                txt_totalcollected.Text = Commonfunction.Getrounding(listbill[0].sumTotalPaid.ToString());
                txt_totaldueamount.Text = Commonfunction.Getrounding(listbill[0].sumTotalDueAmnt.ToString());
                Messagealert_.ShowMessage(lblresult1, "Total:" + listbill[0].MaximumRows.ToString() + " Record(s) found.", 1);
                div2.Attributes["class"] = "SucessAlert";
                GvbillList.Visible = true;
                GvbillList.DataSource = listbill;
                GvbillList.DataBind();
            }
            else
            {
                txt_totalbillamount.Text = "";
                txt_totaldiscount.Text = "";
                txt_totalcollected.Text = "";
                txt_totaldueamount.Text = "";
                lblresult1.Visible = false;
                GvbillList.DataSource = listbill;
                GvbillList.DataBind();
            }
        }
        protected void btnreset_Click(object sender, EventArgs e)
        {
            txt_billnos.Text = "";
            txt_datefrom.Text = "";
            txt_dateto.Text = "";
            ddlpaymentmode.SelectedIndex = 1;
            ddl_collectedby.SelectedIndex = 0;
            ddlstatus.SelectedIndex = 0;
            lblmessage1.Visible = false;
            lblresult1.Visible = false;
            GvbillList.DataSource = null;
            GvbillList.DataBind();
            GvbillList.Visible = false;
            txt_totalbillamount.Text = "";
            txt_totaldiscount.Text = "";
            txt_totalcollected.Text = "";
            txt_totaldueamount.Text = "";

        }
        protected void btnreserREQ_Click(object sender, EventArgs e)
        {
            txt_reqno.Text = "";
            txt_reqdatefrom.Text = "";
            txt_reqdateto.Text = "";
            GvDiscountRequest.DataSource = null;
            GvDiscountRequest.DataBind();
            GvDiscountRequest.Visible = false;
            lbl_result3.Visible = false;
            lblmessage.Visible = false;
        }


    }
}
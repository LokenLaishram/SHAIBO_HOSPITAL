using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedAccount;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.MedAccount;
using Mediqura.Utility;
using Mediqura.Utility.Logging;
using Mediqura.Web.MedCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mediqura.Web.MedAccount
{
    public partial class LabCollection : BasePage
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
            Commonfunction.PopulateDdl(ddl_transactionType, mstlookup.GetLookupsList(LookupName.PHRPaymentType));
            Commonfunction.PopulateDdl(ddlcollectedby, mstlookup.GetLookupsList(LookupName.CollectedBy));
            txtdatefrom.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            txtto.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
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

            bindgrid(1);
            bindExpgrid(1);
        }
        protected void bindgrid(int page)
        {
            try
            {
                List<LabIncomeCollectionData> objIncome = GetLabIncomeCollectionList(page);
                if (objIncome.Count > 0)
                {
                    if (LogData.PrintEnable == 0)
                    {
                        btnprints.Attributes["disabled"] = "disabled";
                    }
                    else
                    {
                        btnprints.Attributes.Remove("disabled");
                    }
                    gvdepositlist.VirtualItemCount = objIncome[0].MaximumRows;//total item is required for custom paging
                    gvdepositlist.PageIndex = page - 1;

                    gvdepositlist.DataSource = objIncome;
                    gvdepositlist.DataBind();
                    gvdepositlist.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total:" + objIncome[0].MaximumRows.ToString() + " Record(s) found.", 1);
                    divmsg3.Attributes["class"] = "SucessAlert";
                    divmsg3.Visible = true;
                    txttotalbillamount.Text = Commonfunction.Getrounding(objIncome[0].GTotalAmount.ToString());
                    txttotaldiscounted.Text = Commonfunction.Getrounding(objIncome[0].GTotalDiscountAmount.ToString());
                    txttotalpaid.Text = Commonfunction.Getrounding(objIncome[0].GTotalPaidAmount.ToString());
                    txt_totaldue.Text = Commonfunction.Getrounding(objIncome[0].GTotalDueAmount.ToString());
                    txttotalincome.Text = Commonfunction.Getrounding(objIncome[0].TotalIncome.ToString());
                    txttotalexp.Text = Commonfunction.Getrounding(objIncome[0].TotalExpenditure.ToString());
                    txtbalance.Text = Commonfunction.Getrounding(objIncome[0].TotalIncomeBalance.ToString());
                   
                }
                else
                {
                    gvdepositlist.DataSource = null;
                    gvdepositlist.DataBind();
                    gvdepositlist.Visible = true;
                    txttotalbillamount.Text = "0.00";
                    txt_totaldue.Text = "0.00";
                    txttotaldiscounted.Text = "0.00";
                    txttotalpaid.Text = "0.00";
                    lblresult.Visible = false;
                    txttotalincome.Text = "0.00";
                   
                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage2, "system", 0);
                divmsg2.Attributes["class"] = "FailAlert";
                divmsg2.Visible = true;
            }
        }
        public List<LabIncomeCollectionData> GetLabIncomeCollectionList(int curIndex)
        {
            LabIncomeCollectionData objlabbill = new LabIncomeCollectionData();
            LabIncomeCollectionBO objbillingBO = new LabIncomeCollectionBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime from = txtdatefrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txtdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime to = txtto.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objlabbill.TransactionTypeID = Convert.ToInt32(ddl_transactionType.SelectedValue == "" ? "0" : ddl_transactionType.SelectedValue);
            objlabbill.CollectedByID = Convert.ToInt64(ddlcollectedby.SelectedValue == "" ? "0" : ddlcollectedby.SelectedValue);             
            objlabbill.DateFrom = from;
            objlabbill.DateTo = to;
            objlabbill.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
            objlabbill.CurrentIndex = curIndex;
            objlabbill.AmountEnable = LogData.AmountEnable;
            return objbillingBO.GetLabIncomeList(objlabbill);
        }
        //------------Expenditure -----------------------//
        protected void bindExpgrid(int page)
        {
            try
            {     
                List<LabIncomeCollectionData> objexp = GetLabExpenditureList(page);
                if (objexp.Count > 0)
                {
                    if (LogData.PrintEnable == 0)
                    {
                        btnprints.Attributes["disabled"] = "disabled";
                    }
                    else
                    {
                        btnprints.Attributes.Remove("disabled");
                    }
                    GVExpditure.VirtualItemCount = objexp[0].MaximumRows;//total item is required for custom paging
                    GVExpditure.PageIndex = page - 1;

                    GVExpditure.DataSource = objexp;
                    GVExpditure.DataBind();
                    GVExpditure.Visible = true;
                    Messagealert_.ShowMessage(lbl_result, "Total:" + objexp[0].MaximumRows.ToString() + " Record(s) found.", 1);
                    divmsg1.Attributes["class"] = "SucessAlert";
                    divmsg1.Visible = true;
                    txttotalexp.Text = Commonfunction.Getrounding(objexp[0].TotalExpenditure.ToString());
                }
                else
                {
                    GVExpditure.DataSource = null;
                    GVExpditure.DataBind();
                    GVExpditure.Visible = true;
                    lblresult.Visible = false;
                    
                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage2, "system", 0);
                divmsg2.Attributes["class"] = "FailAlert";
                divmsg2.Visible = true;
            }
        }
        public List<LabIncomeCollectionData> GetLabExpenditureList(int curIndex)
        {
            LabIncomeCollectionData objlabbill = new LabIncomeCollectionData();
            LabIncomeCollectionBO objbillingBO = new LabIncomeCollectionBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime from = txtdatefrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txtdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime to = txtto.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objlabbill.Paymode = Convert.ToInt32(ddl_transactionType.SelectedValue == "" ? "0" : ddl_transactionType.SelectedValue);
            objlabbill.CollectedByID = Convert.ToInt64(ddlcollectedby.SelectedValue == "" ? "0" : ddlcollectedby.SelectedValue);
            objlabbill.DateFrom = from;
            objlabbill.DateTo = to;
            objlabbill.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
            objlabbill.CurrentIndex = curIndex;
            objlabbill.AmountEnable = LogData.AmountEnable;
            return objbillingBO.GetLabExpenditureList(objlabbill);
        }

        //----------
        protected void btnresets_Click(object sender, EventArgs e)
        {
            txtdatefrom.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            txtto.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            ddlstatus.SelectedIndex = 0;              
            lblresult.Visible = false;
            lblmessage2.Visible = false;
            lblresult.Visible = false;
            divmsg2.Visible = false;
            //divmsg3.Visible = false;
            txttotalbillamount.Text = "0.00";
            txt_totaldue.Text = "0.00";
            txttotaldiscounted.Text = "0.00";
            txttotalpaid.Text = "0.00";
            txttotalbillamount.Text = "0.00";
            txttotaldiscounted.Text = "0.00";
            txttotalpaid.Text = "0.00";
            btnprints.Attributes["disabled"] = "disabled";
            txttotalincome.Text = "0.00";
            txttotalexp.Text = "0.00";
            txtbalance.Text = "0.00";

            gvdepositlist.DataSource = null;
            gvdepositlist.DataBind();
            gvdepositlist.Visible = false;

            GVExpditure.DataSource = null;
            GVExpditure.DataBind();
            GVExpditure.Visible = true;
        }
    }
}
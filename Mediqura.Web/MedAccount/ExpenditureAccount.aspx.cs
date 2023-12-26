using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedAccount;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.LoginData;
using Mediqura.CommonData.MedAccount;
using Mediqura.DAL.MedAccount;
using Mediqura.Utility.ExceptionHandler;
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
    public partial class ExpenditureAccount : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {       
               
                txt_voucher.ReadOnly = true;
                MasterLookupBO mstlookup = new MasterLookupBO();
                Commonfunction.PopulateDdl(ddl_transactionType, mstlookup.GetLookupsList(LookupName.PHRPaymentType));
                Commonfunction.PopulateDdl(ddl_transaction, mstlookup.GetLookupsList(LookupName.PHRPaymentType));                
                txt_TransactionDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                btnprints.Attributes["disabled"] = "disabled";          
                txtdatefrom.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                txtto.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
             
            }
        }
        protected void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
                LabAcntTranData objdata = new LabAcntTranData();
                LabAcntTranBO objstdBO = new LabAcntTranBO();
                if (ddl_transactionType.SelectedIndex == 0)
                {  
                    Messagealert_.ShowMessage(lblmessage, "PaymentType", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    ddl_transactionType.Focus();
                    return; 
                }

                if (Convert.ToDecimal(txt_Amount.Text.Trim() == "" ? "0" : txt_Amount.Text.Trim()) <= 0)
                {  
                    Messagealert_.ShowMessage(lblmessage, "validAmount", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    txt_Amount.Focus();
                    return; 
                }
               
                if (txt_naration.Text.Trim() == "")
                { 
                    Messagealert_.ShowMessage(lblmessage, "AccountNarration", 0);
                    div1.Visible = true;
                    div1.Attributes["class"] = "FailAlert";
                    txt_naration.Focus();
                    return;  
                }
                objdata.TransactionTypeID = Convert.ToInt32(ddl_transactionType.SelectedValue == "" ? "0" : ddl_transactionType.SelectedValue);
                objdata.TransactionAmount = Convert.ToDecimal(txt_Amount.Text == "" ? "0" : txt_Amount.Text.ToString().Trim());
                objdata.TransactionDate = Convert.ToDateTime(txt_TransactionDate.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txt_TransactionDate.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault));
                objdata.TransactionNaration = txt_naration.Text == "" ? null : txt_naration.Text.ToString().Trim();
               
                objdata.EmployeeID = LogData.EmployeeID;
                objdata.HospitalID = LogData.HospitalID;
                objdata.FinancialYearID = LogData.FinancialYearID;

                List<LabAcntTranData> OutputList = new List<LabAcntTranData>();

                OutputList = objstdBO.UpdateLabAccountTransaction(objdata);

                if (OutputList.Count() > 0)
                {
                    btnprints.Attributes.Remove("disabled");
                    txt_voucher.Text = OutputList[0].VoucherNo.ToString();
                    Messagealert_.ShowMessage(lblmessage, "save", 1);
                    div1.Visible = true;
                    div1.Attributes["class"] = "SucessAlert";
                    btnsave.Attributes["disabled"] = "disabled";

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

            }

        }
        protected void btnResrt_Click(object sender, EventArgs e)
        {
            reset();
        }
        public void reset()
        {
            btnsave.Attributes.Remove("disabled");
            txt_TransactionDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            txt_voucher.Text = "";
            txt_Amount.Text = "";
            txt_naration.Text = "";
            ddl_transactionType.SelectedIndex = 0;
          
            btnprints.Attributes["disabled"] = "disabled";

            Gv_incomereport.DataSource = null;
            Gv_incomereport.DataBind();
            Gv_incomereport.Visible = false;
            GV_expensesreport.Visible = false;
            lblgridincome.Visible = false;
            lblresult.Visible = false;
            txt_TotalIncome.Text = "";
            lbldescription.Text = "";
            txtdatefrom.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            txtto.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            ddl_transaction.SelectedIndex = 0;
            divmsg3.Visible = false;
            div1.Visible = false;

            lblgridexpenses.Visible = false;
        }

        //-----------END OF TAB 1 -----------------//

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            transactiontype();
        }

        private void IncomeBindGrid(int page)
        {
            try
            {

                List<LabAcntTranData> objincome = GetIncomeTransactionList(page);
                if (objincome.Count > 0)
                {
                    Gv_incomereport.DataSource = objincome;
                    Gv_incomereport.DataBind();
                    Gv_incomereport.VirtualItemCount = objincome.Count;
                    Gv_incomereport.PageIndex = page - 1;
                    Gv_incomereport.Visible = true;
                    GV_expensesreport.Visible = false;
                    lblgridincome.Visible = true;
                    txt_TotalIncome.Text = Commonfunction.Getrounding(objincome[0].TotalIncome.ToString("N2"));
                   
                }
                else
                {
                    Gv_incomereport.DataSource = null;
                    Gv_incomereport.DataBind();
                    Gv_incomereport.Visible = false;
                    GV_expensesreport.Visible = false;
                    lblgridincome.Visible = false;
                    lblresult.Visible = false;
                    txt_TotalIncome.Text = "";
                    lbldescription.Text = "";
                }
            }

            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
            }
        }
        public List<LabAcntTranData> GetIncomeTransactionList(int curIndex)
        {
            LabAcntTranData ObjData = new LabAcntTranData();
            LabAcntTranBO objgBO = new LabAcntTranBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime from = txtdatefrom.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txtto.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
           
            ObjData.TransactionTypeID = Convert.ToInt32(ddl_transaction.SelectedValue == "" ? "0" : ddl_transaction.SelectedValue);
            ObjData.AccountStatusID = Convert.ToInt32(ddl_account_close.SelectedValue == "" ? "0" : ddl_account_close.SelectedValue);
         
            ObjData.FromDate = from;
            ObjData.ToDate = To;
            ObjData.EmployeeID = LogData.EmployeeID;
            return objgBO.GetIncomeTransactionList(ObjData);


        }

        private void ExpensesBindGrid(int page)
        {
            try
            {

                List<LabAcntTranData> objexpenses = GetExpensesTransactionList(page);
                if (objexpenses.Count > 0)
                {
                    GV_expensesreport.DataSource = objexpenses;
                    GV_expensesreport.DataBind();
                    GV_expensesreport.VirtualItemCount = objexpenses.Count;
                    GV_expensesreport.PageIndex = page - 1;
                    Gv_incomereport.Visible = false;
                    GV_expensesreport.Visible = true;
                    lblgridexpenses.Visible = true;
                    txt_TotalExpenses.Text = Commonfunction.Getrounding(objexpenses[0].TotalExpenditure.ToString("N2"));

                    
                }
                else
                {
                    GV_expensesreport.DataSource = null;
                    GV_expensesreport.DataBind();
                    GV_expensesreport.Visible = false;
                    Gv_incomereport.Visible = false;
                    lblgridexpenses.Visible = false;
                    lblresult.Visible = false;
                    txt_TotalExpenses.Text = "";
                    lbldescription.Text = "";
                }
            }


            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
            }
        }
        public List<LabAcntTranData> GetExpensesTransactionList(int curIndex)
        {
            LabAcntTranData ObjData = new LabAcntTranData();
            LabAcntTranBO objgBO = new LabAcntTranBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime from = txtdatefrom.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txtto.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);

            ObjData.TransactionTypeID = Convert.ToInt32(ddl_transaction.SelectedValue == "" ? "0" : ddl_transaction.SelectedValue);
            ObjData.AccountStatusID = Convert.ToInt32(ddl_account_close.SelectedValue == "" ? "0" : ddl_account_close.SelectedValue);

            ObjData.FromDate = from;
            ObjData.ToDate = To;
            ObjData.EmployeeID = LogData.EmployeeID;

            return objgBO.GetExpensesTransactionList(ObjData);
        }
        protected void Gv_incomereport_RowCommand(object sender, GridViewCommandEventArgs e) 
        {
            try
            {
                if (e.CommandName == "Delete")
                {
                    if (LogData.DeleteEnable == 0)
                    {
                        Messagealert_.ShowMessage(lblmessage2, "DeleteEnable", 0);
                        div3.Visible = true;
                        div3.Attributes["class"] = "FailAlert";
                        return;
                    }
                    else
                    {
                        lblmessage2.Visible = false;
                    }
                    LabAcntTranData objData = new LabAcntTranData();
                    LabAcntTranDA objBO = new LabAcntTranDA();
                    int j = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gv = GV_expensesreport.Rows[j];
                    Label voucher = (Label)gv.Cells[0].FindControl("lblincomeVoucher");
                    TextBox remark = (TextBox)gv.Cells[0].FindControl("txtremarks");
                    objData.VoucherNo = voucher.Text.Trim() == "" ? "" : voucher.Text.Trim();
                    objData.EmployeeID = LogData.EmployeeID;
                    objData.Remarks = remark.Text.Trim() == "" ? "" : remark.Text.Trim();
                    int result = objBO.DeleteTransactionByVoucherNo(objData);
                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblresult, "system", 0);
                div3.Attributes["class"] = "FailAlert";
                div3.Visible = true;
                return;
            }
        }


        private void transactiontype()
        {
            if (LogData.SearchEnable == 0)
            {
                Messagealert_.ShowMessage(lblmessage2, "SearchEnable", 0);
                div3.Visible = true;
                div3.Attributes["class"] = "FailAlert";
                return;
            }
            else
            {
                lblmessage2.Visible = false;
            }

            if (ddl_transaction.SelectedValue == "1")
            {
                IncomeBindGrid(1);
                GV_expensesreport.Visible = false;
                Gv_incomereport.Visible = true;
                lblgridexpenses.Visible = false;
                lblgridincome.Visible = true;
                totalincome.Visible = true;
                totalexpenses.Visible = false;

            }
            else if (ddl_transaction.SelectedValue == "2")
            {
                ExpensesBindGrid(1);
                Gv_incomereport.Visible = false;
                GV_expensesreport.Visible = true;
                lblgridexpenses.Visible = true;
                lblgridincome.Visible = false;
                totalincome.Visible = false;
                totalexpenses.Visible = true;
            }
            else
            {
                IncomeBindGrid(1);
                ExpensesBindGrid(1);
                GV_expensesreport.Visible = true;
                Gv_incomereport.Visible = true;
                lblgridexpenses.Visible = true;
                lblgridincome.Visible = true;
                totalincome.Visible = true;
                totalexpenses.Visible = true;
            }
        }

        protected void GV_expensesreport_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Deletes")
                {
                    if (LogData.DeleteEnable == 0)
                    {
                        Messagealert_.ShowMessage(lblmessage2, "DeleteEnable", 0);
                        div3.Visible = true;
                        div3.Attributes["class"] = "FailAlert";
                        return;
                    }
                    else
                    {
                        lblmessage2.Visible = false;
                    }

                    LabAcntTranData objData = new LabAcntTranData();
                    LabAcntTranDA objBO = new LabAcntTranDA();
                    int j = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gv = GV_expensesreport.Rows[j];
                    Label voucher = (Label)gv.Cells[0].FindControl("lblexpensesVoucher");
                    TextBox remark = (TextBox)gv.Cells[0].FindControl("txtremarks");

                    if (remark.Text == "")
                    {
                        Messagealert_.ShowMessage(lblresult, "Remarks", 0);
                        divmsg3.Attributes["class"] = "FailAlert";
                        divmsg3.Visible = true;
                        remark.Focus();
                        return;
                    }
                    else
                    {
                        objData.Remarks = remark.Text.Trim() == "" ? "" : remark.Text.Trim();
                    }

                    objData.VoucherNo = voucher.Text.Trim() == "" ? "" : voucher.Text.Trim();
                    objData.EmployeeID = LogData.EmployeeID;
                 

                    int result = objBO.DeleteTransactionByVoucherNo(objData);

                    if (result == 1)
                    {
                        transactiontype();
                        divmsg3.Visible = false;
                    }
                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblresult, "system", 0);
                div3.Attributes["class"] = "FailAlert";
                div3.Visible = true;
                return;
            }
        }

        protected void Gv_incomereport_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void Gv_incomereport_RowCommand1(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Deletes")
                {
                    if (LogData.DeleteEnable == 0)
                    {
                        Messagealert_.ShowMessage(lblmessage2, "DeleteEnable", 0);
                        div3.Visible = true;
                        div3.Attributes["class"] = "FailAlert";
                        return;
                    }
                    else
                    {
                        lblmessage2.Visible = false;
                    }
                    LabAcntTranData objData = new LabAcntTranData();
                    LabAcntTranDA objBO = new LabAcntTranDA();
                    int j = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gv = Gv_incomereport.Rows[j];
                    Label voucher = (Label)gv.Cells[0].FindControl("lblincomeVoucher");
                    TextBox remark = (TextBox)gv.Cells[0].FindControl("txtremarks");
                    objData.VoucherNo = voucher.Text.Trim() == "" ? "" : voucher.Text.Trim();
                    objData.EmployeeID = LogData.EmployeeID;

                    if (remark.Text == "")
                    {
                        Messagealert_.ShowMessage(lblresult, "Remarks", 0);
                        divmsg3.Attributes["class"] = "FailAlert";
                        divmsg3.Visible = true;
                        remark.Focus();
                        return;
                    }
                    else
                    {
                        objData.Remarks = remark.Text.Trim() == "" ? "" : remark.Text.Trim();
                    }
      
                    int result = objBO.DeleteTransactionByVoucherNo(objData);

                    if (result == 1)
                    {
                        transactiontype();
                        divmsg3.Visible = false;
                    }
                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblresult, "system", 0);
                div3.Attributes["class"] = "FailAlert";
                div3.Visible = true;
                return;
            }
        }

        protected void GV_expensesreport_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void Gv_incomereport_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void btnprints_Click(object sender, EventArgs e)
        {

        }
    }
}
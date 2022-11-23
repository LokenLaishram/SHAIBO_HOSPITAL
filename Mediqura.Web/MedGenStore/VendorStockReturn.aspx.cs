using Mediqura.BOL.MedStore;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.LoginData;
using Mediqura.CommonData.MedStore;
using Mediqura.Utility;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using Mediqura.Web.MedCommon;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mediqura.Web.MedGenStore
{
    public partial class VendorStockReturn : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetAutoRecieptNo(string prefixText, int count, string contextKey)
        {
            VendorStockReturnData ObjVReturn = new VendorStockReturnData();
            VendorStockReturnBO objVenReturnBO = new VendorStockReturnBO();
            List<VendorStockReturnData> getResult = new List<VendorStockReturnData>();
            ObjVReturn.ReceiptNo = prefixText.Trim();
            getResult = objVenReturnBO.GetAutoRecieptNo(ObjVReturn);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].ReceiptNo.ToString());
            }
            return list;
        }
        protected void txt_recieptno_TextChanged(object sender, EventArgs e)
        {
            bindgrid();
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
                    Messagealert_.ShowMessage(lblmessage, "SearchEnable", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txt_recieptno.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "ReceiptBlank", 0);
                    divmsg1.Attributes["class"] = "FailAlert";
                    divmsg1.Visible = true;
                    txt_recieptno.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
               
                List<VendorStockReturnData> objreturn = GetReturnStockList(0);
                if (objreturn.Count > 0)
                {
                    gvvendorreturn.DataSource = objreturn;
                    gvvendorreturn.DataBind();
                    gvvendorreturn.Visible = true;                 
                    divmsg1.Attributes["class"] = "SucessAlert";
                    divmsg1.Visible = true;
                    lblmessage.Visible = false;                    
                }
                else
                {
                    gvvendorreturn.DataSource = null;
                    gvvendorreturn.DataBind();
                    gvvendorreturn.Visible = true;                   
                    divmsg1.Visible = false;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
                divmsg1.Attributes["class"] = "FailAlert";
                divmsg1.Visible = true;
            }
        }
        public List<VendorStockReturnData> GetReturnStockList(int curIndex)
        {
            VendorStockReturnData objreturnstock = new VendorStockReturnData();
            VendorStockReturnBO objBO = new VendorStockReturnBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            objreturnstock.ReceiptNo = txt_recieptno.Text.Trim().ToString() == "" ? "" : txt_recieptno.Text.Trim().ToString();
            DateTime RecievedDate = txt_recieveddate.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txt_recieveddate.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objreturnstock.RecievedDate = RecievedDate;
            return objBO.GetReturnStockList(objreturnstock);
        }
        protected void Return_TextChanged(object sender, EventArgs e)
        {
            totalcal();
        }
        private void totalcal()
        {
            int TotalReturnQty = 0;
            double ReturnAmount = 0;
            double TotalReturnAmount = 0;
          
            foreach (GridViewRow gvr in gvvendorreturn.Rows)
            {
                Label cpperqty = (Label)(gvr.FindControl("lbl_cpperqty"));
                Label lblavailableqty = (Label)(gvr.FindControl("lbl_availableqty"));
                TextBox returnqty = (TextBox)(gvr.FindControl("txt_returnqty"));
                Label lblreturnamount = (Label)(gvr.FindControl("lbl_returnamount"));
                if (cpperqty.Text == string.Empty || cpperqty.Text == "")
                { cpperqty.Text = "0"; }
                if (returnqty.Text == string.Empty || returnqty.Text == "")
                { returnqty.Text = "0"; }
                if (Convert.ToInt32(returnqty.Text) > Convert.ToInt32(lblavailableqty.Text))
                {
                    Messagealert_.ShowMessage(lblmessage, "NotGreater", 0);
                    divmsg1.Attributes["class"] = "FailAlert";
                    divmsg1.Visible = true;
                    returnqty.Focus();
                    returnqty.ForeColor = System.Drawing.Color.Red;
                    ReturnAmount = 0.00;
                    lblreturnamount.Text = "0.00";
                    return;
                }
                else
                {
                    divmsg1.Visible = false;
                    lblmessage.Visible = false;
                }
                ReturnAmount = Convert.ToDouble(cpperqty.Text) * Convert.ToDouble(returnqty.Text);
                lblreturnamount.Text = ReturnAmount.ToString();
                TotalReturnQty = TotalReturnQty + Convert.ToInt32(returnqty.Text);
                TotalReturnAmount = TotalReturnAmount + ReturnAmount;
                txt_totalreturnqty.Text = TotalReturnQty.ToString();
                txttotalreturnamount.Text = TotalReturnAmount.ToString();
               
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
            if (txt_recieptno.Text == "")
            {
                Messagealert_.ShowMessage(lblmessage, "InterTransferTo", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                txt_recieptno.Focus();
                return;
            }
            List<VendorStockReturnData> ReturnStockList = new List<VendorStockReturnData>();
            VendorStockReturnData objReturnStock = new VendorStockReturnData();
            VendorStockReturnBO objBO = new VendorStockReturnBO();

            try
            {
                // get all the record from the gridview
                int itemcount = 0;
                int CheckReturnQty = 0;
                foreach (GridViewRow row in gvvendorreturn.Rows)
                {
                    Label ID = (Label)gvvendorreturn.Rows[row.RowIndex].Cells[0].FindControl("lblID");
                    Label ReceiptNo = (Label)gvvendorreturn.Rows[row.RowIndex].Cells[0].FindControl("lblreceiptNo");            
                    Label StockNumber = (Label)gvvendorreturn.Rows[row.RowIndex].Cells[0].FindControl("lbl_stockNo");
                    Label ReturnItemID = (Label)gvvendorreturn.Rows[row.RowIndex].Cells[0].FindControl("lbl_ItemID");
                    Label Recievedqty = (Label)gvvendorreturn.Rows[row.RowIndex].Cells[0].FindControl("lbl_recievedqty");
                    Label Availableqty = (Label)gvvendorreturn.Rows[row.RowIndex].Cells[0].FindControl("lbl_availableqty");
                    Label CPPerQty = (Label)gvvendorreturn.Rows[row.RowIndex].Cells[0].FindControl("lbl_cpperqty");                   
                    TextBox Returnqty = (TextBox)gvvendorreturn.Rows[row.RowIndex].Cells[0].FindControl("txt_returnqty");
                    Label Returnamount = (Label)gvvendorreturn.Rows[row.RowIndex].Cells[0].FindControl("lbl_returnamount");              
                    VendorStockReturnData ObjDetails = new VendorStockReturnData();
                    ObjDetails.ID = Convert.ToInt64(ID.Text.Trim());
                    ObjDetails.ReceiptNo = ReceiptNo.Text.Trim() == "" ? "0" : ReceiptNo.Text.Trim();
                    ObjDetails.StockNo = StockNumber.Text.Trim() == "" ? "0" : StockNumber.Text.Trim();
                    ObjDetails.ItemID = Convert.ToInt32(ReturnItemID.Text == "" ? "0" : ReturnItemID.Text);
                    ObjDetails.TotalRecievedQty = Convert.ToInt32(Recievedqty.Text == "" ? "0" : Recievedqty.Text);
                    ObjDetails.CPPerQty = Convert.ToDecimal(CPPerQty.Text == "" ? "0" : CPPerQty.Text);                   
                    ObjDetails.ReturnQty = Convert.ToInt32(Returnqty.Text == "" ? "0" : Returnqty.Text);
                    ObjDetails.ReturnAmount = Convert.ToDecimal(Returnamount.Text == "" ? "0" : Returnamount.Text);
                    if (Convert.ToInt32(Availableqty.Text) >= Convert.ToInt32(Returnqty.Text))
                    {
                        Returnqty.Focus();
                        CheckReturnQty = CheckReturnQty + 1;
                    }
                    itemcount = itemcount + 1;
                    ReturnStockList.Add(ObjDetails);
                }
                if (CheckReturnQty > 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "NotGreater", 0);
                    divmsg1.Attributes["class"] = "FailAlert";
                    divmsg1.Visible = true;
                    return;
                }
                objReturnStock.XMLData = XmlConvertor.ReturnStockDatatoXML(ReturnStockList).ToString();
                if (itemcount == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "ItemCount", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                objReturnStock.ReceiptNo = txt_recieptno.Text.Trim() == "" ? "0" : txt_recieptno.Text.Trim();
                objReturnStock.GrandTotalReturnQty = Convert.ToInt32(txt_totalreturnqty.Text == "" ? "0" : txt_totalreturnqty.Text);
                objReturnStock.GrandTotalReturnAmount = Convert.ToDecimal(txttotalreturnamount.Text.Trim() == "" ? "0" : txttotalreturnamount.Text.Trim());               
                objReturnStock.HospitalID = LogData.HospitalID;
                objReturnStock.EmployeeID = LogData.EmployeeID;
                objReturnStock.FinancialYearID = LogData.FinancialYearID;
                objReturnStock.ActionType = Enumaction.Insert;
                List<VendorStockReturnData> result = objBO.UpdateVendorReturnStockDetails(objReturnStock);
                if (result.Count > 0)
                {
                    txt_vendorReturnNo.Text = result[0].VendorReturnNo.ToString();
                    Messagealert_.ShowMessage(lblmessage, "save", 1);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "SucessAlert";
                    btnsave.Attributes["disabled"] = "disabled";
                }
                else
                {
                    Messagealert_.ShowMessage(lblmessage, "system", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                string msg = ex.ToString();
                Messagealert_.ShowMessage(lblmessage, msg, 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
            }
        }

        protected void btnReset(object sender, EventArgs e)
        {
            gvvendorreturn.DataSource = null;
            gvvendorreturn.DataBind();
            gvvendorreturn.Visible = true;
            divmsg1.Visible = false;
            txt_recieptno.Text = "";
            txt_recieveddate.Text = "";
            txt_totalreturnqty.Text = "";
            txttotalreturnamount.Text = "";
        }

    }
}
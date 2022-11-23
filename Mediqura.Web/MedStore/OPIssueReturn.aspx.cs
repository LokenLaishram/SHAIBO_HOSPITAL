using ClosedXML.Excel;
using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedUtilityBO;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.MedUtilityData;
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
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using System.Text;
using System.Drawing;
using Mediqura.CommonData.MedStore;
using Mediqura.BOL.MedStore;
using Mediqura.Utility;
using Mediqura.CommonData.LoginData;
using Mediqura.CommonData.MedHrData;
using Mediqura.BOL.MedHrBO;

namespace Mediqura.Web.MedStore
{
    public partial class OPIssueReturn : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlbind();
            }
        }
        private void ddlbind()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddl_handover, mstlookup.GetLookupsList(LookupName.StockRecievedBy));
            ddl_handover.SelectedValue = LogData.EmployeeID.ToString();

            btnsave.Attributes["disabled"] = "disabled";
            btnprints.Attributes["disabled"] = "disabled";
            btnprint.Attributes["disabled"] = "disabled";
            txt_totalreturnqty.Text = "0";
            txt_totaldeducted.Text = "0.0";
            txt_returnamounts.Text = "0.0";
            lblmessage.Visible = false;
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetBillNo(string prefixText, int count, string contextKey)
        {
            OPReturnData Objpaic = new OPReturnData();
            OPReturnBO objInfoBO = new OPReturnBO();
            List<OPReturnData> getResult = new List<OPReturnData>();
            Objpaic.OPBillNo = prefixText;
            getResult = objInfoBO.GetAutoPhrOPBills(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].OPBillNo);
            }
            return list;
        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            if (txt_BillNo.Text == "")
            {
                Messagealert_.ShowMessage(lblmessage, "BillNo", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                txt_BillNo.Focus();
                return;
            }
            else
            {
                bindgrid();
            }
        }
        protected void bindgrid()
        {
            try
            {
                List<OPReturnData> objdeposit = GetOPIssueList(0);
                if (objdeposit.Count > 0)
                {
                    txt_billdate.Text = objdeposit[0].AddedDate.ToString("dd/MM/yyyy");
                    txt_custommer.Text = objdeposit[0].PatientName;
                    txt_totalbill.Text = Commonfunction.Getrounding(objdeposit[0].TotalBillAmount.ToString());
                    txt_totalpaid.Text = Commonfunction.Getrounding(objdeposit[0].PaidAmount.ToString());
                    gvopreturn1.DataSource = objdeposit;
                    gvopreturn1.DataBind();
                    gvopreturn1.Visible = true;
                    btnsave.Attributes.Remove("disabled");
                    Messagealert_.ShowMessage(lblresult, "Total:" + objdeposit[0].MaximumRows.ToString() + " Record(s) found.", 1);
                    divmsg3.Attributes["class"] = "SucessAlert";
                    divmsg3.Visible = true;
                    //txt_BillNo.ReadOnly = true;
                }
                else
                {
                    gvopreturn1.DataSource = null;
                    gvopreturn1.DataBind();
                    gvopreturn1.Visible = true;
                    lblresult.Visible = false;
                    divmsg3.Visible = false;
                    txt_billdate.Text = "";
                    txt_custommer.Text = "";
                    txt_address.Text = "";
                    txt_totalbill.Text = "";
                    txt_totalpaid.Text = "";
                    //txt_BillNo.ReadOnly = false;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
                divmsg3.Attributes["class"] = "FailAlert";
                divmsg3.Visible = true;
            }
        }
        public List<OPReturnData> GetOPIssueList(int curIndex)
        {
            OPReturnData objstock = new OPReturnData();
            OPReturnBO objBO = new OPReturnBO();
            objstock.OPBillNo = txt_BillNo.Text.ToString() == "" ? "0" : txt_BillNo.Text.Trim();
            return objBO.GetOPIssueListByBillNo(objstock);
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetEmployeeName(string prefixText, int count, string contextKey)
        {
            EmployeeData Objpaic = new EmployeeData();
            EmployeeBO objInfoBO = new EmployeeBO();
            List<EmployeeData> getResult = new List<EmployeeData>();
            Objpaic.EmpName = prefixText;
            getResult = objInfoBO.GetEmployeeName(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].EmpName);
            }
            return list;
        }
        protected void txtreturnqty_TextChanged(object sender, EventArgs e)
        {
            txt_returnqty.Text = "0";
            txt_totalamount.Text = "0";
            txt_deductedamount.Text = "0";
            txt_returnamount.Text = "0";

            decimal TotalQty = 0;
            decimal TotalAmount = 0;
            decimal GTotalAmount = 0;

            //int Lastindex = gvopreturn1.Rows.Count - 1;
            //TextBox txt = sender as TextBox;
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gvRow.RowIndex;
            foreach (GridViewRow row in gvopreturn1.Rows)
            {
                Label Qty = (Label)gvopreturn1.Rows[row.RowIndex].Cells[0].FindControl("lbl_qty");
                TextBox ReturnQty = (TextBox)gvopreturn1.Rows[row.RowIndex].Cells[0].FindControl("txt_return1");
                Label MRP = (Label)gvopreturn1.Rows[row.RowIndex].Cells[0].FindControl("lbl_charge");
                Label ItemDis = (Label)gvopreturn1.Rows[row.RowIndex].Cells[0].FindControl("lbl_discount");
                Label ReturnAmt = (Label)gvopreturn1.Rows[row.RowIndex].Cells[0].FindControl("lbl_returnAmt");
                if (Convert.ToInt32(ReturnQty.Text == "" ? "0" : ReturnQty.Text) > Convert.ToInt32(Qty.Text == "" ? "0" : Qty.Text))
                {
                    Messagealert_.ShowMessage(lblmessage, "ReturnQty", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    ReturnQty.Text = "";
                    ReturnQty.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                    decimal RQty = Convert.ToDecimal(ReturnQty.Text == "" ? "0" : ReturnQty.Text);
                    decimal MRPpQ = Convert.ToDecimal(MRP.Text == "" ? "0" : MRP.Text);
                    decimal DisPerItem = Convert.ToDecimal(ItemDis.Text == "" ? "0" : ItemDis.Text);
                    if (RQty > 0)
                    {
                        TotalQty = TotalQty + RQty;
                        TotalAmount = TotalAmount + ((MRPpQ * RQty) - DisPerItem);
                        GTotalAmount = TotalAmount ;
                        ReturnAmt.Text = ((MRPpQ * RQty) - DisPerItem).ToString();
                        ReturnQty.Focus();
                        //txt_returnqty.Text = Commonfunction.Getrounding((Convert.ToDecimal(txt_returnqty.Text == "" ? "0" : txt_returnqty.Text) + Convert.ToDecimal(ReturnQty.Text.ToString() == "" ? "0" : ReturnQty.Text.ToString())).ToString());
                        //txt_totalamount.Text = Commonfunction.Getrounding((Convert.ToDecimal(txt_totalamount.Text == "" ? "0" : txt_totalamount.Text) + (Convert.ToDecimal(MRP.Text == "" ? "0" : MRP.Text) * Convert.ToDecimal(ReturnQty.Text.ToString() == "" ? "0" : ReturnQty.Text.ToString())).ToString()));
                        //txt_returnamount.Text = Commonfunction.Getrounding((Convert.ToDecimal(txt_returnamount.Text == "" ? "0" : txt_returnamount.Text) + Convert.ToDecimal(MRP.Text == "" ? "0" : MRP.Text) * Convert.ToDecimal(ReturnQty.Text.ToString() == "" ? "0" : ReturnQty.Text.ToString())).ToString()); 

                    }
                    else
                    {
                        ReturnAmt.Text = "0";
                    }

                    //if (Lastindex > row.RowIndex)
                    //{
                    //    TextBox ReturnQty1 = (TextBox)gvopreturn1.Rows[row.RowIndex].Cells[0].FindControl("txt_return1");
                    //    ReturnQty1.Focus();
                    //}
                    //else if (Lastindex == row.RowIndex)
                    //{
                    //    TextBox ReturnQty2 = (TextBox)gvopreturn1.Rows[row.RowIndex].Cells[0].FindControl("txt_return1");
                    //    ReturnQty2.Focus();
                    //}

                }
            }
            txt_returnqty.Text = TotalQty.ToString();
            txt_totalamount.Text = GTotalAmount.ToString("N");
            txt_returnamount.Text = GTotalAmount.ToString("N");

            if (Convert.ToInt32(txt_returnqty.Text == "" ? "0" : txt_returnqty.Text) > 0)
            {
                btnsave.Attributes.Remove("disabled");
            }
            else
            {
                btnsave.Attributes["disabled"] = "disabled";
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
            if (txt_BillNo.Text == "" || txt_BillNo.Text == "0")
            {
                Messagealert_.ShowMessage(lblmessage, "Billno", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                txt_BillNo.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
                lblmessage.Visible = false;
            }
            if (txt_remarks.Text == "")
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
                lblmessage.Visible = false;
            }
            if (ddl_handover.SelectedIndex == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "ReceivedBy", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                ddl_handover.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
                lblmessage.Visible = false;
            }

            List<OPReturnData> ListStock = new List<OPReturnData>();
            OPReturnData objStock = new OPReturnData();
            OPReturnBO objBO = new OPReturnBO();
            try
            {
                int count = 0;
                // get all the record from the gridview
                foreach (GridViewRow row in gvopreturn1.Rows)
                {
                    IFormatProvider provider = new System.Globalization.CultureInfo("en-GB", true);
                    Label ID = (Label)gvopreturn1.Rows[row.RowIndex].Cells[0].FindControl("lbl_ID");
                    Label UHID = (Label)gvopreturn1.Rows[row.RowIndex].Cells[0].FindControl("lbl_UHID");
                    Label SubStockID = (Label)gvopreturn1.Rows[row.RowIndex].Cells[0].FindControl("lbl_substockID");
                    Label ItemID = (Label)gvopreturn1.Rows[row.RowIndex].Cells[0].FindControl("lbl_ItemID");
                    Label Quantity = (Label)gvopreturn1.Rows[row.RowIndex].Cells[0].FindControl("lbl_qty");
                    Label Charges = (Label)gvopreturn1.Rows[row.RowIndex].Cells[0].FindControl("lbl_charge");                      
                    Label Disc = (Label)gvopreturn1.Rows[row.RowIndex].Cells[0].FindControl("lbl_discount");
                    Label NetCharges = (Label)gvopreturn1.Rows[row.RowIndex].Cells[0].FindControl("lbl_netcharges");
                    TextBox txt_return = (TextBox)gvopreturn1.Rows[row.RowIndex].Cells[0].FindControl("txt_return1");
                    Label ReturnAmt = (Label)gvopreturn1.Rows[row.RowIndex].Cells[0].FindControl("lbl_returnAmt");
                                                                                                               
                    OPReturnData ObjDetails = new OPReturnData();

                    if (Convert.ToInt32(Quantity.Text == "" ? "0" : Quantity.Text) > 0)
                    {
                        count = count + 1;
                        ObjDetails.ID = Convert.ToInt64(ID.Text == "" ? "0" : ID.Text);
                        ObjDetails.UHID = Convert.ToInt64(UHID.Text == "" ? "0" : UHID.Text);
                        ObjDetails.SubStockID = Convert.ToInt64(SubStockID.Text == "" ? "0" : SubStockID.Text);
                        ObjDetails.ItemID = Convert.ToInt64(ItemID.Text == "" ? "0" : ItemID.Text);                         
                        ObjDetails.Charges = Convert.ToDecimal(Charges.Text == "" ? "0" : Charges.Text);
                        ObjDetails.Quantity = Convert.ToInt32(Quantity.Text == "" ? "0" : Quantity.Text);
                        ObjDetails.ItemWiseDiscount = Convert.ToDecimal(Disc.Text == "" ? "0" : Disc.Text);
                        ObjDetails.NetCharges = Convert.ToDecimal(NetCharges.Text == "" ? "0" : NetCharges.Text);
                        ObjDetails.Return = Convert.ToInt32(txt_return.Text == "" ? "0" : txt_return.Text);
                        ObjDetails.ReturnAmt = Convert.ToDecimal(ReturnAmt.Text == "" ? "0" : ReturnAmt.Text);
                        ListStock.Add(ObjDetails);
                    }
                }
                objStock.XMLData = XmlConvertor.OPReturnDetailsDatatoXML(ListStock).ToString();                  
                objStock.totalreturnQty = Convert.ToInt32(txt_returnqty.Text == "" ? "0" : txt_returnqty.Text);
                objStock.TotalReturnAmount = Convert.ToDecimal(txt_totalamount.Text == "" ? "0" : txt_totalamount.Text);
                objStock.TotalActualReturnAmount = Convert.ToDecimal(txt_returnamount.Text == "" ? "0" : txt_returnamount.Text);
               
                if (Convert.ToDecimal(txt_deductedamount.Text == "" ? "0" : txt_deductedamount.Text) > 100)
                {
                    Messagealert_.ShowMessage(lblmessage, "VaildPC", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    txt_deductedamount.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                    divmsg1.Visible = false;
                }
                if (count == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "CountReturn", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    txt_deductedamount.Focus();
                    return;
                }
                else
                {
                    divmsg1.Visible = false;
                    lblmessage.Visible = false;
                }
                objStock.DeductedPC = Convert.ToDecimal(txt_deductedamount.Text == "" ? "0" : txt_deductedamount.Text);
                objStock.HandOver = Convert.ToInt32(ddl_handover.SelectedValue == "" ? "0" : ddl_handover.SelectedValue);
                objStock.PatientName = txt_custommer.Text == "" ? "" : txt_custommer.Text;
                objStock.EmployeeID = LogData.EmployeeID;
                objStock.HospitalID = LogData.HospitalID;
                objStock.FinancialYearID = LogData.FinancialYearID;
                objStock.OPBillNo = txt_BillNo.Text == "" ? "" : txt_BillNo.Text;
                objStock.ActionType = Enumaction.Insert;
                string result = objBO.UpdateOPReturnDetails(objStock);
                if (result!=null)
                {
                    txt_returnNo.Text = result.ToString();
                    lblmessage.Visible = true;
                    Messagealert_.ShowMessage(lblmessage, "save", 1);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "SucessAlert";
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
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
                divmsg1.Attributes["class"] = "SuccessAlert";
                divmsg1.Visible = true;
            }
        }
        protected void gvopreturn1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label BQTY = (Label)e.Row.FindControl("lbl_qty");
                Label LRQTY = (Label)e.Row.FindControl("lbl_lastreturnQty");
                TextBox txtre = (TextBox)e.Row.FindControl("txt_return1");
                if (Convert.ToInt32(BQTY.Text == "" ? "0" : BQTY.Text) == Convert.ToInt32(LRQTY.Text == "" ? "0" : LRQTY.Text))
                {
                    txtre.Enabled = false;
                }
                else
                {
                    txtre.Enabled = true;
                }

            }
        }

        protected void chekboxselect_CheckedChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gvopreturn1.Rows)
            {
                CheckBox cb = (CheckBox)gvopreturn1.Rows[row.RowIndex].Cells[0].FindControl("chekboxselect"); //find the CheckBox
                if (cb.Checked)
                {
                    TextBox txt_return1 = (TextBox)gvopreturn1.Rows[row.RowIndex].Cells[0].FindControl("txt_return1"); //find the CheckBox
                    txt_return1.ReadOnly = false;
                    txt_return1.Focus();
                }
            }
        }
        protected void btnresets_Click(object sender, EventArgs e)
        {
            gvopreturn1.DataSource = null;
            gvopreturn1.DataBind();
            gvopreturn1.Visible = false;
            Session["StockOPList"] = null;
            lblmessage.Visible = false;
            txt_returnNo.Text = "";
            ddl_handover.SelectedIndex = 0;
            lblresult.Visible = false;
            divmsg3.Visible = false;
            txt_billdate.Text = "";
            txt_custommer.Text = "";
            txt_address.Text = "";
            txt_totalbill.Text = "";
            txt_totalpaid.Text = "";
            txt_BillNo.Text = "";
            txt_returnqty.Text = "0";
            txt_totalamount.Text = "0";
            txt_deductedamount.Text = "0";
            txt_returnamount.Text = "0";
            btnsave.Attributes["disabled"] = "disabled";
            btnprint.Attributes["disabled"] = "disabled";
            txt_BillNo.ReadOnly = false;
        }
        protected void txt_BillNo_Textchange(object sender, EventArgs e)
        {
            bindgrid();
        }
        protected void btnsearch1_Click(object sender, EventArgs e)
        {
            bindgrid1();
        }
        protected void bindgrid1()
        {
            try
            {
                if (txt_retdatefrom.Text != "")
                {
                    if (Commonfunction.isValidDate(txt_retdatefrom.Text) == false)
                    {
                        Messagealert_.ShowMessage(lblmessage1, "VaildDate", 0);
                        divmsg2.Attributes["class"] = "FailAlert";
                        divmsg2.Visible = true;
                        txt_retdatefrom.Focus();
                        return;
                    }
                }
                else
                {
                    divmsg2.Visible = false;
                }
                if (txt_returndateTo.Text != "")
                {
                    if (Commonfunction.isValidDate(txt_returndateTo.Text) == false)
                    {
                        Messagealert_.ShowMessage(lblmessage1, "VaildDate", 0);
                        divmsg2.Attributes["class"] = "FailAlert";
                        divmsg2.Visible = true;
                        txt_returndateTo.Focus();
                        return;
                    }
                }
                else
                {
                    divmsg2.Visible = false;
                }
                List<OPReturnData> objReturn = GetOPReturnList1(0);
                if (objReturn.Count > 0)
                {
                    gvopreturnlist.DataSource = objReturn;
                    gvopreturnlist.DataBind();
                    gvopreturnlist.Visible = true;
                    Messagealert_.ShowMessage(lblresult1, "Total:" + objReturn[0].MaximumRows.ToString() + " Record(s) found.", 1);
                    txt_totalreturnqty.Text = Commonfunction.Getrounding(objReturn[0].TotalReturnQty.ToString());
                    txttotalamount.Text = Commonfunction.Getrounding(objReturn[0].TotalReturnAmounts.ToString());
                    txt_totaldeducted.Text = Commonfunction.Getrounding(objReturn[0].TotalDeductedAmount.ToString());
                    txt_returnamounts.Text = Commonfunction.Getrounding(objReturn[0].SumTotalReturn.ToString());
                    div2.Attributes["class"] = "SucessAlert";
                    div2.Visible = true;
                    ddlexport.Visible = true;
                    btnexport.Visible = true;
                    lblmessage1.Visible = false;
                    lblmessage1.Visible = false;
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
                    txt_totalreturnqty.Text = "0";
                    txt_totaldeducted.Text = "0.0";
                    txt_returnamounts.Text = "0.0";
                    gvopreturnlist.DataSource = null;
                    gvopreturnlist.DataBind();
                    gvopreturnlist.Visible = true;
                    ddlexport.Visible = false;
                    btnexport.Visible = false;
                    lblresult.Visible = false;
                    divmsg3.Visible = false;
                    btnprints.Attributes["disabled"] = "disabled";
                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
            }
        }
        public List<OPReturnData> GetOPReturnList1(int curIndex)
        {
            OPReturnData objstock = new OPReturnData();
            OPReturnBO objBO = new OPReturnBO();
            objstock.ReturnNo = txt_returnNum.Text.ToString() == "" ? null : txt_returnNum.Text.ToString();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime from = txt_retdatefrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txt_retdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime to = txt_returndateTo.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txt_returndateTo.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objstock.DateFrom = from;
            objstock.DateTo = to;
            objstock.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
            return objBO.GetOPReturnList(objstock);
        }
        protected void txt_deducted_Textchange(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(txt_deductedamount.Text == "" ? "0.0" : txt_deductedamount.Text) > 0 && Convert.ToDecimal(txt_deductedamount.Text == "" ? "0.0" : txt_deductedamount.Text) < 101)
            {
                txt_returnamount.Text = Commonfunction.Getrounding((Convert.ToDecimal(txt_totalamount.Text == "" ? "0" : txt_totalamount.Text) - Convert.ToDecimal(txt_totalamount.Text == "" ? "0" : txt_totalamount.Text) * Convert.ToDecimal(txt_deductedamount.Text.ToString() == "" ? "0" : txt_deductedamount.Text.ToString()) / 100).ToString());
            }
            else
            {
                txt_returnamount.Text = Commonfunction.Getrounding((Convert.ToDecimal(txt_totalamount.Text == "" ? "0" : txt_totalamount.Text).ToString()));
            }
        }
        protected void gvopreturnlist_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Deletes")
                {
                    OPReturnData objbill = new OPReturnData();
                    OPReturnBO objstdBO = new OPReturnBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = gvopreturnlist.Rows[i];
                    Label ReturnNo = (Label)gr.Cells[0].FindControl("lbl_returnno");
                    TextBox txtremarks = (TextBox)gr.Cells[0].FindControl("txtremarks");
                    txtremarks.Enabled = true;
                    if (txtremarks.Text == "")
                    {
                        Messagealert_.ShowMessage(lblresult1, "Remarks", 0);
                        div2.Attributes["class"] = "FailAlert";
                        div2.Visible = true;
                        txtremarks.Focus();
                        return;
                    }
                    else
                    {
                        objbill.Remarks = txtremarks.Text;
                    }
                    objbill.EmployeeID = LogData.UserLoginId;
                    objbill.ReturnNo = ReturnNo.Text.Trim();
                    int Result = objstdBO.DeleteOPReturnItemListByID(objbill);
                    if (Result == 1)
                    {
                        Messagealert_.ShowMessage(lblmessage1, "delete", 1);
                        div2.Attributes["class"] = "SucessAlert";
                        div2.Visible = true;
                        bindgrid1();
                    }
                    else
                    {
                        Messagealert_.ShowMessage(lblmessage1, "system", 0);
                        div2.Attributes["class"] = "FailAlert";
                        div2.Visible = true;
                    }

                }

            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage1, "system", 0);
                div2.Attributes["class"] = "FailAlert";
                div2.Visible = true;
            }
        }
        protected void gvopreturnlist_RowDataBound(object sender, GridViewRowEventArgs e)
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
                    gvopreturnlist.Columns[10].Visible = false;
                }
                else
                {
                    gvopreturnlist.Columns[10].Visible = true;
                }
            }
        }
        protected DataTable GetDatafromDatabase()
        {
            List<OPReturnData> DepositDetails = GetOPReturnList1(0);
            List<OPReturnDatatoExcel> ListexcelData = new List<OPReturnDatatoExcel>();
            int i = 0;
            foreach (OPReturnData row in DepositDetails)
            {
                OPReturnDatatoExcel Ecxeclpat = new OPReturnDatatoExcel();
                Ecxeclpat.ReturnNo = DepositDetails[i].ReturnNo;
                Ecxeclpat.ReturnBy = DepositDetails[i].ReturnBy;
                Ecxeclpat.TotalReturnQty = DepositDetails[i].TotalReturnQty;
                Ecxeclpat.TotalReturnAmount = DepositDetails[i].TotalReturnAmount;
                Ecxeclpat.DeductedAmount = DepositDetails[i].DeductedAmount;
                Ecxeclpat.TotalActualReturnAmount = DepositDetails[i].TotalActualReturnAmount;
                Ecxeclpat.ReturnDate = DepositDetails[i].ReturnDate;
                ListexcelData.Add(Ecxeclpat);
                i++;
            }
            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            DataTable dt = converter.ToDataTable(ListexcelData);
            return dt;
        }
        protected void btnreset_Click(object sender, EventArgs e)
        {
            gvopreturnlist.DataSource = null;
            gvopreturnlist.DataBind();
            gvopreturnlist.Visible = false;
            lblmessage.Visible = false;
            txt_returnNum.Text = "";
            txt_retdatefrom.Text = "";
            txt_returndateTo.Text = "";
            div2.Visible = false;
            div2.Attributes["class"] = "Blank";
            btnprints.Attributes["disabled"] = "disabled";
            btnexport.Visible = false;
            ddlexport.Visible = false;
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
                Messagealert_.ShowMessage(lblmessage1, "ExportEnable", 0);
                divmsg2.Visible = true;
                divmsg2.Attributes["class"] = "FailAlert";
                return;
            }
            else
            {
                lblmessage1.Visible = false;
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
                    gvopreturnlist.BorderStyle = BorderStyle.None;
                    //Hide the Column containing CheckBox
                    gvopreturnlist.Columns[4].Visible = false;
                    gvopreturnlist.Columns[5].Visible = false;

                    gvopreturnlist.RenderControl(hw);
                    gvopreturnlist.HeaderRow.Style.Add("width", "15%");
                    gvopreturnlist.HeaderRow.Style.Add("font-size", "10px");
                    gvopreturnlist.Style.Add("text-decoration", "none");
                    gvopreturnlist.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    gvopreturnlist.Style.Add("font-size", "8px");
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 7f, 7f, 7f, 0f);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=StockReturnDetails.pdf");
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
                wb.Worksheets.Add(dt, "Stock Details");
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=StockReturnDetails.xlsx");
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
        protected void gvopreturnlist_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvopreturnlist.PageIndex = e.NewPageIndex;
            bindgrid1();
        }
    }
}
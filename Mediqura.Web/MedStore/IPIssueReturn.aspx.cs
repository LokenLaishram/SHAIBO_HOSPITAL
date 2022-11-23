using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedStore;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.MedStore;
using Mediqura.Utility;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using Mediqura.Web.MedCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mediqura.Web.MedStore
{
    public partial class IPIssueReturn : BasePage
    {

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                Session["IPReturnItemList"] = null;
                txt_returnitem.Attributes["disabled"] = "disabled";
                btnadd.Attributes["disabled"] = "disabled";
                btnprint.Attributes["disabled"] = "disabled";
				txtreturnqty.Attributes["disabled"] = "disabled";
				btnsave.Attributes["disabled"] = "disabled";
                if (Session["SaleReturns"] != null)
                {
                    txt_IPNo.Text = Session["SaleReturns"].ToString();
                    getpatientdtails();
                    txt_IPNo.ReadOnly = true;
                    Session["SaleReturns"] = null;
                }
            }
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetadvanceSearchIPNo(string prefixText, int count, string contextKey)
        {
            IPReturnData ObjData = new IPReturnData();
            IPReturnBO ObjBO = new IPReturnBO();
            List<IPReturnData> getResult = new List<IPReturnData>();
            ObjData.PatientDetails = prefixText;
            getResult = ObjBO.GetadvanceSearchIPNo(ObjData);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].PatientDetailsList.ToString());
            }
            return list;
        }

        protected void txt_IPNo_Textchange(object sender, EventArgs e)
        {
            getpatientdtails();
        }
        private void getpatientdtails()
        {
			IPReturnData ObjData = new IPReturnData();
			IPReturnBO ObjBO = new IPReturnBO();
            var source1 = txt_IPNo.Text.ToString();
            if (source1.Contains(":"))
            {
				ObjData.IPNo = source1.Substring(source1.LastIndexOf(':') + 1).Trim();
				int dischargestatus = ObjBO.CheckdischargestatusByIPNO(ObjData);
				if (dischargestatus == 1)
				{
					Messagealert_.ShowMessage(lblmessage, "PatientDischarge", 0);
					divmsg1.Visible = true;
					divmsg1.Attributes["class"] = "FailAlert";
					txt_IPNo.Text = "";
					txt_IPNo.Focus();
					return;
				}
				else
				{
					lblmessage.Text = "";
					divmsg1.Visible = false;
					txt_returnitem.Attributes.Remove("disabled");
					btnadd.Attributes.Remove("disabled");
					btnprint.Attributes.Remove("disabled");
					IPreturnItem_AutoCompleteExtender.ContextKey = source1.Substring(source1.LastIndexOf(':') + 1);
					txtipno.Text = source1.Substring(source1.LastIndexOf(':') + 1).Trim();
					txt_returnitem.Focus();
					Session["IPReturnItemList"] = null;
				}
            }
            else
            {
                txt_IPNo.Text = "";
                txt_IPNo.Focus();
                return;
            }
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> Getreturnitem(string prefixText, int count, string contextKey)
        {
            IPReturnData ObjData = new IPReturnData();
            IPReturnBO ObjBO = new IPReturnBO();
            List<IPReturnData> getResult = new List<IPReturnData>();
            ObjData.IPReturnItemDetails = prefixText;
            ObjData.IPNo = contextKey.ToString();
            getResult = ObjBO.GetIPReturnItem(ObjData);
            List<String> list = new List<String>();

            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].IPReturnItemDetailsList.ToString());
            }
            return list;
        }
        private void addnewrow()
        {
            lblmessage.Text = "";
            divmsg1.Visible = false;
            txt_IPNo.Attributes["disabled"] = "disabled";
            decimal rtnamount = 0;
            
			if (Convert.ToDecimal(txtreturnqty.Text.Trim() == "" ? "0" : txtreturnqty.Text.Trim()) ==0)
			{
				Messagealert_.ShowMessage(lblmessage, "Return quantity cannot be 0", 0);
				divmsg1.Visible = true;
				divmsg1.Attributes["class"] = "FailAlert";
				txtreturnqty.Text = "";
				txtreturnqty.Focus();
				return;
			}
			else
			{
				lblmessage.Text = "";
				divmsg1.Visible = false;
			}
			if (Convert.ToDecimal(txtreturnqty.Text.Trim() == "" ? "0" : txtreturnqty.Text.Trim()) + Convert.ToDecimal(txtlstrtnqty.Text.Trim() == "" ? "0" : txtlstrtnqty.Text.Trim()) > Convert.ToDecimal(txtequvqty.Text.Trim() == "" ? "0" : txtequvqty.Text.Trim()))
            {
                Messagealert_.ShowMessage(lblmessage, "GReturnQty", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                txtreturnqty.Text = "";
                txtreturnqty.Focus();
				return;
            }
            else
            {
                lblmessage.Text = "";
                divmsg1.Visible = false;
            }



            List<IPReturnData> LstIPReturnItem = Session["IPReturnItemList"] == null ? new List<IPReturnData>() : (List<IPReturnData>)Session["IPReturnItemList"];
			IPReturnData objdata = new IPReturnData();
			objdata.IPDrgIssueNo = txtIPDrgIssueNo.Text.Trim();
			objdata.UHID = Convert.ToInt64(txtUHID.Text.Trim());
			objdata.ID = Convert.ToInt64(txtID.Text.Trim());
			objdata.SubStockID = Convert.ToInt64(txtSubStockID.Text.Trim());
			objdata.ItemID = Convert.ToInt64(txtItemID.Text.Trim());
			objdata.ItemName = txtItemName.Text.Trim();
			objdata.Unit = Convert.ToDecimal(txtUnit.Text.Trim());
			objdata.MRPperQty = Convert.ToDecimal(txtmrpperqty.Text.Trim());
			objdata.Quantity = Convert.ToDecimal(txtequvqty.Text.Trim());
			objdata.RtnQuantity = Convert.ToDecimal(txtreturnqty.Text.Trim() == "" ? "0" : txtreturnqty.Text.Trim());
			rtnamount = (Convert.ToDecimal(txtreturnqty.Text.Trim() == "" ? "0" : txtreturnqty.Text.Trim()) * Convert.ToDecimal(txtmrpperqty.Text.Trim() == "" ? "0" : txtmrpperqty.Text.Trim()));
			txtreturnamt.Text = Commonfunction.Getrounding(rtnamount.ToString());
			objdata.ReturnAmt = Convert.ToDecimal(Commonfunction.Getrounding(rtnamount.ToString()));
			objdata.LreturnQty = txtlstrtnqty.Text.Trim();

			
			var source2 = txt_returnitem.Text.ToString();
			if (source2.Contains(":"))
			{
				Int64 ID = Convert.ToInt64(source2.Substring(source2.LastIndexOf(':') + 1).Trim());
				// Check Duplicate data 
				foreach (GridViewRow row in gvipreturn.Rows)
				{
					Label lbl_ID = (Label)gvipreturn.Rows[row.RowIndex].Cells[0].FindControl("lbl_ID");
					if (ID == Convert.ToInt64(lbl_ID.Text))
					{
						Messagealert_.ShowMessage(lblmessage, "Listcheck", 0);
						divmsg1.Visible = true;
						divmsg1.Attributes["class"] = "FailAlert";
						txt_returnitem.Focus();
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
				txt_returnitem.Text = "";
				return;
			}
			LstIPReturnItem.Add(objdata);

			if (LstIPReturnItem.Count > 0)
			{
				gvipreturn.DataSource = LstIPReturnItem;
				gvipreturn.DataBind();
				gvipreturn.Visible = true;
				Session["IPReturnItemList"] = LstIPReturnItem;
				btnsave.Attributes.Remove("disabled");
				txtIPDrgIssueNo.Text = "";
				txtUHID.Text = "";
				txtID.Text = "";
				txtSubStockID.Text = "";
				txtItemID.Text = "";
				txtItemName.Text = "";				
				txtUnit.Text = "";
				txtreturnqty.Text = "";
				txtequvqty.Text = "";
				txtmrpperqty.Text = "";
				txtreturnamt.Text = "";
				txtlstrtnqty.Text = "";
				txt_returnitem.Text = "";
				txt_returnitem.Focus();
				txtreturnqty.Attributes["disabled"] = "disabled";
				return;
			}
			else
			{
				gvipreturn.DataSource = null;
				gvipreturn.DataBind();
				gvipreturn.Visible = true;
				txtIPDrgIssueNo.Text = "";
				txtUHID.Text = "";
				txtID.Text = "";
				txtSubStockID.Text = "";
				txtItemID.Text = "";
				txtItemName.Text = "";
				txtUnit.Text = "";
				txtreturnqty.Text = "";
				txtequvqty.Text = "";
				txtmrpperqty.Text = "";
				txtreturnamt.Text = "";
				txtlstrtnqty.Text = "";
				txt_returnitem.Text = "";
				txt_returnitem.Focus();
				txtreturnqty.Attributes["disabled"] = "disabled";
			}



           

        }
        protected void txt_returnitem_TextChanged(object sender, EventArgs e)
        {
            lblmessage.Text = "";
            divmsg1.Visible = false;
            txt_IPNo.Attributes["disabled"] = "disabled";
            var source2 = txt_returnitem.Text.ToString();
            if (source2.Contains(":"))
            {
                IPReturnData ObjData = new IPReturnData();
                IPReturnBO ObjBO = new IPReturnBO();
                ObjData.ID = Convert.ToInt64(source2.Substring(source2.LastIndexOf(':') + 1).Trim());
                List<IPReturnData> LstIPReturnItemresult = new List<IPReturnData>();
               
				LstIPReturnItemresult = ObjBO.GetItemIssueByIPDrgIssueNo(ObjData);
				txtIPDrgIssueNo.Text = LstIPReturnItemresult[0].IPDrgIssueNo;
				txtUHID.Text = LstIPReturnItemresult[0].UHID.ToString();
				txtID.Text = LstIPReturnItemresult[0].ID.ToString();
				txtSubStockID.Text = LstIPReturnItemresult[0].SubStockID.ToString();
				txtItemID.Text = LstIPReturnItemresult[0].ItemID.ToString();
				txtItemName.Text = LstIPReturnItemresult[0].ItemName.ToString();
				txtUnit.Text = LstIPReturnItemresult[0].Unit.ToString();
				txtmrpperqty.Text = LstIPReturnItemresult[0].MRPperQty.ToString("N2");
				txtequvqty.Text = LstIPReturnItemresult[0].Quantity.ToString();
				txtlstrtnqty.Text = LstIPReturnItemresult[0].LreturnQty.ToString();
				txtreturnqty.Attributes.Remove("disabled");
				txtreturnqty.Focus();
				
				
                
            }
        }

        protected void btnadd_Click(object sender, EventArgs e)
        {
            addnewrow();
        }
        protected void gvipreturn_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Decimal totalrtnAmt = 0;
            Decimal totalrtnqty = 0;
            foreach (GridViewRow row in gvipreturn.Rows)
            {
                Label lbl_charge = (Label)gvipreturn.Rows[row.RowIndex].Cells[0].FindControl("lbl_charge");
                TextBox txt_rtnqty = (TextBox)gvipreturn.Rows[row.RowIndex].Cells[0].FindControl("txt_rtnqty");
                totalrtnqty = totalrtnqty + Convert.ToDecimal(txt_rtnqty.Text.Trim());
                totalrtnAmt = totalrtnAmt + Convert.ToDecimal(lbl_charge.Text.Trim());
            }
            txt_returnqty.Text = totalrtnqty.ToString();
            txt_returnAmount.Text = totalrtnAmt.ToString();
        }
        protected void gvipreturn_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Remove")
                {
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    List<IPReturnData> LstIPReturnItem = Session["IPReturnItemList"] == null ? new List<IPReturnData>() : (List<IPReturnData>)Session["IPReturnItemList"];
                    LstIPReturnItem.RemoveAt(i);
                    Session["IPReturnItemList"] = LstIPReturnItem;
                    gvipreturn.DataSource = LstIPReturnItem;
                    gvipreturn.DataBind();

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
            txt_IPNo.Text = "";
            txt_IPNo.Attributes.Remove("disabled");
            txt_returnitem.Text = "";
            txt_returnitem.Attributes["disabled"] = "disabled";
            Session["IPReturnItemList"] = null;
            gvipreturn.DataSource = null;
            gvipreturn.DataBind();
            gvipreturn.Visible = true;
            txt_returnNo.Text = "";
            txt_returnqty.Text = "";
            lblmessage.Text = "";
            divmsg1.Visible = false;
            txt_remarks.Text = "";
			txtIPDrgIssueNo.Text = "";
			txtUHID.Text = "";
			txtID.Text = "";
			txtSubStockID.Text = "";
			txtItemID.Text = "";
			txtItemName.Text = "";
			txtUnit.Text = "";
			txtreturnqty.Text = "";
			txtequvqty.Text = "";
			txtmrpperqty.Text = "";
			txtreturnamt.Text = "";
			txtlstrtnqty.Text = "";
			txt_returnitem.Text = "";
            txt_IPNo.ReadOnly = false;
			txtreturnqty.Attributes["disabled"] = "disabled";
			btnadd.Attributes["disabled"] = "disabled";
			btnsave.Attributes["disabled"] = "disabled";
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

            if (Convert.ToInt32(txt_returnqty.Text == "" ? "0" : txt_returnqty.Text) == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "Total return quantity cannot be 0", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                txt_returnqty.Focus();
                txt_returnqty.Text = "0";
                txt_returnqty.BorderColor = System.Drawing.Color.Red;
                return;
            }
            else
            {
                lblmessage.Visible = false;
                lblmessage.Visible = false;
            }
            List<IPReturnData> lstdata = new List<IPReturnData>();
            IPReturnData objdata = new IPReturnData();
            IPReturnBO objBO = new IPReturnBO();
           
            try
            {
                
                int i = 0;
                // get all the record from the gridview
                foreach (GridViewRow row in gvipreturn.Rows)
                {
                    IFormatProvider provider = new System.Globalization.CultureInfo("en-GB", true);
                    Label lbl_IPDrgIssueNo = (Label)gvipreturn.Rows[row.RowIndex].Cells[0].FindControl("lbl_IPDrgIssueNo");
                    Label lbl_UHID = (Label)gvipreturn.Rows[row.RowIndex].Cells[0].FindControl("lbl_UHID");
                    Label lbl_ID = (Label)gvipreturn.Rows[row.RowIndex].Cells[0].FindControl("lbl_ID");
                    Label lbl_substockID = (Label)gvipreturn.Rows[row.RowIndex].Cells[0].FindControl("lbl_substockID");
                    Label lbl_ItemID = (Label)gvipreturn.Rows[row.RowIndex].Cells[0].FindControl("lbl_ItemID");
                    Label lbl_unit = (Label)gvipreturn.Rows[row.RowIndex].Cells[0].FindControl("lbl_unit");
                    Label lbl_qty = (Label)gvipreturn.Rows[row.RowIndex].Cells[0].FindControl("lbl_qty");
                    TextBox txt_rtnqty = (TextBox)gvipreturn.Rows[row.RowIndex].Cells[0].FindControl("txt_rtnqty");
                    Label lbl_lastqty = (Label)gvipreturn.Rows[row.RowIndex].Cells[0].FindControl("lbl_lastqty");
                    Label lbl_mrp = (Label)gvipreturn.Rows[row.RowIndex].Cells[0].FindControl("lbl_mrp");
                    Label lbl_charge = (Label)gvipreturn.Rows[row.RowIndex].Cells[0].FindControl("lbl_charge");
                    IPReturnData ObjDetails = new IPReturnData();
                        ObjDetails.ID = Convert.ToInt64(lbl_ID.Text == "" ? "0" : lbl_ID.Text);
                        ObjDetails.IPDrgIssueNo = lbl_IPDrgIssueNo.Text.Trim() == "" ? "0" : lbl_IPDrgIssueNo.Text.Trim();
                        ObjDetails.UHID = Convert.ToInt64(lbl_UHID.Text == "" ? "0" : lbl_UHID.Text);
                        ObjDetails.SubStockID = Convert.ToInt64(lbl_substockID.Text == "" ? "0" : lbl_substockID.Text);
                        ObjDetails.ItemID = Convert.ToInt64(lbl_ItemID.Text == "" ? "0" : lbl_ItemID.Text);
                        ObjDetails.Unit = Convert.ToDecimal(lbl_unit.Text == "" ? "0" : lbl_unit.Text);
                        ObjDetails.Quantity = Convert.ToDecimal(lbl_qty.Text == "" ? "0" : lbl_qty.Text);
                        ObjDetails.Return = Convert.ToDecimal(txt_rtnqty.Text == "" ? "0" : txt_rtnqty.Text);
                        ObjDetails.MRPperQty = Convert.ToDecimal(lbl_mrp.Text == "" ? "0" : lbl_mrp.Text);
                        ObjDetails.ReturnAmt = Convert.ToDecimal(lbl_charge.Text == "" ? "0" : lbl_charge.Text);
					    i++;
                        lstdata.Add(ObjDetails);
                       
                    
                }
               
                    
				if (i == 0)
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
				    objdata.XMLData = XmlConvertor.IPReturnDetailsDatatoXML(lstdata).ToString();
                    objdata.HandOver = LogData.EmployeeID;
                    objdata.totalreturnQty = Convert.ToDecimal(txt_returnqty.Text.Trim() == "" ? "0" : txt_returnqty.Text.Trim());
                    objdata.TotalReturnAmt = Convert.ToDecimal(txt_returnAmount.Text.Trim() == "" ? "0" : txt_returnAmount.Text.Trim());
                    objdata.IPNo = txtipno.Text.Trim();
                    objdata.EmployeeID = LogData.EmployeeID;
                    objdata.HospitalID = LogData.HospitalID;
                    objdata.FinancialYearID = LogData.FinancialYearID;
                    objdata.ActionType = Enumaction.Insert;

					List<IPReturnData> result = objBO.UpdateIPReturnDetails(objdata);
					if (result.Count > 0)
					{
						txt_returnNo.Text = result[0].ReturnNo.ToString();
						lblmessage.Visible = true;
						Messagealert_.ShowMessage(lblmessage, "save", 1);
						divmsg1.Visible = true;
						divmsg1.Attributes["class"] = "SucessAlert";
						btnsave.Attributes["disabled"] = "disabled";
						txt_returnitem.Text = "";
						txt_returnitem.Attributes["disabled"] = "disabled";
						txt_IPNo.Attributes.Remove("disabled");
						btnadd.Attributes["disabled"] = "disabled";
						if (LogData.PrintEnable == 0)
						{
							btnprint.Attributes["disabled"] = "disabled";
						}
						else
						{
							btnprint.Attributes.Remove("disabled");
						}
						Session["IPReturnItemList"] = null;
					}
					else
					{
						txt_returnNo.Text = "";
						Messagealert_.ShowMessage(lblmessage, "system", 0);
						divmsg1.Visible = true;
						divmsg1.Attributes["class"] = "FailAlert";
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
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetadvanceSearchIPPatient(string prefixText, int count, string contextKey)
        {
            IPReturnData ObjData = new IPReturnData();
            IPReturnBO ObjBO = new IPReturnBO();
            List<IPReturnData> getResult = new List<IPReturnData>();
            ObjData.PatientDetails = prefixText;
            getResult = ObjBO.GetadvanceSearchIPNo(ObjData);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].PatientDetailsList.ToString());
            }
            return list;
        }

        protected void txt_tap2IPNO_Textchange(object sender, EventArgs e)
        {
            var source10 = txt_tap2IPNO.Text.ToString();
            if (source10.Contains(":"))
            {

                txt_tap2IPNOs.Text = source10.Substring(source10.LastIndexOf(':') + 1).Trim();
                bindgrid1();


            }
            else
            {
                txt_tap2IPNO.Text = "";
                txt_tap2IPNO.Focus();
                return;
            }

        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetReturnNo(string prefixText, int count, string contextKey)
        {
            IPReturnData ObjData = new IPReturnData();
            IPReturnBO ObjBO = new IPReturnBO();
            List<IPReturnData> getResult = new List<IPReturnData>();
            ObjData.ReturnNo = prefixText.Trim();
            getResult = ObjBO.GetIPReturnNo(ObjData);
            List<String> list = new List<String>();

            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].ReturnNo.ToString());
            }
            return list;
        }
        protected void btnsearch1_Click(object sender, EventArgs e)
        {
            bindgrid1();
        }
        protected void bindgrid1()
        {
            try
            {

                List<IPReturnData> objlst = GetiPReturnList1(0);
                if (objlst.Count > 0)
                {
                    gvipreturnlist.DataSource = objlst;
                    gvipreturnlist.DataBind();
                    gvipreturnlist.Visible = true;
                    Messagealert_.ShowMessage(lblresult1, "Total:" + objlst.Count + " Record(s) found.", 1);
                    txt_totalreturnqty.Text = Commonfunction.Getrounding(objlst[0].GrandTotalReturnQty.ToString());

                    div2.Attributes["class"] = "SucessAlert";
                    div2.Visible = true;
                    lblmessage1.Visible = false;
                    lblmessage1.Visible = false;
                    if (LogData.PrintEnable == 0)
                    {
                        btnprint1.Attributes["disabled"] = "disabled";
                    }
                    else
                    {
                        btnprint1.Attributes.Remove("disabled");
                    }
                }
                else
                {
                    txt_totalreturnqty.Text = "0";
                    gvipreturnlist.DataSource = null;
                    gvipreturnlist.DataBind();
                    gvipreturnlist.Visible = true;
                    lblresult.Visible = false;
                    divmsg3.Visible = false;
                    btnprint1.Attributes["disabled"] = "disabled";
                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
            }
        }
        public List<IPReturnData> GetiPReturnList1(int curIndex)
        {
            IPReturnData objdata = new IPReturnData();
            IPReturnBO objbo = new IPReturnBO();
            objdata.ReturnNo = txt_returnNum.Text.ToString() == "" ? "0" : txt_returnNum.Text.ToString();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime from = txt_retdatefrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txt_retdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime to = txt_returndateTo.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txt_returndateTo.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objdata.DateFrom = from;
            objdata.DateTo = to;
            objdata.IPNo = txt_tap2IPNOs.Text.Trim() == "" ? "0" : txt_tap2IPNOs.Text.Trim();
            objdata.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
            return objbo.GetiPReturnList1(objdata);
        }
        protected void gvipreturnlist_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Print")
                {
                    if (LogData.PrintEnable == 0)
                    {
                        Messagealert_.ShowMessage(lblmessage, "PrintEnable", 0);
                        divmsg1.Visible = true;
                        divmsg1.Attributes["class"] = "FailAlert";
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                    }

                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = gvipreturnlist.Rows[i];
                    Label ReturnNo = (Label)gr.Cells[0].FindControl("lbl_returnno");                   
                    string url = "../MedStore/Reports/ReportViewer.aspx?option=IPReturnList&ReturnNo=" + ReturnNo.Text.Trim();
                    string fullURL = "window.open('" + url + "', '_blank');";
                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_New_Tab", fullURL, true);
                }

                if (e.CommandName == "Deletes")
                {
                    if (LogData.RoleID == 1 || LogData.RoleID == 40)
                    {
                        if (LogData.DeleteEnable == 0)
                        {
                            Messagealert_.ShowMessage(lblmessage1, "DeleteEnable", 0);
                            divmsg2.Visible = true;
                            divmsg2.Attributes["class"] = "FailAlert";
                            return;
                        }
                        else
                        {
                            lblmessage.Visible = false;
                        }
                        IPReturnData objdata = new IPReturnData();
                        IPReturnBO objstdBO = new IPReturnBO();
                        int i = Convert.ToInt16(e.CommandArgument.ToString());
                        GridViewRow gr = gvipreturnlist.Rows[i];
                        Label lbl_returnno = (Label)gr.Cells[0].FindControl("lbl_returnno");
                        TextBox txtremarks = (TextBox)gr.Cells[0].FindControl("txtremarks");
                        txtremarks.Enabled = true;
                        if (txtremarks.Text == "")
                        {
                            Messagealert_.ShowMessage(lblmessage1, "Remarks", 0);
                            divmsg2.Attributes["class"] = "FailAlert";
                            txtremarks.Focus();
                            divmsg2.Visible = true;
                            return;
                        }
                        else
                        {
                            objdata.Remarks = txtremarks.Text;
                            divmsg2.Visible = false;
                        }
                        objdata.ReturnNo = lbl_returnno.Text.Trim();
                        objdata.EmployeeID = LogData.EmployeeID;
                        int Result = objstdBO.DeleteIPReturnItemByReturnNo(objdata);
                        if (Result == 1)
                        {
                            Messagealert_.ShowMessage(lblmessage1, "delete", 1);
                            divmsg2.Attributes["class"] = "SucessAlert";
                            divmsg2.Visible = true;

                            bindgrid1();
                            return;
                        }
                        else
                        {
                            Messagealert_.ShowMessage(lblmessage1, "system", 0);
                            divmsg2.Attributes["class"] = "FailAlert";
                            divmsg2.Visible = true;
                            return;
                        }
                    }
                    else
                    {
                        Messagealert_.ShowMessage(lblmessage1, "DeleteEnable", 0);
                        divmsg2.Attributes["class"] = "FailAlert";
                        divmsg2.Visible = true;
                        return;
                    }
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
        protected void btnreset1_Click(object sender, EventArgs e)
        {
            txt_returnNum.Text = "";
            txt_retdatefrom.Text = "";
            txt_returndateTo.Text = "";
            txt_totalreturnqty.Text = "";
            txt_tap2IPNO.Text = "";
            txt_tap2IPNOs.Text = "";
            ddlstatus.SelectedIndex = 0;
            gvipreturnlist.DataSource = null;
            gvipreturnlist.DataBind();
        }

        protected void btnprint_Click(object sender, EventArgs e)
        {
            if (LogData.PrintEnable == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "PrintEnable", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                return;
            }
            else
            {
                lblmessage.Visible = false;
            }

            string ReturnNo = txt_returnNo.Text.Trim();
			string url = "../MedStore/Reports/ReportViewer.aspx?option=IPReturnList&ReturnNo=" + ReturnNo.Trim();
            string fullURL = "window.open('" + url + "', '_blank');";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_New_Tab", fullURL, true);
        }


    }
}
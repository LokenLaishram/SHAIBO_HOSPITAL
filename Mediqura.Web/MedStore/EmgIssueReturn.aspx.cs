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
    public partial class EmgIssueReturn : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                Session["EmergReturnItemList"] = null;
                txt_returnitem.Attributes["disabled"] = "disabled";
                btnadd.Attributes["disabled"] = "disabled";
				btnsave.Attributes["disabled"] = "disabled";
				txtreturnqty.Attributes["disabled"] = "disabled";
                btnprint.Attributes["disabled"] = "disabled";
                if (Session["SaleReturns"] != null)
                {
                    txt_EmergNo.Text = Session["SaleReturns"].ToString();
                    getpatientdetails();
                    txt_EmergNo.ReadOnly = true;
                    Session["SaleReturns"] = null;
                }
            }
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetadvanceSearchEmgNo(string prefixText, int count, string contextKey)
        {
            EmgReturnData ObjData = new EmgReturnData();
            EmgReturnBO ObjBO = new EmgReturnBO();
            List<EmgReturnData> getResult = new List<EmgReturnData>();
            ObjData.PatientDetails = prefixText;
            getResult = ObjBO.GetadvanceSearchEmergNo(ObjData);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].PatientDetailsList.ToString());
            }
            return list;
        }
        protected void txt_EmergNo_Textchange(object sender, EventArgs e)
        {
            getpatientdetails();
        }
        private void getpatientdetails()
        {
            var source1 = txt_EmergNo.Text.ToString();
            if (source1.Contains(":"))
            {
                txt_returnitem.Attributes.Remove("disabled");
                btnadd.Attributes.Remove("disabled");
                btnprint.Attributes.Remove("disabled");
                EmergreturnItem_AutoCompleteExtender.ContextKey = source1.Substring(source1.LastIndexOf(':') + 1);
                txtEmergno.Text = source1.Substring(source1.LastIndexOf(':') + 1).Trim();
                txt_returnitem.Focus();
                Session["EmergReturnItemList"] = null;

            }
            else
            {
                txt_EmergNo.Text = "";
                txt_EmergNo.Focus();
                return;
            }                           
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> Getreturnitem(string prefixText, int count, string contextKey)
        {
            EmgReturnData ObjData = new EmgReturnData();
            EmgReturnBO ObjBO = new EmgReturnBO();
            List<EmgReturnData> getResult = new List<EmgReturnData>();
            ObjData.EmergReturnItemDetails = prefixText;
            ObjData.EmergNo = contextKey.ToString();
            getResult = ObjBO.GetEmergReturnItem(ObjData);
            List<String> list = new List<String>();

            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].EmergReturnItemDetails.ToString());
            }
            return list;
        }
        private void addnewrow()
        {
			lblmessage.Text = "";
			divmsg1.Visible = false;
			txt_EmergNo.Attributes["disabled"] = "disabled";
			decimal rtnamount = 0;

			if (Convert.ToDecimal(txtreturnqty.Text.Trim() == "" ? "0" : txtreturnqty.Text.Trim()) == 0)
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
			if (Convert.ToDecimal(txtreturnqty.Text.Trim() == "" ? "0" : txtreturnqty.Text.Trim()) > Convert.ToDecimal(txtequvqty.Text.Trim() == "" ? "0" : txtequvqty.Text.Trim()))
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



			List<EmgReturnData> LstEmgReturnItem = Session["EmergReturnItemList"] == null ? new List<EmgReturnData>() : (List<EmgReturnData>)Session["EmergReturnItemList"];
			EmgReturnData objdata = new EmgReturnData();
			objdata.EmergDrgIssueNo = txtIPDrgIssueNo.Text.Trim();
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



			var source2 = txt_returnitem.Text.ToString();
			if (source2.Contains(":"))
			{
				Int64 ID = Convert.ToInt64(source2.Substring(source2.LastIndexOf(':') + 1).Trim());
				// Check Duplicate data 
				foreach (GridViewRow row in gvEmergreturn.Rows)
				{
					Label lbl_ID = (Label)gvEmergreturn.Rows[row.RowIndex].Cells[0].FindControl("lbl_ID");
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
			LstEmgReturnItem.Add(objdata);

			if (LstEmgReturnItem.Count > 0)
			{
				gvEmergreturn.DataSource = LstEmgReturnItem;
				gvEmergreturn.DataBind();
				gvEmergreturn.Visible = true;
				Session["EmergReturnItemList"] = LstEmgReturnItem;
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
				gvEmergreturn.DataSource = null;
				gvEmergreturn.DataBind();
				gvEmergreturn.Visible = true;
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
			txt_EmergNo.Attributes["disabled"] = "disabled";
			var source2 = txt_returnitem.Text.ToString();
			if (source2.Contains(":"))
			{
				EmgReturnData ObjData = new EmgReturnData();
				EmgReturnBO ObjBO = new EmgReturnBO();
				ObjData.ID = Convert.ToInt64(source2.Substring(source2.LastIndexOf(':') + 1).Trim());
				List<EmgReturnData> LstEmgReturnItemresult = new List<EmgReturnData>();

				LstEmgReturnItemresult = ObjBO.GetItemIssueByEmergDrgIssueNo(ObjData);
				txtIPDrgIssueNo.Text = LstEmgReturnItemresult[0].EmergDrgIssueNo;
				txtUHID.Text = LstEmgReturnItemresult[0].UHID.ToString();
				txtID.Text = LstEmgReturnItemresult[0].ID.ToString();
				txtSubStockID.Text = LstEmgReturnItemresult[0].SubStockID.ToString();
				txtItemID.Text = LstEmgReturnItemresult[0].ItemID.ToString();
				txtItemName.Text = LstEmgReturnItemresult[0].ItemName.ToString();
				txtUnit.Text = LstEmgReturnItemresult[0].Unit.ToString();
				txtmrpperqty.Text = LstEmgReturnItemresult[0].MRPperQty.ToString("N2");
				txtequvqty.Text = LstEmgReturnItemresult[0].Quantity.ToString();
				txtlstrtnqty.Text = LstEmgReturnItemresult[0].LreturnQty.ToString();
				txtreturnqty.Attributes.Remove("disabled");
				txtreturnqty.Focus();


			}
        }
        protected void btnadd_Click(object sender, EventArgs e)
        {
            addnewrow();
        }
        protected void gvEmergreturn_RowDataBound(object sender, GridViewRowEventArgs e)
        {
			Decimal totalrtnAmt = 0;
			Decimal totalrtnqty = 0;
			foreach (GridViewRow row in gvEmergreturn.Rows)
			{
				Label lbl_charge = (Label)gvEmergreturn.Rows[row.RowIndex].Cells[0].FindControl("lbl_charge");
				TextBox txt_rtnqty = (TextBox)gvEmergreturn.Rows[row.RowIndex].Cells[0].FindControl("txt_rtnqty");
				totalrtnqty = totalrtnqty + Convert.ToDecimal(txt_rtnqty.Text.Trim());
				totalrtnAmt = totalrtnAmt + Convert.ToDecimal(lbl_charge.Text.Trim());
			}
			txt_returnqty.Text = totalrtnqty.ToString();
			txt_returnAmount.Text = totalrtnAmt.ToString();
        }
        protected void gvEmergreturn_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Remove")
                {
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    List<EmgReturnData> LstEmergReturnItem = Session["EmergReturnItemList"] == null ? new List<EmgReturnData>() : (List<EmgReturnData>)Session["EmergReturnItemList"];
                    LstEmergReturnItem.RemoveAt(i);
                    Session["EmergReturnItemList"] = LstEmergReturnItem;
                    gvEmergreturn.DataSource = LstEmergReturnItem;
                    gvEmergreturn.DataBind();
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
            txt_EmergNo.Text = "";
            txt_EmergNo.Attributes.Remove("disabled");
            txt_returnitem.Text = "";
            txt_returnitem.Attributes["disabled"] = "disabled";
            Session["EmergReturnItemList"] = null;
            gvEmergreturn.DataSource = null;
            gvEmergreturn.DataBind();
            gvEmergreturn.Visible = true;
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
            txt_EmergNo.ReadOnly = false;
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
            List<EmgReturnData> lstdata = new List<EmgReturnData>();
            EmgReturnData objdata = new EmgReturnData();
            EmgReturnBO objBO = new EmgReturnBO();
           
            try
            {
              
                int i = 0;
                // get all the record from the gridview
                foreach (GridViewRow row in gvEmergreturn.Rows)
                {
                    IFormatProvider provider = new System.Globalization.CultureInfo("en-GB", true);
                    Label lbl_EmergDrgIssueNo = (Label)gvEmergreturn.Rows[row.RowIndex].Cells[0].FindControl("lbl_EmergDrgIssueNo");
                    Label lbl_UHID = (Label)gvEmergreturn.Rows[row.RowIndex].Cells[0].FindControl("lbl_UHID");
                    Label lbl_ID = (Label)gvEmergreturn.Rows[row.RowIndex].Cells[0].FindControl("lbl_ID");
                    Label lbl_substockID = (Label)gvEmergreturn.Rows[row.RowIndex].Cells[0].FindControl("lbl_substockID");
                    Label lbl_ItemID = (Label)gvEmergreturn.Rows[row.RowIndex].Cells[0].FindControl("lbl_ItemID");
                    Label lbl_unit = (Label)gvEmergreturn.Rows[row.RowIndex].Cells[0].FindControl("lbl_unit");
                    Label lbl_qty = (Label)gvEmergreturn.Rows[row.RowIndex].Cells[0].FindControl("lbl_qty");
                    TextBox txt_rtnqty = (TextBox)gvEmergreturn.Rows[row.RowIndex].Cells[0].FindControl("txt_rtnqty");
                    Label lbl_lastqty = (Label)gvEmergreturn.Rows[row.RowIndex].Cells[0].FindControl("lbl_lastqty");
					Label lbl_mrp = (Label)gvEmergreturn.Rows[row.RowIndex].Cells[0].FindControl("lbl_mrp");
					Label lbl_charge = (Label)gvEmergreturn.Rows[row.RowIndex].Cells[0].FindControl("lbl_charge");
                    EmgReturnData ObjDetails = new EmgReturnData();


                        ObjDetails.ID = Convert.ToInt64(lbl_ID.Text == "" ? "0" : lbl_ID.Text);
                        ObjDetails.EmergDrgIssueNo = lbl_EmergDrgIssueNo.Text.Trim() == "" ? "0" : lbl_EmergDrgIssueNo.Text.Trim();
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
                    objdata.XMLData = XmlConvertor.EmergReturnDetailsDatatoXML(lstdata).ToString();
					objdata.HandOver = LogData.EmployeeID;
					objdata.totalreturnQty = Convert.ToDecimal(txt_returnqty.Text.Trim() == "" ? "0" : txt_returnqty.Text.Trim());
					objdata.TotalReturnAmt = Convert.ToDecimal(txt_returnAmount.Text.Trim() == "" ? "0" : txt_returnAmount.Text.Trim());
                    objdata.EmergNo = txtEmergno.Text.Trim();
                    objdata.EmployeeID = LogData.EmployeeID;
                    objdata.HospitalID = LogData.HospitalID;
                    objdata.FinancialYearID = LogData.FinancialYearID;
                    objdata.ActionType = Enumaction.Insert;
                  
					List<EmgReturnData> result = objBO.UpdateEmergReturnDetails(objdata);
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
                        txt_EmergNo.Attributes.Remove("disabled");
                        btnadd.Attributes["disabled"] = "disabled";
                        if (LogData.PrintEnable == 0)
                        {
                            btnprint.Attributes["disabled"] = "disabled";
                        }
                        else
                        {
                            btnprint.Attributes.Remove("disabled");
                        }
						Session["EmergReturnItemList"] = null;
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
		public static List<string> GetadvanceSearchEmgPatient(string prefixText, int count, string contextKey)
		{
			EmgReturnData ObjData = new EmgReturnData();
			EmgReturnBO ObjBO = new EmgReturnBO();
			List<EmgReturnData> getResult = new List<EmgReturnData>();
			ObjData.PatientDetails = prefixText;
			getResult = ObjBO.GetadvanceSearchEmergNo(ObjData);
			List<String> list = new List<String>();
			for (int i = 0; i < getResult.Count; i++)
			{
				list.Add(getResult[i].PatientDetailsList.ToString());
			}
			return list;
		}
		protected void txt_tap2_EmergNo_Textchange(object sender, EventArgs e)
		{
			var source10 = txt_tap2_EmergNo.Text.ToString();
			if (source10.Contains(":"))
			{
				
				txt_tap2_EmergNos.Text = source10.Substring(source10.LastIndexOf(':') + 1).Trim();
				bindgrid1();

			}
			else
			{
				txt_tap2_EmergNo.Text = "";
				txt_tap2_EmergNo.Focus();
				return;
			}

		}
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetReturnNo(string prefixText, int count, string contextKey)
        {
            EmgReturnData ObjData = new EmgReturnData();
            EmgReturnBO ObjBO = new EmgReturnBO();
            List<EmgReturnData> getResult = new List<EmgReturnData>();
            ObjData.ReturnNo = prefixText.Trim();
            getResult = ObjBO.GetEmergReturnNo(ObjData);
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

                List<EmgReturnData> objlst = GetEmergReturnList1(0);
                if (objlst.Count > 0)
                {
                    gvEmergreturnlist.DataSource = objlst;
                    gvEmergreturnlist.DataBind();
                    gvEmergreturnlist.Visible = true;
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
                    gvEmergreturnlist.DataSource = null;
                    gvEmergreturnlist.DataBind();
                    gvEmergreturnlist.Visible = true;
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
        public List<EmgReturnData> GetEmergReturnList1(int curIndex)
        {
            EmgReturnData objdata = new EmgReturnData();
            EmgReturnBO objbo = new EmgReturnBO();
            objdata.ReturnNo = txt_returnNum.Text.ToString() == "" ? "0" : txt_returnNum.Text.ToString();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime from = txt_retdatefrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txt_retdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime to = txt_returndateTo.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txt_returndateTo.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objdata.DateFrom = from;
            objdata.DateTo = to;
			objdata.EmergNo = txt_tap2_EmergNos.Text.Trim() == "" ? "0" : txt_tap2_EmergNos.Text.Trim();
            objdata.IsActive = ddlstatus.SelectedValue == "0" ? true : false;
            return objbo.GetEmergReturnList1(objdata);
        }
        protected void gvEmergreturnlist_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
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
                        EmgReturnData objdata = new EmgReturnData();
                        EmgReturnBO objstdBO = new EmgReturnBO();
                        int i = Convert.ToInt16(e.CommandArgument.ToString());
                        GridViewRow gr = gvEmergreturnlist.Rows[i];
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
                        int Result = objstdBO.DeleteEmergReturnItemByReturnNo(objdata);
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
            ddlstatus.SelectedIndex = 0;
            gvEmergreturnlist.DataSource = null;
            gvEmergreturnlist.DataBind();
            lblmessage1.Visible = false;
            lblresult1.Visible = false;
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
			string url = "../MedStore/Reports/ReportViewer.aspx?option=EmgReturnList&ReturnNo=" + ReturnNo.Trim();
			string fullURL = "window.open('" + url + "', '_blank');";
			ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_New_Tab", fullURL, true);

		}
    }
}
using ClosedXML.Excel;
using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedStore;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.LoginData;
using Mediqura.CommonData.MedStore;
using Mediqura.Utility;
using Mediqura.Utility.ExceptionHandler;
using Mediqura.Utility.Logging;
using Mediqura.Web.MedCommon;
using SignalRChat;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mediqura.Web.MedPhr
{
    public partial class IndentRequest : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindddl();
                bindddlPostback();
                btnsave.Attributes["disabled"] = "disabled";
                btnprint.Attributes["disabled"] = "disabled";
                Session["IndentList"] = null;
                ddl_IndentStatus.SelectedValue = "1";
                bindgrid();
            }
        }
        private void bindddl()
        {

            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddl_substock, mstlookup.GetLookupsList(LookupName.SubStockType));
            ddl_substock.SelectedValue = LogData.MedSubStockID.ToString();
            Commonfunction.PopulateDdl(ddl_substocklist, mstlookup.GetLookupsList(LookupName.SubStockType));
            ddl_substocklist.SelectedValue = LogData.MedSubStockID.ToString();
            Commonfunction.PopulateDdl(ddl_userByDepID, mstlookup.GetLookupsList(LookupName.Employee));
            ddl_userByDepID.SelectedValue = LogData.EmployeeID.ToString();
            Commonfunction.PopulateDdl(ddl_requestType, mstlookup.GetLookupsList(LookupName.requestType));
            Commonfunction.PopulateDdl(ddl_requestTypeList, mstlookup.GetLookupsList(LookupName.requestType));
            Commonfunction.PopulateDdl(ddl_user, mstlookup.GetEmployeeByDep(47));
            Commonfunction.PopulateDdl(ddl_IndentStatus, mstlookup.GetLookupsList(LookupName.IndentStatus));
            ddl_userByDepID.Attributes["disabled"] = "disabled";
            txtItemName.Focus();
            if (LogData.RoleID == 1 || LogData.RoleID == 40)
            {
                ddl_substocklist.Attributes.Remove("disabled");
                ddl_substock.Attributes.Remove("disabled");
            }
            else
            {
                ddl_substock.Attributes["disabled"] = "disabled";
                ddl_substocklist.Attributes["disabled"] = "disabled";

            }
        }
        private void bindddlPostback()
        {
            txt_IssuueDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            btn_print.Attributes["disabled"] = "disabled";
            ddl_requestType.SelectedIndex = 1;
        }
        protected void txtItemName_TextChanged(object sender, EventArgs e)
        {
            MedIndentData objItem = new MedIndentData();
            MedStoreIndentBO ObjItemBO = new MedStoreIndentBO();
            List<MedIndentData> getResult = new List<MedIndentData>();
            string source = txtItemName.Text;
            if (source.Contains(":"))
			{
				string item = null;
				item = source.Substring(0,source.LastIndexOf('|') ).Trim();
				objItem.BatchNo = source.Substring(source.LastIndexOf(':') + 1).Trim();
				objItem.ItemID = Convert.ToInt64(item.Substring(item.LastIndexOf(':') + 1).Trim());
                objItem.MedSubStockID = Convert.ToInt32(ddl_substock.SelectedValue == "" ? "0" : ddl_substock.SelectedValue);
                getResult = ObjItemBO.GetItemDetailsByBatchNo(objItem);
                if (getResult.Count > 0)
                {   txtquantity.Focus();
                    txt_unit.Text = getResult[0].Unitname.ToString();
                    hdnHSNCode.Value = getResult[0].HSNCode.ToString();                   
                    hdnSGST.Value = getResult[0].SGST.ToString();
                    hdnCGST.Value = getResult[0].CGST.ToString();
                    hdnIGST.Value = getResult[0].IGST.ToString();  
                }
            }
            else
            {
                hdntotalavail.Value = "";
                txtItemName.Text = "";
                txt_Avail.Text = "";
                txt_unit.Text = "";
                hdnHSNCode.Value = "";
                hdnSGST.Value = "";
                hdnCGST.Value = "";
                hdnIGST.Value = "";
            }
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetItemDetails(string prefixText, int count, string contextKey)
        {
            MedIndentData Objpaic = new MedIndentData();
            MedStoreIndentBO objInfoBO = new MedStoreIndentBO();
            List<MedIndentData> getResult = new List<MedIndentData>();
            Objpaic.ItemName = prefixText;
            getResult = objInfoBO.GetItemNameListInStoreByBatchNo(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].ItemName);

            }
            return list;
        }
        protected void btnadd_Click(object sender, EventArgs e)
        {
            additem();
        }
        private void additem()
        {
            if (txtItemName.Text.Trim() == "" || !txtItemName.Text.Contains(":"))
            {
                Messagealert_.ShowMessage(lblmessage, "ItemName", 0);
                txtItemName.Text = "";
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                txtItemName.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
            }
            if (ddl_substock.SelectedIndex == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "GenSubStock", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                return;
            }
            else
            {
                lblmessage.Visible = false;
            }
            if (ddl_requestType.SelectedIndex == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "RequestType", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                ddl_requestType.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
            }
            if (Commonfunction.isValidDate(txt_IssuueDate.Text) == false && Commonfunction.ChecklowerDate(txt_IssuueDate.Text) == true)
            {
                Messagealert_.ShowMessage(lblmessage, "ValidDate", 0);
                divmsg1.Attributes["class"] = "FailAlert";
                divmsg1.Visible = true;
                txt_IssuueDate.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
            }
            if (txtquantity.Text == "" || txtquantity.Text == "0")
            {
                Messagealert_.ShowMessage(lblmessage, "StockQty", 0);
                txtquantity.Text = "";
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                txtquantity.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
            }
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            List<MedIndentData> IndentList = Session["IndentList"] == null ? new List<MedIndentData>() : (List<MedIndentData>)Session["IndentList"];
            MedIndentData objStock = new MedIndentData();
            string BatchNo;
			
            string source = txtItemName.Text.ToString();

            if (source.Contains(":"))
            {
				

				BatchNo = source.Substring(source.LastIndexOf(':') + 1);
             

                //// Check Duplicate data 
                //foreach (GridViewRow row in gvIndentRequest.Rows)
                //{
                //    Label ItemID = (Label)gvIndentRequest.Rows[row.RowIndex].Cells[0].FindControl("lbl_ItemID");

                //    if (Convert.ToInt64(ItemID.Text) == Convert.ToInt64(ItmID))
                //    {
                //        Messagealert_.ShowMessage(lblmessage, "Listcheck", 0);
                //        divmsg1.Visible = true;
                //        divmsg1.Attributes["class"] = "FailAlert";
                //        txtItemName.Focus();
                //        return;
                //    }
                //    else
                //    {
                //        lblmessage.Visible = false;
                //    }
                //}
            }
            else
            {
                txtItemName.Text = "";
                return;
            }
            objStock.BatchNo = BatchNo;
            objStock.ReqdQty = Convert.ToInt32(txtquantity.Text == "" ? "0" : txtquantity.Text);
            objStock.ItemName = txtItemName.Text.ToString();

            MedIndentData objItem = new MedIndentData();
            MedStoreIndentBO ObjItemBO = new MedStoreIndentBO();
            List<MedIndentData> getResult = new List<MedIndentData>();
     
            if (source.Contains(":"))
            {
				string item = null;
				item = source.Substring(0, source.LastIndexOf('|')).Trim();
				objItem.BatchNo = source.Substring(source.LastIndexOf(':') + 1).Trim();
				objItem.ItemID = Convert.ToInt64(item.Substring(item.LastIndexOf(':') + 1).Trim());
                objItem.MedSubStockID = Convert.ToInt32(ddl_substock.SelectedValue == "" ? "0" : ddl_substock.SelectedValue);
                getResult = ObjItemBO.GetItemDetailsByBatchNo(objItem);
                if (getResult.Count > 0)
                {
                    objStock.MainStockAvail = getResult[0].MainStockAvail;
                    objStock.ItemID = getResult[0].ItemID;
					objStock.SupplierID = getResult[0].SupplierID;
                    txtquantity.Focus();
                    objStock.Unitname = getResult[0].Unitname.ToString();
                    hdnHSNCode.Value = getResult[0].HSNCode.ToString();
                    hdnSGST.Value = getResult[0].SGST.ToString();
                    hdnCGST.Value = getResult[0].CGST.ToString();
                    hdnIGST.Value = getResult[0].IGST.ToString();
                }
            }
            else
            {
                hdntotalavail.Value = "";
                txtItemName.Text = "";
                txt_Avail.Text = "";
                txt_unit.Text = "";
                hdnHSNCode.Value = "";
                hdnSGST.Value = "";
                hdnCGST.Value = "";
                hdnIGST.Value = "";
            }

            IndentList.Add(objStock);
            if (IndentList.Count > 0)
            {
                txt_totRequestQty.Text = (Convert.ToInt32(txt_totRequestQty.Text == "" ? "0" : txt_totRequestQty.Text) + Convert.ToInt32(txtquantity.Text == "" ? "0" : txtquantity.Text)).ToString();
                gvIndentRequest.DataSource = IndentList;
                gvIndentRequest.DataBind();
                gvIndentRequest.Visible = true;
                Session["IndentList"] = IndentList;
                clearall();
                txtItemName.Text = "";
                txtItemName.Focus();
                btnsave.Attributes.Remove("disabled");
                txt_unit.Text = "";
                hdntotalavail.Value = "";
            }
            else
            {
                gvIndentRequest.DataSource = null;
                gvIndentRequest.DataBind();
                gvIndentRequest.Visible = true;
            }
        }
        protected void clearall()
        {
            txtItemName.Text = "";
            txtquantity.Text = "";

        }
        protected void gvIndentRequest_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblSerial = (Label)e.Row.FindControl("lblserialID");
                lblSerial.Text = ((gvIndentRequest.PageIndex * gvIndentRequest.PageSize) + e.Row.RowIndex + 1).ToString();
                Label MainAvail = (Label)e.Row.FindControl("lbl_mainavail");
                Label pcAvail = (Label)e.Row.FindControl("lbl_pc");
                TextBox reqQty = (TextBox)e.Row.FindControl("lbl_indentqty");
                if (Convert.ToDecimal(MainAvail.Text == "" ? "0" : MainAvail.Text) > 0 || Convert.ToDecimal(pcAvail.Text == "" ? "0" : pcAvail.Text) < 25)
                {
                    reqQty.Enabled = true;
                }
                else
                {
                    reqQty.Enabled = false;
                }
                if (Convert.ToDecimal(pcAvail.Text == "" ? "0" : pcAvail.Text) == 0)
                {
                    e.Row.Cells[5].BackColor = System.Drawing.Color.Red;
                    pcAvail.ForeColor = System.Drawing.Color.Black;
                }
                if (Convert.ToDecimal(pcAvail.Text == "" ? "0" : pcAvail.Text) > 0 && Convert.ToDecimal(pcAvail.Text == "" ? "0" : pcAvail.Text) <= 25)
                {
                    e.Row.Cells[5].BackColor = System.Drawing.Color.Yellow;
                    pcAvail.ForeColor = System.Drawing.Color.Black;
                }
                if (Convert.ToDecimal(pcAvail.Text == "" ? "0" : pcAvail.Text) > 25)
                {
                    e.Row.Cells[5].BackColor = System.Drawing.Color.Green;
                    pcAvail.ForeColor = System.Drawing.Color.White;
                }
            }

        }
        protected void btn_save_Click(object sender, EventArgs e)
        {
            if (LogData.MedItemRequestEnable == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "ReqSentEnable", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                return;
            }
            else
            {
                lblmessage.Visible = false;
            }
            if (txt_IssuueDate.Text == "")
            {
                Messagealert_.ShowMessage(lblmessage, "IssueDate", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                txt_IssuueDate.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
            }

            List<MedIndentData> ListStock = new List<MedIndentData>();
            MedIndentData objStock = new MedIndentData();
            MedStoreIndentBO objBO = new MedStoreIndentBO();
            try
            {
                int itemcount = 0;
                foreach (GridViewRow row in gvIndentRequest.Rows)
                {
                    IFormatProvider provider = new System.Globalization.CultureInfo("en-GB", true);
                    Label ItemID = (Label)gvIndentRequest.Rows[row.RowIndex].Cells[0].FindControl("lbl_ItemID");
                    TextBox reqdQty = (TextBox)gvIndentRequest.Rows[row.RowIndex].Cells[0].FindControl("lbl_indentqty");
                    Label Unit = (Label)gvIndentRequest.Rows[row.RowIndex].Cells[0].FindControl("lbl_unit");
                    Label SerialID = (Label)gvIndentRequest.Rows[row.RowIndex].Cells[0].FindControl("lblserialID");
                    Label MainAvil = (Label)gvIndentRequest.Rows[row.RowIndex].Cells[0].FindControl("lbl_mainavail");
                    Label Batch = (Label)gvIndentRequest.Rows[row.RowIndex].Cells[0].FindControl("lbl_batchno");
					Label lbl_supplierID = (Label)gvIndentRequest.Rows[row.RowIndex].Cells[0].FindControl("lbl_supplierID");
                    if (Convert.ToInt32(reqdQty.Text == "" ? " 0" : reqdQty.Text) > 0)
                    {
                        if (Convert.ToInt32(reqdQty.Text == "" ? " 0" : reqdQty.Text) > Convert.ToInt32(MainAvil.Text == "" ? " 0" : MainAvil.Text))
                        {
                            Messagealert_.ShowMessage(lblmessage, "ReqQtyNo", 0);
                            divmsg1.Visible = true;
                            divmsg1.Attributes["class"] = "FailAlert";
                            reqdQty.Focus();
                            return;
                        }
                        MedIndentData ObjDetails = new MedIndentData();
                        ObjDetails.MedSubStockID = Convert.ToInt32(ddl_substock.SelectedValue == "" ? "0" : ddl_substock.SelectedValue);
                        ObjDetails.IndentRequestID = Convert.ToInt32(ddl_requestType.SelectedValue == "" ? "0" : ddl_requestType.SelectedValue);
                        ObjDetails.ItemID = Convert.ToInt32(ItemID.Text == "" ? "0" : ItemID.Text);
                        ObjDetails.ReqdQty = Convert.ToInt32(reqdQty.Text == "" ? "0" : reqdQty.Text);
                        ObjDetails.BatchNo = Batch.Text == "" ? "" : Batch.Text;
						ObjDetails.SupplierID = Convert.ToInt32(lbl_supplierID.Text.Trim() == "" ? "0" : lbl_supplierID.Text.Trim());
                        itemcount = itemcount + 1;
                        ListStock.Add(ObjDetails);
                    }
                }
                objStock.XMLData = XmlConvertor.Med_IndentDetailsDatatoXML(ListStock).ToString();
                objStock.TotRequestQty = Convert.ToInt32(txt_totRequestQty.Text == "" ? "0" : txt_totRequestQty.Text);
                objStock.MedSubStockID = Convert.ToInt32(ddl_substock.SelectedValue == "" ? "0" : ddl_substock.SelectedValue);
                objStock.IndentRequestID = Convert.ToInt32(ddl_requestType.SelectedValue == "" ? "0" : ddl_requestType.SelectedValue);
                IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
                DateTime IssDate = txt_IssuueDate.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txt_IssuueDate.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                objStock.IndentRaiseDate = IssDate;
                if (itemcount == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "ReqQty", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                objStock.HospitalID = LogData.HospitalID;
                objStock.RequestedBy = Convert.ToInt64(ddl_userByDepID.SelectedValue == "" ? "0" : ddl_userByDepID.SelectedValue);
                objStock.EmployeeID = LogData.EmployeeID;
                objStock.FinancialYearID = LogData.FinancialYearID;
                objStock.ActionType = Enumaction.Insert;
                List<MedIndentData> result = new List<MedIndentData>();
                result = objBO.UpdateIndentItemDetails(objStock);
                if (result != null)
                {
                    btnsave.Attributes["disabled"] = "disabled";
                    txt_IndentNo.Text = result[0].IndentNo.ToString();
                    lblmessage.Visible = true;
                    Messagealert_.ShowMessage(lblmessage, "save", 1);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "SucessAlert";
                    Session["IndentList"] = null;
                    btnprint.Attributes.Remove("disabled");
                    ddl_requestType.Attributes.Remove("disabled");
                    ddl_requestType.SelectedIndex = 0;

					ScriptManager.RegisterStartupScript(Page, GetType(), "Indent_confirm", "<script>pushMessageToPhar('Indent Request ','PHRApv','2','" + result[0].IndentNo + "');</script>", false);
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
        protected void btnreset_Click(object sender, EventArgs e)
        {
            gvIndentRequest.DataSource = null;
            gvIndentRequest.DataBind();
            gvIndentRequest.Visible = false;
            lblresult.Visible = false;
            ddlexport.SelectedIndex = 0;
            ddlexport.Visible = false;
            btnexport.Visible = false;
            lblmessage.Visible = false;
            lblresult.Visible = false;
            div1.Visible = false;
            divmsg1.Visible = false;
            //ddl_substock.SelectedIndex = 0;
            ddl_requestType.SelectedIndex = 0;
            txtItemName.Text = "";
            btnsave.Attributes["disabled"] = "disabled";
            btnprint.Attributes["disabled"] = "disabled";
            ddl_requestType.Attributes.Remove("disabled");
            txt_IndentNo.Text = "";
            Session["IndentList"] = null;
            txt_totRequestQty.Text = "";
            ddl_requestType.SelectedIndex = 1;
            Session["IndentList"] = null;
            ddl_userByDepID.SelectedValue = LogData.EmployeeID.ToString();
            txtItemName.Focus();

        }
        protected void btnresets_Click(object sender, EventArgs e)
        {
            gvIndentlist.DataSource = null;
            gvIndentlist.DataBind();
            gvIndentlist.Visible = false;
            //ViewState["ID"] = null;
            lblmessage2.Visible = false;
            lblresult1.Visible = false;
            //ddl_substocklist.SelectedIndex = 0;
            ddl_requestTypeList.SelectedIndex = 0;
            ddl_user.SelectedIndex = 0;
            txt_indentList.Text = "";
            txt_from.Text = "";
            txt_To.Text = "";
            txt_IndentNo.Text = "";
            txt_totalReq.Text = "";
            txt_Indentapprv.Text = "";
            txt_TotIndent.Text = "";
            ViewState["TotalReq"] = null;
            ddl_requestTypeList.Attributes.Remove("disabled");
            ddl_requestType.SelectedIndex = 0;
        }
        protected void ddl_IndentStatus_SelectedIndexChanged(object sender, EventArgs e)
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
                    Messagealert_.ShowMessage(lblmessage2, "SearchEnable", 0);
                    divmsg2.Visible = true;
                    divmsg2.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage2.Visible = false;
                }
                if (ddl_substocklist.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage2, "GenStock", 0);
                    divmsg2.Visible = true;
                    divmsg2.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage2.Visible = false;
                }
                if (txt_from.Text != "")
                {
                    if (Commonfunction.isValidDate(txt_from.Text) == false)
                    {
                        Messagealert_.ShowMessage(lblmessage2, "ValidDate", 0);
                        divmsg2.Attributes["class"] = "FailAlert";
                        divmsg2.Visible = true;
                        txt_from.Focus();
                        return;
                    }
                }
                else
                {
                    divmsg2.Visible = false;
                }
                if (txt_To.Text != "")
                {
                    if (Commonfunction.isValidDate(txt_To.Text) == false)
                    {
                        Messagealert_.ShowMessage(lblmessage2, "ValidDate", 0);
                        divmsg2.Attributes["class"] = "FailAlert";
                        divmsg2.Visible = true;
                        txt_To.Focus();
                        return;
                    }
                }
                else
                {
                    divmsg2.Visible = false;
                }
                List<MedIndentData> objdeposit = GetIndentItemList(0);
                if (objdeposit.Count > 0)
                {
                    txt_totalReq.Text = Commonfunction.Getrounding(objdeposit[0].TotRequestQty.ToString());
                    txt_Indentapprv.Text = Commonfunction.Getrounding(objdeposit[0].TotApproved.ToString());
                    gvIndentlist.DataSource = objdeposit;
                    gvIndentlist.DataBind();
                    gvIndentlist.Visible = true;
                    Messagealert_.ShowMessage(lblresult1, "Total:" + objdeposit[0].MaximumRows.ToString() + " Record(s) found.", 1);
                    div3.Attributes["class"] = "SucessAlert";
                    div3.Visible = true;
                    ddlexport.Visible = true;
                    btnexport.Visible = true;
                    lblmessage2.Visible = false;
                    lblmessage.Visible = false;
                    txt_TotIndent.Text = objdeposit[0].MaximumRows.ToString();
                    btn_print.Attributes.Remove("disabled");
                }
                else
                {
                    gvIndentlist.DataSource = null;
                    gvIndentlist.DataBind();
                    gvIndentlist.Visible = true;
                    ddlexport.Visible = false;
                    btnexport.Visible = false;
                    lblresult.Visible = false;
                    div3.Visible = false;
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
        public List<MedIndentData> GetIndentItemList(int curIndex)
        {
            MedIndentData objstock = new MedIndentData();
            MedStoreIndentBO objBO = new MedStoreIndentBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            objstock.IndentNo = txt_indentList.Text.ToString() == "" ? "" : txt_indentList.Text.ToString();
            objstock.MedSubStockID = Convert.ToInt32(ddl_substocklist.SelectedValue == "" ? "0" : ddl_substocklist.SelectedValue);
            objstock.IndentRequestID = Convert.ToInt32(ddl_requestTypeList.SelectedValue == "" ? "0" : ddl_requestTypeList.SelectedValue);
            objstock.ReceivedBy = Convert.ToInt64(ddl_user.SelectedValue == "" ? "0" : ddl_user.SelectedValue); ;
            DateTime from = txt_from.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txt_from.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime to = txt_To.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txt_To.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objstock.DateFrom = from;
            objstock.DateTo = to;
            objstock.IndentStatusID = Convert.ToInt32(ddl_IndentStatus.SelectedValue == "" ? "0" : ddl_IndentStatus.SelectedValue); ;
            return objBO.GetIndentItemList(objstock);
        }
        protected void gvIndentlist_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvIndentlist.PageIndex = e.NewPageIndex;
            bindgrid();
        }
        protected void gvIndentlist_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Deletes")
                {
                    if (LogData.DeleteEnable == 0)
                    {
                        Messagealert_.ShowMessage(lblmessage, "DeleteEnable", 0);
                        div1.Visible = true;
                        div1.Attributes["class"] = "FailAlert";
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                    }
                    MedIndentData objIndentStatusData = new MedIndentData();
                    MedStoreIndentBO objIndentStatusBO = new MedStoreIndentBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = gvIndentlist.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("code");
                    Label indNo = (Label)gr.Cells[0].FindControl("lbl_Indentno");
                    Label qty = (Label)gr.Cells[0].FindControl("lbl_qty");
                    Label IndentState = (Label)gr.Cells[0].FindControl("lblstatus");
                    if (IndentState.Text.Trim() == "Received")
                    {
                        Messagealert_.ShowMessage(lblresult1, "Received", 0);
                        div3.Visible = true;
                        div3.Attributes["class"] = "FailAlert";
                        return;
                    }
                    objIndentStatusData.IndentID = Convert.ToInt64(ID.Text);
                    objIndentStatusData.IndentNo = indNo.Text;
                    objIndentStatusData.EmployeeID = LogData.EmployeeID;
                    objIndentStatusData.ActionType = Enumaction.Delete;
                    TextBox txtremarks = (TextBox)gr.Cells[0].FindControl("txtremarks");
                    txtremarks.Enabled = true;
                    if (txtremarks.Text == "")
                    {
                        Messagealert_.ShowMessage(lblresult1, "Remarks", 0);
                        div3.Visible = true;
                        div3.Attributes["class"] = "FailAlert";
                        txtremarks.Focus();
                        return;
                    }
                    else
                    {
                        objIndentStatusData.Remarks = txtremarks.Text;
                    }
                    MedStoreIndentBO objIndentStatusBO1 = new MedStoreIndentBO();
                    int Result = objIndentStatusBO1.DeleteIndentReqByID(objIndentStatusData);
                    if (Result == 1)
                    {
                        Messagealert_.ShowMessage(lblmessage2, "delete", 1);
                        divmsg2.Visible = true;
                        divmsg2.Attributes["class"] = "SucessAlert";
                        bindgrid();
                    }
                    if (Result == 2)
                    {
                        Messagealert_.ShowMessage(lblmessage2, "DeleteIndent", 0);
                        divmsg2.Visible = true;
                        divmsg2.Attributes["class"] = "FailAlert";
                    }
                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
            }

        }
        protected void gvIndentRequest_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Deletes")
                {
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = gvIndentRequest.Rows[i];
                    List<MedIndentData> ItemList = Session["IndentList"] == null ? new List<MedIndentData>() : (List<MedIndentData>)Session["IndentList"];
                    ItemList.RemoveAt(i);
                    TextBox ReqQty = (TextBox)gvIndentRequest.Rows[i].Cells[2].FindControl("lbl_indentqty");
                    txt_totRequestQty.Text = (Convert.ToInt32(txt_totRequestQty.Text == "" ? "0" : txt_totRequestQty.Text) - Convert.ToInt32(ReqQty.Text == "" ? "0" : ReqQty.Text)).ToString();
                    Session["IndentList"] = ItemList;
                    gvIndentRequest.DataSource = ItemList;
                    gvIndentRequest.DataBind();

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
        protected void gvIndentlist_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label StockStaus = (Label)e.Row.FindControl("lblStID");
                Label Label1 = (Label)e.Row.FindControl("lblstatus");
                if (StockStaus.Text == "1")
                {
                    e.Row.Cells[7].BackColor = System.Drawing.Color.Gray;
                    Label1.ForeColor = System.Drawing.Color.Black;
                }
                if (StockStaus.Text == "2")
                {
                    e.Row.Cells[7].BackColor = System.Drawing.Color.Green;
                    Label1.ForeColor = System.Drawing.Color.White;
                }
                if (StockStaus.Text == "3")
                {
                    e.Row.Cells[7].BackColor = System.Drawing.Color.Red;
                    Label1.ForeColor = System.Drawing.Color.White;
                }
            }
        }
        protected void btnexport_Click(object sender, EventArgs e)
        {
            if (LogData.ExportEnable == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "ExportEnable", 0);
                div1.Visible = true;
                div1.Attributes["class"] = "FailAlert";
                return;
            }
            else
            {
                lblmessage.Visible = false;
            }
            if (ddlexport.SelectedIndex == 1)
            {
                ExportoExcel();
            }
            else
            {
                Messagealert_.ShowMessage(lblresult1, "ExportType", 0);
                div3.Visible = true;
                div3.Attributes["class"] = "FailAlert";
                ddlexport.Focus();
                return;
            }
        }
        protected void ExportoExcel()
        {
            DataTable dt = GetDatafromDatabase();
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "Patient Type Detail List");

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=StoreIndentRequestList.xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
        }
        private DataTable GetDatafromDatabase()
        {
            List<MedIndentData> DepositDetails = GetIndentItemList(0);
            List<IndentDataToExcel> ListexcelData = new List<IndentDataToExcel>();
            int i = 0;
            foreach (MedIndentData row in DepositDetails)
            {
                IndentDataToExcel Ecxeclpat = new IndentDataToExcel();
                Ecxeclpat.IndentNo = DepositDetails[i].IndentNo;
                Ecxeclpat.TotalRqty = DepositDetails[i].TotalRqty;
                Ecxeclpat.IndentRaiseDate = DepositDetails[i].IndentRaiseDate;
                Ecxeclpat.RequestStat = DepositDetails[i].RequestStat;
                Ecxeclpat.IndentStatus = DepositDetails[i].IndentStatus;
                ListexcelData.Add(Ecxeclpat);
                i++;
            }
            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            DataTable dt = converter.ToDataTable(ListexcelData);
            return dt;
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        public class ListtoDataTableConverter
        {
            public DataTable ToDataTable<T>(List<T> items)
            {
                DataTable dataTable = new DataTable(typeof(T).Name);
                //Get all the properties
                PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (PropertyInfo prop in Props)
                {
                    //Setting column names as Property names
                    dataTable.Columns.Add(prop.Name);
                }
                foreach (T item in items)
                {
                    var values = new object[Props.Length];

                    for (int i = 0; i < Props.Length; i++)
                    {
                        //inserting property values to datatable rows
                        values[i] = Props[i].GetValue(item, null);
                    }
                    dataTable.Rows.Add(values);
                }
                //put a breakpoint here and check datatable
                return dataTable;
            }
        }
        protected void btnsearchItems_Click(object sender, EventArgs e)
        {
            MedIndentData objstock = new MedIndentData();
            MedStoreIndentBO objBO = new MedStoreIndentBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            string source = txtItemName.Text;
            bool isUHIDnumeric = source.Substring(source.LastIndexOf(':') + 1).All(char.IsDigit);
            if (source.Contains(":") && isUHIDnumeric == true)
            {
                objstock.ItemID = Convert.ToInt32(source.Substring(source.LastIndexOf(':') + 1));
            }
            else
            {
                objstock.ItemID = 0;
            }
            objstock.MedSubStockID = Convert.ToInt32(ddl_substocklist.SelectedValue == "" ? "0" : ddl_substocklist.SelectedValue);
            objstock.PC = Convert.ToDecimal(txt_availpc.Text == "" ? "0" : txt_availpc.Text);
            List<MedIndentData> result = objBO.GetSubstockItemRequestableList(objstock);
            if (result.Count > 0)
            {
                Messagealert_.ShowMessage(lblresult, "Total:" + result[0].MaximumRows.ToString() + " Record(s) found.", 1);
                div1.Attributes["class"] = "SucessAlert";
                div1.Visible = true;
                List<MedIndentData> ItemList = Session["IndentList"] == null ? new List<MedIndentData>() : (List<MedIndentData>)Session["IndentList"];
                Session["IndentList"] = result;
                gvIndentRequest.DataSource = Session["IndentList"];
                gvIndentRequest.DataBind();
                gvIndentRequest.Visible = true;
                btnsave.Attributes.Remove("disabled");
            }
            else
            {
                lblresult.Visible = false;
                div1.Visible = false;
                Session["IndentList"] = null;
                gvIndentRequest.DataSource = result;
                gvIndentRequest.DataBind();
                btnsave.Attributes["disabled"] = "disabled";
            }
        }
    }
}
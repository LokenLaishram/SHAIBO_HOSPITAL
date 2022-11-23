using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedPharBO;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.LoginData;
using Mediqura.CommonData.MedPharData;
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

namespace Mediqura.Web.MedPhr
{
    public partial class Phr_InterStockTransfer : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Clearall();
                ddlbind();
                lblmessage.Visible = false;
                txt_intertransferNo.Attributes["disabled"] = "disabled";
                string MSID = "0";
                MSID = LogData.MedSubStockID.ToString() == "" ? "0" : LogData.MedSubStockID.ToString();
                if (Convert.ToInt32(ddl_medsubstock.SelectedValue) == LogData.MedSubStockID)
                {
                    ddl_transferto.Items.FindByValue(MSID).Enabled = false;
                }
                else
                {
                    ddl_transferto.Items.FindByValue("0").Enabled = false;
                }
            }
        }
        private void ddlbind()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddl_medsubstock, mstlookup.GetLookupsList(LookupName.SubStockType));
            ddl_medsubstock.SelectedValue = LogData.MedSubStockID.ToString();
            Commonfunction.PopulateDdl(ddl_transferto, mstlookup.GetLookupsList(LookupName.SubStockType));

            //------------TAB 2----------------------------
            Commonfunction.PopulateDdl(ddlfromgenstock, mstlookup.GetLookupsList(LookupName.SubStockType));
            ddlfromgenstock.SelectedValue = LogData.MedSubStockID.ToString();
            Commonfunction.PopulateDdl(ddltogenstock, mstlookup.GetLookupsList(LookupName.SubStockType));
            Commonfunction.PopulateDdl(ddltransferby, mstlookup.GetGenitemRequestedEmployeeByID(Convert.ToInt32(LogData.MedSubStockID)));
            //--------------------------------------------

            if (LogData.RoleID == 1 || LogData.RoleID == 25)
            {
                ddl_medsubstock.Attributes.Remove("disabled");
            }
            else
            {
                ddl_medsubstock.Attributes["disabled"] = "disabled";
            }
            if (ddl_medsubstock.SelectedValue != "")
            {
                AutoCompleteExtender2.ContextKey = ddl_medsubstock.SelectedValue;
            }
        }

        protected void ddl_medsubstock_SelectedIndexChanged(object sender, EventArgs e)
        {
            AutoCompleteExtender2.ContextKey = ddl_medsubstock.SelectedValue;
            txt_itemname.Text = "";
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetItemName(string prefixText, int count, string contextKey)
        {
            Phr_InterStockTransferData Objtranfer = new Phr_InterStockTransferData();
            Phr_InterStockTransferBO ObjtranferBO = new Phr_InterStockTransferBO();
            List<Phr_InterStockTransferData> getResult = new List<Phr_InterStockTransferData>();
            Objtranfer.ItemName = prefixText;
            Objtranfer.MedSubStockID = Convert.ToInt32(contextKey);
            getResult = ObjtranferBO.GetMedSubStockItemName(Objtranfer);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].ItemName.ToString());
            }
            return list;
        }
        protected void txt_itemname_TextChanged(object sender, EventArgs e)
        {
            if (txt_itemname.Text != "" || !txt_itemname.Text.Contains(":"))
            {

                Phr_InterStockTransferData objdata = new Phr_InterStockTransferData();
                Phr_InterStockTransferBO objstdBO = new Phr_InterStockTransferBO();
                List<Phr_InterStockTransferData> List = new List<Phr_InterStockTransferData>();
                var source = txt_itemname.Text.ToString();
                if (source.Contains(":"))
                {
                    objdata.MedSubStockID = Convert.ToInt32(ddl_medsubstock.SelectedValue == "" ? "0" : ddl_medsubstock.SelectedValue);
                    bool isIDnumeric = txt_itemname.Text.Substring(txt_itemname.Text.LastIndexOf(':') + 1).All(char.IsDigit);
                    if (isIDnumeric == true)
                    {
                        objdata.SubStockID = isIDnumeric ? Convert.ToInt64(txt_itemname.Text.Contains(":") ? txt_itemname.Text.Substring(txt_itemname.Text.LastIndexOf(':') + 1) : "0") : 0;
                    }
                    else
                    {
                        objdata.ID = 0;
                        txt_itemname.Text = "";
                        txt_itemname.Focus();
                        return;
                    }
                    List = objstdBO.Get_Phr_ItemDetailsBySubStockNo(objdata);
                    hdnSubStockID.Value = List[0].SubStockID.ToString();
                    hdnItemID.Value = List[0].ItemID.ToString();
                    txt_itemID.Text = List[0].ItemID.ToString();
                    hdnStockNo.Value = List[0].StockNo.ToString();
                    hdnCPperQty.Value = List[0].CPperQty.ToString();
                    hdnMRPperUnit.Value = List[0].MRPperUnit.ToString();
                    hdnMRPperQty.Value = List[0].MRPperQty.ToString();
                    hdnNoUnitBal.Value = List[0].NoUnitBalance.ToString();
                    hdnEquQtyBal.Value = List[0].EquivalentQtyBalance.ToString();
                    txtExpireDays.Text = List[0].ExpireDays.ToString();
                    txt_Avail.Text = List[0].EquivalentQtyBalance.ToString();
                    txt_itemNametoAdd.Text = List[0].ItemName.ToString();
                    txttransferqty.Focus();
                }
                else
                {
                    txt_itemname.Focus();
                    txt_itemNametoAdd.Text = "";
                }

            }
            else
            {
                txt_itemname.Focus();
                txt_itemNametoAdd.Text = "";
            }
        }

        protected void btnadd_Click(object sender, EventArgs e)
        {
            addtolist();
        }
        protected void addtolist()
        {
            if (ddl_medsubstock.SelectedIndex == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "StockType", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                ddl_medsubstock.Focus();
                return;
            }
            if (txt_itemname.Text == "" || !txt_itemname.Text.Contains(":"))
            {
                Messagealert_.ShowMessage(lblmessage, "ItemName", 0);
                txt_itemname.Text = "";
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                txt_itemname.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
            }
            if (txttransferqty.Text == "" || txttransferqty.Text == "0")
            {
                Messagealert_.ShowMessage(lblmessage, "StockQty", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                txttransferqty.Focus();
                return;
            }

            if (Convert.ToInt32(txttransferqty.Text == "" ? "0" : txttransferqty.Text) > Convert.ToInt32(txt_Avail.Text == "" ? "0" : txt_Avail.Text))
            {
                Messagealert_.ShowMessage(lblmessage, "OverStockTransfer", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                txttransferqty.Focus();
                return;
            }
            IFormatProvider provider = new System.Globalization.CultureInfo("en-GB", true);
            List<Phr_InterStockTransferData> StockItemList = Session["IterStockItemList"] == null ? new List<Phr_InterStockTransferData>() : (List<Phr_InterStockTransferData>)Session["IterStockItemList"];
            Phr_InterStockTransferData objTransferStk = new Phr_InterStockTransferData();
            objTransferStk.MedSubStockID = Convert.ToInt32(ddl_medsubstock.SelectedValue == "" ? "0" : ddl_medsubstock.SelectedValue);
            objTransferStk.SubStockID = Convert.ToInt64(hdnSubStockID.Value.Trim() == "" ? "0" : hdnSubStockID.Value.Trim());
            objTransferStk.ItemID = Convert.ToInt32(hdnItemID.Value.Trim() == "" ? "0" : hdnItemID.Value.Trim());
            objTransferStk.ItemName = txt_itemNametoAdd.Text.ToString() == "" ? "" : txt_itemNametoAdd.Text.ToString();
            objTransferStk.StockNo = hdnStockNo.Value.ToString() == "" ? "" : hdnStockNo.Value.ToString();
            objTransferStk.CPperQty = Convert.ToDecimal(hdnCPperQty.Value.ToString() == "" ? "0" : hdnCPperQty.Value.ToString());
            objTransferStk.MRPperUnit = Convert.ToDecimal(hdnMRPperUnit.Value.ToString() == "" ? "0" : hdnMRPperUnit.Value.ToString());
            objTransferStk.MRPperQty = Convert.ToDecimal(hdnMRPperQty.Value.ToString() == "" ? "0" : hdnMRPperQty.Value.ToString());
            objTransferStk.NoUnitBalance = Convert.ToDecimal(hdnNoUnitBal.Value.ToString() == "" ? "0" : hdnNoUnitBal.Value.ToString());
            objTransferStk.EquivalentQtyBalance = Convert.ToInt32(hdnEquQtyBal.Value.ToString() == "" ? "0" : hdnEquQtyBal.Value.ToString());
            objTransferStk.TransferQty = Convert.ToInt32(txttransferqty.Text.ToString() == "" ? "0" : txttransferqty.Text.ToString());

            string SubStockID;
            var source = txt_itemname.Text.ToString();
            if (source.Contains(":"))
            {
                SubStockID = source.Substring(source.LastIndexOf(':') + 1);
                // Check Duplicate data 
                foreach (GridViewRow row in gvPhrStockItemList.Rows)
                {
                    Label SubStkID = (Label)gvPhrStockItemList.Rows[row.RowIndex].Cells[0].FindControl("lblSubstockID");
                    if (SubStockID == SubStkID.Text)
                    {
                        Messagealert_.ShowMessage(lblmessage, "Listcheck", 0);
                        divmsg1.Visible = true;
                        divmsg1.Attributes["class"] = "FailAlert";
                        txt_itemname.Focus();
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                    }
                }
                objTransferStk.SubStockID = Convert.ToInt32(SubStockID);
            }
            else
            {
                txt_itemname.Text = "";
                return;
            }
            StockItemList.Add(objTransferStk);

            if (StockItemList.Count > 0)
            {
                gvPhrStockItemList.DataSource = StockItemList;
                gvPhrStockItemList.DataBind();
                gvPhrStockItemList.Visible = true;
                Session["IterStockItemList"] = StockItemList;
                Clearall();
                btnsave.Attributes.Remove("disabled");
                TotalSum();
                txt_itemname.Focus();
            }
            else
            {
                gvPhrStockItemList.DataSource = null;
                gvPhrStockItemList.DataBind();
                gvPhrStockItemList.Visible = true;
            }
        }
        //Sum of the gridview
        protected void TotalSum()
        {
            int qtytotal = 0;
            foreach (GridViewRow gvr in gvPhrStockItemList.Rows)
            {
                Label UnitQty = (Label)gvr.Cells[0].FindControl("lbl_transferUnit");
                qtytotal = qtytotal + Convert.ToInt32(UnitQty.Text.Trim());

            }
            txt_totaltransferqty.Text = qtytotal.ToString();
        }
        protected void Clearall()
        {
            hdnSubStockID.Value = "";
            hdnStockNo.Value = "";
            txt_itemID.Text = "";
            hdnItemID.Value = "";
            txt_itemname.Text = "";
            hdnCPperQty.Value = "";
            hdnMRPperUnit.Value = "";
            hdnMRPperQty.Value = "";
            hdnNoUnitBal.Value = "";
            hdnEquQtyBal.Value = "";
            txtExpireDays.Text = "";
            txttransferqty.Text = "";
            txt_Avail.Text = "";
            txt_itemNametoAdd.Text = "";
            txt_transferremarks.Text = "";
        }
        protected void gvPhrStockItemList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Deletes")
                {
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = gvPhrStockItemList.Rows[i];
                    List<Phr_InterStockTransferData> ItemList = Session["IterStockItemList"] == null ? new List<Phr_InterStockTransferData>() : (List<Phr_InterStockTransferData>)Session["IterStockItemList"];
                    ItemList.RemoveAt(i);
                    //Label qty = (Label)gr.Cells[0].FindControl("lbl_transferUnit");
                    //txt_totaltransferqty.Text = (Convert.ToInt32(txt_totaltransferqty.Text == "" ? "0" : txt_totaltransferqty.Text) - Convert.ToInt32(qty.Text == "" ? "0" : qty.Text)).ToString();
                    Session["IterStockItemList"] = ItemList;
                    gvPhrStockItemList.DataSource = ItemList;
                    gvPhrStockItemList.DataBind();
                    TotalSum();
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

        protected void txt_Remark_TextChanged(object sender, EventArgs e)
        {
            btnsave.Focus();
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
            if (ddl_medsubstock.SelectedIndex == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "StockType", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                ddl_medsubstock.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
            }
            if (txt_transferremarks.Text == "")
            {
                Messagealert_.ShowMessage(lblmessage, "Remarks", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                txt_transferremarks.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
            }
            if (ddl_transferto.SelectedIndex == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "InterTransferTo", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                ddl_transferto.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
            }
            List<Phr_InterStockTransferData> TransferStockList = new List<Phr_InterStockTransferData>();
            Phr_InterStockTransferData objStock = new Phr_InterStockTransferData();
            Phr_InterStockTransferBO objBO = new Phr_InterStockTransferBO();

            try
            {
                // get all the record from the gridview
                int itemcount = 0;
                foreach (GridViewRow row in gvPhrStockItemList.Rows)
                {
                    Label SubstockID = (Label)gvPhrStockItemList.Rows[row.RowIndex].Cells[0].FindControl("lblSubstockID");
                    Label MedSubStockID = (Label)gvPhrStockItemList.Rows[row.RowIndex].Cells[0].FindControl("lblMedSubStockID");
                    Label StockNo = (Label)gvPhrStockItemList.Rows[row.RowIndex].Cells[0].FindControl("lblStockNo");
                    Label TransferItemID = (Label)gvPhrStockItemList.Rows[row.RowIndex].Cells[0].FindControl("lbl_ItemID");
                    Label CPpQty = (Label)gvPhrStockItemList.Rows[row.RowIndex].Cells[0].FindControl("lblCPperQty");
                    Label MRPpUnit = (Label)gvPhrStockItemList.Rows[row.RowIndex].Cells[0].FindControl("lbl_MRPperUnit");
                    Label MRPpQty = (Label)gvPhrStockItemList.Rows[row.RowIndex].Cells[0].FindControl("lbl_MRPperQty");
                    Label NoUnitBal = (Label)gvPhrStockItemList.Rows[row.RowIndex].Cells[0].FindControl("lbl_NoUnitBalance");
                    Label EquQtyBal = (Label)gvPhrStockItemList.Rows[row.RowIndex].Cells[0].FindControl("lbl_EquQtyBal");
                    Label TransferUnit = (Label)gvPhrStockItemList.Rows[row.RowIndex].Cells[0].FindControl("lbl_transferUnit");

                    Phr_InterStockTransferData ObjDetails = new Phr_InterStockTransferData();
                    ObjDetails.MedSubStockID = Convert.ToInt32(MedSubStockID.Text == "" ? "0" : MedSubStockID.Text);
                    ObjDetails.SubStockID = Convert.ToInt32(SubstockID.Text == "" ? "0" : SubstockID.Text);
                    ObjDetails.StockNo = StockNo.Text.Trim();
                    ObjDetails.ItemID = Convert.ToInt32(TransferItemID.Text == "" ? "0" : TransferItemID.Text);
                    ObjDetails.CPperQty = Convert.ToDecimal(CPpQty.Text == "" ? "0" : CPpQty.Text);
                    ObjDetails.MRPperUnit = Convert.ToDecimal(MRPpUnit.Text == "" ? "0" : MRPpUnit.Text);
                    ObjDetails.MRPperQty = Convert.ToDecimal(MRPpQty.Text == "" ? "0" : MRPpQty.Text);
                    ObjDetails.NoUnitBalance = Convert.ToDecimal(NoUnitBal.Text == "" ? "0" : NoUnitBal.Text);
                    ObjDetails.EquivalentQtyBalance = Convert.ToInt32(EquQtyBal.Text == "" ? "0" : EquQtyBal.Text);
                    ObjDetails.TransferQty = Convert.ToInt32(TransferUnit.Text == "" ? "0" : TransferUnit.Text);
                    itemcount = itemcount + 1;
                    TransferStockList.Add(ObjDetails);
                }
                objStock.XMLData = XmlConvertor.PhrInterStockTransferDatatoXML(TransferStockList).ToString();
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
                objStock.TransferFromMedSubStockID = Convert.ToInt32(ddl_medsubstock.SelectedValue == "" ? "0" : ddl_medsubstock.SelectedValue);
                objStock.TotalTransferQty = Convert.ToInt32(txt_totaltransferqty.Text.Trim() == "" ? "0" : txt_totaltransferqty.Text.Trim());
                objStock.TransferToMedSubStockID = Convert.ToInt32(ddl_transferto.SelectedValue == "" ? "0" : ddl_transferto.SelectedValue);
                objStock.TransferRemarks = txt_transferremarks.Text;
                objStock.HospitalID = LogData.HospitalID;
                objStock.EmployeeID = LogData.EmployeeID;
                objStock.FinancialYearID = LogData.FinancialYearID;
                objStock.ActionType = Enumaction.Insert;
                List<Phr_InterStockTransferData> result = objBO.Update_Phr_InterStockTransferDetails(objStock);
                if (result.Count > 0)
                {
                    txt_intertransferNo.Text = result[0].InterTransferNo.ToString();
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
        protected void btnreset_Click(object sender, EventArgs e)
        {
            gvPhrStockItemList.DataSource = null;
            gvPhrStockItemList.DataBind();
            gvPhrStockItemList.Visible = false;
            lblmessage.Visible = false;
            divmsg1.Visible = false;
            ddl_medsubstock.SelectedValue = LogData.MedSubStockID.ToString();
            hdnSubStockID.Value = "";
            hdnItemID.Value = "";
            txt_itemID.Text = "";
            txt_itemname.Text = "";
            txt_Avail.Text = "";
            txttransferqty.Text = "";
            txt_intertransferNo.Text = "";
            txt_totaltransferqty.Text = "";
            ddl_transferto.SelectedIndex = 0;
            Session["IterStockItemList"] = null;

        }
        ///--------------------Tab2----------------------//
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
                    Messagealert_.ShowMessage(lblmessage1, "SearchEnable", 0);
                    divmsg2.Visible = true;
                    divmsg2.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage1.Visible = false;
                }

                List<Phr_InterStockTransferData> objtransfer = GetInterStockTransferList(0);
                if (objtransfer.Count > 0)
                {

                    gvtransferstocklist.DataSource = objtransfer;
                    gvtransferstocklist.DataBind();
                    gvtransferstocklist.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total:" + objtransfer[0].MaximumRows.ToString() + " Record(s) found.", 1);
                    divmsg3.Attributes["class"] = "SucessAlert";
                    divmsg3.Visible = true;
                    lblmessage1.Visible = false;
                    lblmessage1.Visible = false;

                }
                else
                {
                    gvtransferstocklist.DataSource = null;
                    gvtransferstocklist.DataBind();
                    gvtransferstocklist.Visible = true;
                    lblresult.Visible = false;
                    divmsg3.Visible = false;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage1, "system", 0);
                divmsg2.Attributes["class"] = "FailAlert";
                divmsg2.Visible = true;
            }
        }
        public List<Phr_InterStockTransferData> GetInterStockTransferList(int curIndex)
        {
            Phr_InterStockTransferData objtransferstock = new Phr_InterStockTransferData();
            Phr_InterStockTransferBO objBO = new Phr_InterStockTransferBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime from = txt_from.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txt_from.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime to = txt_To.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txt_To.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objtransferstock.DateFrom = from;
            objtransferstock.DateTo = to;
            objtransferstock.InterTransferNo = txttransferno.Text.Trim().ToString() == "" ? "" : txttransferno.Text.Trim().ToString();
            objtransferstock.TransferFromMedSubStockID = Convert.ToInt32(ddlfromgenstock.SelectedValue == "" ? "0" : ddlfromgenstock.SelectedValue);
            objtransferstock.TransferToMedSubStockID = Convert.ToInt32(ddltogenstock.SelectedValue == "" ? "0" : ddltogenstock.SelectedValue);
            objtransferstock.TransferBy = Convert.ToInt32(ddltransferby.SelectedValue == "" ? "0" : ddltransferby.SelectedValue);
            objtransferstock.IsActive = ddl_Status.SelectedValue == "1" ? true : false;
            return objBO.GetTransferStockItemList(objtransferstock);
        }
        protected void btnclear_Click(object sender, EventArgs e)
        {
            gvtransferstocklist.DataSource = null;
            gvtransferstocklist.DataBind();
            gvtransferstocklist.Visible = false;
            lblresult.Visible = false;
            lblmessage1.Visible = false;
            lblresult.Visible = false;
            divmsg3.Visible = false;
            txt_from.Text = "";
            txt_To.Text = "";
            ddlfromgenstock.SelectedValue = LogData.MedSubStockID.ToString();
            ddltogenstock.SelectedIndex = 0;
            txttransferno.Text = "";
            ddltransferby.SelectedIndex = 0;
            ddl_Status.SelectedIndex = 0;
        }
        protected void gvtransferstocklist_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Deletes")
                {
                    Phr_InterStockTransferData objtransfer = new Phr_InterStockTransferData();
                    Phr_InterStockTransferBO objstktransferBO = new Phr_InterStockTransferBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = gvtransferstocklist.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("lbl_ID");
                    Label TransferQty = (Label)gr.Cells[0].FindControl("lbl_totaltransferqty");
                    Label TransferNo = (Label)gr.Cells[0].FindControl("lbl_transferno");
                    Label TransferFromID = (Label)gr.Cells[0].FindControl("lbl_FromMedSubStockID");
                    Label TransferToID = (Label)gr.Cells[0].FindControl("lbl_ToMedSubStockID");
                    TextBox Remarks = (TextBox)gr.Cells[0].FindControl("lbl_remarks");
                    if (Remarks.Text == "")
                    {
                        Messagealert_.ShowMessage(lblresult, "Remarks", 0);
                        divmsg3.Visible = true;
                        divmsg3.Attributes["class"] = "FailAlert";
                        Remarks.Focus();
                        return;
                    }
                    else
                    {
                        objtransfer.Remarks = Remarks.Text;
                    }
                    objtransfer.ID = Convert.ToInt64(ID.Text);
                    objtransfer.InterTransferNo = TransferNo.Text.Trim();
                    objtransfer.TotalTransferQty = Convert.ToInt32(TransferQty.Text.Trim());
                    objtransfer.TransferFromMedSubStockID = Convert.ToInt32(TransferFromID.Text);
                    objtransfer.TransferToMedSubStockID = Convert.ToInt32(TransferToID.Text);
                    objtransfer.EmployeeID = LogData.EmployeeID;
                    int Result = objstktransferBO.DeleteTranferStockByID(objtransfer);
                    if (Result == 1)
                    {
                        Messagealert_.ShowMessage(lblmessage1, "delete", 1);
                        lblmessage1.Attributes["class"] = "SucessAlert";
                        divmsg1.Visible = true;
                        bindgrid();
                    }
                    else if (Result == 7)
                    {
                        Messagealert_.ShowMessage(lblmessage1, "StockCannotDelete", 0);
                        lblmessage1.Attributes["class"] = "FailAlert";
                        divmsg1.Visible = true;
                    }
                    else
                    {
                        Messagealert_.ShowMessage(lblmessage1, "system", 0);
                        lblmessage1.Attributes["class"] = "FailAlert";
                        divmsg1.Visible = true;
                    }
                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblresult, "system", 0);
                lblmessage1.Attributes["class"] = "FailAlert";
                divmsg1.Visible = true;
            }
        }
    }
}
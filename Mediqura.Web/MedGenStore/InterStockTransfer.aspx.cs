using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedGenStoreBO;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.LoginData;
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

namespace Mediqura.Web.MedGenStore
{
    public partial class InterStockTransfer : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlbind();
                lblmessage.Visible = false;
            }
        }
        private void ddlbind()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddl_substock, mstlookup.GetLookupsList(LookupName.GenStockType));
            ddl_substock.SelectedValue = LogData.GenSubStockID.ToString();
            Commonfunction.PopulateDdl(ddl_transferto, mstlookup.GetLookupsList(LookupName.GenStockType));

            //------------TAB 2----------------------------
            Commonfunction.PopulateDdl(ddlfromgenstock, mstlookup.GetLookupsList(LookupName.GenStockType));
            ddlfromgenstock.SelectedValue = LogData.GenSubStockID.ToString();
            Commonfunction.PopulateDdl(ddltogenstock, mstlookup.GetLookupsList(LookupName.GenStockType));
            Commonfunction.PopulateDdl(ddltransferby, mstlookup.GetGenitemRequestedEmployeeByID(Convert.ToInt32(LogData.GenSubStockID)));
            //--------------------------------------------

            if (LogData.RoleID == 1 || LogData.RoleID == 25)
            {
                ddl_substock.Attributes.Remove("disabled");
            }
            else
            {
                ddl_substock.Attributes["disabled"] = "disabled";
            }
            if (ddl_substock.SelectedValue != "")
            {
                AutoCompleteExtender2.ContextKey = ddl_substock.SelectedValue;
            }
        }

        protected void ddl_substock_SelectedIndexChanged(object sender, EventArgs e)
        {
            AutoCompleteExtender2.ContextKey = ddl_substock.SelectedValue;
            txt_itemname.Text = "";
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetItemName(string prefixText, int count, string contextKey)
        {
            GenInterStockTransferData Objtranfer = new GenInterStockTransferData();
            GenInterStockTransferBO ObjtranferBO = new GenInterStockTransferBO();
            List<GenInterStockTransferData> getResult = new List<GenInterStockTransferData>();
            Objtranfer.ItemName = prefixText;
            Objtranfer.GenStockID = Convert.ToInt32(contextKey);
            getResult = ObjtranferBO.GetSubStockItemName(Objtranfer);
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
                txttransferqty.Focus();
                GenInterStockTransferData objbill = new GenInterStockTransferData();
                GenInterStockTransferBO objstdBO = new GenInterStockTransferBO();
                List<GenInterStockTransferData> List = new List<GenInterStockTransferData>();
                var source = txt_itemname.Text.ToString();
                string ID;
                if (source.Contains(":"))
                {
                    ID = source.Split('#', '|')[1];
                    objbill.ID = Convert.ToInt64(ID == "" ? "0" : ID);
                    objbill.StockNo = txt_itemname.Text.ToString().Substring(txt_itemname.Text.ToString().LastIndexOf(':') + 1);
                    objbill.GenStockID = Convert.ToInt32(ddl_substock.SelectedValue == "" ? "0" : ddl_substock.SelectedValue);
                    List = objstdBO.GetInterItemDetailsByStockNumbers(objbill);
                    hdnID.Value = List[0].ID.ToString();
                    hdnItemID.Value = List[0].ItemID.ToString();
                    txt_itemID.Text = List[0].ItemID.ToString();
                    txt_Avail.Text = List[0].Totavailable.ToString();
                    txt_itemNametoAdd.Text = List[0].ItemName.ToString();
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
        protected void txt_transferqty_TextChanged(object sender, EventArgs e)
        {
            txt_transferremarks.Focus();
        }
        protected void txt_Remark_TextChanged(object sender, EventArgs e)
        {
            addtolist();
        }

        protected void btnadd_Click(object sender, EventArgs e)
        {
            addtolist();
        }
        protected void addtolist()
        {
            if (ddl_substock.SelectedIndex == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "StockType", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                ddl_substock.Focus();
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
            List<GenInterStockTransferData> StockItemList = Session["IterStockItemList"] == null ? new List<GenInterStockTransferData>() : (List<GenInterStockTransferData>)Session["IterStockItemList"];
            GenInterStockTransferData objTransferStk = new GenInterStockTransferData();
            objTransferStk.ID = Convert.ToInt64(hdnID.Value.Trim() == "" ? "0" : hdnID.Value.Trim());
            objTransferStk.GenStockID = Convert.ToInt32(ddl_substock.SelectedValue == "" ? "0" : ddl_substock.SelectedValue);            
            objTransferStk.ItemID = Convert.ToInt32(hdnItemID.Value.Trim() == "" ? "0" : hdnItemID.Value.Trim());
            objTransferStk.ItemName = txt_itemNametoAdd.Text.ToString() == "" ? "" : txt_itemNametoAdd.Text.ToString();
            objTransferStk.TransferQty = Convert.ToInt32(txttransferqty.Text.ToString() == "" ? "" : txttransferqty.Text.ToString());

            string Stocknos;
            var source = txt_itemname.Text.ToString();
            if (source.Contains(":"))
            {
                Stocknos = source.Substring(source.LastIndexOf(':') + 1);
                // Check Duplicate data 
                foreach (GridViewRow row in gvintersubstocklist.Rows)
                {
                    Label StockNo = (Label)gvintersubstocklist.Rows[row.RowIndex].Cells[0].FindControl("lbl_stockNo");
                    if (Stocknos == StockNo.Text)
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
                objTransferStk.StockNo = Stocknos;
            }
            else
            {
                txt_itemname.Text = "";
                return;
            }
                      
            objTransferStk.TransferRemarks = txt_transferremarks.Text;
            StockItemList.Add(objTransferStk);

            if (StockItemList.Count > 0)
            {
                gvintersubstocklist.DataSource = StockItemList;
                gvintersubstocklist.DataBind();
                gvintersubstocklist.Visible = true;
                Session["IterStockItemList"] = StockItemList;
                Clearall();
                txt_itemname.Focus();
                btnsave.Attributes.Remove("disabled");
                TotalSum();
            }
            else
            {
                gvintersubstocklist.DataSource = null;
                gvintersubstocklist.DataBind();
                gvintersubstocklist.Visible = true;
            }
        }
        //Sum of the gridview
        protected void TotalSum()
        {
            int qtytotal = 0;
            foreach (GridViewRow gvr in gvintersubstocklist.Rows)
            {
                Label qty = (Label)gvr.Cells[0].FindControl("lbl_transferqty");
                qtytotal = qtytotal + Convert.ToInt32(qty.Text.Trim());

            }
            txt_totaltransferqty.Text = qtytotal.ToString();
        }
        protected void Clearall()
        {
            txt_itemname.Text = "";
            txttransferqty.Text = "";
            hdnID.Value = "";
            hdnItemID.Value = "";
            txt_itemID.Text = "";
            txt_Avail.Text = "";
            txt_itemNametoAdd.Text = "";
            txt_transferremarks.Text = "";
        }
        protected void gvsubstocklist_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Deletes")
                {
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = gvintersubstocklist.Rows[i];
                    List<GenInterStockTransferData> ItemList = Session["IterStockItemList"] == null ? new List<GenInterStockTransferData>() : (List<GenInterStockTransferData>)Session["IterStockItemList"];
                    ItemList.RemoveAt(i);
                    Label qty = (Label)gr.Cells[0].FindControl("lbl_transferqty");
                    txt_totaltransferqty.Text = (Convert.ToInt32(txt_totaltransferqty.Text == "" ? "0" : txt_totaltransferqty.Text) - Convert.ToInt32(qty.Text == "" ? "0" : qty.Text)).ToString();
                    Session["IterStockItemList"] = ItemList;
                    gvintersubstocklist.DataSource = ItemList;
                    gvintersubstocklist.DataBind();
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
            if (ddl_transferto.SelectedIndex == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "InterTransferTo", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                ddl_transferto.Focus();
                return;
            }
            List<GenInterStockTransferData> TransferStockList = new List<GenInterStockTransferData>();
            GenInterStockTransferData objStock = new GenInterStockTransferData();
            GenInterStockTransferBO objBO = new GenInterStockTransferBO();

            try
            {
                // get all the record from the gridview
                int itemcount = 0;
                foreach (GridViewRow row in gvintersubstocklist.Rows)
                {
                    Label ID = (Label)gvintersubstocklist.Rows[row.RowIndex].Cells[0].FindControl("lblID");
                    Label TransferItemID = (Label)gvintersubstocklist.Rows[row.RowIndex].Cells[0].FindControl("lbl_ItemID");
                    Label GenStockID = (Label)gvintersubstocklist.Rows[row.RowIndex].Cells[0].FindControl("lbl_GenstockID");
                    Label StockNumber = (Label)gvintersubstocklist.Rows[row.RowIndex].Cells[0].FindControl("lbl_StockNo");
                    Label TransferQty = (Label)gvintersubstocklist.Rows[row.RowIndex].Cells[0].FindControl("lbl_transferqty");
                    Label lbl_TransferRemaarks = (Label)gvintersubstocklist.Rows[row.RowIndex].Cells[0].FindControl("lbl_transferremark");
                    GenInterStockTransferData ObjDetails = new GenInterStockTransferData();
                    ObjDetails.ID = Convert.ToInt32(ID.Text == "" ? "0" : ID.Text);
                    ObjDetails.ItemID = Convert.ToInt32(TransferItemID.Text == "" ? "0" : TransferItemID.Text);
                    ObjDetails.GenStockID = Convert.ToInt32(GenStockID.Text == "" ? "0" : GenStockID.Text);
                    ObjDetails.TransferQty = Convert.ToInt32(TransferQty.Text == "" ? "0" : TransferQty.Text);
                    ObjDetails.StockNo = StockNumber.Text.Trim();
                    ObjDetails.TransferRemarks = lbl_TransferRemaarks.Text;
                    itemcount = itemcount + 1;
                    TransferStockList.Add(ObjDetails);
                }
                objStock.XMLData = XmlConvertor.InterStockTransferDatatoXML(TransferStockList).ToString();
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
                objStock.GenStockID = Convert.ToInt32(ddl_substock.SelectedValue == "" ? "0" : ddl_substock.SelectedValue);
                objStock.TotalTransferQty = Convert.ToInt32(txt_totaltransferqty.Text.Trim() == "" ? "0" : txt_totaltransferqty.Text.Trim());
                objStock.TransferToGenStockID = Convert.ToInt32(ddl_transferto.SelectedValue == "" ? "0" : ddl_transferto.SelectedValue);
                objStock.HospitalID = LogData.HospitalID;
                objStock.EmployeeID = LogData.EmployeeID;
                objStock.FinancialYearID = LogData.FinancialYearID;
                objStock.ActionType = Enumaction.Insert;
                List<GenInterStockTransferData> result = objBO.UpdateInterStockTransferDetails(objStock);
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
            gvintersubstocklist.DataSource = null;
            gvintersubstocklist.DataBind();
            gvintersubstocklist.Visible = false;
            lblmessage.Visible = false;
            divmsg1.Visible = false;
            ddl_substock.SelectedValue = LogData.GenSubStockID.ToString();
            hdnID.Value = "";
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

                List<GenInterStockTransferData> objtransfer = GetInterStockTransferList(0);
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
        public List<GenInterStockTransferData> GetInterStockTransferList(int curIndex)
        {
            GenInterStockTransferData objtransferstock = new GenInterStockTransferData();
            GenInterStockTransferBO objBO = new GenInterStockTransferBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime from = txt_from.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txt_from.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime to = txt_To.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txt_To.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objtransferstock.DateFrom = from;
            objtransferstock.DateTo = to;
            objtransferstock.InterTransferNo = txttransferno.Text.Trim().ToString() == "" ? "" : txttransferno.Text.Trim().ToString();
            objtransferstock.TransferFromGenStockID = Convert.ToInt32(ddlfromgenstock.SelectedValue == "" ? "0" : ddlfromgenstock.SelectedValue);
            objtransferstock.TransferToGenStockID = Convert.ToInt32(ddltogenstock.SelectedValue == "" ? "0" : ddltogenstock.SelectedValue);
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
            ddlfromgenstock.SelectedValue = LogData.GenSubStockID.ToString();
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
                    GenInterStockTransferData objtransfer = new GenInterStockTransferData();
                    GenInterStockTransferBO objstktransferBO = new GenInterStockTransferBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = gvtransferstocklist.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("lbl_ID");
                    Label TransferQty = (Label)gr.Cells[0].FindControl("lbl_totaltransferqty");
                    Label TransferNo = (Label)gr.Cells[0].FindControl("lbl_transferno");
                    Label TransferFomID = (Label)gr.Cells[0].FindControl("lbl_FromGenStockID");
                    Label TransferTOID = (Label)gr.Cells[0].FindControl("lbl_TogenStockID");
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
                    objtransfer.TransferFromGenStockID = Convert.ToInt32(TransferFomID.Text);
                    objtransfer.TransferToGenStockID = Convert.ToInt32(TransferTOID.Text);
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
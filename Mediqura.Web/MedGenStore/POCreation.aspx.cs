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
using Mediqura.BOL.MedGenStoreBO;

namespace Mediqura.Web.MedGenStore
{
    public partial class POCreation : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindddl();
                txt_totalusable.Text = "0";
                txt_totexpiry.Text = "0";
                txt_totalusable.Text = "0";
                txt_totalcondemned.Text = "0";
                txt_totexpiry.Text = "0";
                Session["SelectedItemList"] = null;
                btnprints.Attributes["disabled"] = "disabled";
                btn_send.Attributes["disabled"] = "disabled";

            }
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetItemName(string prefixText, int count, string contextKey)
        {
            GENStrData Objpaic = new GENStrData();
            GenStoreBO objInfoBO = new GenStoreBO();
            List<GENStrData> getResult = new List<GENStrData>();
            Objpaic.ItemName = prefixText;
            getResult = objInfoBO.GetItemName(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].ItemName.ToString());
            }
            return list;
        }
        protected void ddlstoretype_SelectedIndexChanged(object sender, EventArgs e)
        {
            AutoCompleteExtender4.ContextKey = ddlstoretype.SelectedValue;
            txt_itemName.Text = "";
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetItemNameWithID(string prefixText, int count, string contextKey)
        {
            GENStrData Objpaic = new GENStrData();
            GenStoreBO objInfoBO = new GenStoreBO();
            List<GENStrData> getResult = new List<GENStrData>();
            Objpaic.ItemName = prefixText;
            getResult = objInfoBO.GetItemNameWithID(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].ItemName.ToString());
            }
            return list;
        }

        private void bindddl()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddlstoretype, mstlookup.GetLookupsList(LookupName.Groups));
            ddlstoretype.SelectedIndex = 3;
            ddlstoretype.Attributes["disabled"] = "disabled";
        }
        protected void txt_itemName_TextChanged(object sender, EventArgs e)
        {
            bindgrid();
        }
        protected void txt_qtyreqd_TextChanged(object sender, EventArgs e)
        {
            txt_totcp.Text = "0.00";
            txt_totqtyreqd.Text = "0";
            GridViewRow currentrow = (GridViewRow)((TextBox)sender).Parent.Parent;
            foreach (GridViewRow row in gvitemselectedlist.Rows)
            {
                TextBox qty = (TextBox)gvitemselectedlist.Rows[row.RowIndex].Cells[0].FindControl("txt_qtyreqd");
                Label CPperQty = (Label)gvitemselectedlist.Rows[row.RowIndex].Cells[0].FindControl("lbl_cpperqty");
                Label CP = (Label)gvitemselectedlist.Rows[row.RowIndex].Cells[0].FindControl("lbl_cp");
                CP.Text = ((Convert.ToDecimal(CPperQty.Text)) * (Convert.ToInt32(qty.Text))).ToString();
                txt_totcp.Text = (Convert.ToDecimal(txt_totcp.Text) + Convert.ToDecimal(CP.Text)).ToString();
                txt_totqtyreqd.Text = (Convert.ToInt32(txt_totqtyreqd.Text) + Convert.ToInt32(qty.Text)).ToString();
                qty.Focus();
            }
        }
        protected void gvitemchecklist_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            foreach (GridViewRow row in gvitemchecklist.Rows)
            {
                CheckBox cb = (CheckBox)gvitemchecklist.Rows[row.RowIndex].Cells[0].FindControl("chekboxselect"); //find the CheckBox
                cb.Checked = false;
            }

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
                List<GENStrData> objdeposit = GetItemCheckList(0);
                if (objdeposit.Count > 0)
                {
                    gvitemchecklist.DataSource = objdeposit;
                    gvitemchecklist.DataBind();
                    gvitemchecklist.Visible = true;
                    txt_totalusable.Text = "0";
                    txt_totexpiry.Text = "0";
                    txt_totalcondemned.Text = "0";
                    foreach (GridViewRow row in gvitemchecklist.Rows)
                    {
                        Label Avail = (Label)gvitemchecklist.Rows[row.RowIndex].Cells[0].FindControl("lbl_avail");
                        Label Condemend = (Label)gvitemchecklist.Rows[row.RowIndex].Cells[0].FindControl("lbl_condmqty");
                        Label ExpDate = (Label)gvitemchecklist.Rows[row.RowIndex].Cells[0].FindControl("lbl_expdate");
                        txt_totalusable.Text = (Convert.ToInt32(txt_totalusable.Text) + Convert.ToInt32(Avail.Text)).ToString();
                        txt_totalcondemned.Text = (Convert.ToInt32(txt_totalcondemned.Text) + Convert.ToInt32(Condemend.Text)).ToString();
                        //if (Convert.ToDateTime(ExpDate.Text) == DateTime.Today)
                        //{
                        //    txt_totexpiry.Text = (Convert.ToInt32(txt_totexpiry.Text) + Convert.ToInt32(Avail.Text)).ToString();
                        //}
                    }
                    txt_itemName.Text = "";
                }
                else
                {
                    gvitemchecklist.DataSource = null;
                    gvitemchecklist.DataBind();
                    gvitemchecklist.Visible = true;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
                divmsg1.Visible = true;
            }
        }
        public List<GENStrData> GetItemCheckList(int curIndex)
        {
            GENStrData objstock = new GENStrData();
            POCreationBO objBO = new POCreationBO();
            string ID;
            var source = txt_itemName.Text.ToString();
            if (source.Contains(":"))
            {
                ID = source.Substring(source.LastIndexOf(':') + 1);
                objstock.ItemID = Convert.ToInt64(ID);
            }
            return objBO.GetItemCheckList(objstock);
        }
        protected void btnaccept_Click(object sender, EventArgs e)
        {
            if (gvitemchecklist.Rows.Count == 0)
            {
                if (txt_itemName.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "ItemName", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                txt_totalusable.Text = "0";
                txt_totalcondemned.Text = "0";
                txt_totexpiry.Text = "0";
                GENStrData objstock = new GENStrData();
                GenStoreBO objBO = new GenStoreBO();
                IFormatProvider provider1 = new System.Globalization.CultureInfo("en-GB", true);
                List<GENStrData> SelectedItemList = Session["SelectedItemList"] == null ? new List<GENStrData>() : (List<GENStrData>)Session["SelectedItemList"];
                string ID;
                var source = txt_itemName.Text.ToString();
                if (source.Contains(":"))
                {
                    ID = source.Substring(source.LastIndexOf(':') + 1);
                    objstock.ItemID = Convert.ToInt64(ID);
                }
                else
                {
                    Messagealert_.ShowMessage(lblmessage, "Please enter the item in the master page first.", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    return;
                }
                List<GENStrData> getResult = new List<GENStrData>();
                getResult = objBO.Get_ItemNameByID(objstock);
                if (getResult.Count > 0)
                {
                    objstock.ItemName = getResult[0].ItemName.ToString();
                }
                SelectedItemList.Add(objstock);
                if (SelectedItemList.Count > 0)
                {
                    gvitemselectedlist.DataSource = SelectedItemList;
                    gvitemselectedlist.DataBind();
                    gvitemselectedlist.Visible = true;
                    Session["SelectedItemList"] = SelectedItemList;
                    txt_itemName.Text = "";
                    Messagealert_.ShowMessage(lblmessage, "Successfully added to the Item Checklist.", 1);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "SucessAlert";
                    btn_send.Attributes.Remove("disabled");
                    txt_totusable.Text = "0";
                    txt_totexp.Text = "0";
                    txt_totcondemned.Text = "0";
                    txt_totcp.Text = "0.00";
                    txt_totqtyreqd.Text = "0";

                    foreach (GridViewRow row1 in gvitemselectedlist.Rows)
                    {
                        Label Avail = (Label)gvitemselectedlist.Rows[row1.RowIndex].Cells[0].FindControl("lbl_avail");
                        Label Condemend = (Label)gvitemselectedlist.Rows[row1.RowIndex].Cells[0].FindControl("lbl_condmqty");
                        Label ExpDate = (Label)gvitemselectedlist.Rows[row1.RowIndex].Cells[0].FindControl("lbl_expdate");
                        Label CP = (Label)gvitemselectedlist.Rows[row1.RowIndex].Cells[0].FindControl("lbl_cp");
                        TextBox totqty = (TextBox)gvitemselectedlist.Rows[row1.RowIndex].Cells[0].FindControl("txt_qtyreqd");
                        totqty.Text = "0";
                        txt_totcp.Text = (Convert.ToDecimal(txt_totcp.Text) + Convert.ToDecimal(CP.Text)).ToString();
                        txt_totusable.Text = (Convert.ToInt32(txt_totusable.Text) + Convert.ToInt32(Avail.Text)).ToString();
                        txt_totcondemned.Text = (Convert.ToInt32(txt_totcondemned.Text) + Convert.ToInt32(Condemend.Text)).ToString();
                        txt_totqtyreqd.Text = (Convert.ToInt32(txt_totqtyreqd.Text) + Convert.ToInt32(totqty.Text)).ToString();
                        if (Convert.ToDateTime(ExpDate.Text) == DateTime.Today)
                        {
                            txt_totexp.Text = (Convert.ToInt32(txt_totexp.Text) + Convert.ToInt32(Avail.Text)).ToString();
                        }
                    }
                    return;

                }
                else
                {
                    gvitemselectedlist.DataSource = null;
                    gvitemselectedlist.DataBind();
                    gvitemselectedlist.Visible = true;
                    txt_itemName.Text = "";
                }
            }


            else
            {
                foreach (GridViewRow row in gvitemchecklist.Rows)
                {
                    IFormatProvider provider = new System.Globalization.CultureInfo("en-GB", true);
                    List<GENStrData> SelectedItemList = Session["SelectedItemList"] == null ? new List<GENStrData>() : (List<GENStrData>)Session["SelectedItemList"];

                    CheckBox cb = (CheckBox)gvitemchecklist.Rows[row.RowIndex].Cells[0].FindControl("chekboxselect"); //find the CheckBox
                    if (cb.Checked)
                    {
                        Label StockNo = (Label)gvitemchecklist.Rows[row.RowIndex].Cells[0].FindControl("lblstockno");
                        Label ItemName = (Label)gvitemchecklist.Rows[row.RowIndex].Cells[0].FindControl("lblitemname");
                        Label CPperQty = (Label)gvitemchecklist.Rows[row.RowIndex].Cells[0].FindControl("lbl_cpperqty");
                        Label ItemID = (Label)gvitemchecklist.Rows[row.RowIndex].Cells[0].FindControl("lbl_ItemID");
                        Label CP = (Label)gvitemchecklist.Rows[row.RowIndex].Cells[0].FindControl("lbl_cp");
                        Label Supplier = (Label)gvitemchecklist.Rows[row.RowIndex].Cells[0].FindControl("lbl_supplier");
                        Label SupplierID = (Label)gvitemchecklist.Rows[row.RowIndex].Cells[0].FindControl("lbl_supplierID");
                        Label PO = (Label)gvitemchecklist.Rows[row.RowIndex].Cells[0].FindControl("lbl_PO");
                        Label Tax = (Label)gvitemchecklist.Rows[row.RowIndex].Cells[0].FindControl("lbl_tax");
                        Label Avail = (Label)gvitemchecklist.Rows[row.RowIndex].Cells[0].FindControl("lbl_avail");
                        Label TotalRecdQty = (Label)gvitemchecklist.Rows[row.RowIndex].Cells[0].FindControl("lbl_totrecdqty");
                        Label Condemned = (Label)gvitemchecklist.Rows[row.RowIndex].Cells[0].FindControl("lbl_condmqty");
                        Label ExpDate = (Label)gvitemchecklist.Rows[row.RowIndex].Cells[0].FindControl("lbl_expdate");
                        Label Stock_ID = (Label)gvitemchecklist.Rows[row.RowIndex].Cells[0].FindControl("lbl_stockID");

                        GENStrData obj = new GENStrData();
                        obj.StockNo = StockNo.Text;
                        obj.StockID = Convert.ToInt32(Stock_ID.Text);
                        obj.TotalRecdQty = Convert.ToInt32(TotalRecdQty.Text);
                        obj.CPPerQty = Convert.ToDecimal(CPperQty.Text);
                        obj.ItemName = (ItemName.Text);
                        obj.ItemID = Convert.ToInt64(ItemID.Text);
                        obj.SupplierID = Convert.ToInt64(SupplierID.Text);
                        IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
                        DateTime date = ExpDate.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(ExpDate.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                        obj.ExpDate = Convert.ToDateTime(date);
                        obj.CP = Convert.ToDecimal(CPperQty.Text) * Convert.ToInt32(Avail.Text);
                        obj.Supplier = Supplier.Text;
                        obj.TotalCondemned = Convert.ToInt32(Condemned.Text);
                        obj.BalStock = Convert.ToInt32(Avail.Text);
                        obj.Tax = Convert.ToDouble(Tax.Text);
                        foreach (GridViewRow row1 in gvitemselectedlist.Rows)
                        {
                            Label StockID = (Label)gvitemselectedlist.Rows[row1.RowIndex].Cells[0].FindControl("lblstockID");
                            Label Item_ID = (Label)gvitemselectedlist.Rows[row1.RowIndex].Cells[0].FindControl("lbl_ItemID");

                            if ((Convert.ToInt64(StockID.Text) == Convert.ToInt64(obj.StockID)) && (Convert.ToInt64(Item_ID.Text) == Convert.ToInt64(obj.ItemID)))
                            {
                                Messagealert_.ShowMessage(lblmessage, "Listcheck", 0);
                                divmsg1.Visible = true;
                                divmsg1.Attributes["class"] = "FailAlert";
                                return;
                            }
                            else
                            {
                                lblmessage.Visible = false;
                            }
                        }
                        SelectedItemList.Add(obj);
                    }
                    if (SelectedItemList.Count > 0)
                    {
                        gvitemselectedlist.DataSource = SelectedItemList;
                        gvitemselectedlist.DataBind();
                        gvitemselectedlist.Visible = true;
                        Session["SelectedItemList"] = SelectedItemList;
                        Messagealert_.ShowMessage(lblmessage, "Successfully added to the Item Checklist.", 1);
                        divmsg1.Visible = true;
                        divmsg1.Attributes["class"] = "SucessAlert";
                        btn_send.Attributes.Remove("disabled");
                        txt_totusable.Text = "0";
                        txt_totexp.Text = "0";
                        txt_totcondemned.Text = "0";
                        txt_totcp.Text = "0.00";
                        txt_totqtyreqd.Text = "0";
                        foreach (GridViewRow row1 in gvitemselectedlist.Rows)
                        {
                            Label Avail = (Label)gvitemselectedlist.Rows[row1.RowIndex].Cells[0].FindControl("lbl_avail");
                            Label Condemend = (Label)gvitemselectedlist.Rows[row1.RowIndex].Cells[0].FindControl("lbl_condmqty");
                            Label ExpDate = (Label)gvitemselectedlist.Rows[row1.RowIndex].Cells[0].FindControl("lbl_expdate");
                            Label CP = (Label)gvitemselectedlist.Rows[row1.RowIndex].Cells[0].FindControl("lbl_cp");
                            TextBox totqty = (TextBox)gvitemselectedlist.Rows[row1.RowIndex].Cells[0].FindControl("txt_qtyreqd");
                            totqty.Text = "0";
                            txt_totcp.Text = (Convert.ToDecimal(txt_totcp.Text) + Convert.ToDecimal(CP.Text)).ToString();
                            txt_totusable.Text = (Convert.ToInt32(txt_totusable.Text) + Convert.ToInt32(Avail.Text)).ToString();
                            txt_totcondemned.Text = (Convert.ToInt32(txt_totcondemned.Text) + Convert.ToInt32(Condemend.Text)).ToString();
                            txt_totqtyreqd.Text = (Convert.ToInt32(txt_totqtyreqd.Text) + Convert.ToInt32(totqty.Text)).ToString();
                            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
                            DateTime date = ExpDate.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(ExpDate.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);

                            if (Convert.ToDateTime(date) == DateTime.Today)
                            {
                                txt_totexp.Text = (Convert.ToInt32(txt_totexp.Text) + Convert.ToInt32(Avail.Text)).ToString();
                            }
                        }
                        cb.Checked = false;
                    }
                    else
                    {
                        gvitemselectedlist.DataSource = null;
                        gvitemselectedlist.DataBind();
                        gvitemselectedlist.Visible = true;

                    }
                }
            }

        }
        protected void btnreset_Click(object sender, EventArgs e)
        {
            txt_itemName.Text = "";
            txt_totalcondemned.Text = "";
            txt_totalusable.Text = "";
            txt_totexpiry.Text = "";
            txt_totcondemned.Text = "";
            txt_totexp.Text = "";
            txt_totusable.Text = "";
            txt_totcp.Text = "";
            txt_totqtyreqd.Text = "";
            txt_totcp.Text = "";
            txt_AppovalListNo.Text = "";
            Session["SelectedItemList"] = null;
            gvitemchecklist.DataSource = null;
            gvitemchecklist.DataBind();
            gvitemchecklist.Visible = false;
            gvitemselectedlist.DataSource = null;
            gvitemselectedlist.DataBind();
            gvitemselectedlist.Visible = false;
            lblmessage.Visible = false;
            divmsg1.Visible = false;
            btn_send.Attributes["disabled"] = "disabled";
            Commonfunction.Insertzeroitemindex(ddlstoretype);
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddlstoretype, mstlookup.GetLookupsList(LookupName.Groups));
            ddlstoretype.SelectedIndex = 3;

        }
        protected void gvitemselectedlist_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Deletes")
                {
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = gvitemselectedlist.Rows[i];
                    List<GENStrData> SelectedItemList = Session["SelectedItemList"] == null ? new List<GENStrData>() : (List<GENStrData>)Session["SelectedItemList"];

                    Label Avail = (Label)gvitemselectedlist.Rows[i].Cells[0].FindControl("lbl_avail");
                    Label Condemned = (Label)gvitemselectedlist.Rows[i].Cells[0].FindControl("lbl_condmqty");
                    Label ExpDate = (Label)gvitemselectedlist.Rows[i].Cells[0].FindControl("lbl_expdate");
                    Label CP = (Label)gvitemselectedlist.Rows[i].Cells[0].FindControl("lbl_cp");
                    TextBox qtyreqd = (TextBox)gvitemselectedlist.Rows[i].Cells[0].FindControl("txt_qtyreqd");

                    SelectedItemList.RemoveAt(i);
                    txt_totcp.Text = (Convert.ToDecimal(txt_totcp.Text) - Convert.ToDecimal(CP.Text)).ToString();
                    txt_totusable.Text = (Convert.ToInt32(txt_totusable.Text) - Convert.ToInt32(Avail.Text)).ToString();
                    txt_totcondemned.Text = (Convert.ToInt32(txt_totcondemned.Text) - Convert.ToInt32(Condemned.Text)).ToString();
                    txt_totqtyreqd.Text = (Convert.ToInt32(txt_totqtyreqd.Text) - Convert.ToInt32(qtyreqd.Text)).ToString();
                    IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
                    DateTime date = ExpDate.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(ExpDate.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);

                    if (Convert.ToDateTime(date) == DateTime.Today)
                    {
                        txt_totexp.Text = (Convert.ToInt32(txt_totexp.Text) - Convert.ToInt32(Avail.Text)).ToString();
                    }
                    Session["SelectedItemList"] = SelectedItemList;
                    gvitemselectedlist.DataSource = SelectedItemList;
                    gvitemselectedlist.DataBind();
                    gvitemselectedlist.Visible = true;
                    lblmessage.Visible = false;
                    divmsg1.Visible = false;

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
        protected void btn_send_Click(object sender, EventArgs e)
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
            if (ddl_purchasemethod.SelectedValue == "0")
            {
                Messagealert_.ShowMessage(lblmessage, "Please select purchase method.", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                return;
            }
            else
            {
                lblmessage.Visible = false;
            }
            List<GENStrData> List = new List<GENStrData>();
            POCreationBO objBO = new POCreationBO();
            GENStrData objrec = new GENStrData();
            try
            {
                // get all the record from the gridview
                foreach (GridViewRow row in gvitemselectedlist.Rows)
                {
                    IFormatProvider provider = new System.Globalization.CultureInfo("en-GB", true);
                    Label CPperQty = (Label)gvitemselectedlist.Rows[row.RowIndex].Cells[0].FindControl("lbl_cpperqty");
                    Label ItemID = (Label)gvitemselectedlist.Rows[row.RowIndex].Cells[0].FindControl("lbl_ItemID");
                    Label CP = (Label)gvitemselectedlist.Rows[row.RowIndex].Cells[0].FindControl("lbl_cp");
                    Label SupplierID = (Label)gvitemselectedlist.Rows[row.RowIndex].Cells[0].FindControl("lbl_supplierID");
                    Label Tax = (Label)gvitemselectedlist.Rows[row.RowIndex].Cells[0].FindControl("lbl_tax");
                    Label Avail = (Label)gvitemselectedlist.Rows[row.RowIndex].Cells[0].FindControl("lbl_avail");
                    Label Condemned = (Label)gvitemselectedlist.Rows[row.RowIndex].Cells[0].FindControl("lbl_condmqty");
                    TextBox Qty = (TextBox)gvitemselectedlist.Rows[row.RowIndex].Cells[0].FindControl("txt_qtyreqd");
                    if (Convert.ToInt32(Qty.Text) == 0)
                    {
                        Messagealert_.ShowMessage(lblmessage, "ReqdQty", 0);
                        divmsg1.Visible = true;
                        divmsg1.Attributes["class"] = "FailAlert";
                        Qty.Focus();
                        return;
                    }
                    GENStrData obj = new GENStrData();
                    obj.CPPerQty = Convert.ToDecimal(CPperQty.Text);
                    obj.ItemID = Convert.ToInt32(ItemID.Text);
                    obj.SupplierID = Convert.ToInt64(SupplierID.Text);
                    obj.CP = Convert.ToDecimal(CPperQty.Text) * Convert.ToInt32(Qty.Text);
                    obj.TotalCondemned = Convert.ToInt32(Condemned.Text);
                    obj.BalStock = Convert.ToInt32(Avail.Text);
                    obj.Tax = Convert.ToDouble(Tax.Text);
                    obj.TotalQuantity = Convert.ToInt32(Qty.Text);
                    List.Add(obj);
                }
                objrec.XMLData = XmlConvertor.ItemCheckListRecordDatatoXML(List).ToString();
                objrec.TotalUsable = Convert.ToInt32(txt_totusable.Text == "" ? "0" : txt_totusable.Text);
                objrec.TotalCondemnedQty = Convert.ToInt32(txt_totcondemned.Text == "" ? "0" : txt_totcondemned.Text);
                objrec.TotalExpiry = Convert.ToInt32(txt_totexp.Text == "" ? "0" : txt_totexp.Text);
                objrec.TotalQuantity = Convert.ToInt32(txt_totqtyreqd.Text == "" ? "0" : txt_totqtyreqd.Text);
                objrec.CP = Convert.ToDecimal(txt_totcp.Text == "" ? "0.00" : txt_totcp.Text);
                objrec.PuchaseMethodID = Convert.ToInt32(ddl_purchasemethod.SelectedValue == "" ? "0" : ddl_purchasemethod.SelectedValue);
                objrec.FinancialYearID = LogData.FinancialYearID;
                objrec.EmployeeID = LogData.EmployeeID;
                objrec.HospitalID = LogData.HospitalID;
                objrec.IPaddress = LogData.IPaddress;
                objrec.ActionType = Enumaction.Insert;

                int result = objBO.UpdatePurchaseCheckItemList(objrec);
                if (result > 0)
                {
                    txt_AppovalListNo.Text = result.ToString();
                    Messagealert_.ShowMessage(lblmessage, "send", 1);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "SucessAlert";
                    gvitemselectedlist.DataSource = null;
                    gvitemselectedlist.DataBind();
                    gvitemselectedlist.Visible = false;
                    Session["SelectedItemList"] = null;
                    btn_send.Attributes["disabled"] = "disabled";

                }
                else
                {
                    Messagealert_.ShowMessage(lblmessage, "Error", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                }

            }
            catch (Exception ex)
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                lblmessage.Text = ExceptionMessage.GetMessage(ex);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
            }
        }
        protected void btnsearch_Click(object sender, EventArgs e)
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
                if (LogData.SearchEnable == 0)
                {
                    Messagealert_.ShowMessage(lblmessage1, "SearchEnable", 0);
                    divmsg2.Visible = true;
                    divmsg2.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txt_retdatefrom.Text != "")
                {
                    if (Commonfunction.isValidDate(txt_retdatefrom.Text) == false)
                    {
                        Messagealert_.ShowMessage(lblmessage1, "VaildDatefrom", 0);
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
                        Messagealert_.ShowMessage(lblmessage1, "VaildDateto", 0);
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
                

                List<GENStrData> objdeposit = GetItemSelectedList(0);
                if (objdeposit.Count > 0)
                {
                    gvpurchaseitemlist.DataSource = objdeposit;
                    gvpurchaseitemlist.DataBind();
                    gvpurchaseitemlist.Visible = true;
                    Messagealert_.ShowMessage(lblresult1, "Total:" + objdeposit[0].MaximumRows.ToString() + " Record(s) found", 1);
                    div2.Attributes["class"] = "SucessAlert";
                    div2.Visible = true;
                    ddlexport.Visible = true;
                    btnexport.Visible = true;
                     btnprints.Attributes.Remove("disabled");
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
                    gvpurchaseitemlist.DataSource = null;
                    gvpurchaseitemlist.DataBind();
                    gvpurchaseitemlist.Visible = true;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
                divmsg1.Visible = true;
            }
        }
        public List<GENStrData> GetItemSelectedList(int curIndex)
        {
            GENStrData objstock = new GENStrData();
            POCreationBO objBO = new POCreationBO();
            objstock.ItemName = txtitemName.Text.Trim() == "" ? "" : txtitemName.Text.Trim();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime from = txt_retdatefrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txt_retdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime to = txt_returndateTo.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txt_returndateTo.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objstock.DateFrom = from;
            objstock.DateTo = to;
            return objBO.GetItemList(objstock);
        }
        protected void btnresets_Click(object sender, EventArgs e)
        {
            txtitemName.Text = "";
            txt_retdatefrom.Text = "";
            txt_returndateTo.Text = "";
            lblresult1.Visible = false;
            div2.Visible = false;
            gvpurchaseitemlist.DataSource = null;
            gvpurchaseitemlist.DataBind();
            gvpurchaseitemlist.Visible = false;
            lblmessage1.Visible = false;
            divmsg2.Visible = false;
            ddlexport.Visible = false;
            btnexport.Visible = false;
            btnprints.Attributes["disabled"] = "disabled";

        }
        protected DataTable GetDatafromDatabase()
        {
            List<GENStrData> DepositDetails = GetItemSelectedList(0);
            List<ItemListDataTOeXCEL> ListexcelData = new List<ItemListDataTOeXCEL>();
            int i = 0;
            foreach (GENStrData row in DepositDetails)
            {
                ItemListDataTOeXCEL Ecxeclpat = new ItemListDataTOeXCEL();
                Ecxeclpat.ApprovalListNo = DepositDetails[i].ApprovalListNo;
                Ecxeclpat.ItemName = DepositDetails[i].ItemName;
                Ecxeclpat.Supplier = DepositDetails[i].Supplier;
                Ecxeclpat.BalStock = DepositDetails[i].BalStock;
                Ecxeclpat.Tax = DepositDetails[i].Tax;
                Ecxeclpat.CP = DepositDetails[i].CP;
                Ecxeclpat.CPPerQty = DepositDetails[i].CPPerQty;
                ListexcelData.Add(Ecxeclpat);
                i++;
            }
            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            DataTable dt = converter.ToDataTable(ListexcelData);
            return dt;
        }
        protected void gvpurchaseitemlist_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Deletes")
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
                        lblmessage1.Visible = false;
                    }
                    GENStrData objbill = new GENStrData();
                    POCreationBO objstdBO = new POCreationBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = gvpurchaseitemlist.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("lbl_ID");
                    Label itemname = (Label)gr.Cells[0].FindControl("lblitemname");
                    Label approvallistno = (Label)gr.Cells[0].FindControl("lbl_approvallistno");
                    Label cp = (Label)gr.Cells[0].FindControl("lbl_cp");
                    Label Avail = (Label)gr.Cells[0].FindControl("lbl_avail");

                    TextBox txtremarks = (TextBox)gr.Cells[0].FindControl("txtremarks");
                    txtremarks.Enabled = true;
                    if (txtremarks.Text == "")
                    {
                        Messagealert_.ShowMessage(lblresult1, "Remarks", 0);
                        div2.Attributes["class"] = "FailAlert";
                        txtremarks.Focus();
                        return;
                    }
                    else
                    {
                        objbill.Remarks = txtremarks.Text;
                    }
                    objbill.ID = Convert.ToInt64(ID.Text);
                    objbill.ItemName = itemname.Text;
                    objbill.ApprovalListNo = approvallistno.Text;
                    objbill.BalStock = Convert.ToInt32(Avail.Text);
                    objbill.CP = Convert.ToDecimal(cp.Text);


                    objbill.EmployeeID = LogData.EmployeeID;

                    int Result = objstdBO.DeleteItemListByID(objbill);
                    if (Result == 1)
                    {
                        bindgrid1();
                        Messagealert_.ShowMessage(lblmessage1, "delete", 1);
                        divmsg2.Attributes["class"] = "SucessAlert";
                        divmsg2.Visible = true;
                    }
                    else
                    {
                        Messagealert_.ShowMessage(lblmessage1, "system", 0);
                        divmsg2.Attributes["class"] = "FailAlert";
                        divmsg2.Visible = true;
                    }

                }

            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage1, "system", 0);
            }
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
                Messagealert_.ShowMessage(lblresult1, "ExportType", 0);
                divmsg2.Attributes["class"] = "FailAlert";
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
                    gvpurchaseitemlist.BorderStyle = BorderStyle.None;
                    //Hide the Column containing CheckBox
                    gvpurchaseitemlist.Columns[9].Visible = false;
                    gvpurchaseitemlist.Columns[10].Visible = false;

                    gvpurchaseitemlist.RenderControl(hw);
                    gvpurchaseitemlist.HeaderRow.Style.Add("width", "15%");
                    gvpurchaseitemlist.HeaderRow.Style.Add("font-size", "10px");
                    gvpurchaseitemlist.Style.Add("text-decoration", "none");
                    gvpurchaseitemlist.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    gvpurchaseitemlist.Style.Add("font-size", "8px");
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 7f, 7f, 7f, 0f);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=ItemCheckList.pdf");
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
                wb.Worksheets.Add(dt, "Item CheckList");
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=ItemCheckList.xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                    ddlexport.SelectedIndex = 0;
                }
                Messagealert_.ShowMessage(lblresult1, "Exported", 1);
                divmsg2.Attributes["class"] = "SucessAlert";
            }
        }
        protected void gvpurchaseitemlist_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvpurchaseitemlist.PageIndex = e.NewPageIndex;
            bindgrid1();
        }
        protected void txtitemName_TextChanged(object sender, EventArgs e)
        {
            if (txtitemName.Text == "")
            {
                Messagealert_.ShowMessage(lblmessage1, "ItemName", 0);
                divmsg2.Visible = true;
                divmsg2.Attributes["class"] = "FailAlert";
                return;
            }
            else
            {
                bindgrid1();
            }
        }
    }
}


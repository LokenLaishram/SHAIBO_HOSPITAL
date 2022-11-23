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
using Mediqura.CommonData.MedHrData;
using Mediqura.BOL.MedHrBO;
using Mediqura.BOL.MedGenStoreBO;

namespace Mediqura.Web.MedGenStore
{
    public partial class GenStoreGRN : BasePage
    {
        int total, total1, total2, total9, total10, total11;
        decimal total3, total4, total5, total6, total7, total8, total12, total13;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txt_totalcp.Attributes.Add("readonly", "readonly");
                txt_taxamount.Attributes.Add("readonly", "readonly");
                txt_cp.Attributes.Add("readonly", "readonly");
                txt_totalreceivedqty.Attributes.Add("readonly", "readonly");
                txt_totalmrp.Attributes.Add("readonly", "readonly");
                txt_CpberforeTax.Attributes.Add("readonly", "readonly");
                txt_totalCPbeforeDisc.Attributes.Add("readonly", "readonly");
                bindddl();
            }
        }
        private void bindddl()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddl_receivedby, mstlookup.GetLookupsList(LookupName.StoreEmp));
            Commonfunction.PopulateDdl(ddl_stockrecievedby, mstlookup.GetLookupsList(LookupName.StoreEmp));
            Commonfunction.PopulateDdl(ddlgroup, mstlookup.GetLookupsList(LookupName.GenGroups));
            Commonfunction.PopulateDdl(ddl_itemgroup, mstlookup.GetLookupsList(LookupName.GenGroups));
            Commonfunction.Insertzeroitemindex(ddl_subgroup);
            btnsave.Attributes["disabled"] = "disabled";
            //Commonfunction.PopulateDdl(ddl_rackgrp, mstlookup.GetLookupsList(LookupName.GEN_Rack));
            //Commonfunction.Insertzeroitemindex(ddl_racksubgrp);
            Commonfunction.PopulateDdl(ddl_seacrhrackgrp, mstlookup.GetLookupsList(LookupName.GEN_Rack));
            Commonfunction.Insertzeroitemindex(ddl_searchracksubgrp);
            hdn_total_free_item_Amount.Value = null;
            Session["StockItemList"] = null;
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetCompanyName(string prefixText, int count, string contextKey)
        {
            GENStrData Objpaic = new GENStrData();
            GenStoreBO objInfoBO = new GenStoreBO();
            List<GENStrData> getResult = new List<GENStrData>();
            Objpaic.CompanyName = prefixText;
            getResult = objInfoBO.GetCompanyName(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].CompanyName.ToString());
            }
            return list;
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetSupplierName(string prefixText, int count, string contextKey)
        {
            GENStrData Objpaic = new GENStrData();
            GenStoreBO objInfoBO = new GenStoreBO();
            List<GENStrData> getResult = new List<GENStrData>();
            Objpaic.SupplierName = prefixText;
            getResult = objInfoBO.GetSupplierName(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].SupplierName.ToString());
            }
            return list;
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetItemName(string prefixText, int count, string contextKey)
        {
            GENStrData Objpaic = new GENStrData();
            GenStoreBO objInfoBO = new GenStoreBO();
            List<GENStrData> getResult = new List<GENStrData>();
            Objpaic.ItemName = prefixText;
            Objpaic.GroupID = Convert.ToInt32(contextKey);
            getResult = objInfoBO.GetItemNameWithID(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].ItemName.ToString());
            }
            return list;
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetPONo(string prefixText, int count, string contextKey)
        {
            GENStrData Objpaic = new GENStrData();
            POApprovalBO objInfoBO = new POApprovalBO();
            List<GENStrData> getResult = new List<GENStrData>();
            Objpaic.PONo = prefixText;
            getResult = objInfoBO.GetautoPONo(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].PONo.ToString());
            }
            return list;
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetItemNames(string prefixText, int count, string contextKey)
        {
            GENStrData Objpaic = new GENStrData();
            GenStoreBO objInfoBO = new GenStoreBO();
            List<GENStrData> getResult = new List<GENStrData>();
            Objpaic.ItemName = prefixText;
            Objpaic.GroupID = Convert.ToInt32(contextKey);
            Objpaic.SubGroupID = Convert.ToInt32(count);
            getResult = objInfoBO.GetItemNames(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].ItemName.ToString());
            }
            return list;
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetEmployeeNames(string prefixText, int count, string contextKey)
        {
            EmployeeData Objpaic = new EmployeeData();
            EmployeeBO objInfoBO = new EmployeeBO();
            List<EmployeeData> getResult = new List<EmployeeData>();
            Objpaic.EmpName = prefixText;
            getResult = objInfoBO.GetEmployeeName(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].EmpName.ToString());
            }
            return list;
        }

        protected void ddl_itemgroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            AutoCompleteExtender6.ContextKey = ddl_itemgroup.SelectedValue;
            if (ddl_itemgroup.SelectedIndex > 0)
            {
                MasterLookupBO mstlookup = new MasterLookupBO();
                Commonfunction.PopulateDdl(ddl_subgroup, mstlookup.GetItemSubGroupByItemGroupID(Convert.ToInt32(ddl_itemgroup.SelectedValue)));
            }
        }
        protected void ddl_itemsubgroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            AutoCompleteExtender6.CompletionSetCount = Convert.ToInt32(ddl_subgroup.SelectedValue == "" ? "0" : ddl_subgroup.SelectedValue);
        }
        protected void txt_itemname_TextChanged(object sender, EventArgs e)
        {
           // txt_batchno.Text = "";
            txt_receiveddate.Text = "";
            txt_company.Text = "";
            txt_supplier.Text = "";
            txt_mfgdate.Text = "";
            txt_expdate.Text = "";
            txt_qty.Text = "";
            txt_totalreceivedqty.Text = "";
            txt_cp.Text = "";
            txt_totalmrp.Text = "";
            txt_mrp.Text = "";
            txt_freeqty.Text = "";
            txt_tax.Text = "";
            txt_totalcp.Text = "";
            GENStrData objStock = new GENStrData();
            GenStoreBO objInfoBO = new GenStoreBO();
            if (txt_itemname.Text != "")
            {
                string ID;
                var source = txt_itemname.Text.ToString();
                if (source.Contains(":"))
                {
                    ID = source.Substring(source.LastIndexOf(':') + 1);
                    List<GENStrData> getResult = new List<GENStrData>();
                    objStock.ItemID = Convert.ToInt32(ID);
                    getResult = objInfoBO.GetUnitByItemID(objStock);
                    txt_Unit.Text = getResult[0].Unit.ToString();
                }
            }

        }
        protected void txt_no0funit_TextChanged(object sender, EventArgs e)
        {
            txt_qty.Text = "";
            txt_totalreceivedqty.Text = "";
            txt_cp.Text = "";
            txt_totalmrp.Text = "";
            txt_mrp.Text = "";
            txt_freeqty.Text = "";
            txt_tax.Text = "";
            txt_totalcp.Text = "";
            txt_qty.Focus();

        }
        protected void btnadd_Click(object sender, EventArgs e)
        {
            if (ddl_purchagetype.SelectedIndex == 0)
            {

                Messagealert_.ShowMessage(lblmessage, "Purchagetype", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                ddl_purchagetype.Focus();
                return;
            }
            else
            {
                if (txt_receivedno.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "RCNO", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    txt_receivedno.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (ddl_purchagetype.SelectedIndex == 1)
                {
                    if (txt_PONo.Text == "")
                    {
                        Messagealert_.ShowMessage(lblmessage, "PO", 0);
                        divmsg1.Visible = true;
                        divmsg1.Attributes["class"] = "FailAlert";
                        txt_PONo.Focus();
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                    }
                }
                else
                {
                    lblmessage.Visible = false;
                }
            }

            if (ddlgroup.SelectedIndex == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "Group", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                ddl_purchagetype.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
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

            if (txt_receiveddate.Text == "")
            {
                Messagealert_.ShowMessage(lblmessage, "RecivedDate", 0);
                divmsg1.Attributes["class"] = "FailAlert";
                divmsg1.Visible = true;
                txt_receiveddate.Focus();
                return;
            }
            else
            {
                if (Commonfunction.isValidDate(txt_receiveddate.Text) == false || Commonfunction.CheckOverDate(txt_receiveddate.Text) == true)
                {
                    Messagealert_.ShowMessage(lblmessage, "ValidDate", 0);
                    divmsg1.Attributes["class"] = "FailAlert";
                    divmsg1.Visible = true;
                    txt_receiveddate.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
            }

            if (txt_mfgdate.Text == "")
            {
                Messagealert_.ShowMessage(lblmessage, "MfgDate", 0);
                divmsg1.Attributes["class"] = "FailAlert";
                divmsg1.Visible = true;
                txt_mfgdate.Focus();
                return;
            }
            else
            {
                if (Commonfunction.isValidDate(txt_mfgdate.Text) == false || Commonfunction.CheckOverDate(txt_mfgdate.Text) == true)
                {
                    Messagealert_.ShowMessage(lblmessage, "ValidDate", 0);
                    txt_mfgdate.Text = "";
                    divmsg1.Attributes["class"] = "FailAlert";
                    divmsg1.Visible = true;
                    txt_mfgdate.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
            }
            if (txt_expdate.Text != "")
            {
                if (Commonfunction.isValidDate(txt_expdate.Text) == false || Commonfunction.ChecklowerDate(txt_expdate.Text) == true)
                {
                    Messagealert_.ShowMessage(lblmessage, "ValidDate", 0);
                    divmsg1.Attributes["class"] = "FailAlert";
                    divmsg1.Visible = true;
                    txt_expdate.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
            }
            if (txt_company.Text == "")
            {
                Messagealert_.ShowMessage(lblmessage, "mfgcompany", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                txt_company.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
            }
            if (txt_supplier.Text == "")
            {
                Messagealert_.ShowMessage(lblmessage, "supplier", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                txt_supplier.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
            }
            if (txt_qty.Text == "0" || txt_qty.Text == "")
            {
                Messagealert_.ShowMessage(lblmessage, "Unitperqty", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                txt_qty.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
                divmsg1.Visible = false;
            }
            if (txt_totalreceivedqty.Text == "0" || txt_totalreceivedqty.Text == "")
            {
                Messagealert_.ShowMessage(lblmessage, "Quantity", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                txt_totalreceivedqty.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
                divmsg1.Visible = false;
            }
            if (txt_totalcp.Text == "0.0" || txt_totalcp.Text == "")
            {
                Messagealert_.ShowMessage(lblmessage, "Costprice", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                txt_totalcp.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
                divmsg1.Visible = false;
            }

            if (txt_tax.Text == "0.0" || txt_tax.Text == "" || Convert.ToDecimal(txt_tax.Text == "" ? "0.0" : txt_tax.Text) > 100)
            {
                txt_tax.Text = "";
                Messagealert_.ShowMessage(lblmessage, "Tax", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                txt_tax.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
                divmsg1.Visible = false;
            }
            //if (ddl_rackgrp.SelectedIndex == 0)
            //{

            //    Messagealert_.ShowMessage(lblmessage, "SRack", 0);
            //    divmsg1.Visible = true;
            //    divmsg1.Attributes["class"] = "FailAlert";
            //    ddl_purchagetype.Focus();
            //    return;
            //}
            //else
            //{
            //    lblmessage.Visible = false;
            //    divmsg1.Visible = false;
            //}
            //if (ddl_racksubgrp.SelectedIndex == 0)
            //{
            //    Messagealert_.ShowMessage(lblmessage, "SubRack", 0);
            //    divmsg1.Visible = true;
            //    divmsg1.Attributes["class"] = "FailAlert";
            //    ddl_purchagetype.Focus();
            //    return;
            //}
            //else
            //{
            //    lblmessage.Visible = false;
            //    divmsg1.Visible = false;
            //}
            IFormatProvider provider = new System.Globalization.CultureInfo("en-GB", true);
            List<GENStrData> StockItemList = Session["StockItemList"] == null ? new List<GENStrData>() : (List<GENStrData>)Session["StockItemList"];
            GENStrData objStock = new GENStrData();
            objStock.ReceiptNo = txt_receivedno.Text.ToString() == "" ? "" : txt_receivedno.Text.ToString();
            objStock.BatchNo = txt_batchno.Text.ToString() == "" ? "" : txt_batchno.Text.ToString();
            objStock.PONo = txt_PONo.Text.ToString() == "" ? "" : txt_PONo.Text.ToString();
            objStock.ItemName = txt_itemname.Text.ToString() == "" ? "" : txt_itemname.Text.ToString();
            string ID;
            var source = txt_itemname.Text.ToString();
            if (source.Contains(":"))
            {
                ID = source.Substring(source.LastIndexOf(':') + 1);

                // Check Duplicate data 
                foreach (GridViewRow row in gvstocklist.Rows)
                {
                    Label ItemID = (Label)gvstocklist.Rows[row.RowIndex].Cells[0].FindControl("lbl_ItemID");

                    if (Convert.ToInt32(ItemID.Text) == Convert.ToInt32(ID))
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
            }
            else
            {
                txt_itemname.Text = "";
                return;
            }
            objStock.ItemID = Convert.ToInt32(ID);
            //objStock.NoOfUnit = Convert.ToInt32(txt_no0funit.Text == "" ? "0" : txt_no0funit.Text);
            objStock.QtyperUnit = Convert.ToInt32(txt_qty.Text == "" ? "0" : txt_qty.Text);
            objStock.CPperunit = Convert.ToDecimal(txt_cp.Text == "" ? "0" : txt_cp.Text);
            objStock.TotalCPbeforeTax = Convert.ToDecimal(txt_CpberforeTax.Text == "" ? "0" : txt_CpberforeTax.Text);
            objStock.CP = Convert.ToDecimal(txt_totalcp.Text == "" ? "0" : txt_totalcp.Text);
            objStock.FreeQuantity = Convert.ToInt32(txt_freeqty.Text == "" ? "0" : txt_freeqty.Text) * Convert.ToInt32(txt_qty.Text == "" ? "0" : txt_qty.Text);
            objStock.MRPperunit = Convert.ToDecimal(txt_mrp.Text == "" ? "0" : txt_mrp.Text);
            objStock.TotalMRP = Convert.ToDecimal(txt_totalmrp.Text == "" ? "0" : txt_totalmrp.Text);
            objStock.Tax = Convert.ToDouble(txt_tax.Text == "" ? "0" : txt_tax.Text);
            objStock.TotalRecdQty = Convert.ToInt32(txt_totalreceivedqty.Text == "" ? "0" : txt_totalreceivedqty.Text);
            var source1 = txt_company.Text.ToString();
            if (source1.Contains(":"))
            {
                string ID1 = source1.Substring(source1.LastIndexOf(':') + 1);
                objStock.CompanyID = Convert.ToInt32(ID1);
            }
            var source2 = txt_supplier.Text.ToString();
            if (source2.Contains(":"))
            {
                string ID2 = source2.Substring(source2.LastIndexOf(':') + 1);
                objStock.SupplierID = Convert.ToInt32(ID2);
            }
            else
            {
                txt_supplier.Text = "";
            }
            objStock.FreeItemAmount = Convert.ToInt32(txt_freeqty.Text == "" ? "0" : txt_freeqty.Text) * Convert.ToDecimal(txt_cp.Text == "" ? "0" : txt_cp.Text);
            objStock.TotalQuantity = Convert.ToInt32(txt_qty.Text == "" ? "0" : txt_qty.Text);
            objStock.ReceivedBy = LogData.EmployeeID;
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime ReceivedDate = txt_receiveddate.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txt_receiveddate.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime MfgDate = txt_mfgdate.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txt_mfgdate.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime ExpDate = txt_expdate.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txt_expdate.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objStock.ReceivedDate = ReceivedDate;
            objStock.MfgDate = MfgDate;
            objStock.ExpDate = ExpDate;
            hdn_total_free_item_Amount.Value = "0";
            hdn_total_free_item_Amount.Value = Commonfunction.Getrounding((Convert.ToDecimal(hdn_total_free_item_Amount.Value == null ? "0.0" : hdn_total_free_item_Amount.Value) + Convert.ToInt32(txt_freeqty.Text == "" ? "0" : txt_freeqty.Text) * Convert.ToDecimal(txt_cp.Text == "" ? "0" : txt_cp.Text)).ToString());
            txt_recivedqty.Text = Commonfunction.Getrounding((Convert.ToDecimal(txt_recivedqty.Text == "" ? "0" : txt_recivedqty.Text) + Convert.ToInt32(txt_qty.Text == "" ? "0" : txt_qty.Text)).ToString());
            txt_totalfreeqty.Text = Commonfunction.Getrounding((Convert.ToDecimal(txt_totalfreeqty.Text == "" ? "0" : txt_totalfreeqty.Text) + Convert.ToInt32(txt_freeqty.Text == "" ? "0" : txt_freeqty.Text) * Convert.ToInt32(txt_qty.Text == "" ? "0" : txt_qty.Text)).ToString());
            txt_total_recvqty.Text = Commonfunction.Getrounding((Convert.ToDecimal(txt_total_recvqty.Text == "" ? "0" : txt_total_recvqty.Text) + Convert.ToDecimal(txt_totalreceivedqty.Text == "" ? "0" : txt_totalreceivedqty.Text)).ToString());
            txt_totalamount.Text = Commonfunction.Getrounding((Convert.ToDecimal(txt_totalamount.Text == "" ? "0" : txt_totalamount.Text) + Convert.ToDecimal(txt_totalcp.Text == "" ? "0" : txt_totalcp.Text)).ToString());
            txt_totalMRPS.Text = Commonfunction.Getrounding((Convert.ToDecimal(txt_totalMRPS.Text == "" ? "0" : txt_totalMRPS.Text) + Convert.ToDecimal(txt_totalmrp.Text == "" ? "0" : txt_totalmrp.Text)).ToString());
            //objStock.RackGroup = Convert.ToInt32(ddl_rackgrp.SelectedValue == "" ? "0" : ddl_rackgrp.SelectedValue);
            //objStock.RackSubGroup = Convert.ToInt32(ddl_racksubgrp.SelectedValue == "" ? "0" : ddl_racksubgrp.SelectedValue);
            //objStock.RackDetail = ddl_rackgrp.SelectedItem.Text + ", " + ddl_racksubgrp.SelectedItem.Text;
            objStock.RatePerQty = Convert.ToDecimal(txt_rateperqty.Text == "" ? "0" : txt_rateperqty.Text);
            objStock.DiscountType = Convert.ToInt32(ddl_discountype.SelectedValue == "" ? "0" : ddl_discountype.SelectedValue);
            objStock.DiscountPerQty = Convert.ToDecimal(txt_discountperqty.Text == "" ? "0" : txt_discountperqty.Text);
            objStock.TotalCPBeforeDisc = Convert.ToDecimal(txt_totalCPbeforeDisc.Text == "" ? "0" : txt_totalCPbeforeDisc.Text);
            objStock.Temperature =txt_temp.Text.Trim();
            StockItemList.Add(objStock);
            if (StockItemList.Count > 0)
            {
                gvstocklist.DataSource = StockItemList;
                gvstocklist.DataBind();
                gvstocklist.Visible = true;
                Session["StockItemList"] = StockItemList;
                clearall();
                txt_itemname.Focus();
                btnsave.Attributes.Remove("disabled");
            }
            else
            {
                gvstocklist.DataSource = null;
                gvstocklist.DataBind();
                gvstocklist.Visible = true;
            }
        }
        protected void gvstocklist_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblSerial = (Label)e.Row.FindControl("lblserialID");
                lblSerial.Text = ((gvstocklist.PageIndex * gvstocklist.PageSize) + e.Row.RowIndex + 1).ToString();
            }
        }
        protected void gvstocklist_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Deletes")
                {
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = gvstocklist.Rows[i];
                    List<GENStrData> ItemList = Session["StockItemList"] == null ? new List<GENStrData>() : (List<GENStrData>)Session["StockItemList"];
                    Decimal totalmrp = ItemList[i].TotalMRP;
                    Decimal totalcp = ItemList[i].CP;
                    Decimal recvdqty = ItemList[i].TotalQuantity;
                    Decimal freeqty = ItemList[i].FreeQuantity;
                    Decimal totalrecvdqty = ItemList[i].TotalRecdQty;
                    Decimal FreeItemAmount = ItemList[i].FreeItemAmount;
                    hdn_total_free_item_Amount.Value = Commonfunction.Getrounding((Convert.ToDecimal(hdn_total_free_item_Amount.Value == null ? "0.0" : hdn_total_free_item_Amount.Value) - FreeItemAmount).ToString());
                    txt_recivedqty.Text = Commonfunction.Getrounding((Convert.ToDecimal(txt_recivedqty.Text == "" ? "0" : txt_recivedqty.Text) - recvdqty).ToString());
                    txt_totalfreeqty.Text = Commonfunction.Getrounding((Convert.ToDecimal(txt_totalfreeqty.Text == "" ? "0" : txt_recivedqty.Text) - freeqty).ToString());
                    txt_total_recvqty.Text = Commonfunction.Getrounding((Convert.ToDecimal(txt_total_recvqty.Text == "" ? "0" : txt_recivedqty.Text) - totalrecvdqty).ToString());

                    txt_totalamount.Text = Commonfunction.Getrounding((Convert.ToDecimal(txt_totalamount.Text == "" ? "0" : txt_totalamount.Text) - totalcp).ToString());
                    txt_totalMRPS.Text = Commonfunction.Getrounding((Convert.ToDecimal(txt_totalMRPS.Text == "" ? "0" : txt_totalMRPS.Text) - totalmrp).ToString());
                    ItemList.RemoveAt(i);
                    Session["StockItemList"] = ItemList;
                    gvstocklist.DataSource = ItemList;
                    gvstocklist.DataBind();

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
            if (ddl_purchagetype.SelectedIndex == 0)
            {

                Messagealert_.ShowMessage(lblmessage, "Purchagetype", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                ddl_purchagetype.Focus();
                return;
            }
            else
            {
                if (txt_receivedno.Text == "")
                {
                    Messagealert_.ShowMessage(lblmessage, "RCNO", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    txt_receivedno.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (ddl_purchagetype.SelectedIndex == 1)
                {
                    if (txt_PONo.Text == "")
                    {
                        Messagealert_.ShowMessage(lblmessage, "PO", 0);
                        divmsg1.Visible = true;
                        divmsg1.Attributes["class"] = "FailAlert";
                        txt_PONo.Focus();
                        return;
                    }
                    else
                    {
                        lblmessage.Visible = false;
                    }
                }
                else
                {
                    lblmessage.Visible = false;
                }
            }
            if (ddl_purchagetype.SelectedIndex == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "Purchagetype", 0);
                divmsg1.Visible = true;
                divmsg1.Attributes["class"] = "FailAlert";
                return;
            }
            else
            {
                lblmessage.Visible = false;
            }

            List<GENStrData> ListStock = new List<GENStrData>();
            GENStrData objStock = new GENStrData();
            GenStoreBO objBO = new GenStoreBO();

            try
            {
                // get all the record from the gridview
                int itemcount = 0;
                foreach (GridViewRow row in gvstocklist.Rows)
                {
                    IFormatProvider provider = new System.Globalization.CultureInfo("en-GB", true);
                    Label ReceiptNo = (Label)gvstocklist.Rows[row.RowIndex].Cells[0].FindControl("lbl_receiptNo");
                    Label BatchNo = (Label)gvstocklist.Rows[row.RowIndex].Cells[0].FindControl("lbl_batchno");
                    Label PONo = (Label)gvstocklist.Rows[row.RowIndex].Cells[0].FindControl("lbl_PONos");
                    Label RecdDate = (Label)gvstocklist.Rows[row.RowIndex].Cells[0].FindControl("lbl_ReceivedDate");
                    Label MfgDate = (Label)gvstocklist.Rows[row.RowIndex].Cells[0].FindControl("lbl_MfgDate");
                    Label ExpDate = (Label)gvstocklist.Rows[row.RowIndex].Cells[0].FindControl("lbl_ExpDate");
                    Label ItemID = (Label)gvstocklist.Rows[row.RowIndex].Cells[0].FindControl("lbl_ItemID");
                    Label CompanyID = (Label)gvstocklist.Rows[row.RowIndex].Cells[0].FindControl("lbl_CompanyID");
                    Label SupplierID = (Label)gvstocklist.Rows[row.RowIndex].Cells[0].FindControl("lbl_SupplierID");
                    Label NoOfUnit = (Label)gvstocklist.Rows[row.RowIndex].Cells[0].FindControl("lbl_NoOfUnit");
                    Label QtyperUnit = (Label)gvstocklist.Rows[row.RowIndex].Cells[0].FindControl("lbl_QtyperUnit");
                    Label CPperunit = (Label)gvstocklist.Rows[row.RowIndex].Cells[0].FindControl("lbl_CPperunit");
                    Label CP = (Label)gvstocklist.Rows[row.RowIndex].Cells[0].FindControl("lbl_cp");
                    Label MRPperunit = (Label)gvstocklist.Rows[row.RowIndex].Cells[0].FindControl("lbl_MRPperunit");
                    Label MRP = (Label)gvstocklist.Rows[row.RowIndex].Cells[0].FindControl("lbl_totalmrp");
                    Label Tax = (Label)gvstocklist.Rows[row.RowIndex].Cells[0].FindControl("lbl_tax");
                    Label TotalRecdQty = (Label)gvstocklist.Rows[row.RowIndex].Cells[0].FindControl("lbl_totalrecdquantity");
                    Label FreeQuantity = (Label)gvstocklist.Rows[row.RowIndex].Cells[0].FindControl("lbl_freequantity");
                    Label RecvQty = (Label)gvstocklist.Rows[row.RowIndex].Cells[0].FindControl("lbl_totalquantity");
                    Label SerialID = (Label)gvstocklist.Rows[row.RowIndex].Cells[0].FindControl("lblserialID");
                    Label Particulars = (Label)gvstocklist.Rows[row.RowIndex].Cells[0].FindControl("lblparticulars");
                    Label ID = (Label)gvstocklist.Rows[row.RowIndex].Cells[0].FindControl("lblID");
                    Label GrpRack = (Label)gvstocklist.Rows[row.RowIndex].Cells[0].FindControl("lbl_rackgrp");
                    Label SubRack = (Label)gvstocklist.Rows[row.RowIndex].Cells[0].FindControl("lbl_racksubgrp");
                    Label CPbeforeTax = (Label)gvstocklist.Rows[row.RowIndex].Cells[0].FindControl("lbl_cpbeforTax");
                    Label rateperqty = (Label)gvstocklist.Rows[row.RowIndex].Cells[0].FindControl("lbl_rateperQty");
                    Label discperqty = (Label)gvstocklist.Rows[row.RowIndex].Cells[0].FindControl("lbl_discountperQty");
                    Label cpafterdisc = (Label)gvstocklist.Rows[row.RowIndex].Cells[0].FindControl("lbl_cpafterdisc");
                    Label ddl_disctype = (Label)gvstocklist.Rows[row.RowIndex].Cells[0].FindControl("lbl_discounttype");
                    Label temp = (Label)gvstocklist.Rows[row.RowIndex].Cells[0].FindControl("lbl_temp");

                    GENStrData ObjDetails = new GENStrData();
                    ObjDetails.ReceiptNo = ReceiptNo.Text == "" ? "0" : ReceiptNo.Text;
                    ObjDetails.BatchNo = BatchNo.Text == "" ? "0" : BatchNo.Text;

                    IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
                    DateTime ReceivedDate1 = RecdDate.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(RecdDate.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                    DateTime MfgDate1 = MfgDate.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(MfgDate.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
                    DateTime ExpDate1 = ExpDate.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(ExpDate.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);

                    ObjDetails.PONo = PONo.Text == "" ? "0" : PONo.Text;
                    ObjDetails.MfgDate = MfgDate1;
                    ObjDetails.ReceivedDate = ReceivedDate1;
                    ObjDetails.ExpDate = ExpDate1;
                    ObjDetails.PurchasetypeID = Convert.ToInt32(ddl_purchagetype.SelectedValue == "" ? "0" : ddl_purchagetype.SelectedValue);
                    ObjDetails.ItemID = Convert.ToInt32(ItemID.Text == "" ? "" : ItemID.Text);
                    ObjDetails.CompanyID = Convert.ToInt32(CompanyID.Text == "" ? "" : CompanyID.Text);
                    ObjDetails.SupplierID = Convert.ToInt32(SupplierID.Text == "" ? "" : SupplierID.Text);
                    ObjDetails.NoOfUnit = Convert.ToInt32(NoOfUnit.Text == "" ? "" : NoOfUnit.Text);
                    ObjDetails.QtyperUnit = Convert.ToInt32(QtyperUnit.Text == "" ? "" : QtyperUnit.Text);
                    ObjDetails.CPperunit = Convert.ToDecimal(CPperunit.Text == "" ? "" : CPperunit.Text);
                    ObjDetails.RatePerQty = Convert.ToDecimal(rateperqty.Text == "" ? "" : rateperqty.Text);
                    ObjDetails.DiscountType = Convert.ToInt32(ddl_discountype.SelectedValue == "" ? "0" : ddl_discountype.SelectedValue);
                    ObjDetails.DiscountPerQty = Convert.ToDecimal(discperqty.Text == "" ? "" : discperqty.Text);
                    ObjDetails.TotalCPBeforeDisc = Convert.ToDecimal(cpafterdisc.Text == "" ? "" : cpafterdisc.Text);
                    ObjDetails.Temperature = temp.Text.Trim();
                    ObjDetails.TotalCPbeforeTax = Convert.ToDecimal(CPbeforeTax.Text == "" ? "" : CPbeforeTax.Text);
                    ObjDetails.CP = Convert.ToDecimal(CP.Text == "" ? "" : CP.Text);
                    ObjDetails.MRPperunit = Convert.ToDecimal(MRPperunit.Text == "" ? "" : MRPperunit.Text);
                    ObjDetails.MRP = Convert.ToDecimal(MRP.Text == "" ? "" : MRP.Text);
                    ObjDetails.Tax = Convert.ToInt32(Tax.Text == "" ? null : Tax.Text);
                    ObjDetails.FreeQuantity = Convert.ToInt32(FreeQuantity.Text == "" ? "0" : FreeQuantity.Text);
                    ObjDetails.RecvQty = Convert.ToInt32(RecvQty.Text == "" ? "0" : RecvQty.Text);
                    ObjDetails.TotalRecdQty = Convert.ToInt32(TotalRecdQty.Text == "" ? "0" : TotalRecdQty.Text);
                    ObjDetails.SerialID = Convert.ToInt32(SerialID.Text == "" ? "0" : SerialID.Text);
                    ObjDetails.ID = Convert.ToInt32(ID.Text == "" ? "0" : ID.Text);
                    ObjDetails.RackGroup = Convert.ToInt32(GrpRack.Text == "" ? "0" : GrpRack.Text);
                    ObjDetails.RackSubGroup = Convert.ToInt32(SubRack.Text == "" ? "0" : SubRack.Text);
                    itemcount = itemcount + 1;
                    ListStock.Add(ObjDetails);
                }
                objStock.XMLData = XmlConvertor.StockDetailsDatatoXML(ListStock).ToString();
                objStock.TrecievedQty = Convert.ToInt32(txt_recivedqty.Text == "" ? "0" : txt_recivedqty.Text);
                objStock.TotalFreeQty = Convert.ToInt32(txt_totalfreeqty.Text == "" ? "0" : txt_totalfreeqty.Text);
                objStock.TotalrecievedQty = Convert.ToInt32(txt_total_recvqty.Text == "" ? "0" : txt_total_recvqty.Text);
                objStock.TotalFreeItemAmount = Convert.ToDecimal(hdn_total_free_item_Amount.Value);
                objStock.ReceivedBy = Convert.ToInt64(ddl_receivedby.SelectedValue == "" ? "0" : ddl_receivedby.SelectedValue);
                objStock.PurchasetypeID = Convert.ToInt32(ddl_purchagetype.SelectedValue == "" ? "0" : ddl_purchagetype.SelectedValue);
                if (Convert.ToDecimal(txt_discount.Text == "" ? "0" : txt_discount.Text) > Convert.ToDecimal(txt_totalamount.Text == "" ? "0" : txt_totalamount.Text))
                {
                    Messagealert_.ShowMessage(lblmessage, "Discount should not be greater than total amount.", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    txt_discount.Focus();
                    return;
                }
                else
                {
                    objStock.Discount = Convert.ToDecimal(txt_discount.Text == "" ? "0" : txt_discount.Text);
                }

                if (ddl_receivedby.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "Recievedby", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    ddl_receivedby.Focus();
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
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
                objStock.HospitalID = LogData.HospitalID;
                objStock.EmployeeID = LogData.EmployeeID;
                objStock.FinancialYearID = LogData.FinancialYearID;
                objStock.ActionType = Enumaction.Insert;

                int result = objBO.UpdateStockItemDetails(objStock);
                if (result == 1)
                {
                    lblmessage.Visible = true;
                    Messagealert_.ShowMessage(lblmessage, "save", 1);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "SucessAlert";
                    btnsave.Attributes["disabled"] = "disabled";
                    hdn_total_free_item_Amount.Value = null;
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
        protected void clearall()
        {
            txt_receiveddate.Text = "";
            txt_totalreceivedqty.Text = "";
            txt_comp.Text = "";
            txt_suppl.Text = "";
            txt_qty.Text = "";
            txt_totalCPbeforeDisc.Text = "";
            txt_cp.Text = "";
            txt_totalcp.Text = "";
            txt_mrp.Text = "";
            txt_totalmrp.Text = "";
            txt_freeqty.Text = "";
            txt_tax.Text = "";
            txt_itemname.Text = "";
            txt_batchno.Text = "";
            txt_expdate.Text = "";
            txt_mfgdate.Text = "";
            txt_taxamount.Text = "";
            txt_discountperqty.Text = "";
            txt_temp.Text = "";
            txt_rateperqty.Text = "";
            txt_totalCPbeforeDisc.Text = "";
            txt_supplier.Text = "";
            txt_company.Text = "";
            txt_Unit.Text = "";
            txt_CpberforeTax.Text = "";
            txt_discount.Text = "";
        }
        protected void btnresets_Click(object sender, EventArgs e)
        {
            gvstocklist.DataSource = null;
            gvstocklist.DataBind();
            gvstocklist.Visible = false;
            ViewState["ID"] = null;
            lblmessage.Visible = false;
            txt_receivedno.Text = "";
            txt_receiveddate.Text = "";
            txt_PONo.Text = "";
            txt_company.Text = "";
            txt_supplier.Text = "";
            txt_qty.Text = "";
            txt_totalCPbeforeDisc.Text = "";
            txt_cp.Text = "";
            txt_totalcp.Text = "";
            txt_mrp.Text = "";
            txt_totalmrp.Text = "";
            txt_freeqty.Text = "";
            txt_tax.Text = "";
            ddl_purchagetype.SelectedIndex = 0;
            txt_itemname.Text = "";
            txt_batchno.Text = "";
            ddlgroup.SelectedIndex = 0;
            txt_expdate.Text = "";
            txt_mfgdate.Text = "";
            txt_totalreceivedqty.Text = "";
            txt_totalMRPS.Text = "";
            txt_totalamount.Text = "";
            ddl_receivedby.SelectedIndex = 0;
            txt_recivedqty.Text = "";
            txt_totalfreeqty.Text = "";
            txt_total_recvqty.Text = "";
            Session["StockItemList"] = null;
            txt_taxamount.Text = "";
            txt_discountperqty.Text = "";
            txt_CpberforeTax.Text = "";
            txt_discount.Text = "";
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetrecdNo(string prefixText, int count, string contextKey)
        {
            GENStrData Objpaic = new GENStrData();
            GenStoreBO objInfoBO = new GenStoreBO();
            List<GENStrData> getResult = new List<GENStrData>();
            Objpaic.ReceiptNo = prefixText;
            getResult = objInfoBO.GetrecdNo(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].ReceiptNo.ToString());
            }
            return list;
        }

        protected void gvstocklist1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblqty = (Label)e.Row.FindControl("lbl_qty");
                int a_qty = Int32.Parse(lblqty.Text);
                total = total + a_qty;

                Label lblrecdqty = (Label)e.Row.FindControl("lbl_totalrecdquantity");
                int qty = Int32.Parse(lblrecdqty.Text);
                total1 = total1 + qty;

                Label lbl_totfreeqty = (Label)e.Row.FindControl("lbl_freequantity");
                int freeqty = Int32.Parse(lbl_totfreeqty.Text);
                total2 = total2 + freeqty;

                Label lbl_cp = (Label)e.Row.FindControl("lbl_cp");
                decimal cp = Decimal.Parse(lbl_cp.Text);
                total3 = total3 + cp;

                Label lbl_cpperunit = (Label)e.Row.FindControl("lbl_cpperunit");
                decimal cpperunit = Decimal.Parse(lbl_cpperunit.Text);
                total4 = total4 + cpperunit;

                Label lbl_cpperqty = (Label)e.Row.FindControl("lbl_cpperqty");
                decimal cpperqty = Decimal.Parse(lbl_cpperqty.Text);
                total5 = total5 + cpperqty;

                Label lbl_totalmrp = (Label)e.Row.FindControl("lbl_totalmrp");
                decimal totalmrp = Decimal.Parse(lbl_totalmrp.Text);
                total6 = total6 + totalmrp;

                Label lbl_mrpperunit = (Label)e.Row.FindControl("lbl_mrpperunit");
                decimal mrpperunit = Decimal.Parse(lbl_mrpperunit.Text);
                total7 = total7 + mrpperunit;

                Label lbl_mrpperqty = (Label)e.Row.FindControl("lbl_mrpperqty");
                decimal mrpperqty = Decimal.Parse(lbl_mrpperqty.Text);
                total8 = total8 + mrpperqty;

                Label lbl_totalissued = (Label)e.Row.FindControl("lbl_totalissued");
                int totalissued = Int32.Parse(lbl_totalissued.Text);
                total9 = total9 + totalissued;

                //TextBox txt_totalcondemned = (TextBox)e.Row.FindControl("txt_totalcondemned");
                //int totalcondemned = Int32.Parse(txt_totalcondemned.Text);
                //total10 = total10 + totalcondemned;

                Label lbl_balstock = (Label)e.Row.FindControl("lbl_balstock");
                int totalbal = Int32.Parse(lbl_balstock.Text);
                total11 = total11 + totalbal;

                //Label lbl_cpremitem = (Label)e.Row.FindControl("lbl_cpremitem");
                //decimal totalcpremitem = Decimal.Parse(lbl_cpremitem.Text);
                //total12 = total12 + totalcpremitem;

                //Label lbl_mrpremitem = (Label)e.Row.FindControl("lbl_mrpremitem");
                //decimal totalmrpremitem = Decimal.Parse(lbl_mrpremitem.Text);
                //total13 = total13 + totalmrpremitem;

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label totalqty = (Label)e.Row.FindControl("lbl_totalqty");
                totalqty.Text = total.ToString();

                Label totalrecdqty = (Label)e.Row.FindControl("lbl_totrecdqty");
                totalrecdqty.Text = total1.ToString();

                Label lbl_totfreeqty = (Label)e.Row.FindControl("lbl_totfreeqty");
                lbl_totfreeqty.Text = total2.ToString();

                Label lbl_totcp = (Label)e.Row.FindControl("lbl_totcp");
                lbl_totcp.Text = total3.ToString();

                Label lbl_totcpperunit = (Label)e.Row.FindControl("lbl_totcpperunit");
                lbl_totcpperunit.Text = total4.ToString();

                Label lbl_totcpperqty = (Label)e.Row.FindControl("lbl_totcpperqty");
                lbl_totcpperqty.Text = total5.ToString();

                Label lbl_totmrp = (Label)e.Row.FindControl("lbl_totmrp");
                lbl_totmrp.Text = total6.ToString();

                Label lbl_totmrpperunit = (Label)e.Row.FindControl("lbl_totmrpperunit");
                lbl_totmrpperunit.Text = total7.ToString();

                Label lbl_totmrpperqty = (Label)e.Row.FindControl("lbl_totmrpperqty");
                lbl_totmrpperqty.Text = total8.ToString();

                Label lbl_totissued = (Label)e.Row.FindControl("lbl_totissued");
                lbl_totissued.Text = total9.ToString();

                //TextBox txt_totcondemned = (TextBox)e.Row.FindControl("txt_totcondemned");
                //txt_totcondemned.Text = total10.ToString();

                Label lbl_totbalstock = (Label)e.Row.FindControl("lbl_totbalstock");
                lbl_totbalstock.Text = total11.ToString();

                Label lbl_totcpremitem = (Label)e.Row.FindControl("lbl_totcpremitem");
                lbl_totcpremitem.Text = total12.ToString();

                Label lbl_totmrpremitem = (Label)e.Row.FindControl("lbl_totmrpremitem");
                lbl_totmrpremitem.Text = total13.ToString();


            }

        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            bindgrid();
        }
        protected void chekboxselect_CheckedChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gvstocklist1.Rows)
            {
                CheckBox cb = (CheckBox)gvstocklist1.Rows[row.RowIndex].Cells[0].FindControl("chekboxselect"); //find the CheckBox
                if (cb.Checked)
                {
                    TextBox txt_totalcondemned = (TextBox)gvstocklist1.Rows[row.RowIndex].Cells[0].FindControl("txt_totalcondemned"); //find the CheckBox
                    txt_totalcondemned.ReadOnly = false;
                    txt_totalcondemned.Focus();
                }
            }
        }
        protected void btnupdate_Click(object sender, EventArgs e)
        {
            List<GENStrData> ListStock = new List<GENStrData>();
            GENStrData objStock = new GENStrData();
            GenStoreBO objBO = new GenStoreBO();
            try
            {

                // get all the record from the gridview
                foreach (GridViewRow row in gvstocklist1.Rows)
                {
                    CheckBox cb = (CheckBox)gvstocklist1.Rows[row.RowIndex].Cells[0].FindControl("chekboxselect"); //find the CheckBox
                    if (cb.Checked)
                    {
                        Label ID = (Label)gvstocklist1.Rows[row.RowIndex].Cells[0].FindControl("lbl_ID");
                        TextBox TotalCondemned = (TextBox)gvstocklist1.Rows[row.RowIndex].Cells[0].FindControl("txt_totalcondemned");
                        GENStrData ObjDetails = new GENStrData();
                        ObjDetails.ID = Convert.ToInt32(ID.Text == "" ? "0" : ID.Text);
                        ObjDetails.TotalCondemned = Convert.ToInt16(TotalCondemned.Text == "" ? "0" : TotalCondemned.Text);
                        ListStock.Add(ObjDetails);
                    }
                }
                objStock.XMLData = XmlConvertor.StockCondemnedDatatoXML(ListStock).ToString();
                objStock.EmployeeID = LogData.EmployeeID;

                if (ViewState["ID"] != null)
                {
                    objStock.ActionType = Enumaction.Update;
                    objStock.ID = Convert.ToInt32(ViewState["ID"].ToString());
                }
                int result = objBO.UpdateStockCondemnedItemDetails(objStock);
                if (result == 1 || result == 2)
                {
                    lblmessage.Visible = true;
                    Messagealert_.ShowMessage(lblmessage, result == 1 ? "save" : "update", 1);
                    ViewState["ID"] = null;
                    bindgrid();

                }
                else if (result == 5)
                {
                    Messagealert_.ShowMessage(lblmessage, "duplicate", 0);
                }
                else
                {
                    Messagealert_.ShowMessage(lblmessage, "system", 0);
                }
            }

            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);

            }

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
                //if (txt_recdfrom.Text == "")
                //{
                //    if (Commonfunction.isValidDate(txt_recdfrom.Text) == false)
                //    {
                //        Messagealert_.ShowMessage(lblmessage1, "ValidDate", 0);
                //        divmsg2.Attributes["class"] = "FailAlert";
                //        divmsg2.Visible = true;
                //        txt_recdfrom.Focus();
                //        return;
                //    }
                //}
                //else
                //{
                //    divmsg2.Visible = false;
                //}
                //if (txt_recdTo.Text == "")
                //{
                //    if (Commonfunction.isValidDate(txt_recdTo.Text) == false)
                //    {
                //        Messagealert_.ShowMessage(lblmessage1, "ValidDate", 0);
                //        divmsg2.Attributes["class"] = "FailAlert";
                //        divmsg2.Visible = true;
                //        txt_recdTo.Focus();
                //        return;
                //    }
                //}
                //else
                //{
                //    divmsg2.Visible = false;
                //}
                List<GENStrData> objdeposit = GetStockItemList(0);
                if (objdeposit.Count > 0)
                {
                    gvstocklist1.DataSource = objdeposit;
                    gvstocklist1.DataBind();
                    gvstocklist1.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total:" + objdeposit[0].MaximumRows.ToString() + " Record(s) found.", 1);
                    divmsg3.Attributes["class"] = "SucessAlert";
                    divmsg3.Visible = true;
                    ddlexport.Visible = true;
                    btnexport.Visible = true;
                    lblmessage1.Visible = false;
                    lblmessage1.Visible = false;
                }
                else
                {
                    gvstocklist1.DataSource = null;
                    gvstocklist1.DataBind();
                    gvstocklist1.Visible = true;
                    ddlexport.Visible = false;
                    btnexport.Visible = false;
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
        protected void ddlpurchagetype_SelectedIndexChanged(object sender, EventArgs e)
        {
            txt_receivedno.Text = "";
            txt_PONo.Text = "";
            if (ddl_purchagetype.SelectedIndex == 1)
            {
                txt_PONo.ReadOnly = false;
                idpo.Visible = true;
                txt_PONo.Focus();
            }
            else
            {
                txt_PONo.ReadOnly = true;
                idpo.Visible = false;
                txt_receivedno.Focus();
            }
        }
        protected void ddlgroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            AutoCompleteExtender2.ContextKey = ddlgroup.SelectedValue;
            txt_itemname.Text = "";
            txt_batchno.Text = "";
            txt_receiveddate.Text = "";
            txt_supplier.Text = "";
            txt_company.Text = "";
            txt_mfgdate.Text = "";
            txt_expdate.Text = "";
            txt_qty.Text = "";
            txt_totalreceivedqty.Text = "";
            txt_cp.Text = "";
            txt_totalmrp.Text = "";
            txt_mrp.Text = "";
            txt_freeqty.Text = "";
            txt_tax.Text = "";
            txt_totalcp.Text = "";
            txt_totalCPbeforeDisc.Text = "";
            //txt_itemname.Focus();
        }
        public List<GENStrData> GetStockItemList(int curIndex)
        {
            GENStrData objstock = new GENStrData();
            GenStoreBO objBO = new GenStoreBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            objstock.ReceiptNo = txt_recdno.Text.ToString() == "" ? "" : txt_recdno.Text.ToString();
            objstock.PONo = txtPONo.Text.ToString() == "" ? "" : txtPONo.Text.ToString();
            objstock.GroupID = Convert.ToInt32(ddl_itemgroup.SelectedValue == "" ? "0" : ddl_itemgroup.SelectedValue);
            objstock.SubGroupID = Convert.ToInt32(ddl_subgroup.SelectedValue == "" ? "0" : ddl_subgroup.SelectedValue);
            var source1 = txt_company.Text.ToString();
            if (source1.Contains(":"))
            {
                string ID1 = source1.Substring(source1.LastIndexOf(':') + 1);
                objstock.CompanyID = Convert.ToInt32(ID1);
            }
            var source2 = txt_supplier.Text.ToString();
            if (source2.Contains(":"))
            {
                string ID2 = source2.Substring(source2.LastIndexOf(':') + 1);
                objstock.SupplierID = Convert.ToInt32(ID2);
            }
            objstock.IsActive = ddl_Status.SelectedValue == "1" ? true : false;
            objstock.ItemName = txtItemName.Text.ToString() == "" ? "" : txtItemName.Text.ToString();
            DateTime from = txt_recdfrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txt_recdfrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime to = txt_recdTo.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txt_recdTo.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objstock.DateFrom = from;
            objstock.DateTo = to;
            objstock.ReceivedBy = Convert.ToInt64(ddl_stockrecievedby.SelectedValue == "" ? "0" : ddl_stockrecievedby.SelectedValue);
            objstock.RackGroup = Convert.ToInt32(ddl_seacrhrackgrp.SelectedValue == "" ? "0" : ddl_seacrhrackgrp.SelectedValue);
            objstock.RackSubGroup = Convert.ToInt32(ddl_searchracksubgrp.SelectedValue == "" ? "0" : ddl_searchracksubgrp.SelectedValue);
            return objBO.GetStockItemList(objstock);
        }
        protected void btnreset_Click(object sender, EventArgs e)
        {
            gvstocklist1.DataSource = null;
            gvstocklist1.DataBind();
            gvstocklist1.Visible = false;
            lblresult.Visible = false;
            ddlexport.SelectedIndex = 0;
            ddlexport.Visible = false;
            btnexport.Visible = false;
            lblmessage1.Visible = false;
            lblresult.Visible = false;
            divmsg3.Visible = false;
            ddl_purchagetype.SelectedIndex = 0;
            txt_PONo.Text = "";
            txt_receivedno.Text = "";
            ddl_subgroup.ClearSelection();
            Commonfunction.Insertzeroitemindex(ddl_subgroup);
            ddl_itemgroup.SelectedIndex = 0;
            ddl_stockrecievedby.SelectedIndex = 0;
            txt_recdfrom.Text = "";
            txt_recdTo.Text = "";
            ddl_Status.SelectedIndex = 0;
            txt_supplier.Text = "";
            txt_company.Text = "";
        }
        protected void gvstocklist1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Deletes")
                {
                    GENStrData objbill = new GENStrData();
                    GenStoreBO objstdBO = new GenStoreBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = gvstocklist1.Rows[i];
                    Label ID = (Label)gr.Cells[0].FindControl("lbl_ID");
                    Label ReceiptNo = (Label)gr.Cells[0].FindControl("lbl_recdno");
                    Label PONo = (Label)gr.Cells[0].FindControl("lbl_PONo");
                    Label ItemName = (Label)gr.Cells[0].FindControl("lblitemname");

                    TextBox txtremarks = (TextBox)gr.Cells[0].FindControl("txtremarks");
                    txtremarks.Enabled = true;
                    if (txtremarks.Text == "")
                    {
                        Messagealert_.ShowMessage(lblmessage1, "Remarks", 0);
                        divmsg2.Visible = true;
                        divmsg2.Attributes["class"] = "FailAlert";
                        txtremarks.Focus();
                        return;
                    }
                    else
                    {
                        divmsg2.Visible = false;
                        objbill.Remarks = txtremarks.Text;
                    }
                    objbill.ID = Convert.ToInt64(ID.Text);
                    objbill.ReceiptNo = ReceiptNo.Text.Trim();
                    objbill.PONo = PONo.Text.Trim();
                    objbill.ItemName = ItemName.Text.Trim();
                    objbill.EmployeeID = LogData.UserLoginId;

                    int Result = objstdBO.DeleteStockItemListByID(objbill);
                    if (Result == 1)
                    {
                        Messagealert_.ShowMessage(lblmessage1, "delete", 1);
                        lblmessage1.Attributes["class"] = "SucessAlert";
                        divmsg1.Visible = true;
                        bindgrid();
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
        protected DataTable GetDatafromDatabase()
        {
            List<GENStrData> DepositDetails = GetStockItemList(0);
            List<StockItemDataTOeXCEL> ListexcelData = new List<StockItemDataTOeXCEL>();
            int i = 0;
            foreach (GENStrData row in DepositDetails)
            {
                StockItemDataTOeXCEL Ecxeclpat = new StockItemDataTOeXCEL();
                Ecxeclpat.ReceiptNo = DepositDetails[i].ReceiptNo;
                Ecxeclpat.PONo = DepositDetails[i].PONo;
                Ecxeclpat.ItemName = DepositDetails[i].ItemName;
                Ecxeclpat.TotalRecdQty = DepositDetails[i].TotalRecdQty;
                Ecxeclpat.FreeQuantity = DepositDetails[i].FreeQuantity;
                Ecxeclpat.CP = Commonfunction.Getrounding(DepositDetails[i].CP.ToString());
                Ecxeclpat.CPperunit = Commonfunction.Getrounding(DepositDetails[i].CPperunit.ToString());
                Ecxeclpat.CPPerQty = Commonfunction.Getrounding(DepositDetails[i].CPPerQty.ToString());
                Ecxeclpat.TotalMRP = Commonfunction.Getrounding(DepositDetails[i].TotalMRP.ToString());
                Ecxeclpat.MRPperUnit = Commonfunction.Getrounding(DepositDetails[i].MRPperUnit.ToString());
                Ecxeclpat.MRPPerQty = Commonfunction.Getrounding(DepositDetails[i].MRPPerQty.ToString());
                Ecxeclpat.TotalIssued = DepositDetails[i].TotalIssued;
                Ecxeclpat.TotalCondemned = DepositDetails[i].TotalCondemned;
                Ecxeclpat.BalStock = DepositDetails[i].BalStock;
                Ecxeclpat.CPRemItem = Commonfunction.Getrounding(DepositDetails[i].CPRemItem.ToString());
                Ecxeclpat.MRPRemItem = Commonfunction.Getrounding(DepositDetails[i].MRPRemItem.ToString());
                Ecxeclpat.MfgDate = DepositDetails[i].MfgDate;
                Ecxeclpat.ExpDate = DepositDetails[i].ExpDate;
                Ecxeclpat.ReceivedDate = DepositDetails[i].ReceivedDate;
                Ecxeclpat.RecdBy = DepositDetails[i].RecdBy;


                ListexcelData.Add(Ecxeclpat);
                i++;
            }
            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            DataTable dt = converter.ToDataTable(ListexcelData);
            return dt;
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
                    gvstocklist1.BorderStyle = BorderStyle.None;
                    //Hide the Column containing CheckBox
                    gvstocklist1.Columns[21].Visible = false;
                    gvstocklist1.Columns[22].Visible = false;

                    gvstocklist1.RenderControl(hw);
                    gvstocklist1.HeaderRow.Style.Add("width", "15%");
                    gvstocklist1.HeaderRow.Style.Add("font-size", "10px");
                    gvstocklist1.Style.Add("text-decoration", "none");
                    gvstocklist1.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    gvstocklist1.Style.Add("font-size", "8px");
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A4, 7f, 7f, 7f, 0f);
                    PdfWriter writer = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    iTextSharp.tool.xml.XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                    pdfDoc.Close();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=StockDetails.pdf");
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
                Response.AddHeader("content-disposition", "attachment;filename=StockDetails.xlsx");
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
        protected void gvstocklist1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvstocklist1.PageIndex = e.NewPageIndex;
            bindgrid();
        }
        protected void ddl_purchagetype_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_purchagetype.SelectedIndex == 1)
            {
                txtPONo.ReadOnly = false;
                txtPONo.Focus();

            }
            else if (ddl_purchagetype.SelectedIndex == 2)
            {
                txtPONo.ReadOnly = true;
                txt_receivedno.Focus();

            }

        }
        //protected void ddl_rackgrp_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    MasterLookupBO mstlookup = new MasterLookupBO();
        //    Commonfunction.PopulateDdl(ddl_racksubgrp, mstlookup.GetGENItemSubRackByItemRackID(Convert.ToInt32(ddl_rackgrp.SelectedValue)));
        //}
        protected void ddl_seacrhrackgrp_SelectedIndexChanged(object sender, EventArgs e)
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddl_searchracksubgrp, mstlookup.GetGENItemSubRackByItemRackID(Convert.ToInt32(ddl_seacrhrackgrp.SelectedValue)));
        }
    }
}
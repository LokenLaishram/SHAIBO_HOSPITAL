using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedPharBO;
using Mediqura.CommonData.Common;
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
    public partial class Phr_ItemSaleTracking : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
				MasterLookupBO mstlookup = new MasterLookupBO();
				Commonfunction.PopulateDdl(ddlsupplier, mstlookup.GetLookupsList(LookupName.Supplier));
                btn_print.Attributes["disabled"] = "disabled";
                txt_itemname.Attributes["disabled"] = "disabled";
                btn_prints.Attributes["disabled"] = "disabled";
                txttotolsaleqty.Attributes["disabled"] = "disabled";
                txtTotalReturnQty.Attributes["disabled"] = "disabled";
                txttotalsaleamount.Attributes["disabled"] = "disabled";
                txttotalreturnamt.Attributes["disabled"] = "disabled";
            }
           
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetItemName(string prefixText, int count, string contextKey)
        {
            ItemSalesTrackerData Objsale = new ItemSalesTrackerData();
            ItemSalesTrackerBO ObjtranferBO = new ItemSalesTrackerBO();
            List<ItemSalesTrackerData> getResult = new List<ItemSalesTrackerData>();
            Objsale.ItemName = prefixText;
            Objsale.MedSubStockID = Convert.ToInt32(contextKey);
            getResult = ObjtranferBO.GetItemName(Objsale);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].ItemName.ToString());
            }
            return list;
        }
        protected void ItemName_OnTextChanged(object sender, EventArgs e)
        {
            if (txtitemname.Text.Contains(":"))
            {
                bindgrid();
            }
            else
            {                     
                txtitemname.Text = "";
                return;
            } 
          
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

                List<ItemSalesTrackerData> objtransfer = GetSaleItemList(0);
                if (objtransfer.Count > 0)
                {  
                    gvPhrSaleItemList.DataSource = objtransfer;
                    gvPhrSaleItemList.DataBind();
                    gvPhrSaleItemList.Visible = true;
                    lblmessage.Visible = false;
                    lblmessage.Visible = false;
                    btn_print.Attributes.Remove("disabled");

                }
                else
                {
                    gvPhrSaleItemList.DataSource = null;
                    gvPhrSaleItemList.DataBind();
                    gvPhrSaleItemList.Visible = true;
                    divmsg3.Visible = false;
                    btn_print.Attributes["disabled"] = "disabled";
                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage1, "system", 0);
            }
        }
        public List<ItemSalesTrackerData> GetSaleItemList(int curIndex)
        {
            ItemSalesTrackerData objItemSale = new ItemSalesTrackerData();
            ItemSalesTrackerBO objBO = new ItemSalesTrackerBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime from = txt_from.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txt_from.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime to = txt_To.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txt_To.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objItemSale.DateFrom = from;
            objItemSale.DateTo = to;
			objItemSale.SupplierID = Convert.ToInt32(ddlsupplier.SelectedValue == "" ? "0" : ddlsupplier.SelectedValue);
            
            objItemSale.BatchNo = txtBatchNo.Text == "" ? "" : txtBatchNo.Text;
            if (txtitemname.Text.Contains(":"))
            {
                bool isIDnumeric = txtitemname.Text.Substring(txtitemname.Text.LastIndexOf(':') + 1).All(char.IsDigit);
                if (isIDnumeric == true)
                {
                    objItemSale.ItemID = isIDnumeric ? Convert.ToInt64(txtitemname.Text.Contains(":") ? txtitemname.Text.Substring(txtitemname.Text.LastIndexOf(':') + 1) : "0") : 0;
                }
                else
                {
                    objItemSale.ItemID = 0;
                    txtitemname.Text = "";
                }
            }
            else
            {
                objItemSale.ItemID = 0;
                txtitemname.Text = "";
            }               
          
            return objBO.GetSaleItemList(objItemSale);
        }

        protected void btnreset_Click(object sender, EventArgs e)
        {
            Reset();
        }
        protected void Reset()
        {
            txtitemname.Text = "";
            txt_from.Text = "";
            txt_To.Text = "";
            hdnItemID.Value = "";
            gvPhrSaleItemList.DataSource = null;
            gvPhrSaleItemList.DataBind();
            gvPhrSaleItemList.Visible = true;
			ddlsupplier.SelectedIndex = 0;
            divmsg3.Visible = false;
            //divmsg2.Visible = false;
            btn_print.Attributes["disabled"] = "disabled";
            Clear();
        }
        protected void gvPhrSaleItemList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {                   
                if (e.CommandName == "Select")
                {

                    ItemSalesTrackerData objcondemn = new ItemSalesTrackerData();
                    ItemSalesTrackerBO objstdBO = new ItemSalesTrackerBO();
                    int i = Convert.ToInt16(e.CommandArgument.ToString());
                    GridViewRow gr = gvPhrSaleItemList.Rows[i];
                    Label Itemid = (Label)gr.Cells[0].FindControl("lbl_ItemID");
					Label lblbatchnos = (Label)gr.Cells[0].FindControl("lblbatchnos");
                    hdnItemID.Value = Itemid.Text.Trim();
					hdnbatchno.Value = lblbatchnos.Text.Trim();
                    bindSaleItemDetails(Itemid.Text); 
                }
            }
            catch (Exception ex) //Exception in agent layer itself
            {
                PolicyBasedExceptionHandler.HandleException(PolicyBasedExceptionHandler.PolicyName.UIExceptionPolicy, ex, "1000001");
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
            }
        }
		protected void ddlpatienttype_SelectedIndexChanged(object sender, EventArgs e)
		{
			bindSaleItemDetails(hdnItemID.Value.ToString()); 

		}
        protected void bindSaleItemDetails(string Itemid)
        {
            ItemSalesTrackerData objsale = new ItemSalesTrackerData();
            ItemSalesTrackerBO objstdBO = new ItemSalesTrackerBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime from = txt_from.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txt_from.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime to = txt_To.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txt_To.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objsale.DateFrom = from;
            objsale.DateTo = to;
            objsale.ItemID = Convert.ToInt32(Itemid.Trim());
			objsale.PatientTypeID = Convert.ToInt32(ddlpatienttype.SelectedValue == "" ? "0" : ddlpatienttype.SelectedValue);
            objsale.BatchNo = hdnbatchno.Value.ToString();
            List<ItemSalesTrackerData> List = new List<ItemSalesTrackerData>();
            List = objstdBO.GetItemSaleDetailsList(objsale);
            if (List.Count > 0)
            {
                Clear();
                tabContainerSaleList.ActiveTabIndex = 1;
                hdnItemID.Value = List[0].ItemID.ToString();
                txt_itemname.Text = List[0].ItemName.ToString();
                txttotolsaleqty.Text = List[0].SUMSaleEqvQty.ToString();
                txtTotalReturnQty.Text = List[0].SUMReturnQty.ToString();
                txttotalsaleamount.Text = List[0].SUMNetAmount.ToString("N");
                txttotalreturnamt.Text = List[0].SUMSalesReturnAmnt.ToString("N");
              
                gvSaleDetailslist.DataSource = List;
                gvSaleDetailslist.DataBind();
                gvSaleDetailslist.Visible = true;
                divmsg4.Visible = true;
                btn_prints.Attributes.Remove("disabled");
            }
            else
            {
                tabContainerSaleList.ActiveTabIndex = 1;
                gvSaleDetailslist.DataSource = null;
                gvSaleDetailslist.DataBind();
                gvSaleDetailslist.Visible = true;
                lbl_result.Text = "";
                divmsg4.Visible = false;
                btn_prints.Attributes["disabled"] = "disabled";
            }

        }
        protected void Clear()
        {
            txt_itemname.Text = "";
            hdnItemID.Value = "";
            txttotolsaleqty.Text = "";
            txtTotalReturnQty.Text = "";
            txttotalsaleamount.Text = "";
            txttotalreturnamt.Text = "";
            gvSaleDetailslist.DataSource = null;
            gvSaleDetailslist.DataBind();
            gvSaleDetailslist.Visible = true;
            btn_prints.Attributes["disabled"] = "disabled";
            tabContainerSaleList.ActiveTabIndex = 0;
        }
    }
}
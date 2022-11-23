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
    public partial class SupplierSaleTracker : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MasterLookupBO mstlookup = new MasterLookupBO();
                Commonfunction.PopulateDdl(ddlsupplier, mstlookup.GetLookupsList(LookupName.Supplier));
                btn_print.Attributes["disabled"] = "disabled";                  
            }
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetItemName(string prefixText, int count, string contextKey)
        {
            SupplierSaleTrackerData ObjData = new SupplierSaleTrackerData();
            SupplierSaleTrackerBO ObjBO = new SupplierSaleTrackerBO();
            List<SupplierSaleTrackerData> getResult = new List<SupplierSaleTrackerData>();
            ObjData.ItemName = prefixText;           
            getResult = ObjBO.GetItemName(ObjData);
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

                List<SupplierSaleTrackerData> objtransfer = GetSupplierSaleItemList(0);
                if (objtransfer.Count > 0)
                {
                    gvPhrSupplierSaleList.DataSource = objtransfer;
                    gvPhrSupplierSaleList.DataBind();
                    gvPhrSupplierSaleList.Visible = true;
                    lblmessage.Visible = false;
                    lblmessage.Visible = false;
                    btn_print.Attributes.Remove("disabled");  
                }
                else
                {
                    gvPhrSupplierSaleList.DataSource = null;
                    gvPhrSupplierSaleList.DataBind();
                    gvPhrSupplierSaleList.Visible = true;                       
                    btn_print.Attributes["disabled"] = "disabled";
                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
            }
        }
        public List<SupplierSaleTrackerData> GetSupplierSaleItemList(int curIndex)
        {
            SupplierSaleTrackerData ObjData = new SupplierSaleTrackerData();
            SupplierSaleTrackerBO ObjBO = new SupplierSaleTrackerBO();

            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            ObjData.SupplierID = Convert.ToInt32(ddlsupplier.SelectedValue == "" ? "0" : ddlsupplier.SelectedValue);
            DateTime from = txt_from.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txt_from.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime to = txt_To.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txt_To.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            ObjData.DateFrom = from;
            ObjData.DateTo = to;
            ObjData.BatchNo = txtBatchNo.Text == "" ? "" : txtBatchNo.Text;
            if (txtitemname.Text.Contains(":"))
            {
                bool isIDnumeric = txtitemname.Text.Substring(txtitemname.Text.LastIndexOf(':') + 1).All(char.IsDigit);
                if (isIDnumeric == true)
                {
                    ObjData.ItemID = isIDnumeric ? Convert.ToInt64(txtitemname.Text.Contains(":") ? txtitemname.Text.Substring(txtitemname.Text.LastIndexOf(':') + 1) : "0") : 0;
                }
                else
                {
                    ObjData.ItemID = 0;
                    txtitemname.Text = "";
                }
            }
            else
            {
                ObjData.ItemID = 0;
                txtitemname.Text = "";
            }

            return ObjBO.GetSupplierSaleItemList(ObjData);
        }
        protected void btnreset_Click(object sender, EventArgs e)
        {
            Reset();
        }
        protected void Reset()
        {
                                                    
            ddlsupplier.SelectedIndex = 0;
            txt_from.Text = "";
            txt_To.Text = "";
            txtitemname.Text = "";
            txtBatchNo.Text = "";
            //hdnItemID.Value = "";
            gvPhrSupplierSaleList.DataSource = null;
            gvPhrSupplierSaleList.DataBind();
            gvPhrSupplierSaleList.Visible = true;               
            divmsg1.Visible = false;
            lblmessage.Text = "";
            btn_print.Attributes["disabled"] = "disabled";               
        }
    }
}
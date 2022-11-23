using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedGenStoreBO;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.MedStore;
using Mediqura.Utility.Logging;
using Mediqura.Web.MedCommon;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mediqura.Web.MedGenStore
{
    public partial class ItemNonAvail : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindddl();
            }
        }
        private void bindddl()
        {
            MasterLookupBO mstlookup = new MasterLookupBO();
            Commonfunction.PopulateDdl(ddl_substock, mstlookup.GetLookupsList(LookupName.GenStockType));
        }
        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static List<string> GetItemDetails(string prefixText, int count, string contextKey)
        {
            GenIndentData Objpaic = new GenIndentData();
            GenIndentBO objInfoBO = new GenIndentBO();
            List<GenIndentData> getResult = new List<GenIndentData>();
            Objpaic.ItemName = prefixText;
            getResult = objInfoBO.GetItemNameListInStore(Objpaic);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].ItemName);

            }
            return list;
        }
        protected void txtItemName_TextChanged(object sender, EventArgs e)
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

                List<GenIndentData> objdeposit = GetIndentItemList(0);
                if (objdeposit.Count > 0)
                {

                    GvNonavailitems.DataSource = objdeposit;
                    GvNonavailitems.DataBind();
                    GvNonavailitems.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total:" + objdeposit[0].MaximumRows.ToString() + " Record(s) found.", 1);
                    div1.Attributes["class"] = "SucessAlert";
                    div1.Visible = true;
                    lblmessage2.Visible = false;

                }
                else
                {
                    GvNonavailitems.DataSource = null;
                    GvNonavailitems.DataBind();
                    GvNonavailitems.Visible = true;
                    lblresult.Visible = false;
                    div1.Visible = false;
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
        public List<GenIndentData> GetIndentItemList(int curIndex)
        {
            GenIndentData objstock = new GenIndentData();
            GenIndentBO objBO = new GenIndentBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            objstock.GenStockID = Convert.ToInt32(ddl_substock.SelectedValue == "" ? "0" : ddl_substock.SelectedValue);
            objstock.StatusID = Convert.ToInt32(ddl_purchagestatus.SelectedValue == "" ? "0" : ddl_purchagestatus.SelectedValue);
            var source = txtItemName.Text.ToString();
            if (source.Contains(":"))
            {
                string ID = source.Substring(source.LastIndexOf(':') + 1);
                objstock.ItemID = Convert.ToInt32(ID);
            }
            else
            {
                objstock.ItemID = 0;
            }
            return objBO.GetNeedtoPurchageitemlist(objstock);
        }
        protected void GvNonavailitems_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label status = (Label)e.Row.FindControl("lbl_status");

                if (Convert.ToInt32(status.Text) == 3)
                {
                    e.Row.Cells[3].BackColor = System.Drawing.Color.Red;
                }
                if (Convert.ToInt32(status.Text) == 2)
                {
                    e.Row.Cells[3].BackColor = System.Drawing.Color.DarkRed;
                }
                if (Convert.ToInt32(status.Text) == 1)
                {
                    e.Row.Cells[3].BackColor = System.Drawing.Color.Yellow;
                }
            }
        }
        protected void btnresets_Click(object sender, EventArgs e)
        {
            ddl_purchagestatus.SelectedIndex = 0;
            txtItemName.Text = "";
            ddl_substock.SelectedIndex = 0;
            GvNonavailitems.DataSource = null;
            GvNonavailitems.DataBind();
            GvNonavailitems.Visible = false;
            lblresult.Visible = false;
            divmsg2.Visible = false;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Mediqura.BOL.MedStore;
using Mediqura.CommonData.MedStore;
using Mediqura.Utility;
using Mediqura.Utility.Logging;
using Mediqura.Web.MedCommon;

namespace Mediqura.Web.MedStore
{
    public partial class StockProfitLost : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            bindgrid();
        }
        protected void bindgrid()
        {
            try
            {   
                List<StoreProfitStatusData> objStockProfitLost = GetStockProfitLostList(0);
                if (objStockProfitLost.Count > 0)
                {
                    gvstockprofitstatus.DataSource = objStockProfitLost;
                    gvstockprofitstatus.DataBind();
                    gvstockprofitstatus.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total:" + objStockProfitLost[0].MaximumRows.ToString() + " Record(s) found.", 1);
                    divmsg3.Attributes["class"] = "SucessAlert";
                    txt_totalcp.Text = Commonfunction.Getrounding(objStockProfitLost[0].NetTotalCP.ToString());
                    txt_totalmrp.Text = Commonfunction.Getrounding(objStockProfitLost[0].NetTotalMRP.ToString());
                    txt_totalProfit.Text = Commonfunction.Getrounding(objStockProfitLost[0].NetTotalProfit.ToString());
                   
                    divmsg3.Visible = true;
                    ddlexport.Visible = true;
                    btnexport.Visible = true;
                    lblmessage.Visible = false;
                    lblmessage.Visible = false;
                   
                }
                else
                {
                    gvstockprofitstatus.DataSource = null;
                    gvstockprofitstatus.DataBind();
                    gvstockprofitstatus.Visible = true;
                    ddlexport.Visible = false;
                    btnexport.Visible = false;
                    lblresult.Visible = false;
                    divmsg3.Visible = false;
                  

                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
            }
        }
        public List<StoreProfitStatusData> GetStockProfitLostList(int curIndex)
        {
            StoreProfitStatusData objstockprofit = new StoreProfitStatusData();
            StockSaleProfitStatusBO objBO = new StockSaleProfitStatusBO();
          
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime from = txt_datefrom.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txt_datefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime to = txt_dateTo.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txt_dateTo.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objstockprofit.DateFrom = from;
            objstockprofit.DateTo = to;
            objstockprofit.CustomerTypeID = Convert.ToInt32(ddl_customertype.SelectedValue == "" ? "0" : ddl_customertype.SelectedValue);
            objstockprofit.StockStatusID = Convert.ToInt32(ddl_stockstatus.SelectedValue == "" ? "0" : ddl_stockstatus.SelectedValue);
            objstockprofit.IsActive = ddlstatus.SelectedValue == "1" ? true : false;
            return objBO.GetStockSaleProfitStatusList(objstockprofit);
        }

        protected void btnreset_Click(object sender, EventArgs e)
        {
            ddl_customertype.SelectedIndex = 0;
            ddl_stockstatus.SelectedIndex = 0;
            ddlmonth.SelectedIndex = 0;
            ddlyear.SelectedIndex = 0;
            txt_datefrom.Text = "";
            txt_dateTo.Text = "";
            txt_totalcp.Text="";
            txt_totalmrp.Text = "";
            txt_totalProfit.Text = "";
            gvstockprofitstatus.DataSource = null;
            gvstockprofitstatus.DataBind();
            gvstockprofitstatus.Visible = false;
            lblresult.Visible = false;
            ddlexport.SelectedIndex = 0;
            ddlexport.Visible = false;
            btnexport.Visible = false;
        }
    }
}
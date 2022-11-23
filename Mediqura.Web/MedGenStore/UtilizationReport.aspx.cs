using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MedStore;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.MedStore;
using Mediqura.Utility;
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
    public partial class UtilizationReport : BasePage
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
            Commonfunction.PopulateDdl(ddl_financialyr, mstlookup.GetLookupsList(LookupName.FinancialYearID));
            ddl_financialyr.SelectedIndex = 1;
            Commonfunction.PopulateDdl(ddl_monthID, mstlookup.GetLookupsList(LookupName.BelowCurMonth));
            Commonfunction.PopulateDdl(ddl_substock, mstlookup.GetLookupsList(LookupName.GenStockType));
            ddl_substock.SelectedValue = LogData.GenSubStockID.ToString();
            if (LogData.RoleID == 1 || LogData.RoleID == 22)
            {
                ddl_substock.Attributes.Remove("disabled");
            }
            else
            {
                ddl_substock.Attributes["disabled"] = "disabled";
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
            UtilizationReportData Objutili = new UtilizationReportData();
            UtilizationReportBO objUtiliBO = new UtilizationReportBO();
            List<UtilizationReportData> getResult = new List<UtilizationReportData>();
            Objutili.ItemName = prefixText;
            Objutili.GenStockID = Convert.ToInt32(contextKey);
            getResult = objUtiliBO.GetSubStockItemName(Objutili);
            List<String> list = new List<String>();
            for (int i = 0; i < getResult.Count; i++)
            {
                list.Add(getResult[i].ItemName.ToString());
            }
            return list;
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
                    lblmessage.Visible = true;
                    lblmessage.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }

                if (ddl_financialyr.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "FinancialYear", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (ddl_monthID.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "Months", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (ddl_substock.SelectedIndex == 0)
                {
                    Messagealert_.ShowMessage(lblmessage, "GenStock", 0);
                    divmsg1.Visible = true;
                    divmsg1.Attributes["class"] = "FailAlert";
                    return;
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txt_from.Text != "")
                {
                    if (Commonfunction.isValidDate(txt_To.Text) == false)
                    {
                        Messagealert_.ShowMessage(lblmessage, "ValidDate", 0);
                        divmsg1.Attributes["class"] = "FailAlert";
                        divmsg1.Visible = true;
                        txt_from.Focus();
                        return;
                    }
                }
                else
                {
                    lblmessage.Visible = false;
                }
                if (txt_To.Text != "")
                {
                    if (Commonfunction.isValidDate(txt_To.Text) == false)
                    {
                        Messagealert_.ShowMessage(lblmessage, "ValidDate", 0);
                        divmsg1.Attributes["class"] = "FailAlert";
                        divmsg1.Visible = true;
                        txt_To.Focus();
                        return;
                    }
                }
                else
                {
                    lblmessage.Visible = false;
                }
                List<UtilizationReportData> objutil = GetUtilizationItemList(0);
                if (objutil.Count > 0)
                {
                    gvutilizationlist.DataSource = objutil;
                    gvutilizationlist.DataBind();
                    gvutilizationlist.Visible = true;
                    Messagealert_.ShowMessage(lblresult, "Total:" + objutil[0].MaximumRows.ToString() + " Record(s) found.", 1);
                    divmsg3.Attributes["class"] = "SucessAlert";
                    divmsg3.Visible = true;
                    lblmessage.Visible = false;
                    lblmessage.Visible = false;
                }
                else
                {
                    gvutilizationlist.DataSource = null;
                    gvutilizationlist.DataBind();
                    gvutilizationlist.Visible = true;
                    lblresult.Visible = false;
                    divmsg3.Visible = false;
                }
            }
            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
                divmsg3.Attributes["class"] = "FailAlert";
                divmsg3.Visible = true;
            }
        }
        public List<UtilizationReportData> GetUtilizationItemList(int curIndex)
        {
            UtilizationReportData objutil = new UtilizationReportData();
            UtilizationReportBO objBO = new UtilizationReportBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime from = txt_from.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txt_from.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime to = txt_To.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txt_To.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            objutil.FinancialYearID = Convert.ToInt32(ddl_financialyr.SelectedValue == "" ? "0" : ddl_financialyr.SelectedValue);
            objutil.MonthID = Convert.ToInt32(ddl_monthID.SelectedValue == "" ? "0" : ddl_monthID.SelectedValue);
            objutil.GenStockID = Convert.ToInt32(ddl_substock.SelectedValue == "" ? "0" : ddl_substock.SelectedValue);
            string ItemID;
            string source = txt_itemname.Text.ToString();
            if (source.Contains(":"))
            {
                ItemID = source.Substring(source.LastIndexOf(':') + 1);
                objutil.ItemID = Convert.ToInt32(ItemID.Trim());
                hdnItemID.Value = ItemID.ToString();
            }
            else
            {
                objutil.ItemID = 0;
                hdnItemID.Value = "0";
            }
            objutil.DateFrom = from;
            objutil.DateTo = to;
            objutil.HospitalID = LogData.HospitalID;
            objutil.EmployeeID = LogData.EmployeeID;
            return objBO.GetUtilizationItemList(objutil);
        }
        protected void btnreset_Click(object sender, EventArgs e)
        {
            btnrese();
        }
        protected void btnrese()
        {
            gvutilizationlist.DataSource = null;
            gvutilizationlist.DataBind();
            gvutilizationlist.Visible = false;
            lblresult.Visible = false;
            lblmessage.Visible = false;
            lblresult.Visible = false;
            divmsg3.Visible = false;
            ddl_financialyr.SelectedIndex = 0;
            ddl_monthID.SelectedIndex = 0;
            ddl_substock.SelectedIndex = 0;
            txt_itemname.Text = "";
            txt_from.Text = "";
            txt_To.Text = "";
        }
    }
}
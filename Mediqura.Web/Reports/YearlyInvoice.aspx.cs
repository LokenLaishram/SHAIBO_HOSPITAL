using Mediqura.BOL.CommonBO;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.MedAnalytics;
using Mediqura.DAL;
using Mediqura.Utility;
using Mediqura.Utility.Util;
using Mediqura.Web.MedCommon;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mediqura.Web.Reports
{
    public partial class YearlyInvoice : BasePage
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
            Commonfunction.PopulateDdl(ddl_centre, mstlookup.GetLookupsList(LookupName.RunnerList));
            Commonfunction.PopulateDdl(ddl_year, mstlookup.GetLookupsList(LookupName.FinancialYearID));
            ddl_year.SelectedIndex = 1;
            btnprints.Attributes["disabled"] = "disabled";
            txt_from.Attributes["disabled"] = "disabled";
            txt_from.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            txt_to.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            int year = Convert.ToInt32(ddl_year.SelectedItem.Text);
            getdaterange(year);
            getdata(1);
        }
        protected void ddl_year_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl_year.SelectedIndex > 0)
            {
                lblmessage.Visible = false;
                int year = Convert.ToInt32(ddl_year.SelectedItem.Text == "" ? "0" : ddl_year.SelectedItem.Text);
                getdaterange(year);
                getdata(1);
            }
            else
            {
                Gvinvoicelist.DataSource = null;
                Gvinvoicelist.DataBind();
                lbl_totalinvoice.Text = "0.0";
                lbl_netamount.Text = "0.0";
                lbl_totaltest.Text = "0.0";
                lblresult.Visible = false;
                Messagealert_.ShowMessage(lblmessage, "Please select year", 0);
                divmsg.Attributes["class"] = "FailAlert";
                divmsg.Visible = true;
                ddl_year.Focus();
                return;
            }
        }
        protected void getdaterange(int year)
        {
            DateTime startDate = new DateTime(year, 1, 1);
            DateTime endDate = new DateTime(year, 12, 31);
            txt_from.Text = startDate.ToString("dd/MM/yyyy");
            txt_to.Text = endDate.ToString("dd/MM/yyyy");
        }
        protected void getdata(int page)
        {

            if (ddl_year.SelectedIndex == 0)
            {
                Messagealert_.ShowMessage(lblmessage, "Please select year", 0);
                divmsg.Attributes["class"] = "FailAlert";
                divmsg.Visible = true;
                ddl_year.Focus();
                return;
            }
            else
            {
                lblmessage.Visible = false;
            }

            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime from = txt_from.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txt_from.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txt_to.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txt_to.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);

            // Facility
            SqlParameter[] arParms = new SqlParameter[6];

            arParms[0] = new SqlParameter("@from", SqlDbType.DateTime);
            arParms[0].Value = from;

            arParms[1] = new SqlParameter("@To", SqlDbType.DateTime);
            arParms[1].Value = To;

            arParms[2] = new SqlParameter("@CentreID", SqlDbType.Int);
            arParms[2].Value = ddl_centre.SelectedValue == "" ? "0" : ddl_centre.SelectedValue;

            arParms[3] = new SqlParameter("@YearID", SqlDbType.Int);
            arParms[3].Value = ddl_year.SelectedItem.Text == "--Select--" ? "0" : ddl_year.SelectedItem.Text;

            arParms[4] = new SqlParameter("@pageno", SqlDbType.Int);
            arParms[4].Value = page;

            arParms[5] = new SqlParameter("@pageSize", SqlDbType.Int);
            arParms[5].Value = Convert.ToInt32(ddl_show.SelectedValue == "10000" ? lbl_totalinvoice.Text : ddl_show.SelectedValue);

            Gvinvoicelist.PageSize = Convert.ToInt32(ddl_show.SelectedValue == "10000" ? lbl_totalinvoice.Text : ddl_show.SelectedValue);

            SqlDataReader sqlReader = null;
            sqlReader = SqlHelper.ExecuteReader(GlobalConstant.ConnectionString, CommandType.StoredProcedure, "usp_MDQ_Get_Centres_invoices", arParms);
            List<InvoiceData> InvoiceList = ORHelper<InvoiceData>.FromDataReaderToList(sqlReader);
            if (InvoiceList.Count > 0)
            {
                Gvinvoicelist.VirtualItemCount = InvoiceList[0].TotalInvoice;//total item is required for custom paging
                Gvinvoicelist.PageIndex = page - 1;
                Gvinvoicelist.DataSource = InvoiceList;
                Gvinvoicelist.DataBind();
                Messagealert_.ShowMessage(lblresult, "Total:" + InvoiceList[0].TotalInvoice.ToString() + " Record(s) found.", 1);
                divmsg3.Attributes["class"] = "SucessAlert";
                divmsg3.Visible = true;
                lbl_totalinvoice.Text = InvoiceList[0].TotalInvoice.ToString();
                lbl_totaltest.Text = InvoiceList[0].TotalTestCount.ToString();
                lbl_netamount.Text = "INR:" + Commonfunction.Getrounding((InvoiceList[0].TotalAmount).ToString());
                btnprints.Attributes.Remove("disabled");
                lblresult.Visible = true;
                if (LogData.RoleID == 1)
                {
                    idnetamount.Visible = true;
                }
                else
                {
                    idnetamount.Visible = false;
                }
            }
            else
            {
                Gvinvoicelist.DataSource = null;
                Gvinvoicelist.DataBind();
                lbl_totalinvoice.Text = "0.0";
                lbl_netamount.Text = "0.0";
                lbl_totaltest.Text = "0.0";
                lblresult.Visible = false;
                if (LogData.RoleID == 1)
                {
                    idnetamount.Visible = true;
                }
                else
                {
                    idnetamount.Visible = false;
                }

            }
        }
        protected void ddl_show_SelectedIndexChanged(object sender, EventArgs e)
        {
            getdata(1);
        }
        protected void btn_search_Click(object sender, EventArgs e)
        {

            getdata(1);
        }
        protected void Gvinvoicelist_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Gvinvoicelist.PageIndex = e.NewPageIndex;
            getdata(Convert.ToInt32(e.NewPageIndex + 1));
        }
        protected void ddl_centre_SelectedIndexChanged(object sender, EventArgs e)
        {
            getdata(1);
        }
        protected void btnprint_Click(object sender, EventArgs e)
        {
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime from = txt_from.Text.Trim() == "" ? GlobalConstant.MinSQLDateTime : DateTime.Parse(txt_from.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txt_to.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txt_to.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);

            string param = "option=PrintInvoice&from=" + from + "&To=" + To + "&CentreID=" + ddl_centre.SelectedValue + "&YearID=" + ddl_year.SelectedItem.Text;

            Commonfunction common = new Commonfunction();
            string ecryptstring = common.Encrypt(param);
            string baseurl = "../Reports/ReportViewerInvoice.aspx?ID=" + ecryptstring;

            string fullURL = "window.open('" + baseurl + "', '_blank');";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_New_Tab", fullURL, true);
        }
        protected void txt_from_TextChanged(object sender, EventArgs e)
        {
            getdata(1);
        }
        protected void txt_to_TextChanged(object sender, EventArgs e)
        {
            getdata(1);
        }
        protected void btnresets_Click(object sender, EventArgs e)
        {
            ddl_year.SelectedIndex = 1;
            ddl_show.SelectedIndex = 0;
            ddl_year.SelectedIndex = 1;
            btnprints.Attributes["disabled"] = "disabled";
            txt_from.Attributes["disabled"] = "disabled";
            txt_from.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            txt_to.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            int year = Convert.ToInt32(ddl_year.SelectedItem.Text);
            getdaterange(year);
            getdata(1);
        }
    }
}
using Mediqura.BOL.CommonBO;
using Mediqura.BOL.MIS;
using Mediqura.CommonData.Common;
using Mediqura.CommonData.MIS;
using Mediqura.Utility.Logging;
using Mediqura.Web.MedCommon;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Mediqura.Web.Reports
{
    public partial class StaffFundReport : BasePage
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
            Commonfunction.PopulateDdl(ddl_staffFund_service, mstlookup.GetLookupsList(LookupName.PaymentType));
            txtdatefrom.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            txtto.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            if (LogData.PrintEnable == 0)
            {
                btn_print.Attributes["disabled"] = "disabled";
            }
            else
            {
                btn_print.Attributes.Remove("disabled");
            }
        }
        protected void btnsearch_Click(object sender, EventArgs e)
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

            bindgrid();
        }

        protected void bindgrid()
        {
            try
            {

                List<StaffFundReportData> objData = GetTransactionList(0);
                if (objData.Count > 0)
                {
                    Gv_staffFund.DataSource = objData;
                    Gv_staffFund.DataBind();
                    Gv_staffFund.Visible = true;
                    txtTotalAmount.Text = Commonfunction.Getrounding(objData[0].TotalAmount.ToString());
                    txtTotalDiscount.Text = Commonfunction.Getrounding(objData[0].TotalDiscount.ToString());
                   


                }
                else
                {
                    Gv_staffFund.DataSource = null;
                    Gv_staffFund.DataBind();
                    Gv_staffFund.Visible = true;
                    lblresult.Visible = false;
                }
            }

            catch (Exception ex)
            {
                LogManager.LogMedError(ex, EnumErrorLogSourceTier.Web);
                Messagealert_.ShowMessage(lblmessage, "system", 0);
            }
        }
        public List<StaffFundReportData> GetTransactionList(int curIndex)
        {
            StaffFundReportData ObjData = new StaffFundReportData();
            StaffFundBO objgBO = new StaffFundBO();
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime from = txtdatefrom.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txtto.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            ObjData.ServiceID = Convert.ToInt32(ddl_staffFund_service.SelectedValue == "" ? "0" : ddl_staffFund_service.SelectedValue);
            ObjData.EmpName = LogData.EmpName;

            ObjData.DateFrom = from;
            ObjData.DateTo = To;

            return objgBO.GetTransactionList(ObjData);
        }
        protected void btnresets_Click(object sender, EventArgs e)
        {
            ViewState["ID"] = null;
            lblmessage.Visible = false;
            txtdatefrom.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            txtto.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            ddl_staffFund_service.SelectedIndex = 0;
            Gv_staffFund.DataSource = null;
            Gv_staffFund.DataBind();
            Gv_staffFund.Visible = true;
            txtTotalAmount.Text = "0.00";
            txtTotalDiscount.Text = "0.00";



        }

        protected void btn_print_Click(object sender, EventArgs e)
        {
            IFormatProvider option = new System.Globalization.CultureInfo("en-GB", true);
            DateTime from = txtdatefrom.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtdatefrom.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            DateTime To = txtto.Text.Trim() == "" ? System.DateTime.Now : DateTime.Parse(txtto.Text.Trim(), option, System.Globalization.DateTimeStyles.NoCurrentDateDefault);
            int service = Convert.ToInt32(ddl_staffFund_service.SelectedValue == "" ? "0" : ddl_staffFund_service.SelectedValue);
            string name = LogData.EmpName;
       //     string url = "../MIS/Reports/ReportViewer.aspx?option=CollectionReport&ledger=" + ledger.ToString() + "&Ttype=" + tType.ToString() + "&CollectedBy=" + collectedBy.ToString() + "&status=" + AcntStat.ToString() + "&Datefrom=" + (from.ToString("yyyy-MM-dd") + " " + timefrom).ToString() + "&Dateto=" + (To.ToString("yyyy-MM-dd") + " " + timeto).ToString();
          //  string fullURL = "window.open('" + url + "', '_blank');";
        //    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_New_Tab", fullURL, true);
        }
    

        protected void Gv_staffFund_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label isSubHeading = (Label)e.Row.FindControl("lblIsSubHeading");
                Label lblVoucher = (Label)e.Row.FindControl("lblVoucher");
                Label lblTransactionAmount = (Label)e.Row.FindControl("lblTransactionAmount");

                if (Convert.ToInt32(isSubHeading.Text) == 1)
                {
                    if (Convert.ToDecimal(lblTransactionAmount.Text) > 0)
                    {
                        e.Row.BackColor = Color.FromName("#cfede3");
                        lblVoucher.ForeColor = Color.FromName("#fd0808");
                        lblTransactionAmount.ForeColor = Color.FromName("#fd0808");
                        GridView editGrid = sender as GridView;

                        e.Row.Cells[0].Controls.Clear();

                        e.Row.Cells[1].ColumnSpan = 2;
                        e.Row.Cells[2].Visible = false;

                        e.Row.Cells[3].Controls.Clear();

                        e.Row.Cells[4].Controls.Clear();

                        e.Row.Cells[5].Controls.Clear();

                        e.Row.Cells[6].Controls.Clear();
                    }
                    else
                    {
                        e.Row.Visible = false;
                    }



                }
                else if (Convert.ToInt32(isSubHeading.Text) == 2)
                {
                    if (Convert.ToDecimal(lblTransactionAmount.Text) > 0)
                    {
                        e.Row.BackColor = Color.FromName("#33aa99");
                        lblVoucher.ForeColor = Color.FromName("#FFFFFF");
                        GridView editGrid = sender as GridView;

                        e.Row.Cells[0].Controls.Clear();
                        e.Row.Cells[1].ColumnSpan = 2;
                        e.Row.Cells[2].Visible = false;

                        e.Row.Cells[3].Controls.Clear();

                        e.Row.Cells[4].Controls.Clear();

                        e.Row.Cells[5].Controls.Clear();

                        e.Row.Cells[6].Controls.Clear();

                        e.Row.Cells[7].Controls.Clear();
                    }
                    else
                    {
                        e.Row.Visible = false;
                    }
                }


            }
        }
    }
}